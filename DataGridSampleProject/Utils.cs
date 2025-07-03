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
using System.Diagnostics; 
using System.Xml.Serialization;
using System.Text.RegularExpressions; 

namespace DataGridSampleProject
{
    public class Utils
    {
        private static readonly string _xmlfile = "employees.xml";

        public static List<Employee> LoadEmployees(string filePath)
        {
            // if string does not end with .xml
            if (!Regex.IsMatch(filePath, @"\.xml$", RegexOptions.IgnoreCase))
            {

                throw new ArgumentException($"Invalid Argument for variable {nameof(filePath)}");
            }

            string serializedXmlString = ReadFromFile(filePath);

            // If content is empty, return empty list
            if (String.IsNullOrEmpty(serializedXmlString))
            {
                return new List<Employee>(); 
            }


            // If content is not valid, exception is thrown. 
            try
            {
                return DeserializeFromXml<List<Employee>>(ReadFromFile(serializedXmlString));
            }
            catch (Exception ex)
            {
                throw new InvalidDataException($"File {nameof(filePath)} contains invalid data and cannot be deserialized; {ex.message}"); 
            }
        }

        public static void SaveEmployees(List<Employee> employeeList, string filePath)
        {
            // Logic 
            string serializedXmlString = SerializeToXml<List<Employee>>(employeeList);
            WriteToFile(serializedXmlString, filePath);
        }

        public static List<Employee> AddPerson(Employee employee, string filePath)
        {

            List<Employee> employeeList = LoadEmployees(filePath);  

            employeeList.Add(employee);
            SaveEmployees(employeeList, filePath);

            return employeeList; 
        }

        public static List<Employee> EditEmployee(Employee updatedEmployee, string filePath)
        {

            List<Employee> employeeList = LoadEmployees(filePath); 

            Employee employee = employeeList.FirstOrDefault(u => u.Id == updatedEmployee.Id);

            if (employee != null)
            {

                employee.Name = updatedEmployee.Name;
                employee.Designation = updatedEmployee.Designation;
                employee.EmailId = updatedEmployee.EmailId;
                employee.Reporter = updatedEmployee.Reporter;
                employee.Reportee = updatedEmployee.Reportee;
                employee.ProductLineResponsibility = updatedEmployee.ProductLineResponsibility;
            }

            SaveEmployees(employeeList, filePath);

            return employeeList; 
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

                T obj = (T)serializer.Deserialize(stringReader) ?? throw new InvalidDataException("[Utils] [DeserializeFromXml] Invalid input data");
                return obj;
            }
        }

        /// <summary>
        /// Read contents from a file
        /// </summary>
        /// <param name="filePath">Path of the file</param>
        /// <returns>File content in String</returns>
        public static string ReadFromFile(string filePath)
        {
            return File.ReadAllText(filePath);
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
