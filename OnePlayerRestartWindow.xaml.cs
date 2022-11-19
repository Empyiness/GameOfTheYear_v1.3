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
using System.Windows.Shapes;

namespace GameOfTheYear
{
    /// <summary>
    /// Логика взаимодействия для OnePlayerRestartWindow.xaml
    /// </summary>
    public partial class OnePlayerRestartWindow : Window
    {
        public OnePlayerRestartWindow()
        {
            InitializeComponent();
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            WindowOnePlayer windowOnePlayer = new WindowOnePlayer();
            windowOnePlayer.Show();
            Close();
        }
        private void BackToMenu(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                WindowOnePlayer windowOnePlayer = new WindowOnePlayer();
                windowOnePlayer.Show();
                Close();
            }
        }
    }
}
