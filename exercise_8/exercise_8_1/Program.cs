using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace exercise_8_1
{
    internal class Program
    {
        private static List<int> _list = new List<int>();
        private static int[] _parametrs = { 25, 50 };
        private static Random _random = new Random();
        private static readonly int _max = 101;

        private static void Main(string[] args)
        {
            _list = Fill(_list);
            Output(_list);
            _list = RemoveAt(_list, _parametrs[0], _parametrs[1]);
            Output(_list);

            ReadKey();
        }

        private static void Output(List<int> list)
        {
            IEnumerable<int> sequence = _list.Select(element => element);
            WriteLine("\n" + string.Join(" ", sequence));
        }

        private static List<int> Fill(List<int> list) => Enumerable.Range(0, _max)
                                                                   .Select(r => _random.Next(_max))
                                                                   .ToList();

        private static List<int> RemoveAt(List<int> list, params int[] array) => 
            list = list.Where(element => !(element > array[0] && element < array[1])).ToList();

    }
}
