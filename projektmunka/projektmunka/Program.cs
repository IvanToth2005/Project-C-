using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ConsoleApp1
{
    internal class Program
    {
        class Datas
        {
            public string name;
            public double price;
            public int amount;
            public string parameters;
        }
        static void Main(string[] args)
        {
            List<Datas> list = new List<Datas>();
            StreamReader sr = new StreamReader("datas.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] split = line.Split(';');
                Datas temp = new Datas();
                temp.name = split[0];
                temp.price = double.Parse(split[1]);
                temp.amount = int.Parse(split[2]);
                temp.parameters = split[3];
                list.Add(temp);
            }
            sr.Close();

            string[] menu = { "Kiíratás", "Hozzáadás", "Eltávolítás", "Módosítás", "Leárazás" };
            int mainmenu = 0;
            ConsoleKeyInfo keymenu;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Válasszon az alábbi lehetőségek közül:\n");
                for (int i = 0; i < menu.Length; i++)
                {
                    if (i == mainmenu)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("\t" + (i + 1) + ") " + menu[i]);
                }
                keymenu = Console.ReadKey();

                switch (keymenu.Key)
                {
                    case ConsoleKey.UpArrow: if (mainmenu > 0) mainmenu--; break;
                    case ConsoleKey.DownArrow: if (mainmenu < menu.Length - 1) mainmenu++; break;
                }
                //kiíratás
                if (keymenu.Key == ConsoleKey.Spacebar && mainmenu == 0)
                {
                    ConsoleKeyInfo keyfirst;
                    Console.WriteLine("Sima vagy részleges kiiratást akar? (s/r)");
                    string melyik = Console.ReadLine();
                    while(melyik != "s" || melyik != "r" && melyik.Length !=1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Rossz input!");
                        melyik = Console.ReadLine();
                    }
                    //sima kiiratás
                    if(melyik == "s")
                    {
                        do
                        {
                            Console.WriteLine("Neve,  Ára,  Raktáron,  Paraméterei");
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (list[i].amount == 0)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine(list[i].name + "; " + list[i].price + "; " + list[i].amount + "; " + list[i].parameters);
                                }
                                else if (list[i].amount >= 1 && list[i].amount < 10)
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine(list[i].name + "; " + list[i].price + "; " + list[i].amount + "; " + list[i].parameters);
                                }
                                else if (list[i].amount >= 10)
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine(list[i].name + "; " + list[i].price + "; " + list[i].amount + "; " + list[i].parameters);

                                }

                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("---------------------------------------------------------------------------------------");
                            Console.WriteLine("Fogytán lévő tekmékek: ");
                            Console.WriteLine("---------------------------------------------------------------------------------------");
                            for (int i = 0; i < list.Count; i++)
                            {
                                if (list[i].amount <= 10)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("---------------------------------------------------------------------------------------");
                                    Console.WriteLine(list[i].name);
                                }
                            }
                            keyfirst = Console.ReadKey();
                        } while (keyfirst.Key != ConsoleKey.Backspace);
                    }
                    //részleges kiiratás
                    else if(melyik == "r")
                    {
                        //TO DO
                    }
                    
                }
                //hozzáadas
                if (keymenu.Key == ConsoleKey.Spacebar && mainmenu == 1)
                {
                    ConsoleKeyInfo keysecond;
                    do
                    {
                        Console.WriteLine("Hány terméket akar hozzáadni?");
                        int hany = Convert.ToInt32(Console.ReadLine());
                        for(int i = 0; i <hany; i++)
                        {
                            Console.WriteLine("Hozzáadás");
                            StreamWriter srw = File.AppendText("datas.txt");
                            Console.WriteLine("Írd be a termék nevét: ");
                            string nameadd = Console.ReadLine();
                            srw.Write(nameadd);
                            srw.Write(";");

                            //szám lekezelés1
                            double priceadd;
                            Console.WriteLine("Írd be a termék árát: ");
                            string priceadd_ = Console.ReadLine();
                            while (!double.TryParse(priceadd_, out priceadd))
                            {
                                Console.WriteLine("Számot kérek!");
                                priceadd_ = Console.ReadLine();
                            }

                            srw.Write(priceadd);
                            srw.Write(";");


                            Console.WriteLine("Írd be a raktáron lévő termékek számát: ");
                            //szám lekezelés2
                            int amountadd;
                            string amountadd_ = Console.ReadLine();
                            while (!int.TryParse(amountadd_, out amountadd))
                            {
                                Console.WriteLine("Számot kérek!");
                                amountadd_ = Console.ReadLine();
                            }

                            srw.Write(amountadd);
                            srw.Write(";");

                            Console.WriteLine("Írd be a termék paramétereit: ");
                            string parameteradd = Console.ReadLine();
                            srw.Write(parameteradd);
                            srw.WriteLine(";");
                            srw.Close();
                        }
                        keysecond = Console.ReadKey();
                    } while (keysecond.Key != ConsoleKey.Backspace);
                }
                //törlés
                if (keymenu.Key == ConsoleKey.Spacebar && mainmenu == 2)
                {
                    ConsoleKeyInfo keythird;
                    Console.WriteLine("Törlés");
                    do
                    {


                        //TO DO törlés

                        keythird = Console.ReadKey();
                    } while (keythird.Key != ConsoleKey.Backspace);
                }
                //módosítás
                if (keymenu.Key == ConsoleKey.Spacebar && mainmenu == 3)
                {
                    ConsoleKeyInfo keyfourth;
                    Console.WriteLine("Módosítás");
                    do
                    {
                        Console.WriteLine("Nyomja le az entert ismeri a módosítani kívánt termék(ek) id-jét!");
                        Thread.Sleep(5000);



                        keyfourth = Console.ReadKey();
                    } while (keyfourth.Key != ConsoleKey.Backspace);
                }
                if (keymenu.Key == ConsoleKey.Spacebar && mainmenu == 4)
                {
                    ConsoleKeyInfo keyfifth;
                    Console.WriteLine("Leárazás");
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Random rnd = new Random();
                        for (int i = 0; i < 5; i++)
                        {
                            int temporary = rnd.Next(list.Count);
                            Console.WriteLine(list[temporary].name + ",  " + list[temporary].price * 0.9);
                        }

                        keyfifth = Console.ReadKey();
                    } while (keyfifth.Key != ConsoleKey.Backspace);
                }
            } while (keymenu.Key != ConsoleKey.Enter);
        }
    }
}

