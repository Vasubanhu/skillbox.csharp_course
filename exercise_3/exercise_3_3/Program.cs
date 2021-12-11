using static System.Console;

namespace exercise_3_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 2;
            bool isSimple = true;

            Write("Введите целое число: ");
            int number = int.Parse(ReadLine());

            while (i <= number / 2)
            {
                if (number % i == 0)
                {
                    isSimple = false;
                    break;
                }
                i++;
            }

            if (isSimple)
            {
                WriteLine($"{number} - простое число.");
            }

            else
            {
                WriteLine($"{number} - не является простым числом.");
            }

            ReadKey();
        }
    }
}
