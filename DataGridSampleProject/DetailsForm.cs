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
    public partial class DetailsForm : Form
    {
        private Employee EditEmployee; 
        private bool _editMode;
        public DetailsForm(bool editMode = false, Employee employee = null)
        {

            _editMode = editMode;

            if (_editMode)
            {

                if (employee == null)
                {
                    Trace.WriteLine($"[DetailsForm.cs] [DetailsForm] Constructor; Argument employee should not be null. ");
                }

                EditEmployee = employee; 
            }


            InitializeComponent();
            this.Load += LoadDetails; 
        }

        private void LoadDetails(object sender, EventArgs e)
        {

            if (_editMode)
            {
                PopulateEditForm(EditEmployee); 
            }
        }

        private void submit_Click(object sender, EventArgs e)
        {

            bool validity = IsInputValid();
            if (!validity)
            {

                MessageBox.Show("Please correctly enter the data. ");
                return;
            }

            Employee employee = CreateEmployeeFromForm();

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

                DialogResult = DialogResult.OK;
                Close(); 
            }
        }

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


        private void PopulateEditForm(Employee employee)
        {
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

        private bool IsInputValid()
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
                errId.SetError(txtId, "Id cannot be empty. ");
                IsValid = false;
            }
            else if (!int.TryParse(txtId.Text, out int id) || id < 0)
            {
                errId.SetError(txtId, "Id should be a non-negative integer. ");
                IsValid = false;
            }
            else
            {

                List<Employee> employeeList = Utils.LoadEmployees(AppConstants.XmlFilePath);
                int count = employeeList.Count(u => u.Id == int.Parse(txtId.Text));

                if (count > 0)
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
                errDesignation.SetError( comboxDesignation, "Designation cannot be empty. ");
                IsValid = false;
            }

            // Email validation
            if (String.IsNullOrWhiteSpace(txtEmail.Text))
            {
                errEmail.SetError( txtEmail, "Email cannot be empty. ");
                IsValid = false;
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errEmail.SetError( txtEmail, "Email format is incorrect. ");
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
                errorWorkExperience.SetError( txtWorkExperience, "Work Experience cannot be empty. ");
                IsValid = false;
            }
            else if (!int.TryParse(txtWorkExperience.Text, out int id) || id < 0)
            {
                errorWorkExperience.SetError( txtWorkExperience, "Work Experience should be non negative integer. ");
                IsValid = false;
            }

            return IsValid; 
        }





















        private void ErrorHandler()
        {

            // ErrorProvider list
            List<ErrorProvider> errorProviders = new List<ErrorProvider>
            {
                errId, errName, errEmail, errReporter, errReportee,
                errProdLineResp, errorWorkExperience
            };

            // TextBox list
            List<TextBox> textBoxes = new List<TextBox>
            {
                txtId, txtName, txtEmail, txtReporter, txtReportee,
                txtProductLineResponsibility, txtWorkExperience
            };

            // TextBox names
            List<string> TextBoxtitles = new List<string>
            {
                "Id", "Name", "Email Id", "Reporter", "Reportee", "Product Line Responsibility", "Work Experience"
            };

            // ComboBoxes
            List<ComboBox> comboBoxes = new List<ComboBox>
            {
                comboxDesignation
            };

            // ComboBoxNames
            List<string> comboBoxTitles = new List<string>
            {
                "Designation"
            };

            // Setting error for empty ComboBoxes
            ErrorAssigner(comboxDesignation, errDesignation, "Designation");

            // Setting error for empty TextBoxes
            for (int i = 0; i < textBoxes.Count; i++)
            {
                ErrorAssigner(textBoxes[i], errorProviders[i], TextBoxtitles[i]);
            }

            // Assigning error for non-integer WorkExperience text.  
            if (!IsEmptyOrNullOrWhiteSpace(txtWorkExperience) && !int.TryParse(txtWorkExperience.Text, out _))
            {
                errorWorkExperience.SetError(txtWorkExperience, $"Work Experience should be integer");
            }

            // Assigning error for non-integer Id text.  
            int result;

            if (!IsEmptyOrNullOrWhiteSpace(txtId) && !int.TryParse(txtId.Text, out _))
            {
                errId.SetError(txtId, $"Id should be a non negative integer");
            }
            else if (!IsEmptyOrNullOrWhiteSpace(txtId) && int.TryParse(txtId.Text, out result))
            {
                if (result < 0)
                {
                    errId.SetError(txtId, $"Id should be non negative integer");
                }
            }

            // FIXME: Check whether email entered is in correct format. 
        }

        /// <summary>
        /// Checking whether textbox is empty or null or contains only whitespace
        /// </summary>
        /// <param name="textBox">TextBox object</param>
        /// <returns>boolean val of error status</returns>
        private bool IsEmptyOrNullOrWhiteSpace(TextBox textBox)
        {

            if (string.IsNullOrEmpty(textBox.Text) | string.IsNullOrWhiteSpace(textBox.Text))
            {
                return true; 
            }

            return false; 
        }

        /// <summary>
        /// Checking whether combobox is empty or null or contains only whitespace
        /// </summary>
        /// <param name="textBox">TextBox object</param>
        /// <returns>boolean val of error status</returns>
        private bool IsEmptyOrNullOrWhiteSpace(ComboBox comboBox)
        {

            if (string.IsNullOrEmpty(comboBox.Text) | string.IsNullOrWhiteSpace(comboBox.Text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Set or unset error for TextBox
        /// </summary>
        /// <param name="textBox">TextBox object</param>
        /// <param name="errorProvider">ErrorProvider object</param>
        /// <param name="name">Name of TextBox</param>
        private void ErrorAssigner(TextBox textBox, ErrorProvider errorProvider, string name)
        {

            if (IsEmptyOrNullOrWhiteSpace(textBox))
            {
                errorProvider.SetError(textBox, $"{name} should not be empty"); 
            } else
            {
                errorProvider.Clear(); 
            }
        }


        /// <summary>
        /// Set or unset error for ComboBox
        /// </summary>
        /// <param name="textBox">ComboBox object</param>
        /// <param name="errorProvider">ErrorProvider object</param>
        /// <param name="name">Name of ComboBox</param>
        private void ErrorAssigner(ComboBox comboBox, ErrorProvider errorProvider, string name)
        {

            if (IsEmptyOrNullOrWhiteSpace(comboBox))
            {
                errorProvider.SetError(comboBox, $"{name} should not be empty");
            }
            else
            {
                errorProvider.Clear();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
