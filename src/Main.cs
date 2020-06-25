using System;
using TTODO;

public class MainClass
{
    public static void Main(string[] args)
    {
        TerminalTodo todo = new TerminalTodo();

        Console.WriteLine("Welcome to TerminalTodo!");

        todo.CreateSheet();

        todo.Selected.Items.Add(new Sheet.ListItem("Morning Reading",   new ListItemAttribute(false, false)));
        todo.Selected.Items.Add(new Sheet.ListItem("Breakfast",         new ListItemAttribute(true, false)));
        todo.Selected.Items.Add(new Sheet.ListItem("Flutter Tutorial",  new ListItemAttribute(false, true)));
        todo.Selected.Items.Add(new Sheet.ListItem("Flutter Tutorial",  new ListItemAttribute(true, true)));

        todo.DisplaySheet();

        var g = todo.GetListItem(2);

        Console.WriteLine(g.Value);
    }
}