using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWork_V2._0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Repository Repository = new Repository();
            if (!File.Exists("Diary.txt"))
            {
                File.Create("Diary.txt");
                Console.WriteLine("Нет ни одной записи. нужно её добавить");
                Repository.AddWorker();
            }
            Repository.MainMenu();
            

            Console.WriteLine("\nНажмите любую клавишу, чтобы выйти...");
            Console.ReadKey();
        }
    }
}
