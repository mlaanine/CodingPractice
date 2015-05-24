using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;


namespace LocalSolarTimeWPF
{
    /// Practicing programming in C# and WPF
    /// Calculates correction for clock time to sundial time
    /// Mikko L 2014
    /// 
    public partial class MainWindow : Window
    {
        private Location location = new Location();
        private static DateTime now = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = location;
            location.Longitude = 24.92;  // Helsinki
            location.TimeZone = 2;

            dayOfYear.Text = now.DayOfYear.ToString();
            eqOfTime.Text = ComputeEqOfTime(now.DayOfYear).ToString("F1");
            if (now.IsDaylightSavingTime())
	        {
                isDst.Text = "is";
	        }
            else
            {
                isDst.Text = "not";
            }
        }

        private void enteredLongitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            double longitude;
            if (double.TryParse(enteredLongitude.Text, out longitude))
                location.Longitude = longitude;
            sundialOffset.Text = ComputeSundialOffset(location.Longitude, location.TimeZone).ToString("F1");
        }

        private void enteredTimeZone_TextChanged(object sender, TextChangedEventArgs e)
        {
            int timeZone;
            if (int.TryParse(enteredTimeZone.Text, out timeZone))
                location.TimeZone = timeZone;
            sundialOffset.Text = ComputeSundialOffset(location.Longitude, location.TimeZone).ToString("F1");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            location.Longitude = 24.92;
            location.TimeZone = 2;
            sundialOffset.Text = ComputeSundialOffset(location.Longitude, location.TimeZone).ToString("F1");
        }

        static double ComputeSundialOffset(double longitude, int timeZone)
        {
            double timeZoneCenter = timeZone * 15.0;
            double solarTimeOffset = 4 * (longitude - timeZoneCenter) + ComputeEqOfTime(now.DayOfYear);
            solarTimeOffset = now.IsDaylightSavingTime() ? solarTimeOffset - 60 : solarTimeOffset;
            return solarTimeOffset;
        }

        static double ComputeEqOfTime(int dayOfYear)
        {
            double B = (360.0 / 365) * (dayOfYear - 81);
            B = B * (Math.PI / 180);
            return 9.87 * Math.Sin(2 * B) - 7.53 * Math.Cos(B) - 1.5 * Math.Sin(B);
        }
    }

    public class Location : INotifyPropertyChanged
    {
        private double longitude;
        public double Longitude
        {
            get { return this.longitude; }
            set
            {
                if (this.longitude != value)
                {
                    this.longitude = value;
                    this.NotifyPropertyChanged("Longitude");
                }
            }
        }

        private int timeZone;
        public int TimeZone
        {
            get { return this.timeZone; }
            set
            {
                if (this.timeZone != value)
                {
                    this.timeZone = value;
                    this.NotifyPropertyChanged("TimeZone");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
