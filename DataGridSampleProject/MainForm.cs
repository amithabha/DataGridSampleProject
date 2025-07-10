using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics; 
using DataGridSampleProject;

namespace DataGridSampleProject
{
    public partial class MainForm : Form
    {

        private string _searchString = String.Empty; 

        public MainForm()
        {

            InitializeComponent();

            /**************************************************************
               Load is an event member of Form class.   
               Load event is fired when form is loaded into memory, 
               right before it is displayed for Ist time. 

               += : Subscription operator. When Load event occur, all 
                    the methods subscribed to it will be executed. 
            ***************************************************************/

            // Subscribing to Load is preferred instead of direct calls in the constructor of UI setup. 
            // Because, it will ensure that any calls that needed strictly after the UI setup is completed, will
            // be called after UI setup only, because Load will be fired after UI setup is done. 
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
        /// LoadData is run only by add, edit, delete and search functions in MainForm. 
        /// </summary>
        private void LoadData()
        {

            List<Employee> employees = Utils.LoadEmployees(AppConstants.XmlFilePath);

            // Search Functionality
            if (!String.IsNullOrEmpty(_searchString))
            {

                employees = employees.Where(p => p.ToString().Contains(_searchString)).ToList();                                                    
            }

            // Autosizing stuffs
            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; 
            dgvEmployees.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; 
            dgvEmployees.DefaultCellStyle.WrapMode = DataGridViewTriState.True; 

            // Binding Source: used to decouple table from data. Don't know why. 
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = employees;
            dgvEmployees.DataSource = bindingSource; 
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            // This declaration will instantiate the object
            using (DetailsForm detailsForm = new DetailsForm())
            {
                // ShowDialog() will open form as a dialog box. 
                // we can set the dialog result as OK in the end of Details form. 
                if (detailsForm.ShowDialog() == DialogResult.OK)
                {

                    LoadData(); 
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (dgvEmployees.CurrentRow == null)
            {

                MessageBox.Show("Please select a row. ");
                return;
            }

            Employee employee = dgvEmployees.CurrentRow.DataBoundItem as Employee; 
            using (DetailsForm detailsForm = new DetailsForm(true, employee))
            {

                if (detailsForm.ShowDialog() == DialogResult.OK)
                {

                    LoadData(); 
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.CurrentRow == null)
            {

                MessageBox.Show("Please select a row. ");
                return;
            }

            // FIXME: Confirmation message before deleting

            Employee employee = dgvEmployees.CurrentRow.DataBoundItem as Employee;
            bool status = Utils.DeleteEmployee(AppConstants.XmlFilePath, employee.Id);
            if (!status)
            {

                Trace.WriteLine($"[MainForm.cs] [MainForm] btnRemove_Click(); Utils.DeleteEmployee() failed. ");
            }
            else
            {
                LoadData(); 
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(txtSearch.Text))
            {
                _searchString = String.Empty;
            }
            else
            {
                _searchString = txtSearch.Text; 
            }

            // List<Employee> employeeList = Utils.LoadEmployees(AppConstants.XmlFilePath);
                // List<Employee> newEmployeeList = employeeList.Where(p => p.ToString().Contains(txtSearch.Text)).ToList();
                // LoadData(newEmployeeList); 
                LoadData(); 
        }
    }
}
