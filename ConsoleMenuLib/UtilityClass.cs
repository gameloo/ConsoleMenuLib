using System;

namespace ConsoleMenuLib
{
    public class UtilityClass
    {
        public static void PressAnyKey()
        {
            Console.WriteLine("Нажмите любую клавишу чтобы продолжить");
            Console.ReadKey();
            Console.Clear();
        }

        public static void CloseApp()
        {
            Console.Clear();
            Console.WriteLine(">Завершение работы приложения...");
            PressAnyKey();
            Environment.Exit(0);
        }
    }
}
