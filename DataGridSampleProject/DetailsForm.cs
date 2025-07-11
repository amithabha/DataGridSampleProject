/******************************************************************************
* Filename    = DetailsForm.cs
*
* Author      = Amithabh A
*
* Product     = Details Form 
* 
* Project     = EmployeeDatabase
*
* Description = Implements form where employee data is added / edited
*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics; 
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DataGridSampleProject
{

    /// <summary>
    /// Details Form implementation
    /// <summary>
    public partial class DetailsForm : Form
    {

        private Employee _editEmployee;
        private bool _editMode;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="editMode"> Boolean value of edit status </param>
        /// <param name="employee"> Employee to be edited </param>
        public DetailsForm(bool editMode = false, Employee employee = null)
        {

            _editMode = editMode;

            // employee should not be null when editing
            if (_editMode)
            {

                if (employee == null)
                {
                    Trace.WriteLine($"[DetailsForm.cs] [DetailsForm] Constructor; Argument employee should not be null. ");
                    return; 
                }

                _editEmployee = employee;
            }


            InitializeComponent();
            // FIXME: Form title can be customised according to edit mode. 
            this.Load += LoadDetails;
        }

        /// <summary>
        /// Form will be populated with details of employee to be edited, 
        /// if forms is opened in edit mode. 
        /// </summary>
        private void LoadDetails(object sender, EventArgs e)
        {

            if (_editMode)
            {
                PopulateEditForm(_editEmployee);
            }
        }

        /// <summary>
        /// Event handler that handles submitted employee data
        /// </summary>
        private void submit_Click(object sender, EventArgs e)
        {

            // Check whether entered data is valid. 
            bool validity = IsInputValid(_editMode);
            if (!validity)
            {

                MessageBox.Show("Please correctly enter the data. ");
                return;
            }

            // Create employee object with valid data
            Employee employee = CreateEmployeeFromForm();

            // Variable that records add/edit function status
            bool status;

            if (_editMode)
            {

                status = Utils.EditEmployee(employee, AppConstants.XmlFilePath);
                if (!status)
                {

                    Trace.WriteLine($"[DetailsForm.cs] [DetailsForm] submit_Click(); EditEmployee() from Utils class failed. ");
                }
            }
            else
            {
                status = Utils.AddEmployee(employee, AppConstants.XmlFilePath);
                if (!status)
                {

                    Trace.WriteLine($"[DetailsForm.cs] [DetailsForm] submit_Click(); AddEmployee() from Utils class failed. ");
                }
            }

            if (status)
            {

                // Set to OK denoting success employee creation/edit
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Create employee object from data entered in the form
        /// </summary>
        /// <returns> Employee object with entered data </returns> 
        private Employee CreateEmployeeFromForm()
        {
            Employee employee = new Employee
                                (
                                    int.Parse(txtId.Text),
                                    txtName.Text,
                                    comboxDesignation.Text,
                                    txtEmail.Text,
                                    txtReporter.Text,
                                    txtReportee.Text,
                                    txtProductLineResponsibility.Text,
                                    int.Parse(txtWorkExperience.Text)
                                );
            return employee;
        }


        /// <summary>
        /// Populate edit form with employee details
        /// </summary>
        /// <param name="employee"> Employee object to be edited </param>
        private void PopulateEditForm(Employee employee)
        {

            // Id is set to readonly mode
            txtId.Text = employee.Id.ToString();
            txtId.ReadOnly = true;

            txtName.Text = employee.Name;
            comboxDesignation.SelectedItem = employee.Designation;
            txtEmail.Text = employee.EmailId;
            txtReporter.Text = employee.Reporter;
            txtReportee.Text = employee.Reportee;
            txtProductLineResponsibility.Text = employee.ProductLineResponsibility;
            txtWorkExperience.Text = employee.WorkExperience.ToString();
        }

        /// <summary>
        /// Check input validity and indicate errors if input is invalid. 
        /// </summary>
        /// <param name="edit"></param>
        /// <returns></returns> 
        private bool IsInputValid(bool edit)
        {

            // Clear all errors
            errId.Clear();
            errName.Clear();
            errDesignation.Clear();
            errEmail.Clear();
            errProdLineResp.Clear();
            errorWorkExperience.Clear();

            bool IsValid = true;

            // Id validation
            if (String.IsNullOrWhiteSpace(txtId.Text))
            {

                // Empty Id error
                errId.SetError(txtId, "Id cannot be empty. ");
                IsValid = false;
            }
            else if (!int.TryParse(txtId.Text, out int id) || id < 0)
            {

                // Negative Id error
                errId.SetError(txtId, "Id should be a non-negative integer. ");
                IsValid = false;
            }
            else
            {

                // Duplicate record exists error
                List<Employee> employeeList = Utils.LoadEmployees(AppConstants.XmlFilePath);
                int count = employeeList.Count(u => u.Id == int.Parse(txtId.Text));

                if (edit && (count > 1) || !edit && (count > 0))
                {

                    errId.SetError(txtId, "Employee with Id already exists. ");
                    IsValid = false;
                }
            }

            // Name validation
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {

                errName.SetError(txtName, "Name cannot be empty. ");
                IsValid = false;
            }

            // Designation validation
            if (comboxDesignation.SelectedItem == null || String.IsNullOrWhiteSpace(comboxDesignation.Text))
            {

                errDesignation.SetError(comboxDesignation, "Designation cannot be empty. ");
                IsValid = false;
            }

            // Email validation
            if (String.IsNullOrWhiteSpace(txtEmail.Text))
            {

                // Empty email error
                errEmail.SetError(txtEmail, "Email cannot be empty. ");
                IsValid = false;
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {

                // Invalid email format error
                errEmail.SetError(txtEmail, "Email format is incorrect. ");
                IsValid = false;
            }

            // ProductLineResponsibility validation
            if (String.IsNullOrWhiteSpace(txtProductLineResponsibility.Text))
            {

                errProdLineResp.SetError(txtProductLineResponsibility, "ProductLineResponsibility cannot be empty. ");
                IsValid = false;
            }

            if (String.IsNullOrWhiteSpace(txtWorkExperience.Text))
            {

                errorWorkExperience.SetError(txtWorkExperience, "Work Experience cannot be empty. ");
                IsValid = false;
            }
            else if (!int.TryParse(txtWorkExperience.Text, out int id) || id < 0)
            {

                // Negative work experience error
                errorWorkExperience.SetError(txtWorkExperience, "Work Experience should be non negative integer. ");
                IsValid = false;
            }

            return IsValid;
        }
    }
}