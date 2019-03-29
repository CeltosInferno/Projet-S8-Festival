using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Logique d'interaction pour BDD.xaml
    /// </summary>
    public partial class BDD : Window
    {
        public BDD()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //IDatabaseInitializer<BddContext> init = new CreateDatabaseIfNotExists<BddContext>();
            //IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            IDatabaseInitializer<BddContext> init = new DropCreateDatabaseIfModelChanges<BddContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new BddContext());
        }
    }
}
