using System;
using System.Collections;
using System.Net;

namespace IPCalculator.Core
{
    public static class IPv4Calculator
    {
        public static void ValidateIPAddress(string address)
        {
            bool isValid;

            if (address.StartsWith("0")) throw new Exception("IP Address can not start with '0' ");
            else
            {
                IPAddress ip;
                isValid = IPAddress.TryParse(address, out ip);

                if (!isValid) throw new Exception("Please enter a valid IP address");

            }
        }

        public static BitArray ConvertByteToBitArray(byte b)
        {
            BitArray bitArray = new BitArray(new byte[] { b });
            return bitArray;
        }

        public static string ConvertBitArrayToString(BitArray bitArray)
        {
            string result = "";

            for (int i = bitArray.Count - 1; i >= 0; i--)
            {
                result += bitArray[i] ? '1' : '0';
            }

            return result;
        }

        public static string ConvertAddressToBinary(string decimalAddress)
        {

            string binaryAddress = null;
            IPAddress ip = IPAddress.Parse(decimalAddress);

            foreach (byte b in ip.GetAddressBytes())
            {
                BitArray octet = ConvertByteToBitArray(b);
                binaryAddress += ConvertBitArrayToString(octet);
            }

            return binaryAddress;
        }

        public static string ConvertAddressToDecimal(string binaryAddress)
        {

            string Octet1 = binaryAddress.Substring(0, 8);
            string Octet2 = binaryAddress.Substring(8, 8);
            string Octet3 = binaryAddress.Substring(16, 8);
            string Octet4 = binaryAddress.Substring(24, 8);

            return $"{Convert.ToInt32(Octet1, 2)}." +
                     $"{Convert.ToInt32(Octet2, 2)}." +
                     $"{Convert.ToInt32(Octet3, 2)}." +
                     $"{Convert.ToInt32(Octet4, 2)}";
        }



    }
}
