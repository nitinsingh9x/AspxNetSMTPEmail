Imports System.Net.Mail
Imports System.Configuration
Imports System.IO
Partial Class VB
    Inherits System.Web.UI.Page
    Protected Sub SendEmail(ByVal sender As Object, ByVal e As EventArgs)
        Dim body As String = Me.PopulateBody("John", _
            "Fetch multiple values as Key Value pair in ASP.Net AJAX AutoCompleteExtender", _
            "http://www.aspsnippets.com/Articles/Fetch-multiple-values-as-Key-Value" & _
            "-pair-in-ASP.Net-AJAX-AutoCompleteExtender.aspx", _
            ("Here Mudassar Ahmed Khan has explained how to fetch multiple column values i.e. ID and Text values in" & _
            " the ASP.Net AJAX Control Toolkit AutocompleteExtender" & _
            "and also how to fetch the select text and value server side on postback"))
        Me.SendHtmlFormattedEmail("nitin.singh@yardi.com", "New article published!", body)
    End Sub

    Private Function PopulateBody(ByVal userName As String, ByVal title As String, ByVal url As String, ByVal description As String) As String
        Dim body As String = String.Empty
        Dim reader As StreamReader = New StreamReader(Server.MapPath("~/EmailTemplate.htm"))
        body = reader.ReadToEnd
        body = body.Replace("{UserName}", userName)
        body = body.Replace("{Title}", title)
        body = body.Replace("{Url}", url)
        body = body.Replace("{Description}", description)
        Return body
    End Function

    Private Sub SendHtmlFormattedEmail(ByVal recepientEmail As String, ByVal subject As String, ByVal body As String)
        Dim mailMessage As MailMessage = New MailMessage
        mailMessage.From = New MailAddress(ConfigurationManager.AppSettings("UserName"))
        mailMessage.Subject = subject
        mailMessage.Body = body
        mailMessage.IsBodyHtml = True
        mailMessage.To.Add(New MailAddress(recepientEmail))
        Dim smtp As SmtpClient = New SmtpClient
        smtp.Host = ConfigurationManager.AppSettings("Host")
        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings("EnableSsl"))
        Dim NetworkCred As System.Net.NetworkCredential = New System.Net.NetworkCredential
        NetworkCred.UserName = ConfigurationManager.AppSettings("UserName")
        NetworkCred.Password = ConfigurationManager.AppSettings("Password")
        smtp.UseDefaultCredentials = True
        smtp.Credentials = NetworkCred
        smtp.Port = Integer.Parse(ConfigurationManager.AppSettings("Port"))
        smtp.Send(mailMessage)
    End Sub


End Class
