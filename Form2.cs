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
    public partial class Form2 : Form
    {
        Form1 form1 = Form1.frm1;
        public Form2()
        {
            InitializeComponent();
        }
        Bitmap resize;
        int ctImg=0;
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=G:\\C# Rezolvari\\OJTI 2017\\Turism.mdf;Integrated Security=True;Connect Timeout=30");
        Bitmap[] vectorImag = new Bitmap[11];
        private void Form2_Load(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmdSel = new SqlCommand("Select Nume from Localitati",con);
            SqlDataReader sdr=cmdSel.ExecuteReader();
            while(sdr.Read())
            {
                comboBox1.Items.Add(sdr[0]);
            }
            sdr.Close();
        }

        private void Close(object sender, FormClosedEventArgs e)
        {
            form1.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            //label5.Text=comboBox1.SelectedItem.ToString();
            SqlCommand cmdSelID = new SqlCommand("Select IDLocalitate from Localitati where Nume=@param1", con);
            cmdSelID.Parameters.Add("@param1", comboBox1.SelectedItem.ToString());
            SqlDataReader sdr = cmdSelID.ExecuteReader();
            sdr.Read();
            int idLocalitate = Convert.ToInt32(sdr[0]);
            sdr.Close();
            SqlCommand cmdImg = new SqlCommand("Select CaleFisier from Imagini where IDLocalitate=@param1", con);
            cmdImg.Parameters.Add("@param1", idLocalitate);
            SqlDataReader sdr1 = cmdImg.ExecuteReader();
            while(sdr1.Read())
            {
                comboBox2.Items.Add(sdr1[0]);
            }
            comboBox2.SelectedItem = comboBox1.Items[0];

            sdr1.Close();

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //label1.Text = Form1.path + "\\Imagini\\" + (comboBox2.SelectedItem.ToString()).Trim();
            Bitmap img = new Bitmap(Form1.path + "\\Imagini\\" + (comboBox2.SelectedItem.ToString()).Trim());
            resize = new Bitmap(img, new Size(pictureBox1.Width, pictureBox1.Height));
            pictureBox1.Image = resize;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add((comboBox2.SelectedItem.ToString()).Trim());
            ctImg++;
            vectorImag[ctImg] = resize;
        }
        private Bitmap unesteImg(Bitmap img1, Bitmap img2)
        {

            Bitmap imgRez= new Bitmap(img1.Width +img2.Width, img1.Height);
            Graphics g = Graphics.FromImage(imgRez);
            g.DrawImage(img1, 0, 0);
            g.DrawImage(img2, new Point(img1.Width, 0));
            return imgRez;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap imgDeSalvat=vectorImag[1];
            for (int i = 2; i <= ctImg; i++)
                imgDeSalvat = unesteImg(imgDeSalvat, vectorImag[i]);
            saveFileDialog1.ShowDialog();
            imgDeSalvat.Save(saveFileDialog1.FileName+".png");
        }
    }
}
