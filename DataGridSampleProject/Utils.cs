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

        /// <summary>
        /// Load Employee list from storage xml file. File will be created if it does not exists. 
        /// </summary>
        /// <param name="filePath">Path of xml file</param>
        /// <returns>List of Employees</returns>
        public static List<Employee> LoadEmployees(string filePath)
        {

            // Return null if filepath does not end with .xml
            if (!Regex.IsMatch(filePath, @"\.xml$", RegexOptions.IgnoreCase))
            {

                Trace.WriteLine($"[Utils] LoadEmployees(); invalid xml filepath: {filePath}. ");
                return null;
            }

            // If file does not exists, create one. 
            if (!File.Exists(filePath))
            {
                bool status = CreateFile(filePath); 
                if (!status)
                {

                    Trace.WriteLine($"[Utils] LoadEmployees(): {nameof(CreateFile)}() failed and returned status error. ");
                    return null;
                }
                return new List<Employee>(); 
            }

            // Read file content. 
            string serializedXmlString = ReadFromFile(filePath, false);
            if (String.IsNullOrEmpty(serializedXmlString))
            {
                return new List<Employee>(); 
            }

            // Deserialize serialized string. If success, return employee list and if failed, return status error. 
            List<Employee> employeeList = DeserializeFromXml<List<Employee>>(serializedXmlString);

            if (employeeList == null)
            {

                Trace.WriteLine($"[Utils] LoadEmployees(); Xml file Deserialization failed: {nameof(DeserializeFromXml)}() returned null. ");
            }

            return employeeList; 
        }

        /// <summary>
        /// Save an Employee list in a file
        /// </summary>
        /// <param name="employeeList"></param>
        /// <param name="filePath"></param>
        /// <returns>execution status</returns> 
        public static bool SaveEmployees(List<Employee> employeeList, string filePath)
        {

            // Check whether filepath is of an xml file
            if (!Regex.IsMatch(filePath, @"\.xml$", RegexOptions.IgnoreCase))
            {

                Trace.WriteLine($"[Utils] SaveEmployees(): {filePath} is not an xml filepath. "); 
                return false; 
            }

            // if filePath is invalid, return false. 
            if (!File.Exists(filePath))
            {

                Trace.WriteLine($"[Utils] SaveEmployees(): File does not exist in filepath: {filePath}"); 
                return false; 
            }

            // Serialize the contents. 
            string serializedXmlString = SerializeToXml<List<Employee>>(employeeList);

            // If content is null, return false
            if (serializedXmlString == null)
            {

                Trace.WriteLine($"[Utils] SaveEmployees(): SerializeToXml returned invalid data"); 
                return false;
            }

            // Overwrite the contents. 
            try
            {

                File.WriteAllText(filePath, serializedXmlString);
                return true; 
            }
            catch (Exception ex)
            {

                Trace.WriteLine($"[Utils] SaveEmployees(): Failed to write into filepath: {filePath}. Error. {ex.Message} ");
                return false; 
            }
        }

        /// <summary>
        /// Add employee to the database 
        /// </summary>
        /// <param name="employee">Employee object to be added</param>
        /// <param name="filePath"></param>
        /// <returns>status</returns>
        public static bool AddEmployee(Employee employee, string filePath)
        {

            // Load employees. Return status error if loading fails. 
            List<Employee> employeeList = LoadEmployees(filePath);

            // if (employeeList == null)
            // {

            //     Trace.WriteLine($"[Utils] AddEmployee(): {nameof(LoadEmployees)} function has falied and returned null.");
            //     return false;
            // }

            // Add employee to the list
            employeeList.Add(employee);

            // Save employee list. Return status error when saving fails.  
            bool status = SaveEmployees(employeeList, filePath);
            if (!status)
            {

                Trace.WriteLine($"[Utils] AddEmployee(): {nameof(SaveEmployees)}() returned status error. ");
                return false;
            }

            // return status ok 
            return true; 
        }

        /// <summary>
        /// Edit data of an employee in xml database. 
        /// </summary>
        /// <param name="updatedEmployee">Employee object containing updated data</param>
        /// <param name="filePath"></param>
        /// <returns>execution status</returns> 
        public static bool EditEmployee(Employee updatedEmployee, string filePath)
        {

            // Load employee list. Return status error if employee list is null or empty.  
            List<Employee> employeeList = LoadEmployees(filePath);

            // if (employeeList == null)
            // {

            //     Trace.WriteLine($"[Utils] EditEmployee(): {nameof(LoadEmployees)} function has falied and returned null.");
            //     return false;
            // }
            // else if (employeeList.Count == 0)
            if (employeeList.Count == 0)
            {

                Trace.WriteLine($"[Utils] EditEmployee(); Unexpected behaviour: {nameof(employeeList)} of type {nameof(List<Employee>)}does not contain any employee record to edit. ");
                return false; 
            }

            // Get employee record from employee list. Return status error if record not found. 
            Employee employee = employeeList.FirstOrDefault(u => u.Id == updatedEmployee.Id);

            if (employee == null)
            {

                Trace.WriteLine($"[Utils] EditEmployee(); {nameof(updatedEmployee)} does not exist in employeeList. ");
                return false; 
            }

            // Update employee data 
            employee.Name = updatedEmployee.Name;
            employee.Designation = updatedEmployee.Designation;
            employee.EmailId = updatedEmployee.EmailId;
            employee.Reporter = updatedEmployee.Reporter;
            employee.Reportee = updatedEmployee.Reportee;
            employee.ProductLineResponsibility = updatedEmployee.ProductLineResponsibility;
            employee.WorkExperience = updatedEmployee.WorkExperience; 

            // Saving the data back to xml file. Return status error if saving failed. 
            bool status = SaveEmployees(employeeList, filePath);
            if (!status)
            {

                Trace.WriteLine($"[Utils] EditEmployee(): {nameof(SaveEmployees)}() returned status error. "); 
                return false; 
            }

            // Return status ok
            return true; 
        }

        public static bool DeleteEmployee(string filePath, int id)
        {

            // Load employee list
            List<Employee> employeeList = LoadEmployees(filePath);

            // if (employeeList == null)
            // {

            //     Trace.WriteLine($"[Utils] DeleteEmployee(); {nameof(LoadEmployees)}() malfunctioned and returned null. ");
            //     return false;
            // }
            // else if (employeeList.Count == 0)
            if (employeeList.Count == 0)
            {

                Trace.WriteLine($"[Utils] DeleteEmployee(); Unexpected behaviour: {nameof(employeeList)} of type {nameof(List<Employee>)}does not contain any employee record to edit. ");
                return false; 
            }

            // Remove record from employee list 
            int count = employeeList.RemoveAll(u => u.Id == id);

            if (count != 1)
            {
                if (count == 0)
                {
                    Trace.WriteLine($"[Utils] DeleteEmployee(): Record with id {id} does not exist in the employee list. ");
                }
                else
                {
                    Trace.WriteLine($"[Utils] DeleteEmployee(): Employee list contained duplicate entries. ");
                }
                return false;
            }

            // Save back the data to xml database
            bool status = SaveEmployees(employeeList, filePath);

            if (!status)
            {

                Trace.WriteLine($"[Utils] DeleteEmployee(): {nameof(SaveEmployees)}() failed and returned status error. ");
                return false;
            }

            // return status OK
            return true;
        }


        // Utility functions to make above functions work
        // FIXME: Make some functions private and segregate the namespaces. 


        /// <summary>
        /// Serialize a generic object to xml string
        /// </summary>
        /// <param name="obj">Generic object</param>
        /// <typeparam name="T">Type of generic object</typeparam>
        /// <returns>xml string</returns>
        private static string SerializeToXml<T>(T obj)
        {

            if (obj == null)
            {

                Trace.WriteLine("[Utils] SerializeToXml(): Argument is null");
                return null; 
            }

            try
            {

                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (StringWriter stringWriter = new StringWriter())
                {

                    serializer.Serialize(stringWriter, obj);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {

                Trace.WriteLine($"[Utils] SerializeToXml(): Serialization failed. Error: {ex.Message}. ");
                return null; 
            }
        }

        /// <summary>
        /// Deserialize xml string to object of type T 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedXmlString">Serialized string</param>
        /// <returns>Object of type T</returns> 
        private static T DeserializeFromXml<T>(string serializedXmlString)
        {

            if (String.IsNullOrEmpty(serializedXmlString))
            {
                Trace.WriteLine($"[Utils] DeserializeFromXml(): Argument is null or empty. ");
                // return null;
                return default(T); 
            }
            else
            {
                try
                {

                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    using (StringReader stringReader = new StringReader(serializedXmlString))
                    {

                        return (T)serializer.Deserialize(stringReader);
                    }
                }
                catch (Exception ex)
                {

                    Trace.WriteLine($"[Utils] DeserializeFromXml(): XML Deserialization failed. Error: {ex.Message}");
                    // return null; 
                    return default(T); 
                }
            }
        }

        /// <summary>
        /// Create file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>File creation status</returns>
        private static bool CreateFile(string filePath)
        {
            // If file already exists, return status error
            if (File.Exists(filePath))
            {

                Trace.WriteLine($"[Utils] CreateFile(): File already exists in path {filePath}. ");
                return false;
            }

            // Create file. If creation fails, return status error. 
            // FIXME: Advanced ErrorHandling: put the below line inside try catch
            try
            {

                using (File.Create(filePath)) { }
                return true; 
            }
            catch
            {

                Trace.WriteLine($"[Utils] CreateFile(): File creation failed. Please debug the code, error handling is poorly implemented in this function.");
                return false; 
            }
        }

        /// <summary>
        /// Read contents from a file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Content of file</returns>
        private static string ReadFromFile(string filePath, bool writeTrace = true)
        {

            // return null if file does not exists. 
            if (!File.Exists(filePath))
            {

                if (writeTrace)
                {
                    Trace.WriteLine($"[Utils] ReadFromFile(): File does not exist in this filepath {filePath}");
                }
                return null;
            }

            return File.ReadAllText(filePath);
        }
    }
}