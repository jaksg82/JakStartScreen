//https://stackoverflow.com/questions/32122679/getting-icon-of-modern-windows-app-from-a-desktop-application/36559301#36559301

using System.Diagnostics;
using System;

namespace ListAppxPackages
{
    public class Program
    {
        private static void Main(string[] args)
        {
            string outFileName = "";

            if (args != null)
            {
                if (args.Length > 0)
                {
                    outFileName = args[0];
                }
            }
            if (outFileName.Length > 0)
            {
                using (StreamWriter outFs = new StreamWriter(outFileName, false, System.Text.Encoding.Default))
                {
                    foreach (var p in Process.GetProcesses())
                    {
                        var package = AppxPackage.FromProcess(p);
                        if (package != null)
                        {
                            Save(0, package, outFs);
                            outFs.WriteLine(">>> END OF PACKAGE <<<");
                            outFs.WriteLine();
                        }
                    }
                }
            }
            else
            {
                foreach (var p in Process.GetProcesses())
                {
                    var package = AppxPackage.FromProcess(p);
                    if (package != null)
                    {
                        Show(0, package);
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }
            }
        }

        private static void Show(int indent, AppxPackage package)
        {
            string sindent = new string(' ', indent);
            Console.WriteLine(sindent + "FullName               : " + package.FullName);
            Console.WriteLine(sindent + "FamilyName             : " + package.FamilyName);
            Console.WriteLine(sindent + "IsFramework            : " + package.IsFramework);
            Console.WriteLine(sindent + "ApplicationUserModelId : " + package.ApplicationUserModelId);
            Console.WriteLine(sindent + "Path                   : " + package.Path);
            Console.WriteLine(sindent + "Publisher              : " + package.Publisher);
            Console.WriteLine(sindent + "PublisherId            : " + package.PublisherId);
            Console.WriteLine(sindent + "Logo                   : " + package.Logo);
            Console.WriteLine(sindent + "Best Logo Path         : " + package.FindHighestScaleQualifiedImagePath(package.Logo));
            Console.WriteLine(sindent + "ProcessorArchitecture  : " + package.ProcessorArchitecture);
            Console.WriteLine(sindent + "Version                : " + package.Version);
            Console.WriteLine(sindent + "PublisherDisplayName   : " + package.PublisherDisplayName);
            Console.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(package.PublisherDisplayName));
            Console.WriteLine(sindent + "DisplayName            : " + package.DisplayName);
            Console.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(package.DisplayName));
            Console.WriteLine(sindent + "Description            : " + package.Description);
            Console.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(package.Description));

