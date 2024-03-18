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
        static List<Datas> list = new List<Datas>();

        class Datas
        {
            public string name;
            public double price;
            public int amount;
            public string parameters;

            public void s_kiirat(bool kelle)
            {
                Console.WriteLine("Neve,  Ára,  Raktáron,  Paraméterei");
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].amount == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(i+1+"." + list[i].name + "; " + list[i].price + "; " + list[i].amount + "; " + list[i].parameters);
                    }
                    else if (list[i].amount >= 1 && list[i].amount < 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(i+1+"." + list[i].name + "; " + list[i].price + "; " + list[i].amount + "; " + list[i].parameters);
                    }
                    else if (list[i].amount >= 10)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(i+1+"." + list[i].name + "; " + list[i].price + "; " + list[i].amount + "; " + list[i].parameters);

                    }

                }
                Console.ForegroundColor = ConsoleColor.White;
                if(kelle)
                {
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
                }
                
                Console.ReadKey();
            }

            public void r_kiirat()
            {
                Console.Clear();
                ConsoleKeyInfo figyel;
                int szamol = 0;
                bool valtozas = true;
                do
                {
                    if(valtozas)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (szamol < 100 && list[szamol].amount == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(szamol+1+"." + list[szamol].name + "; " + list[szamol].price + "; " + list[szamol].amount + "; " + list[szamol].parameters);
                            }
                            else if (szamol < 100 && list[szamol].amount >= 1 && list[szamol].amount < 10)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(szamol+1+"." + list[szamol].name + "; " + list[szamol].price + "; " + list[szamol].amount + "; " + list[szamol].parameters);
                            }
                            else if (szamol < 100 && list[szamol].amount >= 10)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(szamol+1+"." + list[szamol].name + "; " + list[szamol].price + "; " + list[szamol].amount + "; " + list[szamol].parameters);

                            }
                            if(szamol < 100)
                            {
                                szamol++;
                            }
                        }
                    }
                    valtozas = false;
                    szamol--;
                    figyel = Console.ReadKey();
                    
                    switch (figyel.Key)
                    {
                        case (ConsoleKey.UpArrow): 
                            if(szamol >= 19)
                            {
                                Console.Clear();
                                szamol -= 19;
                                valtozas = true;
                            }
                            break;
                        case (ConsoleKey.DownArrow):
                            if (szamol <= 99)
                            {
                                Console.Clear();
                                szamol += 1;
                                valtozas = true;
                            }
                            break;
                    }
                }
                while (figyel.Key != ConsoleKey.Backspace);
                
            }

            public void hozzaad()
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
            }

            public void torles()
            {
                Datas temp2 = new Datas();
                Console.WriteLine("Az entert lenyomva válassza ki a törlni kívánt elemek id-jét!");
                Thread.Sleep(5000);
                Console.Clear();
                bool kelle = false;
                temp2.s_kiirat(kelle);
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Kérem a id(k)-at!(ha több spaceval elválasztva)");
                string id_ = Console.ReadLine();
                int id;
                string[] split = id_.Split(' ');
                int k = 0;
                bool joe = false;
                do
                {
                    while (!int.TryParse(split[k], out id) || split.Length != split.Distinct().Count() || Convert.ToInt32(split[k]) > list.Count || Convert.ToInt32(split[k]) < 1)
                    {
                        //lekezelések
                        //szam-e?
                        if (!int.TryParse(split[k], out id))
                        {
                            Console.WriteLine($"A {k + 1}. érték nem szám!");
                        }
                        //ha túl nagy/kicsi a szám
                        else if (Convert.ToInt32(split[k]) > list.Count || Convert.ToInt32(split[k]) < 1)
                        {
                            Console.WriteLine("Nincs ilyen id-jű termék!");
                        }
                        else
                        {
                            Console.WriteLine("Ne legyen ismétlődő szám!");
                        }
                        id_ = Console.ReadLine();
                        split = id_.Split(' ');
                        k = 0;
                    }
                    k++;
                    if (k == split.Length)
                    {
                        joe = true;
                    }
                }
                while (!joe);
                Array.Sort(split);
                for (int i = split.Length; i > 0 ; i--)
                {
                    list.RemoveAt(Convert.ToInt32(split[i-1])-1);
                }
                Console.Clear();
            }

            public void modositas()
            {
                Datas temp2 = new Datas();
                Console.WriteLine("Az entert lenyomva válassza ki a módosítani kívánt elemek id-jét!");
                Thread.Sleep(5000);
                Console.Clear();
                bool kelle = false;
                temp2.s_kiirat(kelle);
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Kérem a id(k)-at! (ha több spaceval elválasztva)");
                string id_ = Console.ReadLine();
                int id;
                string[] split = id_.Split(' ');
                int k = 0;
                bool joe = false;
                do
                {
                    while (!int.TryParse(split[k], out id) || split.Length != split.Distinct().Count() || Convert.ToInt32(split[k]) > list.Count || Convert.ToInt32(split[k]) < 1)
                    {
                        //lekezelések
                        //szam-e?
                        if(!int.TryParse(split[k], out id))
                        {
                            Console.WriteLine($"A {k + 1}. érték nem szám!");
                        }
                        //ha túl nagy/kicsi a szám
                        else if (Convert.ToInt32(split[k]) > list.Count || Convert.ToInt32(split[k]) < 1)
                        {
                            Console.WriteLine("Nincs ilyen id-jű termék!");
                        }
                        else
                        {
                            Console.WriteLine("Ne legyen ismétlődő szám!");
                        }
                        id_ = Console.ReadLine();
                        split = id_.Split(' ');
                        k = 0;
                    }
                    k++;
                    if (k == split.Length)
                    {
                        joe = true;
                    }
                }
                while (!joe);
                for(int i = 0; i < split.Length; i++)
                {
                    int x = Convert.ToInt32(split[i])-1;
                    Console.WriteLine($"{i+1}. elem:");
                    Console.WriteLine(i + 1 + "." + list[x].name + "; " + list[x].price + "; " + list[x].amount + "; " + list[x].parameters);
                    Console.WriteLine("Változtatások:");
                    //név
                    Console.WriteLine("Neve:");
                    list[x].name = Console.ReadLine();
                    double ar;
                    Console.Clear();
                    //ár
                    Console.WriteLine("Ára:");
                    string ar_ = Console.ReadLine();
                    while (!double.TryParse(ar_, out ar))
                    {
                        Console.WriteLine("Számot kérek!");
                        ar_ = Console.ReadLine();
                    }
                    list[x].price = ar;
                    Console.Clear();
                    //mennyiség
                    Console.WriteLine("Mennyisége:");
                    ar_ = Console.ReadLine();
                    while (!double.TryParse(ar_, out ar))
                    {
                        Console.WriteLine("Számot kérek!");
                        ar_ = Console.ReadLine();
                    }
                    list[x].amount = Convert.ToInt32(ar);
                    Console.Clear();
                    //paraméterek
                    Console.WriteLine("Paraméterei:");
                    list[x].parameters = Console.ReadLine();
                    Console.Clear();
                }
            }

            public void learazas()
            {
                Console.WriteLine("Leárazás");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Random rnd = new Random();
                for (int i = 0; i < 5; i++)
                {
                    int temporary = rnd.Next(list.Count);
                    Console.WriteLine(list[temporary].name + ",  " + list[temporary].price * 0.9);
                }
                Console.ReadKey();
            }
        }
        static void Main(string[] args)
        {
            //feltöltés
            Datas temp2 = new Datas();
            StreamReader sr = new StreamReader("datas.txt");
            while (!sr.EndOfStream)
            {
                Datas temp = new Datas();
                string line = sr.ReadLine();
                string[] split = line.Split(';');
                temp.name = split[0];
                temp.price = double.Parse(split[1]);
                temp.amount = int.Parse(split[2]);
                temp.parameters = split[3];
                list.Add(temp);
            }
            sr.Close();
            //menü
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
                    Console.WriteLine("Sima vagy részleges kiiratást akar? (s/r)");
                    string melyik = Console.ReadLine();
                    while(melyik != "s" && melyik != "r" || melyik.Length !=1)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Rossz input!");
                        melyik = Console.ReadLine();
                    }
                    //sima kiiratás
                    if(melyik == "s")
                    {
                        bool kelle = true;
                        temp2.s_kiirat(kelle);
                    }
                    //részleges kiiratás
                    else if(melyik == "r")
                    {
                        temp2.r_kiirat();
                    }
                    
                }
                //hozzáadas
                if (keymenu.Key == ConsoleKey.Spacebar && mainmenu == 1)
                {
                    temp2.hozzaad();
                }
                //törlés
                if (keymenu.Key == ConsoleKey.Spacebar && mainmenu == 2)
                {
                    temp2.torles();
                }
                //módosítás
                if (keymenu.Key == ConsoleKey.Spacebar && mainmenu == 3)
                {
                    temp2.modositas();
                }
                //leárazás
                if (keymenu.Key == ConsoleKey.Spacebar && mainmenu == 4)
                {
                    temp2.learazas();
                }
            } while (keymenu.Key != ConsoleKey.Enter);
        }
    }
}

