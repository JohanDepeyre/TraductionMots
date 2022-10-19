using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TraductionMots.Utils
{
    sealed class HtmlHolder : IDisposable
    {
        // Disable the warning.
#pragma warning disable SYSLIB0014
        WebClient webClient;

        public HtmlHolder()
        {

        }
         public void OpenConnection()
        {

            this.webClient = new WebClient();
        }
        public void Dispose()
        {
            this.webClient.Dispose();
        }

        public  string GetHtml()
        {

            string page = webClient.DownloadString("https://www.lemonde.fr/");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(page);

            var htmlNode = doc.DocumentNode.SelectSingleNode("//p[contains(@class, 'article__desc')]");


            string innerText = htmlNode.InnerText.Replace(",", "");
            
            return innerText;

        }
    }
}
