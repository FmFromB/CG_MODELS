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
    /// Логика взаимодействия для Tonal_Modulation.xaml
    /// </summary>
    public partial class Tonal_Modulation : Window
    {
        public double number_of_counts;
        public double signal_amp;
        public double h_freq;
        public double T_1;
        public double begin_phase;
        public double curve_freq;
        public double m;
        public double desc;
        public Tonal_Modulation()
        {
            InitializeComponent();
        }
        public double power;
        public double cycle_value = 0;
        oscillogram Oscillogram = new oscillogram();
        
        private void tonal_curve_modultaion(object sender, RoutedEventArgs e)
        {
            Oscillogram.Title = "TONAL_MODULATION_WAVE_1";
            string cnt = counts.Text;
            string amp = amplitude.Text;
            string TS = time_step.Text;
            string m_1 = modul.Text;
            string bg = beg_phase.Text;

            number_of_counts = Convert.ToDouble(cnt);
            signal_amp = Convert.ToDouble(amp);
            T_1 = Convert.ToDouble(TS);
            begin_phase = Convert.ToDouble(bg);
            m = Convert.ToDouble(m_1);
            desc = 1 / T_1;
            curve_freq = (desc / 20);
            h_freq = (desc / 400);
            this.Close();
            Oscillogram.Show();


            List<double> modulation_values = new List<double>();
            for (int i = 0; i <= number_of_counts; i++)
            {
                cycle_value = signal_amp * (1 + m * Math.Cos(2 * Math.PI * curve_freq * i)) * Math.Cos(2 * Math.PI * h_freq * i + begin_phase);
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
