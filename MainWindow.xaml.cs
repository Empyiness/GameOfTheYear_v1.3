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
using System.Windows.Threading;

namespace GameOfTheYear
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
        private void OnePlayer(object sender, RoutedEventArgs e)
        {
            WindowOnePlayer windowOnePlayer = new WindowOnePlayer();
            windowOnePlayer.Show();
            Close();
        }
        private void TwoPlayer(object sender, RoutedEventArgs e)
        {
            WindowTwoPlayers windowTwoPlayers = new WindowTwoPlayers();
            windowTwoPlayers.Show();
            Close();
        }
        private void Exit(object sender, RoutedEventArgs e)
        {
            ExitWindow exitWindow = new ExitWindow();
            exitWindow.Show();
            Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ExitWindow exitWindow = new ExitWindow();
                exitWindow.Show();
                Close();
            }

        }
    }
}
