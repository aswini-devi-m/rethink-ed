using NUnitLite;
using System;

namespace PlaywrightTests
{
    public class Program
    {
        public static int Main(string[] args)
        {
            // Run the tests programmatically with NUnitLite
            return new AutoRun().Execute(args);
        }
    }
}
