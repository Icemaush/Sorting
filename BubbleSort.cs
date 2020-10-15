/*
 * Author: Reece Pieri
 * ID: M087496
 * Date: 7/10/2020
 * Assessment: Portfolio AT2 - Question 3
 */

namespace Sorting
{
    class BubbleSort
    {
        public static void Sort(int[] array)
        {
            int temp;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 1; j < (array.Length - i); j++)
                {
                    if (array[j - 1] > array[j])
                    {
                        temp = array[j - 1];
                        array[j - 1] = array[j];
                        array[j] = temp;
                    }
                }
            }
        }
    }
}
