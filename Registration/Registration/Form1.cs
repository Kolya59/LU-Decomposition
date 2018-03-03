using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace registration
{
    public partial class Form1 : Form
    {
        public string SConnStr = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "RegistrationTable",
            IntegratedSecurity = true
        }.ConnectionString;

        public Form1()
        {
            InitializeComponent();
            btRegister.Enabled = false;
            bt_signin.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtRegister_Click(object sender, EventArgs e) 
        {
            bt_signin.Enabled = true;
            if (tbhashedPass.Text != "")
            {
                using (var sConn = new SqlConnection(SConnStr))
                {
                    var createsalt = new RNGCryptoServiceProvider();
                    var newsalt = new byte[32];
                    createsalt.GetBytes(newsalt);
                    var salt = BitConverter.ToString(newsalt).Replace("-", "");

                    sConn.Open();
                    var sCommand = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"INSERT INTO users(login,hashedPassword,salt,dateOfReg)
                                        VALUES (@login, HASHBYTES('SHA1', @password + @salt), @salt, GETDATE())"
                    };
                    sCommand.Parameters.AddWithValue("@login", tbLogin.Text);
                    sCommand.Parameters.AddWithValue("@password", tbhashedPass.Text);
                    sCommand.Parameters.AddWithValue("@salt", salt);
                    if (sCommand.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show(@"Вы зарегистрированы");
                        btRegister.Enabled = false;
                    }
                }
                tbhashedPass.Text = "";
            }
            else
                MessageBox.Show(@"Поле пароля не должно быть пустым, заполните его!");
        }

        private void TbLogin_TextChanged(object sender, EventArgs e) 
        {
            if (tbLogin.Text != "")
            {
                bt_signin.Enabled = true;
                using (var sConn = new SqlConnection(SConnStr))
                {
                    sConn.Open();
                    var sCommand = new SqlCommand
                    {
                        Connection = sConn,
                        CommandText = @"SELECT COUNT(*) FROM users WHERE login = @login"
                    };
                    sCommand.Parameters.AddWithValue("@login", tbLogin.Text);
                    if ((int)sCommand.ExecuteScalar() == 0)
                    {
                        btRegister.Enabled = true;
                        bt_signin.Enabled = false;
                    }
                    else
                    {
                        btRegister.Enabled = false;
                        bt_signin.Enabled = true;
                    }
                }
            }
            else
            {
                bt_signin.Enabled = false;
                btRegister.Enabled = false;
            }
        }

        private void Bt_signin_Click(object sender, EventArgs e) 
        {
            using (var sConn = new SqlConnection(SConnStr))
            {
                sConn.Open();
                var sCommand = new SqlCommand
                {
                    Connection = sConn,
                    CommandText = @" DECLARE @sil nvarchar(max);
                                    set @sil=(Select salt FROM users WHERE login = @login )
                                    select count(*) FROM users WHERE login = @login  and hashedPassword = HASHBYTES('SHA1', @Hash + @sil)"
                };
                sCommand.Parameters.AddWithValue("@login", tbLogin.Text);
                sCommand.Parameters.AddWithValue("@Hash", tbhashedPass.Text);
                if ((int)sCommand.ExecuteScalar() == 1)
                {
                    MessageBox.Show(@"Вход выполнен");
                    tbhashedPass.Text = "";
                }
                else
                {
                    MessageBox.Show(@"Введенные логин/пароль неверны");
                    tbhashedPass.Text = "";
                }
            }
        }
    }
}
