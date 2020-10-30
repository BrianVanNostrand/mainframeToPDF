using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace mf2pdfWpfFramework
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int jobsCount = 0;
        public MainWindow()
        {
            InitializeComponent();
            loadState(@"./Jobs.json");
        }
        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            JSONUpdateMethods.updateJobs(jobsItemsControl);
        }
        private void addJobbutton_Click(object sender, RoutedEventArgs e)
        {
            jobsCount += 1;
            jobUserControl newJob = new jobUserControl();
            newJob.jobNumber = jobsCount;
            newJob.Name = $"Job{newJob.jobNumber}";
            jobsItemsControl.Items.Add(newJob);
        }
        private void loadSampleButton_Click(object sender, RoutedEventArgs e)
        {
            loadState(@"./sampleJobs.json");
        }
        private void changeTemplateDefaultsButton_Click(object sender, RoutedEventArgs e)
        {
            templateDefaults changeTemplateDefaultsWindow = new templateDefaults();
            changeTemplateDefaultsWindow.Show();
        }
        private void runButton_Click(object sender, RoutedEventArgs e)
        {
            JSONUpdateMethods.updateJobs(jobsItemsControl);
            string jobsRead = File.ReadAllText(@"./Jobs.json");
            JObject jobsJSON;//jobsJson Json Object
            jobsJSON = JObject.Parse(jobsRead);
            spirePDF.getPDFParameters(jobsJSON, jobsItemsControl, progressBar);
        }
        public void deleteJob(jobUserControl job)
        {
            object jobToRemove = new object();
            foreach (jobUserControl item in jobsItemsControl.Items)
            {
                if (item == job)
                {
                    jobToRemove = item;
                }
            }
            jobsItemsControl.Items.Remove(jobToRemove);
        }
        private void clearJobsButton_Click(object sender, RoutedEventArgs e)
        {
           if(jobsItemsControl.Items.Count>0)
            {
                jobsItemsControl.Items.Clear();
            }
        }
        private void loadState(string source)
        {
            string json = File.ReadAllText(source);
            JObject lastStateJson;
            try { 
                lastStateJson = JObject.Parse(json);
                for (int i = 0; i < lastStateJson.Count; i += 1)
                {
                    jobUserControl jobToAdd = new jobUserControl();
                    TextBox reportTitlebox = (TextBox)jobToAdd.FindName("reportTitleTextbox");
                    TextBox inputFileTextBox = (TextBox)jobToAdd.FindName("inputFileTextBox");
                    TextBox outputFileTextBox = (TextBox)jobToAdd.FindName("outputFileTextBox");
                    RadioButton highwayLogRadio = (RadioButton)jobToAdd.FindName("highwayLogRadio");
                    RadioButton locatorLogRadio = (RadioButton)jobToAdd.FindName("locatorLogRadio");
                    RadioButton topBottomRadio = (RadioButton)jobToAdd.FindName("topBottomRadio");
                    RadioButton bottomTopRadio = (RadioButton)jobToAdd.FindName("bottomTopRadio");
                    reportTitlebox.Text = (string)lastStateJson[$"job_{i}"]["reportTitle"];
                    inputFileTextBox.Text = (string)lastStateJson[$"job_{i}"]["inputFile"];
                    outputFileTextBox.Text = (string)lastStateJson[$"job_{i}"]["outputFile"];
                    if ((string)lastStateJson[$"job_{i}"]["readDirection"] == "topToBottom")
                    {
                        topBottomRadio.IsChecked = true;
                        bottomTopRadio.IsChecked = false;
                    }
                    else
                    {
                        topBottomRadio.IsChecked = false;
                        bottomTopRadio.IsChecked = true;
                    }
                    if ((string)lastStateJson[$"job_{i}"]["reportType"] == "stateHighwayLog")
                    {
                        highwayLogRadio.IsChecked = true;
                        locatorLogRadio.IsChecked = false;
                    }
                    else
                    {
                        highwayLogRadio.IsChecked = false;
                        locatorLogRadio.IsChecked = true;
                    }
                    jobsItemsControl.Items.Add(jobToAdd);
                }
            }
            catch {
                ItemCollection items = jobsItemsControl.Items;
            }
            
        }
    }
}
