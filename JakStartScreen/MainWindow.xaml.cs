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

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
