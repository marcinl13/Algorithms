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
        public int dlugosc;

        public void dodaj(Wagon node)
        {
            node.nextNode = head;
            val = tail;
            tail = head;
            head = node;
            dlugosc += 1;
        }

        public void usunGlowe()
        {
            dlugosc -= 1;
            if (head != null)
            {
                head = tail;
                if (tail != null)
                    tail = tail.nextNode;
            }
        }

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

}
