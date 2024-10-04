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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Clipboard {
    public partial class Form1 : Form {
        private const int INDENTATION = 23;

        bool _isAdmin;
        bool _isClient;
        bool _isLoginSuccessful;

        Form2 _form2;
        Client _client;

        public Form1() {
            InitializeComponent();

            //Настройка label1
            label1.Size = new Size(Width, 50);
            label1.BackColor = ColorTranslator.FromHtml("#465f87");
            label1.ForeColor = Color.White;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "Авторизация";

            //Form
            BackColor = ColorTranslator.FromHtml("#606978");

            //Настройка textbox1
            textBox1.Size = new Size(Width - INDENTATION, textBox1.Height);

            //Настройка textbox2
            textBox2.Size = new Size(Width - INDENTATION, textBox2.Height);

            //Настройка btn1
            button1.Size = new Size(Width - INDENTATION, button1.Height);
            button1.Text = "Войти";

            //Настройка btn2
            button2.Size = new Size(Width - INDENTATION, button2.Height);
            button2.Text = "Регистрация";
        }

        private void Form1_Resize(object sender, EventArgs e) {
            //Настройка label1
            label1.Size = new Size(Width, 50);

            //Настройка textbox1
            textBox1.Size = new Size(Width - INDENTATION, textBox1.Height);

            //Настройка textbox2
            textBox2.Size = new Size(Width - INDENTATION, textBox2.Height);

            //Настройка btn1
            button1.Size = new Size(Width - INDENTATION, button1.Height);

            //Настройка btn2
            button2.Size = new Size(Width - INDENTATION, button2.Height);
        }

        //Кнопка входа
        private void button1_Click(object sender, EventArgs e) {
            if (textBox1.Text == "" || textBox2.Text == "") {
                MessageBox.Show("The fields on the form are empty");
                return;
            }

            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                //string query = "SELECT COUNT(*) FROM Clients WHERE email = @Email";
                string query =
                    @"SELECT COUNT(*) FROM 
	                    (SELECT id, surname, name, patronymic, date_of_birth, address, phone, email, password, 'client' AS role
	                    FROM Clients
	                    UNION ALL
	                    SELECT id, surname, name, patronymic, NULL AS date_of_birth, NULL AS address, phone, email, password, role
	                    FROM Employees) AS combat_table
                    where email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                SqlParameter pr1 = new SqlParameter("@Email", textBox1.Text);
                command.Parameters.Add(pr1);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                if (count == 1) {
                    /*query = @"OPEN SYMMETRIC KEY SymKey DECRYPTION BY PASSWORD = 'Kurs'
                  SELECT Role, CONVERT(nvarchar, DecryptByKey(Password)) as Password FROM Users WHERE Login = @Log
                  CLOSE SYMMETRIC KEY SymKey";*/
                    //query = @"SELECT password as Password FROM Clients WHERE email = @Email";
                    query =
                        @"SELECT Role, password as Password FROM 
	                        (SELECT id, surname, name, patronymic, date_of_birth, address, phone, email, password, 'client' AS role
	                        FROM Clients
	                        UNION ALL
	                        SELECT id, surname, name, patronymic, NULL AS date_of_birth, NULL AS address, phone, email, password, role
	                        FROM Employees) AS combat_table
                        where email = @Email";
                    command = new SqlCommand(query, connection);
                    pr1 = new SqlParameter("@Email", textBox1.Text);
                    command.Parameters.Add(pr1);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read()) {
                        string role = reader["Role"].ToString();
                        string decryptedPassword = reader["Password"].ToString();

                        if (textBox2.Text == decryptedPassword) {
                            MessageBox.Show("The data is entered correctly. Welcome!");

                            if (role == "admin") {
                                // Handle admin role
                                MessageBox.Show("You are logged in as an admin.");
                                _isAdmin = true;
                                _isClient = false;
                            } else if (role == "user") {
                                // Handle user role
                                MessageBox.Show("You are logged in as a user.");
                                _isAdmin = false;
                                _isClient = false;
                            } else if (role == "client") {
                                MessageBox.Show("You are logged in as a client.");
                                _isAdmin = false;
                                _isClient = true;
                            } else {
                                MessageBox.Show("Your role is not recognized.");
                            }
                            _isLoginSuccessful = true;
                        } else {
                            MessageBox.Show("You entered the wrong password!");
                            _isLoginSuccessful = false;
                        }
                    }

                    connection.Close();
                } else {
                    MessageBox.Show("Data entered incorrectly!");
                    _isLoginSuccessful = false;
                }
            }

            if (_isLoginSuccessful) {
                if (_isClient == true) {
                    _client = new Client(textBox1.Text);
                    _client.Owner = this;
                    _client.ShowDialog();
                } else {
                    _form2 = new Form2(_isAdmin, textBox1.Text);
                    _form2.Owner = this;
                    _form2.ShowDialog();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) {
            if (textBox1.Text == "" || textBox2.Text == "") {
                MessageBox.Show("The fields on the form are empty");
                return;
            }

            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                string query =
                    @"SELECT COUNT(*) FROM 
	                    (SELECT id, surname, name, patronymic, date_of_birth, address, phone, email, password, 'client' AS role
	                    FROM Clients
	                    UNION ALL
	                    SELECT id, surname, name, patronymic, NULL AS date_of_birth, NULL AS address, phone, email, password, role
	                    FROM Employees) AS combat_table
                    where email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                SqlParameter pr1 = new SqlParameter("@Email", textBox1.Text);
                command.Parameters.Add(pr1);

                connection.Open();
                int count = (int)command.ExecuteScalar();
                connection.Close();

                if (count == 0) {
                    query =
                        @"INSERT INTO Clients (id, surname, name, patronymic, date_of_birth, address, phone, email, password)
                            VALUES
                        ((select count(*) from Clients) + 1, '', '', '', '', '', '', @Email, @Pass);";

                    // Выполнение команды вставки данных
                    connection.Open();
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", textBox1.Text);
                    command.Parameters.AddWithValue("@Pass", textBox2.Text);
                    command.ExecuteNonQuery();
                    connection.Close();

                    MessageBox.Show("Данные успешно добавлены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    MessageBox.Show("Данные уже существуют!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
