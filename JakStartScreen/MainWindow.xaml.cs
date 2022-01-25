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

        public MainWindow()
        {
            InitializeComponent();
            Title = "StartScreen - Loading Apps";
            availApps = new List<LinkItem>();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            availApps = ScanAppFolder.GetApps();
            Title = "StartScreen - Available Apps: " + availApps.Count;
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