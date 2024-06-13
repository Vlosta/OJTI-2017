using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace OJTI_2017
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\C# Rezolvari\\OJTI 2017\\Turism.mdf;Integrated Security=True;Connect Timeout=30");


        
        private void Form3_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'turismDataSet2.Planificari' table. You can move, or remove it, as needed.
            this.planificariTableAdapter.Fill(this.turismDataSet2.Planificari);
            DataTable dt = new DataTable();
            con.Open();
            dt.NewRow();
            dt.Columns.Add();
            dt.Columns[0].ColumnName = "Nume";

            // label1.Text = dataGridView1.RowCount.ToString();
            string[] alou = new string[100];
            SqlCommand cmd = new SqlCommand("SELECT NUME FROM Localitati",con);
            SqlDataReader sdr = cmd.ExecuteReader();
            int k = 0;
            while (sdr.Read())
            {
                alou[k] = sdr[0].ToString();
                k++;
            }
            sdr.Close();
            DataRow rand;
            dt.Columns.Add();
            dt.Columns[1].ColumnName = "DatStart";
            dt.Columns.Add();
            dt.Columns[2].ColumnName = "DatStop";
            dt.Columns.Add();
            dt.Columns[3].ColumnName = "Frecventa";
            dt.Columns.Add();
            dt.Columns[4].ColumnName = "Ziua";
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                    rand = dt.NewRow();
                    rand[0] = alou[i];
                    rand[1] = dataGridView1[1, i].Value;
                    rand[2] = dataGridView1[2, i].Value;
                    rand[3] = dataGridView1[3, i].Value;
                    rand[4] = dataGridView1[4, i].Value;
                    dt.Rows.Add(rand);

            }
            dataGridView3.DataSource = dt;
            con.Close();

        }

        private void Close(object sender, FormClosedEventArgs e)
        {
            Form1.frm1.Close();

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
            /* SqlCommand sel = new SqlCommand("Select IDLocalitate,Frecventa,DataStart,DataStop,Ziua from Planificari", con);
            SqlDataReader sdr = sel.ExecuteReader();
            DateTime alou = sdr.GetDateTime(3);
            label1.Text = alou.ToString();
           /* while (sdr.Read())
            {

                if (dateTimePicker2.Value.CompareTo(Convert.ToDateTime(sdr[3])) <= 0)
                {
                    SqlCommand insert = new SqlCommand("Insert into PlanificariSortat(IDLocalitate,Frecventa,DataStart,DataStop,Ziua) values (@param1,@param2,@param3,@param4,@param5)", con);
                    insert.Parameters.Add("@param1", sdr[0]);
                    insert.Parameters.Add("@param2", sdr[1]);
                    insert.Parameters.Add("@param3", sdr[2]);
                    insert.Parameters.Add("@param4", sdr[3]);
                    insert.Parameters.Add("@param5", sdr[4]);

                } 
            }
            sdr.Close();*/
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            /*SqlCommand sel = new SqlCommand("Select IDLocalitate,Frecventa,DataStart,DataStop,Ziua from Planificari", con);
            SqlDataReader sdr = sel.ExecuteReader();
            while (sdr.Read())
            {

                if (dateTimePicker2.Value.CompareTo(Convert.ToDateTime(sdr[3])) <= 0)
                {
                    SqlCommand insert = new SqlCommand("Insert into PlanificariSortat(IDLocalitate,Frecventa,DataStart,DataStop,Ziua) values (@param1,@param2,@param3,@param4,@param5)", con);
                    insert.Parameters.Add("@param1", sdr[0]);
                    insert.Parameters.Add("@param2", sdr[1]);
                    insert.Parameters.Add("@param3", sdr[2]);
                    insert.Parameters.Add("@param4", sdr[3]);
                    insert.Parameters.Add("@param5", sdr[4]);

                }
            }
            sdr.Close();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add();
            dataTable.Columns[0].ColumnName = "Nume";
            dataTable.Columns.Add();
            dataTable.Columns[1].ColumnName = "DataStart";
            dataTable.Columns.Add();
            dataTable.Columns[2].ColumnName = "DataStop";
            dataTable.Columns.Add();
            dataTable.Columns[3].ColumnName = "Frecventa";
            dataTable.Columns.Add();
            dataTable.Columns[4].ColumnName = "Ziua";
            SqlCommand cmdSel = new SqlCommand("Select * FROM Planificari",con);
            SqlDataReader rdr = cmdSel.ExecuteReader();
            while (rdr.Read())
            {
              
                DataRow rand = dataTable.NewRow();
                DateTime data1, data2, data3;
                //rand[0] = "MUIE";
                //dataTable.Rows.Add(rand);
                string frecventa = rdr[2].ToString();
                frecventa = frecventa.Trim();
                label1.Text = frecventa.Length.ToString();
                if (frecventa == "ocazional")
                {
                   // richTextBox1.Text += "S-A TRECUT LA OCAZIONAL\n";

                    data1 = Convert.ToDateTime(rdr[3]);
                    data2 = Convert.ToDateTime(rdr[4]);
                   // richTextBox1.Text += data1.ToString();
//                    richTextBox1.Text += data2.ToString();

                    if (DateTime.Compare(dt1,data1)==-1 & DateTime.Compare(data2,dt2)==-1)
                    {
                        rand[0] = rdr[1];
                        rand[1] = rdr[3];
                        rand[2] = rdr[4];
                        rand[3] = rdr[2];
                        rand[4] = rdr[5];
                        dataTable.Rows.Add(rand);
 
                    }
                }
                else if (frecventa=="anual")
                {
 
                    int an = dt1.Year,luna=1,zi=1;
                    DateTime dt3 = new DateTime(an, luna, zi);
                    double zileAdaugat = Convert.ToDouble(rdr[5]);
                    dt3=dt3.AddDays(zileAdaugat);
                    if(DateTime.Compare(dt1,dt3)==-1 & DateTime.Compare(dt3,dt2)==-1)
                    {
                        rand[0] = rdr[1];
                        rand[1] = rdr[3];
                        rand[2] = rdr[4];
                        rand[3] = rdr[2];
                        rand[4] = rdr[5];
                        dataTable.Rows.Add(rand);
                        //richTextBox1.Text += "S-A TRECUT LA anual\n";
                    }
                   // richTextBox1.Text += dt3.ToString();
                }
                else if(frecventa=="lunar")
                {
 
                    int an = dt1.Year, luna = dt1.Month, zi = 1;
                    DateTime dt3 = new DateTime(an, luna, zi);
                    double zileAdaugat = Convert.ToDouble(rdr[5]);
                    dt3=dt3.AddDays(zileAdaugat);
                    if (DateTime.Compare(dt1, dt3) == -1 & DateTime.Compare(dt3, dt2) == -1)
                    {
                        rand[0] = rdr[1];
                        rand[1] = rdr[3];
                        rand[2] = rdr[4];
                        rand[3] = rdr[2];
                        rand[4] = rdr[5];
                        dataTable.Rows.Add(rand);
                       // richTextBox1.Text += "S-A TRECUT LA lunar\n";


                    }

                }
            }
            rdr.Close();
            /*DataTable datatable2 = new DataTable();
            datatable2 = dataTable;
            datatable2.Clear();
            for (int i=0; i<dataTable.Rows.Count; i++)
            {
                SqlCommand cmdSelect = new SqlCommand("SELECT Nume from Localitati where IDLocalitate=@param1", con);
                SqlDataReader r = cmdSelect.ExecuteReader();
                r.Read();
                DataRow rand1 = dataTable.Rows[i];
                rand1[0] = Convert.ToString(r[0]);
                datatable2.Rows.Add(rand1);
                
            }*/
            con.Close(); 
       
            dataGridView2.DataSource = dataTable;
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //  richTextBox1.Text += dataTable.Rows[i].ItemArray[1].ToString()+'\n';
            DataTable tabloumatii = new DataTable();
            tabloumatii.Columns.Add();
            tabloumatii.Columns[0].ColumnName = "Nume";
            tabloumatii.Columns.Add();
            tabloumatii.Columns[1].ColumnName = "DataStart";
            tabloumatii.Columns.Add();
            tabloumatii.Columns[2].ColumnName = "DataStop";
            tabloumatii.Columns.Add();
            tabloumatii.Columns[3].ColumnName = "Frecventa";
            tabloumatii.Columns.Add();
            tabloumatii.Columns[4].ColumnName = "Ziua";
            for (DateTime d=dt1; DateTime.Compare(d,dt2)==1; d=d.AddDays(1))
            {
                for(int i=0;i<dataTable.Rows.Count; i++)
                {
                    if(dataTable.Rows[i].ItemArray[3].ToString()=="anual")
                    {
                        DateTime timpu = new DateTime(dt1.Year, 1, 1);
                        timpu = timpu.AddDays(Convert.ToDouble(dataTable.Rows[i].ItemArray[4]));
                        if(DateTime.Compare(d,timpu)==1)
                        {
                            tabloumatii.Rows.Add(dataTable.Rows[i]);
                        }
                    }
                    if (dataTable.Rows[i].ItemArray[3].ToString() == "lunar")
                    {
                        DateTime timpu = new DateTime(dt1.Year, dt1.Month, 1);
                        timpu = timpu.AddDays(Convert.ToDouble(dataTable.Rows[i].ItemArray[4]));
                        if (DateTime.Compare(d, timpu) == 1)
                        {
                            tabloumatii.Rows.Add(dataTable.Rows[i]);
                        }
                    }
                  /*  if (dataTable.Rows[i].ItemArray[3].ToString() == "ocazional")
                    {
                        DateTime ds=Convert.ToDateTime(dataTable.Rows[i].ItemArray[1]), ds1= Convert.ToDateTime(dataTable.Rows[i].ItemArray[2]);

                    }*/
                }
            }
            dataGridView4.DataSource = tabloumatii;
        }
    }
}
