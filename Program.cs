using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

/*
 * Author: Reece Pieri
 * ID: M087496
 * Date: 7/10/2020
 * Assessment: Portfolio AT2 - Question 3
 */

namespace Sorting
{
    class Program
    {
        private static Stopwatch sw = new Stopwatch();
        private static int[] salaryList;

        static void Main(string[] args)
        {
            salaryList = new int[1000];
            PopulateList(salaryList.Length);
            //DisplaySalaries(salaryList);
            DisplayMenu();
        }

        // Generate random number between 1 and 10,000,000
        private static int GenerateRandomNumber()
        {
            Random rand = new Random();
            return rand.Next(1, 10_000_000);
        }

        // Fill salarylist with random numbers
        private static void PopulateList(int number)
        {
            Array.Resize(ref salaryList, number);

            for (int i = 0; i < number; i++)
            {
                salaryList[i] = GenerateRandomNumber();
            }
        }

        // Display items in salarylist
        private static void DisplaySalaries()
        {
            foreach (var salary in salaryList)
            {
                Console.WriteLine(salary);
            }
            Console.WriteLine("Total: " + salaryList.Length);
        }

        // Prompt number of salaries to perform sort methods on.
        private static void PromptSalaryNumber()
        {
            while (true)
            {
                Console.Write("Enter number of salaries: ");

                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    continue;
                }

                PopulateList(input);
                break;
            }
            
        }

        // Display menu
        private static void DisplayMenu()
        {
            string[] options = new string[]
            {
                "1. All sort methods (may take a while)",
                "2. Merge sort",
                "3. Bubble sort",
                "4. Radix sort",
                "5. Change number of salaries",
                "6. Display salaries",
                "7. Exit"
            };

            while (true)
            {
                sw.Reset();

                Console.WriteLine();
                Console.WriteLine("Current number of salaries: " + salaryList.Length + "\n");
                foreach (var option in options)
                {
                    Console.WriteLine(option);
                }
                Console.Write("Select an option: ");

                // Check if input is a number
                if (!int.TryParse(Console.ReadLine(), out int input))
                {
                    continue;
                }

                switch (input)
                {
                    // Perform all sort method tests
                    case 1:
                        PerformAll(PerformMergeSortTest(), PerformBubbleSortTest(), PerformRadixSortTest());
                        break;
                    // Perform merge sort test
                    case 2:
                        PerformMergeSortTest();
                        break;
                    // Perform bubble sort test
                    case 3:
                        PerformBubbleSortTest();
                        break;
                    // Perform radix sort test
                    case 4:
                        PerformRadixSortTest();
                        break;
                    // Chage number of salaries
                    case 5:
                        PromptSalaryNumber();
                        break;
                    case 6:
                        DisplaySalaries();
                        break;
                    // Exit program
                    case 7:
                        return;
                    default:
                        continue;
                }
            }

        }

        // Perform merge sort x100 and write to .csv file
        private static long[] PerformMergeSortTest()
        {
            try
            {
                long[] results;

                using (StreamWriter outputFile = new StreamWriter(Path.Combine("MergeSort.csv")))
                {
                    results = new long[100];

                    for (int i = 0; i < 100; i++)
                    {
                        int[] salaryListCopy = GetListCopy();
                        sw.Start();
                        MergeSort.Sort(salaryListCopy);
                        sw.Stop();
                        results[i] = sw.ElapsedMilliseconds;
                        outputFile.WriteLine(sw.ElapsedMilliseconds);
                        sw.Reset();
                    }

                    Console.WriteLine("Performed 100 merge sorts. Average completion time: " + (results.Sum() / results.Length) + "\n");
                    outputFile.WriteLine(results.Sum() / results.Length);
                };

                return results;
            } catch (IOException)
            {
                Console.WriteLine("File is in use. Close the file and try again.");
                return null;
            }
        }

        // Perform bubble sort x100 and write to .csv file
        private static long[] PerformBubbleSortTest()
        {
            try
            {
                long[] results;

                using (StreamWriter outputFile = new StreamWriter(Path.Combine("BubbleSort.csv")))
                {
                    results = new long[100];

                    for (int i = 0; i < 100; i++)
                    {
                        int[] salaryListCopy = GetListCopy();
                        sw.Start();
                        BubbleSort.Sort(salaryListCopy);
                        sw.Stop();
                        results[i] = sw.ElapsedMilliseconds;
                        outputFile.WriteLine("," + sw.ElapsedMilliseconds);
                        sw.Reset();
                    }

                    Console.WriteLine("Performed 100 bubble sorts. Average completion time: " + (results.Sum() / results.Length) + "\n");
                    outputFile.WriteLine("," + results.Sum() / results.Length);
                };

                return results;
            }
            catch (IOException)
            {
                Console.WriteLine("File is in use. Close the file and try again.");
                return null;
            }
        }

        // Perform radix sort x100 and write to .csv file
        private static long[] PerformRadixSortTest()
        {
            try
            {
                long[] results;

                using (StreamWriter outputFile = new StreamWriter(Path.Combine("RadixSort.csv")))
                {
                    results = new long[100];

                    for (int i = 0; i < 100; i++)
                    {
                        int[] salaryListCopy = GetListCopy();
                        sw.Start();
                        RadixSort.Sort(salaryListCopy);
                        sw.Stop();
                        results[i] = sw.ElapsedMilliseconds;
                        outputFile.WriteLine(",," + sw.ElapsedMilliseconds);
                        sw.Reset();
                    }

                    Console.WriteLine("Performed 100 radix sorts. Average completion time: " + (results.Sum() / results.Length) + "\n");
                    outputFile.WriteLine(",," + results.Sum() / results.Length);
                };

                return results;
            }
            catch (IOException)
            {
                Console.WriteLine("File is in use. Close the file and try again.");
                return null;
            }
        }

        // Create a copy of the salary list
        private static int[] GetListCopy()
        {
            int[] salaryListCopy;
            salaryListCopy = new int[salaryList.Length];
            Array.Copy(salaryList, salaryListCopy, salaryList.Length);
            return salaryListCopy;
        }

        // Write results to file
        private static void PerformAll(long[] mergeSortResults, long[] bubbleSortResults, long[] radixSortResults)
        {
            try
            {
                using (StreamWriter outputFile = new StreamWriter(Path.Combine("Results.csv")))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        outputFile.WriteLine(mergeSortResults[i] + "," + bubbleSortResults[i] + "," + radixSortResults[i]);
                    }
                }
            } catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

     }
}


