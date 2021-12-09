using static System.Console;

namespace exercise_2_1
{
    internal class Program
    {     
        static void Main(string[] args)
        {
            string fullName = "Иван Иванов";
            string email = "iivanov@school13.ru";
            byte age = (byte)15;
            float programmingPoints = 90f;
            float mathPoints = 80f;
            float physicsPoints = 74f;
            float sum;
            float average;

            string pattern = $"Ф.И.О.: {fullName, 12}\nEmail: {email, 21}\nВозраст: {age}\nБаллы по программированию: {programmingPoints}\nБаллы по математике: {mathPoints}\nБаллы по физике: {physicsPoints}";

            sum = programmingPoints + mathPoints + physicsPoints;
            average = (programmingPoints + mathPoints + physicsPoints) / 3;

            WriteLine(pattern);

            ReadKey();

            Write($"\nCумма баллов: {sum} \nСредний балл: {average.ToString("#.##")}");

            ReadKey();
        }
    }
}
