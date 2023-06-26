using System.Collections;


namespace IPCalculator.Core
{
    class SubnetMask
    {

        public BitArray GenerateBinarySubnetMask(int cidr)
        {

            int length = 32;
            BitArray mask = new BitArray(32);

            for (int i = 0; i < cidr; i++)
            {
                mask[--length] = true;
            }

            return mask;

        }


    }

}
