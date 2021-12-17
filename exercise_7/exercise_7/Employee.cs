using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace exercise_7
{
    internal struct Employee
    {
        #region Fields

        private string _id;
        private string _timeStamp { get; set; }
        private string _fullName { get; set; }
        private int _age { get; set; }
        private int _height { get; set; }
        private string _birthday { get; set; }
        public string _birthplace { get; set; }

        #endregion
        #region Properties

        public string ID { get => _id; set => _id = value; }
        public string TimeStamp { get => _timeStamp; set => _timeStamp = value; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public int Age { get => _age; set => _age = value; }
        public int Height { get => _height; set => _height = value; }
        public string Birthday { get => _birthday; set => _birthday = value; }
        public string Birthplace { get => _birthplace; set => _birthplace = value; }

        #endregion
        #region Constructors

        public Employee(string fullName, int age, int height, string birthday, string birthplace)
        {
            _id = Guid.NewGuid().ToString("N");
            _timeStamp = DateTime.Now.ToString("dd.MM.yy HH:mm");
            _fullName = fullName;
            _age = age;
            _height = height;
            _birthday = birthday;
            _birthplace = birthplace;
        }

        #endregion
        #region Methods
        public static string Trim(string text)
        {
            string[] words = text.Split('#');
            return string.Join(" ", words);
        }

        public static string GetEntry(string id, string fileName)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        string line = streamReader.ReadLine();
                        string[] array = line.Split('#');
                        if (id == array[0]) return line;
                    }
                }
            }

            else
            {
                WriteLine("Файл не найден.");
            }

            return null;
        }

        public static Employee CreateEntry(string data)
        {
            string[] array = data.Split('#');
            Employee employee = new Employee(array[2], int.Parse(array[3]), int.Parse(array[4]), array[5], array[6]);
            return employee;
        }

        public static string ConvertEntry(Employee employee)
        {
            return $"{employee.ID}#{employee.TimeStamp}#{employee.FullName}#{employee.Age}#{employee.Height}#{employee.Birthday}#{employee.Birthplace}";
        }

        public static void RemoveEntry(string id, string fileName)
        {
            if (File.Exists(fileName))
            {
                string lineToRemove = GetEntry(id, fileName);
                File.WriteAllLines(fileName, File.ReadLines(fileName).Where(line => line != lineToRemove).ToList());
            }
            
            else
            {
                WriteLine("Файл не найден.");
            }
        }

        public static void EditEntry()
        {
            //
        }

        #endregion
    }
}
