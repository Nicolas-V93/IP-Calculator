using System;
using System.Net;

namespace IPCalculator.Core
{
    public class IPv4Segment
    {
        public string Ipv4Address { get; }
        public string Subnetmask { get; }

        public string NetworkClass
        {
            get
            {
                string ipv4 = IPv4Calculator.ConvertAddressToDecimal(Ipv4Address);
                IPAddress ip = IPAddress.Parse(ipv4);

                byte firstOctet = ip.GetAddressBytes()[0];

                if (firstOctet >= 1 && firstOctet <= 127) return "Class A";
                else if (firstOctet >= 128 && firstOctet <= 191) return "Class B";
                else if (firstOctet >= 192 && firstOctet <= 223) return "Class C";
                else if (firstOctet >= 224 && firstOctet <= 239) return "Class D";
                else return "Class E";
            }
        }
        public string NetworkType
        {
            get
            {
                string ipv4 = IPv4Calculator.ConvertAddressToDecimal(Ipv4Address);
                IPAddress ip = IPAddress.Parse(ipv4);

                byte firstOctet = ip.GetAddressBytes()[0];
                byte secondOctet = ip.GetAddressBytes()[1];

                if (firstOctet == 10) return "Private";
                if (firstOctet == 172 && secondOctet >= 16 && secondOctet <= 31) return "Private";
                if (firstOctet == 192 && secondOctet == 168) return "Private";
                else return "Public";
            }
        }
        public string NetworkAddress { get; private set; }
        public string FirstHostAddress
        {
            get
            {
                string firstHost = NetworkAddress.Remove(NetworkAddress.Length - 1);
                firstHost = firstHost.Insert(NetworkAddress.Length - 1, '1'.ToString());
                return firstHost;
            }
        }
        public string BroadcastAddress { get; private set; }
        public string LastHostAddress
        {
            get
            {
                string lastHost = BroadcastAddress.Remove(BroadcastAddress.Length - 1);
                lastHost = lastHost.Insert(BroadcastAddress.Length - 1, '0'.ToString());
                return lastHost;
            }
        }
        public double MaxHosts
        {
            get
            {
                int amountOf1Bits = Subnetmask.Split('1').Length - 1;
                int cidr = 32 - amountOf1Bits;
                return Math.Pow(2, cidr) - 2;
            }
        }

        public IPv4Segment(string ipv4Address, string subnetmask)
        {
            Ipv4Address = ipv4Address;
            Subnetmask = subnetmask;
            CalculateNetworkAndBroadcastAddress(ipv4Address, subnetmask);
        }


        private void CalculateNetworkAndBroadcastAddress(string address, string subnetMask)
        {
            int cidr = subnetMask.Split('1').Length - 1;
            string unchanged = address.Substring(0, cidr);
            string partToChange = address.Substring(cidr);

            string networkAddress = partToChange.Replace('1', '0');
            string broadcastAddress = partToChange.Replace('0', '1');

            NetworkAddress = string.Concat(unchanged, networkAddress);
            BroadcastAddress = string.Concat(unchanged, broadcastAddress);
        }

    }
}
