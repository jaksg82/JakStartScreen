using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace JakStartScreen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public List<GroupView> GroupViews { get; set; }
        public static string AppDataRoamingFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "JakStartScreen");

        public App()
        {
            InitializeComponent();
            GroupViews = new List<GroupView>();
        }
    }
}