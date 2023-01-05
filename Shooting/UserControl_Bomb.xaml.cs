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
    /// Логика взаимодействия для UserControl_Bomb.xaml
    /// </summary>
    public partial class UserControl_Bomb : UserControl
    {
        public UserControl_Bomb()
        {
            InitializeComponent();
            Binding binding = new Binding("Location.X");
            tbBomb.SetBinding(TextBlock.TextProperty, new Binding("Location") { Converter = new ConvPosition() });
            SetBinding(Canvas.LeftProperty, binding);
            SetBinding(Canvas.BottomProperty, new Binding("Location.Y"));
        }

        public Bomb Bomb { get => (Bomb)DataContext; set => DataContext = value; }

        public void Explosion()
        {
            mainGrid.Background = (ImageBrush)Resources["exp"];
        }

        public void ReNew()
        {
            mainGrid.Background = (ImageBrush)Resources["def"];
            Bomb.Time = 0;
        }

    }
}
