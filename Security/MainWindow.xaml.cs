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
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace Secureity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i= 0;

        private MediaPlayer _mpBgr;
        private MediaPlayer _mpCurSound;
        public MainWindow()
        {
            
            InitializeComponent();
            _mpBgr = new MediaPlayer();
            _mpCurSound = new MediaPlayer();
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Reg_Form Reg_Rorm = new Reg_Form();
            Reg_Rorm.ShowDialog();
            this.Close();
        }

        private void Button_Click_Enter(object sender, RoutedEventArgs e)
        {
            string appDir = Environment.CurrentDirectory;
            string Opened = appDir+"/Sound/Opened.wav" ;
            string Error = appDir +"/Sound/Error.wav";
 
            string emailUser = textBoxemail.Text;
            string loginUser = textBoxlogin.Text;
            string passUser = textBoxpass.Password;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `users`WHERE `email`=@eU AND `login`=@lU ", db.getConnection());
            command.Parameters.Add("@eU", MySqlDbType.VarChar).Value = emailUser;
            command.Parameters.Add("@lU", MySqlDbType.VarChar).Value = loginUser;
           

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {

                int sum = -1;

                MySqlCommand command2 = new MySqlCommand("SELECT `id`,`password` FROM `users` WHERE   email=@email  ", db.getConnection());
                command2.Parameters.Add("@email", MySqlDbType.VarChar).Value = emailUser;
                var hasher = new SHA512Managed();
                var unhashed = System.Text.Encoding.Unicode.GetBytes(passUser);
                var hashed = hasher.ComputeHash(unhashed);
                var hashedPassword = Convert.ToBase64String(hashed);
                using (MySqlDataReader reader1 = command2.ExecuteReader())
                {
                    while (reader1.Read())
                    {
                        int id_user;
                        var id = reader1["id"].ToString();
                      
                        id_user = int.Parse(id);
                        id_user = id_user % 88;
                        var tablepassword = reader1["password"].ToString();
                       
                        tablepassword = tablepassword.Remove(id_user, 10);
                       
                        if (tablepassword == hashedPassword)
                        {
                            sum = 1;
                         
                        }
                        else
                        {
                            sum = 0;
                           
                        }
                    }
                }
             if (sum==1)
                {
                    _mpBgr.Open(new Uri(@Opened, UriKind.Absolute));
                    _mpBgr.Play();
                    Alarm.Visibility = Visibility.Hidden;
                    Security_Notes Security_Notes = new Security_Notes();
                    Security_Notes.Name.Text = textBoxlogin.Text;
                    this.Close();
                    Security_Notes.Show();
                    db.closeConnection();
                }
             else
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    Alarm.Text = "Вы ввели не тот пароль!";
                    Alarm.Visibility = Visibility.Visible;
                }
            }
            else
            {
                db.closeConnection();
                _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                _mpBgr.Play();
                Alarm.Text = "Вы либо ввели неправильные данные, либо не зарегистированы.";
                Alarm.Visibility = Visibility.Visible;
               
            }
        }

        private void Button_Exit(object sender, RoutedEventArgs e)
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

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {

            string appDir = Environment.CurrentDirectory;
   
            string Error_1 = appDir + "/Sound/Error.wav";
           
            Exit.Background = new SolidColorBrush(Color.FromRgb(0, 150, 0));
            Exit.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 150, 0));

            i = i + 1;
            if (i>5)
            {
                _mpBgr.Open(new Uri(@Error_1, UriKind.Absolute));
                _mpBgr.Play();
                MessageBox.Show("Хороших заметок, броу!");
                i = 0;
            }


        }

        private void Exit_MouseLeave(object sender, MouseEventArgs e)
        { 
            Exit.Background = Brushes.Red;
            Exit.BorderBrush = Brushes.Red;
        }
    }
}
