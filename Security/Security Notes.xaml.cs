using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
using Microsoft.Win32;
using System.IO;
using System.Windows.Navigation;


namespace Secureity
{
 
     
    public partial class Security_Notes : Window
    {

        private MediaPlayer _mpBgr;
        private MediaPlayer _mpCurSound;


        public Security_Notes()
        {
            InitializeComponent();
            _mpBgr = new MediaPlayer();
            _mpCurSound = new MediaPlayer();
            FontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            FontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72, 96, 144 };

            string Dir = Environment.CurrentDirectory;
            string mkdir = Dir + "/Notes";

            if (!Directory.Exists(mkdir))
            {
                DirectoryInfo di = Directory.CreateDirectory(mkdir);
                di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
        }
        int k = 0;
       
      







        private void Hello_MouseEnter(object sender, MouseEventArgs e)
        {
            while (k < 1)
            { 
                k = 1;
                string Semen = Name.Text;
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.openConnection();



            MySqlCommand command = new MySqlCommand("SELECT `name`,`secondname`  FROM `users` WHERE  `login`=@lU", db.getConnection());
            command.Parameters.Add("@lU", MySqlDbType.VarChar).Value = Semen;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                        
                    var fname = reader["name"].ToString();
                    var sname = reader["secondname"].ToString();
                    Hello_User.Text = fname;
                    Name.Text = sname;
                }
            }
            
