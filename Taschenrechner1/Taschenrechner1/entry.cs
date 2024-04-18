using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taschenrechner1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            gamemenu gm = new gamemenu();
            gm.DisplayMenu();

            while (true)
            {
                Console.WriteLine("Willst du nochmal rechen? [y]: ja ; [n]: nein");
                char input = Convert.ToChar(Console.ReadLine());

                // || oder, && ist und
                if (input != 'y' && input != 'n')
                {
                    Console.WriteLine("Falsche Eingabe!");
                    Console.ReadKey();
                    return; // exit

                }
                else
                {
                    switch (input)
                    {
                        case 'y':
                            Console.Clear();
                            gm.DisplayMenu();
                            break;
                        case 'n':
                            return;
                        default:
                            return;
                    }
                }
            }
            
        }
    }
}
