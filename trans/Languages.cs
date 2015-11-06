using System.Collections.Generic;
using System.Linq;

namespace trans {
     public class Languages {
        private List<string> _lang = new List<string>();
        private  Dictionary<string, string> langDictionary = new Dictionary<string, string>();

        public Languages() {
            langDictionary.Add("sq", "Albanian");
            langDictionary.Add("en", "English");
            langDictionary.Add("ar", "Arabic");
            langDictionary.Add("hy", "Armenian");
            langDictionary.Add("az", "Azerbaijan");
            langDictionary.Add("af", "Afrikaans");
            langDictionary.Add("eu", "Basque");
            langDictionary.Add("be", "Belarusian");
            langDictionary.Add("bg", "Bulgarian");
            langDictionary.Add("bs", "Bosnian");
            langDictionary.Add("cy", "Welsh");
            langDictionary.Add("vi", "Vietnamese");
            langDictionary.Add("hu", "Hungarian");
            langDictionary.Add("ht", "Haitian");
            langDictionary.Add("gl", "Galician");
            langDictionary.Add("nl", "Dutch");
            langDictionary.Add("el", "Greek");
            langDictionary.Add("ka", "Georgian");
            langDictionary.Add("da", "Danish");
            langDictionary.Add("he", "Yiddish");
            langDictionary.Add("id", "Indonesian");
            langDictionary.Add("ga", "Irish");
            langDictionary.Add("it", "Italian");
            langDictionary.Add("is", "Icelandic");
            langDictionary.Add("es", "Spanish");
            langDictionary.Add("kk", "Kazakh");
            langDictionary.Add("ca", "Catalan");
            langDictionary.Add("ky", "Kyrgyz");
            langDictionary.Add("zh", "Chinese");
            langDictionary.Add("ko", "Korean");
            langDictionary.Add("la", "Latin");
            langDictionary.Add("lv", "Latvian");
            langDictionary.Add("lt", "Lithuanian");
            langDictionary.Add("mg", "Malagasy");
            langDictionary.Add("ms", "Malay");
            langDictionary.Add("mt", "Maltese");
            langDictionary.Add("mk", "Macedonian");
            langDictionary.Add("mn", "Mongolian");
            langDictionary.Add("de", "German");
            langDictionary.Add("no", "Norwegian");
            langDictionary.Add("fa", "Persian");
            langDictionary.Add("pl", "Polish");
            langDictionary.Add("pt", "Portuguese");
            langDictionary.Add("ro", "Romanian");
            langDictionary.Add("ru", "Russian");
            langDictionary.Add("sr", "Serbian");
            langDictionary.Add("sk", "Slovakian");
            langDictionary.Add("sl", "Slovenian");
            langDictionary.Add("sw", "Swahili");
            langDictionary.Add("tg", "Tajik");
            langDictionary.Add("th", "Thai");
            langDictionary.Add("tl", "Tagalog");
            langDictionary.Add("tt", "Tatar");
            langDictionary.Add("tr", "Turkish");
            langDictionary.Add("uz", "Uzbek");
            langDictionary.Add("uk", "Ukrainian");
            langDictionary.Add("fi", "Finish");
            langDictionary.Add("fr", "French");
            langDictionary.Add("hr", "Croatian");
            langDictionary.Add("cs", "Czech");
            langDictionary.Add("sv", "Swedish");
            langDictionary.Add("et", "Estonian");
            langDictionary.Add("ja", "Japanese");


            Lang = langDictionary.Values.ToList();
        }

        public string GetLang(string key) {

            return langDictionary.ContainsKey(key).ToString();
        }

        public List<string> Lang {
            get { return _lang; }
            set { _lang = value; }
        }

        public  string GetKodeByLang(string lang) {
            return langDictionary.FirstOrDefault(x => x.Value == lang).Key;

        }
    }
}
