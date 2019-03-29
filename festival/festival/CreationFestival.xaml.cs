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

namespace festival
{
    /// <summary>
    /// Logique d'interaction pour CreationFestival.xaml
    /// </summary>
    public partial class CreationFestival : Window
    {
        public CreationFestival()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Festival festival = new Festival(TextBox1.Text, TextBox2.Text);
            using (IDalFestival dal = new DalFestival())
            {
                dal.CreateFestival(festival);
            }

            //MessageBox.Show(festival.ToString(), "validation", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
    }
}
