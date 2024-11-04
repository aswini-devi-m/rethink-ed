using Microsoft.Extensions.Configuration;


namespace RethinkEd_Automation.Utilities
{
    public static class GlobalAppSettings
    {
        public static IConfiguration Configuration { get; private set; }
        // Property to hold the folder path
        public static string _dataFolderPath { get; private set; }

        public static string _filename { get; private set; }
        public static string _foldername { get; private set; }

        static GlobalAppSettings()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            _filename = Configuration["test_data:file_name"];
            _foldername = Configuration["test_data:folder_name"];

            // Get the current directory
            var currentDirectory = Directory.GetCurrentDirectory();

            // Find the index of the "bin" folder
            var binIndex = currentDirectory.IndexOf("bin", StringComparison.OrdinalIgnoreCase);

            // If "bin" is found, take the path before it
            string rootPath;
            if (binIndex >= 0)
            {
                rootPath = currentDirectory.Substring(0, binIndex);
            }
            else
            {
                rootPath = currentDirectory; // Fallback to current if "bin" not found
            }

            // Define the folder path (e.g., "Data" in the root project folder)
            _dataFolderPath = Path.Combine(rootPath, _foldername);

            // Create the folder if it does not exist
            if (!Directory.Exists(_dataFolderPath))
            {
                Directory.CreateDirectory(_dataFolderPath);
            }

            var csvFilePath = Path.Combine(_dataFolderPath, _filename);
            using (var stream = new FileStream(csvFilePath, FileMode.Create, FileAccess.Write))
            {
                // This will truncate the file if it exists or create a new one
            }

        }

        public static string GetBaseUrl()
        {
            return Configuration["UserCredentials:BaseUrl"];
        }

        public static string GetUsername()
        {
            return Configuration["UserCredentials:Username"];
        }

        public static string GetPassword()
        {
            return Configuration["UserCredentials:Password"];
        }
        public static string iterations()
        {
            return Configuration["iterations"];
        }

        // Method to get the CSV file path
        public static string GetCsvfilePath()
        {
            return Path.Combine(_dataFolderPath, _filename);
            ;
        }
    }
}

