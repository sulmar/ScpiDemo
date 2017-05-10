using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScpiDemo
{
    static class Commands
    {
        public const string Ping = "*IDN?";

        public const string GetVoltage = "MEAS:VOLT?";
    }
}
