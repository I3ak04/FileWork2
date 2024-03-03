using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FileWork_V2._0
{
    class Repository
    {
        private Worker[] GetAllWorkers()
        {
            string[] AllWorkers = File.ReadAllLines("Diary.txt");
            Worker[] workerArray = new Worker[AllWorkers.Length];
            
            for (int i = 0; i < AllWorkers.Length; i++)
            {
                string[] InfoWorker = AllWorkers[i].Split('#');
                workerArray[i].ID = int.Parse(InfoWorker[0]);
                workerArray[i].TimeOfAdd = DateTime.Parse(InfoWorker[1]);
                workerArray[i].FIO = InfoWorker[2];
                workerArray[i].Age = byte.Parse(InfoWorker[3]);
                workerArray[i].Height = int.Parse(InfoWorker[4]);
                workerArray[i].DateOfBirth = DateTime.Parse(InfoWorker[5]);
                workerArray[i].PlaceOfBorn = InfoWorker[6];
            }
            return workerArray;
        }
        public void GetWorkerById()
        {
            Console.Clear();
            Console.WriteLine("Введите ID рабочего, которого хотите увидеть");
            int id = int.Parse(Console.ReadLine());
            Console.Clear();
            Worker[] workersAll = GetAllWorkers();
            --id;
            Console.Write($"{workersAll[id].ID,2}|" +
                          $"{workersAll[id].TimeOfAdd,20}|" +
                          $"{workersAll[id].FIO,30}|" +
                          $"{workersAll[id].Age,3}|" +
                          $"{workersAll[id].Height,3}|" +
                          $"{workersAll[id].DateOfBirth.ToString("d"),11}|" +
                          $"{workersAll[id].PlaceOfBorn}\n");

            Console.WriteLine("\nВыберите функцию к данному рабочему:\n" +
                              "1 - Удалить рабочего\n" +
                              "2 - Изменить данные рабочего\n" +
                              "3 - Вернуться назад\n");
            string choise = Console.ReadLine();
            Worker workerByID = workersAll[id];
            if (choise == "1") DeleteWorkerByID(workerByID);
            else if (choise == "2") ChangeInfoWorkerByID(workerByID);
            else if (choise == "3") MainMenu();
            else MainMenu();
        }

        private void DeleteWorker(int id)
        {
            Worker[] workersAll = GetAllWorkers();
            id--;
            Console.Write($"{workersAll[id].ID,2}|" +
                          $"{workersAll[id].TimeOfAdd,20}|" +
                          $"{workersAll[id].FIO,30}|" +
                          $"{workersAll[id].Age,3}|" +
                          $"{workersAll[id].Height,3}|" +
                          $"{workersAll[id].DateOfBirth.ToString("d"),11}|" +
                          $"{workersAll[id].PlaceOfBorn} - Был Удален\n");
            id++;

            StreamWriter sw = new StreamWriter("Diary.txt");
            for (int i = 0; i < workersAll.Length; i++)
            {
                if (id == workersAll[i].ID)
                {
                    continue;
                }
                else
                {
                    if(id <= workersAll[i].ID)
                    {
                        workersAll[i].ID--;
                    }
                    sw.Write($"{workersAll[i].ID}#" +
                         $"{workersAll[i].TimeOfAdd}#" +
                         $"{workersAll[i].FIO}#" +
                         $"{workersAll[i].Age}#" +
                         $"{workersAll[i].Height}#" +
                         $"{workersAll[i].DateOfBirth}#" +
                         $"{workersAll[i].PlaceOfBorn}\n");
                }
            }
            sw.Close();
            Console.WriteLine("\nНажмите любую клавишу, чтобы перейти к главному функционалу...");
            Console.ReadKey();
            MainMenu();
        }

        public void AddWorker()
        {
            Console.Clear();
            string[] Text = File.ReadAllLines("Diary.txt");
            
            int ID = Text.Length + 1;
            Worker worker = new Worker();
            worker = Recording(ID, worker);

            StreamWriter sw = new StreamWriter("Diary.txt", File.Exists("Diary.txt"));
           
            sw.Write($"{worker.ID}#" +
                     $"{worker.TimeOfAdd}#" +
                     $"{worker.FIO}#" +
                     $"{worker.Age}#" +
                     $"{worker.Height}#" +
                     $"{worker.DateOfBirth}#" +
                     $"{worker.PlaceOfBorn}");
            if (worker.ID > 1)
            {
                sw.WriteLine();
            }
            sw.Close();

            Console.WriteLine("\nНажмите любую клавишу, чтобы перейти к главному функционалу...");
            Console.ReadKey();
            MainMenu();

        }

        private Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {

            Worker[] workersAll = GetAllWorkers();
            
            // Задает размер массива workersDateFromTo
            int workersDateFromToLenght = 0;
            for (int i = 0; i < workersAll.Length; i++)
            {
                if (workersAll[i].TimeOfAdd > dateFrom && workersAll[i].TimeOfAdd < dateTo)
                {
                    workersDateFromToLenght++;
                }
            }
            Worker[] workersDateFromTo = new Worker[workersDateFromToLenght];
            
            
            int wDFTIndex = 0;
            for (int i = 0; i < workersAll.Length; i++)
            {
                if (workersAll[i].TimeOfAdd > dateFrom && workersAll[i].TimeOfAdd < dateTo)
                {
                    workersDateFromTo[wDFTIndex] = workersAll[i];
                    wDFTIndex++;
                }
            }
            return workersDateFromTo;
        }

        private Worker Recording(int _ID, Worker worker)
        {
            worker.ID = _ID;
            Console.WriteLine("ID " + _ID);

            worker.TimeOfAdd = DateTime.Now;
            Console.WriteLine($"Дата и время добавления записи: {worker.TimeOfAdd}");

            Console.Write("Введите ФИО: ");
            worker.FIO = Console.ReadLine();

            Console.Write("Введите возраст: ");
            worker.Age = byte.Parse(Console.ReadLine());

            Console.Write("Введите рост: ");
            worker.Height = int.Parse(Console.ReadLine());

            Console.Write("Введите дату рождения: ##.##.#### - ");
            worker.DateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.Write("Введите место рождения: ");
            worker.PlaceOfBorn = Console.ReadLine();

            Console.WriteLine("Запись добавлена!");
            return worker;
        }
        public void WriteAllWorkers()
        {
            Console.Clear();
            Worker[] workersAll = GetAllWorkers();
            Console.WriteLine("ID|Время добавления записи|Фамилия Имя Отчество|Возраст|Рост |Дата рождения|Место рождения|\n");
            for(int i = 0; i < workersAll.Length; i++) 
            {
                
                Console.Write($"{workersAll[i].ID,2}|" +
                              $"{workersAll[i].TimeOfAdd,20}|" +
                              $"{workersAll[i].FIO,30}|" +
                              $"{workersAll[i].Age,3}|" +
                              $"{workersAll[i].Height,3}|" +
                              $"{workersAll[i].DateOfBirth.ToString("d"),11}|" +
                              $"{workersAll[i].PlaceOfBorn}\n");
            }
            Console.WriteLine("\nНажмите любую клавишу, чтобы перейти к главному функционалу...");
            Console.ReadKey();
            MainMenu();
        }
        public void WriteWorkersBetweenTwoDates()
        {
            Console.Clear();
            Console.WriteLine("Введите начальную дату ##.##.####: ");
            DateTime dateFrom = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите начальную дату ##.##.####: ");
            DateTime dateTo = DateTime.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine($"Даты добавления {dateFrom.ToString("d")} - {dateTo.ToString("d")}\n");
            Worker[] workersAll = GetWorkersBetweenTwoDates(dateFrom, dateTo);
            for(int i = 0; i < workersAll.Length; i++)
            {
                Console.Write($"{workersAll[i].ID,2}|" +
                              $"{workersAll[i].TimeOfAdd,20}|" +
                              $"{workersAll[i].FIO,30}|" +
                              $"{workersAll[i].Age,3}|" +
                              $"{workersAll[i].Height,3}|" +
                              $"{workersAll[i].DateOfBirth.ToString("d"),11}|" +
                              $"{workersAll[i].PlaceOfBorn}\n");
            }
            Console.WriteLine("\nНажмите любую клавишу, чтобы перейти к главному функционалу...");
            Console.ReadKey();
            MainMenu();
        }
        private void DeleteWorkerByID(Worker workerDelete)
        {
            Worker[] workersAll = GetAllWorkers();
            StreamWriter sw = new StreamWriter("Diary.txt");
            for (int i = 0; i < workersAll.Length; i++)
            {

                if (workerDelete.ID == workersAll[i].ID)
                {
                    continue;
                }
                else
                {
                    if (workerDelete.ID <= workersAll[i].ID)
                    {
                        workersAll[i].ID--;
                    }
                    sw.Write($"{workersAll[i].ID}#" +
                         $"{workersAll[i].TimeOfAdd}#" +
                         $"{workersAll[i].FIO}#" +
                         $"{workersAll[i].Age}#" +
                         $"{workersAll[i].Height}#" +
                         $"{workersAll[i].DateOfBirth}#" +
                         $"{workersAll[i].PlaceOfBorn}\n");
                }
            }
            sw.Close();
            Console.Write($"{workersAll[workerDelete.ID].ID,2}|" +
              $"{workersAll[workerDelete.ID].TimeOfAdd,20}|" +
              $"{workersAll[workerDelete.ID].FIO,30}|" +
              $"{workersAll[workerDelete.ID].Age,3}|" +
              $"{workersAll[workerDelete.ID].Height,3}|" +
              $"{workersAll[workerDelete.ID].DateOfBirth.ToString("d"),11}|" +
              $"{workersAll[workerDelete.ID].PlaceOfBorn} - Был Удален\n");
            Console.WriteLine("\nНажмите любую клавишу, чтобы перейти к главному функционалу...");
            Console.ReadKey();
            MainMenu();
        }
        private void ChangeInfoWorkerByID(Worker worker)
        {
            Console.Clear();
            Console.WriteLine($"{worker.ID} " +
                              $"{worker.TimeOfAdd} " +
                              $"{worker.FIO} " +
                              $"{worker.Age} " +
                              $"{worker.Height} " +
                              $"{worker.DateOfBirth} " +
                              $"{worker.PlaceOfBorn}");
            Console.WriteLine("Выберите, что нужно изменить:\n" +
                              "1 - ФИО\n" +
                              "2 - Возраст\n" +
                              "3 - Рост\n" +
                              "4 - Дату рождения\n" +
                              "5 - Место рождения\n" +
                              "6 - Вернуться назад\n");
            string choise = Console.ReadLine();
            switch (choise) 
            {
                case "1":
                Console.Write("Введите ФИО: ");
                worker.FIO = Console.ReadLine();
                break;
                
                case "2":
                Console.Write("Введите возраст: ");
                worker.Age = byte.Parse(Console.ReadLine());
                break;
                
                case "3":
                Console.Write("Введите рост: ");
                worker.Height = int.Parse(Console.ReadLine());
                break;
                
                case "4":
                Console.Write("Введите дату рождения: ##.##.#### - ");
                worker.DateOfBirth = DateTime.Parse(Console.ReadLine());
                break;
                
                case "5":
                Console.WriteLine("Введите место рождения");
                worker.PlaceOfBorn = Console.ReadLine();
                break;
                
                case "6":
                MainMenu();
                break;
                
                default:
                ChangeInfoWorkerByID(worker);
                break;
            }
            Worker[] workersAll = GetAllWorkers();
            StreamWriter sw = new StreamWriter("Diary.txt");
            for (int i = 0; i < workersAll.Length; i++)
            {
                if (workersAll[i].ID == worker.ID)
                {
                    sw.Write($"{worker.ID}#" +
                             $"{worker.TimeOfAdd}#" +
                             $"{worker.FIO}#" +
                             $"{worker.Age}#" +
                             $"{worker.Height}#" +
                             $"{worker.DateOfBirth}#" +
                             $"{worker.PlaceOfBorn}\n");
                }
                else
                {


                    sw.Write($"{workersAll[i].ID}#" +
                             $"{workersAll[i].TimeOfAdd}#" +
                             $"{workersAll[i].FIO}#" +
                             $"{workersAll[i].Age}#" +
                             $"{workersAll[i].Height}#" +
                             $"{workersAll[i].DateOfBirth}#" +
                             $"{workersAll[i].PlaceOfBorn}\n");
                }
                
            }
            sw.Close();
            ChangeInfoWorkerByID(worker);
        }
        public void AskWorkerIDForDelete()
        {
            Console.Clear();
            Console.WriteLine("Введите ID работника, которого хотите удалить");
            int IdDelete = int.Parse(Console.ReadLine());
            DeleteWorker(IdDelete);
        }

        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Выберите нужную функцию:\n\n" +
                              "1 - Просмотр всех записей\n" +
                              "2 - Просмотр одной записи\n" +
                              "3 - Создание записи\n" +
                              "4 - Удаление записи\n" +
                              "5 - Просмотр записей в выбранном диапазоне дат");
            
            string choise = Console.ReadLine();
            if (choise == "1") WriteAllWorkers();
            else if (choise == "2") GetWorkerById();
            else if (choise == "3") AddWorker();
            else if (choise == "4") AskWorkerIDForDelete();
            else if (choise == "5") WriteWorkersBetweenTwoDates();
            else MainMenu();
        }
    }
}
