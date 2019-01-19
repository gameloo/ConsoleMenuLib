using System;
using System.Collections.Generic;

namespace ConsoleMenuLib
{
    /// <summary>
    /// Класс реализует функционал меню
    /// </summary>
    public class SubmenuAction : IMenu
    {
        private string description;
        public string Headline
        {
            get { return description; }
            set { description = value ?? throw new ArgumentNullException(); }
        }

        private Stack<IMenu> navigationHistory;
        public Stack<IMenu> NavigationHistory
        {
            get { return navigationHistory; }
            set { navigationHistory = value ?? throw new ArgumentNullException(); }
        }
        private readonly Action action;

        /// <param name="action">Делегат</param>
        /// <param name="desctription">Описание</param>
        public SubmenuAction(Action action, string desctription)
        {
            this.action = action;
            Headline = desctription;
        }

        public void ShowMenu()
        {
            Console.Clear();
            action();
            UtilityClass.PressAnyKey();
            NavigationHistory.Pop().ShowMenu();
        }

    }
}
