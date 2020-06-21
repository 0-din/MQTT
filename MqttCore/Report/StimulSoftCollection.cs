using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MqttCore.Report
{
    [ConfigurationCollection(typeof(StimulSoftElement), AddItemName = "report")]
    class StimulSoftCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new StimulSoftElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return element;
        }
    }
}