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
            LoadData(); 
        }

        public void LoadData()
        {

            List<Employee> employeeList = Utils.LoadEmployees(_xmlfile);

            // If employeeList is empty list, create a file in filepath 
            if (employeeList.Count == 0)
            {

                Utils.SaveEmployees(employeeList, _xmlfile);
            }
            else
            {
                dgvEmployees.DataSource = employeeList;
            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {

            DetailsForm detailsForm = new DetailsForm();
            detailsForm.Show();
            this.Hide();
        }
    }
}
