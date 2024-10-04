using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data;

namespace AbstractClass {
    internal class DataAccess {
        static public void FillingWithTables(System.Windows.Forms.ComboBox comboBox) {
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // создаем объект SqlCommand для выполнения SQL-запроса
                string query = "SELECT name FROM sys.tables";
                SqlCommand command = new SqlCommand(query, connection);

                // выполняем запрос и получаем SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // читаем данные из SqlDataReader и добавляем их в comboBox1
                while (reader.Read()) {
                    comboBox.Items.Add(reader["name"].ToString());
                }

                // закрываем SqlDataReader
                reader.Close();
            }
        }

        static public void FillingWithTables(System.Windows.Forms.ComboBox comboBox, string query, string field) {
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // создаем объект SqlCommand для выполнения SQL-запроса
                string _query = query;
                SqlCommand command = new SqlCommand(_query, connection);

                // выполняем запрос и получаем SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // читаем данные из SqlDataReader и добавляем их в comboBox1
                while (reader.Read()) {
                    comboBox.Items.Add(reader[field].ToString());
                }

                // закрываем SqlDataReader
                reader.Close();
            }
        }

        static public void FillingWithTables(System.Windows.Forms.ToolStripComboBox comboBox) {
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // создаем объект SqlCommand для выполнения SQL-запроса
                string query = "SELECT name FROM sys.tables";
                SqlCommand command = new SqlCommand(query, connection);

                // выполняем запрос и получаем SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // читаем данные из SqlDataReader и добавляем их в comboBox1
                while (reader.Read()) {
                    comboBox.Items.Add(reader["name"].ToString());
                }

                // закрываем SqlDataReader
                reader.Close();
            }
        }

        static public void FillingWithTablesForUs(System.Windows.Forms.ToolStripComboBox comboBox) {
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // создаем объект SqlCommand для выполнения SQL-запроса
                string query = "SELECT name FROM sys.tables";
                SqlCommand command = new SqlCommand(query, connection);

                // выполняем запрос и получаем SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // читаем данные из SqlDataReader и добавляем их в comboBox1
                while (reader.Read()) {
                    if (reader["name"].ToString() == "Clients" ||
                        reader["name"].ToString() == "Loans" ||
                        reader["name"].ToString() == "ClientAccounts" ||
                        reader["name"].ToString() == "Cards") {
                        comboBox.Items.Add(reader["name"].ToString());
                    }
                }

                // закрываем SqlDataReader
                reader.Close();
            }
        }

        static public void FillingTables(DataGridView dataGridView, string tableName) {
            // Устанавливаем свойства AutoSizeColumnsMode и AutoSizeRowsMode
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // создаем объект SqlCommand для выполнения SQL-запроса
                string query = $"SELECT * FROM {tableName}";
                SqlCommand command = new SqlCommand(query, connection);

                // создаем объект SqlDataAdapter для заполнения DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // присваиваем DataTable элементу DataGridView
                dataGridView.DataSource = table;
            }

            dataGridView.AutoResizeColumns();
            dataGridView.AutoResizeRows();

            // Получаем ширину Datagridview без учета ширины прокрутки
            int width = dataGridView.ClientRectangle.Width;

            // Вычисляем среднюю ширину столбца
            int averageColumnWidth = width / dataGridView.ColumnCount;

            // Устанавливаем ширину каждого столбца равной средней ширине столбца
            foreach (DataGridViewColumn column in dataGridView.Columns) {
                column.Width = averageColumnWidth;
            }

            // Устанавливаем свойство AutoSizeColumnsMode
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        static public void FillingTablesViaQuery(DataGridView dataGridView, string query) {
            // Устанавливаем свойства AutoSizeColumnsMode и AutoSizeRowsMode
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // создаем объект SqlCommand для выполнения SQL-запроса
                string _query = query;
                SqlCommand command = new SqlCommand(_query, connection);

                // создаем объект SqlDataAdapter для заполнения DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // присваиваем DataTable элементу DataGridView
                dataGridView.DataSource = table;
            }

            dataGridView.AutoResizeColumns();
            dataGridView.AutoResizeRows();

            // Получаем ширину Datagridview без учета ширины прокрутки
            int width = dataGridView.ClientRectangle.Width;

            // Вычисляем среднюю ширину столбца
            int averageColumnWidth = width / dataGridView.ColumnCount;

            // Устанавливаем ширину каждого столбца равной средней ширине столбца
            foreach (DataGridViewColumn column in dataGridView.Columns) {
                column.Width = averageColumnWidth;
            }

            // Устанавливаем свойство AutoSizeColumnsMode
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        static public void SaveChangesToDatabase(DataGridView dataGridView, string tableName) {
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                // создаем объект SqlCommand для выполнения SQL-запроса на выборку данных
                string query = $"SELECT * FROM {tableName}";
                SqlCommand command = new SqlCommand(query, connection);

                // создаем объект SqlDataAdapter для выполнения команд
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                connection.Open();
                adapter.Update(dataGridView.DataSource as DataTable);
                connection.Close();
            }
        }

        static public void FillComboBoxWithColumnNames(System.Windows.Forms.ComboBox comboBox, string tableName) {
            comboBox.Items.Clear();
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // создаем объект SqlCommand для выполнения SQL-запроса
                string query = $"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";
                SqlCommand command = new SqlCommand(query, connection);

                // выполняем запрос и получаем SqlDataReader
                SqlDataReader reader = command.ExecuteReader();

                // читаем данные из SqlDataReader и добавляем их в ComboBox
                while (reader.Read()) {
                    comboBox.Items.Add(reader["COLUMN_NAME"].ToString());
                }

                // закрываем SqlDataReader
                reader.Close();
            }
        }

        static public void SearchOnField(DataGridView dataGridView, string tableName, string fieldName, string parameterValue) {
            // Устанавливаем свойства AutoSizeColumnsMode и AutoSizeRowsMode
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // создаем объект SqlCommand для выполнения SQL-запроса
                string query = $"SELECT * FROM {tableName} where {fieldName} = @Param";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Param", parameterValue);

                // создаем объект SqlDataAdapter для заполнения DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // присваиваем DataTable элементу DataGridView
                dataGridView.DataSource = table;
            }

            dataGridView.AutoResizeColumns();
            dataGridView.AutoResizeRows();

            // Получаем ширину Datagridview без учета ширины прокрутки
            int width = dataGridView.ClientRectangle.Width;

            // Вычисляем среднюю ширину столбца
            int averageColumnWidth = width / dataGridView.ColumnCount;

            // Устанавливаем ширину каждого столбца равной средней ширине столбца
            foreach (DataGridViewColumn column in dataGridView.Columns) {
                column.Width = averageColumnWidth;
            }

            // Устанавливаем свойство AutoSizeColumnsMode
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        static public void SearchInDataGridView(DataGridView dataGridView, string columnName, string searchText) {
            // обходим все строки в DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows) {
                // проверяем, содержит ли ячейка в указанном столбце искомую подстроку
                if (row.Cells[columnName].Value != null && row.Cells[columnName].Value.ToString().Contains(searchText)) {
                    // выделяем строку
                    dataGridView.CurrentCell = row.Cells[columnName];
                    row.Selected = true;
                    return;
                }
            }

            // если искомая подстрока не найдена, то сбрасываем выделение
            dataGridView.ClearSelection();
        }

        static public void TaskOnOption(DataGridView dataGridView, string numOfMonth) {
            // Устанавливаем свойства AutoSizeColumnsMode и AutoSizeRowsMode
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // создаем объект SqlConnection и устанавливаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // создаем объект SqlCommand для выполнения SQL-запроса
                string query = $@"  SELECT 
                                        D.Name, 
                                        D.Surname, 
                                        SUM(DS.TaxAmount) + MAX(D.PaymentRate) AS TotalSalary
                                    FROM 
                                        DoctorSalaries DS
                                    INNER JOIN 
                                        Doctors D ON DS.DoctorID = D.DoctorID
                                    INNER JOIN 
                                        Receptions R ON DS.ReceptionID = R.ReceptionID
                                    WHERE 
                                        MONTH(R.ReceptionDate) = @Param AND YEAR(R.ReceptionDate) = 2022
                                    GROUP BY 
                                        D.Name, 
                                        D.Surname
                                    ORDER BY 
                                        TotalSalary DESC;";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("Param", numOfMonth);

                // создаем объект SqlDataAdapter для заполнения DataTable
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // присваиваем DataTable элементу DataGridView
                dataGridView.DataSource = table;
            }

            dataGridView.AutoResizeColumns();
            dataGridView.AutoResizeRows();

            // Получаем ширину Datagridview без учета ширины прокрутки
            int width = dataGridView.ClientRectangle.Width;

            // Вычисляем среднюю ширину столбца
            int averageColumnWidth = width / dataGridView.ColumnCount;

            // Устанавливаем ширину каждого столбца равной средней ширине столбца
            foreach (DataGridViewColumn column in dataGridView.Columns) {
                column.Width = averageColumnWidth;
            }

            // Устанавливаем свойство AutoSizeColumnsMode
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        static public int ReturnOfQuantitative(string query) {
            //Функция, принимающая запрос вида select count(*) from table_name и возвращающая число, как результат выполнения

            // Проверяем, является ли запрос запросом на подсчет строк
            if (!query.StartsWith("SELECT COUNT(*) FROM", StringComparison.OrdinalIgnoreCase)) {
                throw new ArgumentException("Запрос должен быть вида SELECT COUNT(*) FROM table_name");
            }

            int count = 0;

            // Создаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(DB.connectionString)) {
                connection.Open();

                // Создаем команду для выполнения запроса
                using (SqlCommand command = new SqlCommand(query, connection)) {
                    // Выполняем запрос и получаем результат
                    object result = command.ExecuteScalar();

                    // Преобразуем результат в целое число
                    if (result != null) {
                        count = Convert.ToInt32(result);
                    }
                }
            }

            return count;
        }
    }
}
