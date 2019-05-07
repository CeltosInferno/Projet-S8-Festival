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
using System.Windows.Shapes;

namespace WpfFestival.Views
{
    /// <summary>
    /// Identification.xaml 的交互逻辑
    /// </summary>
    public partial class Identification : UserControl
    {
        public static string password { get; set; }

        public Identification()
        {
            InitializeComponent();
           
        }

        private void lien(object sender, RoutedEventArgs e)
        {
            password = Mdp.Password;
        }

       
    }
}
