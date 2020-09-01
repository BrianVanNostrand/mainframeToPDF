using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mf2pdfWpfFramework
{
    /// <summary>
    /// Interaction logic for templateDefaults.xaml
    /// </summary>
    public partial class templateDefaults : Window
    {
        public templateDefaults()
        {
            InitializeComponent();
            loadTemplateDefaults();
        }

        private void templateDefaultsCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void templateDefaultsSave_Click(object sender, RoutedEventArgs e)
        {
            templateDefaultsJson highwayLogDefaultsJson = new templateDefaultsJson()
            {
                creator = highwayLogCreator.Text,
                author = highwayLogAuthor.Text,
                indexTopTitle = highwayLogIndexTopTitle.Text,
                fontName = highwayLogFontName.Text,
                fontSize = float.Parse(highwayLogFontSize.Text),
                pageXinches = float.Parse(highwayLogPageXInches.Text),
                pageYinches = float.Parse(highwayLogPageYInches.Text),
                leftMargin = float.Parse(highwayLogLeftMargin.Text),
                topMargin = float.Parse(highwayLogTopMargin.Text),
                headerID = highwayLogHeaderID.Text,
                linesToID = float.Parse(highwayLogLinesToID.Text),
                offsetToID = float.Parse(highwayLogOffsetToID.Text),
                IDTextLength = float.Parse(highwayLogIDTextLength.Text),
                linesToAddID = float.Parse(highwayLogLinesToAddID.Text),
                offsetToAddID = float.Parse(highwayLogOffsetToAddID.Text),
                addIDLength = float.Parse(highwayLogAddIDLength.Text),
                linesToDetail = float.Parse(highwayLogLinesToDetail.Text),
                offsetToDetail = float.Parse(highwayLogOffsetToDetail.Text),
                detailTextLength = float.Parse(highwayLogDetailTextLength.Text)/**/
            };
            templateDefaultsJson locatorLogDefaultsJson = new templateDefaultsJson()
            {
                creator = locatorLogCreator.Text,
                author = locatorLogAuthor.Text,
                indexTopTitle = locatorLogIndexTopTitle.Text,
                fontName = locatorLogFontName.Text,
                fontSize = float.Parse(locatorLogFontSize.Text),
                pageXinches = float.Parse(locatorLogPageXInches.Text),
                pageYinches = float.Parse(locatorLogPageYInches.Text),
                leftMargin = float.Parse(locatorLogLeftMargin.Text),
                topMargin = float.Parse(locatorLogTopMargin.Text),
                headerID = locatorLogHeaderID.Text,
                linesToID = float.Parse(locatorLogLinesToID.Text),
                offsetToID = float.Parse(locatorLogOffsetToID.Text),
                IDTextLength = float.Parse(locatorLogIDTextLength.Text),
                linesToAddID = float.Parse(locatorLogLinesToAddID.Text),
                offsetToAddID = float.Parse(locatorLogOffsetToAddID.Text),
                addIDLength = float.Parse(locatorLogAddIDLength.Text),
                linesToDetail = float.Parse(locatorLogLinesToDetail.Text),
                offsetToDetail = float.Parse(locatorLogOffsetToDetail.Text),
                detailTextLength = float.Parse(locatorLogDetailTextLength.Text)/**/
            };
            JSONUpdateMethods.updateTemplateDefaults(highwayLogDefaultsJson, locatorLogDefaultsJson);
            this.Close();
        }
        private void loadTemplateDefaults()
        {
            string json = File.ReadAllText(@"./templateDefaults.json");
            JObject savedTemplateDefaults = JObject.Parse(json);
            highwayLogCreator.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["creator"];
            highwayLogAuthor.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["author"];
            highwayLogIndexTopTitle.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["indexTopTitle"];
            highwayLogFontName.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["fontName"];
            highwayLogFontSize.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["fontSize"];
            highwayLogPageXInches.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["pageXinches"];
            highwayLogPageYInches.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["pageYinches"];
            highwayLogLeftMargin.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["leftMargin"];
            highwayLogTopMargin.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["topMargin"];
            highwayLogHeaderID.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["headerID"];
            highwayLogLinesToID.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["linesToID"];
            highwayLogOffsetToID.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["offsetToID"];
            highwayLogIDTextLength.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["IDTextLength"];
            highwayLogLinesToAddID.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["linesToAddID"];
            highwayLogOffsetToAddID.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["offsetToAddID"];
            highwayLogAddIDLength.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["addIDLength"];
            highwayLogLinesToDetail.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["linesToDetail"];
            highwayLogOffsetToDetail.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["offsetToDetail"];
            highwayLogDetailTextLength.Text = (string)savedTemplateDefaults[$"stateHighwayLog"]["detailTextLength"];

            locatorLogCreator.Text = (string)savedTemplateDefaults[$"locatorLog"]["creator"];
            locatorLogAuthor.Text = (string)savedTemplateDefaults[$"locatorLog"]["author"];
            locatorLogIndexTopTitle.Text = (string)savedTemplateDefaults[$"locatorLog"]["indexTopTitle"];
            locatorLogFontName.Text = (string)savedTemplateDefaults[$"locatorLog"]["fontName"];
            locatorLogFontSize.Text = (string)savedTemplateDefaults[$"locatorLog"]["fontSize"];
            locatorLogPageXInches.Text = (string)savedTemplateDefaults[$"locatorLog"]["pageXinches"];
            locatorLogPageYInches.Text = (string)savedTemplateDefaults[$"locatorLog"]["pageYinches"];
            locatorLogLeftMargin.Text = (string)savedTemplateDefaults[$"locatorLog"]["leftMargin"];
            locatorLogTopMargin.Text = (string)savedTemplateDefaults[$"locatorLog"]["topMargin"];
            locatorLogHeaderID.Text = (string)savedTemplateDefaults[$"locatorLog"]["headerID"];
            locatorLogLinesToID.Text = (string)savedTemplateDefaults[$"locatorLog"]["linesToID"];
            locatorLogOffsetToID.Text = (string)savedTemplateDefaults[$"locatorLog"]["offsetToID"];
            locatorLogIDTextLength.Text = (string)savedTemplateDefaults[$"locatorLog"]["IDTextLength"];
            locatorLogLinesToAddID.Text = (string)savedTemplateDefaults[$"locatorLog"]["linesToAddID"];
            locatorLogOffsetToAddID.Text = (string)savedTemplateDefaults[$"locatorLog"]["offsetToAddID"];
            locatorLogAddIDLength.Text = (string)savedTemplateDefaults[$"locatorLog"]["addIDLength"];
            locatorLogLinesToDetail.Text = (string)savedTemplateDefaults[$"locatorLog"]["linesToDetail"];
            locatorLogOffsetToDetail.Text = (string)savedTemplateDefaults[$"locatorLog"]["offsetToDetail"];
            locatorLogDetailTextLength.Text = (string)savedTemplateDefaults[$"locatorLog"]["detailTextLength"];
        }

        private void restoreTemplateValues_Click(object sender, RoutedEventArgs e)
        {
            string originalDefaultsText = File.ReadAllText(@"./originalTemplateDefaults.json");
            File.WriteAllText(@".\templateDefaults.json", string.Empty);
            File.WriteAllText(@".\templateDefaults.json", originalDefaultsText);
            loadTemplateDefaults();

        }
    }
}
