using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Configuration;
using System.IO;

public partial class _Default : System.Web.UI.Page
{
    protected void SendEmail(object sender, EventArgs e)
    {
        string body = this.PopulateBody("John",
            "Fetch multiple values as Key Value pair in ASP.Net AJAX AutoCompleteExtender",
            "http://www.aspsnippets.com/Articles/Fetch-multiple-values-as-Key-Value-pair-" + 
            "in-ASP.Net-AJAX-AutoCompleteExtender.aspx",
            "Here Mudassar Ahmed Khan has explained how to fetch multiple column values i.e." +
            " ID and Text values in the ASP.Net AJAX Control Toolkit AutocompleteExtender"
            + "and also how to fetch the select text and value server side on postback");
        this.SendHtmlFormattedEmail("recipient@gmail.com", "New article published!", body);
    }

    private string PopulateBody(string userName, string title, string url, string description)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("~/EmailTemplate.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{UserName}", userName);
        body = body.Replace("{Title}", title);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Description}", description);
        return body;
    }

    private void SendHtmlFormattedEmail(string recepientEmail, string subject, string body)
    {
        using (MailMessage mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["UserName"]);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(new MailAddress(recepientEmail));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfigurationManager.AppSettings["Host"];
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
            NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"];
            NetworkCred.Password = ConfigurationManager.AppSettings["Password"];
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
            smtp.Send(mailMessage);
        }
    }
}