using AbstractClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Clipboard {
    public partial class Report : Form {
        private const int INDENTATION = 23;
        private string _email = "";
        private bool _admin;

        public Report(bool admin, string email) {
            InitializeComponent();

            //Установка переменных admin и email
            _admin = admin;
            _email = email;

            //Настройка label1
            label1.Size = new Size(Width, 50);
            label1.BackColor = ColorTranslator.FromHtml("#465f87");
            label1.ForeColor = Color.White;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "Отчет";

            //Form
            BackColor = ColorTranslator.FromHtml("#606978");
            FormBorderStyle = FormBorderStyle.FixedSingle;

            //label2
            label2.Size = new Size(Width - INDENTATION, label2.Height);
            label2.Text = "Выбор сотрудника";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            label2.ForeColor = Color.White;

            //comboBox1
            comboBox1.Size = new Size(Width - INDENTATION, comboBox1.Height);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            //label3
            label3.Size = new Size(Width - INDENTATION, label2.Height);
            label3.Text = "Выбор даты";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            label3.ForeColor = Color.White;

            //comboBox1
            comboBox2.Size = new Size(Width - INDENTATION, comboBox2.Height);

            //comboBox2
            comboBox2.Size = new Size(Width - INDENTATION, comboBox2.Height);
            string[] months = new string[] { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
                                    "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            comboBox2.Items.AddRange(months);
            comboBox2.SelectedIndex = 0;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;

            //Check admin
            if (admin) {
                try {
                    DataAccess.FillingWithTables(comboBox1, "select email from Employees", "email");
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            } else {
                comboBox1.Enabled = false;
                comboBox1.Items.Add(_email);
                comboBox1.SelectedIndex = 0;
            }

            //Настройка btn1
            button1.Size = new Size(Width - INDENTATION, button1.Height);
            button1.Text = "Cоставить отчет";

                
        }

        private void button1_Click(object sender, EventArgs e) {
            string email = comboBox1.Text;
            int month = comboBox2.SelectedIndex + 1;
            string query = $"SELECT e.surname, e.name, e.patronymic, r.request_date, r.request_result FROM Employees e JOIN Requests r ON e.id = r.employee_id WHERE e.email = '{email}' and MONTH(request_date) = {month}; ";

            try {
                DataAccess.FillingTablesViaQuery(dataGridView1, query);
            } catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
