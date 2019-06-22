using System.Net;
using System.Windows;

namespace AddressDefinitionApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void VerifyButonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(inputFieldText.Text))
                    MessageBox.Show("Не введено ни какое значение");
                else
                {
                    var host = await Dns.GetHostEntryAsync(inputFieldText.Text);
                    IPAddress ipAddr;

                    addressList.Items.Clear();

                    if (IPAddress.TryParse(inputFieldText.Text, out ipAddr))
                        addressList.Items.Add(host.HostName);

                    else
                        foreach (var ip in host.AddressList)
                            addressList.Items.Add(ip.ToString());

                    infoPanel.Visibility = Visibility.Visible;
                }
            }
            catch (System.ArgumentException exception)
            {
                MessageBox.Show(exception.Message);
            }
            catch (System.Net.Sockets.SocketException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ExitButon_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
