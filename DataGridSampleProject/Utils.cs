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
        /// Load Employee list from xml file. File will be created if it does not exists. 
        /// </summary>
        /// <param name="filePath">Path of xml file</param>
        /// <returns>List of employees</returns>
        public static List<Employee> LoadEmployees(string filePath)
        {

            // Return null if filepath does not end with .xml
            if (!Regex.IsMatch(filePath, @"\.xml$", RegexOptions.IgnoreCase))
            {

                Trace.WriteLine($"[Utils] LoadEmployees(); invalid xml filepath: {filePath}. ");
                return null;
            }

            // Create xml file if it does not exist. 
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

            // Return empty list if file content is null. 
            if (String.IsNullOrEmpty(serializedXmlString))
            {
                return new List<Employee>();
            }

            // Deserialize serialized string. Return status failure if deserialization fails.  
            List<Employee> employeeList = DeserializeFromXml<List<Employee>>(serializedXmlString);

            // if list is null, then content is not serialized employee list. 
            if (employeeList == null)
            {

                Trace.WriteLine($"[Utils] LoadEmployees(); Xml file Deserialization failed: {nameof(DeserializeFromXml)}() returned null. ");
                return null; 
            }
            else
            {

                return employeeList; 
            }
        }

        /// <summary>
        /// Save employee list to xml file
        /// </summary>
        /// <param name="employeeList"> list to be saved </param>
        /// <param name="filePath"> Path of xml file </param>
        /// <returns> Saving status </returns> 
        public static bool SaveEmployees(List<Employee> employeeList, string filePath)
        {

            // Return status failure if path is not of xml file. 
            if (!Regex.IsMatch(filePath, @"\.xml$", RegexOptions.IgnoreCase))
            {

                Trace.WriteLine($"[Utils] SaveEmployees(): {filePath} is not an xml filepath. "); 
                return false; 
            }

            // Serialize employee list to xml.  
            string serializedXmlString = SerializeToXml<List<Employee>>(employeeList);
            if (serializedXmlString == null)
            {

                // Return status failure if serialized string is null. This is just debug helper
                Trace.WriteLine($"[Utils] SaveEmployees(): SerializeToXml returned invalid data"); 
                return false;
            }

            // Overwrite serialized string in xml file. 
            try
            {

                // File.WriteAllText will create file if it is not there. 
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
        /// <param name="employee"> Employee object to be added </param>
        /// <param name="filePath"> Path of Xml file </param>
        /// <returns> Employee record insertion status </returns>
        public static bool AddEmployee(Employee employee, string filePath)
        {

            // Load employees. 
            List<Employee> employeeList = LoadEmployees(filePath);

            // Add employee to the list
            employeeList.Add(employee);

            // Save employee list. Return status failure if saving fails.   
            bool status = SaveEmployees(employeeList, filePath);
            if (!status)
            {

                Trace.WriteLine($"[Utils] AddEmployee(): {nameof(SaveEmployees)}() returned status error. ");
                return false;
            }
            else
            {

                return true; 
            }
        }

        /// <summary>
        /// Edit employee record data
        /// </summary>
        /// <param name="updatedEmployee">Employee object containing updated data</param>
        /// <param name="filePath"> Path of xml file</param>
        /// <returns> Employee record edit status </returns> 
        public static bool EditEmployee(Employee updatedEmployee, string filePath)
        {

            // Load employee list.   
            List<Employee> employeeList = LoadEmployees(filePath);

            // Return failure status if no file is there to edit. 
            if (employeeList.Count == 0)
            {

                Trace.WriteLine($"[Utils] EditEmployee(); Unexpected behaviour: {nameof(employeeList)} of type {nameof(List<Employee>)}does not contain any employee record to edit. ");
                return false; 
            }

            // Get employee record from employee list. Return status failure if record not found. 
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

            // Save the data. Return status failure if save fails. 
            bool status = SaveEmployees(employeeList, filePath);
            if (!status)
            {

                Trace.WriteLine($"[Utils] EditEmployee(): {nameof(SaveEmployees)}() returned status error. ");
                return false;
            }
            else
            {

                return true; 
            }
        }

        /// <summary>
        /// Delete employee from employee list stored in Xml file
        /// </summary>
        /// <param name="filePath"> Filepath of xml database </param>
        /// <param name="id"> Id of employee to be deleted </param>
        /// <returns> Deletion status </returns>
        public static bool DeleteEmployee(string filePath, int id)
        {

            // Load employee list
            List<Employee> employeeList = LoadEmployees(filePath);

            // return failure status if employee does not exists. 
            if (employeeList.Count == 0)
            {

                Trace.WriteLine($"[Utils] DeleteEmployee(); Unexpected behaviour: {nameof(employeeList)} of type {nameof(List<Employee>)}does not contain any employee record to edit. ");
                return false; 
            }

            // Remove record from employee list 
            int count = employeeList.RemoveAll(u => u.Id == id);

            // Application malfunction conditions
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

            // Save data to xml file and get save status
            bool status = SaveEmployees(employeeList, filePath);

            // Return status failure if saving failed. 
            if (!status)
            {

                Trace.WriteLine($"[Utils] DeleteEmployee(): {nameof(SaveEmployees)}() failed and returned status error. ");
                return false;
            }
            else
            {

                return true;
            }
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

            // Return default(T) if string is empty. 
            if (String.IsNullOrEmpty(serializedXmlString))
            {

                Trace.WriteLine($"[Utils] DeserializeFromXml(): Argument is null or empty. ");
                return default(T); 
            }
            else
            {

                // Deserialize string and return T object. 
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

            // Create file. Return status failure if file creation fails.  
            try
            {

                using (File.Create(filePath)) { }
                return true; 
            }
            catch
            {

                Trace.WriteLine($"[Utils] CreateFile(): File creation failed. ");
                return false; 
            }
        }

        /// <summary>
        /// Read contents from file
        /// </summary>
        /// <param name="filePath"> Path of file </param>
        /// <returns>Contents of the file</returns>
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