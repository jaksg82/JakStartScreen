using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        public App()
        {
            InitializeComponent();
            GroupViews = new List<GroupView>();
        }
    }
}