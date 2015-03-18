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

namespace LocalSolarTimeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static DateTime now = DateTime.Now;

        public MainWindow()
        {
            InitializeComponent();
            
            dayOfYear.Text = now.DayOfYear.ToString();
            eqOfTime.Text = ComputeEqOfTime(now.DayOfYear).ToString("F1");

            int timeZone = Convert.ToInt32(enteredTimeZone.Text);
            double longitude = Convert.ToDouble(enteredLongitude.Text);

            sundialOffset.Text = ComputeSundialOffset(longitude, timeZone).ToString("F1");

            if (now.IsDaylightSavingTime())
	        {
                isDst.Text = "is";
	        }
            else
            {
                isDst.Text = "not";
            }
        }

        static double ComputeSundialOffset(double longitude, int timeZone)
        {
            double timeZoneCenter = timeZone * 15.0;
            double solarTimeOffset = 4 * (longitude - timeZoneCenter) + ComputeEqOfTime(now.DayOfYear);
            solarTimeOffset = now.IsDaylightSavingTime() ? solarTimeOffset + 60 : solarTimeOffset;
            return solarTimeOffset;
        }

        static double ComputeEqOfTime(int dayOfYear)
        {
            double B = (360.0 / 365) * (dayOfYear - 81);
            B = B * (Math.PI / 180);
            return 9.87 * Math.Sin(2 * B) - 7.53 * Math.Cos(B) - 1.5 * Math.Sin(B);
        }

        private void enteredLongitude_TextChanged(object sender, TextChangedEventArgs e)
        {
            double longitude;
            int timeZone = 2;
            if (double.TryParse(enteredLongitude.Text, out longitude))
                sundialOffset.Text = ComputeSundialOffset(longitude, timeZone).ToString("F1");    
        }

        private void enteredTimeZone_TextChanged(object sender, TextChangedEventArgs e)
        {
            int timeZone; 
            double longitude = 24.92;
            if (int.TryParse(enteredTimeZone.Text, out timeZone))
                sundialOffset.Text = ComputeSundialOffset(longitude, timeZone).ToString("F1");    
        }
    }
}
