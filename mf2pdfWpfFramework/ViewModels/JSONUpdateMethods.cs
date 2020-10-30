using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;

namespace mf2pdfWpfFramework
{
    class JSONUpdateMethods
    {
        //save jobs to json for loading when application is opened.
        public static void updateJobs(ListBox jobsItemControl)
        {
            File.WriteAllText(@".\Jobs.json", "{}");//clear json
            JObject jobsJSON = new JObject();
            string jobsJSONText = "";
            jobObject jobDefinition;
            if (jobsItemControl.Items.Count == 0)
            {
                jobsJSONText = "{}";
            }
            else
            {
                for (int i=0;i< jobsItemControl.Items.Count; i += 1)
                {
                    string reportTitle = ((TextBox)LogicalTreeHelper.FindLogicalNode((jobUserControl)jobsItemControl.Items[i], "reportTitleTextbox")).Text;
                    string inputFile = ((TextBox)LogicalTreeHelper.FindLogicalNode((jobUserControl)jobsItemControl.Items[i], "inputFileTextBox")).Text;
                    string outputFile = ((TextBox)LogicalTreeHelper.FindLogicalNode((jobUserControl)jobsItemControl.Items[i], "outputFileTextBox")).Text;
                    string reportType = "";

                    if(((RadioButton)LogicalTreeHelper.FindLogicalNode((jobUserControl)jobsItemControl.Items[i], "highwayLogRadio")).IsChecked == true)
                    {
                        reportType = "stateHighwayLog";
                    }
                    else
                    {
                        reportType = "locatorLog";
                    }
                    string readDirection = "";
                    if(((RadioButton)LogicalTreeHelper.FindLogicalNode((jobUserControl)jobsItemControl.Items[i], "topBottomRadio")).IsChecked == true)
                    {
                        readDirection = "topToBottom";
                    }
                    else 
                    {
                        readDirection = "bottomToTop";
                    }
                    jobDefinition = new jobObject(reportTitle, reportType, readDirection, inputFile, outputFile);// new instance of jobObject class
                    if (i == 0)
                    {
                        if (jobsItemControl.Items.Count == 1)
                        {
                            jobsJSONText = $@"{{""job_{i}"":{JsonConvert.SerializeObject(jobDefinition)}}}";
                        }
                        else
                        {
                            jobsJSONText = $@"{{""job_{i}"":{JsonConvert.SerializeObject(jobDefinition)}";
                        }
                    }
                    else
                    {
                        jobsJSONText = jobsJSONText + $@", ""job_{i}"":"+JsonConvert.SerializeObject(jobDefinition);
                    }
                }
                if(jobsItemControl.Items.Count > 1)
                {
                    jobsJSONText = jobsJSONText + "}";
                }
                
            }
            
            File.WriteAllText(@".\Jobs.json", jobsJSONText);
        }
        public static void updateTemplateDefaults(templateDefaultsJson highwayLogDefaultsJson, templateDefaultsJson locatorLogDefaultsJson)
        {
            File.WriteAllText(@".\templateDefaults.json", "{}");
            string templatesText = $@"{{""stateHighwayLog"":{ JsonConvert.SerializeObject(highwayLogDefaultsJson)},""locatorLog"":{ JsonConvert.SerializeObject(locatorLogDefaultsJson)}}}";
            File.WriteAllText(@".\templateDefaults.json", templatesText);
        }
    }
}
