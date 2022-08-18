using System;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Blue;
            for (int i = 0; i <= 100; i++)
            {
                Console.Write("\r{0}% Loading...", i);
                Thread.Sleep(50);
            }
            Console.ResetColor();
            Console.Clear();
            string cfgPath = @"c:\SSM\Software configuration file.ini";
            string sdPath = @"c:\SSM";
            string iFolder = "", iDeveloper = "", iVer = "";
            if (!Directory.Exists(sdPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(sdPath);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(@"c:\SSM"));
            }
            if (!File.Exists(cfgPath))
            {
                // Create a file to write to. 
                using (StreamWriter sw = File.CreateText(cfgPath))
                {
                    sw.WriteLine("SSM Configuration file");
                    sw.WriteLine("Folder=C:\\Users\\user\\Desktop\\SAMPYL");
                    sw.WriteLine("Version=v1.0RC2");
                    sw.WriteLine("Developer=_EvilBoy_");
                    Console.WriteLine("The file was created successfully at {0}.", File.GetCreationTime(cfgPath));
                }
            }
            // Open the file to read from. 
            using (StreamReader sr = File.OpenText(cfgPath))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                    if (s.Contains("Folder=")) iFolder = s.Substring(7);
                    else if (s.Contains("Version=")) iVer = s.Substring(8);
                    else if (s.Contains("Developer=")) iDeveloper = s.Substring(10);
            }
            DateTime thisDate = DateTime.Now;
            Console.WriteLine("\t\t\t\t\t\t\t  " + thisDate);
            Console.WriteLine();
	        Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\t\t\t- SA-MP Server Manager -\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("SSM Program loaded sucessfully. (»{0})\n\tDeveloped by {1}\nThis software allows you to manage the files servers your SA-MP\nCurrently the software is run DOS interface\n", iVer, iDeveloper);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\t\t* System Info *");
            Console.WriteLine(
                "Program Directory: \"{0}\"\nProgram Configuration File: \"{1}\"\nInstallation Folder is: \"{2}\"\n"
                , sdPath, cfgPath, iFolder);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("To start type 'start'. \n\totherwise the program will be closed");
            Console.WriteLine();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(".. ");
            if (Console.ReadLine().ToUpper().Equals("start".ToUpper()))
            {
                Console.Clear();
                goto StartGUI;
            }
            else Environment.Exit(0);

        StartGUI:
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\t\t\t- Graphical User Interface -");
                Console.WriteLine("This part only works on the installation folder.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("CMDs: (List of commands)");
                Console.WriteLine();
                Console.WriteLine("/LOF - View list of files");
                Console.WriteLine("/LOD - View list of sub directories");
                Console.WriteLine("/DownloadSSF - Download SA-MP Server folder from \"www.SA-MP.com\"");
                Console.WriteLine("/CreateSSF - Create SA-MP Server folder directory\n");
                Console.WriteLine("/Credits - Credits list");
                if (Console.ReadLine().ToUpper().Equals("/LOF".ToUpper()))
                {
                    Console.WriteLine("List of files:\n");
                    string[] filePaths = Directory.GetFiles(iFolder);
                    int count = 0;
                    foreach (string file in filePaths)
                    {
                        Console.WriteLine("\t-" + Path.GetFileName(file));
                        count++;
                    }
                    Console.WriteLine(count == 0? ("No files found in the folder") : ("{0} files found in the folder"), count);
                }
                Console.WriteLine();
                if (Console.ReadLine().ToUpper().Equals("/LOD".ToUpper()))
                {
                    Console.WriteLine("List of subdirectories:\n");
                    string[] dirPaths = Directory.GetDirectories(iFolder);
                    int i = 0;
                    foreach (string dir in dirPaths)
                    {
                        Console.WriteLine("\t-" + (dir));
                        i++;
                    }
                    Console.WriteLine(i == 0 ? ("No directories found in the folder") : ("{0} directories found in the folder"), i);
                }
                Console.WriteLine();
                if (Console.ReadLine().ToUpper().Equals("/createssf".ToUpper()))
                {
                    Console.WriteLine("To Generate SA-MP Server Folder directory,\tType 'Go'");
                    Console.WriteLine();
                    if (Console.ReadLine().ToUpper().Equals("Go".ToUpper()))
                    {
                        string dest = "";
                        Console.WriteLine("Please enter a name for new SA-MP Folder");
                        goto ssfName;
                    ssfName:
                        {
                            dest = @"C:\Users\LironChuK_2\Desktop\SAMPYL\" + Console.ReadLine();
                            if (Directory.Exists(dest))
                            {
                                Console.WriteLine("Directory exists, Please enter another name ");
                                goto ssfName;
                            }
                        }
                        DirectoryCopy(@"C:\SSM\samp03x_svr_R2_win32_SSM", dest, true);
                        Console.WriteLine("The directory {0} was created successfully.", dest);
                    }
                }
                if (Console.ReadLine().ToUpper().Equals("/downloadssf".ToUpper()))
                {
                    Console.WriteLine("To download SA-MP Server Folders automaticaly to directory,\n\tWrite 'Download'");
                    if (Console.ReadLine().Equals("Download"))
                    {
                        Console.WriteLine("Downloading..");
                        using (WebClient wc = new WebClient())
                        {
                            wc.DownloadFile("http://files.sa-mp.com/samp03x_svr_R2_win32.zip", iFolder);
                            Console.WriteLine("\"samp03x_svr_R2_win32\" Downloaded sucessfully.");
                        }
                    }
                }
                // Credits command.
                Console.ReadKey();
            }
        }
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
