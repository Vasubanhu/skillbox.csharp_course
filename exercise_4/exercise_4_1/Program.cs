using System;
using static System.Console;

namespace exercise_4_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int sum = 0;

            Write("Введите количество строк: ");
            int row = int.Parse(ReadLine());
            Write("Введите количество столбцов: ");
            int column = int.Parse(ReadLine());
            WriteLine();

            int[,] array = new int[row, column];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    array[i, j] = random.Next(1, 10);
                    sum += array[i, j];
                    Write($"{array[i, j],3} ");
                }
                WriteLine();
            }

            WriteLine($"\nСумма всех элеметнов матрицы: {sum}");

            ReadKey();
        }
    }
}
