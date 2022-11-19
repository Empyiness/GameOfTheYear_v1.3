using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace GameOfTheYear
{
    /// <summary>
    /// Логика взаимодействия для WindowTwoPlayers.xaml
    /// </summary>
    public partial class WindowTwoPlayers : Window
    {
        public WindowTwoPlayers()
        {
            InitializeComponent();
        }
        int size = 16,
            stepXPlayer1 = 0,
            stepYPlayer1 = 0,
            stepXPlayer2 = 0,
            stepYPlayer2 = 0,
            speed = 10,
            player1Score = 0,
            player2Score = 0;
        DispatcherTimer gameTimer;
        Rect foodRect;
        Rect player1Rect;
        Rect player2Rect;
        Ellipse food;
        Random random;
        Path player1;
        Path player2;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            random = new Random();
            foodRect = new Rect(random.Next(size, (int)CanvasMap.ActualWidth - size),
               random.Next(size, (int)CanvasMap.ActualHeight - size), size, size);
            food = CreateEllipse(foodRect, Brushes.Red);
            CanvasMap.Children.Add(food);

            player1Rect = new Rect(random.Next(size, (int)CanvasMap.ActualWidth - 32),
                random.Next(size, (int)CanvasMap.ActualHeight - 30), size * 2, size * 2);
            player1 = CreatePacman(player1Rect, Brushes.Red);
            CanvasMap.Children.Add(player1);

            player2Rect = new Rect(random.Next(size, (int)CanvasMap.ActualWidth - 32),
                random.Next(size, (int)CanvasMap.ActualHeight - 30), size * 2, size * 2);
            player2 = CreatePacman(player2Rect, Brushes.Red);
            CanvasMap.Children.Add(player2);

            Player1ScoreBox.Text = player1Score.ToString();
            Player2ScoreBox.Text = player2Score.ToString();

            gameTimer = new DispatcherTimer();
            gameTimer.Tick += GameTimerTick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            gameTimer.IsEnabled = true;
        }

        private void GameTimerTick(object sender, EventArgs e)
        {
            MovePacman();
            if (player1Rect.IntersectsWith(foodRect))
            {
                MoveFood();
                player1Score++;
                Player1ScoreBox.Text = player1Score.ToString();
            }
            if (player2Rect.IntersectsWith(foodRect))
            {
                MoveFood();
                player2Score++;
                Player2ScoreBox.Text = player2Score.ToString();
            }
            Death();
        }

        private void Death()
        {
            if (player1Rect.X < 0 || player1Rect.X + 32 > CanvasMap.ActualWidth || player1Rect.Y < 0 || player1Rect.Y + 30 > CanvasMap.ActualHeight ||
                player2Rect.X < 0 || player2Rect.X + 32 > CanvasMap.ActualWidth || player2Rect.Y < 0 || player2Rect.Y + 30 > CanvasMap.ActualHeight)
            {
                gameTimer.IsEnabled = false;
                TwoPlayerRestartWindow twoPlayerRestartWindow = new TwoPlayerRestartWindow();
                twoPlayerRestartWindow.Show();
                Close();
            }
        }
        private void MovePacman()
        {
            player1Rect.X += stepXPlayer1;
            player1Rect.Y += stepYPlayer1;
            Canvas.SetLeft(player1, player1Rect.X);
            Canvas.SetTop(player1, player1Rect.Y);
            player2Rect.X += stepXPlayer2;
            player2Rect.Y += stepYPlayer2;
            Canvas.SetLeft(player2, player2Rect.X);
            Canvas.SetTop(player2, player2Rect.Y);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                stepYPlayer1 = speed;
                stepXPlayer1 = 0;
                player1.Data.Transform = new RotateTransform(90, size, size);
            }
            if (e.Key == Key.Up)
            {
                stepYPlayer1 = -1 * speed;
                stepXPlayer1 = 0;
                player1.Data.Transform = new RotateTransform(270, size, size);
            }
            if (e.Key == Key.Left)
            {
                stepYPlayer1 = 0;
                stepXPlayer1 = -1 * speed;
                player1.Data.Transform = new RotateTransform(180, size, size);
            }
            if (e.Key == Key.Right)
            {
                stepYPlayer1 = 0;
                stepXPlayer1 = speed;
                player1.Data.Transform = new RotateTransform(360, size, size);
            }
            if (e.Key == Key.S)
            {
                stepYPlayer2 = speed;
                stepXPlayer2 = 0;
                player2.Data.Transform = new RotateTransform(90, size, size);
            }
            if (e.Key == Key.W)
            {
                stepYPlayer2 = -1 * speed;
                stepXPlayer2 = 0;
                player2.Data.Transform = new RotateTransform(270, size, size);
            }
            if (e.Key == Key.A)
            {
                stepYPlayer2 = 0;
                stepXPlayer2 = -1 * speed;
                player2.Data.Transform = new RotateTransform(180, size, size);
            }
            if (e.Key == Key.D)
            {
                stepYPlayer2 = 0;
                stepXPlayer2 = speed;
                player2.Data.Transform = new RotateTransform(360, size, size);
            }
        }

        Ellipse CreateEllipse(Rect rect, Brush brush)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = rect.Width;
            ellipse.Height = rect.Height;
            ellipse.Fill = brush;
            Canvas.SetLeft(ellipse, rect.X);
            Canvas.SetTop(ellipse, rect.Y);
            return ellipse;
        }
        private void MoveFood()
        {
            foodRect = new Rect(random.Next(size, (int)CanvasMap.ActualWidth - size),
            random.Next(size, (int)CanvasMap.ActualHeight - size), size, size);
            Canvas.SetLeft(food, foodRect.X);
            Canvas.SetTop(food, foodRect.Y);
        }
        Path CreatePacman(Rect rect, Brush brush)
        {
            Path pacman = new Path();
            PathFigure p1 = new PathFigure();
            PathFigure p2 = new PathFigure();
            LineSegment l1 = new LineSegment();
            LineSegment l2 = new LineSegment();
            p1.StartPoint = new Point(30, 8);
            l1.Point = new Point(16, 16);
            l2.Point = new Point(30, 24);
            p1.Segments.Add(l1);
            p1.Segments.Add(l2);
            ArcSegment al = new ArcSegment();
            p2.StartPoint = new Point(30, 8);
            al.Point = new Point(30, 24);
            al.Size = new Size(size, size);
            al.IsLargeArc = true;
            p2.Segments.Add(al);
            PathGeometry pg = new PathGeometry();
            pg.Figures.Add(p1);
            pg.Figures.Add(p2);
            pacman.Data = pg;
            pacman.Stroke = brush;
            pacman.Fill = brush;
            Canvas.SetLeft(pacman, rect.X);
            Canvas.SetTop(pacman, rect.Y);
            return pacman;
        }
    }
}
