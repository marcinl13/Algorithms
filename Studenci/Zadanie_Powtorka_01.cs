using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//algorytm wyszukujący przywódcę ciągu po sortowaniu
namespace Zadanie_Powtorka_01
{ 
    class Program
    {
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
        static void sortowanieBabelkowe(int[] tab)
        {
            for (int i = 1; i < tab.Length; i++)
                for (int j = tab.Length - 1; j > 0; j--)
                    if (tab[j - 1] > tab[j])
                        Swap(tab, j - 1, j);
        }


        static int PrzywodcaCiagu(int[] A)
        {
            int ile = 0;
            int j = 0;
            int n = A.Length;

            for (int i = 0; i < n; i++)
                if (ile == 0)
                {
                    ile = ile + 1;
                    j = i;
                }
                else if (A[i] == A[j])
                    ile = ile + 1;
                else ile = ile - 1;

            return A[j];
        }

        //static int PrzywodcaCiagu_v2(int[] A)
        

        static void Main(string[] args)
        {
            int N = 20;
            int[] A = new int[N];
            Random randnumber = new Random();
            for (int i = 0; i < N; i++) { A[i] = randnumber.Next(10); }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.Write("Wypisz tablice: ");
            wypisz(A);

            stopwatch.Stop(); // stopwatch
            stopwatch.Start();

            Console.Write(", milis = {0}\nSortuj tablice: ", stopwatch.ElapsedMilliseconds);

            sortowanieBabelkowe(A);
            wypisz(A);

            stopwatch.Stop(); // stopwatch
            stopwatch.Start();

            Console.Write(", milis = {0}\n", stopwatch.ElapsedMilliseconds);
            
            int wynik = PrzywodcaCiagu(A);

            stopwatch.Stop();


            Console.Write("Przywódca ciągu: {0} ", wynik);

            Console.ReadLine();
        }
    }
}
