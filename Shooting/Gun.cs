using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Shooting
{
    public class Gun : INotifyPropertyChanged
    {
        double initalSpeed = 100;
        double angle=30;
        double x=110;//координата дула пушки (точка вылета бомбы)
        double y=45;// для дула пушки (точка вылета бомбы)
        Point location;
        public double X 
        {
            get => x;
            set 
            { 
                x= value;
                OnPropertyChanged(nameof(X));
            }
        }
        public double Y 
        { 
            get => y;
            set
            {
                y= value;
                OnPropertyChanged(nameof(Y));
            }
        }
        public double InitialSpeed
        {
            get=> initalSpeed;            
            set
            {
                initalSpeed = value;
                OnPropertyChanged(nameof(InitialSpeed));
            }
        }
        public double Angle
        { 
            get=>angle;
            set
            { 
                angle = value;
                OnPropertyChanged(nameof(Angle));
            }
             }

        public Point Location 
        { 
            get => location; 
            set
            { 
                location = value; 
                OnPropertyChanged(nameof(Location)); 
            } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
