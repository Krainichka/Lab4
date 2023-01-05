using Shooting;
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

namespace Game
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        UserControl_Bomb bombUC;
        UserControl_Hill hill = new UserControl_Hill();
        UserControl_Target target=new UserControl_Target();
        UserControl_Gun gunUC;
        Random random= new Random();
        bool isStopped= false;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += Timer_Tick;
        }

        private void GunUC_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point relativePoint = gunUC.TransformToAncestor(mainCanvas).Transform(new Point(0, 0));
        }

        private void StartGame()
        {
            double cx = mainCanvas.ActualWidth;
            mainCanvas.Children.Clear();
            bombUC = new UserControl_Bomb();
            bombUC.MouseRightButtonDown += BombUC_MouseRightButtonDown;
            mainCanvas.Children.Remove(target);
            target = new UserControl_Target();
            target.Target.Location = new Point(random.Next((int)(2 * cx / 3), (int)(cx - 100)),0);
            mainCanvas.Children.Add(target);
            hill.Hill.Location= new Point(random.Next((int)(cx / 3) + 100, (int)(2 * cx / 3) - 100), 0);
            mainCanvas.Children.Add(hill);
            int gunCoordinateX = random.Next(0, (int)(cx / 3));
            gunUC=new UserControl_Gun();
            var gun = gunUC.Gun;
            grid.DataContext = gun;
            gun.X +=  gunCoordinateX;
            gun.Location= new Point(gunCoordinateX, 0);
            mainCanvas.Children.Add(gunUC);
            bombUC.Bomb = new Bomb(0, 0, new Point(gun.X, gun.Y));
            tb.DataContext= bombUC.Bomb;
            tb.SetBinding(TextBlock.TextProperty, new Binding("Location") { Converter= new ConvPosition() });
        }

        private void BombUC_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            tb.Visibility = tb.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var gun=gunUC.Gun;
            var bomb = bombUC.Bomb;
            bomb.Move();
            gogo(bomb.Location.X +gun.X, bomb.Location.Y +gun.Y);
            if (hasHit())
            {
                ButtonStart.Content = "Старт";
                target.Destroy();
                bombUC.Explosion();
                timer.Stop();
                return;
            }
            if (bomb.Location.Y <= 0 || hasHitOnHill())
            {
                ButtonStart.Content = "Старт";
                bombUC.Explosion();
                timer.Stop();
                isStopped= true;
            }
        }

        private bool hasHit()
        {
            GeneralTransform transform2 = target.TransformToVisual(this);
            GeneralTransform transform1 = bombUC.TransformToVisual(this);

            Rect r1 = transform1.TransformBounds(new Rect { X = 0, Y = 0, Width = bombUC.ActualWidth, Height = bombUC.ActualHeight });
            Rect r2 = transform2.TransformBounds(new Rect { X = 0, Y = 0, Width = target.ActualWidth, Height = target.ActualHeight });

            return r1.IntersectsWith(r2);
        }
        private bool hasHitOnHill()
        {
            GeneralTransform transform2 = hill.TransformToVisual(this);
            GeneralTransform transform1 = bombUC.TransformToVisual(this);

            Rect r1 = transform1.TransformBounds(new Rect { X = 0, Y = 0, Width =bombUC.ActualWidth, Height = bombUC.ActualHeight });
            Rect r2 = transform2.TransformBounds(new Rect { X = 0, Y = 0, Width = hill.ActualWidth, Height = hill.ActualHeight });

            return r1.IntersectsWith(r2);
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            ButtonStart.Content = ButtonStart.Content.ToString() == "Старт" ? "Пауза" : "Старт";
            if (timer.IsEnabled == false)
            {
                var gun = gunUC.Gun;
                if (isStopped) // если ропали в гору или в землю
                {
                    bombUC.Bomb.Time = 0;
                    bombUC.ReNew();
                    isStopped = false;
                }
                bombUC.Bomb.Angle = gun.Angle;
                bombUC.Bomb.Gravity = 9.8;
                bombUC.Bomb.Velocity = gun.InitialSpeed;
                bombUC.Bomb.Location = new Point(gun.X, gun.Y);
                timer.Start(); 
            }
            else
            {
                timer.Stop();
            }           
        }

        private void gogo(double left, double top)
        {
            mainCanvas.Children.Remove(bombUC);
            mainCanvas.Children.Add(bombUC);
            bombUC.Bomb.Location= new Point(left, top);
        }
        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            StartGame();

        }

        private void mainCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

    }
}
