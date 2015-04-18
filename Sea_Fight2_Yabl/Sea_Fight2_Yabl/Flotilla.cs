using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Fight2_Yabl
{
    [Serializable]
    public class Flotilla
    {
        public ObservableCollection<Ship> Flot;

        public Flotilla()
        {
            Flot = new ObservableCollection<Ship>();
        }

        public void Arrangement(Rectangl R, int ind)
        {
            Flot.ElementAt(ind).SetShip(R);
        }

        public void ArrangementX(Rectangl R, int ind)
        {
            Flot.ElementAt(ind).SetShipX(R);
        }

        public void Compare(int y, int x, ref bool[,] Matrix, ref bool[,] MatrixShot, Rectangl C)
        {
            int n = -1;// номер палубы
            bool shot = false;
            bool crash = false;
            for (int i = 0; i < Flot.Count; i++)
            {
                if (Flot.ElementAt(i).ChekShottwo(y, x, ref n))
                {
                    Console.SetCursorPosition(0, 3);
                    Console.Write("\tПопал, в палубу : {0}", n + 1);
                    Console.WriteLine("\tКараблю из {0} палуб.", Flot.ElementAt(i).Sh.Count);
                    Flot.ElementAt(i).Sh.Remove(n);
                    if (crash) Console.WriteLine("\tКОРАБЛЮ УТОНУЛ.");
                    Console.WriteLine("\n\tНажми любую клавишу (Esc-выход)");
                    Flot.ElementAt(i).Sh.Add(n, (new Palub(x, y, ConsoleColor.Red, 'X')));
                    Flot.ElementAt(i).ShipCrash(ref Matrix, ref MatrixShot, C);
                    Flot.ElementAt(i).Sh.ElementAt(n).Value.PrintPalub();
                    shot = true;

                    Flot.CollectionChanged += Flot_CollectionChanged;  // ВОТ СДЕСЬ
                    break;
                }

            }
            if (!shot)
            {
                Console.SetCursorPosition(0, 3);
                Console.WriteLine("\tПромазал");
                Console.WriteLine("\n\tНажми любую клавишу (Esc-выход)");
            }
        }

        public void Flot_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
                Console.WriteLine("Добавил");
            if (e.Action == NotifyCollectionChangedAction.Reset)
                Console.WriteLine("Существенно изменил!!!");
            if (e.Action == NotifyCollectionChangedAction.Replace)
                Console.WriteLine("Один или несколько изменены!!!");
        }

        public void InsertShips(Rectangl R, ref bool[,] Matrix)
        {
            Random a = new Random();
            int x = 0 , y = 0, direction;
            bool directionGood = false;
            bool clear = false, bild = false;
            int[] massShip = new int[] { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
            int X1, Y1, X2, Y2;
            X1 = R.A.X + 1;
            X2 = R.B.X;
            Y1 = R.A.Y + 1;
            Y2 = R.C.Y;

            for (int i = 0; i < massShip.Length; i++)
            {
                bild = false;
                while (!bild)
                {
                    directionGood = false;
                    clear = false;

                    while (!clear)
                    {
                        x = a.Next(X1, X2);
                        y = a.Next(Y1, Y2);
                        int X = x - X1 + 1;
                        int Y = y - Y1 + 1;
                        if (CheckMatrix(Matrix, X, Y))
                        {
                            clear = true;
                        }
                        else
                        {
                            clear = false;
                        }
                    }

                    while (!directionGood)
                    {
                        direction = a.Next(0, 4);
                        int X = x - X1 + 1;
                        int Y = y - Y1 + 1;
                        switch (direction)
                        {
                            case 0:
                                if (y - massShip[i] >= Y1 && CheckMatrixFoShip(Matrix, X, Y, massShip[i], direction))
                                {
                                    Ship A = new Ship(massShip[i], x, y, -1, 0);
                                    Flot.Add(A);
                                    directionGood = true;
                                    FillingTheMatrix(ref Matrix, X, Y, massShip[i], direction);
                                    bild = true;
                                }
                                else
                                    directionGood = false;
                                break;
                            case 1:
                                if (x + massShip[i] <= X2 && CheckMatrixFoShip(Matrix, X, Y, massShip[i], direction))
                                {
                                    Ship A = new Ship(massShip[i], x, y, 0, 1);
                                    Flot.Add(A);
                                    directionGood = true;
                                    FillingTheMatrix(ref Matrix, X, Y, massShip[i], direction);
                                    bild = true;
                                }
                                else
                                    directionGood = false;
                                break;
                            case 2:
                                if (y + massShip[i] <= Y2 && CheckMatrixFoShip(Matrix, X, Y, massShip[i], direction))
                                {
                                    Ship A = new Ship(massShip[i], x, y, 1, 0);
                                    Flot.Add(A);
                                    directionGood = true;
                                    FillingTheMatrix(ref Matrix, X, Y, massShip[i], direction);
                                    bild = true;
                                }
                                else
                                    directionGood = false;
                                break;
                            case 3:
                                if (x - massShip[i] >= X1 && CheckMatrixFoShip(Matrix, X, Y, massShip[i], direction))
                                {
                                    Ship A = new Ship(massShip[i], x, y, 0, -1);
                                    Flot.Add(A);
                                    directionGood = true;
                                    FillingTheMatrix(ref Matrix, X, Y, massShip[i], direction);
                                    bild = true;
                                }
                                else
                                    directionGood = false;
                                break;
                        }
                    }
                }
                
            }
        }

        public void FillingTheMatrix(ref bool[,] Matrix, int x, int y , int TypeOfShip, int Direction)
        {
            int X = x, Y = y;
            switch (Direction)
            {
                case 0:
                    for (int i = 0; i < TypeOfShip; i++)
                    {
                        Matrix[X, Y-i] = true;
                    }
                    break;
                case 1:
                    for (int i = 0; i < TypeOfShip; i++)
                    {
                        Matrix[X +i, Y] = true;
                    }
                    break;
                case 2:
                    for (int i = 0; i < TypeOfShip; i++)
                    {
                        Matrix[X, Y + i] = true;
                    }
                    break;
                case 3:
                    for (int i = 0; i < TypeOfShip; i++)
                    {
                        Matrix[X - i, Y] = true;
                    }
                    break;
            }
        }

        public bool CheckMatrix(bool[,] Matrix, int x, int y)
        {
            int X = x, Y = y;
            if (!Matrix[X, Y] && !Matrix[X, Y - 1] && !Matrix[X - 1, Y - 1] && !Matrix[X - 1, Y] && !Matrix[X - 1, Y] && !Matrix[X - 1, Y + 1] && !Matrix[X, Y + 1] && !Matrix[X + 1, Y + 1] && !Matrix[X + 1, Y] && !Matrix[X + 1, Y - 1])
                return true;
            else
                return false;
        }

        public bool CheckMatrixFoShip(bool[,] Matrix, int x, int y, int TypeOfShip, int Direction)
        {
            int X = x, Y = y;
            int l=0;
            switch (Direction)
            {
                case 0:
                    for (int i = 0; i < TypeOfShip; i++)
                    {
                        if (CheckMatrix(Matrix, X, Y - i))
                            l++;
                    }
                    break;
                case 1:
                    for (int i = 0; i < TypeOfShip; i++)
                    {
                        if (CheckMatrix(Matrix, X +i, Y))
                            l++;
                    }
                    break;
                case 2:
                    for (int i = 0; i < TypeOfShip; i++)
                    {
                        if (CheckMatrix(Matrix, X, Y + i))
                            l++;
                    }
                    break;
                case 3:
                    for (int i = 0; i < TypeOfShip; i++)
                    {
                        if (CheckMatrix(Matrix, X - i, Y))
                            l++;
                    }
                    break;
            }
            if (l == TypeOfShip)
                return true;
            else
                return false; 
        }



    }
}