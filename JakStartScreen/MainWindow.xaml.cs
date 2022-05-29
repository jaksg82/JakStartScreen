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
        private List<LinkGroup> userGroups;
        private bool AvailAppsLoaded = false;
        private bool UserGroupsLoaded = false;
        private static string xListFile = Path.Combine(App.AppDataRoamingFolder, JakStartScreen.Language.Strings.JakStartScreen, "AvailApps.xml");
        private static string xUserGroups = Path.Combine(App.AppDataRoamingFolder, JakStartScreen.Language.Strings.JakStartScreen, "UserGroups.xml");
        private BackgroundWorker BgwGetAvailApps = new BackgroundWorker();
        private BackgroundWorker BgwUpdateAvailApps = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();
            Title = JakStartScreen.Language.Strings.StartScreenAvailableLinks;
            availApps = new List<LinkItem>();
            userGroups = new List<LinkGroup>();

            // Background workers
            BgwGetAvailApps.DoWork += new DoWorkEventHandler(BgwGetAvailApps_DoWork);
            BgwGetAvailApps.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgwGetAvailApps_RunWorkerCompleted);
            BgwUpdateAvailApps.DoWork += new DoWorkEventHandler(BgwUpdateAvailApps_DoWork);
            BgwUpdateAvailApps.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BgwUpdateAvailApps_RunWorkerCompleted);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // check and load the list of user selected apps
            if (File.Exists(xUserGroups))
            {
                // Load the file content
                UserGroupsLoaded = true;
            }
            else
            {
                // Create and load a default file
                UserGroupsLoaded = true;
            }

            // Check and load the list of installed apps
            if (File.Exists(xListFile))
            {
                //availApps = XmlDb.LoadAppListDb(xListFile);
                lst001.Items.Clear();
                TextBlock txb = new TextBlock();
                txb.Text = JakStartScreen.Language.Strings.ListUpdating;
                lst001.Items.Add(txb);
                if (BgwGetAvailApps.IsBusy != true)
                {
                    BgwGetAvailApps.RunWorkerAsync(new DoWorkEventArgs(xListFile));
                }
            }
            else
            {
                //availApps = ScanAppFolder.GetApps();
                //XmlDb.SaveAppListDb(xListFile, availApps);
                TextBlock txb = new TextBlock();
                txb.Text = JakStartScreen.Language.Strings.ListUpdating;
                lst001.Items.Add(txb);
                if (BgwUpdateAvailApps.IsBusy != true)
                {
                    BgwUpdateAvailApps.RunWorkerAsync(new DoWorkEventArgs(xListFile));
                }
            }
        }

        private void BgwGetAvailApps_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do work
            string appFile = "";
            DoWorkEventArgs passArgs = (DoWorkEventArgs)e.Argument;
            if (passArgs.Argument != null)
            {
                appFile = (string)passArgs.Argument;
                e.Result = XmlDb.LoadAppListDb(appFile);
            }
        }

        private void BgwGetAvailApps_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Work completed
            if (e.Cancelled)
            {
                // The user canceled the operation.
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
            }
            else
            {
                // The operation completed normally.
                availApps = (List<LinkItem>)e.Result;
                AvailAppsLoaded = true;
                lst001Update();
            }
        }

        private void BgwUpdateAvailApps_DoWork(object sender, DoWorkEventArgs e)
        {
            if (e.Argument != null)
            {
                // Do work
                string appFile = (string)e.Argument;
                List<LinkItem> tmpList = ScanAppFolder.GetApps();
                XmlDb.SaveAppListDb(appFile, tmpList);
                e.Result = tmpList;
            }
        }

        private void BgwUpdateAvailApps_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Work completed
            if (e.Cancelled)
            {
                // The user canceled the operation.
            }
            else if (e.Error != null)
            {
                // There was an error during the operation.
            }
            else
            {
                // The operation completed normally.
                if (e.Result != null)
                {
                    availApps = (List<LinkItem>)e.Result;
                    AvailAppsLoaded = true;
                    lst001Update();
                }
                else
                {
                    AvailAppsLoaded = false;
                }
            }
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
                    TextBlock txb = new TextBlock
                    {
                        Text = apptype + appimg + app.Name + "|" + app.LinkURL
                    };
                    lst001.Items.Add(txb);
                }
            }
        }

        private void lst002Update()
        {
            if (UserGroupsLoaded)
            {
                // Load the groups
            }
        }
    }
}