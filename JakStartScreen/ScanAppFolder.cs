using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JakStartScreen
{
    public static class ScanAppFolder
    {
        public static List<LinkItem> GetApps()
        {
            List<LinkItem> apps = new List<LinkItem>();

            // https://stackoverflow.com/questions/908850/get-installed-applications-in-a-system
            // GUID taken from https://docs.microsoft.com/en-us/windows/win32/shell/knownfolderid
            var FOLDERID_AppsFolder = new Guid("{1e87508d-89c2-42f0-8a7e-645a0f50ca58}");
            ShellObject appsFolder = (ShellObject)KnownFolderHelper.FromKnownFolderId(FOLDERID_AppsFolder);

            foreach (var app in (IKnownFolder)appsFolder)
            {
                // The friendly app name
                //string name = app.Name;
                // The ParsingName property is the AppUserModelID
                //string appUserModelID = app.ParsingName; // or app.Properties.System.AppUserModel.ID
                // You can even get the Jumbo icon in one shot
                Bitmap icon = app.Thumbnail.ExtraLargeBitmap;
                BitmapImage bmp = Bitmap2BitmapImage(icon);
                LinkItem.AppType apty = app.IsLink ? LinkItem.AppType.Desktop : LinkItem.AppType.AppX;

                LinkItem lnk = new LinkItem(app.Name, app.ParsingName, bmp, apty);
                apps.Add(lnk);
            }
            return apps;
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapSource retsrc;
            BitmapImage retval = new BitmapImage();

            try
            {
                retsrc = Imaging.CreateBitmapSourceFromHBitmap(
                             hBitmap,
                             IntPtr.Zero,
                             Int32Rect.Empty,
                             BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                DeleteObject(hBitmap);
            }
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            MemoryStream ms = new MemoryStream();
            encoder.Frames.Add(BitmapFrame.Create(retsrc));
            encoder.Save(ms);

            ms.Position = 0;
            retval.BeginInit();
            retval.StreamSource = new MemoryStream(ms.ToArray());
            retval.EndInit();
            retval.Freeze();

            ms.Close();

            return retval;
        }
    }
}