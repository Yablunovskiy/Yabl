using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Fight2_Yabl
{
    [Serializable]
    public struct Palub
    {
        public int X;
        public int Y;
        public ConsoleColor color;
        public char A;


        public Palub(int x = 0, int y = 0, ConsoleColor? a=null, char? P=null)
        {
            X = x;
            Y = y;
            color= a ?? ConsoleColor.White;
            A = P ?? 'O';
        }

        public void PrintPalub()
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor=color;
            Console.SetCursorPosition(X, Y);
            Console.WriteLine(A);
            Console.ForegroundColor = prev;
        }

    }
    [Serializable]
    public class Ship
    {
        public Dictionary<int, Palub> Sh = new Dictionary<int, Palub>();

        public Ship()
        {

        }

        public Ship(int n, int x, int y, int Up, int Right)
        {
            for (int i = 0; i < n; i++)
            {
                if (Up != 0)
                {
                    Sh.Add(i, (new Palub(x, y + i * Up)));
                }
                else if (Right != 0)
                {
                    Sh.Add(i, (new Palub(x + i * Right, y)));
                }
            }
        }

        public void SetShip(Rectangl G)
        {
            int Count = Sh.Count;
            for (int i = 0; i < Count; i++)
            {
                Sh[i].PrintPalub();
            }
            Console.SetCursorPosition(10, 1);
        }

        public void SetShipX(Rectangl G)
        {
            int Count = Sh.Count;
            for (int i = 0; i < Count; i++)
            {
                if(Sh[i].A == 'X')
                    Sh[i].PrintPalub();
            }
            Console.SetCursorPosition(10, 1);
        }

        //public void ChekShot(int y, int x)
        //{
        //    int Count = Sh.Count;
        //    int n = -1;
        //    bool shot = false;
        //    for (int i = 0; i < Count; i++)
        //    {
        //        if (Sh.ElementAt(i).Value.X == x && Sh.ElementAt(i).Value.Y == y)
        //        {
        //            shot = true;
        //            n = Sh.ElementAt(i).Key;
        //            i = Count;
        //        }
        //        else shot = false;
        //    }
        //    if (shot)
        //    {
        //        Sh.Remove(n);
        //        Sh.Add(n, (new Palub(x, y, ConsoleColor.Red, 'X')));
        //        Sh.ElementAt(n).Value.PrintPalub();

        //        Console.SetCursorPosition(10, 40);

        //        Console.WriteLine("Попал, в палубу : {0}", n + 1);
        //    }
        //    else Console.WriteLine("Промазал");

        //}

        public bool ChekShottwo(int y, int x, ref int n)
        {
            int Count = Sh.Count;
            bool shot = false;
            for (int i = 0; i < Count; i++)
            {
                if (Sh.ElementAt(i).Value.X == x && Sh.ElementAt(i).Value.Y == y)
                {
                    shot = true;
                    n = Sh.ElementAt(i).Key;
                    i = Count;
                }
                else shot = false;
            }
            if (shot)
            {
                return true;
            }
            return false;
        }

        public void ShipCrash(ref bool[,] Matrix, ref bool[,] MatrixShot, Rectangl C)
        {
            int Count = Sh.Count;
            int z=0;
            for (int i = 0; i < Count; i++)
            {
                if (Sh.ElementAt(i).Value.A == 'X')
                {
                    z++;
                }
            }
            if (z == Count)
            {
                for (int i = 0; i < Count; i++)
                {
                    Matrix[Sh.ElementAt(i).Value.X - C.A.X, Sh.ElementAt(i).Value.Y - C.A.Y] = true;
                    MatrixShot[Sh.ElementAt(i).Value.X - C.A.X, Sh.ElementAt(i).Value.Y - C.A.Y] = true;
                    Matrix[Sh.ElementAt(i).Value.X + 1 - C.A.X, Sh.ElementAt(i).Value.Y - 1 - C.A.Y] = true;
                    MatrixShot[Sh.ElementAt(i).Value.X + 1 - C.A.X, Sh.ElementAt(i).Value.Y - 1 - C.A.Y] = true;
                    Matrix[Sh.ElementAt(i).Value.X - 1 - C.A.X, Sh.ElementAt(i).Value.Y + 1 - C.A.Y] = true;
                    MatrixShot[Sh.ElementAt(i).Value.X - 1 - C.A.X, Sh.ElementAt(i).Value.Y + 1 - C.A.Y] = true;
                    Matrix[Sh.ElementAt(i).Value.X + 1 - C.A.X, Sh.ElementAt(i).Value.Y - C.A.Y] = true;
                    MatrixShot[Sh.ElementAt(i).Value.X + 1 - C.A.X, Sh.ElementAt(i).Value.Y - C.A.Y] = true;
                    Matrix[Sh.ElementAt(i).Value.X - 1 - C.A.X, Sh.ElementAt(i).Value.Y - C.A.Y] = true;
                    MatrixShot[Sh.ElementAt(i).Value.X - 1 - C.A.X, Sh.ElementAt(i).Value.Y - C.A.Y] = true;
                    Matrix[Sh.ElementAt(i).Value.X - C.A.X, Sh.ElementAt(i).Value.Y + 1 - C.A.Y] = true;
                    MatrixShot[Sh.ElementAt(i).Value.X - C.A.X, Sh.ElementAt(i).Value.Y + 1 - C.A.Y] = true;
                    Matrix[Sh.ElementAt(i).Value.X - C.A.X, Sh.ElementAt(i).Value.Y - 1 - C.A.Y] = true;
                    MatrixShot[Sh.ElementAt(i).Value.X - C.A.X, Sh.ElementAt(i).Value.Y - 1 - C.A.Y] = true;
                    Matrix[Sh.ElementAt(i).Value.X + 1 - C.A.X, Sh.ElementAt(i).Value.Y + 1 - C.A.Y] = true;
                    MatrixShot[Sh.ElementAt(i).Value.X + 1 - C.A.X, Sh.ElementAt(i).Value.Y + 1 - C.A.Y] = true;
                    Matrix[Sh.ElementAt(i).Value.X - 1 - C.A.X, Sh.ElementAt(i).Value.Y - 1 - C.A.Y] = true;
                    MatrixShot[Sh.ElementAt(i).Value.X - 1 - C.A.X, Sh.ElementAt(i).Value.Y - 1 - C.A.Y] = true;
                }
            }
        }
    }
}
