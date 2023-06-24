using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StaffContactDirectory.ViewModels;

public class AboutViewModel
{
    private const string V = "Version: ";
    public string AboutInfo => "I'm just a simple staff contact directory.";
    public string Copyright => "Copyright 2023 - Red Opal Innovations";
    public static Version Version => AppInfo.Version;
    public string VersionString => V + Version.ToString();

    public string BaseDir => "BaseDir: " + AppDomain.CurrentDomain.BaseDirectory;
    public string DataDir => "DataDir: " + FileSystem.Current.AppDataDirectory;

    public AboutViewModel()
    {
 
    }
}
