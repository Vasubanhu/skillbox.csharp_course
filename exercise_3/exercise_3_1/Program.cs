using static System.Console;

namespace exercise_3_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Write("Введите целое число: ");
            int number = int.Parse(ReadLine());

            if (number % 2 == 0)
            {
                WriteLine($"\n{number} чётное число.");
            }

            else
            {
                WriteLine($"\n{number} нечётное число.");
            }

            ReadKey();
        }
    }
}
