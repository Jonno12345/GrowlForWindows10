using System;
using Growl.DisplayStyle;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace GrowlWindows10Notifier
{
    public class Windows10Notifier : Display
    {
        internal const string SETTING_FILE_NAME = "FILENAME";

        public Windows10Notifier()
        {
            // make sure we associate the proper SettingsPanel with our display
            this.SettingsPanel = new SampleSettingsPanel();

            // NOTE: if you do not set the .SettingsPanel property, a default settings panel
            // with no customization options will be used instead
        }

        /// <summary>
        /// This is the name that will appear in the Growl client to identify this display
        /// </summary>
        public override string Name
        {
            get { return "Windows 10 Notification"; }
        }

        /// <summary>
        /// This description will appear in the Growl client to describe what this display does.
        /// It should be no longer than about 20 or 30 words.
        /// </summary>
        public override string Description
        {
            get { return "Shows notifications to the Windows 10 Action Centre"; }
        }

        public override string Author
        {
            get { return "Jonno"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override string Website
        {
            get { return "http://stackexchange.com/users/2455616/"; }
        }

        /// <summary>
        /// This is where we actually deal with the notification. The Growl client will call this method
        /// when a notification is received that is configured to use this display.
        /// </summary>
        protected override void HandleNotification(Notification notification, string displayName)
        {
            // read the filename from the settings
            string filename = (string)this.SettingsCollection[SETTING_FILE_NAME];

            if (!String.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                var startInfo = new ProcessStartInfo()
                {
                    FileName = filename,
                    Arguments = "\"" + notification.Description + "\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                };
                Process.Start(startInfo);
            } else
            {
                MessageBox.Show("Unable to locate WindowsNotification.exe! File path specified: " + filename, "Unable to find support EXE", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }

        public override void CloseAllOpenNotifications()
        {
            // since we dont have 'open' notifications, we dont have to do anything here
        }

        public override void CloseLastNotification()
        {
            // since we dont have 'open' notifications, we dont have to do anything here
        }

        // since our display can't be clicked, we dont need to do anything else with these events
        public override event Growl.CoreLibrary.NotificationCallbackEventHandler NotificationClicked;
        public override event Growl.CoreLibrary.NotificationCallbackEventHandler NotificationClosed;
    }
}
