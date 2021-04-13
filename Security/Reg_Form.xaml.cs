using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Secureity
{
    public partial class Reg_Form : Window
    {
        private MediaPlayer _mpBgr;
        private MediaPlayer _mpCurSound;

        int i = 0;

        public Reg_Form()
        {
            InitializeComponent();
            _mpBgr = new MediaPlayer();
            _mpCurSound = new MediaPlayer();
        }

        string Salt = "l#mXG7!C&H";
        public void Button_Click_Check(object sender, RoutedEventArgs e)
        {
            string name = textBoxname.Text.Trim();
            string secondname = textBoxsecondname.Text.Trim();
            string email = textBoxemail.Text.Trim();
            string login = textBoxlogin.Text.Trim();
            string pass = textBoxpass.Password.Trim();
            string repeatpass = textBoxrepeatpass.Password.Trim();
            string post = comboBoxpost.Text;


            string appDir = Environment.CurrentDirectory;
            string Opened = appDir + "/Sound/Opened.wav";
            string Error = appDir + "/Sound/Error.wav";
            string Gun= appDir+ "/Sound/Gun.wav";
            string Exit = appDir + "/Sound/Exit.wav";



            Boolean isUserExists()
            {
                DB db = new DB();

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command = new MySqlCommand("SELECT * FROM `users`WHERE `name`=@N AND `secondname`=@sN AND `email`=@eU AND `post`=@PS", db.getConnection());
            
                command.Parameters.Add("@N", MySqlDbType.VarChar).Value =textBoxname.Text;
                command.Parameters.Add("@sN", MySqlDbType.VarChar).Value = textBoxsecondname.Text;
                command.Parameters.Add("@eU", MySqlDbType.VarChar).Value = textBoxemail.Text;
                command.Parameters.Add("@PS", MySqlDbType.VarChar).Value = comboBoxpost.Text;

                adapter.SelectCommand = command;
                adapter.Fill(table);
                if(table.Rows.Count>0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            Boolean Check_login()
            {

                DB db = new DB();

                DataTable table = new DataTable();

                MySqlDataAdapter adapter = new MySqlDataAdapter();

                MySqlCommand command1 = new MySqlCommand("SELECT * FROM `users` WHERE `login`=@log", db.getConnection());
                command1.Parameters.Add("@log", MySqlDbType.VarChar).Value = textBoxlogin.Text;
                adapter.SelectCommand = command1;
                adapter.Fill(table);
                if (table.Rows.Count > 0)
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    Alarm.Text = "Такой логин уже используется";
                    Alarm.Visibility = Visibility.Visible;
                    return true;
                }
                else
                {
                    return false;
                }
            }


            if (post == "")
            {
                comboBoxpost.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                comboBoxpost.ToolTip = "Это поле введено не корректно!";
                _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                _mpBgr.Play();
            }
            else
            {
                comboBoxpost.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
                comboBoxpost.ToolTip = "Это поле введено  корректно!";




                if (name != "")
                {
                    textBoxname.ToolTip = "Это поле введено  корректно!";
                    textBoxname.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                }
                else
                {
                    textBoxname.ToolTip = "Это поле введено не корректно!";
                    textBoxname.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                }

                if (secondname != "")
                {
                    textBoxsecondname.ToolTip = "Это поле введено  корректно!";
                    textBoxsecondname.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                }
                else
                {
                    textBoxsecondname.ToolTip = "Это поле введено не корректно!";
                    textBoxsecondname.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                }

                if (email == "")
                {
                    textBoxemail.ToolTip = "Это поле введено не корректно!";
                    textBoxemail.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                }

                if (login == "")
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    textBoxlogin.ToolTip = "Это поле введено не корректно!";
                    textBoxlogin.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                }

                if (pass == "")
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    textBoxpass.ToolTip = "Это поле введено не корректно!";
                    textBoxpass.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                }

                if (repeatpass == "")
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    textBoxrepeatpass.ToolTip = "Это поле введено не корректно!";
                    textBoxrepeatpass.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                }




                if (email.Length < 5)
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    textBoxemail.ToolTip = "Это поле введено не корректно!";
                    textBoxemail.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                }
                else if (login.Length < 5 || login.Length > 15)
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    textBoxemail.ToolTip = "Это поле введено  корректно!";
                    textBoxemail.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxlogin.ToolTip = "Это поле введено не корректно!";
                    textBoxlogin.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                }
                else if (pass.Length < 8 || !((pass.Contains("_" )|| (pass.Contains("@")  || (pass.Contains("/") || (pass.Contains("#") || (pass.Contains("-"))))))))
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    textBoxlogin.ToolTip = "Это поле введено  корректно!";
                    textBoxemail.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxlogin.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxpass.ToolTip = "Это поле введено не корректно!";
                    textBoxpass.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                }
                else if (pass != repeatpass)
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    textBoxpass.ToolTip = "Это поле введено  корректно!";
                    textBoxemail.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxlogin.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxpass.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxrepeatpass.ToolTip = "Это поле введено не корректно!";
                    textBoxrepeatpass.Background = new SolidColorBrush(Color.FromRgb(204, 0, 0));
                }
                else
                {
                    textBoxname.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxsecondname.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxemail.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxlogin.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxpass.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    textBoxrepeatpass.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));
                    comboBoxpost.Background = new SolidColorBrush(Color.FromRgb(0, 220, 0));

                    if (isUserExists())
                    {
                        if (Check_login())
                        { }
                        else
                        { 
                            DB db = new DB();
                            MySqlCommand command = new MySqlCommand("Update  `users` SET login=@log, password=@pass WHERE name=@name AND secondname=@secondname AND email=@email", db.getConnection());
                            MySqlCommand command2 = new MySqlCommand("SELECT `id` FROM `users` WHERE   email=@email1  ", db.getConnection());
                            command.Parameters.AddWithValue("@name", name);
                            command.Parameters.AddWithValue("@secondname", secondname);
                            command.Parameters.AddWithValue("@email", email);
                            command2.Parameters.Add("@email1", MySqlDbType.VarChar).Value = email;
                            db.openConnection();
                            
                            

                            using (MySqlDataReader reader = command2.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var id = reader["id"].ToString();
                                    int id_user = int.Parse(id);
                                    id_user = id_user%88;
                                    string passnonhesh = textBoxpass.Password;
                                    var hasher = new SHA512Managed();
                                    var unhashed = System.Text.Encoding.Unicode.GetBytes(passnonhesh);
                                    var hashed = hasher.ComputeHash(unhashed);
                                    var hashedPassword = Convert.ToBase64String(hashed);
                                    hashedPassword = hashedPassword.Insert(id_user, Salt);
                                    command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = hashedPassword;
                                }
                            }

                            command.Parameters.Add("@log", MySqlDbType.VarChar).Value = textBoxlogin.Text;
                            

                            db.openConnection();

                            if (command.ExecuteNonQuery() == 1)
                            {
                                Alarm.Visibility = Visibility.Hidden;
                                _mpBgr.Open(new Uri(@Gun, UriKind.Absolute));
                                _mpBgr.Play();
                                db.closeConnection();

                                this.Close();
                                MainWindow MainWindow = new MainWindow();
                                MainWindow.Show();
                            }
                            db.closeConnection();
                        }
                    }
                    else
                    {
                        _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                        _mpBgr.Play();
                        Alarm.Text = "Вас либо нет в базе данных, либо вы неправильно ввели данные!Обращайтесь в поддержку.";
                        Alarm.Visibility = Visibility.Visible;
                    }
                  
               
                    
                }
               


            }

        }

        private void Button_MouseEnter_Exit(object sender, MouseEventArgs e)
        {
            string appDir = Environment.CurrentDirectory;
            
            string Error_1 = appDir + "/Sound/Error.wav";
            Exit.Background = Brushes.Red;
            Exit.BorderBrush = Brushes.Red;
            i = i + 1;
            if (i > 5)
            {
                _mpBgr.Open(new Uri(@Error_1, UriKind.Absolute));
                _mpBgr.Play();
                MessageBox.Show("Хороших заметок, броу!");
                i = 0;
            }
        }

        private void Button_MouseLeave_Exit(object sender, MouseEventArgs e)
        {
            Exit.Background = new SolidColorBrush(Color.FromRgb(50, 230, 0));
            Exit.BorderBrush = new SolidColorBrush(Color.FromRgb(50, 230, 0));
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            string appDir = Environment.CurrentDirectory;
            string Exit_1 = appDir + "/Sound/Exit.wav";
            _mpBgr.Open(new Uri(@Exit_1, UriKind.Absolute));
            _mpBgr.Play();
            var awaiter = Task.Delay(800).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                this.Close();
            });
        }

        private void Back_MouseEnter_Back(object sender, MouseEventArgs e)
        {
           
            Back.Background = Brushes.Red;
            Back.BorderBrush = Brushes.Red;
        }

        private void Back_MouseLeave_Back(object sender, MouseEventArgs e)
        {
            Back.Background = new SolidColorBrush(Color.FromRgb(50, 230, 0));
            Back.BorderBrush = new SolidColorBrush(Color.FromRgb(50, 230, 0));
        }

        private void Back_Click_Back(object sender, RoutedEventArgs e)
        {
            string appDir = Environment.CurrentDirectory;
            string Back = appDir + "/Sound/Back.wav";
            _mpBgr.Open(new Uri(@Back, UriKind.Absolute));
            _mpBgr.Play();
            this.Close();
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
        }
    }
}
