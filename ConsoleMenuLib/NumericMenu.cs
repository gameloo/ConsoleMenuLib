using System;
using System.Collections.Generic;

namespace ConsoleMenuLib
{
    /// <summary>
    /// Класс реализует консольное меню с навигацией по ключам\занчениям
    /// </summary>
    public class NumericMenu : IMenu
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
        private Dictionary<int, IMenu> submenus;

        /// <param name="headline">Заголовок меню</param>
        public NumericMenu(string headline)
        {
            Headline = headline;
            submenus = new Dictionary<int, IMenu>();
            NavigationHistory = new Stack<IMenu>();
        }

        /// <summary>
        /// Добавить подменю в данное меню
        /// </summary>
        /// <param name="menu">Подменю</param>
        public void Add(IMenu menu)
        {
            submenus.Add(submenus.Count, menu);
            menu.NavigationHistory = NavigationHistory;                                                                     // Добавление подменю к общей истории навигации
        }

        /// <summary>
        /// Вывод меню в консоль
        /// </summary>
        public void ShowMenu()
        {
            ReadCommand();
        }

        private void PrintMenu()                                                                                            // Вывод в консоль меню
        {
            Console.Clear();
            Console.WriteLine($"### {Headline} ###\n");
            foreach (var i in submenus)
            {
                Console.WriteLine("{0}) {1}", i.Key, i.Value.Headline);
            }
            Console.WriteLine("\n### Команды для навигации в меню ###");
            if (NavigationHistory.Count != 0) Console.WriteLine("back - Вернуться в предыдущее меню");
            Console.WriteLine("exit - Завершить работу приложения");
        }

        private void ReadCommand()                                                                                          // Чтение команд из консоли
        {
            while (true)
            {
                PrintMenu();

                string inputString = Console.ReadLine();
                switch (inputString)
                {
                    case "exit":                                                                                            // Завершение работы приложения
                        {
                            UtilityClass.CloseApp();
                            return;
                        }
                    case "back":                                                                                            // Переход в предыдущее меню
                        {
                            if (NavigationHistory.Count != 0)
                            {
                                NavigationHistory.Pop().ShowMenu();
                                return;
                            }
                            else
                            {
                                Console.WriteLine("Вы в главном меню");
                            }
                            break;
                        }
                    default:                                                                                                // Переход в подменю
                        {
                            if (!Int32.TryParse(inputString, out int key))                                                  // Проверка введенной сроки на формат
                            {
                                Console.WriteLine($"Команды {inputString} не существует. Повторите ввод команды.");
                                UtilityClass.PressAnyKey();
                                break;
                            }
                            else if (!submenus.ContainsKey(key))                                                            // Проверка есть ли подменю с номером key
                            {
                                Console.WriteLine($"Подменю с номером {inputString} не существует. Повторите ввод команды.");
                                UtilityClass.PressAnyKey();
                                break;
                            }
                            // Проверки пройдены
                            else
                            {
                                NavigationHistory.Push(this);
                                submenus[key].ShowMenu();
                                return;
                            }
                        }
                }
            }
        }
    }
}
