using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eagle.WebApp.Areas.Admin.Controllers.Services.Mail
{
    public class MailServerProviderController : BaseController
    {
        //
        // GET: /Admin/MailServerProvider/

        public ActionResult Index()
        {
            return View();
        }

        //private void PopulateTabList()
        //{
        //    string tab_lst = string.Empty, li_content = string.Empty, div_content = string.Empty, Mail_Server_Provider_Name = string.Empty,
        //        Mail_Server_Protocol = string.Empty, Incoming_Mail_Server_Host = string.Empty, Outgoing_Mail_Server_Host = string.Empty, SSL = string.Empty, TLS = string.Empty;
        //    int? Incoming_Mail_Server_Port = 0, Outgoing_Mail_Server_Port = 0; int Mail_Server_Provider_Id = 0;

        //    List<Mail_Server_Providers> lst = MailServerProviderRespository.GetList();
        //    if (lst.Count > 0)
        //    {
        //        foreach (var item in lst)
        //        {
        //            SSL = (item.SSL == "0" ? "" : "checked");
        //            TLS = (item.TLS == "0" ? "" : "checked");

        //            Mail_Server_Provider_Id = item.Mail_Server_Provider_Id;
        //            Mail_Server_Provider_Name = item.Mail_Server_Provider_Name;
        //            Mail_Server_Protocol = item.Mail_Server_Protocol;

        //            Incoming_Mail_Server_Host = item.Incoming_Mail_Server_Host;
        //            Incoming_Mail_Server_Port = item.Incoming_Mail_Server_Port;
        //            Outgoing_Mail_Server_Host = item.Outgoing_Mail_Server_Host;
        //            Outgoing_Mail_Server_Port = item.Outgoing_Mail_Server_Port;



        //            li_content += "<li><a href=\"#tabs-" + Mail_Server_Provider_Id + "\">" + Mail_Server_Provider_Name + "</a></li>";
        //            div_content += "<div id=\"tabs-" + Mail_Server_Provider_Id + "\">"
        //                                + "<table cellpadding=\"2\" cellspacing=\"2\" width=\"100%\">"
        //                                    + "<tr>"
        //                                        + "<td>* Mail Server Provider</td><td><input id=\"input_Mail_Server_Provider_Name_" + Mail_Server_Provider_Id + "\" type=\"text\" value='" + Mail_Server_Provider_Name + "' /></td>"
        //                                        + "<td>* Mail Server Protocol Type:</td>"
        //                                        + "<td>" + createCombobox(Mail_Server_Provider_Id, Mail_Server_Protocol) + "</td>"
        //                                    + "</tr>"
        //                                    + "<tr>"
        //                                        + "<td colspan=\"2\">Incoming_Mail_Server (POP3/IMAP)</td>"
        //                                        + "<td colspan=\"2\">Outcoming_Mail_Server (SMTP)</td>"
        //                                    + "</tr>"
        //                                    + "<tr>"
        //                                        + "<td>Incoming Host</td><td><input id=\"input_Incoming_Mail_Server_Host_" + Mail_Server_Provider_Id + "\" type=\"text\" value='" + Incoming_Mail_Server_Host + "' /></td>"
        //                                        + "<td>Outgoing Host</td><td><input id=\"input_Outgoing_Mail_Server_Host_" + Mail_Server_Provider_Id + "\" type=\"text\" value='" + Outgoing_Mail_Server_Host + "' /></td>"
        //                                    + "</tr>"
        //                                    + "<tr>"
        //                                        + "<td>Port</td><td><input id=\"input_Incoming_Mail_Server_Port_" + Mail_Server_Provider_Id + "\" type=\"text\" value='" + Incoming_Mail_Server_Port + "' /></td>"
        //                                        + "<td>Outcoming Port</td><td><input id=\"input_Outgoing_Mail_Server_Port_" + Mail_Server_Provider_Id + "\" type=\"text\" value='" + Outgoing_Mail_Server_Port + "' /></td>"
        //                                    + "</tr>"
        //                                    + "<tr><td>&nbsp;</td><td>&nbsp;</td></tr>"
        //                                    + "<tr><td>* SSL :</td><td><input id=\"chk_SSL_" + Mail_Server_Provider_Id + "\" type=\"checkbox\" " + SSL + "/></td></tr>"
        //                                    + "<tr><td>* TLS :</td><td><input id=\"chk_TLS_" + Mail_Server_Provider_Id + "\" type=\"checkbox\" " + TLS + "/></td></tr>"
        //                                    + "<tr><td>&nbsp;</td><td align='right'><input id=\"btnSubmit\" type=\"button\" value=\"Cập nhật\" onclick=\"UpdateMailServerProvider(" + Mail_Server_Provider_Id + ")\" /></td></tr>"
        //                                + "</table>"
        //                           + "</div>";
        //        }
        //    }
        //    else
        //    {
        //        li_content = "Khong co du lieu";
        //        div_content = "Khong co du lieu";
        //    }


        //    tab_lst = "<ul class=\"left_tab_list\">"
        //                      + li_content
        //                    + "</ul>"
        //                    + div_content;
        //    tabs.InnerHtml = tab_lst;
        //}

        //private string createCombobox(int Mail_Server_Provider_Id, string Mail_Server_Protocol)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("<select id='cbo_Protocal_Type_" + Mail_Server_Provider_Id + "' >");
        //    sb.AppendLine("<option value='POP3' ");
        //    sb.AppendLine(Mail_Server_Protocol == "POP3" ? "selected='selected'" : "");
        //    sb.AppendLine(">POP3</option>");
        //    sb.AppendLine("<option value=\"IMAP\" ");
        //    sb.AppendLine(Mail_Server_Protocol == "IMAP" ? "selected='selected'" : "");
        //    sb.AppendLine(">IMAP</option>");
        //    sb.AppendLine("</select>");
        //    return sb.ToString();
        //}


        //protected void btnAdd_Click(object sender, EventArgs e)
        //{
        //    string Mail_Server_Provider_Name = txtMailServerProviderName.Text;
        //    string Mail_Server_Protocol = ddlMailServerProtocol.SelectedValue;
        //    string Incoming_Mail_Server_Host = txtIncomingMailServerHost.Text;
        //    int Incoming_Mail_Server_Port = Convert.ToInt32(txtIncomingMailServerPort.Text);
        //    string Outgoing_Mail_Server_Host = txtOutgoingMailServerHost.Text;
        //    int Outgoing_Mail_Server_Port = Convert.ToInt32(txtOutgoingMailServerPort.Text);
        //    string SSL = chkSSL.Checked == true ? "1" : "0";
        //    string TLS = chkTLS.Checked == true ? "1" : "0";
        //    int result = MailServerProviderController.Insert(Mail_Server_Provider_Name, Mail_Server_Protocol, Incoming_Mail_Server_Host, Incoming_Mail_Server_Port,
        //        Outgoing_Mail_Server_Host, Outgoing_Mail_Server_Port, SSL, TLS);

        //    if (result == 1)
        //    {
        //        ClearContent();
        //        PopulateTabList();
        //        message_box.InnerHtml = "Cập nhật thành công";
        //    }
        //    else
        //    {
        //        message_box.InnerHtml = "Lỗi xảy ra";
        //    }

        //}

        //private void ClearContent()
        //{
        //    txtMailServerProviderName.Text = "";
        //    ddlMailServerProtocol.SelectedIndex = 0;
        //    txtIncomingMailServerHost.Text = "";
        //    txtIncomingMailServerPort.Text = "25";
        //    txtOutgoingMailServerHost.Text = "";
        //    txtOutgoingMailServerPort.Text = "25";
        //    chkSSL.Checked = false;
        //    chkTLS.Checked = false;
        //}
    }
}
