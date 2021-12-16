using System;
using System.IO;
using static System.Console;

namespace exercise_6
{
    internal class Program
    {
        private static readonly string _fileName = $@"{Directory.GetCurrentDirectory()}\employees.txt";
        private static readonly char _separator = '#';
        private static string _dateTimePattern = "dd.MM.yy HH:mm";
        private static string _dashPattern = "---------------------------------------------------------------------------------------------------------";
        private static string _headerPattern = @"|ID                             |Дата и время  |Ф.И.О.  |Возраст  |Рост  |Дата рождения  |Место рождения|";

        private static void Main()
        {
            WriteLine("Введите номер операции и нажмите Enter.\n" + "\n" +
                      "* Вывести данные о сотруднике на экран - 1;\n" +
                      "* Заполнить данные о сотруднике и добавить новую запись в конец файла - 2.");

            string action = ReadLine();

            switch (action)
            {
                case "1":
                    Clear();
                    WriteLine($"Сотрудники:\n{_dashPattern}");
                    WriteLine($"{_headerPattern}");
                    WriteLine($"{ _dashPattern}");
                    ReadData(_fileName);
                    break;
                case "2":
                    Clear();
                    WriteData(InputData());
                    break;
                default:
                    WriteLine("Некорректный номер операции.");
                    break;
            }

            WriteLine("\nДля выхода нажмите Enter.");
            ReadKey();
        }

        #region Methods
        private static string Trim(string text)
        {
            string[] words;
            words = text.Split('#');
            return string.Join(" ", words);
        }

        private static string GenerateID() => string.Format($"{Guid.NewGuid():N}");

        private static void ReadData(string fileName)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    while (streamReader.Peek() >= 0)
                    {
                        WriteLine(Trim(streamReader.ReadLine()));
                    }
                }
            }

            else
            {
                WriteLine("Файл не найден.");
            }
        }

        private static string InputData()
        {
            string record = String.Empty;
            string timeStamp;
            string id = GenerateID();

            record += id + _separator;
            Write($"Введите данные сотрудника.\n-------------------------\nID: {id}\n");

            timeStamp = DateTime.Now.ToString(_dateTimePattern);
            Write($"Дата и время добавления записи: {timeStamp}\n");
            record += timeStamp + _separator;

            Write("Ф.И.О.: ");
            record += ReadLine() + _separator;

            Write("Возраст: ");
            record += ReadLine() + _separator;

            Write("Рост: ");
            record += ReadLine() + _separator;

            Write("Дата рождения: ");
            record += ReadLine() + _separator;

            Write("Место рождения: ");
            record += ReadLine();

            return record;
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
        #endregion
    }
}
