using System.Configuration;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net.Mail;
using MySqlX.XDevAPI;
using Tulpep.NotificationWindow;
using FireSharp;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using MySqlX.XDevAPI.Common;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SmartHome
{
   
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
        }

        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "***",
            BasePath = "***"
        };
        IFirebaseClient client;




        private void LoadDataIntoDataGridView()
        {

            MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
            con.Open();

            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "Select * from members";

            MySqlDataReader sdr = cmd.ExecuteReader();

            DataTable dtRecords = new DataTable();

            dtRecords.Load(sdr);

            dataGridView1.DataSource = dtRecords;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string constring = AppSettings.ConnectionString();

            MySqlConnection con = new MySqlConnection(constring);
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                MessageBox.Show("Connection created Succesfully");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
      

        private async void Form1_Load(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
                MessageBox.Show("Baðlantý hatasi.");


            


            //  rfid2();


            //  Application.Idle += new EventHandler(rfid2);
          //  Application.Idle += new EventHandler(hareketalgýlayýcý);
            

           //  Thread hrkt = new Thread(rfid2);
            //hrkt.Start();
            //rfid.Start();
            //hrkt.Start();

        }

        // object lockObject = new object();



        /*
        private async void hareketAlgýlaycý(object sender, EventArgs e)
        {
            string hareket_control = " ";
            FirebaseResponse response = await client.GetAsync("");
            Data result = response.ResultAs<Data>();

            while (result.Current_State == "1")
            {
                DialogResult result2;
                result2 = MessageBox.Show("Beklenmeyen bir hareket algýlandý! Fotoðraf çekilsin mi ?", "Tehlike", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result2 == DialogResult.Yes && hareket_control != "1")
                {
                    hareket_control = result.Current_State;
                    DialogResult rslt;
                    rslt = MessageBox.Show("Fotoðraf e-mail ile bildirilsin mi?", "Bildiri", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (rslt == DialogResult.Yes)
                    {
                        Data data = new Data()
                        {
                            Camera = "take"
                        };
                        var setter = client.Update("Camera/" + "/", data);
                    }
                }

            }


        }
        */

        private async void button2_Click(object sender, EventArgs e)
        {
            FirebaseResponse img = await client.GetAsync("");
            Data result = img.ResultAs<Data>();             //Deneme

          

            MailMessage mymessage = new MailMessage();
            SmtpClient smtp = new SmtpClient(); //istemci oluþturuldu




            smtp.Credentials = new System.Net.NetworkCredential("zeynepuzunsoyxzu@gmail.com", "chdsksqfmxfhyfsm");
            smtp.Port = 587;  //465 dene
            smtp.Host = "smtp.gmail.com";    //host sunucu olarak düþünülebilir. hotmailin sunucu aðýný (live) kullandýk.  
            smtp.EnableSsl = true;  //sunucu ile istemci arasýndaki verileri doðru adrese gönderene kadar þifreleme yapar
                                    // smtp.UseDefaultCredentials = true;
            mymessage.To.Add(textBox1.Text);  //mesajýmý bunu ekleme anlamý taþýr.
            mymessage.From = new MailAddress("zeynepuzunsoyxzu@gmail.com");
            mymessage.Subject = "Güvenlik Uyarýsý";
            mymessage.Body = "HIRSIZ VAR " + Process.Start(result.picture_url);

            


            smtp.Send(mymessage);

        }




        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Data data = new Data()
            {
                State = "Open"
            };
            var setter = client.Update("Door/" + "/", data);
           // textBox3.Text = "Kapý açýldý.";
           
        }




        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FirebaseResponse rsp = await client.GetAsync("");
            Data result = rsp.ResultAs<Data>();
            if (comboBox1.SelectedIndex == 0)
            {
               
                label7.Text = result.Lumen ;

                Data data = new Data()
                {
                    Lamp = "lamp1"
                };
                var setter = client.Set("Lamp/" + "/", data);
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                
                label7.Text = result.Lumen2 ;

                Data data = new Data()
                {
                    Lamp = "lamp2"
                };
                var setter = client.Update("Lamp/" + "/", data);

            }
            else 
            {
               
                label7.Text = result.Lumen3;
                Data data = new Data()
                {
                    Lamp = "lamp3"
                };
                var setter = client.Update("Lamp/" + "/", data);
            }


        }

        private async void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button10_Click(object sender, EventArgs e)
        {
            client = new FireSharp.FirebaseClient(config);
            if (client == null)
                MessageBox.Show("Baðlantý hatasi.");

            FirebaseResponse rsp = await client.GetAsync("");
            Data result = rsp.ResultAs<Data>();
            textBox2.Text = result.Current_State;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void label7_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("");

            Data result = response.ResultAs<Data>();
            label7.Text = result.Lumen3;

        }



        private async void label8_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("");
            Data result = response.ResultAs<Data>();
            label8.Text = result.RFID_Control;
        }



        private void button4_Click(object sender, EventArgs e)
        {
            Data data = new Data()
            {
                State = "Close"
            };
            var setter = client.Update("Door/" + "/", data);
           // textBox3.Text = "Kapý kapandý.";
           


        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                Data data = new Data()
                {
                    Lamp_Level = "high"
                };
                var setter = client.Update("LampLevel/" + "/", data);
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                Data data = new Data()
                {
                    Lamp_Level = "medium"
                };
                var setter = client.Update("LampLevel/" + "/", data);

            }
            else
            {
                Data data = new Data()
                {
                    Lamp_Level = "low"
                };
                var setter = client.Update("LampLevel/" + "/", data);
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Data data = new Data()
            {
                Lamp_Level = "run"
            };
            var setter = client.Update("LampLevel/" + "/", data);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Data data = new Data()
            {
                Lamp_Level = "stop"
            };
            var setter = client.Update("LampLevel/" + "/", data);
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            Data data = new Data()
            {
                Camera = "take"
            };
            var setter = client.Update("Camera/" + "/", data);
            Thread.Sleep(4000);

            DialogResult rslt;
            rslt = MessageBox.Show("Fotoðraf e-mail ile bildirilsin mi?", "Bildiri", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (rslt == DialogResult.Yes)
            {
                FirebaseResponse img = await client.GetAsync("");
                Data result = img.ResultAs<Data>();             //Deneme


                MailMessage mymessage = new MailMessage();
                SmtpClient smtp = new SmtpClient(); //istemci oluþturuldu

                smtp.Credentials = new System.Net.NetworkCredential("zeynepuzunsoyxzu@gmail.com", "chdsksqfmxfhyfsm");
                smtp.Port = 587;  //465 dene
                smtp.Host = "smtp.gmail.com";    //host sunucu olarak düþünülebilir. hotmailin sunucu aðýný (live) kullandýk.  
                smtp.EnableSsl = true;  //sunucu ile istemci arasýndaki verileri doðru adrese gönderene kadar þifreleme yapar
                                        // smtp.UseDefaultCredentials = true;
                mymessage.To.Add("zeynepuzunsoyy@gmail.com");  //mesajýmý bunu ekleme anlamý taþýr.
                mymessage.From = new MailAddress("zeynepuzunsoyxzu@gmail.com");
                mymessage.Subject = "Güvenlik Uyarýsý";
                mymessage.Body = "HIRSIZ VAR " + result.picture_url;

                smtp.Send(mymessage);
            }
        }

        private async void label9_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("");
            Data result = response.ResultAs<Data>();
            label9.Text = result.Camera;

        }

        private async void pictureBox2_Click_1(object sender, EventArgs e)
        {


        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load_2(object sender, EventArgs e)
        {

        }

        private async void button11_Click(object sender, EventArgs e)
        {
            //   FirebaseResponse response = await client.GetAsync("Image/");
            //   Data image = response.ResultAs<Data>();
            //
            //    byte[] b = Convert.FromBase64String(image.picture_url);
            //    MemoryStream ms = new MemoryStream();
            //    ms.Write(b, 0, Convert.ToInt32(b.Length));
            //    Bitmap bm = new Bitmap(ms, false);
            //    ms.Dispose();
            //    pictureBox2.Image = bm;
        }

        private void panel3_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private async void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FirebaseResponse img = await client.GetAsync("");
            Data result = img.ResultAs<Data>();
            string url =result.picture_url;
            System.Diagnostics.Process.Start(url);

        }

       
        public string rf = null;
        public async void rfid2()
        {
          //  Control.CheckForIllegalCrossThreadCalls = false;

            FirebaseResponse rp = await client.GetAsync("");
            Data result = rp.ResultAs<Data>();

            if (rf != result.RFID_Control)
            {
                if (result.RFID_Control == "zuzu")
                {
                    textBox3.Text = "Zeynep Uzunsoy kullanýcýsý giriþ yapmak istiyor..";

                }
                else if (result.RFID_Control == "elif")
                {
                    textBox3.Text = "Elif Ay kullanýcýsý giriþ yapmak istiyor..";
                }
                else if (result.RFID_Control == "ali")
                {
                    textBox3.Text = "Ali Deniz kullanýsýcý giriþ yapmak istiyor..";
                }
                else if (result.RFID_Control != null)
                {
                    textBox3.Text = "Kayýtlý olmayan biri giriþ yapmak istiyor..";
                }
                else
                {
                    textBox3.Text = " ";
                }
                rf = result.RFID_Control;
            }

        }
        private async void textBox3_Text(object sender, EventArgs e)
        {
            Console.WriteLine("asdasdasd");
            FirebaseResponse rp = await client.GetAsync("");
            Data result = rp.ResultAs<Data>();
            if (result.RFID_Control == "zuzu")
            {
                textBox3.Text = "Zeynep Uzunsoy kullanýcýsý giriþ yapmak istiyor..";

            }
            else if (result.RFID_Control == "elif")
            {
                textBox3.Text = "Elif Ay kullanýcýsý giriþ yapmak istiyor..";
            }
            else if (result.RFID_Control == "ali")
            {
                textBox3.Text = "Ali Deniz kullanýsýcý giriþ yapmak istiyor..";
            }
            else if (result.RFID_Control != null)
            {
                textBox3.Text = "Kayýtlý olmayan biri giriþ yapmak istiyor..";
            }
            else
            {
                textBox3.Text = " ";
            }
           


        }

        private async void textBox4_TextChanged(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("");
            Data result = response.ResultAs<Data>();
            if (result.Current_State == "1")
            {
              //  textBox4.Text = "Hareket algýlandý!!";
            }

        }

        public async void hareketalgýlayýcý(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("");
            Data result = response.ResultAs<Data>();
            if (result.Current_State == "1")
            {
             //   textBox4.Text = "Hareket algýlandý!!";
            }
        }

        private async void label10_Click(object sender, EventArgs e)
        {
            FirebaseResponse response = await client.GetAsync("");
            Data result = response.ResultAs<Data>();
            if (result.Current_State == "1")
            {
                textBox4.Text = "Hareket algýlandý!!";
            }
            else
            {
                textBox4.Text = "Hareket yok!!";
            }
        }

        private async void label11_Click(object sender, EventArgs e)
        {
            FirebaseResponse rp = await client.GetAsync("");
            Data result = rp.ResultAs<Data>();
            if (result.RFID_Control == "zuzu")
            {
                label11.Text = "Zeynep Uzunsoy kullanýcýsý giriþ yapmak istiyor..";

            }
            else if (result.RFID_Control == "elif")
            {
                label11.Text = "Elif Ay kullanýcýsý giriþ yapmak istiyor..";
            }
            else if (result.RFID_Control == "ali")
            {
                label11.Text = "Ali Deniz kullanýsýcý giriþ yapmak istiyor..";
            }
            else if (result.RFID_Control != null)
            {
                label11.Text = "Kayýtlý olmayan biri giriþ yapmak istiyor..";
            }
            else
            {
                label11.Text = " ";
            }

        }

        private async void label11_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("asdasdasd");
            FirebaseResponse rp = await client.GetAsync("");
            Data result = rp.ResultAs<Data>();
            if (result.RFID_Control == "zuzu")
            {
                textBox3.Text = "Zeynep Uzunsoy kullanýcýsý giriþ yapmak istiyor..";

            }
            else if (result.RFID_Control == "elif")
            {
                textBox3.Text = "Elif Ay kullanýcýsý giriþ yapmak istiyor..";
            }
            else if (result.RFID_Control == "ali")
            {
                textBox3.Text = "Ali Deniz kullanýsýcý giriþ yapmak istiyor..";
            }
            else if (result.RFID_Control != null)
            {
                textBox3.Text = "Kayýtlý olmayan biri giriþ yapmak istiyor..";
            }
            else
            {
                textBox3.Text = " ";
            }

        }

        
    }
}
