using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using ServiceStack;
using UIKit;

namespace TechStacks.XamForms.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.

            IosPclExportClient.Configure();
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}
