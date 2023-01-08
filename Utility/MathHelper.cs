using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class MathHelper
    {
        public static double Get2Dp(double number)
        {
            return Math.Round(number * 100) / (double)100;
        }
    }
}
