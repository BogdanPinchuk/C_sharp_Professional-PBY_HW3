using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp2
{
    class Program
    {
        // випадкові числа
        static Random rnd = new Random();

        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

            // Заголовок
            Console.Title = "Зіграємо в хованки?";

            // вибираємо директорію в папці з exe файлом, щоб не мусорити по системі
            var directory = new DirectoryInfo(@".");

            #region Мусорка
            // створюємо мусорку
            string trash = @"Dump\";

            try
            {
                // так як директорія вибрана біля exe файла, 
                // то не треба первіряти наявність каталогу
                directory.CreateSubdirectory(trash);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            // зміна ссилки на директорію
            directory = new DirectoryInfo(trash);
            #endregion

            // кількість папок
            int folderCount = rnd.Next(27, 101);

            // якщо директорія наявна створюємо випадкову кількість папок
            if (directory.Exists)
            {
                #region Створення папок
                // створення папок
                for (int i = 0; i < folderCount; i++)
                {
                    directory.CreateSubdirectory("Folder " + i);
                }

                // виведення інформації
                Console.WriteLine("\n\tДиректорії створені.");
                #endregion

                #region Ховання файла у випадковій папці
                // створення файлу в мусорці
                var file = new FileInfo(directory.FullName +
                    "Folder " + rnd.Next(0, folderCount) + @"\MyHiddenFile.txt");

                #region Запис у файл
                // створює файл для запису
                StreamWriter writer = file.CreateText();
                // записує рядок
                writer.WriteLine("Вітаю, Ви знайшли схований файл!!!");
                // закриває файл
                writer.Close();
                #endregion

                // виведення сповіщення
                Console.WriteLine("\n\tФайл сховано, спробуєте найти вручну?");

                // відкриття директорії
                Process.Start(directory.FullName);

                // нажати для продовження
                Console.WriteLine("\n\tНажміть для продовження.");
                Console.ReadKey(true);
                #endregion

                #region Пошук файла
                // находження файла за допомогою програми 
                var res = directory.GetFiles("MyHiddenFile.txt", SearchOption.AllDirectories);

                // результат
                Console.WriteLine("\n\tФайл був найдений в наступних директоріях:\n");
                foreach (var i in res)
                {
                    Console.WriteLine(i.DirectoryName + "\n");
                }

                // відкриття необхідної директорії
                Process.Start(res[0].DirectoryName);

                // нажати для продовження
                Console.WriteLine("\n\tНажміть для продовження.");
                Console.ReadKey(true);
                #endregion

                // відкриття першого файлу
                #region Не підходить для реалізації згідно умови
#if false
                FileStream stream = res[0].Open(FileMode.Open, FileAccess.Read, FileShare.Read);

                // змінна для даних в середині
                byte[] data = new byte[30];
                stream.Read(data, 0, data.Length);

                Console.WriteLine("\n\tЗчитані дані:\n\t");
                foreach (var i in data)
                {
                    Console.Write(" " + (char)i);
                }
                Console.WriteLine();  
#endif
                #endregion

                #region Читання найденого файла
                // створюємо читача
                StreamReader reader = file.OpenText();

                // зчитуємо дані до кінця
                string data = reader.ReadToEnd();

                Console.WriteLine("\n\tЗчитані дані:\n\t");
                Console.WriteLine(data);
                Console.WriteLine();

                // закриття файлу
                reader.Close();
                #endregion

                #region Архівція
                // створення архівів
                FileStream destination1 = File.Create(res[0].DirectoryName + @"\ArchiveG.zip"),
                    destination2 = File.Create(res[0].DirectoryName + @"\ArchiveD.zip");

                // компресори
                GZipStream compres1 = new GZipStream(destination1, CompressionMode.Compress);
                DeflateStream compres2 = new DeflateStream(destination2, CompressionMode.Compress);

                // відкриття необхідної директорії
                Process.Start(res[0].DirectoryName);

                // закриття компресорів
                compres1.Close();
                compres2.Close();

                // нажати для продовження
                Console.WriteLine("\n\tНажміть для продовження.");
                Console.ReadKey(true);
                #endregion

            }
            else
            {
                Console.WriteLine($"Дитректорія: {directory.FullName} - не існує.");
            }

            #region Видалення мусору
            // можна видалити їх всіх по одній і марнувати час, а можна всі зразу
            try
            {
                // видалення цієї директорії з піддеректоріями
                directory.Delete(true);

                // виведення інформації
                Console.WriteLine("\n\tДиректорія видалена.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            // Delay
            Console.ReadKey(true);
        }
    }
}
