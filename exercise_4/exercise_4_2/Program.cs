using System;
using static System.Console;

namespace exercise_4_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Write("Введите длину последовательности целых чисел: ");

            int n = int.Parse(ReadLine());
            int[] array = new int[n];
            WriteLine();

            for (int i = 0; i < n; i++)
            {
                Write("Введите целое число: ");
                array[i] = int.Parse(ReadLine());
            }

            //int min = int.MaxValue;

            //for (int i = 0; i < array.Length; i++)
            //{
            //    if (array[i] < min)
            //    {
            //        min = array[i];
            //    }
            //}

            //WriteLine($"\nНаименьшее число последовательности: {min}");

            Array.Sort(array);
            WriteLine($"\nНаименьшее число последовательности: {array[0]}");

            ReadKey();
        }
    }
}