            Console.WriteLine(sindent + "Apps                   :");
            int i = 0;
            foreach (var app in package.Apps)
            {
                Console.WriteLine(sindent + " App [" + i + "] Description       : " + app.Description);
                Console.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(app.Description));
                Console.WriteLine(sindent + " App [" + i + "] DisplayName       : " + app.DisplayName);
                Console.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(app.DisplayName));
                Console.WriteLine(sindent + " App [" + i + "] ShortName         : " + app.ShortName);
                Console.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(app.ShortName));
                Console.WriteLine(sindent + " App [" + i + "] EntryPoint        : " + app.EntryPoint);
                Console.WriteLine(sindent + " App [" + i + "] Executable        : " + app.Executable);
                Console.WriteLine(sindent + " App [" + i + "] Id                : " + app.Id);
                Console.WriteLine(sindent + " App [" + i + "] Logo              : " + app.Logo);
                Console.WriteLine(sindent + " App [" + i + "] SmallLogo         : " + app.SmallLogo);
                Console.WriteLine(sindent + " App [" + i + "] StartPage         : " + app.StartPage);
                Console.WriteLine(sindent + " App [" + i + "] Square150x150Logo : " + app.Square150x150Logo);
                Console.WriteLine(sindent + " App [" + i + "] Square30x30Logo   : " + app.Square30x30Logo);
                Console.WriteLine(sindent + " App [" + i + "] BackgroundColor   : " + app.BackgroundColor);
                Console.WriteLine(sindent + " App [" + i + "] ForegroundText    : " + app.ForegroundText);
                Console.WriteLine(sindent + " App [" + i + "] WideLogo          : " + app.WideLogo);
                Console.WriteLine(sindent + " App [" + i + "] Wide310x310Logo   : " + app.Wide310x310Logo);
                Console.WriteLine(sindent + " App [" + i + "] Square310x310Logo : " + app.Square310x310Logo);
                Console.WriteLine(sindent + " App [" + i + "] Square70x70Logo   : " + app.Square70x70Logo);
                Console.WriteLine(sindent + " App [" + i + "] MinWidth          : " + app.MinWidth);
                Console.WriteLine(sindent + " App [" + i + "] Square71x71Logo   : " + app.GetStringValue("Square71x71Logzo"));
                i++;
            }

            Console.WriteLine(sindent + "Deps                   :");
            foreach (var dep in package.DependencyGraph)
            {
                Show(indent + 1, dep);
            }
        }

        private static void Save(int indent, AppxPackage package, StreamWriter outStream)
        {
            string sindent = new string(' ', indent);
            outStream.WriteLine(sindent + "FullName               : " + package.FullName);
            outStream.WriteLine(sindent + "FamilyName             : " + package.FamilyName);
            outStream.WriteLine(sindent + "IsFramework            : " + package.IsFramework);
            outStream.WriteLine(sindent + "ApplicationUserModelId : " + package.ApplicationUserModelId);
            outStream.WriteLine(sindent + "Path                   : " + package.Path);
            outStream.WriteLine(sindent + "Publisher              : " + package.Publisher);
            outStream.WriteLine(sindent + "PublisherId            : " + package.PublisherId);
            outStream.WriteLine(sindent + "Logo                   : " + package.Logo);
            outStream.WriteLine(sindent + "Best Logo Path         : " + package.FindHighestScaleQualifiedImagePath(package.Logo));
            outStream.WriteLine(sindent + "ProcessorArchitecture  : " + package.ProcessorArchitecture);
            outStream.WriteLine(sindent + "Version                : " + package.Version);
            outStream.WriteLine(sindent + "PublisherDisplayName   : " + package.PublisherDisplayName);
            outStream.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(package.PublisherDisplayName));
            outStream.WriteLine(sindent + "DisplayName            : " + package.DisplayName);
            outStream.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(package.DisplayName));
            outStream.WriteLine(sindent + "Description            : " + package.Description);
            outStream.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(package.Description));

            outStream.WriteLine(sindent + "Apps                   :");
            int i = 0;
            foreach (var app in package.Apps)
            {
                outStream.WriteLine(sindent + " App [" + i + "] Description       : " + app.Description);
                outStream.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(app.Description));
                outStream.WriteLine(sindent + " App [" + i + "] DisplayName       : " + app.DisplayName);
                outStream.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(app.DisplayName));
                outStream.WriteLine(sindent + " App [" + i + "] ShortName         : " + app.ShortName);
                outStream.WriteLine(sindent + "   Localized           : " + package.LoadResourceString(app.ShortName));
                outStream.WriteLine(sindent + " App [" + i + "] EntryPoint        : " + app.EntryPoint);
                outStream.WriteLine(sindent + " App [" + i + "] Executable        : " + app.Executable);
                outStream.WriteLine(sindent + " App [" + i + "] Id                : " + app.Id);
                outStream.WriteLine(sindent + " App [" + i + "] Logo              : " + app.Logo);
                outStream.WriteLine(sindent + " App [" + i + "] SmallLogo         : " + app.SmallLogo);
                outStream.WriteLine(sindent + " App [" + i + "] StartPage         : " + app.StartPage);
                outStream.WriteLine(sindent + " App [" + i + "] Square150x150Logo : " + app.Square150x150Logo);
                outStream.WriteLine(sindent + " App [" + i + "] Square30x30Logo   : " + app.Square30x30Logo);
                outStream.WriteLine(sindent + " App [" + i + "] BackgroundColor   : " + app.BackgroundColor);
                outStream.WriteLine(sindent + " App [" + i + "] ForegroundText    : " + app.ForegroundText);
                outStream.WriteLine(sindent + " App [" + i + "] WideLogo          : " + app.WideLogo);
                outStream.WriteLine(sindent + " App [" + i + "] Wide310x310Logo   : " + app.Wide310x310Logo);
                outStream.WriteLine(sindent + " App [" + i + "] Square310x310Logo : " + app.Square310x310Logo);
                outStream.WriteLine(sindent + " App [" + i + "] Square70x70Logo   : " + app.Square70x70Logo);
                outStream.WriteLine(sindent + " App [" + i + "] MinWidth          : " + app.MinWidth);
                outStream.WriteLine(sindent + " App [" + i + "] Square71x71Logo   : " + app.GetStringValue("Square71x71Logzo"));
                i++;
            }

            outStream.WriteLine(sindent + "Deps                   :");
            foreach (var dep in package.DependencyGraph)
            {
                Save(indent + 4, dep, outStream);
            }
        }
    }
}