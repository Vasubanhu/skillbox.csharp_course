using System;
using static System.Console;

namespace exercise_4_3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            string input;

            Write("Введите целое положительное число (максимальное число диапазона чисел): ");
            int n = int.Parse(ReadLine());
            int hiddenNumber = random.Next(0, n + 1);

            Write($"\nПопробуйте угадать число от 0 до {n}.");

            do
            {
                WriteLine();
                Write("\nВедите число: ");
                input = ReadLine();

                if (input.ToString() == "")
                {
                    WriteLine("\nУстали играть? Нажмите Enter для выхода.");
                    ReadKey();
                    Write($"Было загадано число {hiddenNumber}.");
                    break;
                }

                else if (int.Parse(input) == hiddenNumber)
                {
                    WriteLine("\nВы угадали!");
                    break;
                }

                else
                {
                    switch ((int)Math.Sign(int.Parse(input) - hiddenNumber))
                    {
                        case 1:
                            Write($"Загаданное число меньше чем {input}.");
                            break;
                        case -1:
                            Write($"Загаданное число больше чем {input}.");
                            break;
                    }
                }
            } while (true);

            ReadKey();
        }
    }
}
