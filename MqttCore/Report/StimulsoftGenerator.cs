using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Dictionary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MqttCore.Report
{
    public class StimulsoftGenerator : IDisposable
    {
        public string Path
        {
            get
            {
                return "Broker.mrt";
            }
        }

        public string Name
        {
            get;
        }

        public StiReport Report
        {
            get;
            private set;
        }

        public StimulsoftGenerator(string name)
        {
            Name = name;
            //StiElement = StimulSoftSetting.Instance[name];
            //Stimulsoft.Base.StiLicense.Key = StiElement.Licence;
            Report = new StiReport();
        }

        public void RegData(string name, System.Data.DataTable dataTable)
        {
            Report.RegData(name, dataTable);
            Report.Dictionary.Synchronize();
        }

        public void Compile()
        {
            Report.Compile();
        }

        public void Render()
        {
            Report.Render();
        }

        public void LoadReport()
        {
            if (!System.IO.File.Exists(Path))
                throw new Exception($"Couldn't find {Name}.");

            using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Report.Load(fs);
            }
        }

        public void AddBusinessObject(string category, string name, object value)
        {
            Report.RegBusinessObject(category, name, value);
        }

        public void AddBusinussObjects(Dictionary<string, object> objects)
        {
            foreach (var obj in objects)
                Report.RegBusinessObject(obj.Key, obj.Key, obj.Value);
        }

        public void AddVariable(string name, object value)
        {
            Report[name] = value;
        }

        public void AddVariables(Dictionary<string, object> variables)
        {
            foreach (var obj in variables)
                Report[obj.Key] = obj.Value;
        }

        public void AddDataSources(StiDataSource dataSource)
        {
            Report.DataSources.Add(dataSource);
        }

        public MemoryStream ExportDocument(StiExportFormat format)
        {
            MemoryStream stream = new MemoryStream();
            Report.ExportDocument(format, stream);
            return stream;
        }

        public MemoryStream LoadDocument()
        {
            MemoryStream stream = new MemoryStream();
            Report.LoadDocument(stream);
            return stream;
        }

        public void AddStiHeaderBand(StiHeaderBand stiHeaderBand)
        {
            AddComponent(stiHeaderBand);
        }

        public void AddStiDataBand(StiDataBand stiDataBand)
        {
            AddComponent(stiDataBand);
        }

        public void AddStiDataBand(IEnumerable<StiDataBand> stiDataBands)
        {
            foreach (StiDataBand d in stiDataBands)
                AddComponent(d);
        }

        public void AddComponent(StiComponent component, int pageIndex = 0)
        {
            Report.Pages[pageIndex].Components.Add(component);
        }

        public StiHeaderBand FindHeaderBand(string name, int pageIndex = 0)
        {
            return (StiHeaderBand)Report.GetComponentByName(name);
        }

        public void Dispose()
        {
            Report.Dispose();
        }
    }
}
