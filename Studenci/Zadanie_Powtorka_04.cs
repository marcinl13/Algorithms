using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie_Powtorka_04
{
    class Program
    {
        static int kroki = 0;

        static void wypisz(int[] B)
        {
            foreach (int x in B)
            {
                Console.Write("{0} ", x);
            }
        }
        static void Swap(int[] tab, int i, int j)
        {
            int pom = tab[i];
            tab[i] = tab[j];
            tab[j] = pom;
        }

        
        // Sortowanie bąbelkowe
        
        static void sortowanieBabelkowe(int[] tab)
        {
            
            for (int i = 1; i < tab.Length; i++)
                for (int j = tab.Length - 1; j > 0; j--)
                    if (tab[j - 1] > tab[j])
                    {
                        Swap(tab, j - 1, j);
                        kroki++;
                        Console.Write("\nkrok {0}\t ", kroki);
                        wypisz(tab);
                    }
        }

        
        // Sortowanie przez wstawianie
        
        static void sortowanieWstawianie(int[] tab)
        {
            int x, j;
            for (int i = 1; i < tab.Length - 1; i++)
            {
                //a[0..i-1] jest już posortowana
                x = tab[i]; //odkładamy element z pozycji i
                j = i;
                while (j > 0 && x < tab[j - 1])
                {
                    tab[j] = tab[j - 1]; //przesuwamy element większy od x o jedną pozycję w prawo    
                    j = j - 1;
                    
                    Console.Write("\nkrok {0}\t ", kroki);
                    kroki++;
                    wypisz(tab);

                }
                tab[j] = x;
            }
        }

        
        // Merge Sort
        
        public static void MergeSort(int[] input, int low, int high)
        {

            if (low < high)
            {
                int middle = (low / 2) + (high / 2);
                MergeSort(input, low, middle);
                MergeSort(input, middle + 1, high);
                Merge(input, low, middle, high);

                Console.Write("\nkrok {0}\t ", kroki);
                kroki++;
                wypisz(input);
            }
        }

        public static void MergeSort(int[] input)
        {
            MergeSort(input, 0, input.Length - 1);
        }

        private static void Merge(int[] input, int low, int middle, int high)
        {
            int left = low;
            int right = middle + 1;
            int[] tmp = new int[(high - low) + 1];
            int tmpIndex = 0;

            while ((left <= middle) && (right <= high))
            {
                if (input[left] < input[right])
                {
                    tmp[tmpIndex] = input[left];
                    left = left + 1;
                }
                else
                {
                    tmp[tmpIndex] = input[right];
                    right = right + 1;
                }
                tmpIndex = tmpIndex + 1;
            }

            if (left <= middle)
            {
                while (left <= middle)
                {
                    tmp[tmpIndex] = input[left];
                    left = left + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }
            if (right <= high)
            {
                while (right <= high)
                {
                    tmp[tmpIndex] = input[right];
                    right = right + 1;
                    tmpIndex = tmpIndex + 1;
                }
            }
            for (int i = 0; i < tmp.Length; i++)
            {
                input[low + i] = tmp[i];
            }
        }

        //
        // QuictSort
        //
        public static void QuickSort(int[] input, int left, int right)
        {
            if (left < right)
            {
                int q = Partition(input, left, right);
                QuickSort(input, left, q - 1);
                QuickSort(input, q + 1, right);

                Console.Write("\nkrok {0}\t ", kroki);
                kroki++;
                wypisz(input);
            }
        }

        private static int Partition(int[] input, int left, int right)
        {
            int pivot = input[right];
            int temp;

            int i = left;
            for (int j = left; j < right; j++)
            {
                if (input[j] <= pivot)
                {
                    temp = input[j];
                    input[j] = input[i];
                    input[i] = temp;
                    i++;
                }
            }

            input[right] = input[i];
            input[i] = pivot;

            return i;
        }

        static void Main(string[] args)
        {
            int N = 10;
            int[] A = new int[N];

            Random randnumber = new Random();
            for (int i = 0; i < N; i++) { A[i] = randnumber.Next(10); }

            Console.Write("Tablica {0} elementowa: ", N);
            wypisz(A);

            Console.WriteLine("\n\nsortowanieBabelkowe:");
            sortowanieBabelkowe(A);

            Console.WriteLine("\nsortowanieWstawianie:");
            sortowanieWstawianie(A);

            Console.WriteLine("\nMergeSort:");
            MergeSort(A);

            Console.WriteLine("\nQuickSort:");
            QuickSort(A, 0, A.Length - 1);

            Console.ReadKey();
        }
    }
}
