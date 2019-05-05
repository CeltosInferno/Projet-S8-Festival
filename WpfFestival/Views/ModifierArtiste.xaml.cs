using Microsoft.Win32;
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

namespace WpfFestival.Views
{
    /// <summary>
    /// ModifierArtiste.xaml 的交互逻辑
    /// </summary>
    public partial class ModifierArtiste : UserControl
    {
        private string filePath;
        public ModifierArtiste()
        {
            InitializeComponent();
        }
        private void ChooseImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "images|*.jpg;*.png;*.jpeg|all|*.*";

            if (ofd.ShowDialog() == true)
            {
                filePath = ofd.FileName;
                image.Source = new BitmapImage(new Uri(filePath));
            }
        }
    }
}
