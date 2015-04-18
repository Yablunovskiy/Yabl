using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Sea_Fight2_Yabl
{
    class Program
    {
        static void Main(string[] args)
        {
            int X1 = 20, X2 = 50, Y = 15, L = 11;
            
            Rectangl G = new Rectangl(X1, Y, L);
            Rectangl C = new Rectangl(X2, Y, L);
            Flotilla A = new Flotilla();
            Flotilla B = new Flotilla();
            Random rand = new Random();
            bool[,] MatrixPlaer = new bool[12, 12];
            bool[,] MatrixComp = new bool[12, 12];
            bool[,] MatrixCompShot = new bool[12, 12];
            bool[,] MatrixPlaerShot = new bool[12, 12];

            bool K1 = true;

            while (K1)
            {
                Menu(ref A, ref B, ref MatrixPlaer, ref MatrixComp, ref MatrixCompShot, ref MatrixPlaerShot, G, C, ref K1);
            }
        }

        static void Menu(ref Flotilla A, ref Flotilla B, ref bool[,] MatrixPlaer, ref bool[,] MatrixComp, ref bool[,] MatrixCompShot, ref bool[,] MatrixPlaerShot, Rectangl G, Rectangl C, ref bool K1)
        {

            bool next = true;
            int I = 0;
            string[] m1 = new string[] { " New Battle ", " Load the last Saved" };
            Action<string, ConsoleColor, ConsoleColor> action = DispayMessage;
            bool See = true;

            Console.Clear();
            Console.WriteLine("\n\n\n");
            Console.WriteLine("\t\t\t\t     This is your SEA BATTLE.");
            for (int i = 0; i < m1.Length; i++)
            {
                if (i == I)
                    action(m1[i], ConsoleColor.Yellow, ConsoleColor.Blue);
                else
                {
                    action(m1[i], ConsoleColor.DarkBlue, ConsoleColor.DarkGreen);
                }
            }
            Console.WriteLine("\t\t\t\t\t   Выход Esc.");

            while (next)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    --I;
                    if (I < 0) I = m1.Length - 1;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    ++I;
                    if (I > m1.Length - 1) I = 0;
                }

                Console.Clear();
                Console.WriteLine("\n\n\n");
                Console.WriteLine("\t\t\t\t     This is your SEA BATTLE.");
                for (int i = 0; i < m1.Length; i++)
                {
                    if (i == I)
                        action(m1[i], ConsoleColor.Yellow, ConsoleColor.Blue);
                    else
                        action(m1[i], ConsoleColor.DarkBlue, ConsoleColor.DarkGreen);
                }
                Console.WriteLine("\t\t\t\t\t   Выход Esc.");

                if (key.Key == ConsoleKey.Enter)
                {
                    switch (I)
                    {
                        case 0:
                            {
                                SetMatpix(ref MatrixPlaer);
                                SetMatpix(ref MatrixComp);
                                SetMatpix(ref MatrixCompShot);
                                SetMatpix(ref MatrixPlaerShot);
                                A.InsertShips(G, ref MatrixPlaer);
                                
                                See = Menu2(I, ref B, C, ref MatrixComp);
                                Console.Clear();
                                B.InsertShips(C, ref MatrixComp);
                                Play(ref A, ref B, ref MatrixPlaer, ref MatrixComp, ref MatrixCompShot, ref MatrixPlaerShot, G, C, ref K1, See);
                                next = false;
                                K1 = false;
                            }
                            break;
                        case 1:
                            Read(ref A, ref B, ref MatrixPlaer, ref MatrixComp, ref MatrixCompShot, ref MatrixPlaerShot);
                            See = Menu2(I, ref B, C, ref MatrixComp);
                            Console.Clear();
                            Play(ref A, ref B, ref MatrixPlaer, ref MatrixComp, ref MatrixCompShot, ref MatrixPlaerShot, G, C, ref K1, See);
                            next = false;
                            K1 = false;
                            break;
                    }                    
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    next = false;
                    K1 = false;
                }
            }
            Console.WriteLine("\n\n\n\t\t\t\t\t   Досвидания.");
        }

        static void Play(ref Flotilla A, ref Flotilla B, ref bool[,] MatrixPlaer, ref bool[,] MatrixComp, ref bool[,] MatrixCompShot, ref bool[,] MatrixPlaerShot, Rectangl G, Rectangl C, ref bool K1, bool See)
        {
            Random rand = new Random();
            ABC a = new ABC();
            bool next = true;
            int n = 0, ch = 1;
            char c;
            int b = 0;
            while (next)
            {

                Console.Clear();
                PrintMatrix(MatrixPlaerShot, G);
                G.Border("Player");
                PrintMatrix(MatrixCompShot, C);
                C.Border("  Comp");
                PrintFlotillia(A, B, See, G, C);
                if (ch % 2 == 0)
                {
                    b = rand.Next(1, 11);
                    n = rand.Next(1, 11);
                    Console.WriteLine("Комп Выстрел {0}-{1}", b, n);
                    Shot(b, n, G, ref A, ref MatrixPlaer, ref MatrixPlaerShot);
                    PrintFlotillia(A, B, See, G, C);
                    ch++;
                }
                else
                {
                    try
                    {
                        while (n < 1 || n > 10)
                        {
                            Console.Write("Введите букву и цифру (делаем выстрел)->");
                            c = Console.ReadKey().KeyChar;
                            b = a.GetKod(Char.ToUpper(c));
                            n = Convert.ToInt32(Console.ReadLine());
                            if (n < 1 || n > 10)
                            {
                                Console.WriteLine("Цифра должна быть от 1 до 10");
                                Console.WriteLine("Попробуй снова.");
                            }
                            ch++;
                            Shot(b, n, C, ref B, ref MatrixComp, ref MatrixCompShot);
                            PrintFlotillia(A, B, See, G, C);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Ошибка ввода
                        Console.WriteLine(ex.Message + " Попробуйте снова!!!\n");
                    }
                }
                //ход компа
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    next = false;
                    Write(A, B, MatrixPlaer, MatrixComp, MatrixCompShot, MatrixPlaerShot);
                }
                Console.Clear();
                n = b = 0;
            }
        }

        static void PrintFlotillia(Flotilla A, Flotilla B, bool See, Rectangl G, Rectangl C)
        {
            for (int i = 0; i < A.Flot.Count; i++)
            {
                A.Arrangement(G, i);
            }
            if (See)
            {
                for (int i = 0; i < B.Flot.Count; i++)
                {
                    B.Arrangement(C, i);
                }
            }
            else
            {
                Console.WriteLine("Play");
                for (int i = 0; i < B.Flot.Count; i++)
                {
                    B.ArrangementX(C, i);
                }
            }
        }

        static void Write( Flotilla A, Flotilla B, bool[,] MatrixPlaer, bool[,] MatrixComp, bool[,] MatrixCompShot, bool[,] MatrixPlaerShot)
        {
            BinaryFormatter d = new BinaryFormatter();
            using (Stream stream = new FileStream("AvtoSave/userA.bin", FileMode.Create, FileAccess.Write))
            {
                d.Serialize(stream, A);
            }
            using (Stream stream = new FileStream("AvtoSave/userB.bin", FileMode.Create, FileAccess.Write))
            {
                d.Serialize(stream, B);
            }
            using (Stream stream = new FileStream("AvtoSave/userMC.bin", FileMode.Create, FileAccess.Write))
            {
                d.Serialize(stream, MatrixComp);
            }
            using (Stream stream = new FileStream("AvtoSave/userMCS.bin", FileMode.Create, FileAccess.Write))
            {
                d.Serialize(stream, MatrixCompShot);
            }
            using (Stream stream = new FileStream("AvtoSave/userMP.bin", FileMode.Create, FileAccess.Write))
            {
                d.Serialize(stream, MatrixPlaer);
            }
            using (Stream stream = new FileStream("AvtoSave/userMPS.bin", FileMode.Create, FileAccess.Write))
            {
                d.Serialize(stream, MatrixPlaerShot);
            }
        }

        static void Read(ref Flotilla A, ref Flotilla B, ref bool[,] MatrixPlaer, ref bool[,] MatrixComp, ref bool[,] MatrixCompShot, ref bool[,] MatrixPlaerShot)
        {
            BinaryFormatter z = new BinaryFormatter();
            using (Stream stream = new FileStream("AvtoSave/userA.bin", FileMode.Open, FileAccess.Read))
            {
                A = (Flotilla)z.Deserialize(stream);
            }
            using (Stream stream = new FileStream("AvtoSave/userB.bin", FileMode.Open, FileAccess.Read))
            {
                B = (Flotilla)z.Deserialize(stream);
            }

            using (Stream stream = new FileStream("AvtoSave/userMC.bin", FileMode.Open, FileAccess.Read))
            {
                MatrixComp = (bool[,])z.Deserialize(stream);
            }
            using (Stream stream = new FileStream("AvtoSave/userMCS.bin", FileMode.Open, FileAccess.Read))
            {
                MatrixCompShot = (bool[,])z.Deserialize(stream);
            }
            using (Stream stream = new FileStream("AvtoSave/userMP.bin", FileMode.Open, FileAccess.Read))
            {
                MatrixPlaer = (bool[,])z.Deserialize(stream);
            }
            using (Stream stream = new FileStream("AvtoSave/userMPS.bin", FileMode.Open, FileAccess.Read))
            {
                MatrixPlaerShot = (bool[,])z.Deserialize(stream);
            }
        }

        static bool Menu2(int I, ref Flotilla B, Rectangl C, ref bool[,] MatrixComp)
        {
            bool next=true;
            
            int K = 0;
            string[] m2 = new string[] { " New Battle ", " Last Saved " };
            string[] m3 = new string[] { " Видеть поле противника. ", " Не видеть поле противника. " };
            Action<string, ConsoleColor, ConsoleColor> action = DispayMessage1;
            Console.Clear();
            Console.WriteLine("\n\n\n");
            Console.WriteLine("\t\t\t\t     " + m2[I]);
            for (int i = 0; i < m3.Length; i++)
            {
                if (i == K)
                    action(m3[i], ConsoleColor.Yellow, ConsoleColor.Blue);
                else
                {
                    action(m3[i], ConsoleColor.DarkBlue, ConsoleColor.DarkGreen);
                }
            }
            while (next)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    --K;
                    if (K < 0) K = m3.Length - 1;
                }
                if (key.Key == ConsoleKey.DownArrow)
                {
                    ++K;
                    if (K > m3.Length - 1) K = 0;
                }

                Console.Clear();
                Console.WriteLine("\n\n\n");
                Console.WriteLine("\t\t\t\t     " + m2[I]);
                for (int i = 0; i < m3.Length; i++)
                {
                    if (i == K)
                        action(m3[i], ConsoleColor.Yellow, ConsoleColor.Blue);
                    else
                    {
                        action(m3[i], ConsoleColor.DarkBlue, ConsoleColor.DarkGreen);
                    }
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    if (K == 0)
                    {
                        //B.InsertShips(C, ref MatrixComp);
                        return true;
                    }
                    else return false;
                }
            }    
            return true;
        }

        static public void Shot(int y, int x, Rectangl C, ref Flotilla A, ref bool[,] Matrix, ref bool[,] MatrixShot) 
        {
            A.Compare(y + C.A.Y, x + C.A.X, ref Matrix, ref MatrixShot, C);
            Matrix[x , y]=true;
            MatrixShot[x, y] = true;
        }

        static public void PrintMatrix(bool[,] Matrix, Rectangl C)
        {
            for (int i = 0; i < 12; i++)
            {
                Console.SetCursorPosition(C.A.X, C.A.Y+i);
                for (int j = 0; j < 12; j++)
                {
                    if (Matrix[j, i])
                        Console.Write('+');
                    else
                        Console.Write(' ');
                }
                Console.WriteLine();
            }
        }

        static void SetMatpix(ref bool[,] M)
        {
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    M[i, j] = false;
                }
            }
        }

        static void DispayMessage(string msg, ConsoleColor color, ConsoleColor Bcolor)
        {
            ConsoleColor prev = Console.ForegroundColor;
            ConsoleColor Bprev = Console.BackgroundColor;
            Console.WriteLine();
            Console.Write("\t\t\t\t\t");
            Console.ForegroundColor = color;
            Console.BackgroundColor = Bcolor;
            Console.WriteLine(msg);
            Console.ForegroundColor = prev;
            Console.BackgroundColor = Bprev;
            Console.WriteLine();
        }

        static void DispayMessage1(string msg, ConsoleColor color, ConsoleColor Bcolor)
        {
            ConsoleColor prev = Console.ForegroundColor;
            ConsoleColor Bprev = Console.BackgroundColor;
            Console.WriteLine();
            Console.Write("\t\t\t\t");
            Console.ForegroundColor = color;
            Console.BackgroundColor = Bcolor;
            Console.WriteLine(msg);
            Console.ForegroundColor = prev;
            Console.BackgroundColor = Bprev;
            Console.WriteLine();
        }
    }
}
