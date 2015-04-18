using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Fight2_Yabl
{
    public struct Poin
    {
        public int X;
        public int Y;

        public int PoinX()
        {
            return X;
        }
        public int PoinY()
        {
            return Y;
        }
    }

    public struct Rectangl
    {
        public Poin A;
        public Poin B;
        public Poin C;
        public Poin D;

        public Rectangl(int x, int y, int l)
        {
            A.X = x;
            A.Y = y;
            B.X = x + l;
            B.Y = y;
            C.X = x;
            C.Y = y + l;
            D.X = x + l;
            D.Y = y + l;
        }

        public int CoordX(Poin a)
        {
            return a.PoinX();
        }

        public void Border(string p)
        {
            Console.SetCursorPosition(A.X+3, A.Y-3);
            Console.WriteLine(p);
            for (int i = A.X + 1, j = 1; i < A.X + (B.X - A.X); i++, j++)
            {
                Console.SetCursorPosition(i, A.Y);
                Console.WriteLine('-');
                Console.SetCursorPosition(i, A.Y - 1);
                Console.WriteLine(j);
                Console.SetCursorPosition(i, A.Y + (B.X - A.X));
                Console.WriteLine('-');
                Console.SetCursorPosition(i, A.Y + (B.X - A.X) + 1);
                Console.WriteLine(j);
            }
            char c = 'A';
            for (int i = A.Y + 1; i < A.Y + (B.X - A.X); i++, c++)
            {
                Console.SetCursorPosition(A.X, i);
                Console.WriteLine('|');
                Console.SetCursorPosition(A.X - 1, i);
                Console.WriteLine(c);
                Console.SetCursorPosition(A.X + (B.X - A.X), i);
                Console.WriteLine('|');
                Console.SetCursorPosition(A.X + (B.X - A.X) + 1, i);
                Console.WriteLine(c);
            }
            Console.SetCursorPosition(A.X, A.Y);
            Console.WriteLine('+');
            Console.SetCursorPosition(B.X, B.Y);
            Console.WriteLine('+');
            Console.SetCursorPosition(C.X, C.Y);
            Console.WriteLine('+');
            Console.SetCursorPosition(D.X, D.Y);
            Console.WriteLine('+');
        }

    }
}

