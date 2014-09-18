using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;

using BMC.Transport;
using System.Reflection;
using BMC.Common.ExceptionManagement;

namespace BMC.Business.CashDeskManager
{
    public class PrintUtility
    {
        float yPos;
        float leftMargin;
        float topMargin;
        Font printFont;
      
        char cpad2 = '=';
                char ctab = ' ';
        int number = 50;
        String line = null;
        int PageNo = 1;
        List<TicketExceptions> lstPrintItems = null;
      

        public PrintUtility(List<TicketExceptions> items)
        {
            this.lstPrintItems = items;
        }

        public void GetCommonPrintValues(System.Drawing.Printing.PrintPageEventArgs ev)
        {
            printFont = new Font("Arial", 18, FontStyle.Bold);
            yPos = 0;
            leftMargin = ev.MarginBounds.Left;
            topMargin = ev.MarginBounds.Top;
            ev.PageSettings.Landscape = true;


            try
            {
                line = "Bally One Void Treasury Report" + "  - " + DateTime.Now.ToString("ddd dd mm yyyy hh:nn");
                   
                yPos = topMargin + (printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                printFont = new Font("Arial", 10, FontStyle.Regular);

                line = "Page " + Environment.NewLine + PageNo + Environment.NewLine;
                yPos = topMargin + (4 * printFont.GetHeight(ev.Graphics));
                ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                //line = "Date/Time".PadRight(number, ctab) + System.DateTime.Now.ToShortDateString() + " " + System.DateTime.Now.ToShortTimeString();
                //yPos = topMargin + (10 * printFont.GetHeight(ev.Graphics));
                //ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                //line = "Workstation".PadRight(number, ctab) + System.Environment.MachineName.ToString();
                //yPos = topMargin + (11 * printFont.GetHeight(ev.Graphics));
                //ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());

                foreach (TicketExceptions item in lstPrintItems)
                {
                    foreach (PropertyInfo info in item.GetType().GetProperties())
                    {
                        line = info.Name + " : " + info.GetValue(item, null).ToString();
                        yPos = topMargin + (11 * printFont.GetHeight(ev.Graphics));
                        ev.Graphics.DrawString(line, printFont, Brushes.Black, leftMargin, yPos, new StringFormat());
                    }
                }

            }
            catch (Exception Ex)
            {
                ExceptionManager.Publish(Ex);
            }

            //return ev;
        }
    }
}
