using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Console;

namespace exercise_7
{
    internal struct Employee
    {
        #region Fields

        private string _id;
        private string _timeStamp;
        private string _fullName;
        private int _age;
        private int _height;
        private string _birthday;
        public string _birthplace;

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

        public Employee(string id, string timeStamp, string fullName, int age, int height, string birthday, string birthplace)
        {
            _id = id;
            _timeStamp = timeStamp;
            _fullName = fullName;
            _age = age;
            _height = height;
            _birthday = birthday;
            _birthplace = birthplace;
        }

        #endregion
        #region Methods
        /// <summary>
        /// Форматирование строки по разделителю
        /// </summary>
        /// <param name="text">строка</param>
        /// <returns>строка</returns>
        public static string Trim(string text)
        {
            string[] words = text.Split('#');
            return string.Join(" ", words);
        }
        /// <summary>
        /// Получение записи по id
        /// </summary>
        /// <param name="id">строка</param>
        /// <param name="fileName">строка</param>
        /// <returns></returns>
        public static string GetEntryByID(string id, string fileName)
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
        /// <summary>
        /// Получение записи по id
        /// </summary>
        /// <param name="id">строка</param>
        /// <param name="list">коллекция Employee</param>
        /// <returns>строка</returns>
        public static Employee GetEntryByID(string id, List<Employee> list) => list.Single(s => s.ID == id);
        /// <summary>
        /// Создание записи
        /// </summary>
        /// <param name="data">строка</param>
        /// <returns>Employee</returns>
        public static string CreateEntry(string data)
        {
            string[] array = data.Split('#');
            Employee employee = new Employee(array[2], int.Parse(array[3]), int.Parse(array[4]), array[5], array[6]);
            return ConvertEntry(employee);
        }
        /// <summary>
        /// Чтение записи
        /// </summary>
        /// <param name="line">строка</param>
        /// <returns>Employee</returns>
        public static Employee GetEntry(string line)
        {
            string[] array = line.Split('#');
            Employee employee = new Employee(array[0], array[1], array[2], int.Parse(array[3]), int.Parse(array[4]), array[5], array[6]);
            return employee;
        }
        /// <summary>
        /// Конвертирование записи
        /// </summary>
        /// <param name="employee">Employee</param>
        /// <returns>строка</returns>
        public static string ConvertEntry(Employee employee)
        {
            return $"{employee.ID}#{employee.TimeStamp}#{employee.FullName}#{employee.Age}#{employee.Height}#{employee.Birthday}#{employee.Birthplace}";
        }
        /// <summary>
        /// Удаление записи
        /// </summary>
        /// <param name="id">строка</param>
        /// <param name="fileName">строка</param>
        public static void RemoveEntry(string id, string fileName)
        {
            if (File.Exists(fileName))
            {
                string lineToRemove = GetEntryByID(id, fileName);
                File.WriteAllLines(fileName, File.ReadLines(fileName).Where(line => line != lineToRemove).ToList());
            }
            
            else
            {
                WriteLine("Файл не найден.");
            }
        }
        /// <summary>
        /// Сортировка по возрастанию(дата)
        /// </summary>
        /// <param name="list">коллекция Employee</param>
        /// <returns>коллекция Employee</returns>
        public static List<Employee> OrderEntriesByAscending(List<Employee> list) => list.OrderBy(entry => entry.TimeStamp).ToList();
        /// <summary>
        /// Сортировка по убыванию(дата)
        /// </summary>
        /// <param name="list">коллекция Employee</param>
        /// <returns>коллекция Employee</returns>
        public static List<Employee> OrderEntriesByDescending(List<Employee> list) => list.OrderByDescending(entry => entry.TimeStamp).ToList();
        /// <summary>
        /// Генерация id
        /// </summary>
        /// <returns>строка</returns>
        public static string GenerateID() => Guid.NewGuid().ToString("N");
        /// <summary>
        /// Редактирование записи
        /// </summary>
        /// <param name="id">строка</param>
        /// <param name="list">коллекция Employee</param>
        /// <param name="fileName">строка</param>
        public static void EditEntry(string id, List<Employee> list, string fileName)
        {
            Employee entry = GetEntryByID(id, list);
            Employee newEntry = GetEntry(Program.InputData());
            int index = list.IndexOf(entry);

            list.Remove(entry);
            entry = newEntry;
            list.Insert(index, entry);

            string[] array = new string[list.Count];

            for (int i = 0; i < list.Count; i++)
            {
                array[i] = ConvertEntry(list[i]);
            }

            File.WriteAllLines(fileName, array);
        }
        /// <summary>
        /// Выбор диапазона дат
        /// </summary>
        /// <param name="start">строка</param>
        /// <param name="end">строка</param>
        /// <param name="list">коллекция Employee</param>
        /// <returns></returns>
        public static List<Employee> SelectInRange(string start, string end, List<Employee> list) => list
                                    .Where(entry => DateTime.Parse(entry.TimeStamp) >= DateTime.Parse(start) && 
                                                            DateTime.Parse(entry.TimeStamp) <= DateTime.Parse(end)).ToList();

        #endregion
    }
}
