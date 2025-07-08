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
        public DetailsForm()
        {

            InitializeComponent();
        }

        private void submit_Click(object sender, EventArgs e)
        {

            ErrorHandler(); 
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
            if (!IsEmptyOrNullOrWhiteSpace(txtId) && !int.TryParse(txtId.Text, out _))
            {
                errId.SetError(txtId, $"Id should be integer");
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
