using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace trans {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
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
            dataGridView1.Columns.Add("name", "name");
            dataGridView1.Columns.Add("def", "defoult");
            foreach (XmlNode item in xmlNodeList) {
                dataGridView1.Rows.Add(item.Attributes["name"].Value, item.InnerXml);
            }
        }

        private void label1_Click(object sender, EventArgs e) {
        }

        private void button1_Click(object sender, EventArgs e) {
            dataGridView1.Columns.Add(comboBox2.Text, comboBox2.Text);
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

        private void delliteToolStripMenuItem_Click(object sender, EventArgs e) {
            using (
                TranslateForm translateForm = new TranslateForm(dataGridView1.CurrentRow.Cells["def"].Value.ToString(), new Languages().GetKodeByLang(comboBox1.Text), new Languages().GetKodeByLang(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText))) {
                if (translateForm.ShowDialog() == DialogResult.OK) {
                    dataGridView1.CurrentCell.Value = translateForm.TranslatedText;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e) {

            comboBox1.DataSource = new Languages().Lang;
            comboBox2.DataSource = new Languages().Lang;
        }

        private void button2_Click(object sender, EventArgs e) {


            for (int i = 2; i<dataGridView1.ColumnCount ;i++) {
                for (int j =0; j < dataGridView1.RowCount -1; j++) {




                    dataGridView1.Rows[j].Cells[i].Value =
                        YandexTranslate.Translate(new Languages().GetKodeByLang(comboBox1.Text) +
                                                  "-" +
                                                  new Languages().GetKodeByLang(
                                                      dataGridView1.Columns[i].HeaderCell.Value.ToString()),
                                                      dataGridView1.Rows[j].Cells[1].Value.ToString());
                }
                
            }
        }

        
    }
}
