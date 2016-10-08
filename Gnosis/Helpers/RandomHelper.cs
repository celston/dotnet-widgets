using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gnosis.Extensions;

namespace Gnosis.Helpers
{
    public class RandomHelper : Helper<RandomHelper>
    {
        private Random random = new Random();

        public short NextShort()
        {
            return random.NextShort();
        }

        public int NextInt()
        {
            return random.NextInt();
        }

        public long NextLong()
        {
            return random.NextLong();
        }

        public ushort NextUShort()
        {
            return random.NextUShort();
        }

        public uint NextUInt()
        {
            return random.NextUInt();
        }

        public ulong NextULong()
        {
            return random.NextULong();
        }

        public double NextDouble()
        {
            return random.NextDouble();
        }

        public double NextDouble(double min, double max)
        {
            return random.NextDouble(min, max);
        }

        public bool NextBoolean()
        {
            return random.NextBoolean();
        }

        public decimal NextDecimal()
        {
            return random.NextDecimal();
        }

        public DateTime NextDateTime()
        {
            return random.NextDateTime();
        }

        public DateTime NextDateTime(DateTime min, DateTime max)
        {
            return random.NextDateTime(min, max);
        }
    }
}
