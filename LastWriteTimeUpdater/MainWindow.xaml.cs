using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LastWriteTimeUpdater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Label_PreviewDragOver(object sender, DragEventArgs e)
        {
            if(e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effects = DragDropEffects.Move;
                e.Handled = true;
            }
        }

        private void Label_Drop(object sender, DragEventArgs e)
        {
            var files = (string[]) e.Data.GetData(DataFormats.FileDrop);

            if (files != null && files.Length > 0)
            {
                var now = DateTime.Now;

                int success = 0;
                int error = 0;

                foreach (var file in files)
                {
                    try
                    {
                        File.SetLastWriteTime(file, now);
                        success += 1;
                    }
                    catch
                    {
                        error += 1;
                    }
                }

                if (error == 0)
                {
                    StatusBarItem1.Content = $"Successfully updated last write time of {success} file(s)";
                }
                else
                {
                    StatusBarItem1.Content = $"Error: Unable to set write time for {error} file(s)";
                }
            }
        }
    }
}