using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace OJTI_2017
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string path = "";
        public static Form1 frm1;
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\C# Rezolvari\\OJTI 2017\\Turism.mdf;Integrated Security=True;Connect Timeout=30");
        private void Form1_Load(object sender, EventArgs e)
        {
            frm1 = this;
            con.Open();
            this.Show();
            SqlCommand delloc = new SqlCommand("DELETE FROM Localitati",con);
            delloc.ExecuteNonQuery();
            SqlCommand delimg = new SqlCommand("DELETE FROM Imagini", con);
            delimg.ExecuteNonQuery();
            SqlCommand delplan = new SqlCommand("DELETE FROM Planificari", con);
            delplan.ExecuteNonQuery();
            // folderBrowserDialog1.ShowDialog();
            path =folderBrowserDialog1.SelectedPath.ToString();
            StreamReader sr = new StreamReader(path + "\\planificari.txt");
            string line;
            while((line=sr.ReadLine())!=null)
            {
                int verif = 0;
                if(line.Split('*')[1]==" ocazional ")
                { verif = 1; }
                if (line.Split('*')[1] == " anual ")
                { verif = 2; }
                if (line.Split('*')[1] == " lunar ")
                { verif = 3; }
                if(verif==1) //ocazional
                {
                    string numeOras = line.Split('*')[0];
                    string frecv = line.Split('*')[1];
                    string dataStart = line.Split('*')[2];
                    string dataStop = line.Split('*')[3];
                    char[] arr = new char[1000];
                    arr = line.ToCharArray();
                    int ct = 0;
                    string cale = "";
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] == '*')
                            ct++;
                        if (ct == 4 & arr[i] == '*')
                            i++;
                        if (ct >= 4)
                            cale += arr[i].ToString();
                    }
                    SqlCommand cmd1 = new SqlCommand("Insert into Localitati(Nume) values (@param1)",con);
                    cmd1.Parameters.Add("@param1", numeOras);
                    cmd1.ExecuteNonQuery();
                    int idLocalitate=0;
                    SqlCommand cmd2 = new SqlCommand("SELECT IDLocalitate FROM Localitati where Nume=@param1",con);
                    cmd2.Parameters.Add("@param1", numeOras);
                    SqlDataReader sdr = cmd2.ExecuteReader();
                    sdr.Read();
                    idLocalitate = Convert.ToInt32(sdr[0]);
                    sdr.Close();
                    ct -= 4;
                    for(int i=0; i<=ct; i++)
                    {
                        SqlCommand cmd3 = new SqlCommand("INSERT INTO Imagini(IdLocalitate,CaleFisier) values (@param1,@param2)", con);
                        cmd3.Parameters.Add("@param1", idLocalitate);
                        cmd3.Parameters.Add("@param2", cale.Split('*')[i]);
                        cmd3.ExecuteNonQuery();
                    }
                    SqlCommand cmd4 = new SqlCommand("Insert into Planificari(IDLocalitate,Frecventa,DataStart,DataStop) values (@param4,@param1,@param2,@param3)",con);
                    cmd4.Parameters.Add("@param1", frecv);
                    DateTime data1 = new DateTime(Convert.ToInt32(dataStart.Split('.')[2]), Convert.ToInt32(dataStart.Split('.')[1]), Convert.ToInt32(dataStart.Split('.')[0]));
                    DateTime data2 = new DateTime(Convert.ToInt32(dataStop.Split('.')[2]), Convert.ToInt32(dataStop.Split('.')[1]), Convert.ToInt32(dataStop.Split('.')[0]));
                    cmd4.Parameters.Add("@param2", data1);
                    cmd4.Parameters.Add("@param3", data2);
                    cmd4.Parameters.Add("@param4", idLocalitate);
                    cmd4.ExecuteNonQuery();




                }
                if(verif==2) // anual
                {
                    string numeOras = line.Split('*')[0];
                    string frecv = line.Split('*')[1];
                    int zi = Convert.ToInt32(line.Split('*')[2]);
                    char[] arr = new char[1000];
                    arr = line.ToCharArray();
                    int ct = 0;
                    string cale = "";
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] == '*')
                            ct++;
                        if (ct == 3 & arr[i] == '*')
                            i++;
                        if (ct >= 3)
                            cale += arr[i].ToString();
                    }
                    SqlCommand cmd1 = new SqlCommand("Insert into Localitati(Nume) values (@param1)", con);
                    cmd1.Parameters.Add("@param1", numeOras);
                    cmd1.ExecuteNonQuery();
                    int idLocalitate = 0;
                    SqlCommand cmd2 = new SqlCommand("SELECT IDLocalitate FROM Localitati where Nume=@param1", con);
                    cmd2.Parameters.Add("@param1", numeOras);
                    SqlDataReader sdr = cmd2.ExecuteReader();
                    sdr.Read();
                    idLocalitate = Convert.ToInt32(sdr[0]);
                    sdr.Close();
                    ct -= 3;
                    for (int i = 0; i <= ct; i++)
                    {
                        SqlCommand cmd3 = new SqlCommand("INSERT INTO Imagini(IdLocalitate,CaleFisier) values (@param1,@param2)", con);
                        cmd3.Parameters.Add("@param1", idLocalitate);
                        cmd3.Parameters.Add("@param2", cale.Split('*')[i]);
                        cmd3.ExecuteNonQuery();
                    }
                    SqlCommand cmd4 = new SqlCommand("INSERT INTO Planificari(IDLocalitate,Frecventa,Ziua) values (@param1,@param2,@param3)", con);
                    cmd4.Parameters.Add("@param1", idLocalitate);
                    cmd4.Parameters.Add("@param2", frecv);
                    cmd4.Parameters.Add("@param3", zi);
                    cmd4.ExecuteNonQuery();


                }
                if(verif==3) // lunar
                {
                    string numeOras = line.Split('*')[0];
                    string frecv = line.Split('*')[1];
                    int zi = Convert.ToInt32(line.Split('*')[2]);
                    char[] arr = new char[1000];
                    arr = line.ToCharArray();
                    int ct = 0;
                    string cale = "";
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (arr[i] == '*')
                            ct++;
                        if (ct == 3 & arr[i] == '*')
                            i++;
                        if (ct >= 3)
                            cale += arr[i].ToString();
                    }
                    SqlCommand cmd1 = new SqlCommand("Insert into Localitati(Nume) values (@param1)", con);
                    cmd1.Parameters.Add("@param1", numeOras);
                    cmd1.ExecuteNonQuery();
                    int idLocalitate = 0;
                    SqlCommand cmd2 = new SqlCommand("SELECT IDLocalitate FROM Localitati where Nume=@param1", con);
                    cmd2.Parameters.Add("@param1", numeOras);
                    SqlDataReader sdr = cmd2.ExecuteReader();
                    sdr.Read();
                    idLocalitate = Convert.ToInt32(sdr[0]);
                    sdr.Close();
                    ct -= 3;
                    for (int i = 0; i <= ct; i++)
                    {
                        SqlCommand cmd3 = new SqlCommand("INSERT INTO Imagini(IdLocalitate,CaleFisier) values (@param1,@param2)", con);
                        cmd3.Parameters.Add("@param1", idLocalitate);
                        cmd3.Parameters.Add("@param2", cale.Split('*')[i]);
                        cmd3.ExecuteNonQuery();
                    }
                    SqlCommand cmd4 = new SqlCommand("INSERT INTO Planificari(IDLocalitate,Frecventa,Ziua) values (@param1,@param2,@param3)", con);
                    cmd4.Parameters.Add("@param1", idLocalitate);
                    cmd4.Parameters.Add("@param2", frecv);
                    cmd4.Parameters.Add("@param3", zi);
                    cmd4.ExecuteNonQuery();

                }

            }
             
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            frm2.Show();
            frm1.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            frm1.Hide();
        }
    }
}
