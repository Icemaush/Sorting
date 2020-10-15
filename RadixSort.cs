using System;

/*
 * Author: Reece Pieri
 * ID: M087496
 * Date: 7/10/2020
 * Assessment: Portfolio AT2 - Question 3
 */

namespace Sorting
{
    class RadixSort
    {
        private struct KVEntry
        {
            private int key;
            private int value;
            public int Key
            {
                get { return key; }
                set
                {
                    if (key >= 0)
                        key = value;
                    else
                        throw new Exception("Invalid key value");
                }
            }
            public int Value
            {
                get { return value; }
                set { this.value = value; }
            }
        }

        public static int[] Sort(int[] salaryList)
        {
            return RadixSortAux(salaryList, 1);
        }

        private static int[] RadixSortAux(int[] array, int digit)
        {
            bool Empty = true;
            // array that holds the digits;
            KVEntry[] digits = new KVEntry[array.Length];
            // Hold the sorted array
            int[] SortedArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                digits[i] = new KVEntry();
                digits[i].Key = i;
                digits[i].Value = (array[i] / digit) % 10;
                if (array[i] / digit != 0)
                    Empty = false;
            }
            if (Empty)
                return array;
            KVEntry[] SortedDigits = CountingSort(digits);
            for (int i = 0; i < SortedArray.Length; i++)
                SortedArray[i] = array[SortedDigits[i].Key];
            return RadixSortAux(SortedArray, digit * 10);
        }

        private static KVEntry[] CountingSort(KVEntry[] ArrayA)
        {
            int[] ArrayB = new int[MaxValue(ArrayA) + 1];
            KVEntry[] ArrayC = new KVEntry[ArrayA.Length];
            for (int i = 0; i < ArrayB.Length; i++)
                ArrayB[i] = 0;
            for (int i = 0; i < ArrayA.Length; i++)
                ArrayB[ArrayA[i].Value]++;
            for (int i = 1; i < ArrayB.Length; i++)
                ArrayB[i] += ArrayB[i - 1];
            for (int i = ArrayA.Length - 1; i >= 0; i--)
            {
                int value = ArrayA[i].Value;
                int index = ArrayB[value];
                ArrayB[value]--;
                ArrayC[index - 1] = new KVEntry();
                ArrayC[index - 1].Key = i;
                ArrayC[index - 1].Value = value;
            }
            return ArrayC;
        }

        private static int MaxValue(KVEntry[] arr)
        {
            int Max = arr[0].Value;
            for (int i = 1; i < arr.Length; i++)
                if (arr[i].Value > Max)
                    Max = arr[i].Value;
            return Max;
        }
    }
}
