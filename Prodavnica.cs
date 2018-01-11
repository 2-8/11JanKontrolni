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
using System.Windows.Forms.DataVisualization.Charting;


namespace VezbaProdavnica9jan
{
    public partial class Prodavnica : Form
    {
        public Prodavnica()
        {
            InitializeComponent();
        }
        OpenFileDialog opd = new OpenFileDialog();
        Random r = new Random();
        List<string> lista = new List<string>(50);
        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAzuriraj_Click(object sender, EventArgs e)
        {
            StreamReader sr;
            string stavka, roba, kod;
            string[] roba_kod = new string[20];
            opd.Filter = "Tekst datoteke(.txt) | *.txt";
            opd.RestoreDirectory = true;
            opd.Title = "Otvori datoteku";
            opd.FileName = "Artikli.txt";
            opd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if(opd.ShowDialog() == DialogResult.OK)
            {
                sr = new StreamReader(opd.FileName);
                while((stavka=sr.ReadLine())!=null)
                {
                    roba_kod = stavka.Split('#');
                    roba = roba_kod[0];
                    kod = roba_kod[1];
                    kod = kod.TrimStart();
                    cbKod.Items.Add(kod);
                    cbRoba.Items.Add(roba);
                    lista.Add(stavka);
                }
                sr.Dispose();
            }
            btnOdustani.Enabled = btnPotvrdi.Enabled = true;

        }

        private void btnPotvrdi_Click(object sender, EventArgs e)
        {
            DateTime dat = new DateTime();
            DateTime sad = new DateTime();
            sad = DateTime.Now;
            dat = dateTimePicker1.Value;
            if((sad-dat).Days>7)
            {
                 MessageBox.Show("Datum unosa ne moze biti stariji od 7 dana!");
            }
            else if((sad-dat).Days<0)
            {
                MessageBox.Show("Datum unosa ne moze biti posle danasnjeg datuma!");
            }
            else
            {
                string artikal = tbNazRobe.Text + "," + dat.ToShortDateString() + "," + tbCena.Text + "," + tbBarKod.Text;
                StreamWriter sw = new StreamWriter(opd.FileName.Remove(opd.FileName.Length - 4, 4) + "Novi.txt", true);
                sw.WriteLine(artikal);
                sw.Dispose();
                tbBarKod.Text = tbCena.Text = tbNazRobe.Text = "";
                cbKod.Text = "Spisak bar - kodova";
                cbRoba.Text = "Spisak robe";
                
            }
            

        }

        private void cbRoba_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbRoba.SelectedIndex;
            tbNazRobe.Text = cbRoba.SelectedItem.ToString();
            tbBarKod.Text = cbKod.Items[i].ToString();
            string[] s = lista[i].Split('#');
            tbCena.Text = s[2];
        }

        private void tbBarKod_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void tbBarKod_MouseClick(object sender, MouseEventArgs e)
        {
            int k = r.Next(10000000, 99999999);
            tbBarKod.Text = Convert.ToString(k);
        }

        private void btnOtvori_Click(object sender, EventArgs e)
        {
            string stavka, naziv, datum, cena, kod;
            string[] s;
            if(opd.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(opd.FileName);
                while ((stavka = sr.ReadLine())!=null)
                {
                    s = stavka.Split(',');
                    naziv = s[0];
                    datum = s[1];
                    cena = s[2];
                    kod = s[3];
                    dataGridView1.Rows.Add(naziv, datum, cena, kod);
                }
                chart1.Titles.Add("Artikli - cene");
                foreach(DataGridViewRow data in dataGridView1.Rows)
                {
                    Series ser = chart1.Series.Add(Convert.ToString(data.Cells[0].Value));
                    ser.Points.Add(Convert.ToDouble(data.Cells[2].Value));
                }
            }
            
        }
        private void dijagram()
        {
            
        }
        private void btnOdustani_Click(object sender, EventArgs e)
        {
            tbNazRobe.Text = tbCena.Text = tbBarKod.Text = "";
            cbKod.Items.Clear();
            cbRoba.Items.Clear();
            cbRoba.Text = "Spisak robe";
            cbKod.Text = "Spisak bar - kodova";
        }
    }
}
