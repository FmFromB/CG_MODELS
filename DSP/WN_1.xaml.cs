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
    /// Логика взаимодействия для WN_1.xaml
    /// </summary>
    public partial class WN_1 : Window
    {
        Random rnd = new Random();
        public double s = 0;
        public double e_1 = 0;
        public double num_of_counts = 0;
        public double cycle_value = 0;
        public double m = 0;
        public WN_1()
        {
            InitializeComponent();
        }
        oscillogram Oscillogram = new oscillogram();

        private void WN1_modultaion(object sender, RoutedEventArgs e)
        {
            Oscillogram.Title = "WHITE_NOISE_WAVE_1";
            string cnt = counts.Text;
            string start = a.Text;
            string end = b.Text;

            num_of_counts = Convert.ToDouble(cnt);
            s = Convert.ToInt32(start);
            e_1 = Convert.ToInt32(end);
            this.Close();
            Oscillogram.Show();


            List<double> modulation_values = new List<double>();
            for (int i = 0; i <= num_of_counts; i++)
            {
                m = rnd.NextDouble();
                cycle_value = s + (e_1 - s) * m;
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
                    LineSmoothness = 0,
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
