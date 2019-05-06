using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// ModifierProgrammation.xaml 的交互逻辑
    /// </summary>
    public partial class ModifierProgrammation : UserControl
    {
        public ModifierProgrammation()
        {
            InitializeComponent();
        }

        //private void ProgrammationsList_Loaded(object sender, RoutedEventArgs e)
        //{

        //}

        private void Bouton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 50000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("festivalprojets8equipe4@gmail.com", "festivalequipe4");


                MailMessage msg = new MailMessage();
                msg.To.Add("daoudi.m22@laposte.net");
                msg.From = new MailAddress("festivalprojets8equipe4@gmail.com");
                msg.Subject = "INFOS: Modification du festival";
                msg.Body = "Bonjour," +
                    "Nous vous informons que des modifications ont été apportées sur le festival sur lequel vous vous êtes inscrit." +
                    "Veuillez vous connecter à votre compte pour voir ces modifications." +
                    "L'équipe organisatrice du festival vous remercie pour votre compréhension." +
                    "Codialement," +
                    "L'équipe.";
                client.Send(msg);
                MessageBox.Show("Les mails ont été envoyés avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
