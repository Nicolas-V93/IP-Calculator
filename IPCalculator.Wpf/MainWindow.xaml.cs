using IPCalculator.Core;
using System;
using System.Windows;
using System.Windows.Controls;


namespace Ait.IPCalculator.Wpf
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillCmbBox();
        }

        private void btnConvert_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            try
            {
                if (cmbSubnet.SelectedItem == null)
                {
                    MessageBox.Show("Please select a subnetmask", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string ipv4Address = txtIP.Text;
                string subnetmask = cmbSubnet.SelectedItem.ToString();
                int index = subnetmask.IndexOf(" ");
                subnetmask = subnetmask.Substring(0, index);

                IPv4Calculator.ValidateIPAddress(ipv4Address);

                txtIPBit.Text = IPv4Calculator.ConvertAddressToBinary(ipv4Address);
                txtSubnetBit.Text = IPv4Calculator.ConvertAddressToBinary(subnetmask);

                IPv4Segment ipv4Segment = new IPv4Segment(txtIPBit.Text, txtSubnetBit.Text);

                GetNetworkAddress(ipv4Segment);
                GetBroadcastAddress(ipv4Segment);
                GetFirstHostAddress(ipv4Segment);
                GetLastHostAddress(ipv4Segment);
                GetGeneralInfo(ipv4Segment);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void ClearControls()
        {
            txtIPBit.Text = "";
            txtSubnetBit.Text = "";
            txtNetworkAddressBit.Text = "";
            txtNetworkAddressDD.Text = "";
            txtFirstHostAddressBit.Text = "";
            txtFirstHostAddressDD.Text = "";
            txtLastHostAddressBit.Text = "";
            txtLastHostAddressDD.Text = "";
            txtBroadcastAddressBit.Text = "";
            txtBroadcastAddressDD.Text = "";
            txtMaxNumberOfHosts.Text = "";
            txtNetworkClass.Text = "";
            txtNetworkType.Text = "";
        }

        private void cmbSubnet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
        }

        private void FillCmbBox()
        {
            SubnetService subnetService = new SubnetService();
            int cidr = 0;

            cmbSubnet.Items.Clear();
            foreach (string mask in subnetService.SubnetMasks)
            {

                cmbSubnet.Items.Add(mask + $" (/{cidr})");
                cidr++;
            }
        }

        private void GetNetworkAddress(IPv4Segment ipv4Segment)
        {
            txtNetworkAddressBit.Text = ipv4Segment.NetworkAddress;
            txtNetworkAddressDD.Text = IPv4Calculator.ConvertAddressToDecimal(txtNetworkAddressBit.Text);
        }

        private void GetBroadcastAddress(IPv4Segment ipv4Segment)
        {
            txtBroadcastAddressBit.Text = ipv4Segment.BroadcastAddress;
            txtBroadcastAddressDD.Text = IPv4Calculator.ConvertAddressToDecimal(txtBroadcastAddressBit.Text);
        }

        private void GetFirstHostAddress(IPv4Segment ipv4Segment)
        {
            txtFirstHostAddressBit.Text = ipv4Segment.FirstHostAddress;
            txtFirstHostAddressDD.Text = IPv4Calculator.ConvertAddressToDecimal(txtFirstHostAddressBit.Text);
        }

        private void GetLastHostAddress(IPv4Segment ipv4Segment)
        {
            txtLastHostAddressBit.Text = ipv4Segment.LastHostAddress;
            txtLastHostAddressDD.Text = IPv4Calculator.ConvertAddressToDecimal(txtLastHostAddressBit.Text);
        }

        private void GetGeneralInfo(IPv4Segment ipv4Segment)
        {

            txtNetworkClass.Text = ipv4Segment.NetworkClass;
            txtNetworkType.Text = ipv4Segment.NetworkType;

            txtMaxNumberOfHosts.Text = ipv4Segment.MaxHosts.ToString();

        }
    }
}