            db.closeConnection();
            
            
                Scroll_Menu.Visibility = Visibility.Hidden;
                Scroll_Notes.Visibility = Visibility.Hidden;
                var awaiter = Task.Delay(2000).GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    Hello_1.Visibility = Visibility.Hidden;
                    Hello_2.Visibility = Visibility.Hidden;
                    Scroll_Menu.Visibility = Visibility.Visible;
              
                    
                    awaiter = Task.Delay(0).GetAwaiter();
                });
            }
        }

        int s = 0;
        string i = "0";
       

        private void Button_Menu(object sender, RoutedEventArgs e)
        {
            Scroll_Notes.Visibility = Visibility.Visible;
            Scroll_Notes.Opacity = 1;
            string UserName = Name.Text;
          
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.openConnection();



            MySqlCommand command = new MySqlCommand("SELECT `login`, `post`  FROM `users` WHERE  `secondname`=@US", db.getConnection());
            command.Parameters.Add("@US", MySqlDbType.VarChar).Value = UserName;

            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var userlogin = reader["login"].ToString();
                    var userpost = reader["post"].ToString();
                    LoginMenu.Text += " " + userlogin;
                    RangMenu.Text +=" "+ userpost;
                   
                }
            }


            string appDir = Environment.CurrentDirectory;
            string Page = appDir + "/Sound/Page.wav";
            _mpBgr.Open(new Uri(@Page, UriKind.Absolute));
            _mpBgr.Play();
            string user_sname = Name.Text;
            string user_fname = Hello_User.Text;
            
            
           
            if (s == 0)
            {
                s += 1;
                user_sname = user_sname.ToUpper().Remove(1);
                user_fname = user_fname.ToUpper().Remove(1);
                i= user_sname + user_fname;
            }
           

            if (this.Width == 300)
            {
                Name.Text = i;
                this.Width = 600;
                Menu.Width = 300;
                Main.Margin = new Thickness(300, 0, 0, 0);
                this.Left -= 150;
                Scroll_Menu.Visibility = Visibility.Hidden;
            }
           else
            {
                Name.Text = i;
                this.Width = 1300;
                Menu.Width = 300;
                Notes.Margin = new Thickness(600, 0, 0, 0);
                Main.Margin = new Thickness(300, 0, 0, 0);
                this.Left -= 150;
                Scroll_Menu.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Notes(object sender, RoutedEventArgs e)
        {
            




            string appDir = Environment.CurrentDirectory;
            string Page = appDir + "/Sound/Page.wav";
            _mpBgr.Open(new Uri(@Page, UriKind.Absolute));
            _mpBgr.Play();
            if (this.Width == 300)
            {
                this.Width = 1000;
                Notes.Width = 700;
                this.Left -= 350;
                Scroll_Notes.Visibility = Visibility.Hidden;
                Notes.Margin = new Thickness(300, 0, 0, 0);
            }
            else
            {
                this.Width = 1300;
                Notes.Width = 700;
                Notes.Margin = new Thickness(600, 0, 0, 0);
                this.Left -= 350;
                Scroll_Notes.Visibility = Visibility.Hidden;
            }
        }

        private void Back_Menu_Click(object sender, RoutedEventArgs e)
        {
            string appDir = Environment.CurrentDirectory;
            string PageTurn = appDir + "/Sound/PageTurn.wav";
            _mpBgr.Open(new Uri(@PageTurn, UriKind.Absolute));
            _mpBgr.Play();
            if (this.Width == 600)
            {
                Menu.Width = 0;
                this.Width = 300;
                Main.Margin = new Thickness(0, 0,0, 0);
                this.Left = 620;
                Scroll_Menu.Visibility = Visibility.Visible;
            } 
            else
            {
                Menu.Width = 0;
                this.Width = 1000;
                Main.Margin = new Thickness(0, 0, 700, 0);
                Notes.Margin = new Thickness(0, 0,0, 0);
                this.Left = 300;
                Scroll_Menu.Visibility = Visibility.Visible;
            }
        }

        private void Back_Notes_Click(object sender, RoutedEventArgs e)
        {
            string appDir = Environment.CurrentDirectory;
            string PageTurn = appDir + "/Sound/PageTurn.wav";
            _mpBgr.Open(new Uri(@PageTurn, UriKind.Absolute));
            _mpBgr.Play();
            if (this.Width==1000)
            {
                Notes.Width = 0;
                this.Width = 300;
                this.Left = 620;
                Main.Margin = new Thickness(0, 0, 0, 0);
                Scroll_Notes.Visibility = Visibility.Visible;
            }
            else
            {
                Notes.Width = 0;
                this.Width = 600;
                this.Left = 450;
                Scroll_Notes.Visibility = Visibility.Visible;
            }
        }

        private void User_Exit(object sender, RoutedEventArgs e)
        {
            string appDir = Environment.CurrentDirectory;
            string ExitPerson = appDir + "/Sound/ExitPerson.wav";
            _mpBgr.Open(new Uri(@ExitPerson, UriKind.Absolute));
            _mpBgr.Play();
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }
        
        private void Swap_Color(object sender, RoutedEventArgs e)
        {
            
            if (Color_Border.Opacity ==0.99) 
            {
                Circle.Margin = new Thickness(18, 0, 0, 0); Color_Border.Opacity = 1;

                Color_Border.Background = new SolidColorBrush(Color.FromRgb(143, 209, 1));
                Color_Border.BorderThickness = new Thickness(2);
                Title_Notes.Background= new SolidColorBrush(Color.FromRgb(204, 204, 204));
                Notes.Background = new SolidColorBrush(Color.FromRgb(119, 119, 119));
                Main.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                Menu.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                Title.Background= new SolidColorBrush(Color.FromRgb(204, 204, 204));
                SwapColor.Background= new SolidColorBrush(Color.FromRgb(204, 204, 204));
                TitleText.Foreground= new SolidColorBrush(Color.FromRgb(0, 0, 0));
                Programm.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                Version.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                Creators.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                Back_Menu.Background= new SolidColorBrush(Color.FromRgb(204, 204, 204));
                Back_Notes.Background= new SolidColorBrush(Color.FromRgb(204, 204, 204));
                Name_Border.Background = new SolidColorBrush(Color.FromRgb(53, 7, 183));
                RangMenu.Background= new SolidColorBrush(Color.FromRgb(53, 7, 183));
                LoginMenu.Background = new SolidColorBrush(Color.FromRgb(53, 7, 183));
                Scroll_Menu.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
                Scroll_Notes.Background = new SolidColorBrush(Color.FromRgb(204, 204, 204));
            }
            else
            {  
                Circle.Margin = new Thickness(-18, 0, 0, 0); Color_Border.Opacity = 0.99;
                Color_Border.BorderThickness = new Thickness(2);
                Color_Border.Background = new SolidColorBrush(Color.FromRgb(95,10,10));
                Title_Notes.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Notes.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Main.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Menu.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Title.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                SwapColor.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                TitleText.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                Programm.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Version.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Creators.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Back_Menu.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Back_Notes.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Name_Border.Background = new SolidColorBrush(Color.FromRgb(80, 47, 134));
                RangMenu.Background = new SolidColorBrush(Color.FromRgb(80, 47, 134));
                LoginMenu.Background = new SolidColorBrush(Color.FromRgb(80, 47, 134));
                Scroll_Menu.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
                Scroll_Notes.Background = new SolidColorBrush(Color.FromRgb(85, 85, 85));
            }
        }



        private void Text_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = Rich_Text.Selection.GetPropertyValue(Inline.FontWeightProperty);
            Bold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            bool bool_bold = (bool)Bold.IsChecked;
            if (bool_bold)
            {
                Bold.Opacity = 0.99;
                Bold.Background = new SolidColorBrush(Color.FromRgb(95, 10, 10));
            }
            else
            {
                Bold.Opacity = 1;
                Bold.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }
            temp = Rich_Text.Selection.GetPropertyValue(Inline.FontStyleProperty);
            Corse.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            bool bool_corse = (bool)Corse.IsChecked;
            if (bool_corse)
            {
                Corse.Opacity = 0.99;
                Corse.Background = new SolidColorBrush(Color.FromRgb(95, 10, 10));
            }
            else
            {
                Corse.Opacity = 1;
                Corse.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }
            temp = Rich_Text.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            Underline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));
            bool bool_underline = (bool)Underline.IsChecked;
            if (bool_underline)
            {
                Underline.Opacity = 0.99;
                Underline.Background = new SolidColorBrush(Color.FromRgb(95, 10, 10));
            }
            else
            {
                Underline.Opacity = 1;
                Underline.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            if (Bold.Opacity == 1)
            {
                Bold.Opacity = 0.99;
                Bold.Background= new SolidColorBrush(Color.FromRgb(95, 10, 10));
            }
            else
            {
                Bold.Opacity = 1;
                Bold.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }
        }

        private void Corse_Click(object sender, RoutedEventArgs e)
        {
            if (Corse.Opacity == 1)
            {
                Corse.Opacity = 0.99;
                Corse.Background = new SolidColorBrush(Color.FromRgb(95, 10, 10));
            }
            else
            {
                Corse.Opacity = 1;
                Corse.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            if (Underline.Opacity == 1)
            {
                Underline.Opacity = 0.99;
                Underline.Background = new SolidColorBrush(Color.FromRgb(95, 10, 10));
            }
            else
            {
                Underline.Opacity = 1;
                Underline.Background = new SolidColorBrush(Color.FromRgb(51, 51, 51));
            }
        }

        private void FontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamily.SelectedItem != null)
                Rich_Text.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, FontFamily.SelectedItem);
        }

        private void FontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSize.SelectedItem != null)
                Rich_Text.Selection.ApplyPropertyValue(formattingProperty: Inline.FontSizeProperty, FontSize.SelectedItem);
        }

        public void Open_Click(object sender, RoutedEventArgs e)
        {
            MainSave.Visibility = Visibility.Hidden;
            Alarm.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            Alarm.Visibility = Visibility.Visible;
            Alarm.Text = "Нажмите кнопку загрузить";
            textBoxSource.Visibility = Visibility.Hidden;
            string post = RangMenu.Text;
            post = post.Remove(0, 7);
            string login = LoginMenu.Text;
            login = login.Remove(0, 8);
            Open_File.Visibility = Visibility.Hidden;
            Save.Visibility = Visibility.Hidden;
            Open Open = new Open();
            Loaded_File.Visibility = Visibility.Visible;
          

            Open.Post_User.Text = post;
            Open.Name_User.Text = login;
            Open.Show();
        }
  


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            textBoxSource.Visibility= Visibility.Visible;
            MainSave.Visibility = Visibility.Visible;
            Save.Visibility = Visibility.Hidden;
        }

         public void MainSave_Click(object sender, RoutedEventArgs e)
        {
            Alarm.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            SaveFileDialog sfd = new SaveFileDialog();

            string Dir = Environment.CurrentDirectory;
            sfd.FileName = Dir + "/Notes";
            string Error = Dir + "/Sound/Error.wav";
            string Savemus = Dir + "/Sound/Save.wav";
            string login = LoginMenu.Text;
            string post = RangMenu.Text;
            login = login.Remove(0, 8);
            post = post.Remove(0, 7);
            string sourse = textBoxSource.Text;
                if (sourse == "")
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    Alarm.Text = "Имя файла не может быть пустым";
                    Alarm.Visibility = Visibility.Visible;
                    
                }
                else if (sourse.Length>30)
                {
                Alarm.Text = "Длина имени файла не может быть больше 30 символов";
                Alarm.Visibility = Visibility.Visible;
                }
                else
                {

                    Alarm.Visibility = Visibility.Hidden;
                    sfd.FileName = sfd.FileName + "/" + sourse+"_log-"+login+"_"+post + ".rtf";
                if (File.Exists(sfd.FileName))
                {
                    _mpBgr.Open(new Uri(@Error, UriKind.Absolute));
                    _mpBgr.Play();
                    Alarm.Text = "Такой файл уже есть";
                    Alarm.Visibility = Visibility.Visible;
                }
                else
                {

                    _mpBgr.Open(new Uri(@Savemus, UriKind.Absolute));
                    _mpBgr.Play();
                    Alarm.Visibility = Visibility.Hidden;
                    Alarm.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    Alarm.Text = "Успешно сохранено";
                    Alarm.Visibility = Visibility.Visible;
                    var awaiter = Task.Delay(1000).GetAwaiter();
                    awaiter.OnCompleted(() =>
                    {
                        Alarm.Visibility = Visibility.Hidden;
                       
                        awaiter = Task.Delay(0).GetAwaiter();
                    });

                    FileStream fileStream = new FileStream(sfd.FileName, FileMode.Create);
                    TextRange range = new TextRange(Rich_Text.Document.ContentStart, Rich_Text.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Rtf);
                    fileStream.Close();
                    MainSave.Visibility = Visibility.Hidden;
                    textBoxSource.Visibility = Visibility.Hidden;
                    textBoxSource.Text = "";
                    Save.Visibility = Visibility.Visible;
                }
                
                
            }
        }

        private void Loaded_File_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginMenu.Text;
            login = login.Remove(0, 8);
            Loaded_File.Visibility = Visibility.Visible;
            Close_File.Visibility = Visibility.Visible;
            if (DataBank.Name_File == "")
            {
                Reload_Save.Visibility = Visibility.Hidden;
                Close_File.Visibility = Visibility.Hidden;
                Alarm.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                Alarm.Text = "Вы не выбрали файл на открытие";
                Alarm.Visibility = Visibility.Visible;
                Loaded_File.Visibility = Visibility.Hidden;
                var awaiter = Task.Delay(1500).GetAwaiter();
                awaiter.OnCompleted(() =>
                {
                    Alarm.Visibility = Visibility.Hidden;

                    awaiter = Task.Delay(0).GetAwaiter();
                });
                Open_File.Visibility = Visibility.Visible;
                Save.Visibility = Visibility.Visible;
            }
            else
            {
                
                if (DataBank.Name_File.Contains(login))
                {
                OpenFileDialog openFile =new OpenFileDialog();

                openFile.Filter = "RichText files (*.rtf)|*.rtf|All files (*.*)|*.*";
                openFile.FileName = DataBank.Name_File;
                TextRange tr = new TextRange(
                Rich_Text.Document.ContentStart, Rich_Text.Document.ContentEnd);
                using (FileStream fs = File.Open(openFile.FileName, FileMode.Open))
                {
                    tr.Load(fs, DataFormats.Rtf);
                    fs.Close();
                }
                    Rich_Text.IsReadOnly = false;
                    Loaded_File.Visibility = Visibility.Hidden;
                    Reload_Save.Visibility = Visibility.Visible;
                    Alarm.Text = DataBank.Alarm_Swap_Text+" Сохранить=Перезаписать! "+"Хотите выйти, нажмите закрыть!";
                    Alarm.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    Open_File.Visibility = Visibility.Visible;
                    Alarm.Visibility = Visibility.Visible;
                    DataBank.Name_File_Reload = DataBank.Name_File;
                    DataBank.Name_File = "";
                }
                else
                {
                    Loaded_File.Visibility = Visibility.Hidden;
                    Reload_Save.Visibility = Visibility.Hidden;
                    Open_File.Visibility = Visibility.Visible;
                    Alarm.Text = DataBank.Alarm_Swap_Text + " Хотите выйти, нажмите закрыть!";
                    Alarm.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    Alarm.Visibility = Visibility.Visible;
                    OpenFileDialog openFile = new OpenFileDialog();
                    openFile.Filter = "RichText files (*.rtf)|*.rtf|All files (*.*)|*.*";
                    openFile.FileName = DataBank.Name_File;
                    TextRange tr = new TextRange(
                    Rich_Text.Document.ContentStart, Rich_Text.Document.ContentEnd);
                    using (FileStream fs = File.Open(openFile.FileName, FileMode.Open))
                    {
                        tr.Load(fs, DataFormats.Rtf);
                        fs.Close();
                    }
                    Rich_Text.IsReadOnly = true;
                }
            }

        }

        private void Reload_Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = DataBank.Name_File_Reload;
            FileStream fileStream = new FileStream(sfd.FileName, FileMode.Create);
            TextRange range = new TextRange(Rich_Text.Document.ContentStart, Rich_Text.Document.ContentEnd);
            range.Save(fileStream, DataFormats.Rtf);
            fileStream.Close();
            DataBank.Name_File_Reload = "";
            Rich_Text.Document.Blocks.Clear();
            Save.Visibility = Visibility.Visible;
            Reload_Save.Visibility = Visibility.Hidden;
            Close_File.Visibility = Visibility.Hidden;
            Rich_Text.IsReadOnly = false;
            Alarm.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            Alarm.Text = "Вы успешно перезаписали файл";
            var awaiter = Task.Delay(1500).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Alarm.Visibility = Visibility.Hidden;

                awaiter = Task.Delay(0).GetAwaiter();
            });
        }

        private void Close_File_Click(object sender, RoutedEventArgs e)
        {
            Rich_Text.Document.Blocks.Clear();
            Save.Visibility = Visibility.Visible;
            Rich_Text.IsReadOnly = false;
            Reload_Save.Visibility = Visibility.Hidden;
            Close_File.Visibility = Visibility.Hidden;
            Alarm.Foreground = new SolidColorBrush(Color.FromRgb(0, 255, 0));
            Alarm.Text = "Вы закрыли файл";
            var awaiter = Task.Delay(1500).GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Alarm.Visibility = Visibility.Hidden;

                awaiter = Task.Delay(0).GetAwaiter();
            });

        }
    }
}
