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

        private static readonly string _xmlfile = "employees.txt"; 

        public MainForm()
        {

            InitializeComponent();
        }

        public void LoadData()
        {
            List<Employee> employees = Utils.LoadEmployees(_xmlfile);
            // BindingSource bindingSource = new BindingSource();
            // bindingSource.DataSource = employees;
            // dgvEmployees.DataSource = bindingSource; 
            dgvEmployees.DataSource = employees; 
        }

        private void addButton_Click(object sender, EventArgs e)
        {

            DetailsForm detailsForm = new DetailsForm();
            detailsForm.Show();
            this.Hide();
        }
    }
}
