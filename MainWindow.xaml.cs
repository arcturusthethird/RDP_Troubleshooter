using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace RDP_Troubleshooter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void sonicwallTestButton_Click(object sender, RoutedEventArgs e)
        {
            var pingSender = new Ping();
            var reply = await pingSender.SendPingAsync("kkbcpa.local");
            if(reply != null)
            {
                if(reply.Status == 0)
                {
                    statusText.Text =
                        "Sonicwall is on and activated. If you are getting \"cannot reach computer\" error, proceed to potential fixes below.";
                }
                else
                {
                    statusText.Text =
                        "Sonicwall may not be connected. Please verify and try again.";
                }
            }
            else
            {
                Console.WriteLine("Unexpected null");
            }
            //sonicwallTestStatus.Text = "Test Output";
        }

        private void potentialFixOneButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            string strCmdText;
            strCmdText = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C copy /b Image1.jpg + Archive.rar Image2.jpg";
            process.StartInfo = startInfo;
            process.Start();
            */
            string strCmdText;
            strCmdText = "/C ipconfig /flushdns";
            System.Diagnostics.Process.Start("CMD.exe", strCmdText);
            statusText.Text = "Potential Fix One Completed. Try connecting again.";
        }

        private void potentialFixTwoButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = MessageBox.Show("Will disconnect internet for 2 to 3 minutes along with Sonicwall. Continue?", "Confirmation", MessageBoxButton.YesNo);
            if (dr == MessageBoxResult.Yes)
            {
                string strCmdText;
                strCmdText = "/C ipconfig /release";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                strCmdText = "/C ipconfig /renew";
                System.Diagnostics.Process.Start("CMD.exe", strCmdText);
                statusText.Text = "Potential Fix Two Completed. Try connecting again in a couple minutes. Remember to reconnect Sonicwall. If still no connection, send message to IT";
            }
            else
            {
                statusText.Text = "Potential Fix Two Cancelled.";
            }
        }
    }
}
