using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace WindowsNotification
{
    public class WindowsToast
    {
        private ToastNotification myToast;

        public WindowsToast(string Notification)
        {
            // Get a toast XML template
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            // Fill in the text elements
            XmlNodeList stringElements = toastXml.GetElementsByTagName("text");
                stringElements[0].AppendChild(toastXml.CreateTextNode("Growl"));
                stringElements[1].AppendChild(toastXml.CreateTextNode(Notification));


            // Specify the absolute path to an image
            //String imagePath = "file:///" + Path.GetFullPath("toastImageAndText.png");
            XmlNodeList imageElements = toastXml.GetElementsByTagName("image");

            myToast = new ToastNotification(toastXml);
        }

        public void ShowToast()
        {
            ToastNotificationManager.CreateToastNotifier("Growl").Show(myToast);
        }
    }
}
