/******************************************************************************
* Filename    = MainForm.cs
*
* Author      = Amithabh A
*
* Product     = Main Form 
* 
* Project     = EmployeeDatabase
*
* Description = Implements form that displays employee records
*****************************************************************************/

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

namespace DataGridSampleProject
{

    /// <summary>
    /// Main Form implementation
    /// </summary> 
    public partial class MainForm : Form
    {

        private string _searchString = String.Empty;

        /// <summary>
        /// Constructor
        /// <summary>
        public MainForm()
        {

            // Load UI. Then load data. 
            InitializeComponent();
            this.Load += MainForm_Load;
        }

        /// <summary>
        /// Event Handler that runs LoadData() after UI is loaded
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {

            LoadData();
        }

        /// <summary>
        /// Loads employee data from XmlFile to UI. 
        /// If `_searchString` is not empty, employee data containing _searchString content only will be showed. 
        /// </summary>
        private void LoadData()
        {

            List<Employee> employees = Utils.LoadEmployees(AppConstants.XmlFilePath);

            // Search Functionality implementation
            if (!String.IsNullOrEmpty(_searchString))
            {

                if (_searchString.ToLower() == _searchString)
                {

                    employees = employees.Where(p => p.ToString().ToLower().Contains(_searchString.ToLower())).ToList();
                }
                else
                {

                    employees = employees.Where(p => p.ToString().Contains(_searchString)).ToList();
                }
            }

            // Autosizing stuffs
            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvEmployees.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvEmployees.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Binding Source: used to decouple table from data. Don't know why. 
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = employees;
            dgvEmployees.DataSource = bindingSource;

            // Display the no of records
            UpdateRecordCount();
        }

        /// <summary>
        /// Event handler that handle logic of adding employee record
        /// </summary>
        private void addButton_Click(object sender, EventArgs e)
        {

            // This declaration will instantiate the object
            using (DetailsForm detailsForm = new DetailsForm())
            {
                // ShowDialog() will open form as a dialog box. 
                // we can set the dialog result as OK in the end of Details form. 
                if (detailsForm.ShowDialog() == DialogResult.OK)
                {

                    // Reload data after successful employee addition
                    LoadData();
                }
            }
        }

        /// <summary>
        /// Event handler that handle logic of editing employee record
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {

            // A row should be highlighted to edit. 
            if (dgvEmployees.CurrentRow == null)
            {

                MessageBox.Show("Please select a row. ");
                return;
            }

            // Get highlighted emploee record
            Employee employee = dgvEmployees.CurrentRow.DataBoundItem as Employee;

            // Open details form, passing employee record, in edit mode. 
            using (DetailsForm detailsForm = new DetailsForm(true, employee))
            {

                if (detailsForm.ShowDialog() == DialogResult.OK)
                {

                    // Reload data after successful edit. 
                    LoadData();
                }
            }
        }


        /// <summary>
        /// Event handler that handle logic of removing employee record
        /// </summary>
        private void btnRemove_Click(object sender, EventArgs e)
        {

            // A row should be highlighted to delete. 
            if (dgvEmployees.CurrentRow == null)
            {

                MessageBox.Show("Please select a row. ");
                return;
            }

            // FIXME: Confirmation message before deleting

            // Get highlighted employee as Employee object
            Employee employee = dgvEmployees.CurrentRow.DataBoundItem as Employee;

            // status variable to check delete status
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

        /// <summary>
        /// Set `_searchString` if text in search bar is changed, and reload data. 
        /// </summary>
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

            LoadData();
        }

        /// <summary>
        /// Update record count when called. 
        /// </summary>
        private void UpdateRecordCount()
        {

            lblRecordCount.Text = $"Employee Count: {dgvEmployees.Rows.Count}";
        }
    }
}