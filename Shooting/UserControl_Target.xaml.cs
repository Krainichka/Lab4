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

namespace Shooting
{
    /// <summary>
    /// Логика взаимодействия для UserControl_Target.xaml
    /// </summary>
    public partial class UserControl_Target : UserControl
    {

        Target target = new Target();
        public UserControl_Target()
        {
            InitializeComponent();
            DataContext = target;
            SetBinding(Canvas.LeftProperty, new Binding("Location.X"));
            SetBinding(Canvas.BottomProperty, new Binding("Location.Y"));
        }

        public Target Target { get => target; set => target = value; }

        public void Destroy()
        {
            img.Source = new BitmapImage(new Uri("Resources/explosion.png", UriKind.RelativeOrAbsolute));
        }
    }

}
