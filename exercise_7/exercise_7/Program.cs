using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;
using static exercise_7.Employee;

namespace exercise_7
{
    internal class Program
    {
        private static string _id;
        private static readonly string _fileName = @"employees.txt";
        private static readonly char _separator = '#';
        private static string _dateTimePattern = "dd.MM.yy HH:mm";
        private static string _dashPattern = "---------------------------------------------------------------------------------------------------------";
        private static string _headerPattern = @"|ID                             |Дата и время  |Ф.И.О.  |Возраст  |Рост  |Дата рождения  |Место рождения|";

        private static void Main()
        {
            WriteLine("Введите номер операции и нажмите Enter.\n" + "\n" +
                      "* Просмотр всех записей - 1;\n" +
                      "* Просмотр записи по ID сотрудника - 2;\n" +
                      "* Добавление записи - 3.\n" +
                      "* Удаление записи - 4.");

            string action = ReadLine();

            switch (action)
            {
                case "1":
                    Clear();
                    WriteLine("Данные о сотрудниках.");
                    Clear();
                    WriteLine($"{_dashPattern}\n{_headerPattern}\n{ _dashPattern}");
                    OutputData(ReadData(_fileName));
                    break;
                case "2":
                    Clear();
                    Write("Просмотр записи. Введите id записи: ");
                    _id = ReadLine();// метод
                    Clear();
                    WriteLine($"{_dashPattern}\n{_headerPattern}\n{ _dashPattern}");
                    WriteLine(GetEntry(_id, _fileName));
                    break;
                case "3":
                    Clear();
                    WriteData(ConvertEntry(CreateEntry(InputData())));
                    break;
                case "4":
                    Clear();
                    Write("Удаление записи. Введите id записи: ");
                    _id = ReadLine();// метод
                    RemoveEntry(_id, _fileName);
                    break;
                case "5":
                    Clear();
                    Write("Редактирование записи. Введите id записи: ");
                    _id = ReadLine();// метод
                    EditEntry();
                    break;
                default:
                    WriteLine("Некорректный номер операции.");
                    break;
            }

            WriteLine("\nДля выхода нажмите Enter.");
            ReadKey();
        }

        #region Methods
        /// <summary>
        /// Читает данные из файла
        /// </summary>
        /// <param name="fileName">имя файла</param>
        /// <returns>коллекция строк</returns>
        private static List<string> ReadData(string fileName)
        {
            List<string> list = new List<string>();

            if (File.Exists(fileName))
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        list.Add(streamReader.ReadLine());
                    }
                }
            }

            else
            {
                WriteLine("Файл не найден.");
            }

            return list;
        }
        /// <summary>
        /// Выводит данные в консоль
        /// </summary>
        /// <param name="list">коллекция строк</param>
        private static void OutputData(List<string> list)
        {
            foreach (string item in list)
            {
                WriteLine(Trim(item));      
            }
        }

        private static string InputData()
        {
            string entry = String.Empty;
            string timeStamp;
            string id = GenerateID();

            entry += id + _separator;
            Write($"Введите данные сотрудника.\n-------------------------\nID: {id}\n");

            timeStamp = DateTime.Now.ToString(_dateTimePattern);
            Write($"Дата и время добавления записи: {timeStamp}\n");
            entry += timeStamp + _separator;

            Write("Ф.И.О.: ");
            entry += ReadLine() + _separator;

            Write("Возраст: ");
            entry += ReadLine() + _separator;

            Write("Рост: ");
            entry += ReadLine() + _separator;

            Write("Дата рождения: ");
            entry += ReadLine() + _separator;

            Write("Место рождения: ");
            entry += ReadLine();

            return entry;
        }

        private static void WriteData(string data)
        {
            if (File.Exists(_fileName))
            {
                using (StreamWriter streamWriter = File.AppendText(_fileName))
                {
                    streamWriter.WriteLine(data);
                }
            }

            else
            {
                using (StreamWriter streamWriter = File.CreateText(_fileName))
                {
                    streamWriter.WriteLine(data);
                }
            }
        }

        private static string GenerateID() => string.Format($"{Guid.NewGuid():N}");// ?
        #endregion
    }
}