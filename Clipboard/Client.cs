using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard {
    public partial class Client : Form {
        private const int INDENTATION = 23;
        private bool _admin;
        private string _email = "";

        private ViewingInvoices _viewingInvoices;
        private RegistrationOfServices _registrationOfServices;

        public Client(string email) {
            InitializeComponent();

            //Уставновка переменных
            _email = email;

            //label1
            label1.Size = new Size(Width, 50);
            label1.BackColor = ColorTranslator.FromHtml("#465f87");
            label1.ForeColor = Color.White;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Text = "Здравствуйте, " + email + "!";

            //Form
            BackColor = ColorTranslator.FromHtml("#606978");
            FormBorderStyle = FormBorderStyle.FixedSingle;

            //btn1
            button1.Size = new Size(Width - INDENTATION, button1.Height);
            button1.Text = "Просмотр счетов";

            //btn2
            button2.Size = new Size(Width - INDENTATION, button1.Height);
            button2.Text = "Оформление услуги";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Просмотр счетов

            _viewingInvoices = new ViewingInvoices(_email);
            _viewingInvoices.ShowDialog();
            _viewingInvoices.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Оформление услуг

            _registrationOfServices = new RegistrationOfServices(_email);
            _registrationOfServices.ShowDialog();
            _registrationOfServices.Close();
        }
    }
}
