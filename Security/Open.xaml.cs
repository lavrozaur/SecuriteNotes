using System;
using System.Windows;
using System.IO;
using System.Drawing;


namespace Secureity
{
    public partial class Open : Window
    {
        public Open()
        {
            InitializeComponent();
        }
        int k = 0;
        private void btnOpenFiles_Click(object sender, RoutedEventArgs e)
        {

            if (k == 0)
            {
                k = 1;
                string Dir = Environment.CurrentDirectory;
                Dir = Dir + "/Notes";

                DirectoryInfo dr = new DirectoryInfo(Dir);

                if (Post_User.Text == "Руководитель")
                {
                    foreach (var d in dr.GetFiles())
                    {


                        int len = d.Name.Length;
                        int num = 60 - len;
                        DateTime d1 = d.CreationTime;
                        DateTime d2 = d.LastWriteTime;

                        string result = d.Name.PadRight(80, ' ');

                        listBox1.Items.Add(result + "Cоздание: " + d1 + "     " + "Изменение: " + d2);


                    }
                }
                else if (Post_User.Text == "Администратор")
                {
                    foreach (var d in dr.GetFiles())
                    {
                        if (d.Name.Contains("Руководитель"))
                        {
                        }
                        else
                        {
                            int len = d.Name.Length;
                            int num = 60 - len;
                            DateTime d1 = d.CreationTime;
                            DateTime d2 = d.LastWriteTime;

                            string result = d.Name.PadRight(80, ' ');


                            listBox1.Items.Add(result + "Cоздание: " + d1 + "     " + "Изменение: " + d2);
                        }
                    }
                }
                else
                {
                    foreach (var d in dr.GetFiles())
                    {
                        if (d.Name.Contains("Руководитель"))
                        {
                        }
                        if (d.Name.Contains("Администратор"))
                        {
                        }
                        if (d.Name.Contains("Сотрудник"))
                        {
                            int len = d.Name.Length;
                            int num = 60 - len;
                            DateTime d1 = d.CreationTime;
                            DateTime d2 = d.LastWriteTime;

                            string result = d.Name.PadRight(80, ' ');


                            listBox1.Items.Add(result + " Cоздание: " + d1 + "     " + "Изменение: " + d2);
                        }

                    }
                }
            }
        }

       
        private void listBox1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
            string select = listBox1.SelectedItem.ToString();

            if (listBox1.SelectedItem != null)
            {
                if (listBox1.SelectedItem.ToString().Contains(Name_User.Text))
                { 
                    DataBank.Alarm_Swap_Text = "Вы открыли файл на запись и чтение!";
                    string Dir = Environment.CurrentDirectory; 
                    string openfile = "/Notes/";
                    openfile =openfile+ listBox1.SelectedItem.ToString().Substring(0,80).Trim();
                    Dir = Dir + openfile;
                    DataBank.Name_File = Dir;
                }else
                {
                    DataBank.Alarm_Swap_Text = "Вы открыли файл только на чтение!";
                 
                    string Dir = Environment.CurrentDirectory;

                    string openfile = "/Notes/";
                    openfile = openfile + listBox1.SelectedItem.ToString().Substring(0, 80).Trim();
                    Dir = Dir + openfile;
                    DataBank.Name_File = Dir;
                    StreamReader sr = new StreamReader(Dir);
                   
                    sr.Close();
                }
            }
            this.Close();
          

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
          
        }

        private void Grid_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
           
            this.Close();
        }
    }
}

