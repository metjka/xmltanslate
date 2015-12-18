using System;
using System.IO;
using System.Net;
using System.Xml;

namespace TranslatorXml {
    public class YandexTranslate {
        private  string _yandexApiKey;
        private const string YandexUri = "https://translate.yandex.net/api/v1.5/tr/translate?key=";

        public YandexTranslate(string key) {
            _yandexApiKey = key;
        }

        public string Translate(string lang, string text) {
            WebRequest request = WebRequest.Create(YandexUri + _yandexApiKey + "&lang=" + lang + "&text=" + text);
            request.Timeout = 10000;
            string fetchedXml;
            string OutPut;
            try {
                HttpWebResponse response = (HttpWebResponse) request.GetResponse();

                StreamReader sr = new StreamReader(response.GetResponseStream());
                fetchedXml = sr.ReadToEnd();
                XmlDocument d = new XmlDocument();
                d.LoadXml(fetchedXml);
                XmlNodeList textNodes = d.GetElementsByTagName("text");
                OutPut = textNodes[0].InnerText;
            }
            catch (WebException exe) {
                int a = (int) ((HttpWebResponse) exe.Response).StatusCode;
                switch (a) {
                    case 404:
                        throw new Exception("No internet conection or exceeded the daily limit" +
                                            " on the amount of translated text!");
                    case 401:
                        throw new Exception("Invalid API key!");
                    case 402:
                        throw new Exception("Blocked API key!");
                    case 403:
                        throw new Exception("Exceeded the daily limit on the number of requests!");
                    case 413:
                        throw new Exception("Exceeded the maximum text size!");
                    case 422:
                        throw new Exception("The text cannot be translated!");
                    case 501:
                        throw new Exception("The specified translation direction is not supporteda!");
                    default:
                    break;
                }
                
                return string.Empty;
            }
            return OutPut;
        }
    }
}