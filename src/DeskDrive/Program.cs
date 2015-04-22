// Copyright (c) 2011 Blue Onion Software, All rights reserved
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlueOnion.Properties;
using Microsoft.Win32;

namespace BlueOnion
{
    internal static class Program
    {
        private static DeskDrive _deskDriveForm;

        [STAThread]
        private static void Main()
        {
            try
            {
                LogInformation("Program started");

                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
                {
                    const int flags = (int) NativeMethods.MessageBroadcastFlags.BSF_POSTMESSAGE;
                    var recipients = (int) NativeMethods.MessageBroadcastRecipients.BSM_APPLICATIONS;
                    NativeMethods.BroadcastSystemMessage(flags, ref recipients, NativeMethods.WM_NOTIFYDD, 0, 0);
                    LogInformation("Second instance detected");
                    return;
                }

                UpgradeSettings();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                SystemEvents.SessionEnding += SystemEventsSessionEnding;
                _deskDriveForm = new DeskDrive();
                Application.Run(_deskDriveForm);
                Desktop.SaveIconPositions();
            }

            catch (Exception ex)
            {
                LogError("Unhandled exception: " + ex);
                throw;
            }

            finally
            {
                LogInformation("Program terminated");
            }
        }

        private static void SystemEventsSessionEnding(object sender, SessionEndingEventArgs e)
        {
            SystemEvents.SessionEnding -= SystemEventsSessionEnding;
            Desktop.SaveIconPositions();
        }

        private static void UpgradeSettings()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var version = assembly.GetName().Version.ToString();

            if (version != Settings.Default.ApplicationVersion)
            {
                Settings.Default.Upgrade();
                var oldVersion = Settings.Default.ApplicationVersion;
                Settings.Default.ApplicationVersion = version;
                Settings.Default.Save();
                LogInformation("Settings upgraded: " + oldVersion + " -> " + version);
            }
        }

        public static void LogError(string message)
        {
            Log(message, EventLogEntryType.Error);
        }

        public static void LogInformation(string message)
        {
            Log(message, EventLogEntryType.Information);
        }

        private static void Log(string message, EventLogEntryType logEntryType)
        {
            Debug.WriteLine("{0} -> {1}", logEntryType, message);
        }
    }
}