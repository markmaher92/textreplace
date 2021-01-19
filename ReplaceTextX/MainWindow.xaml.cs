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
                Mapping.Add(new Tuple<string, string>(item.Split(':')[0].Trim(), item.Split(':')[1].Trim()));
            }
            List<string> MapNames = File.ReadLines(PathIFC.Text).ToList();
            List<string> NEwMapped = new List<string>();
            foreach (var item in File.ReadLines(PathIFC.Text))
            {
                NEwMapped.Add(item);
                foreach (var Map in Mapping)
                {
                    string NewTest = item;
                    if (item.Contains(@"\X2\00F6\X0\"))
                    {
                        NewTest = item.Replace(@"\X2\00F6\X0\","ö");
                    }
                    if (item.Contains(@"\X2\00FC\X0\"))
                    {
                        NewTest = item.Replace(@"\X2\00FC\X0\", "ü");
                    }
                    if (item.Contains(@"\X2\00E4\X0\"))
                    {
                        NewTest = item.Replace(@"\X2\00E4\X0\", "ä");
                    }
                    if (NewTest.Contains(Map.Item1))
                    {
                        var newstring = Map.Item2;
                        if (Map.Item2.ToLower().Contains(@"ö"))
                        {
                            newstring = newstring.Replace("ö",@"\X2\00F6\X0\");
                        }
                        if (Map.Item2.ToLower().Contains(@"ü"))
                        {
                            newstring = newstring.Replace("ü",@"\X2\00FC\X0\" );
                        }
                        if (Map.Item2.ToLower().Contains(@"ä"))
                        {
                            newstring = newstring.Replace("ä",@"\X2\00E4\X0\");
                        }
                        var MAppednewstring = NewTest.Replace(Map.Item1, newstring);
                        NEwMapped.RemoveAt(NEwMapped.Count -1);
                        NEwMapped.Add(MAppednewstring);
                    }
                }
            }
            File.WriteAllLines(PathIFC.Text.Replace(".ifc", "New.ifc"), NEwMapped);
        }
    }
}
