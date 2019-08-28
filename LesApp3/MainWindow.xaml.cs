using System;
using System.Collections.Generic;
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

// В даному прикладі використано збереження налаштувань через Properties.
// стр. 302, 2008 C#. Советы программистам (Климов А. П.)

// Зберігає в "c:\Users\Bohdan\AppData\Local\LesApp3\"

namespace LesApp3
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
            cp.SelectedColor = Properties.Settings.Default.SaveColor;
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
            Properties.Settings.Default.SaveColor = cp.SelectedColor.Value;
            // Збереження налаштувань
            Properties.Settings.Default.Save();
        }
    }
}
