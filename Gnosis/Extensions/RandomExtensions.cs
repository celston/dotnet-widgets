using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gnosis.Extensions
{
    public static class RandomExtensions
    {
        public static short NextShort(this Random rnd)
        {
            byte[] buffer = new byte[sizeof(Int16)];
            rnd.NextBytes(buffer);
            return BitConverter.ToInt16(buffer, 0);
        }

        public static int NextInt(this Random rnd)
        {
            byte[] buffer = new byte[sizeof(Int32)];
            rnd.NextBytes(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static long NextLong(this Random rnd)
        {
            byte[] buffer = new byte[sizeof(Int64)];
            rnd.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static ushort NextUShort(this Random rnd)
        {
            byte[] buffer = new byte[sizeof(UInt16)];
            rnd.NextBytes(buffer);
            return BitConverter.ToUInt16(buffer, 0);
        }

        public static uint NextUInt(this Random rnd)
        {
            byte[] buffer = new byte[sizeof(UInt32)];
            rnd.NextBytes(buffer);
            return BitConverter.ToUInt32(buffer, 0);
        }

        public static ulong NextULong(this Random rnd)
        {
            byte[] buffer = new byte[sizeof(UInt64)];
            rnd.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0);
        }

        public static double NextDouble(this Random rnd)
        {
            byte[] buffer = new byte[sizeof(double)];
            rnd.NextBytes(buffer);
            return BitConverter.ToDouble(buffer, 0);
        }

        public static double NextDouble(this Random rnd, double min, double max)
        {
            return min + rnd.NextDouble() * max;
        }

        public static decimal NextDecimal(this Random rnd)
        {
            return (decimal)rnd.NextDouble();
        }

        public static bool NextBoolean(this Random rnd)
        {
            return rnd.NextDouble() > 0.5;
        }

        public static DateTime NextDateTime(this Random rnd)
        {
            return rnd.NextDateTime(DateTime.MinValue, DateTime.MaxValue);
        }

        public static DateTime NextDateTime(this Random rnd, DateTime min, DateTime max)
        {
            TimeSpan ts = max - min;
            double seconds = rnd.NextDouble(0, ts.TotalSeconds);

            return min.AddSeconds(seconds);
        }
    }
}
