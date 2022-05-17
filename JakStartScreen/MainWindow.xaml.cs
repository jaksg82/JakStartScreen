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

//using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;

namespace JakStartScreen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly int BaseTileSize = 64;
        public static readonly int ImageTileSize1 = BaseTileSize - 8; // 64 - 8 = 56
        public static readonly int ImageTileSize2 = (2 * BaseTileSize) - 8; // 128 - 8 = 120
        private List<LinkItem> availApps;
        private bool AvailAppsLoaded = false;
        private static string xListFile = Path.Combine(App.AppDataRoamingFolder, JakStartScreen.Language.Strings.JakStartScreen, "AvailApps.xml");
        private BackgroundWorker BgwGetAvailApps = new BackgroundWorker();
        private BackgroundWorker BgwUpdateAvailApps = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            Title = JakStartScreen.Language.Strings.StartScreenAvailableLinks;
            availApps = new List<LinkItem>();

            // Background workers
            BgwGetAvailApps.DoWork += BgwGetAvailApps_DoWork;
            BgwGetAvailApps.RunWorkerCompleted += BgwGetAvailApps_RunWorkerCompleted;
            BgwUpdateAvailApps.DoWork += BgwUpdateAvailApps_DoWork;
            BgwUpdateAvailApps.RunWorkerCompleted += BgwUpdateAvailApps_RunWorkerCompleted;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(xListFile))
            {
                //availApps = XmlDb.LoadAppListDb(xListFile);
                lst001.Items.Clear();
                TextBlock txb = new TextBlock();
                txb.Text = JakStartScreen.Language.Strings.ListUpdating;
                lst001.Items.Add(txb);
                BgwGetAvailApps.RunWorkerAsync(new DoWorkEventArgs(xListFile));
            }
            else
            {
                //availApps = ScanAppFolder.GetApps();
                //XmlDb.SaveAppListDb(xListFile, availApps);
                TextBlock txb = new TextBlock();
                txb.Text = JakStartScreen.Language.Strings.ListUpdating;
                lst001.Items.Add(txb);
                BgwUpdateAvailApps.RunWorkerAsync(new DoWorkEventArgs(xListFile));
            }

            lst001Update();
        }

        private void BgwGetAvailApps_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do work
            string appFile = "";
            DoWorkEventArgs passArgs = (DoWorkEventArgs)e.Argument;
            if (passArgs.Argument != null)
            {
                appFile = (string)passArgs.Argument;
                availApps = XmlDb.LoadAppListDb(appFile);
            }
        }

        private void BgwGetAvailApps_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Work completed
            AvailAppsLoaded = true;
            lst001Update();
        }

        private void BgwUpdateAvailApps_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do work
            string appFile = "";
            DoWorkEventArgs passArgs = (DoWorkEventArgs)e.Argument;
            if (passArgs.Argument != null)
            {
                appFile = (string)passArgs.Argument;
                availApps = ScanAppFolder.GetApps();
                XmlDb.SaveAppListDb(xListFile, availApps);
            }
        }

        private void BgwUpdateAvailApps_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Work completed
            AvailAppsLoaded = true;
            lst001Update();
        }

        private void lst001Update()
        {
            if (AvailAppsLoaded)
            {
                lst001.Items.Clear();
                Title = JakStartScreen.Language.Strings.StartScreenAvailableLinks + availApps.Count;
                foreach (LinkItem app in availApps)
                {
                    string apptype = app.Type == LinkItem.AppType.AppX ? "AppX|" : "Desk|";
                    string appimg = app.Image == null ? "null|" : "OK  |";
                    TextBlock txb = new TextBlock();
                    txb.Text = apptype + appimg + app.Name + "|" + app.LinkURL;
                    lst001.Items.Add(txb);
                }
            }
        }
    }
}