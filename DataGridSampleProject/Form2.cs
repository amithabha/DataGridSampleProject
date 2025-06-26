using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridSampleProject
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form2_Load(object sender, EventArgs e)
        {

        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void nameTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    
        private void submit_Click(object sender, EventArgs e)
        {

            // ErrorProvider list
            List<ErrorProvider> errorProviders = new List<ErrorProvider>
            {
                errName, errEmail, errReporter, errReportee,
                errProdLineResp, errorWorkExperience
            };

            // TextBox list
            List<TextBox> textBoxes = new List<TextBox>
            {
                txtName, txtEmail, txtReporter, txtReportee, 
                txtProductLineResponsibility, txtWorkExperience
            };

            // TextBox names
            List<string> TextBoxtitles = new List<string>
            {
                "Name", "Email Id", "Reporter", "Reportee", "Product Line Responsibility", "Work Experience"
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
            for (int i = 0; i < comboBoxes.Count; i++)
            {
                ErrorAssigner(comboBoxes[i], errorProviders[i], comboBoxTitles[i]);
            }

            // Setting error for empty TextBoxes
            for (int i = 0; i < textBoxes.Count; i++)
            {
                ErrorAssigner(textBoxes[i], errorProviders[i], TextBoxtitles[i]); 
            }
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
    }
}
