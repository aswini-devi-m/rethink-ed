//using AventStack.ExtentReports;
//using AventStack.ExtentReports.Reporter;
//using System;
//using System.IO;

//namespace RethinkEd_Automation.Utilities
//{
//    public static class ReportManager
//    {
//        private static AventStack.ExtentReports.ExtentReports _extent;
//        private static string _reportPath;

//        public static AventStack.ExtentReports.ExtentReports GetExtent()
//        {
//            if (_extent == null)
//            {
//                // Get the current directory
//                var currentDirectory = Directory.GetCurrentDirectory();

//                // Find the index of the "bin" folder
//                var binIndex = currentDirectory.IndexOf("bin", StringComparison.OrdinalIgnoreCase);

//                // If "bin" is found, take the path before it
//                string rootPath;
//                if (binIndex >= 0)
//                {
//                    rootPath = currentDirectory.Substring(0, binIndex);
//                }
//                else
//                {
//                    rootPath = currentDirectory; // Fallback to current if "bin" not found
//                }

//                // Define the folder path (e.g., "Data" in the root project folder)
//                _reportPath = Path.Combine(rootPath, "ExtentReports");
//                Directory.CreateDirectory(_reportPath);

//                var htmlReporter = new ExtentHtmlReporter(Path.Combine(_reportPath, "RethinkEd.html"));
//                _extent = new AventStack.ExtentReports.ExtentReports();
//                _extent.AttachReporter(htmlReporter);
//            }
//            return _extent;
//        }

//        public static void Flush()
//        {
//            _extent?.Flush();
//        }
//    }
//}
