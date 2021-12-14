using static System.Console;

namespace exercise_5_1
{
    public class Program
    {
        private static void Main()
        {
            WriteLine("Напишите предложение: ");
            string text = ReadLine();

            Output(SplitString(text));

            ReadKey();
        }
        /// <summary>
        /// Конвертирует строку в массив строк
        /// </summary>
        /// <param name="text">строка</param>
        /// <returns>массив строк</returns>
        public static string[] SplitString(string text)
        {
            string[] words;
            return words = text.Split(' ');
        }
        /// <summary>
        /// Вывод на консоль
        /// </summary>
        /// <param name="array">массив</param>
        public static void Output(string[] array)
        {
            WriteLine();

            foreach (string item in array)
            {
                WriteLine($"{item}");
            }
        }
        /// <summary>
        /// Вывод на консоль
        /// </summary>
        /// <param name="text">строка</param>
        public static void Output(string text)
        {
            WriteLine(text);
        }
    }
}
