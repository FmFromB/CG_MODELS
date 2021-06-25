using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
using LiveCharts.Wpf;

namespace DSP
{
    /// <summary>
    /// Логика взаимодействия для Exponent_enter.xaml
    /// </summary>
    public partial class Exponent_enter : Window
    {
        public double number_of_counts;
        public double a;
        public Exponent_enter()
        {
            InitializeComponent();
        }

        public double cycle_value = 0;
        oscillogram Oscillogram = new oscillogram();

        private void exponent_modultaion(object sender, RoutedEventArgs e)
        {
            //string str = square_value.Text;
            //string period = square_period_value.Text;
            //number = Convert.ToDouble(str);
            //L = Convert.ToDouble(period);

            string N = number.Text;
            string L = param.Text;

            number_of_counts = Convert.ToDouble(N);
            a = Convert.ToDouble(L);
            this.Close();
            Oscillogram = new oscillogram { Height = 300, Width = 250 };
            Oscillogram.Title = "EXPONENT_WAVE_1";
            Oscillogram.Show();

            List<double> modulation_values = new List<double>();
            for (int i = 0; i <= number_of_counts; i++)
            {
                cycle_value = Math.Pow(a, i);
                modulation_values.Add(cycle_value);
            }

            int k = 1;
            TextBlock name = new TextBlock();
            Grid.SetRow(name, k);
            CartesianChart ch = new CartesianChart();

            ch.Series = new SeriesCollection
            {
                new LineSeries
                {
                    LineSmoothness = 1,
                    StrokeThickness = 2,
                    DataLabels = false,
                    Fill = Brushes.Transparent,
                    PointGeometrySize = 0,
                    Values = new ChartValues<double>(modulation_values),
                }
            };


            RowDefinition rowDef = new RowDefinition();
            TextBlock text = new TextBlock();

            Oscillogram.oscil.RowDefinitions.Add(rowDef);
            Grid.SetRow(ch, k);

            Oscillogram.oscil.Children.Add(ch);

            Grid.SetRow(text, k);

            Oscillogram.oscil.Children.Add(text);
        }
    }
}
