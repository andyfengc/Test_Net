using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.Sort
{
    public class BubbleSortTest
    {
        public static int[] SortV1(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0) return new int[]{};
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = i+1; j < numbers.Length; j++)
                {
                    if (numbers[i] > numbers[j])
                    {
                        // swap
                        var temp = numbers[i];
                        numbers[i] = numbers[j];
                        numbers[j] = temp;
                    }
                }
            }
            return numbers;
        }
        public static int[] SortV2(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0) return new int[] { };
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                for (int j = 0; j < numbers.Length-1-i; j++)
                {
                    if (numbers[j] > numbers[j+1])
                    {
                        // swap
                        var temp = numbers[j];
                        numbers[j] = numbers[j+1];
                        numbers[j + 1] = temp;
                    }
                }
            }
            return numbers;
        }
    }
}
