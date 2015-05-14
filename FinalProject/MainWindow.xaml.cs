using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;
using System.Globalization;
using System.Threading;

public class ADD_NEW_DATA
{
    public ADD_NEW_DATA()
    {
        // default constructor
    }

    public ADD_NEW_DATA(string C1, string C2, string C3)
    {
        Column_1 = C1;
        Column_2 = C2;
        Column_3 = C3;
    }

    public string Column_1 { get; set; }
    public string Column_2 { get; set; }
    public string Column_3 { get; set; }
}


namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string dbConnectionString = @"Data Source=checkBookDB.db;Version=3;";
        public MainWindow()
        {
            InitializeComponent();
            fill_combo();
        }
        void fill_combo()
        {
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
            //Open connection to database
            try
            {
                sqliteCon.Open();
                string Query = "select * from checkBookDB";
                SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
                //createCommand.ExecuteNonQuery();
                SQLiteDataReader dr = createCommand.ExecuteReader();
                while (dr.Read())
                {
                    string name = dr.GetString(0);
                    combo_names.Items.Add(name);
                }
                sqliteCon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

       private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            ADD_NEW_DATA data = new ADD_NEW_DATA(txt_payee.Text, txt_payment.Text, date_pkr.Text);
            lst_vw.Items.Add(data);
            
           string s = "";
           if(combo_accnt.SelectedIndex>=0)
               s= ((ComboBoxItem)combo_accnt.SelectedItem).Content.ToString();
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
           //Open connection to database
            try 
            {
                sqliteCon.Open();
                string Query = "insert into checkBookDB (payee,account,payment,date,card) values('"+this.txt_payee.Text+"','"+s+"','"+this.txt_payment.Text+"','"+this.date_pkr.Text+"','"+this.txt_card.Text+"')";
                SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
                createCommand.ExecuteNonQuery();
                sqliteCon.Close();
            
            }
           catch(Exception ex)
           {
               MessageBox.Show(ex.Message);
            
           }
        }

       private void btn_ld_tbls_Click(object sender, RoutedEventArgs e)
       {
           
           SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
           //Open connection to database
           try
           {
               sqliteCon.Open();
               string Query = "select payee,account,payment,card from checkBookDB";
               SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
               createCommand.ExecuteNonQuery();
               SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(createCommand);
               DataTable dt = new DataTable("checkBookDB");
               dataAdp.Fill(dt);
               dataGrid1.ItemsSource = dt.DefaultView;
               dataAdp.Update(dt);
               sqliteCon.Close();

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);

           }
       }

       private void btn_total_Click(object sender, RoutedEventArgs e)
       {
           SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
           //Open connection to database
           try
           {
               sqliteCon.Open();
               string Query = "select sum(payment) from checkBookDB";
               SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
               createCommand.ExecuteNonQuery();
               SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(createCommand);
               SQLiteDataReader dr = createCommand.ExecuteReader();
               txt_total.Text =dr.GetValue(0).ToString();
               sqliteCon.Close();

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);

           }
       }

       private void combo_names_SelectionChanged(object sender, SelectionChangedEventArgs e)
       {
           SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionString);
           //Open connection to database
           
           try
           {
               sqliteCon.Open();
               string Query = "select payee,account,payment,card from checkBookDB where payee = '"+combo_names.Text+"'";
               SQLiteCommand createCommand = new SQLiteCommand(Query, sqliteCon);
               createCommand.ExecuteNonQuery();
               SQLiteDataAdapter dataAdp = new SQLiteDataAdapter(createCommand);
               DataTable dt = new DataTable("checkBookDB");
               dataAdp.Fill(dt);
               dataGrid1.ItemsSource = dt.DefaultView;
               dataAdp.Update(dt);
               sqliteCon.Close();
              

           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);

           }
       }

      
    }
}
