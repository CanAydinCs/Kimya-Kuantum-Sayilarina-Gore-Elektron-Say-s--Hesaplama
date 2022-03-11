using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kuantum_Sayılarına_Göre_Katman_Sayısını_Bulma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Programlayan Mehmet Can AYDIN

        void Combobox1Values(int kirma)
        {
            //daha kısa kodla halletmek için döngü kullanmayı tercih ettim.
            comboBox1.Items.Clear();
            comboBox1.Text = "";

            while (true)
            {
                comboBox1.Items.Add("s");
                if (kirma == 0) break;

                comboBox1.Items.Add("p");
                if (kirma == 1) break;

                comboBox1.Items.Add("d");
                if (kirma == 2) break;

                comboBox1.Items.Add("f");
                break;
            }
        }

        void ComboBox2Values(int kirma)
        {
            //daha kısa kodla halletmek için döngü kullanmayı tercih ettim.
            comboBox2.Items.Clear();
            comboBox2.Text = "";

            for (int i = 0; i < kirma; i++)
            {
                comboBox2.Items.Add(i);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.Enabled = false;
            radioButton1.Checked = true;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // Doluluk durumuna göre son orbitaldeki elektron sayısı ayarlanıyor.
            comboBox2.Enabled = radioButton2.Checked;

            // Açısal Momentum Kuantum Sayısına Göre Orbitalde olabilecek elektron sayısını ekleme

            comboBox2.Items.Clear();

            switch (comboBox1.Text)
            {
                case "s":
                    ComboBox2Values(2); break;
                case "p":
                    ComboBox2Values(6); break;
                case "d":
                    ComboBox2Values(10); break;
                case "f":
                    ComboBox2Values(14); break;
                default:
                    ComboBox2Values(0); break;
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            //Baş Kuantum Sayısına Göre İkincil Kuantum Sayısını Ayarlamamızı Sağlayan Kutucuğu Ayarlama

            switch (numericUpDown1.Value)
            {
                case 1:
                    Combobox1Values(0); break;
                case 2:
                    Combobox1Values(1); break;
                case 3:
                    Combobox1Values(2); break;
                default:
                    Combobox1Values(3); break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "")
            {
                MessageBox.Show("Açısal Momentum Kuantum Sayısının Değeri Boş Bırakılmamalıdır!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if(radioButton2.Checked && comboBox2.Text == "")
            {
                MessageBox.Show("Son Katmanda Kaç Elektron Olduğunun Bilgisi Boş Bırakılmamalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                /*buradan irtibaren hesaplamalar başladığı için
                 * proje raporunda formüllere verilen değerler kullanılmıştır */

                int lverilen, n = Convert.ToInt32(numericUpDown1.Value);
                int sonuc = 0;

                switch (comboBox1.Text)
                {
                    case "s":
                        lverilen = 0; break;
                    case "p":
                        lverilen = 1; break;
                    case "d":
                        lverilen = 2; break;
                    default:
                        lverilen = 3; break;
                }

                for (int listenen = 0; listenen < 5; listenen++)
                {
                    int ofazla = lverilen - listenen;

                    int orbital = n - listenen + ofazla;

                    if (ofazla > 0) orbital--; //"--"   -1'i ifade ediyor.
                    if (orbital <= 0) break;

                    switch (listenen)
                    {
                        case 0:
                            sonuc += orbital * 2; break;
                        case 1:
                            sonuc += orbital * 6; break;
                        case 2:
                            sonuc += orbital * 10; break;
                        default:
                            sonuc += orbital * 14; break;
                    }
                }

                //son katmandaki eksik elektrona göre kontrol etme
                if (radioButton2.Checked)
                {
                    int son_katman = Convert.ToInt32(comboBox2.Text);
                    int normalde_olan;

                    switch (comboBox1.Text)
                    {
                        case "s":
                            normalde_olan = 2; break;
                        case "p":
                            normalde_olan = 6; break;
                        case "d":
                            normalde_olan = 10; break;
                        default:
                            normalde_olan = 14; break;
                    }

                    sonuc -= normalde_olan - son_katman;
                }

                //sonucu son label'a yazdırmak.
                label5.Text = "Toplam Elektron Sayısı: " + sonuc;
            }
        }
    }
}
