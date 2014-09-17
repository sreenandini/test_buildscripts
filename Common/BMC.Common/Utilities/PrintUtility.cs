using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BMC.Common.Utilities
{
    public class PrintUtility
    {
        public void Print(DataTable lineHeader, DataTable lineItem, string fileName)
        {
            try
            {
                var srTemplateFile = new StreamReader(File.OpenRead(fileName));
                var sbFileContent = new StringBuilder(srTemplateFile.ReadToEnd());
                var sbNewFileContent = new StringBuilder();

                if (!Directory.Exists(@"c:\temp"))
                    Directory.CreateDirectory(@"c:\temp");

                var tempfilename = @"c:\temp\~print_" + DateTime.Now.ToFileTime() + ".html";
                var tempFile = new StreamWriter(tempfilename, true, Encoding.UTF8);

                /////////////////////////----Extracting Header------////////////////////////

                var expressionMatch = new Regex(@"(\w|\b|^|$|\B|\n|\r|\t|\s|\S|\W|\d|\D|)*{{{{HeaderEnd}}}}", RegexOptions.IgnoreCase);
                var matches = expressionMatch.Matches(sbFileContent.ToString());

                foreach (Match match in matches)
                {
                    var sbNewFile = new StringBuilder(match.Value);
                    sbNewFile.Replace("{{{{HeaderBegin}}}}", "");
                    sbNewFile.Replace("{{{{HeaderEnd}}}}", "");

                    var innerExpression = new Regex(@"{{(\w)*}}", RegexOptions.IgnoreCase);
                    foreach (Match matchingEntity in innerExpression.Matches(sbNewFile.ToString()))
                    {
                        var iDataColumn = lineHeader.Columns.IndexOf(matchingEntity.Value.Replace("{{", "").Replace("}}", ""));
                        if (iDataColumn != -1)
                            sbNewFile.Replace(matchingEntity.Value, lineHeader.Rows[0][iDataColumn].ToString());
                        else
                            sbNewFile.Replace(matchingEntity.Value, "");
                    }

                    sbNewFileContent.Append(sbNewFile.ToString());
                    break;
                }

                /////////////////////////----Extracting Lines Between Header End and line Item------////////////////////////

                expressionMatch = new Regex(@"{{{{HeaderEnd}}}}(\w|\b|^|$|\B|\n|\r|\t|\s|\S|\W|\d|\D|)*{{{{LineItemBegin}}}}",
                                            RegexOptions.IgnoreCase);
                matches = expressionMatch.Matches(sbFileContent.ToString());

                foreach (Match match in matches)
                {
                    var fileAppender = new StringBuilder(match.Value);
                    fileAppender.Replace("{{{{HeaderEnd}}}}", "");
                    fileAppender.Replace("{{{{LineItemBegin}}}}", "");

                    sbNewFileContent.Append(fileAppender.ToString());
                    break;
                }

                /////////////////////////----Extracting Lines Between LineItem Begin and LineItem End------////////////////////////

                expressionMatch = new Regex(@"{{{{LineItemBegin}}}}(\w|\b|^|$|\B|\n|\r|\t|\s|\S|\W|\d|\D|)*{{{{LineItemEnd}}}}",
                                            RegexOptions.IgnoreCase);
                matches = expressionMatch.Matches(sbFileContent.ToString());

                foreach (Match match in matches)
                {
                    var fileAppender = new StringBuilder(match.Value);
                    fileAppender.Replace("{{{{LineItemEnd}}}}", "");
                    fileAppender.Replace("{{{{LineItemBegin}}}}", "");

                    foreach (DataRow row in lineItem.Rows)
                    {
                        var tempStringBuilder = new StringBuilder(fileAppender.ToString());
                        var innerExpression = new Regex(@"{{(\w)*}}", RegexOptions.IgnoreCase);
                        foreach (Match matchingEntity in innerExpression.Matches(tempStringBuilder.ToString()))
                        {
                            var iDataColumn = lineItem.Columns.IndexOf(matchingEntity.Value.Replace("{{", "").Replace("}}", ""));
                            if (iDataColumn != -1)
                                tempStringBuilder.Replace(matchingEntity.Value, row[iDataColumn].ToString());
                            else
                                tempStringBuilder.Replace(matchingEntity.Value, "");

                        }

                        sbNewFileContent.Append(tempStringBuilder.ToString());
                    }

                    break;

                }

                /////////////////////////----Extracting Lines Between line Item End and <HTML> Tag Finishing Tag------////////////////////////

                expressionMatch = new Regex(@"{{{{LineItemEnd}}}}(\w|\b|^|$|\B|\n|\r|\t|\s|\S|\W|\d|\D|)*</html>",
                                            RegexOptions.IgnoreCase);
                matches = expressionMatch.Matches(sbFileContent.ToString());

                foreach (Match match in matches)
                {
                    var fileAppender = new StringBuilder(match.Value);
                    fileAppender.Replace("{{{{LineItemEnd}}}}", "");

                    sbNewFileContent.Append(fileAppender.ToString());
                    break;
                }

                tempFile.Write(sbNewFileContent.ToString());
                tempFile.Flush();
                tempFile.Close();

                srTemplateFile.Close();
                PrintFunction(tempfilename);

            }
            catch (System.IO.DirectoryNotFoundException dirNotFound)
            {
                
                LogManagement.LogManager.WriteLog(fileName+" Not found"+"\\n Message:"+dirNotFound.Message,LogManagement.LogManager.enumLogLevel.Error);
            }

        }

        private static void PrintFunction(string filepath)
        {
            try
            {
                if (File.Exists(filepath))
                {

                    var webBrowse = new WebBrowser();
                    webBrowse.DocumentCompleted += PrintDoc;
                    webBrowse.Navigate(filepath);
                }
            }
            catch (Exception)
            {

            }


        }
        private static void PrintDoc(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                ((WebBrowser)sender).ShowPrintDialog();
                Cursor.Current = Cursors.Default;
                ((WebBrowser)sender).Dispose();
            }
            catch 
            {

            }
        }
    }
}
