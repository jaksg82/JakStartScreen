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
    /// Logica di interazione per LinkItemView.xaml
    /// </summary>
    public partial class LinkItemView : UserControl
    {
        private LinkItem _item;
        public string Title { get; set; }
        public BitmapImage Image { get; set; }
        public string Link { get; set; }


        public LinkItemView()
        {
            InitializeComponent();
            _item = new LinkItem();
            Title = "New Shortcut";
            Image = new BitmapImage();
            Link = "";
        }
    }
}
