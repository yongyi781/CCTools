using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CCTools.CCDesign
{
    static class CCTest
    {
        public const string ChipsChallengeTestDirectory = @"C:\Temp\CCTest";

        static Process ccTestProcess = null;

        public static string ChipsChallengeDirectory
        {
            get { return Path.GetDirectoryName(Properties.Settings.Default.ChipsChallengeLocation); }
        }

        public static void Test(LevelSet levelSet, Level level)
        {
            if (ccTestProcess != null && !ccTestProcess.HasExited)
            {
                ccTestProcess.Kill();
            }

            var chipsExeFile = Properties.Settings.Default.ChipsChallengeLocation;
            var sourcePath = ChipsChallengeDirectory;
            var destPath = ChipsChallengeTestDirectory;
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);
            levelSet.Save(Path.Combine(destPath, "CCT_Data"));
            var newChipsExeLocation = Path.Combine(destPath, @"CCTest.exe");
            CopyFiles(sourcePath, destPath);
            try
            {
                File.Copy(chipsExeFile, newChipsExeLocation, true);
                using (var fs = new FileStream(newChipsExeLocation, FileMode.Open, FileAccess.Write, FileShare.Read))
                {
                    fs.Seek(0x4a68, SeekOrigin.Begin);
                    var buffer1 = new byte[] { 0x43, 0x43, 0x54, 0x65, 0x6D, 0x70, 0x2E, 0x69, 0x6E, 0x69, 0x00, 0x00, 0x63, 0x68, 0x69, 0x70, 0x73, 0x20, 0x74, 0x65, 0x6D, 0x70, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x00 };
                    fs.Write(buffer1, 0, buffer1.Length);

                    fs.Seek(0x4ad4, SeekOrigin.Begin);
                    var buffer2 = new byte[] { 0x43, 0x43, 0x54, 0x5F, 0x44, 0x61, 0x74, 0x61, 0x00 };
                    fs.Write(buffer2, 0, buffer2.Length);
                }
            }
            catch (IOException) { throw new ApplicationException("Please close any existing instances of Chip's Challenge before testing."); }
            var windowsDirectory = Properties.Settings.Default.WindowsDirectory;
            if (!Directory.Exists(windowsDirectory))
                Directory.CreateDirectory(windowsDirectory);
            var ccTempPath = Path.Combine(windowsDirectory, "CCTemp.ini");
            var ccTemp = level == null ? Properties.Resources.CCTemp.Replace("Level{0}={1}\r\nCurrent Level={0}\r\n", string.Empty) : string.Format(CultureInfo.CurrentCulture, Properties.Resources.CCTemp, level.Index + 1, level.Password);
            File.WriteAllText(ccTempPath, ccTemp, Encoding.Default);
            ccTestProcess = Process.Start(new ProcessStartInfo { WorkingDirectory = destPath, FileName = Path.Combine(destPath, "CCTest.exe") });
        }

        static void CopyFiles(string sourcePath, string destPath)
        {
            var chipsExeFile = Properties.Settings.Default.ChipsChallengeLocation;
            if (!File.Exists(chipsExeFile))
                throw new FileNotFoundException(Properties.Resources.ChipsChallengeNotFound, chipsExeFile);
            if (!Directory.Exists(destPath))
                Directory.CreateDirectory(destPath);
            var wep4utilPath = Path.Combine(sourcePath, "wep4util.dll");
            if (!File.Exists(wep4utilPath))
                throw new FileNotFoundException("wep4util.dll was not found in " + sourcePath + ". Please check to make sure the file exists.", wep4utilPath);
            var wep4utilDestPath = Path.Combine(destPath, "wep4util.dll");
            if (!File.Exists(wep4utilDestPath))
                File.Copy(wep4utilPath, wep4utilDestPath, true);

            CopyAudioFiles();
        }

        static void CopyAudioFiles()
        {
            CopyFile("blip2.wav");
            CopyFile("door.wav");
            CopyFile("bummer.wav");
            CopyFile("ditty1.wav");
            CopyFile("oof3.wav");
            CopyFile("strike.wav");
            CopyFile("chimes.wav");
            CopyFile("click3.wav");
            CopyFile("pop2.wav");
            CopyFile("water2.wav");
            CopyFile("hit3.wav");
            CopyFile("teleport.wav");
            CopyFile("click1.wav");
            CopyFile("bell.wav");
            CopyFile("chip01.mid");
            CopyFile("chip02.mid");
        }

        static void CopyFile(string fileName)
        {
            var source = Path.Combine(ChipsChallengeDirectory, fileName);
            if (!File.Exists(source))
                return;
            var dest = Path.Combine(ChipsChallengeTestDirectory, fileName);
            File.Copy(source, dest, true);
        }
    }
}
