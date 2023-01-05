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
    public class Bomb : INotifyPropertyChanged
    {
        Point point;
        double t=0,v,a,g;
        public Bomb()
        {
            point = new Point(0,0);
            g = 9.8;
            v = 0;
            a = 0;
        }
        public Bomb(double v, double a, Point location, double g=9.8) 
        {
            this.v = v;
            this.a = a*Math.PI/180;
            this.g = g;
            point= location;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public Point Location 
        { 
            get => point; 
            set 
            {
                point = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        public double Angle { get => a; set => a = value * Math.PI / 180; }
        public double Velocity { get => v; set => v = value; }
        public double Gravity { get => g; set => g = value; }
        public double Time { get => t; set => t = value; }

        public void Move()
        {
            t += 0.10;
            point.X = (v * Math.Cos(a)) * t;
            point.Y = (v * Math.Sin(a)) * t - g * t * t / 2;
        }
    }
}
