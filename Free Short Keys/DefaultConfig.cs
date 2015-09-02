using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free_Short_Keys
{
    public class DefaultConfig
    {
        [DisplayName("Log Keys (Debug)")]
        public bool LogKeysDebug { get; set; } = false;
        [DisplayName("Suffix")]
        public string Suffix { get; set; } = "``";
    }
}
