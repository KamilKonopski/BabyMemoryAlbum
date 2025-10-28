using System.Windows;
using System.Windows.Controls;
using MojePierwsze.Views;
using System.IO;
using System.Reflection;

namespace MojePierwsze
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string? appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dataDirectory = Path.Combine(appDirectory ?? string.Empty, "Data");

            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            MainContent.Content = new HomeView(this);
        }

        public void NavigateTo(UserControl view)
        {
            MainContent.Content = view;
        }
    }
}