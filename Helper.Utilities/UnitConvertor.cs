using System;
namespace Helper.Utilities
{
    public static class UnitConvertor
    {
        static string checkIfValueIsDecimal(string value)
        {
            string result = null;
            if (value.Contains(","))
            {
                result = value.ToType<double>().ToString("###.##");
            }
            else
            {
                result = value.ToType<double>().ToString("###.00");
            }
            return result;
        }
        public static string roundObjectSize(string ObjectSize)
        {
            int oneByte = 1;
            int kiloByte = 1024;
            int megaByte = 1048576;
            int gigaByte = 1073741824;
            long terraByte = 1099511627776L;
            long pettaByte = 1125899906842624L;
            if (ObjectSize.ToType<Int64>().IsBetween(0, kiloByte - 1))
            {
                if ((Convert.ToDouble(checkIfValueIsDecimal(Convert.ToString(Convert.ToDecimal(ObjectSize) / oneByte))) >= 1000) == false)
                {
                    ObjectSize = Convert.ToString(Convert.ToInt32(ObjectSize) / 1) + " Bytes";
                }
                else
                {
                    ObjectSize = "1,00 KB";
                }
            }
            else if (ObjectSize.ToType<Int64>().IsBetween(0, megaByte - 1))
            {
                if ((Convert.ToDouble(checkIfValueIsDecimal(Convert.ToString(Convert.ToDecimal(ObjectSize) / kiloByte))) >= 1000) == false)
                {
                    ObjectSize = checkIfValueIsDecimal(Convert.ToString(Convert.ToDecimal(ObjectSize) / kiloByte)) + " KB";
                }
                else
                {
                    ObjectSize = "1,00 MB";
                }
            }
            else if (ObjectSize.ToType<Int64>().IsBetween(0, gigaByte - 1))
            {
                if ((Convert.ToDouble(checkIfValueIsDecimal(Convert.ToString(Convert.ToDecimal(ObjectSize) / megaByte))) >= 1000) == false)
                {
                    ObjectSize = checkIfValueIsDecimal(Convert.ToString(Convert.ToDecimal(ObjectSize) / megaByte)) + " MB";
                }
                else
                {
                    ObjectSize = "1,00 GB";
                }
            }
            else if (ObjectSize.ToType<Int64>().IsBetween(0, terraByte - 1))
            {
                if ((Convert.ToDouble(checkIfValueIsDecimal(Convert.ToString(Convert.ToDecimal(ObjectSize) / gigaByte))) >= 1000) == false)
                {
                    ObjectSize = checkIfValueIsDecimal(Convert.ToString(Convert.ToDecimal(ObjectSize) / gigaByte)) + " GB";
                }
                else
                {
                    ObjectSize = "1,00 TB";
                }
            }
            else if (ObjectSize.ToType<Int64>().IsBetween(0, pettaByte - 1))
            {
                if ((Convert.ToDouble(checkIfValueIsDecimal(Convert.ToString(Convert.ToDecimal(ObjectSize) / terraByte))) >= 1000) == false)
                {
                    ObjectSize = checkIfValueIsDecimal(Convert.ToString(Convert.ToDecimal(ObjectSize) / terraByte)) + " TB";
                }
                else
                {
                    ObjectSize = "1,00 PB";
                }
            }
            return ObjectSize;
        }
    }
}
