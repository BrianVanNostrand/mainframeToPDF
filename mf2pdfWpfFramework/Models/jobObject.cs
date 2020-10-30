using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mf2pdfWpfFramework
{
    public class jobObject
    {
        public string ReportTitle;
        public string ReportType;
        public string ReadDirection;
        public string InputFile;
        public string OutputFile;
        public jobObject(string reportTitle, string reportType, string readDirection, string inputFile, string outputFile)
        {
            this.ReportTitle = reportTitle;
            this.ReportType = reportType;
            this.ReadDirection = readDirection;
            this.InputFile = inputFile;
            this.OutputFile = outputFile;
        }
    }
}