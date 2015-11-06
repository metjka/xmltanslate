using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trans {
    public partial class TranslateForm : Form {
        private readonly BackgroundWorker backgroundWorker =new BackgroundWorker();
        private string text;

        public string LangTo {
            get { return _langTo; }
            set { _langTo = value; }

        }

        public string LangFrom {
            get { return _langFrom; }
            set { _langFrom = value; }
        }

        private string _translatedText;
        private string _langTo;
        private string _langFrom;

        public string TranslatedText {
            get { return _translatedText; }
        }

        private void Tranclate_Load(object sender, EventArgs e) {
            textBox1.Text = this.text;

        }
        public TranslateForm(string text,string langFrom, string langTo) {
            InitializeComponent();
            this.text = text;
            LangFrom =langFrom;
            LangTo = langTo;

            this.AcceptButton = button2;
            this.button2.DialogResult = DialogResult.OK;

            backgroundWorker.DoWork += TranslateInBackground;
            backgroundWorker.RunWorkerCompleted += OnTranslateCompleted;
        }

        private void OnTranslateCompleted(object sender, RunWorkerCompletedEventArgs e) {
            textBox2.Text = _translatedText;
        }

        private void TranslateInBackground(object sender, DoWorkEventArgs e) {
            _translatedText = YandexTranslate.Translate(LangFrom +"-"+LangTo, textBox1.Text);
            
            
        }

        private void button1_Click(object sender, EventArgs e) {
            backgroundWorker.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e) {
            _translatedText = textBox2.Text;
            Close();
        }

        
    }
}
