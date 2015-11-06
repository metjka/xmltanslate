using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace trans {
    public class YandexTranslate {
        private static string _yandexApiKey =
            "trnsl.1.1.20151021T164248Z.4105ddc6a622d9b7.282e50fe8f1fca99b38e3894cb29ea37fac3d144";

        private static readonly string YandexUri = "https://translate.yandex.net/api/v1.5/tr/translate?key=";
        private string lang;
 
        public YandexTranslate(string key, string langFrom,string langTo) {
            _yandexApiKey = key;
            lang = langFrom + "-" + langTo;

        }

        public YandexTranslate(string key) {
            _yandexApiKey = key;
        }

        public static string Translate(string lang, string text) {
            WebRequest request = WebRequest.Create(YandexUri + _yandexApiKey + "&lang=" + lang + "&text=" + text);
            request.Timeout = 10000;
            string fetchedXml;
            string OutPut;
            try {
                WebResponse response = request.GetResponse();

                StreamReader sr = new StreamReader(response.GetResponseStream());
                fetchedXml = sr.ReadToEnd();
                XmlDocument d = new XmlDocument();
                d.LoadXml(fetchedXml);
                XmlNodeList textNodes = d.GetElementsByTagName("text");
                OutPut = textNodes[0].InnerText;
            }
            catch (Exception e) {
                MessageBox.Show("Internet problem, or you already used your limit!");
                return String.Empty;
            }
            return OutPut;
        }
    }

}