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
    /// Логика взаимодействия для Linear.xaml
    /// </summary>
    public partial class Linear : Window
    {
        public double number_of_counts;
        public double signal_amp;
        public double begin_phase;
        public double end_freq;
        public double begin_freq;
        public double N_1;
        public double T_1;
        public double T_2;
        public double desc;
        public Linear()
        {
            InitializeComponent();
        }
        public double power;
        public double cycle_value = 0;
        oscillogram Oscillogram = new oscillogram();
        private void linear_curve_modultaion(object sender, RoutedEventArgs e)
        {
            string amp = amplitude.Text;
            string b_ph = beg_phase.Text;
            string e_frq = e_freq.Text;
            string b_frq = beg_freq.Text;
            string N = counts.Text;
            string T = step.Text;
            N_1 = Convert.ToDouble(N);
            T_1 = Convert.ToDouble(T);
            signal_amp = Convert.ToDouble(amp);
            begin_phase = Convert.ToDouble(b_ph);
            end_freq = Convert.ToDouble(e_frq);
            begin_freq = Convert.ToDouble(b_frq);
            number_of_counts = N_1;
            T_2 = N_1 * T_1;
            desc = 1 / T_1;
            Oscillogram.Title = "LINEAR_MODULATION_WAVE_1";
            this.Close();
            Oscillogram.Show();


            List<double> modulation_values = new List<double>();
            for (int i = 0; i <= number_of_counts; i++)
            { 
                cycle_value = signal_amp * Math.Cos(2*Math.PI * (begin_freq + ((end_freq - begin_freq)/T_2)*i) * i + begin_phase);
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
