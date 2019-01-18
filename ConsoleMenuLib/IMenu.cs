using System.Collections.Generic;

namespace ConsoleMenuLib
{
    public interface IMenu
    {
        void ShowMenu();
        string Headline { get; set; }
        Stack<IMenu> NavigationHistory { get; set; }
    }
}
