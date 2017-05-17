using System;
using System.Linq;
using System.Text;
using System.IO;
using obrotnica_v1a;
using System.Text.RegularExpressions;


namespace obrotnica_v1
{
    
    class Program
    { 
        static void WyświetlListe(Lista l)
        {
            Console.WriteLine("{0} ", l.wartosc);
            if (l.next != null)
            {
                WyświetlListe(l.next);
            }
        }

        static void DodajNaKońcu(Lista l, string wartosc)
        {
            if (l.next != null)  DodajNaKońcu(l.next, wartosc);
            
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

        static void inicjacja(Lista dane, Lista tory,   Lista obrotnica, Lista rozkazy)
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
                        if      (licz == 0) DodajNaKońcu(dane, line);
                        else if (licz == 1) 
                        { 
                            //Console.Write(line.Length);
                            DodajNaKońcu(tory, line);                            

                        }
                        else if (licz == 2) DodajNaKońcu(obrotnica, line);
                        else DodajNaKońcu(rozkazy, line);  
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine(e.Message);
            }
        }

        static string[] SplitWords(string s)
        {
            return Regex.Split(s, @"\W+");
        }

        static void prawo(string liczba, Flaga f , int i, int ilosc_torów) 
        {
            int l = Convert.ToInt16(liczba);
            int licz2 = l % ilosc_torów;
            Console.WriteLine("licz2 " + licz2);
            //if (licz2 >0)
            //{
                int licz = f.fl - l;
                Console.WriteLine("prawo\t" + l + "flaga\t" + i + "licz\t" + licz);

                if (licz < 0)
                {
                    int aa = ilosc_torów + licz;
                    Console.WriteLine("FLAGA " + aa);
                    f.fl = aa;
                }
            //}         
        }

        static void lewo(string liczba, Flaga f , int i, int ilosc_torów)
        {
            int l = Convert.ToInt16(liczba);
            int licz2 = l % ilosc_torów;
            Console.WriteLine("lewo\t" + l + " flaga\t" + i + " licz\t" + licz2);
            if (licz2 > 1)
            {
                int licz = f.fl - licz2; //Console.WriteLine("licz " + licz);
                if (licz < 0)
                {                    
                    int aa = 0 - licz;
                    Console.WriteLine("FLAGA " + aa);
                    f.fl = aa;
                }
            }
            else 
            {
                int licz = f.fl - l; 
                Console.WriteLine("licz  l2 " + licz);
                if (licz < 0) 
                {
                    int aa = 0 - licz;
                    Console.WriteLine("FLAGA " + aa);
                    f.fl = aa;
                }
            }
        }

        static void pobierz(string liczba) 
        { 
            
            Console.WriteLine("pobierz\t"+liczba); 
        }

        static void wydaj(string liczba) 
        { Console.WriteLine("wydaj\t" + liczba); 
        }

        static void rozkazywanie(Lista r, int ilosc_torów)
        {
            Flaga flaga = new Flaga();
            flaga.fl = 0;
            Console.Write("flaga "+flaga.fl); 


            int dlugosc=r.Dlugosc-1;
            string [] symbol=new string[r.Dlugosc];
            string [] liczba=new string[r.Dlugosc];
            for (int i = 0; i < dlugosc;i++)
            { 
                string [] a = SplitWords(r.next.wartosc);
                symbol[i] = a[0]; 
                liczba[i] = a[1];
                UsuńPrzód(ref r);
            }
            Console.WriteLine("");
            for (int i = 0; i < dlugosc;i++)
            {
                //rozkazuj
                if (symbol[i] == "r") { prawo(liczba[i], flaga, flaga.fl, ilosc_torów); }
                else if (symbol[i] == "l") { lewo(liczba[i],flaga, flaga.fl, ilosc_torów); }
                else if (symbol[i] == "i") { pobierz(liczba[i]); }
                else wydaj(liczba[i]);                
            } Console.WriteLine("");
        }

        static void Main(string[] args)
        {
            Lista dane = new Lista();
            Lista tory = new Lista();
            Lista obrotnica = new Lista();
            Lista rozkazy = new Lista();

           
            
            inicjacja(dane, tory, obrotnica, rozkazy);            
            
            Console.WriteLine("dane\t\t<{0}>\t Dlugość<{1}>", dane.ToString(), dane.Dlugosc-1);
            //Console.WriteLine("tory\t\t<{0}>\t Dlugość<{1}>", tory.ToString(), tory.Dlugosc-1);            
            Console.WriteLine("obrotnica\t<{0}>\t Dlugość<{1}>", obrotnica.ToString(),obrotnica.Dlugosc-1);
            //Console.WriteLine("rozkazy\t\t<{0}\t Dlugość<{1}>>", rozkazy.ToString(),rozkazy.Dlugosc-1);          
            Console.WriteLine("\nROZKAZY");
            

            //SimpleStruct ss = new SimpleStruct();
            //ss.X = 500;
            //ss.DisplayX();
             int ilosc_torów = Convert.ToInt16(dane.next.wartosc)-1;
            Console.WriteLine("ilosc torów" + ilosc_torów);           
            rozkazywanie(rozkazy, ilosc_torów); 
             
            Lista3[] toryw = new Lista3[10];           
            //generuj tory
            for (int ii = 0; ii < ilosc_torów; ii++)
            {
                toryw[ii] = new Lista3();
                string[] ww = SplitWords(tory.next.wartosc);  
                //generuj wagony
                for (int w = 0; w < ww.Length; w++)
                {
                    toryw[ii].dodaj(new Wagon(ww[w]));
                }
                UsuńPrzód(ref tory);
            }
            //wypisz stan torów
            for (int ii = 0; ii < ilosc_torów; ii++) { Console.WriteLine("Tor " + (ii+1) + "\t<{0}", toryw[ii]+">"); }
            

            




           Console.ReadKey();
        }
    }
}
