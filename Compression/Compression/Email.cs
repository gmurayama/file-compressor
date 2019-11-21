using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop;

namespace Compression
{
    static class Email
    {
        static Microsoft.Office.Interop.Outlook.Application oApp = new Microsoft.Office.Interop.Outlook.Application();
        static Microsoft.Office.Interop.Outlook.MailItem oMail = (Microsoft.Office.Interop.Outlook.MailItem)oApp.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);

        public static void SendMail(string subject, string Body, string FilePath)
        {
            oMail.Subject = subject;
            oMail.BodyFormat = Microsoft.Office.Interop.Outlook.OlBodyFormat.olFormatHTML;
            oMail.Body = Body;
            oMail.Attachments.Add(FilePath, Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, Type.Missing, Type.Missing);
            oMail.Display(false);
        }

        public static void SendMail(string subject, string Body)
        {
            SendMail(subject, Body, null);
        }

        public static void SendMail(string subject)
        {
            SendMail(subject, null, null);
        }

        public static void SendMail()
        {
            SendMail(null, null, null);
        }

        public static string Cumpliment()
        {
            int hour = System.DateTime.Now.Hour;

            if(hour < 12 && hour >= 6)
                return "Bom dia!";
            if (hour < 18)
                return "Boa Tarde!";
            return "Boa Noite!";
        }

        public static string GetDayMonth()
        {
            return System.DateTime.Now.Day.ToString() + '/' + System.DateTime.Now.Month.ToString();
        }
    }

}
