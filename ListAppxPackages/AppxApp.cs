using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListAppxPackages
{
    public sealed class AppxApp
    {
        private AppxPackage.IAppxManifestApplication _app;

        internal AppxApp(AppxPackage.IAppxManifestApplication app)
        {
            _app = app;
        }

        public string GetStringValue(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            return AppxPackage.GetStringValue(_app, name);
        }

        // we code well-known but there are others (like Square71x71Logo, Square44x44Logo, whatever ...)
        // https://msdn.microsoft.com/en-us/library/windows/desktop/hh446703.aspx
        public string Description { get; internal set; }

        public string DisplayName { get; internal set; }
        public string EntryPoint { get; internal set; }
        public string Executable { get; internal set; }
        public string Id { get; internal set; }
        public string Logo { get; internal set; }
        public string SmallLogo { get; internal set; }
        public string StartPage { get; internal set; }
        public string Square150x150Logo { get; internal set; }
        public string Square30x30Logo { get; internal set; }
        public string BackgroundColor { get; internal set; }
        public string ForegroundText { get; internal set; }
        public string WideLogo { get; internal set; }
        public string Wide310x310Logo { get; internal set; }
        public string ShortName { get; internal set; }
        public string Square310x310Logo { get; internal set; }
        public string Square70x70Logo { get; internal set; }
        public string MinWidth { get; internal set; }
    }
}