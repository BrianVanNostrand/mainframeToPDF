using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spire.Pdf;
using Spire.Pdf.Actions;
using Spire.Pdf.Bookmarks;
using Spire.Pdf.General;
using Spire.Pdf.General.Find;
using Spire.Pdf.Graphics;
using Spire.Pdf.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FontStyle = System.Drawing.FontStyle;

namespace mf2pdfWpfFramework
{
    class spirePDF
    {
        static class Globals
        {
            public static int currentIDBookmarkIndex;
        }
        static public void getPDFParameters(JObject jobsJSON, ListBox jobsItemsControl)
        {
            string templateDefaultsRead = File.ReadAllText(@"./templateDefaults.json");
            JObject templateDefaultsJSON = JObject.Parse(templateDefaultsRead);
            for (int j = 0; j < jobsJSON.Count; j += 1)
            {
                string readDirection = (string)jobsJSON[$"job_{j}"]["readDirection"];
                string reportType = (string)jobsJSON[$"job_{j}"]["reportType"];
                JObject templateDefaultSet = (JObject)templateDefaultsJSON[reportType];
                var items = jobsItemsControl.Items[j];
                generatePDF((JObject)jobsJSON[$"job_{j}"], templateDefaultSet, j, jobsItemsControl);
               
            }
        }
        static public void setLoadStatus(string prompt, ListBox jobsItemsControl, int jobIndex)
        {
            if(prompt == "run")
            {
                var waitingBrush = new System.Windows.Media.ImageBrush(new BitmapImage(new Uri("./ellipses.png", UriKind.Relative)));
                foreach (jobUserControl job in jobsItemsControl.Items)
                {
                    
                }
            }
            if(prompt == "processing")
            {
                
                
            }
        }
        static public void generatePDF(JObject jobJSON, JObject templateDefaultSet, int j, ListBox jobsItemsControl)
        {
            #region variable declarations
            //line number within the current page, not within the broader set of lines in the file
            int fileLineNum = 0; //i or file lines count -1 -i, iterated value index through list of all lines in the document
            int fontSize = (int)templateDefaultSet["fontSize"];
            int pageLineNum = 0;
            int pageNumber = 1;
            string ID1 = "";
            string Detail;
            string[] fileLines = new string[] { "" }; //array of all of the lines red in the text file, in the order that they were read, top to bottom
            bool headerIDFound = false;
            var passBrush = new System.Windows.Media.ImageBrush(new BitmapImage(new Uri("./images/checkMark.png", UriKind.Relative)));
            var failBrush = new System.Windows.Media.ImageBrush(new BitmapImage(new Uri("./images/exclamationPoint.png", UriKind.Relative)));
            List<string> pageLines = new List<string>();//aray of lines in the current page
            jobUserControl job = (jobUserControl)jobsItemsControl.Items[j];
            FontFamily fontName = new System.Drawing.FontFamily((string)templateDefaultSet["fontName"]);
            PdfDocument document = new PdfDocument();
            PdfBookmarkCollection bookmarks = document.Bookmarks;
            Font font = new Font(fontName, fontSize, FontStyle.Regular);
            PdfTrueTypeFont trueTypeFont = new PdfTrueTypeFont(font);
            #endregion
            #region check form inputs
            if (job.reportTitleTextbox.Text == "")
            {
                job.pdfCreateResponseButton.ToolTip = "Processing Failed. Please provide a report title.";
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            if (job.inputFileTextBox.Text == "")
            {
                job.pdfCreateResponseButton.ToolTip = "Processing Failed. No input file given.";
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            if (job.inputFileTextBox.Text.Substring(job.inputFileTextBox.Text.Length - 4, 4) != ".txt")
            {
                job.pdfCreateResponseButton.ToolTip = "Processing Failed. Input must be a text (.txt) file.";
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            if (job.outputFileTextBox.Text == "")
            {
                job.pdfCreateResponseButton.ToolTip = "Processing Failed. No output file given.";
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            if (job.outputFileTextBox.Text.Substring(job.outputFileTextBox.Text.Length - 4, 4) != ".pdf")
            {
                job.pdfCreateResponseButton.ToolTip = "Processing Failed. Output must be a PDF (.pdf) file.";
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            // set report metadata
            try
            {
                document.PageSettings.Height = (int)templateDefaultSet["pageYinches"] * 72;
            }
            catch
            {
                job.pdfCreateResponseButton.ToolTip = $"Processing failed. Please check the 'pageYInches' parameter in the default values for ${(string)jobJSON["reportType"]} reports. See readme document for more information and parameter definitions.";
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            try
            {
                document.PageSettings.Width = (int)templateDefaultSet["pageXinches"] * 72;
            }
            catch
            {
                job.pdfCreateResponseButton.ToolTip = $"Processing failed. Please check the 'pageXinches' parameter in the default values for ${(string)jobJSON["reportType"]} reports. See readme document for more information and parameter definitions.";
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            try
            {
                document.DocumentInformation.Author = (string)templateDefaultSet["author"];
            }
            catch
            {
                job.pdfCreateResponseButton.ToolTip = $"Processing failed. Please check the 'author' parameter in the default values for ${(string)jobJSON["reportType"]} reports. See readme document for more information and parameter definitions."; ;
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            try
            {
                document.DocumentInformation.Title = (string)jobJSON["reportTitle"];
            }
            catch
            {
                job.pdfCreateResponseButton.ToolTip = $"Processing failed. Please check the 'reportTitle' parameter in the default values for ${(string)jobJSON["reportType"]} reports. See readme document for more information and parameter definitions."; ;
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            try
            {
                document.DocumentInformation.Creator = (string)templateDefaultSet["creator"];
            }
            catch
            {
                job.pdfCreateResponseButton.ToolTip = "Processing Failed. Please provide a report title.";
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            #endregion
            #region get array of lines in the file
            try
            {
                fileLines = File.ReadAllLines((string)jobJSON["inputFile"]);
            }
            catch (Exception e)
            {
                job.pdfCreateResponseButton.ToolTip = e.Message;
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                return;
            }
            #endregion
            #region create first page and top level bookmark
            PdfBookmark titleBookmark = bookmarks.Add((string)templateDefaultSet["indexTopTitle"]);//"routes" etc.
            if ((string)jobJSON["reportType"] == "stateHighwayLog")
            {
                PdfPageBase titlePage = document.Pages.Add();
                titlePage.Canvas.DrawString((string)jobJSON["reportTitle"], trueTypeFont, new PdfSolidBrush(Color.Black), new PointF(0, 0));

                PdfDestination titleBookmarkDest = new PdfDestination(document.Pages[0], new PointF(0, 0));
                titleBookmark.Action = new PdfGoToAction(titleBookmarkDest);
            };
            PdfPageBase currentPage = document.Pages.Add();
            #endregion
            #region iterate through lines
            for (int i = 0; i < fileLines.Length; i += 1)
            {
                #region set read direction of text document and assign current line from file
                if ((string)jobJSON["readDirection"] == "topToBottom")
                {
                    fileLineNum = i;
                }
                if ((string)jobJSON["readDirection"] == "bottomToTop")
                {
                    fileLineNum = fileLines.Length - i - 1;
                }
                #endregion
                // ** SECOND BOOKMARK LEVEL (ID1) **
                //create ID1 (SR ID) Bookmark
                #region create ID1 bookmark (SRID)
                if ((int)templateDefaultSet["linesToID"] != 0)
                {
                    if (pageLineNum == (int)templateDefaultSet["linesToID"])
                    {
                        //get ID1 (SR Number) value
                        string id1lineText = fileLines[fileLineNum];
                        try
                        {
                            //string headerA = (fileLines[fileLineNum].Substring((int)templateDefaultSet["offsetToID"] - 1, (int)templateDefaultSet["IDTextLength"]).Trim());
                            ID1 = fileLines[fileLineNum].Substring((int)templateDefaultSet["offsetToID"] - 1, (int)templateDefaultSet["IDTextLength"]).Trim();
                        }
                        catch(Exception e)
                        {
                            if(e.Message == "Index and length must refer to a location within the string.\r\nParameter name: length")
                            {
                                job.pdfCreateResponseButton.ToolTip = $"Processing Failed. Either the number of lines to the ID ('lineToID') or the length of the ID text ('IDTextLength') is incorrect. Please check these values for ${(string)jobJSON["reportType"]} reports. See readme document for more information and parameter definitions.";
                            }
                            if(e.Message == "startIndex cannot be larger than length of string.\r\nParameter name: startIndex")
                            {
                                job.pdfCreateResponseButton.ToolTip = $"Processing Failed. Either the number of lines to the ID ('lineToID') or the offset to the ID text ('offsetToID') is incorrect. Please check these values for ${(string)jobJSON["reportType"]} reports. See readme document for more information and parameter definitions.";
                            }
                            job.pdfCreateResponseButton.Background = failBrush;
                            job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                            break;
                        }
                       
                    }
                }
                #endregion
                #region  create addID bookmark (SRID modifier: KELSO, HYAK, etc.)
                if ((int)templateDefaultSet["linesToAddID"] != 0)
                {
                    if (pageLineNum == (int)templateDefaultSet["linesToAddID"])
                    {
                        string lineText = fileLines[fileLineNum];
                        if (lineText.Length > 2)
                        {
                            Console.WriteLine("text");
                        }
                        if (fileLines[fileLineNum].Length >= (int)templateDefaultSet["offsetToAddID"] && fileLines[fileLineNum].Length <= ((int)templateDefaultSet["offsetToAddID"]+ (int)templateDefaultSet["addIDLength"]))
                        {
                            string line = fileLines[fileLineNum].Substring((int)templateDefaultSet["offsetToAddID"], fileLines[fileLineNum].Length - (int)templateDefaultSet["offsetToAddID"]);
                            ID1 = ID1 + line;
                        }
                    }
                }
                #endregion
                createID1Bookmark(ID1, bookmarks, titleBookmark, document, pageNumber, (string)jobJSON["reportType"]);
                #region create detail bookmark (sr number)
                if ((int)templateDefaultSet["linesToDetail"] != 0)
                {
                    if (pageLineNum == (int)templateDefaultSet["linesToDetail"])
                    {
                        string detailString = fileLines[fileLineNum];
                        int offsetValue = (int)templateDefaultSet["offsetToDetail"] - 1;
                        int detailLen = (int)templateDefaultSet["detailTextLength"];
                        if(fileLines[fileLineNum].Length > (int)templateDefaultSet["offsetToDetail"]+ (int)templateDefaultSet["detailTextLength"])
                        {
                            Detail = fileLines[fileLineNum].Substring((int)templateDefaultSet["offsetToDetail"] - 1, (int)templateDefaultSet["detailTextLength"]).Trim();
                            PdfBookmark newDetailBookmark = bookmarks[0][Globals.currentIDBookmarkIndex - 1].Add(Detail);
                            PdfDestination newDetailBookmarkDest;
                            if ((string)jobJSON["reportType"] == "stateHighwayLog")
                            {
                                newDetailBookmarkDest = new PdfDestination(document.Pages[pageNumber], new PointF(0, 0));
                            }
                            else
                            {
                               newDetailBookmarkDest = new PdfDestination(document.Pages[pageNumber-1], new PointF(0, 0));
                            }  
                            newDetailBookmark.Action = new PdfGoToAction(newDetailBookmarkDest);
                        }
                    }
                }
                #endregion
                #region populate PDF page and add new page to doc
                if (fileLines[fileLineNum].Contains((string)templateDefaultSet["headerID"]) == true)//if header ID is found, populate PDF page and add new page add new page
                {
                    addLineToPageLinesArray((string)jobJSON["readDirection"], pageLines, fileLines, fileLineNum);//add line with header to page
                    #region populate PDF page with lines from pageLines array
                    headerIDFound = true;
                    if (i > 1)
                    {
                        for (int z = 0; z < pageLines.Count - 1; z++)
                        {
                            currentPage.Canvas.DrawString(pageLines[z], trueTypeFont, new PdfSolidBrush(Color.Black), new PointF(0, z * fontSize));
                        }
                        currentPage = document.Pages.Add();//add new page to PDF document
                        pageLines = new List<string>();//clear page lines array
                        pageNumber += 1; //increment page number
                        pageLineNum = 0; //reset page line
                    }
                    #endregion
                }
                #endregion
                #region push line to pageLines array
                else
                {
                    addLineToPageLinesArray((string)jobJSON["readDirection"], pageLines, fileLines, fileLineNum); //draw current line (no new page header) to page lines array
                }
                pageLineNum += 1;
                #endregion
            }
            #endregion
            if ((string)jobJSON["reportType"] == "locatorLog")
            {
                PdfPageBase locatorLogTitlePage = document.Pages.Add();
                // titlePage.Canvas.DrawString((string)jobJSON["reportTitle"], trueTypeFont, new PdfSolidBrush(Color.Black), new PointF(0, 0));
                locatorLogTitlePage.Canvas.DrawString("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", trueTypeFont, new PdfSolidBrush(Color.Black), new PointF(15, 15));
            };
            //if in that iteration, the line header wasn't found, then fail the process.
            if (headerIDFound == false)
            {
                job.pdfCreateResponseButton.ToolTip = $"No header ID ${(string)templateDefaultSet["headerID"]} found. Please check value for header ID ('headerID') for ${(string)jobJSON["reportType"]} reports. See readme document for more information and parameter definitions."; ;
                job.pdfCreateResponseButton.Background = failBrush;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
            }
            //Else, save the document
            else {
                PdfDocument reverseDoc = new PdfDocument();
                if((string)jobJSON["readDirection"] == "bottomToTop"){
                    List<int> pageOrderList = new List<int>();
                    List<int> bookmarkOrderList = new List<int>();
                    for (int z = document.Pages.Count-1; z > 0; z--)
                    {
                        pageOrderList.Add(z);
                    }
                    Console.WriteLine(document.Bookmarks);
                    PdfBookmark newTitleBookMark = bookmarks.Add((string)templateDefaultSet["indexTopTitle"]);//create new "Routes" bookmark
                    for (int z = document.Bookmarks[0].Count - 1; z > -1; z--)//iterate through bookmarks by route, bottom to top
                    {
                        PdfBookmark newID1BookMark = newTitleBookMark.Add(document.Bookmarks[0][z].Title);
                        newID1BookMark.Action = document.Bookmarks[0][z].Action;
                        for (int x = document.Bookmarks[0][z].Count - 1; x > -1; x--)
                        {
                            PdfBookmark newDetailBookmark =newID1BookMark.Add(document.Bookmarks[0][z][x].Title);
                            newDetailBookmark.Action = document.Bookmarks[0][z][x].Action;
                        }
                    }
                    int[] orderArray = pageOrderList.ToArray();
                    document.SaveToFile((string)jobJSON["outputFile"]);
                    PdfDocument reArrangeDoc = new PdfDocument((string)jobJSON["outputFile"]);
                    reArrangeDoc.Bookmarks.RemoveAt(0);
                    reArrangeDoc.Pages.ReArrange(orderArray);
                    reArrangeDoc.SaveToFile((string)jobJSON["outputFile"]);
                }
                else
                {
                    document.SaveToFile((string)jobJSON["outputFile"]);
                }
                
            };
        }/**/
   
        public static void createID1Bookmark(string ID1, PdfBookmarkCollection bookmarks, PdfBookmark titleBookmark, PdfDocument document, int pageNumber, string reportType)
        {
            //confirm that the current ID1 is not already a bookmark
            bool IDBookmarkExists = false;
            for (int book = 0; book < bookmarks[0].Count; book += 1)
            {
                if (bookmarks[0][book].Title == ID1)
                {
                    IDBookmarkExists = true;
                }
            }
            //if ID1 bookmark doesn't yet exist, create it.
            if (IDBookmarkExists == false)
            {
                PdfBookmark ID1Bookmark = titleBookmark.Add(ID1);
                PdfDestination newBookmarkDest;
                if (reportType == "stateHighwayLog"){
                    newBookmarkDest = new PdfDestination(document.Pages[pageNumber], new PointF(0, 0));
                }
                else
                {
                    newBookmarkDest = new PdfDestination(document.Pages[pageNumber-1], new PointF(0, 0));
                }
                ID1Bookmark.Action = new PdfGoToAction(newBookmarkDest);
                Globals.currentIDBookmarkIndex = bookmarks[0].Count;
            }
        }
        public static void addLineToPageLinesArray(string readDirection, List<string> pageLines, string[] fileLines, int fileLineNum)
        {
            if (readDirection == "topToBottom")
            {
                pageLines.Add(fileLines[fileLineNum]);
            }
            else
            {
                pageLines.Insert(0, fileLines[fileLineNum]);
            }
        }
    }
}
