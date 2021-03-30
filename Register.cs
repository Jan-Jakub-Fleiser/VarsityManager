using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VarsityManager
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
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
            DataTable dt = Util.ConvertToDataTable(result);
            dataGridReport.DataSource = dt;
            dataGridReport.CurrentCell = null;
        }
    }
}