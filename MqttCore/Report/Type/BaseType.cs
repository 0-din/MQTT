using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttCore.Report.Type
{
    public interface IData{
        string Type { get; set; }

        string Name { get; set; }

        string IP { get; set; }

        int Time { get; set; }

        object Value { get; set; }
    }
}
