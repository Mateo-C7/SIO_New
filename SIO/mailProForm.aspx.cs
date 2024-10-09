using CapaControl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using CapaControl.Entity;
using Microsoft.Reporting.WebForms;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Threading;
using System.Xml.XPath;
using System.Text;
using System.Xml;

namespace SIO
{
	public partial class mailProForm : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
            string ApiKey = "b6c21791-6d1c-4cbe-b3c7-49d3aca46eeb"; //On demand  
            string IDClient = "129113";
            string MessageBody = "<html><head></head><body>Send from API</body></html>";
            string Subject = "Send from API with ASP.NET";
            string email = "fjsalazar@global-bi.net";
            string IDEmailExp = GetEmailSenders(ApiKey, IDClient);
            string lista = GetAddressBook(ApiKey, IDClient);
            /*            string addressBookID = AddAddressBook(ApiKey, IDClient);
                        AddEmail (ApiKey,  IDClient,  email,  addressBookID);
                        string messageID = AddMessage(ApiKey, IDClient, Subject, MessageBody);
                        AddSend(ApiKey, IDClient, messageID, IDEmailExp, addressBookID);
                        */

        }

		[WebMethod]
        public string GetEmailSenders(string ApiKey, string IDClient) {
            string TagValue = "//ExpEmails/ExpEmail";
            string NodeValue = "ExpEmailId";
            string apiUrl = "https://api.mailpro.com/v2/senderEmail/list.xml?apiKey=" + ApiKey + "&IDClient=" + IDClient;
            string IDEmailExp = methodGet(ApiKey, IDClient, TagValue, NodeValue, apiUrl);
            return IDEmailExp;
        }

        [WebMethod]
        public string GetAddressBook(string ApiKey, string IDClient)
        {
            string TagValue = "//AddressBooks/AddressBook";
            string NodeValue = "Title";
            string apiUrl = "https://api.mailpro.com/v2/addressbook/list.xml?apiKey=" + ApiKey + "&IDClient=" + IDClient;
            string IDEmailExp = methodGet(ApiKey, IDClient, TagValue, NodeValue, apiUrl);
            return IDEmailExp;
        }

        [WebMethod]
        public string AddAddressBook(string ApiKey, string IDClient) {
            string PostTagValue = "//AddressBooks/AddressBook/AddressBookId";
            string PostUrl = "https://api.mailpro.com/v2/addressbook/add.xml";
            string postData = "APIKey=" + ApiKey + "&IDClient=" + IDClient + "&Title=PruebaForsa";
            string addressBookID = methodPostReturn(PostTagValue, postData, PostUrl);
            return addressBookID;
        }

        [WebMethod]
        public void AddEmail(string ApiKey, string IDClient, string email, string addressBookID)
        {
            string postData = "APIKey=" + ApiKey + "&IDClient=" + IDClient +
            "&AddressBookID=" + addressBookID + "&emailList=" + email;
            string PostUrl = "https://api.mailpro.com/v2/email/add.xml?";
            methodPost(postData, PostUrl);
        }

        [WebMethod]
        public string AddMessage(string ApiKey, string IDClient, string Subject, string MessageBody)
        {
            string PostTagValue = "//Message/MessageId";
            string PostUrl = "https://api.mailpro.com/v2/message/add.xml";
            string postData = "APIKey=" + ApiKey + "&IDClient=" + IDClient + "&Subject="
            + Subject + "&BodyHTML=" + MessageBody + "&Language=EN&linkAlign=center&LinkUp=1";
            string IDMessage = methodPostReturn(PostTagValue, postData, PostUrl);
            return IDMessage;
        }

        [WebMethod]
        public void AddSend(string ApiKey, string IDClient, string messageID, string  IDEmailExp, string addressBookID)
        {
            string PostUrl = "https://api.mailpro.com/v2/send/add.xml";
            string postData = "APIKey=" + ApiKey + "&IDClient=" + IDClient +
            "&IDAddressBook=" + addressBookID + "&IDMessage=" + messageID + "&IDEmailExp=" +
            IDEmailExp + "&Campaign=1";
            methodPost(postData, PostUrl);
        }

        [WebMethod]
        public string methodGet(string ApiKey, string IDClient, string TagValue, string NodeValue, string apiUrl)
        {
            Uri address = new Uri(apiUrl);
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/xml";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string strOutputXml = reader.ReadToEnd();
                XPathDocument doc = new XPathDocument(apiUrl);
                XPathNavigator nav = doc.CreateNavigator();
                XPathNodeIterator iter = (XPathNodeIterator)nav.Evaluate(TagValue);
                while (iter.MoveNext())
                {
                    Response.Write("" +
                    iter.Current.SelectSingleNode(NodeValue).Value + " <br />");
                }
                string myvalue = iter.Current.SelectSingleNode(NodeValue).Value;
                return myvalue;
            }
        }

        [WebMethod]
        public void methodPost(string postData, string PostUrl)
        {
            WebRequest requestPost = WebRequest.Create(PostUrl);
            requestPost.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            requestPost.ContentType = "application/x-www-form-urlencoded";
            requestPost.ContentLength = byteArray.LongLength;
            
            Stream dataStream = requestPost.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse responsePost = requestPost.GetResponse();
            Console.WriteLine(((HttpWebResponse)responsePost).StatusDescription);
            dataStream = responsePost.GetResponseStream();
            StreamReader readerPost = new StreamReader(dataStream);
            string responseFromServer = readerPost.ReadToEnd();
            readerPost.Close();
            dataStream.Close();
            responsePost.Close();
        }

        [WebMethod]
        public string methodPostReturn(string PostTagValue, string postData, string PostUrl)
        {
            WebRequest requestPost = WebRequest.Create(PostUrl);
            requestPost.Method = "POST";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            requestPost.ContentType = "application/x-www-form-urlencoded";
            requestPost.ContentLength = byteArray.LongLength;
            Stream dataStream = requestPost.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse responsePost = requestPost.GetResponse();
            Console.WriteLine(((HttpWebResponse)responsePost).StatusDescription);
            dataStream = responsePost.GetResponseStream();
            StreamReader readerPost = new StreamReader(dataStream);
            string responseFromServer = readerPost.ReadToEnd();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(responseFromServer);
            XPathNavigator nav = doc.CreateNavigator();
            XPathNavigator node = nav.SelectSingleNode(PostTagValue);
            StringWriter sw = new StringWriter();
            XmlTextWriter tx = new XmlTextWriter(sw);
            doc.WriteTo(tx);
            string str = node.ToString();
            readerPost.Close();
            dataStream.Close();
            responsePost.Close();
            return str;
        }
    }
}