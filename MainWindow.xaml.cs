using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Panuon.UI.Silver;
using KMCCC.Launcher;
using KMCCC.Authentication;

namespace PFL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : WindowX
    {
        public static LauncherCore Core = LauncherCore.Create();
        public MainWindow()
        {
            InitializeComponent();
            var versions = Core.GetVersions().ToArray();
            versionCombo.ItemsSource = versions;//绑定数据源
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Core.JavaPath = @"C:\Program Files\Java\jre1.8.0_321\bin\javaw.exe";
            var ver = (KMCCC.Launcher.Version)versionCombo.SelectedItem;
            var result = Core.Launch(new LaunchOptions
            {
                Version = ver,
                MaxMemory = 1024,
                Authenticator = new OfflineAuthenticator("SANTALE"),
                Mode = LaunchMode.MCLauncher
            });
        }
    }
}
