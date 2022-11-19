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
    /// Логика взаимодействия для WindowOnePlayer.xaml
    /// </summary>
    public partial class WindowOnePlayer : Window
    {
        public WindowOnePlayer()
        {
            InitializeComponent();
        }
        int size = 16,
            stepX = 0,
            stepY = 0,
            score = 0,
            speed = 10;
        DispatcherTimer gameTimer;
        Rect foodRect;
        Rect pacmanRect;
        Ellipse food;
        Random random;
        Path pacman;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            random = new Random();
            foodRect = new Rect(random.Next(size, (int)CanvasMap.ActualWidth - size),
               random.Next(size, (int)CanvasMap.ActualHeight - size), size, size);
            food = CreateEllipse(foodRect, Brushes.Red);
            CanvasMap.Children.Add(food);

            pacmanRect = new Rect(random.Next(size, (int)CanvasMap.ActualWidth - 32),
                random.Next(size, (int)CanvasMap.ActualHeight - 30), size * 2, size * 2);
            pacman = CreatePacman(pacmanRect, Brushes.Red);
            CanvasMap.Children.Add(pacman);

            ScoreBox.Text = score.ToString();

            gameTimer = new DispatcherTimer();
            gameTimer.Tick += GameTimerTick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            gameTimer.IsEnabled = true;
        }

        private void GameTimerTick(object sender, EventArgs e)
        {
            MovePacman();
            if (pacmanRect.IntersectsWith(foodRect)) MoveFood();
            Death();
        }

        private void Death()
        {
            if (pacmanRect.X < 0 || pacmanRect.X + 32 > CanvasMap.ActualWidth || pacmanRect.Y < 0 || pacmanRect.Y + 30 > CanvasMap.ActualHeight)
            {
                gameTimer.IsEnabled = false;
                OnePlayerRestartWindow onePlayerRestartWindow = new OnePlayerRestartWindow();
                onePlayerRestartWindow.Show();
                Close();
            }
        }
        private void MovePacman()
        {
            pacmanRect.X = pacmanRect.X + stepX;
            pacmanRect.Y = pacmanRect.Y + stepY;
            Canvas.SetLeft(pacman, pacmanRect.X);
            Canvas.SetTop(pacman, pacmanRect.Y);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                stepY = speed;
                stepX = 0;
                pacman.Data.Transform = new RotateTransform(90, size, size);
            }
            if (e.Key == Key.Up)
            {
                stepY = -1 * speed;
                stepX = 0;
                pacman.Data.Transform = new RotateTransform(270, size, size);
            }
            if (e.Key == Key.Left)
            {
                stepY = 0;
                stepX = -1 * speed;
                pacman.Data.Transform = new RotateTransform(180, size, size);
            }
            if (e.Key == Key.Right)
            {
                stepY = 0;
                stepX = speed;
                pacman.Data.Transform = new RotateTransform(360, size, size);
            }
            if (e.Key == Key.S)
            {
                stepY = speed;
                stepX = 0;
                pacman.Data.Transform = new RotateTransform(90, size, size);
            }
            if (e.Key == Key.W)
            {
                stepY = -1 * speed;
                stepX = 0;
                pacman.Data.Transform = new RotateTransform(270, size, size);
            }
            if (e.Key == Key.A)
            {
                stepY = 0;
                stepX = -1 * speed;
                pacman.Data.Transform = new RotateTransform(180, size, size);
            }
            if (e.Key == Key.D)
            {
                stepY = 0;
                stepX = speed;
                pacman.Data.Transform = new RotateTransform(360, size, size);
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
            score++;
            ScoreBox.Text = score.ToString();
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
