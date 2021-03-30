using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VarsityManager
{
    public partial class StudentRegristration : Form
    {
        public Home()
        {
            InitializeComponent();
            BindGrid();
            btnUpdate.Visible = false;
            dataGridReport.Visible= false;
            panelReport.Visible = false;
            
        }

        private void cbSorting_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFirstName.Text == "")
            {
                MessageBox.Show("Please enter first name", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            else if (txtLastName.Text == "" || txtLastName.Text == null)
            {
                MessageBox.Show("Please enter last name", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtAddress.Text == "" || txtAddress.Text == null)
            {
                MessageBox.Show("Please enter an address", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtContactNo.Text == "" || txtContactNo.Text == null)
            {
                MessageBox.Show("Please enter a valid Phone Number", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (gender.SelectedItem == null)
            {
                MessageBox.Show("Please select a Gender", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtEmail.Text == "" || txtEmail.Text == null)
            {
                MessageBox.Show("Please enter a valid Email", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (Course.SelectedItem == null)
            {
                MessageBox.Show("Please select a Course", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (rBtnPending.Checked == false && rBtnPublished.Checked == false)
            {
                MessageBox.Show("Please select status", "Warning Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (Regex.IsMatch(txtContactNo.Text,"^[0-9]{10}"))
                {
                    Student obj = new Student();
                    obj.FirstName = txtFirstName.Text;
                    obj.LastName = txtLastName.Text;
                    obj.Name = txtFirstName.Text + " " + txtLastName.Text;
                    obj.Address = txtAddress.Text;
                    obj.Email = txtEmail.Text;
                    obj.BirthDate = dob.Value;
                    obj.ContactNo = txtContactNo.Text;
                    obj.Gender = gender.SelectedItem.ToString();
                    obj.RegistrationDate = RegsiterdDate.Value;
                    obj.Course = Course.SelectedItem.ToString();
                    if (rBtnPending.Checked == true)
                    {
                        obj.Status = rBtnPending.Text;
                    }

                    else if (rBtnPublished.Checked == true)
                    {
                        obj.Status = rBtnPublished.Text;
                    }

                    obj.Add(obj);
                    MessageBox.Show("info is saved", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindGrid();
                    Clear();
                }
                else
                {
                    MessageBox.Show("Invalid Number Occurred! Please enter a valid phone number", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
        }
        private void BindGrid()
        {
            Student obj = new Student();
            List<Student> listStudents = obj.List();
            DataTable dt = Util.ConvertToDataTable(listStudents);
            dataGridStudent.DataSource = dt;
            
        }
        private void Clear()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            dob.Value = DateTime.Today;
            txtContactNo.Text = "";
            gender.SelectedItem = null;
            RegsiterdDate.Value = DateTime.Today;
            Course.SelectedItem = null;
            if (rBtnPending.Checked)
            {
                rBtnPending.Checked = false;
            }
            else
            {
                rBtnPublished.Checked = false;
            }
            
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            if (cbSorting.SelectedItem != null)
            {
                if (cbSorting.SelectedItem.ToString() == "First Name")
                {
                    Student obj = new Student();
                    List<Student> listStudents = obj.List();
                    List<Student> lst = obj.Sort(listStudents, "First Name");
                    DataTable dt = Util.ConvertToDataTable(lst);
                    dataGridStudent.DataSource = dt;
                }
                else
                {
                    Student obj = new Student();
                    List<Student> listStudents = obj.List();
                    List<Student> lst = obj.Sort(listStudents, "Registration Date");
                    DataTable dt = Util.ConvertToDataTable(lst);
                    dataGridStudent.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("Invalid Input! Please, select any value.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            obj.Id = int.Parse(txtId.Text);
            obj.FirstName = txtFirstName.Text;
            obj.LastName = txtLastName.Text;
            obj.Name = txtFirstName.Text + " " + txtLastName.Text;
            obj.Address = txtAddress.Text;
            obj.Email = txtEmail.Text;
            obj.BirthDate = dob.Value;
            if (Regex.IsMatch(txtContactNo.Text, "^[0-9]{10}"))
            {
                obj.ContactNo = txtContactNo.Text;
                obj.Gender = gender.SelectedItem.ToString();
                obj.RegistrationDate = RegsiterdDate.Value;
                obj.Course = Course.SelectedItem.ToString();
                if (rBtnPending.Checked == true)
                {
                    obj.Status = rBtnPending.Text;
                    
                }

                else if (rBtnPublished.Checked == true)
                {
                    obj.Status = rBtnPublished.Text;
                }
                obj.Edit(obj);
                BindGrid();
                Clear();
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                MessageBox.Show("info Sucessfully Updated", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Invalid Number. Please enter valid phone number", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            int rowIndex = dataGridStudent.CurrentCell.RowIndex;
            string message = "So you want to delete this record?";
            string title = "Deleted";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                string value = dataGridStudent[0, rowIndex].Value.ToString();
                obj.Delete(int.Parse(value));
                BindGrid();
                MessageBox.Show("Sucessfully Deleted", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Clear();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            int rowIndex = dataGridStudent.CurrentCell.RowIndex;
            string value = dataGridStudent[0, rowIndex].Value.ToString();
            int id = 0;
            if (String.IsNullOrEmpty(value))
            {
                MessageBox.Show("Empty record found, Please select the record", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                id = int.Parse(value);
                Student s = obj.List().Where(x => x.Id == id).FirstOrDefault();
                txtId.Text = s.Id.ToString();
                txtFirstName.Text = s.Name.Split(' ')[0];
                txtLastName.Text = s.Name.Split(' ')[1];
                txtAddress.Text = s.Address;
                txtContactNo.Text = s.ContactNo;
                txtEmail.Text = s.Email;
                gender.SelectedItem = s.Gender;
                Course.SelectedItem = s.Course;
                RegsiterdDate.Value = s.RegistrationDate;

                if (dataGridStudent.CurrentRow.Cells["Status"].FormattedValue.Equals("Pending"))
                {
                    rBtnPending.Checked = true;
                }
                else
                {
                    rBtnPublished.Checked = false;
                }
                if (dataGridStudent.CurrentRow.Cells["Status"].FormattedValue.Equals("Published"))
                {
                    rBtnPublished.Checked = true;
                }
                else
                {
                    rBtnPending.Checked = false;
                }
            }
            btnUpdate.Visible = true;
            btnSave.Visible = false;
        }


        private void btnReport_Click(object sender, EventArgs e)
        {
            Student obj = new Student();
            DateTime registeredDate = dateReport.Value;
            List<Student> listStudents = obj.List();
            DateTime[] dateArray = obj.FindWeek(registeredDate);
            List<Student> weeklyStudents = obj.WeeklyStudent(dateArray, listStudents);
            var result = weeklyStudents
                    .GroupBy(l => l.Course)
                    .Select(cl => new
                    {
                        Course = cl.First().Course,
                        Count = cl.Count().ToString()
                    }).ToList();

            dataGridReport.Visible = true;
            panelReport.Visible = true;
            dataGridStudent.Hide();

            DataTable dt = Util.ConvertToDataTable(result);
            dataGridReport.DataSource = dt;
            dataGridReport.CurrentCell = null;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Import importFile = new Import();
            importFile.ShowDialog();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();

            dialog = MessageBox.Show("close?", "Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes)
            {
                System.Environment.Exit(1);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialog = new DialogResult();

            dialog = MessageBox.Show("close?", "Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dialog == DialogResult.Yes)
            {
                System.Environment.Exit(1);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridReport.Visible = false;
            panelReport.Visible = false;
            dataGridStudent.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            MessageBox.Show("Info is cleared", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}