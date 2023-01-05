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
    /// Логика взаимодействия для UserControl_Hill.xaml
    /// </summary>
    public partial class UserControl_Hill : UserControl
    {
        Hill hill = new Hill();
        public UserControl_Hill()
        {
            InitializeComponent();
            DataContext= hill;
            SetBinding(Canvas.LeftProperty, new Binding("Location.X"));
            SetBinding(Canvas.BottomProperty, new Binding("Location.Y"));
        }

        public Hill Hill { get => hill; set => hill = value; }
    }
}
