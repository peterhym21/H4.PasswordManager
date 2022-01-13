using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Service.Services
{
    public class Menu
    {
        int ind = 0;
        ConsoleKeyInfo KPLast;
        List<string> items;
        string choice = "";
        // int choice = -1; til hvis du skal have tal ind i stedet for strings
        ConsoleColor menuColor;
        int x_akse;
        int y_akse;

        public Menu(List<string> items, ConsoleColor menuColor = ConsoleColor.Green)
        {
            Console.CursorVisible = false;
            this.items = items;
            this.menuColor = menuColor;
            x_akse = Console.WindowWidth;
            y_akse = Console.WindowHeight;
        }

        public void Select()
        {
            do
            {
                #region Select items
                foreach (var item in items)
                {
                    int maxSize = FindLongestItemLength(items);
                    Console.CursorTop = Console.WindowHeight / 2 - items.Count + items.IndexOf(item) + 1;

                    #region Select item

                    #region Padding
                    Console.CursorLeft = Console.WindowWidth / 2 - maxSize / 2 - 2;
                    Console.Write(new String(' ', maxSize / 2 - item.Length / 2));
                    #endregion

                    #region Select item text
                    if (items.IndexOf(item) == ind)
                    {
                        Console.Write($"[ ");
                        Console.ForegroundColor = menuColor;
                        Console.Write(item);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ]");
                    }
                    else
                    {
                        Console.Write($"  {item}  ");
                    }
                    #endregion

                    #region Padding
                    Console.Write(new String(' ', maxSize / 2 + item.Length / 2));
                    Console.WriteLine();
                    #endregion

                    #endregion
                }
                #endregion

                #region Menu controls
                KPLast = Console.ReadKey();
                switch (KPLast.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (ind > 0)
                            ind--;
                        else
                            ind = items.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (ind < items.Count - 1)
                            ind++;
                        else
                            ind = 0;
                        break;
                    default:
                        break;
                }
                // for resising af vinduet
                if (x_akse != Console.WindowWidth || y_akse != Console.WindowHeight)
                {
                    x_akse = Console.WindowWidth;
                    y_akse = Console.WindowHeight;
                    Console.Clear();
                    Console.CursorVisible = false;
                }
                #endregion

            } while (KPLast.Key != ConsoleKey.Enter);   // kører indtil den er færdig
            choice = items[ind];   // gemmer
            // choice = index; 
            // Console.CursorVisible = true;
        }

        //public int GetResult() {return choice;}
        public string Result()
        {
            return choice;
        }

        #region Internal methods

        static int FindLongestItemLength(List<string> items)
        {
            int maxLength = 0;
            foreach (var item in items)
            {
                if (item.Length > maxLength)
                    maxLength = item.Length;
            }
            return maxLength;
        }

        #endregion

    }
}
