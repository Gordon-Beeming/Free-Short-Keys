using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Free_Short_Keys
{
    public class ShortKeyConfiguration
    {
        private static ShortKeyConfiguration instance;

        public ShortKeyConfiguration() { }

        public static ShortKeyConfiguration Instance
        {
            get
            {
                return instance;
            }
        }

        public List<ShortKey> ShortKeys { get; set; } = new List<ShortKey>();
        public string DefaultSuffix { get; set; } = "``";

        public static async Task Start()
        {
            if (instance == null)
            {
                instance = await Storage.ReadData<ShortKeyConfiguration>(null, "Configuration.json");
            }

            Keylogger kl = new Keylogger(null, false);
            kl.Enabled = true; // enable key logging
            kl.FlushInterval = 60000; // set buffer flush interval
            //kl.Flush2File(null, true); // force buffer flush
        }

        public static async Task Save()
        {
            await Storage.WriteData(null, "Configuration.json", Instance);
        }
    }
}
