using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace MqttCore.Report
{
    class StimulSoftSetting : ConfigurationSection
    {
        private List<StimulSoftElement> _lstReports;
        private static StimulSoftSetting _instance;
        private StimulSoftCollection _st;

        public StimulSoftElement this[string name]
        {
            get
            {
                StimulSoftElement element = this.ListReports.Where(r => r.Name == name).FirstOrDefault();
                if (element == null)
                    throw new Exception("Cannot find report.");
                return element;
            }
        }

        public static StimulSoftSetting Instance
        {
            get
            {
                if (_instance == null)
                    _instance = (StimulSoftSetting)ConfigurationManager.GetSection("StimulSoftReports");
                return _instance;
            }
        }

        [ConfigurationProperty("reports")]
        public StimulSoftCollection Reports
        {
            get
            {
                if (_st == null)
                    _st = (StimulSoftCollection)base["reports"];
                return _st;
            }
        }

        public List<StimulSoftElement> ListReports
        {
            get
            {
                if (_lstReports == null)
                    _lstReports = new List<StimulSoftElement>();

                foreach (StimulSoftElement element in Reports)
                    this._lstReports.Add(element);

                return _lstReports;
            }
        }
    }
}