using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace TranslatorXml {
    public partial class TranslateForm : Form {
        private readonly BackgroundWorker backgroundWorker = new BackgroundWorker();
        private readonly string text;
        private YandexTranslate yandexTranslate;
        public string LangTo { get; set; }
        private string Key;

        public string LangFrom { get; set; }

        public string TranslatedText { get; private set; }

        private void Tranclate_Load(object sender, EventArgs e) {
            textBox1.Text = text;
            yandexTranslate = new YandexTranslate(Key);
        }

        public TranslateForm(string text, string langFrom, string langTo, string key) {
            InitializeComponent();
            this.text = text;
            this.Key = key;
            LangFrom = langFrom;
            LangTo = langTo;
            AcceptButton = button2;
            button2.DialogResult = DialogResult.OK;

            backgroundWorker.DoWork += TranslateInBackground;
            backgroundWorker.RunWorkerCompleted += OnTranslateCompleted;
        }

        private void OnTranslateCompleted(object sender, RunWorkerCompletedEventArgs e) {
            textBox2.Text = TranslatedText;
        }

        private void TranslateInBackground(object sender, DoWorkEventArgs e) {
            TranslatedText = yandexTranslate.Translate(LangFrom + "-" + LangTo, textBox1.Text);
        }

        private void button1_Click(object sender, EventArgs e) {
            backgroundWorker.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e) {
            TranslatedText = textBox2.Text;
            Close();
        }
    }
}
