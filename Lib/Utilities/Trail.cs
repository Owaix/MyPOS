using System;
using System.IO;

namespace TrialApp
{
    public class Trail
    {
        private string installDateFilePath = "InstallDate.txt"; // Path to store installation date
        private string launchCountFilePath = "LaunchCount.txt"; // Path to store launch count

        public DateTime GetInstallationDate()
        {
            if (File.Exists(installDateFilePath))
            {
                string installDateStr = File.ReadAllText(installDateFilePath);
                return DateTime.Parse(installDateStr);
            }
            else
            {
                DateTime installDate = DateTime.Now;
                File.WriteAllText(installDateFilePath, installDate.ToString());
                return installDate;
            }
        }

        public int GetLaunchCount()
        {
            if (File.Exists(launchCountFilePath))
            {
                string launchCountStr = File.ReadAllText(launchCountFilePath);
                return int.Parse(launchCountStr);
            }
            else
            {
                File.WriteAllText(launchCountFilePath, "1");
                return 1;
            }
        }

        public void SaveLaunchCount(int count)
        {
            File.WriteAllText(launchCountFilePath, count.ToString());
        }
    }
}
