using System;
using System.Linq;
using System.Text;

namespace obrotnica_v1a
{
    class Lista
    {
        public string wartosc;
        public Lista next;

        public int Dlugosc
        {
            get { return (next == null ? 1 : 1 + next.Dlugosc); }
        }

        public override string ToString()
        {
            if (next == null) return wartosc + "";
            return wartosc + "|" + next.ToString();
        }
    }

    class Lista2
    {
        public string[] wartosc;
        public Lista2 next;

        public int Dlugosc
        {
            get { return (next == null ? 1 : 1 + next.Dlugosc); }
        }

        public override string ToString()
        {
            if (next == null) return wartosc + "";
            return wartosc + "|" + next.ToString();
        }
    }

   /* public class Wagon
    {
        public int value;
        public Wagon nextNode;

        public Wagon(int v)
        {
            this.value = v;
            this.nextNode = null;
        }

        public override string ToString()
        {
            if (nextNode == null)
                return value + "";
            return value + ", " + nextNode.ToString();
        }
    }*/

    //public class Obrotnica
    //{
    //    public int valueo;
    //    public Obrotnica nextNode;

    //    public Obrotnica(int v)
    //    {
    //        this.valueo = v;
    //        this.nextNode = null;
    //    }

    //    public override string ToString()
    //    {
    //        if (nextNode == null)
    //            return valueo + "";
    //        return valueo + ", " + nextNode.ToString();
    //    }
    //}

  /*  public class Wagon2
    {
        public Wagon head = null;
        public Wagon tail = null;
        public Wagon val;
        //public Obrotnica head1 = null;
        //public Obrotnica tail1 = null;

        public void dodaj(Wagon node)
        {
            node.nextNode = head;
            val = tail;
            tail = head;
            head = node;
        }

        public void usunGlowe()
        {
            if (head != null)
            {
                head = tail;
                if (tail != null)
                    tail = tail.nextNode;
            }
        }


    }      */

    public class Wagon
    {
        public string value;
        public Wagon nextNode;

        public Wagon(string v)
        {
            this.value = v;
            this.nextNode = null;
        }

        public override string ToString()
        {
            if (nextNode == null)
                return value + "";
            return value + ", " + nextNode.ToString();
        }
    }

    public class Lista3
    {
        public Wagon head = null;
        public Wagon tail = null;
        public Wagon val;
        //public Obrotnica head1 = null;
        //public Obrotnica tail1 = null;

        public void dodaj(Wagon node)
        {
            node.nextNode = head;
            val = tail;
            tail = head;
            head = node;
        }

        public void usunGlowe()
        {
            if (head != null)
            {
                head = tail;
                if (tail != null)
                    tail = tail.nextNode;
            }
        }


        //public void dodaj_do_obrotnicy(Obrotnica obr)
        //{
        //    obr.nextNode = head1;
        //    tail1 = head1;
        //    head1 = obr;
        //}

        public override string ToString()
        {
            if (head == null) return "NULL";
            return head.ToString();
        }
    }

    public class Flaga
    {
        public int fl;
    }

    public class flaga2
    {
        public int flaga;

        public flaga2 (int v)
        {
            this.flaga = v;            
        }
    }

   /* struct SimpleStruct
    {
        private int xval;
        private int min;
        

        public int X
        {
            get
            {
                return xval;
            }
            set
            {
                if (value < 100)                    xval = value;
                else xval = 3;

              


            }
        }
        public void DisplayX()
        {
            Console.WriteLine("The stored value is: {0}", xval);
        }
    }*/


}
