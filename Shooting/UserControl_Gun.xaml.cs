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
    /// Логика взаимодействия для UserControl_Gun.xaml
    /// </summary>
    public partial class UserControl_Gun : UserControl
    {
        public Gun Gun { get => (Gun)DataContext; }
        public UserControl_Gun()
        {
            InitializeComponent();
            DataContext= new Gun();
            Binding binding= new Binding("Location.X");
            tbCoord.SetBinding(TextBlock.TextProperty, new Binding("Location") { Converter =new ConvPosition()});
            SetBinding(Canvas.LeftProperty, binding);
            SetBinding(Canvas.BottomProperty, new Binding("Location.Y"));
        }
        private void main_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tbCoord.Visibility = tbCoord.Visibility == Visibility.Hidden ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
