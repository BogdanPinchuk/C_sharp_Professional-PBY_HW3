using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// https://docs.microsoft.com/ru-ru/dotnet/standard/io/isolated-storage

namespace LesApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// При завантаженні вікна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // установка кольору
            //cp.SelectedColor = Properties.Settings.Default.SaveColor;

            // створення ізольованого середовища
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                // перевірка інстування директорії
                if (!storage.DirectoryExists("Settings"))
                {
                    // виходимо
                    return;
                }

                // перевірка існування файла
                if (!storage.FileExists(@"Settings\Properties.txt"))
                {
                    // виходимо
                    return;
                }

                // створення файлового фотоку
                using (IsolatedStorageFileStream stream =
                    new IsolatedStorageFileStream(@"Settings\Properties.txt", FileMode.Open, FileAccess.Read, FileShare.Read, storage))
                {
                    // створення читача
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        // зчитування даних з файлу по типу ARGB
                        byte[] ARGB = reader.ReadBytes(4);

                        // установка кольору
                        try
                        {
                            cp.SelectedColor = new Color()
                            {
                                A = ARGB[0],
                                R = ARGB[1],
                                G = ARGB[2],
                                B = ARGB[3],
                            };
                        }
                        catch (IndexOutOfRangeException ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Коли змінився колір
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cp_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            // відображення назви кольору
            lb.Content = cp.SelectedColorText;
            // установка кольору фону label
            lb.Background = new SolidColorBrush(cp.SelectedColor.Value);
        }

        /// <summary>
        /// Дії при натисканні на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.SaveColor = cp.SelectedColor.Value;
            // Збереження налаштувань
            //Properties.Settings.Default.Save();

            // створення ізольованого середовища
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForAssembly())
            {
                // перевірка інстування директорії
                if (!storage.DirectoryExists("Settings"))
                {
                    // створюємо  директорію
                    storage.CreateDirectory("Settings");
                }

                // перевірка існування файла
                if (!storage.FileExists(@"Settings\Properties.txt"))
                {
                    // створюємо файл
                    storage.CreateFile(@"Settings\Properties.txt");
                }

                // створення файлового фотоку
                using (IsolatedStorageFileStream stream =
                    new IsolatedStorageFileStream(@"Settings\Properties.txt", FileMode.Open, FileAccess.Write, FileShare.Write, storage))
                {
                    // створення письменника
                    using (BinaryWriter writer = new BinaryWriter(stream))
                    {
                        // запис даних в файл по типу ARGB
                        writer.Write(new byte[]
                        {
                            cp.SelectedColor.Value.A,
                            cp.SelectedColor.Value.R,
                            cp.SelectedColor.Value.G,
                            cp.SelectedColor.Value.B,
                        });
                    }
                }
            }
        }

    }
}
