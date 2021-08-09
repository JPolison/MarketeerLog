using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketeerLog
{
    [Serializable]
    public class ConfigData
    {
        [field: NonSerialized()]
        public event EventHandler<EventArgs> ConfigDataChanged;


        private string _lastFileLocation;


        public string LastFileLocation
        {
            get => _lastFileLocation;
            set 
            {
                _lastFileLocation = value;
                ConfigDataChanged?.Invoke(this, new EventArgs());
            }


        }
    }
}
