using AbstractClass;
using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Clipboard {
    public partial class Form2 : Form {
        private string _email = "";
        private bool _admin;

        private Report report;
        private Statistics stat;

        public Form2(bool admin, string email) {
            InitializeComponent();

            //toolStrip1
            toolStrip1.BackColor = ColorTranslator.FromHtml("#465f87");

            //Form
            BackColor = ColorTranslator.FromHtml("#606978");

            //Btn1
            button1.Text = "Найти";

            //Btn2
            button2.Text = "Сбросить";

            if (admin) {
                //toolStripComboBox1
                DataAccess.FillingWithTables(toolStripComboBox1);
                toolStripComboBox1.SelectedIndex = 0;
                toolStripComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            } else {
                //toolStripComboBox1
                DataAccess.FillingWithTablesForUs(toolStripComboBox1);
                toolStripComboBox1.SelectedIndex = 0;
                toolStripComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            //dataGtidView
            dataGridView1.RowHeadersVisible = false;
            DataAccess.FillingTables(dataGridView1, toolStripComboBox1.Items[0].ToString());
            DataAccess.FillComboBoxWithColumnNames(comboBox1, toolStripComboBox1.Items[0].ToString());

            //comboBox1
            comboBox1.SelectedIndex = 0;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            //Установка переменной _email(для дальнейшего использования в формировании формы отчета)
            _email = email;

            //Установка переменной _admin(для дальнейшего использования в формировании формы отчета)
            _admin = admin;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            DataAccess.FillingTables(dataGridView1, toolStripComboBox1.SelectedItem.ToString());
            DataAccess.FillComboBoxWithColumnNames(comboBox1, toolStripComboBox1.SelectedItem.ToString());
            comboBox1.SelectedIndex = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e) {
            try {
                DataAccess.SaveChangesToDatabase(dataGridView1, toolStripComboBox1.SelectedItem.ToString());
                DataAccess.FillingTables(dataGridView1, toolStripComboBox1.SelectedItem.ToString());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                DataAccess.FillingTables(dataGridView1, toolStripComboBox1.SelectedItem.ToString());
                return;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e) {
            report = new Report(_admin, _email);
            report.ShowDialog();
            report.Close();
        }

        private void toolStripButton3_Click(object sender, EventArgs e) {
            stat = new Statistics(_admin, _email);
            stat.ShowDialog();
            stat.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            try {
                //DataAccess.SearchOnField(dataGridView1, comboBox1.SelectedItem.ToString(), comboBox2.SelectedItem.ToString(), textBox1.Text);
                DataAccess.SearchInDataGridView(dataGridView1, comboBox1.SelectedItem.ToString(), textBox1.Text);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            try {
                DataAccess.FillingTables(dataGridView1, toolStripComboBox1.SelectedItem.ToString());
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
