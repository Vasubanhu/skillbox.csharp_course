using static System.Console;

namespace exercise_3_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;

            Write("Приветствуем! Сколько у вас карт: ");
            int n = int.Parse(ReadLine());

            for (int i = 1; i <= n; i++)
            {
                Write("\nВведите номинал карты (2-10, J, Q, K, T): ");
                string card = ReadLine();

                switch (card)
                {
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                    case "9":
                    case "10":
                        sum += int.Parse(card);
                        break;
                    case "J": 
                    case "Q": 
                    case "K": 
                    case "T":
                        sum += 10;
                        break;
                    default:
                        Write("Такого номинала нет.");
                        break;
                }
            }   
         
            WriteLine($"\nОбщий \"вес\" карт: {sum}");
            ReadKey();
        }
    }
}
