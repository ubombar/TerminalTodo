using System;
using System.Collections;
using System.Collections.Generic;

namespace TTODO
{
    public class TerminalTodo
    {
        public static int TAB_SPACE = 3;
        public List<Sheet> Sheets;
        public Sheet Selected;

        public TerminalTodo()
        {
            Sheets = new List<Sheet>();
            Selected = null;
        }

        public int CreateSheet(bool select=true)
        {
            Sheet tmp = new Sheet();
            Sheets.Add(tmp);

            if (select) Selected = tmp;
            return Sheets.Count - 1; // return the index
        }

        public void DisplaySheet()
        {
            if (Selected == null) Console.WriteLine("Oh no you have not selected a sheet!");
            else Selected.DisplayItems(); // Better display this.
        }

        public Sheet.ListItem GetListItem(int order)
        {
            if (Selected == null) 
                return null;
            
            return Selected.GetListItem(order);
        }

        public static void ResetColor()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public struct ListItemAttribute
    {
        public bool IsFinished;
        public bool IsSelected;

        public ListItemAttribute(bool isfinished, bool isselected)
        {
            IsFinished = isfinished;
            IsSelected = isselected;
        }
    }

    public class Sheet
    {
        public class ListItem
        {
            public string Value {get; set;}

            public ListItemAttribute Attributes;
            public List<ListItem> Childs;

            public ListItem(string value, ListItemAttribute attributes)
            {
                Attributes = attributes;
                Value = value;
                Childs = new List<ListItem>();
            }

            public void Display(int depth=1)
            {
                TerminalTodo.ResetColor();
                // Decide on color based on attributes

                if (Attributes.IsSelected)
                    if (Attributes.IsFinished) 
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                    }  
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                    }
                else
                    if (Attributes.IsFinished) 
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }  
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    
                    


                for (int i = 0; i < depth * TerminalTodo.TAB_SPACE; i++)
                    Console.Write(' ');
                

                Console.Write(Value);

                for (int i = 1; i < Console.BufferWidth - depth * TerminalTodo.TAB_SPACE - Value.Length; i++)
                    Console.Write(' ');

                Console.WriteLine();

                foreach (ListItem item in Childs)
                    item.Display(depth + 1);
            }

            public ListItem GetSelf(int order, ref int current)
            {
                if (order == current)
                    return this;

                foreach (ListItem child in Childs)
                {
                    current += 1;

                    var response = child.GetSelf(order, ref current);

                    if (response != null) return response;
                }

                return null;
            }
        }

        public List<ListItem> Items;
        public string SheetName;

        public Sheet(string sheetname=null)
        {
            SheetName = sheetname;
            Items = new List<ListItem>();
        }

        public void DisplayItems()
        {
            if (SheetName != null) Console.WriteLine(SheetName);

            foreach (ListItem item in Items)
                item.Display();

            TerminalTodo.ResetColor();
        }

        public ListItem GetListItem(int order)
        {
            int current = 0;

            foreach (ListItem item in Items)
            {
                var response = item.GetSelf(order, ref current);

                if (response != null) return response;

                current += 1;
            }

            return null;
        }
    }   
}