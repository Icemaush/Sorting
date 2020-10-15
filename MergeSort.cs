using System;

/*
 * Author: Reece Pieri
 * ID: M087496
 * Date: 7/10/2020
 * Assessment: Portfolio AT2 - Question 3
 */

namespace Sorting
{
    class MergeSort
    {
        public static void Sort(int[] array)
        {
            if (array.Length <= 1)
            {
                return;
            }

            int[] first = new int[array.Length / 2];
            int[] second = new int[array.Length - first.Length];
            Array.Copy(array, first, first.Length);
            Array.Copy(array, first.Length, second, 0, first.Length);

            Sort(first);
            Sort(second);

            Merge(first, second, array);
        }

        private static void Merge(int[] first, int[] second, int[] result)
        {
            int firstIndex = 0;
            int secondIndex = 0;
            int mergedIndex = 0;

            while (firstIndex < first.Length && secondIndex < second.Length)
            {
                if (first[firstIndex].CompareTo(second[secondIndex]) < 0)
                {
                    result[mergedIndex] = first[firstIndex];
                    firstIndex++;
                } else
                {
                    result[mergedIndex] = second[secondIndex];
                    secondIndex++;
                }
                mergedIndex++;
            }
            Array.Copy(first, firstIndex, result, mergedIndex, first.Length - firstIndex);
            Array.Copy(second, secondIndex, result, mergedIndex, second.Length - secondIndex);
        }
    }
}
