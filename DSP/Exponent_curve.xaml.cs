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
    /// Логика взаимодействия для Exponent_curve.xaml
    /// </summary>
    public partial class Exponent_curve : Window
    {
        public double number_of_counts;
        public double signal_amp;
        public double width_curve;
        public double curve_freq;
        public double begin_phase;
        public Exponent_curve()
        {
            InitializeComponent();
            Random rnd = new Random();

        }

        public double power;
        public double cycle_value = 0;
        oscillogram Oscillogram = new oscillogram();
        private void exp_curve_modultaion(object sender, RoutedEventArgs e)
        {

            string cnt = counts.Text;
            string amp = amplitude.Text;
            string bg = beg_phase.Text;

            number_of_counts = Convert.ToDouble(cnt);
            signal_amp = Convert.ToDouble(amp);
            begin_phase = Convert.ToDouble(bg);
            Oscillogram.Title = "EXPONENT_ENVELOPE_WAVE_1";
            width_curve = number_of_counts / 5;
            curve_freq = (25 / Math.Pow(10, cnt.Length - 1));
            this.Close();
            Oscillogram.Show();


            List<double> modulation_values = new List<double>();
            for (int i = 0; i <= number_of_counts; i++)
            {
                power = -i / width_curve;
                cycle_value = signal_amp * Math.Exp(power) * Math.Cos(6.28*curve_freq*i + begin_phase);
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
                    LineSmoothness = 2,
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
