using System;
using System.Collections.Generic;
using static System.Console;

namespace exercise_8_2
{
    internal class Program
    {
        private static Dictionary<string, string> _dictionary = new Dictionary<string, string>();
        private static string _phoneNumber;
        private static string _fullName;

        private static void Main(string[] args)
        {
            while (true)
            {
                // Input
                Clear();
                Write("Введите номер телефона: ");
                _phoneNumber = ReadLine();
                if (string.IsNullOrEmpty(_phoneNumber))
                    break;
                Write("Введите Ф.И.О. владельца номера: ");
                _fullName = ReadLine();
                if (string.IsNullOrEmpty(_fullName))
                    break;

                try
                {
                    _dictionary.Add(_phoneNumber, _fullName);
                }
                catch (Exception e)
                {
                    WriteLine(e);
                    throw;
                }
            }

            Clear();
            Write("Введите номер телефона для поиска владельца: ");
            _phoneNumber = ReadLine();

            string result;

            if (_dictionary.TryGetValue(_phoneNumber, out result))
            {
                WriteLine($"\nПо номеру телефона - {_phoneNumber} найден владелец - {result}");
            }

            else
            {
                WriteLine($"\nПо номеру телефона - {_phoneNumber} владелец не найден.");
            }

            ReadKey();
        }
    }
}
