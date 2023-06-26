using System.Collections;
using System.Collections.Generic;

namespace IPCalculator.Core
{
    public class SubnetService
    {
        private int totalSubnetMasks = 30;

        public List<string> SubnetMasks { get; } = new List<string>();

        public SubnetService()
        {
            GetAllSubnetMasks();
        }

        private void GetAllSubnetMasks()
        {
            for (int cidr = 0; cidr <= totalSubnetMasks; cidr++)
            {
                SubnetMask subnetMask = new SubnetMask();
                BitArray bitArrayMask = subnetMask.GenerateBinarySubnetMask(cidr);
                string binaryAddress = IPv4Calculator.ConvertBitArrayToString(bitArrayMask);
                string decimalMask = IPv4Calculator.ConvertAddressToDecimal(binaryAddress);

                SubnetMasks.Add(decimalMask);
            }
        }

    }
}