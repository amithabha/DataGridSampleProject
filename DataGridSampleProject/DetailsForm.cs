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

namespace DataGridSampleProject
{
    public partial class DetailsForm : Form
    {
        private static readonly string _xmlfile = "employees.txt"; 
        private static bool _editMode; 
        public DetailsForm(bool editMode = false)
        {

            _editMode = editMode; 
            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {

            ErrorHandler();
            Employee employee = CreateEmployee();

            if (_editMode == true)
            {

                Utils.EditEmployee(employee, _xmlfile);
            }
            else
            {

                Utils.AddEmployee(employee, _xmlfile);
            }

            Close(); 
            DialogResult = DialogResult.OK;
        }

        private Employee CreateEmployee()
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
