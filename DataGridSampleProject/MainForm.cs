using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataGridSampleProject;

namespace DataGridSampleProject
{
    public partial class MainForm : Form
    {

        public MainForm()
        {

            InitializeComponent();

            /* ***********************
               Load is an event member of Form class.   
               Load event is fired when form is loaded into memory, 
               right before it is displayed for Ist time. 

               += : Subscription operator. When Load event occur, all 
                    the methods subscribed to it will be executed. 
               ***********************/
            this.Load += MainForm_Load; 
        }

        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {

            LoadData(); 
        }

        /// <summary>
        /// Load data from xml file and bind data to DataGridView table 
        /// </summary>
        private void LoadData()
        {

            List<Employee> employees = Utils.LoadEmployees(AppConstants.XmlFilePath);

            // Binding Source: used to decouple table from data. Don't know why. 
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = employees;
            dgvEmployees.DataSource = bindingSource; 
        }

        private void addButton_Click(object sender, EventArgs e)
        {

            DetailsForm detailsForm = new DetailsForm();
            detailsForm.Show();
            this.Hide();
            if (detailsForm.ShowDialog() == DialogResult.OK)
            {
                this.Show();
                LoadData();
            }
        }
    }
}
