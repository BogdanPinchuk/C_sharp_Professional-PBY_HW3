using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp0
{
    class Program
    {
        static void Main()
        {
            // Join Unicode
            Console.OutputEncoding = Encoding.Unicode;

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

            #region Створення 100 папок
            // перевірка наявності директорії
            if (directory.Exists)
            {
                // створеня 100 директорій
                for (int i = 0; i < 100; i++)
                {
                    directory.CreateSubdirectory("Folder_" + i);
                    // другий варіант
                    //Directory.CreateDirectory(trash + "Folder_" + i);
                }

                // виведення інформації
                Console.WriteLine("\n\tДиректорії створені.");

                // відкриваємо папку для перегляду
                Process.Start(directory.FullName);

                // нажати для продовження
                Console.WriteLine("\n\tНажміть для продовження, видалення директорій.");
                Console.ReadKey(true);

            }
            else
            {
                Console.WriteLine($"Дитректорія: {directory.FullName} - не існує.");
            }
            #endregion

            #region Видалення мусору
            // можна видалити їх всіх по одній і марнувати час, а можна всі зразу
            try
            {
                // видалення цієї директорії з піддеректоріями
                directory.Delete(true);
                // другий варіант
                //Directory.Delete(trash, true);

                // виведення інформації
                Console.WriteLine("\n\tДиректорії видалені.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            // delay
            Console.ReadKey(true);
        }
    }
}
