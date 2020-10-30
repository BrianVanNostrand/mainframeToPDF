using System;
using System.Windows;

namespace mf2pdfWpfFramework
{
      // public string youtubePath = "https://www.youtube.com/watch?v=WYBoTT2ce8c&list=UUXZWxO7EUC0ZSoeZIR2V9UQ&index=48";
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class jobUserControl : System.Windows.Controls.UserControl
    {
        public int jobNumber;
        public int ButtonContent = 1;
        public jobUserControl()
        {
            InitializeComponent();
        }
        private void deleteJobButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(this);
            Console.WriteLine(jobNumber);
            MainWindow mainWindow = (MainWindow) Window.GetWindow(this);
            mainWindow.deleteJob(this);
           
        }
        private void inputFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text Files (*.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                inputFileTextBox.Text = filename;
            }
        }
        private void outputFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                outputFileTextBox.Text = filename;
            }

        }
    }
}
