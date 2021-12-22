using System.Collections.Generic;
using static System.Console;

namespace exercise_8_3
{
    internal class Program
    {
        private static HashSet<int> _hashSet = new HashSet<int>();
        private static bool _success;

        private static void Main(string[] args)
        {
            do
            {
                // Input
                int _number;
                Write("Введите число: ");
                int.TryParse(ReadLine(), out _number);

                // Add
                _success = _hashSet.Add(_number);

                // Output
                if (_success)
                    WriteLine("Число успешно добавлено.");
                else
                    WriteLine("Число уже было добавлено ранее.");

            } while (_success);

            ReadKey();
        }
    }
}
