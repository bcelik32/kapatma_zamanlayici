using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Runtime.InteropServices;

namespace kapatma_zamanlayici
{
    public partial class Form1 : Form
    {
        private Form2 form2;
        private DateTime targetDateTime;

        private Timer timer1;
        private Timer timer2;
        private Timer timer3;
        private string kapatmamod="kapatma";
        [DllImport("Powrprof.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);
        private bool son10DakikaUyariGosterildi = false;


        private int toplamSaniye;
        private int saatal;
        private int dakikaal;
        private int saniyeal;
        private int kapatmaformul;
        private int kalanSaniye;
        bool kapatma = false;
        public Form1()
        {
            InitializeComponent();
            form2 = new Form2();

            timer1 = new Timer();

            timer1.Tick += Timer1_Tick;

            timer1.Interval = 1000; 
            //-----//
            timer2 = new Timer();

            timer2.Tick += Timer2_Tick;

            timer2.Interval = 1000;
            //----//

            timer3 = new Timer();

            timer3.Tick += Timer3_Tick;

            timer3.Interval = 1000;
   
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan timeDifference = targetDateTime - DateTime.Now;

            // Form üzerinde güncelleme yapmak için Invoke kullanılıyor
         
            //MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Kapatılacak.", "Uyarı");


            if (timeDifference.TotalSeconds > 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label7.Text = $"{timeDifference.Hours:00}:{timeDifference.Minutes:00}:{timeDifference.Seconds:00}";
                });
                if (timeDifference.TotalSeconds <= 600 && timeDifference.TotalSeconds > 0 && !son10DakikaUyariGosterildi)
                {
                   
                        son10DakikaUyariGosterildi = true; // Bayrağı true olarak ayarla, böylece bir daha gösterilmez
                    switch (kapatmamod)
                    {
                        case "yb":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Yeniden Başlatılacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "ok":
                            MessageBox.Show("10 Dakika Sonra Oturumunuz Kapatılcak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "hb":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Hazırda Bekletme Moduna Alınacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "u":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Uyku Moduna Alınacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "kapat":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Kapatılacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                            // Diğer durumları ekleyebilirsiniz.
                    }
                }
            }
            else
            {
                timer1.Stop();
                label7.Text = "Bye";
                label3.Text = @"";
                switch (kapatmamod)
                {
                    case "yb":
                        Process.Start("shutdown", "/r /t 0");
                        break;
                    case "ok":
                        Process.Start("shutdown", "/l /f"); // Oturumu kapatma komutu
                        break;
                    case "hb":
                        BekletmeModunaAl();
                        break;
                    case "u":
                        Application.SetSuspendState(PowerState.Suspend, true, true);
                        break;
                    case "kapat":
                        System.Diagnostics.Process.Start("shutdown", "/s /t 0");
                        break;
                        // Diğer durumları ekleyebilirsiniz.

                }
            }
           


        }
        private void Timer2_Tick(object sender, EventArgs e)
        {


            kapatmaformul--;
            label7.Text = kapatmaformul.ToString();
            if (kalanSaniye > 0)
            {
                kalanSaniye--;

                TimeSpan sure = TimeSpan.FromSeconds(kalanSaniye);
                if (kalanSaniye <= 600 && kalanSaniye> 0 && !son10DakikaUyariGosterildi)
                {
                    son10DakikaUyariGosterildi = true; // Bayrağı true olarak ayarla, böylece bir daha gösterilmez
                    switch (kapatmamod)
                    {
                        case "yb":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Yeniden Başlatılacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "ok":
                            MessageBox.Show("10 Dakika Sonra Oturumunuz Kapatılcak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "hb":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Hazırda Bekletme Moduna Alınacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "u":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Uyku Moduna Alınacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "kapat":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Kapatılacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                            // Diğer durumları ekleyebilirsiniz.
                    }
                }
                // Form üzerinde güncelleme yapmak için Invoke kullanılıyor
                this.Invoke((MethodInvoker)delegate
                {
                    label7.Text = $"{sure.Hours}:{sure.Minutes}:{sure.Seconds}";
                });
            }
            else
            {
                timer2.Stop(); // Geri sayım bittiğinde timer durduruluyor
                label7.Text = "Bye";
                label3.Text = @"";

                switch (kapatmamod)
                {
                    case "yb":
                        Process.Start("shutdown", "/r /t 0");
                        break;
                    case "ok":
                        Process.Start("shutdown", "/l /f"); // Oturumu kapatma komutu
                        break;
                    case "hb":
                        BekletmeModunaAl();
                        break;
                    case "u":
                        Application.SetSuspendState(PowerState.Suspend, true, true);
                        break;
                    case "kapat":
                        System.Diagnostics.Process.Start("shutdown", "/s /t 0");
                        break;
                        // Diğer durumları ekleyebilirsiniz.

                }
            }
        }
        private void Timer3_Tick(object sender, EventArgs e)
        {
            if (toplamSaniye > 0)
            {
                // Her saniye bir azalt
                toplamSaniye--;

                // TimeSpan nesnesini oluştur
                TimeSpan sure = TimeSpan.FromSeconds(toplamSaniye);

                // TimeSpan'i parçalara ayır
                int saat = sure.Hours;
                int dakika = sure.Minutes;
                int saniye = sure.Seconds;
                if (toplamSaniye <= 600 && toplamSaniye> 0 && !son10DakikaUyariGosterildi)
                {
                    son10DakikaUyariGosterildi = true; // Bayrağı true olarak ayarla, böylece bir daha gösterilmez

                    switch (kapatmamod)
                    {
                        case "yb":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Yeniden Başlatılacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "ok":
                            MessageBox.Show("10 Dakika Sonra Oturumunuz Kapatılcak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "hb":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Hazırda Bekletme Moduna Alınacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "u":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Uyku Moduna Alınacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                        case "kapat":
                            MessageBox.Show("Kişisel Bilgisayarınız 10 Dakika Sonra Kapatılacak.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                            break;
                            // Diğer durumları ekleyebilirsiniz.
                    }
                }
                // Sonucu label'a yazdır
                label7.Text = $"{saat}:{dakika}:{saniye}";
            }
            else
            {
                timer3.Stop(); // Zaman dolduğunda timer'ı durdur
                label7.Text = "Bye";
                switch (kapatmamod)
                {
                    case "yb":
                        Process.Start("shutdown", "/r /t 0");
                        break;
                    case "ok":
                        Process.Start("shutdown", "/l /f"); // Oturumu kapatma komutu
                        break;
                    case "hb":
                        BekletmeModunaAl();
                        break;
                    case "u":
                        Application.SetSuspendState(PowerState.Suspend, true, true);
                        break;
                    case "kapat":
                        System.Diagnostics.Process.Start("shutdown", "/s /t 0");
                        break;
                        // Diğer durumları ekleyebilirsiniz.

                }
            }
        }
        static void BekletmeModunaAl()
        {
            // Standby süresini sıfıra ayarla
            ExecuteCommand("powercfg", "-change -standby-timeout-ac 0");

            // Monitör kapanma süresini sıfıra ayarla
            ExecuteCommand("powercfg", "-change -monitor-timeout-ac 0");

            // Disk kapanma süresini sıfıra ayarla
            ExecuteCommand("powercfg", "-change -disk-timeout-ac 0");

            // Hibernate özelliğini kapat
            ExecuteCommand("powercfg", "-hibernate off");

            // Bilgisayarı hazırda bekletme moduna al
            ExecuteCommand("rundll32.exe", "powrprof.dll,SetSuspendState 0,1,0");
        }

        static void ExecuteCommand(string command, string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo(command, arguments);
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;

            using (Process process = new Process())
            {
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "HH:mm:ss";
        }


        private void basla_Click(object sender, EventArgs e)
        {

            if (kapatma == false)
            {
                saatal = (int)numericUpDown5.Value * 3600;
                dakikaal = (int)numericUpDown4.Value * 60;
                saniyeal = (int)numericUpDown2.Value;
                kapatmaformul = saatal + dakikaal + saniyeal;
                kalanSaniye = kapatmaformul;
                if (saatal > 0 || dakikaal > 0 || saniyeal > 0)
                {
                    timer2.Start();
                    kapatma = true;
                    label3.Text = @"";
                    durdur.Enabled = true;
                    basla.Enabled = false;
                    button1.Enabled = false;
                    onbes.Enabled = false;
                    otuzd.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    label3.Text = "Zamanlayıcı Ayarlandı.";
                }
                else
                {
                    label3.Text = @"Lütfen Bir Zaman Giriniz";
                }

            }
            else
            {
                label3.Text = @"Zaten Bekleyen Bir Kapatma İşlemi Var";
            }

        }

        private void durdur_Click(object sender, EventArgs e)
        {
            if (kapatma == true)
            {
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();

                label7.Text = "";
                kapatma = false;
                label3.Text = @"";
                label3.Text = @"Kapatma Zamanlayıcı 
İptal Edildi";
                basla.Enabled = true;
                button1.Enabled = true;
                durdur.Enabled = false;
                onbes.Enabled = true;
                otuzd.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                son10DakikaUyariGosterildi = false; // Bayrağı true olarak ayarla, böylece bir daha gösterilmez

            }
            else
            {
                label3.Text =@"Zaten Bir Kapatma İşlemi Yok";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (kapatma == false)
            {
                // DateTimePicker'dan hedef tarih ve saat bilgisini al
                targetDateTime = dateTimePicker1.Value;

                // Hedef tarih ve saat ile şu anki zaman arasındaki farkı hesapla
                TimeSpan timeDifference = targetDateTime - DateTime.Now;

                // Zaman sıfırlandığında timer'ı durdur
                if (timeDifference.TotalSeconds > 0)
                {
                    // Form üzerinde güncelleme yapmak için Invoke kullanılıyor
                    this.Invoke((MethodInvoker)delegate
                    {
                        label7.Text = $"{timeDifference.Hours:00}:{timeDifference.Minutes:00}:{timeDifference.Seconds:00}";
                    });

                    // Timer'ı başlat
                    timer1.Start();
                    label3.Text = "Zamanlayıcı Ayarlandı.";

                }
                else
                {
                    MessageBox.Show("Geçerli bir gelecek tarih ve saat seçin.", "Uyarı");
                }
                timer1.Start();
                kapatma = true;
                label3.Text = @"";
                durdur.Enabled = true;
                button1.Enabled = false;
                basla.Enabled = false;
                onbes.Enabled = false;
                otuzd.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;

            }
            else
            {
                label3.Text = "Zaten Varsayılan Bir Kapatma İşlemi Var";
            }
        }

        private void onbes_Click(object sender, EventArgs e)
        {
            if (kapatma == false)
            {
                toplamSaniye = 15 * 60;
                timer3.Start();
                kapatma = true;
                label3.Text = @"";
                durdur.Enabled = true;
                button1.Enabled = false;
                basla.Enabled = false;
                onbes.Enabled = false;
                otuzd.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                label3.Text = "Zamanlayıcı Ayarlandı.";

            }
            else
            {
                label3.Text = "Zaten Varsayılan Bir Kapatma İşlemi Var";
            }
        }

        private void otuzd_Click(object sender, EventArgs e)
        {
            if (kapatma == false)
            {
                toplamSaniye = 30* 60;
                timer3.Start();
                kapatma = true;
                label3.Text = @"";
                durdur.Enabled = true;
                button1.Enabled = false;
                basla.Enabled = false;
                onbes.Enabled = false;
                otuzd.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                label3.Text = "Zamanlayıcı Ayarlandı.";

            }
            else
            {
                label3.Text = "Zaten Varsayılan Bir Kapatma İşlemi Var";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (kapatma == false)
            {
                toplamSaniye = 45* 60;
                timer3.Start();
                kapatma = true;
                label3.Text = @"";
                durdur.Enabled = true;
                button1.Enabled = false;
                basla.Enabled = false;
                onbes.Enabled = false;
                otuzd.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                label3.Text = "Zamanlayıcı Ayarlandı.";

            }
            else
            {
                label3.Text = "Zaten Varsayılan Bir Kapatma İşlemi Var";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (kapatma == false)
            {
                toplamSaniye = 60* 60;
                timer3.Start();
                kapatma = true;
                label3.Text = @"";
                durdur.Enabled = true;
                button1.Enabled = false;
                onbes.Enabled = false;
                otuzd.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                basla.Enabled = false;
                label3.Text = "Zamanlayıcı Ayarlandı.";

            }
            else
            {
                label3.Text = "Zaten Varsayılan Bir Kapatma İşlemi Var";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (kapatma == false)
            {
                DialogResult result = MessageBox.Show("Çıkış Yapmak İstiyor musunuz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Çıkış işlemini iptal et
                }
            }
            else
            {
                DialogResult result = ShowCustomMessageBox("Zamanlayıcıyı Durdurmak İstiyormusunuz?", "Çıkış");
                if (result == DialogResult.Yes)
                {
                    // Kapat seçeneği seçildiyse formu kapat
                    e.Cancel = false;
                }
                else if (result == DialogResult.No)
                {
                    // Arkaya al seçeneği seçildiyse formu gizle

                    // Form kapatılmaya çalışıldığında sadece gizle, kapatma işlemini iptal et.
                    e.Cancel = true;
                    this.Hide(); // Form1'i gizle
                    form2.ShowDialog(); // Form2'yi göster (modal olarak)
                    this.Show();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
        private DialogResult ShowCustomMessageBox(string message, string caption)
        {
            Form messageBoxForm = new Form();
            Label label = new Label();
            Button yesButton = new Button();
            Button noButton = new Button();
            Button iptal = new Button();

            messageBoxForm.Text = caption;
            label.Text = message;
            yesButton.Text = "Durdur";
            noButton.Text = "Arkada Çalış";
            iptal.Text = "İptal";

            yesButton.DialogResult = DialogResult.Yes;
            noButton.DialogResult = DialogResult.No;
            iptal.DialogResult = DialogResult.Cancel;

            yesButton.BackColor = Color.White;
            noButton.BackColor = Color.White;
            iptal.BackColor = Color.White;

            messageBoxForm.StartPosition = FormStartPosition.CenterScreen;
            label.Dock = DockStyle.Top;
            yesButton.Dock = DockStyle.Left;
            noButton.Dock = DockStyle.Right;
            iptal.Dock = DockStyle.Fill;

            // Buton boyutlarını ayarla

            messageBoxForm.Width =225;
            messageBoxForm.Height =50;
            messageBoxForm.BackColor = Color.DarkGray;
            messageBoxForm.FormBorderStyle= FormBorderStyle.None;
            yesButton.Click += (sender, e) => { messageBoxForm.DialogResult = DialogResult.Yes; messageBoxForm.Close(); };
            noButton.Click += (sender, e) => { messageBoxForm.DialogResult = DialogResult.No; messageBoxForm.Close(); };
            iptal.Click += (sender, e) => { messageBoxForm.DialogResult = DialogResult.Cancel; messageBoxForm.Close(); };

            messageBoxForm.Controls.Add(noButton);
            messageBoxForm.Controls.Add(yesButton);
            messageBoxForm.Controls.Add(iptal);

            messageBoxForm.Controls.Add(label);

            return messageBoxForm.ShowDialog();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            kapatmamod = "yb";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            kapatmamod = "kapat";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            kapatmamod = "ok";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            kapatmamod = "hb";
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            kapatmamod = "u";
        }
    }
}
