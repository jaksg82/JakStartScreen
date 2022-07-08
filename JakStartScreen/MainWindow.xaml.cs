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
using System.Collections.ObjectModel;
using System.Diagnostics;

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
        private List<GroupView> userGroups;
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
            userGroups = new List<GroupView>();

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
                UserGroupsLoaded = GetUserGroupList(xUserGroups);
            }
            else
            {
                // Create and load a default file
                List<LinkItemView> listlnk = new List<LinkItemView>();
                listlnk.Add(new LinkItemView());
                GroupView g01 = new("Start Group", listlnk);
                userGroups.Add(g01);
                UserGroupsLoaded = true;
            }
            if (UserGroupsLoaded)
            {
                lst002Update();
            }

            LinkItem tmpLnk = new LinkItem();
            tmpLnk.Name = "Loading list ..";
            ObservableCollection<LinkItem> obs = new();
            obs.Add(tmpLnk);
            lst001.ItemsSource = obs;
            // Check and load the list of installed apps
            if (File.Exists(xListFile))
            {
                //availApps = XmlDb.LoadAppListDb(xListFile);
                //lst001.Items.Clear();
                //TextBlock txb = new TextBlock();
                //txb.Text = JakStartScreen.Language.Strings.ListUpdating;
                //lst001.Items.Add(txb);
                if (BgwGetAvailApps.IsBusy != true)
                {
                    BgwGetAvailApps.RunWorkerAsync(new DoWorkEventArgs(xListFile));
                }
            }
            else
            {
                //availApps = ScanAppFolder.GetApps();
                //XmlDb.SaveAppListDb(xListFile, availApps);
                //TextBlock txb = new TextBlock();
                //txb.Text = JakStartScreen.Language.Strings.ListUpdating;
                //lst001.Items.Add(txb);
                if (BgwUpdateAvailApps.IsBusy != true)
                {
                    BgwUpdateAvailApps.RunWorkerAsync(new DoWorkEventArgs(xListFile));
                }
            }
        }

        private bool GetUserGroupList(string xmlFile)
        {
            if (xmlFile == null)
            {
                return false;
            }
            else
            {
                if (!File.Exists(xmlFile))
                {
                    userGroups = XmlDb.LoadUserGroups(xmlFile);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private void BgwGetAvailApps_DoWork(object sender, DoWorkEventArgs e)
        {
            // Do work
            if (e.Argument != null)
            {
                DoWorkEventArgs eArg = (DoWorkEventArgs)e.Argument;
                string appFile = (string)eArg.Argument;
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
                DoWorkEventArgs eArg = (DoWorkEventArgs)e.Argument;
                string appFile = (string)eArg.Argument;
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
                //lst001.Items.Clear();
                ObservableCollection<LinkItem> tmpColl = new();

                Title = JakStartScreen.Language.Strings.StartScreenAvailableLinks + availApps.Count;
                for (int i = 0; i < availApps.Count; i++)
                {
                    tmpColl.Add(availApps[i]);
                }
                lst001.ItemsSource = tmpColl;
            }
        }

        private void lst002Update()
        {
            if (UserGroupsLoaded)
            {
                lst002.Items.Clear();
                // Load the groups
                foreach (GroupView item in userGroups)
                {
                    lst002.Items.Add(item);
                }
            }
        }

        private void MenuListAllButtonClick(object sender, RoutedEventArgs e)
        {
            if (lst001.Visibility == Visibility.Visible)
            {
                lst001.Visibility = Visibility.Collapsed;
                lst002.Visibility = Visibility.Visible;
            }
            else
            {
                lst001.Visibility = Visibility.Visible;
                lst002.Visibility = Visibility.Collapsed;
            }
        }

        private void lst001_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lst001.SelectedItem != null)
            {
                LinkItem selItem = (LinkItem)lst001.SelectedItem;
                if (selItem.Type == LinkItem.AppType.Desktop || selItem.Type == LinkItem.AppType.AppX)
                {
                    string lnk = @"shell:AppsFolder\" + selItem.LinkURL;
                    Process.Start("explorer.exe", lnk);
                }
                else if (selItem.Type == LinkItem.AppType.Website)
                {
                    // Create a Uri object from a URI string
                    var uri = new Uri(selItem.LinkURL);
                    Windows.System.Launcher.LaunchUriAsync(uri);
                }
                else
                {
                    // Do Nothing
                }
            }
        }
    }
}