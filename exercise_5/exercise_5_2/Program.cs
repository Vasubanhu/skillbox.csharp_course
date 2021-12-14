using System;
using static System.Console;
using static exercise_5_1.Program;

namespace exercise_5_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            WriteLine("Напишите предложение: ");
            string text = ReadLine();

            Output(ReverseWords(text));

            ReadKey();
        }
        /// <summary>
        /// Меняет местами элементы строки 
        /// </summary>
        /// <param name="inputPhrase">строка</param>
        /// <returns>строка</returns>
        private static string ReverseWords(string inputPhrase)
        {
            string[] words = SplitString(inputPhrase);

            //Array.Reverse(words);

            int n = words.Length;

            string[] reversePhrase = new string[words.Length];

            for (int i = 0; i < n; i++)
            {
                reversePhrase[n - 1 - i] = words[i];
            }

            for (int i = 0; i < n; i++)
            {
                words[i] = reversePhrase[i];
            }

            return String.Join(" ", words);
        }
    }
}
