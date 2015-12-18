using System;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using TranslatorXml;

namespace TranslateXMLTooL {
    public partial class MainForm : Form {
        private YandexTranslate yandexTranslate;
        private DataTable dataTable;
        
        public MainForm() {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e) {
            dataGridView1.DataSource = dataTable;

            yandexTranslate = new YandexTranslate(textBox1.Text);
            comboBox1.DataSource = new Languages().Lang;
            comboBox2.DataSource = new Languages().Lang;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e) {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();

            OpenFileDialog openFileDialog = new OpenFileDialog {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = @"Xml|*.xml|Text Files|*.txt|All Files|*.*"
            };


            if (openFileDialog.ShowDialog() == DialogResult.OK) {
                StreamReader sr = new StreamReader(openFileDialog.FileName);
                string xmlText = sr.ReadToEnd();
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xmlText);
                SetXmlToDataGrid(xmlDocument);
            }
        }

        private void SetXmlToDataGrid(XmlDocument xmlDocument) {
            XmlNodeList xmlNodeList = xmlDocument.GetElementsByTagName("string");
            dataGridView1.Columns.Add("context", "Context");
            dataGridView1.Columns.Add("defoult", "Defoult value");
            dataGridView1.Columns["defoult"].SortMode = DataGridViewColumnSortMode.NotSortable;

            foreach (XmlNode item in xmlNodeList) {
                dataGridView1.Rows.Add(item.Attributes["name"].Value, item.InnerXml);
            }
        }

        private void label1_Click(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
            string header = comboBox2.Text;
            if (dataGridView1.Columns[new Languages().GetCodeByLang(header)] == null) {
                dataGridView1.Columns.Add(new Languages().GetCodeByLang(header), header);
                dataGridView1.Columns[new Languages().GetCodeByLang(header)].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            else {
                MessageBox.Show("This locale has already added!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e) {
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (e.RowIndex != -1 && e.ColumnIndex != -1) {
                    if (e.Button == MouseButtons.Right) {
                        DataGridViewCell clickedCell = (sender as DataGridView).Rows[e.RowIndex].Cells[e.ColumnIndex];

                        // Here you can do whatever you want with the cell
                        dataGridView1.CurrentCell = clickedCell; // Select the clicked cell, for instance

                        // Get mouse position relative to the vehicles grid
                        var relativeMousePosition = dataGridView1.PointToClient(Cursor.Position);

                        // Show the context menu
                        contextMenuStrip1.Show(dataGridView1, relativeMousePosition);
                    }
                }
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) {
            if (e.Button == MouseButtons.Right) {
                if (e.ColumnIndex > 1) {
                    if (e.Button == MouseButtons.Right) {
                        contextMenuStrip2.Show(MousePosition);
                    }
                }
            }
        }

        private void translateToolStripMenuItem_Click(object sender, EventArgs e) {
            using (
                TranslateForm translateForm =
                    new TranslateForm(dataGridView1.CurrentRow.Cells["defoult"].Value.ToString(),
                        new Languages().GetCodeByLang(comboBox1.Text),
                        new Languages().GetCodeByLang(
                            dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText), textBox1.Text)) {
                if (translateForm.ShowDialog() == DialogResult.OK) {
                    dataGridView1.CurrentCell.Value = translateForm.TranslatedText;
                }
            }
        }

        


   

        private void clearToolStripMenuItem_Click(object sender, EventArgs e) {
            dataGridView1.CurrentCell.Value = String.Empty;
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e) {
            for (int i = 2; i < dataGridView1.ColumnCount; i++) {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++) {
                    dataGridView1.Rows[j].Cells[i].Value = String.Empty;
                }
            }
        }

        private void translateAllToolStripMenuItem_Click(object sender, EventArgs e) {
            for (int i = 2; i < dataGridView1.ColumnCount; i++) {
                for (int j = 0; j < dataGridView1.RowCount - 1; j++) {
                    dataGridView1.Rows[j].Cells[i].Value =
                        yandexTranslate.Translate(new Languages().GetCodeByLang(comboBox1.Text) +
                                                  "-" +
                                                  new Languages().GetCodeByLang(
                                                      dataGridView1.Columns[i].HeaderCell.Value.ToString()),
                            dataGridView1.Rows[j].Cells[1].Value.ToString());
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(@"https://tech.yandex.com/translate/");
        }

        private void saveAsXMLToolStripMenuItem_Click(object sender, EventArgs e) {
            //todo saving all of xml files
        }

        private void deleteColumnToolStripMenuItem_Click(object sender, EventArgs e) {
            //todo deleting column in datagridview
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e) {

            DataTable data = new DataTable();
            data = dataTable;
            string a = "dad";


        }


        public void loadingparty() {
            string file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\mygrid.bin";
            if (File.Exists(file)) {
                dataGridView1.Rows.Clear();

                using (BinaryReader bw = new BinaryReader(File.Open(file, FileMode.Open))) {
                    int n = bw.ReadInt32();
                    int m = bw.ReadInt32();
                    for (int i = 0; i < m; ++i) {
                        dataGridView1.Rows.Add();
                        for (int j = 0; j < n; ++j) {
                            if (bw.ReadBoolean()) {
                                dataGridView1.Rows[i].Cells[j].Value = bw.ReadString();
                            }
                            else bw.ReadBoolean();
                        }
                    }
                }
            }
        }

        public void savingparty() {
            string file = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\mygrid.bin";

            using (BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.Create))) {
                bw.Write(dataGridView1.Columns.Count);
                bw.Write(dataGridView1.Rows.Count);
                foreach (DataGridViewRow dgvR in dataGridView1.Rows) {
                    for (int j = 0; j < dataGridView1.Columns.Count; ++j) {
                        var val = dgvR.Cells[j].Value;
                        if (val == null) {
                            bw.Write(false);
                            bw.Write(false);
                        }
                        else {
                            bw.Write(true);
                            bw.Write(val.ToString());
                        }
                    }
                }
            }
        }
    }
}
