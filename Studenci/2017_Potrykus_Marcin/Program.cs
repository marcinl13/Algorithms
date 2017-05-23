/* w debug/bin -> TextFile.txt
7
6
#
1 2 3 4 5
6

7 8 9
10
14
15 16
#
11
12
13
#
r 3
l 10
i 1
r 3
o 2
*/

using System;
using System.Linq;
using System.Text;
using System.IO;
using ob;
using System.Text.RegularExpressions;


namespace obrotnica_v_2_0
{    
    class Program
    { 
        static void DodajNaKoncu(Lista l, string wartosc)
        {
            if (l.next != null)  DodajNaKoncu(l.next, wartosc);
            
            else
            {
                l.next = new Lista();
                l.next.wartosc = wartosc;
            }
        }
        
        static void UsuńPrzód(ref Lista l)
        {
            l = l.next;
        }

        static void inicjacja(Lista dane, Lista tory,   Lista3 obrotnica, Lista rozkazy)
        {
            int licz = 0;
            try
            {
                string line;
                StreamReader sr = new StreamReader("TextFile.txt");

                while ((line = sr.ReadLine()) != null)
                {
                    if (line == "#")  licz++; 

                    else
                    {
                        if      (licz == 0) DodajNaKoncu(dane, line);
                        else if (licz == 1) DodajNaKoncu(tory, line);
                        else if (licz == 2) obrotnica.dodaj(new Wagon(line));
                        else DodajNaKoncu(rozkazy, line);  
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine(e.Message);
            }
        }

        static string[] Rozdziel(string wyraz)
        {
            return Regex.Split(wyraz, @"\W+");
        }

        static void lewo(string liczba, Flaga f , int ilosc_torów)  //obroty lewo
        {   
            int licz = Convert.ToInt16(liczba) % ilosc_torów;
            
            if ((f.fl + licz) > ilosc_torów) f.fl = ilosc_torów - (ilosc_torów - licz + 1);
            else  f.fl += licz;
        }

        static void prawo(string liczba, Flaga f, int ilosc_torów) //obroty w prawo
        { 
            int licz = Convert.ToInt16(liczba) % ilosc_torów;
            
            if ((f.fl - licz) < 0) f.fl = 1+(ilosc_torów - licz);
            else f.fl -= licz;                     
        }
    
        static void pobierz(Lista3 obrotnica, Lista3 tablica, string liczba, string obrotnica_max) 
        {
            int a=0;                       
            if ((obrotnica.dlugosc - 1) > Convert.ToInt16(obrotnica_max)) Console.WriteLine("Nie mozna więcej pobrać");
            else
            {         
                if (tablica.dlugosc > Convert.ToInt16(liczba)) a = Convert.ToInt16(liczba);
                else a = tablica.dlugosc;
                
                for (int i = 0; i < a; i++)
                {
                    string dodaj = tablica.head.value;
                    obrotnica.dodaj(new Wagon(dodaj));
                    tablica.usunGlowe();                    
                }
            }   
        }

        static void wydaj(Lista3 obrotnica, Lista3 tablica, string liczba, string obrotnica_max) 
        {
            int a = 0;
            if (obrotnica.dlugosc-1 > Convert.ToInt16(liczba)) a=Convert.ToInt16(liczba);
            else a = obrotnica.dlugosc-1;
            for (int i = 0; i < a; i++)
            {
                string dodaj = obrotnica.head.value;
                tablica.dodaj(new Wagon(dodaj));
                obrotnica.usunGlowe();
            }            
        }

        static void rozkazywanie(Lista r, Lista d, Lista3 ob, Lista t)
        {            
            Flaga flaga = new Flaga();            
            flaga.fl = 0;
            Lista3[] toryw = new Lista3[Convert.ToInt16(d.next.wartosc)];
            int dlugosc = r.Dlugosc-1;
            string [] symbol=new string[r.Dlugosc];
            string [] liczba=new string[r.Dlugosc];

            for (int i = 0; i < dlugosc; i++)//rozdziel rozkazy
            { 
                string [] a = Rozdziel(r.next.wartosc);
                symbol[i] = a[0]; 
                liczba[i] = a[1];
                UsuńPrzód(ref r);
            }
                       
            for (int ii = 0; ii < Convert.ToInt16(d.next.wartosc); ii++)    //generuj tory
            {
                toryw[ii] = new Lista3();
                string[] ww = Rozdziel(t.next.wartosc);
                for (int w = 0; w < ww.Length; w++) //generuj wagony
                {
                    toryw[ii].dodaj(new Wagon(ww[w]));
                }
                UsuńPrzód(ref t);
            }
            Console.WriteLine("");                                                                    
            for (int ii = 0; ii < Convert.ToInt16(d.next.wartosc); ii++) { Console.WriteLine("Tor " + (ii+1 ) + "\t<{0}", toryw[ii] + ">"); }
            Console.WriteLine("Obrotnica <{0}>\n",ob);


            for (int i = 0; i < dlugosc; i++)//wydaj rozkaz
            {
                if (symbol[i] == "r") { prawo(liczba[i], flaga, (Convert.ToInt16(d.next.wartosc) - 1)    ); }
                else if (symbol[i] == "l") { lewo(liczba[i], flaga, (Convert.ToInt16(d.next.wartosc) - 1)); }
                else if (symbol[i] == "i")
                {
                    pobierz(ob, toryw[flaga.fl], liczba[i], d.next.next.wartosc);
                    Console.WriteLine("-------------------------------------------------------");
                    for (int ii = 0; ii < Convert.ToInt16(d.next.wartosc); ii++) { Console.WriteLine("Tor " + (ii + 1) + "\t<{0}", toryw[ii] + ">"); }
                    Console.WriteLine("obrotnica <{0}>\n", ob);
                }
                else
                {
                    wydaj(ob, toryw[flaga.fl], liczba[i], d.next.next.wartosc);
                    Console.WriteLine("-------------------------------------------------------");
                    for (int ii = 0; ii < Convert.ToInt16(d.next.wartosc); ii++) { Console.WriteLine("Tor " + (ii + 1) + "\t<{0}", toryw[ii] + ">"); }
                    Console.WriteLine("obrotnica <{0}>\n", ob);
                }
            }
        }
       
        static void Main(string[] args)
        {
            Lista dane = new Lista();
            Lista tory = new Lista();
            Lista3 obrotnica = new Lista3();
            Lista rozkazy = new Lista();

            Console.WriteLine("OBROTNICA 2.0");                       
            
            inicjacja(dane, tory, obrotnica, rozkazy);            
            rozkazywanie(rozkazy, dane, obrotnica, tory);
        
            Console.ReadKey();
        }
    }
}
