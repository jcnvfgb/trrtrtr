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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Clipboard
{
    public partial class ViewingInvoices : Form
    {
        private string _email = "";

        public ViewingInvoices(string email)
        {
            InitializeComponent();

            //label1
            label1.Size = new Size(Width, 50);
            label1.BackColor = ColorTranslator.FromHtml("#465f87");
            label1.ForeColor = Color.White;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "Здравствуйте, " + email + "!";

            //Form
            BackColor = ColorTranslator.FromHtml("#606978");
            FormBorderStyle = FormBorderStyle.FixedSingle;
            _email = email;

            //Создание запроса
            string query = $@"
                select c.surname, c.name, ca.account_number, ca.account_type, ca.balance from 
                ClientAccounts ca join Clients c 
                on ca.client_id = c.id
                where client_id = (select id from Clients where email = '{email}')";

            //dataGridView1
            DataAccess.FillingTablesViaQuery(dataGridView1, query);
        }
    }
}
