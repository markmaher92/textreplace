using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ReplaceTextX
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog F = new OpenFileDialog();
            if (F.ShowDialog() == true)
            {
                PathMAP.Text = F.FileName;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog F = new OpenFileDialog();
            if (F.ShowDialog() == true)
            {
                PathIFC.Text = F.FileName;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<Tuple<string, string>> Mapping = new List<Tuple<string, string>>();
            foreach (var item in File.ReadLines(PathMAP.Text))
            {
               Mapping.Add(new Tuple<string,string>(item.Split(':')[0], item.Split(':')[1] ));
            }
            List<string> MapNames = File.ReadLines(PathIFC.Text).ToList();
            foreach (var item in File.ReadLines(PathIFC.Text))
            {
                foreach (var Map in Mapping)
                {
                    if (item.Contains(Map.Item1))
                    {
                        item.Replace(Map.Item1, Map.Item2);
                    }
                }
            }
            File.WriteAllLines(PathIFC.Text.Replace(".IFC", "New.IFC"), MapNames);
        }
    }
}
