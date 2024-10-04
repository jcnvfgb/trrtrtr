using AbstractClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Clipboard
{
    public partial class RegistrationOfServices : Form
    {
        private const int INDENTATION = 23;
        private string _email = "";

        public RegistrationOfServices(string email)
        {
            InitializeComponent();

            //Установка значений переменным
            _email = email;

            //label1
            label1.Size = new Size(Width, 50);
            label1.BackColor = ColorTranslator.FromHtml("#465f87");
            label1.ForeColor = Color.White;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "Оформление услуг";

            //Form
            BackColor = ColorTranslator.FromHtml("#606978");
            FormBorderStyle = FormBorderStyle.FixedSingle;

            //label2
            label2.ForeColor = Color.White;

            //label3
            label3.ForeColor = Color.White;

            //label4
            label4.ForeColor = Color.White;

            //label5
            label5.ForeColor = Color.White;

            //Запрос на получение типов услуг
            string query = "select service_name from Services";

            //comboBox1
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            DataAccess.FillingWithTables(comboBox1, query, "service_name");
            comboBox1.SelectedIndex = 0;

            //btn1
            button1.Text = "Отправить заявку";
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) {
            string text = ((TextBox)sender).Text;
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
                return;
            if (e.KeyChar == (char)Keys.Back)
                return;
            e.KeyChar = '\0';
        }

        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text == "" ||
                textBox2.Text == "" ||
                textBox3.Text == "") {
                MessageBox.Show("Формат введенных данных неправильный");
                return;
            }

            string serviceName = comboBox1.Text;
            int year = Convert.ToInt32(textBox1.Text),
                month = Convert.ToInt32(textBox2.Text),
                day = Convert.ToInt32(textBox3.Text);
            string query = $@"INSERT INTO Requests (id, client_id, employee_id, service_id, request_date, request_time, request_result)
                            VALUES
                                ((select count(*) from Requests) + 1,
                                (select id from Clients where email = '{_email}'), 
                                1, 
                                (select id from Services where service_name = '{serviceName}'),
                                '{year}-{month}-{day}',
                                '10:00:00',
                                '(Запрос обработан)');";

            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                try {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Запрос успешно добавлен");
                } catch (SqlException ex) {
                    MessageBox.Show("Ошибка добавления запроса: " + ex.Message);
                } finally {
                    connection.Close();
                }
            }
        }
    }
}
