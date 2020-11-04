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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using FontStyle = System.Drawing.FontStyle;

namespace mf2pdfWpfFramework
{
    class spirePDF
    {
        public static int currentIDBookmarkIndex;
        static public async void getPDFParameters(JObject jobsJSON, ListBox jobsItemsControl, ProgressBar progressBar, TextBlock progressBarLabel)
        {
            string templateDefaultsRead = File.ReadAllText(@"./templateDefaults.json");
            JObject templateDefaultsJSON = JObject.Parse(templateDefaultsRead);
            progressBar.Maximum = jobsJSON.Count;
            progressBarLabel.Text = $"Processing job 1/{jobsJSON.Count}";
            #region update progress bar to reflect jobs processed
            IProgress<int> progress = new Progress<int>((value)=>
            {
                progressBar.Value = value;
                if (value != jobsJSON.Count)
                {
                    progressBarLabel.Text = $"Processing job {value+1}/{jobsJSON.Count}";
                }
                else
                {
                    progressBarLabel.Text = $"Processing Complete";
                }
            });
            #endregion
            for (int j = 0; j < jobsJSON.Count; j += 1)
            {
                string reportType = (string)jobsJSON[$"job_{j}"]["ReportType"];
                JObject templateDefaultSet = (JObject)templateDefaultsJSON[reportType];
                jobUserControl job = (jobUserControl)jobsItemsControl.Items[j];
             
                await Task.Run(() =>
                {
                    try
                    {
                        generatePDF((JObject)jobsJSON[$"job_{j}"], templateDefaultSet, j, jobsItemsControl, progress, job);//run the generate pdf method
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }
        }
        static public async void generatePDF(JObject jobJSON, JObject templateDefaultSet, int j, ListBox jobsItemsControl, IProgress<int> progress, jobUserControl job)
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
            List<string> pageLines = new List<string>();//aray of lines in the current page
            FontFamily fontName = new System.Drawing.FontFamily((string)templateDefaultSet["fontName"]);
            PdfDocument document = new PdfDocument();
            PdfBookmarkCollection bookmarks = document.Bookmarks;
            Font font = new Font(fontName, fontSize, FontStyle.Regular);
            PdfTrueTypeFont trueTypeFont = new PdfTrueTypeFont(font);
            #endregion

            #region set report metadata
            document.PageSettings.Height = (int)templateDefaultSet["pageYinches"] * 72;
            document.PageSettings.Width = (int)templateDefaultSet["pageXinches"] * 72;
            document.DocumentInformation.Author = (string)templateDefaultSet["author"];
            document.DocumentInformation.Title = (string)jobJSON["ReportTitle"];
            document.DocumentInformation.Creator = (string)templateDefaultSet["creator"];
            fileLines = File.ReadAllLines((string)jobJSON["InputFile"]);
            #endregion

            #region create IProgress to monitor text document parsing and PDF creation
            IProgress<int> jobProgress = new Progress<int>((linevalue) =>
            {
                 ProgressBar jobProgressBar;
                 TextBlock jobProgressBarLabel;
                 Grid jobProgressBarGrid;
                 job.Dispatcher.Invoke(() =>
                 {
                     jobProgressBar = (ProgressBar)job.FindName("jobProgressBar");
                     jobProgressBarLabel = (TextBlock)job.FindName("jobProgressBarLabel");
                     jobProgressBarGrid= (Grid)job.FindName("jobProgressBarGrid");
                     jobProgressBarLabel.Text = $"Processing line 1/${fileLines.Length}";
                     jobProgressBar.Value = linevalue;
                     if (linevalue != fileLines.Length-1)
                     {
                         if(jobProgressBar.Visibility == Visibility.Hidden || jobProgressBarLabel.Visibility == Visibility.Hidden)//expose the progress bar
                         {
                             jobProgressBar.Visibility = Visibility.Visible;
                             jobProgressBarLabel.Visibility = Visibility.Visible;
                         }
                         jobProgressBarLabel.Text = $"Processing job {linevalue + 1}/{fileLines.Length}";//update the progress bar text label
                     }
                     else//hide the progress bar
                     {
                         jobProgressBar.Visibility = Visibility.Hidden;
                         jobProgressBarLabel.Visibility = Visibility.Hidden;
                     }
                 });
             });
            IProgress<string> jobMessage = new Progress<string>((Message) =>
            {
                job.Dispatcher.Invoke(() => {
                    BitmapImage BitmapToImageSource(Bitmap bitmap)
                    {
                        using (MemoryStream memory = new MemoryStream())
                        {
                            bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                            memory.Position = 0;
                            BitmapImage bitmapimage = new BitmapImage();
                            bitmapimage.BeginInit();
                            bitmapimage.StreamSource = memory;
                            bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapimage.EndInit();

                            return bitmapimage;
                        }
                    }
                    Button pdfCreateResponseButton;
                    System.Windows.Media.ImageBrush jobStatusImage;
                    System.Windows.Media.ImageSource checkMarkImageSource = BitmapToImageSource(Properties.Resources.checkMark);
                    System.Windows.Media.ImageSource exclamationPointImageSource = BitmapToImageSource(Properties.Resources.exclamationPoint);
                    pdfCreateResponseButton = (Button)job.FindName("pdfCreateResponseButton");
                    jobStatusImage = (System.Windows.Media.ImageBrush)job.FindName("jobStatusImage");

                    if (Message == "jobComplete")
                    {
                        jobStatusImage.ImageSource = checkMarkImageSource;//set button image
                        pdfCreateResponseButton.Visibility = Visibility.Visible;//show response button
                        pdfCreateResponseButton.Click += (s, e) =>
                        {
                            try
                            {
                                System.Diagnostics.Process.Start((string)jobJSON["OutputFile"]);
                            }
                            catch
                            {
                                return;
                            }
                            //open the file in windows
                        };
                    }
                    else
                    {
                        jobStatusImage.ImageSource = exclamationPointImageSource;//set button image
                        pdfCreateResponseButton.Visibility = Visibility.Visible;//show response button
                        pdfCreateResponseButton.Click += (s, e) =>
                        {
                            MessageBox.Show(Message);
                        };
                    }
                });
            });


             #endregion

            #region create first page and top level bookmark
            PdfBookmark titleBookmark = bookmarks.Add((string)templateDefaultSet["indexTopTitle"]);//"routes" etc.
            if ((string)jobJSON["ReportType"] == "stateHighwayLog")
            {
                PdfPageBase titlePage = document.Pages.Add();
                titlePage.Canvas.DrawString((string)jobJSON["ReportTitle"], trueTypeFont, new PdfSolidBrush(Color.Black), new PointF(0, 0));

                PdfDestination titleBookmarkDest = new PdfDestination(document.Pages[0], new PointF(0, 0));
                titleBookmark.Action = new PdfGoToAction(titleBookmarkDest);
            };
            PdfPageBase currentPage = document.Pages.Add();
            #endregion

            #region iterate through lines
            for (int i = 0; i < fileLines.Length; i += 1)
            {
                #region set read direction of text document and assign current line from file
                if ((string)jobJSON["ReadDirection"] == "topToBottom")
                {
                    fileLineNum = i;
                }
                if ((string)jobJSON["ReadDirection"] == "bottomToTop")
                {
                    fileLineNum = fileLines.Length - i - 1;
                }
                #endregion
                // ** SECOND BOOKMARK LEVEL (ID1) **
                //create ID1 (SR ID) Bookmark
                #region create ID1 bookmark (SRID)
               // if ((int)templateDefaultSet["linesToID"] != 0)
               // {
                    if (pageLineNum == (int)templateDefaultSet["linesToID"])
                    {
                        //get ID1 (SR Number) value
                        string id1lineText = fileLines[fileLineNum];
                        try
                        {
                            //string headerA = (fileLines[fileLineNum].Substring((int)templateDefaultSet["offsetToID"] - 1, (int)templateDefaultSet["IDTextLength"]).Trim());
                            ID1 = fileLines[fileLineNum].Substring((int)templateDefaultSet["offsetToID"], (int)templateDefaultSet["IDTextLength"]).Trim();
                        }
                        catch(Exception e)
                        {
                            if(e.Message == "Index and length must refer to a location within the string.\r\nParameter name: length")
                            {
                                job.pdfCreateResponseButton.ToolTip = $"Processing Failed. Either the number of lines to the ID ('lineToID') or the length of the ID text ('IDTextLength') is incorrect. Please check these values for ${(string)jobJSON["ReportType"]} reports. See readme document for more information and parameter definitions.";
                            }
                            if(e.Message == "startIndex cannot be larger than length of string.\r\nParameter name: startIndex")
                            {
                                job.pdfCreateResponseButton.ToolTip = $"Processing Failed. Either the number of lines to the ID ('lineToID') or the offset to the ID text ('offsetToID') is incorrect. Please check these values for ${(string)jobJSON["ReportType"]} reports. See readme document for more information and parameter definitions.";
                            }
                            job.pdfCreateResponseButton.Visibility = Visibility.Visible;
                            break;
                        }
                       
                    }
                //}
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
                createID1Bookmark(ID1, bookmarks, titleBookmark, document, pageNumber, (string)jobJSON["ReportType"]);
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
                            PdfBookmark newDetailBookmark = bookmarks[0][currentIDBookmarkIndex - 1].Add(Detail);
                            PdfDestination newDetailBookmarkDest;
                            if ((string)jobJSON["ReportType"] == "stateHighwayLog")
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
                    addLineToPageLinesArray((string)jobJSON["ReadDirection"], pageLines, fileLines, fileLineNum);//add line with header to page
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
                    addLineToPageLinesArray((string)jobJSON["ReadDirection"], pageLines, fileLines, fileLineNum); //draw current line (no new page header) to page lines array
                }
                pageLineNum += 1;
                #endregion
                jobProgress.Report(i);

            }
            #endregion
            if ((string)jobJSON["ReportType"] == "locatorLog")
            {
                PdfPageBase locatorLogTitlePage = document.Pages.Add();
                // titlePage.Canvas.DrawString((string)jobJSON["reportTitle"], trueTypeFont, new PdfSolidBrush(Color.Black), new PointF(0, 0));
                locatorLogTitlePage.Canvas.DrawString("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", trueTypeFont, new PdfSolidBrush(Color.Black), new PointF(15, 15));
            };
            //if in that iteration, the line header wasn't found, then fail the process.
            if (headerIDFound == false)
            {
                job.pdfCreateResponseButton.ToolTip = $"No header ID ${(string)templateDefaultSet["headerID"]} found. Please check value for header ID ('headerID') for ${(string)jobJSON["ReportType"]} reports. See readme document for more information and parameter definitions."; ;
                job.pdfCreateResponseButton.Visibility = Visibility.Visible;
            }
            //Else, save the document
            else {
                PdfDocument reverseDoc = new PdfDocument();
                if((string)jobJSON["ReadDirection"] == "bottomToTop"){
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
                    try
                    {
                        document.SaveToFile((string)jobJSON["OutputFile"]);
                    }
                    catch(Exception e)
                    {
                        jobMessage.Report(e.ToString());
                    }
                    PdfDocument reArrangeDoc = new PdfDocument((string)jobJSON["OutputFile"]);
                    reArrangeDoc.Bookmarks.RemoveAt(0);
                    reArrangeDoc.Pages.ReArrange(orderArray);
                    reArrangeDoc.SaveToFile((string)jobJSON["OutputFile"]);
                }
                else
                {
                    try
                    {
                           document.SaveToFile((string)jobJSON["OutputFile"]);
                    }
                    catch
                    {
                        jobMessage.Report("Unable to save. Make sure the output document is not open in a PDF reader and that the output path is valid.");
                    }
                    
                }
            };
            progress.Report(j+1);//report to the main window the number (not index) of the job that just finished.
            jobMessage.Report("jobComplete");//report to the job user control that the job has saved
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
                currentIDBookmarkIndex = bookmarks[0].Count;
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
