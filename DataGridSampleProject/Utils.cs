/******************************************************************************
* Filename    = Utils.cs
*
* Author      = Amithabh A
*
* Product     = EmployeeDatabase
* 
* Project     = Backend
*
* Description = Implement Utility functions
*****************************************************************************/
using System;
using System.IO;
using System.Linq;
using System.Diagnostics; 
using System.Xml.Serialization;
using System.Collections.Generic; 
using System.Text.RegularExpressions;

namespace DataGridSampleProject
{
    public class Utils
    {

        public static List<Employee>? LoadEmployees(string filePath)
        {
            // if string does not end with .xml
            if (!Regex.IsMatch(filePath, @"\.xml$", RegexOptions.IgnoreCase))
            {

                Trace.WriteLine($"[Utils] LoadEmployees(): {filePath} is not an xml filepath. "); 
                return null; 
            }

            // If filePath is invalid, return null
            if (!File.Exists(filePath))
            {

                Trace.WriteLine($"[Utils] LoadEmployees(): {nameof(filePath)} variable content is invalid. {filePath} does not exists. "); 
                return null; 
            }

            string serializedXmlString = ReadFromFile(filePath);

            // If content is empty, create an empty file and return empty list
            if (String.IsNullOrEmpty(serializedXmlString))
            {
                return new List<Employee>();
            }


            // If content is not valid, exception is thrown. 
            try
            {
                return DeserializeFromXml<List<Employee>>(ReadFromFile(serializedXmlString));
            }
            catch 
            {
                Trace.WriteLine($"[Utils] LoadEmployees(): {nameof(filePath)} contains invalid data to process and unable to deserialize into {nameof(List<Employee>)}. "); 
                return null; 
            }
        }

        public static void SaveEmployees(List<Employee> employeeList, string filePath)
        {
            // Logic 
            string serializedXmlString = SerializeToXml<List<Employee>>(employeeList);
            WriteToFile(serializedXmlString, filePath);
        }

        public static List<Employee>? AddEmployee(Employee employee, string filePath)
        {

            List<Employee>? employeeList = LoadEmployees(filePath);

            if (employeeList == null)
            {

                Trace.WriteLine($"[Utils] AddEmployee(): {nameof(LoadEmployees)} function has falied and returned null.");
                return null; 
            }
            else
            {

                employeeList.Add(employee);
                SaveEmployees(employeeList, filePath);

                return employeeList;
            }
        }

        public static List<Employee>? EditEmployee(Employee updatedEmployee, string filePath)
        {

            List<Employee>? employeeList = LoadEmployees(filePath);

            if (employeeList == null)
            {

                Trace.WriteLine($"[Utils] EditEmployee(): {nameof(LoadEmployees)} function has falied and returned null.");
                return null;
            }
            else if (employeeList.Count == 0)
            {

                Trace.WriteLine($"[Utils] EditEmployee(); Unexpected behaviour: {nameof(employeeList)} of type {nameof(List<Employee>)}does not contain any employee record to edit. ");
                return null; 
            }
            else
            {

                Employee employee = employeeList.FirstOrDefault(u => u.Id == updatedEmployee.Id);

                if (employee == null)
                {
                    Trace.WriteLine($"[Utils] EditEmployee(); {nameof(updatedEmployee)} does not exist in employeeList. ");
                    return null; 
                }
                else
                {

                    // Updating the employee information 
                    employee.Name = updatedEmployee.Name;
                    employee.Designation = updatedEmployee.Designation;
                    employee.EmailId = updatedEmployee.EmailId;
                    employee.Reporter = updatedEmployee.Reporter;
                    employee.Reportee = updatedEmployee.Reportee;
                    employee.ProductLineResponsibility = updatedEmployee.ProductLineResponsibility;

                    // Saving the data back to xml file
                    SaveEmployees(employeeList, filePath);

                    return employeeList;
                }
            }
        }

        /// <summary>
        /// Delete an Employee object from Employee List
        /// </summary>
        /// <param name="id">Id of employee to be deleted</param>
        /// <param name="filePath">Path of xml file</param>
        /// <returns>Updated employee list</returns>
        public static List<Employee> DeleteEmployee(string filePath, int id)
        {

            List<Employee> employeeList = LoadEmployees(filePath);

            employeeList.RemoveAll(u => u.Id == id);
            SaveEmployees(employeeList, filePath);

            return employeeList;
        }


        // Utility functions to make above functions work
        // FIXME: Make some functions private and segregate the namespaces. 


        /// <summary>
        /// Serialize a generic object to xml string
        /// </summary>
        /// <param name="obj">Generic object</param>
        /// <typeparam name="T">Type of generic object</typeparam>
        /// <returns>xml string</returns>
        public static string SerializeToXml<T>(T obj)
        {

            if (obj == null)
            {

                Trace.WriteLine("[Error] [Utils] [SerializeToXml] Argument is null");
                throw new ArgumentNullException("[Utils] [SerializeToXml] Argument cannot be null");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));

            using (StringWriter stringWriter = new StringWriter())
            {

                serializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        /// <summary>
        /// Deserialize xml string to object of type T 
        /// </summary>
        /// <typeparam name="T">Return object type</typeparam>
        /// <param name="serializedXmlString">Serialized string</param>
        /// <returns>Object of type T</returns> 
        public static T DeserializeFromXml<T>(string serializedXmlString)
        {
            if (string.IsNullOrEmpty(serializedXmlString))
            {
                Trace.WriteLine("[Error] [Utils] [DeserializeFromXml] Argument is null");
                throw new ArgumentNullException("[Utils] [DeserializeFromXml] Argument cannot be null");
            }

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader stringReader = new StringReader(serializedXmlString))
            {

                try
                {

                    T obj = (T)serializer.Deserialize(stringReader);
                    return obj;
                }
                catch 
                {

                    throw new InvalidDataException("[Utils] [DeserializeFromXml] Invalid input data");
                }
            }
        }

        /// <summary>
        /// Read contents from a file
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <returns>File content in String</returns>
        public static string ReadFromFile(string filePath)
        {

            if (File.exists(filePath))
            {
                return File.ReadAllText(filePath); 
            }
            else
            {
                throw FileNotFoundException(nameof(filePath), "File does not exist in the path. "); 
            }
        }

        /// <summary>
        /// Write contents to a file
        /// </summary>
        /// <param name="content">content in string</param>
        /// <param name="filePath">File to which content to be written</param>
        /// <returns></returns>
        public static void WriteToFile(string content, string filePath)
        {
            File.WriteAllText(filePath, content);
        }

    }
}