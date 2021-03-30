using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VarsityManager
{
    class Student
    {
        private string _filePath = "student.json";
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Course { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; }
        

        public void Add(Student form)
        {
            Random r = new Random();
            form.Id = r.Next(1000, 9999);
            string data = JsonConvert.SerializeObject(form, Formatting.None);
            Util.WriteToTextFile(_filePath, data);
        }
        
        public void Edit(Student info)
        {
            List<Student> list = List();
            Student s = list.Where(x => x.Id == info.Id).FirstOrDefault();
            list.Remove(s);
            list.Add(info);
            string data = JsonConvert.SerializeObject(list, Formatting.None);
            Util.WriteToTextFile(_filePath, data, false);
        }
        public void Delete(int id)
        {
            List<Student> list = List();

            Student s = list.Where(x => x.Id == id).FirstOrDefault();
            list.Remove(s);
            int count = list.Count;
            string data = JsonConvert.SerializeObject(list, Formatting.None);
            Util.WriteToTextFile(_filePath, data, false, count);
        }
        public List<Student> List()
        {
            string objList = Util.ReadFromTextFile(_filePath);
            if (objList != null)
            {
                List<Student> lst = JsonConvert.DeserializeObject<List<Student>>(objList);
                return lst;
            }
            else
            {
                return null;
            }
        }

        public List<Student> Sort(List<Student> listStudents, string sortType)
        {
            if (sortType == "First Name")
            {
                string[] list = new string[listStudents.Count];
                for (var i = 0; i < listStudents.Count; i++)
                {
                    list[i] = listStudents[i].FirstName;
                }


                for (int i = list.Length - 1; i > 0; i--)
                {
                    for (int j = 0; j <= i - 1; j++)
                    {
                        if (list[j].CompareTo(list[j + 1]) > 0) 
                        {
                            string name = list[j];
                            list[j] = list[j + 1];
                            list[j + 1] = name;
                            Student nameLists = listStudents[j];
                            listStudents[j] = listStudents[j + 1];
                            listStudents[j + 1] = nameLists;
                        }
                    }
                }
            }
            else
            {
                DateTime[] list = new DateTime[listStudents.Count];

                for (var i = 0; i < listStudents.Count; i++)
                {
                    list[i] = listStudents[i].RegistrationDate;
                }

                for (int i = list.Length - 1; i > 0; i--)
                {
                    for (int j = 0; j <= i - 1; j++)
                    {
                        if (list[j].CompareTo(list[j + 1]) > 0)
                        {
                            DateTime registerDate = list[j];
                            list[j] = list[j + 1];
                            list[j + 1] = registerDate;
                            Student regDateList = listStudents[j];
                            listStudents[j] = listStudents[j + 1];
                            listStudents[j + 1] = regDateList;
                        }
                    }
                }
            }
            return listStudents;
        }

        public DateTime[] FindWeek(DateTime registeredDate)
        {
            DateTime[] dayArray = new DateTime[2]; 
            string[] days = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            int index = Array.IndexOf(days, registeredDate.DayOfWeek.ToString());
            DateTime startDay = registeredDate.AddDays(-index);
            int remainingIndex = 6 - index;
            DateTime endDay = registeredDate.AddDays(remainingIndex);
            dayArray[0] = startDay;
            dayArray[1] = endDay;
            return dayArray;
        }

        public List<Student> WeeklyStudent(DateTime[] dayArray, List<Student> listStudents)
        {
            List<Student> weeklyStudents = new List<Student>();
            for (int i = 0; i < listStudents.Count(); i++)
            {
                if (listStudents[i].RegistrationDate > dayArray[0] && listStudents[i].RegistrationDate < dayArray[1])
                {  
                    weeklyStudents.Add(listStudents[i]);
                }
            }
            return weeklyStudents; 
        }
    }
}