using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesApp1
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

            // створення файлу в мусорці
            var file = new FileInfo(directory.FullName + "MyFile.txt");
            //var file = File.Create(directory.FullName + "MyFile.txt");

            #region WriteByte
#if false
            #region Створення файлу і запис в нього даних
            // змінна для стріму в файл
            FileStream stream;

            // пробуємо відкрити і записати щось в нього
            try
            {
                // відкриваємо файл
                stream = file.Open(FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);

                // створюємо дані для запису
                byte[] data = Enumerable.Range(27, 27).Select(t => (byte)t).ToArray();

                // записуємо щось в файл
                stream.Write(data, 0, data.Length);

                // закриваємо файл
                stream.Close();

                // показуємо, що щось записано
                Process.Start(file.FullName);

                // нажати для продовження
                Console.WriteLine("\n\tНажміть для продовження.");
                Console.ReadKey(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion

            #region Відкриття файлу і зчитування даних
            // пробуємо відкрити і прочитати
            try
            {
                // відкриваємо файл
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.Read);

                // змінна для даних в середині
                byte[] data = new byte[30];
                stream.Read(data, 0, data.Length);

                Console.WriteLine("\n\tЗчитані дані:\n\t");
                foreach (var i in data)
                {
                    Console.Write(" " + i);
                }
                Console.WriteLine();

                // відкриваємо директорію для слідкування за файлом
                Process.Start(directory.FullName);

                // закриваємо файл
                stream.Close();

                // нажати для продовження
                Console.WriteLine("\n\tНажміть для продовження.");
                Console.ReadKey(true);

                // видаляємо вайл
                file.Delete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion
#endif
            #endregion

            #region StreamWriter/Reader
#if false
            // створює файл для запису
            StreamWriter writer = file.CreateText();
            // записує рядок
            writer.WriteLine("Записую що небуть.");
            // закриває файл
            writer.Close();

            // Відкриваємо для перегляду
            Process.Start(file.FullName);

            // нажати для продовження
            Console.WriteLine("\n\tНажміть для продовження.");

            // створюємо читача
            StreamReader reader = file.OpenText();

            // зчитуємо дані до кінця
            string data = reader.ReadToEnd();

            Console.WriteLine("\n\tЗчитані дані:\n\t");
            Console.WriteLine(data);
            Console.WriteLine();

            // відкриваємо директорію для слідкування за файлом
            Process.Start(directory.FullName);

            // закриваємо файл
            reader.Close();

            // нажати для продовження
            Console.WriteLine("\n\tНажміть для продовження.");
            Console.ReadKey(true);

            // видаляємо вайл
            file.Delete();
#endif
            #endregion

            Console.ReadKey(true);

            #region Видалення мусору
            // можна видалити їх всіх по одній і марнувати час, а можна всі зразу
            try
            {
                // видалення цієї директорії з піддеректоріями
                directory.Delete(true);

                // виведення інформації
                Console.WriteLine("\n\tМусорка видалена.");
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
