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

namespace Kontrolni_tabela
{
    public partial class Form1 : Form
    {
        OpenFileDialog opd = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnIzbor_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Text = "Domacin";
            comboBox2.Text = "Gost";
            StreamReader sr;
            string stavka, ime;
            int broj = 0;
            int pom = 0;
            string[] ime_kluba = new string[10];
            opd.Filter = "Tekst datoteke(.txt) | *.txt";
            opd.FileName = "Tabela";
            opd.RestoreDirectory = true;
            opd.Title = "Otvori datoteku";
            opd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (opd.ShowDialog() == DialogResult.OK)
            {
                sr = new StreamReader(opd.FileName);
                while ((stavka = sr.ReadLine()) != null)
                {
                    broj++;
                }
                sr.Dispose();
                sr = new StreamReader(opd.FileName);
                while ((stavka = sr.ReadLine()) != null)
                {
                    ime_kluba = stavka.Split('/');
                    ime = ime_kluba[0];
                    pom++;
                    if (pom <= broj/2)
                    {
                        comboBox1.Items.Add(ime);
                    }
                    else
                    {
                        comboBox2.Items.Add(ime);
                    }
                }
                sr.Dispose();
                btnPotvrdi.Enabled = btnOdustani.Enabled = true;
            }
        }

        private void btnPotvrdi_Click(object sender, EventArgs e)
        {
            DateTime d1 = new DateTime(2017, 12, 15);
            DateTime d2 = dateTimePicker1.Value;
            if (d2.DayOfWeek != DayOfWeek.Wednesday && d2.DayOfWeek != DayOfWeek.Saturday)
            {
                MessageBox.Show("Utakmice se mogu igrati samo sredom i subotom");
            }
            else if (d2>d1)
            {
                MessageBox.Show("Utakmice se mogu igrati pre 15.12.2017.");
            }
            else
            {
                string klubDomacin;
                string klubGost;
                string stavka;
                string[] ime_kluba = new string[10];
                string ime;
                int odig;
                int datih;
                int primljenih;
                int bodovi;
                if(textBox1.Text == "")
                {
                    textBox1.Text = "0";
                }
                if (textBox2.Text == "")
                {
                    textBox2.Text = "0";
                }
                int golDomacin = Convert.ToInt16(textBox1.Text);
                int golGost = Convert.ToInt16(textBox3.Text);
                StreamReader sr;
                sr = new StreamReader(opd.FileName);
                StreamWriter sw = new StreamWriter(opd.FileName.Remove(opd.FileName.Length - 4, 4) + "Novi.txt", true);
                while ((stavka = sr.ReadLine()) != null)
                {
                    ime_kluba = stavka.Split('/');
                    ime = ime_kluba[0];
                    if ((string)comboBox1.SelectedItem == ime)
                    {
                        odig = Convert.ToInt16(ime_kluba[1].Trim()) + 1;
                        datih = Convert.ToInt16(ime_kluba[2].Trim()) + golDomacin;
                        primljenih = Convert.ToInt16(ime_kluba[3].Trim()) + golGost;
                        bodovi = Convert.ToInt16(ime_kluba[4].Trim());
                        if (golDomacin > golGost)
                        {
                            bodovi = bodovi + 3;
                        }
                        else if (golDomacin == golGost)
                        {
                            bodovi = bodovi + 1;
                        }
                        else
                        {
                            bodovi = bodovi + 0;
                        }
                        klubDomacin = ime + "_" + odig + "_" + datih + "_" + primljenih + "_" + bodovi + "_" + dateTimePicker1.Value.ToShortDateString();
                        sw.WriteLine(klubDomacin);
                        comboBox1.Items.Remove(comboBox1.SelectedItem);
                        comboBox1.Text = "Domacin";
                    }
                    if((string)comboBox2.SelectedItem == ime)
                    {
                        odig = Convert.ToInt16(ime_kluba[1].Trim()) + 1;
                        datih = Convert.ToInt16(ime_kluba[2].Trim()) + golGost;
                        primljenih = Convert.ToInt16(ime_kluba[3].Trim()) + golDomacin;
                        bodovi = Convert.ToInt16(ime_kluba[4].Trim());
                        if (golGost > golDomacin)
                        {
                            bodovi = bodovi + 3;
                        }
                        else if (golGost == golDomacin)
                        {
                            bodovi = bodovi + 1;
                        }
                        else
                        {
                            bodovi = bodovi + 0;
                        }
                        klubGost = ime + "_" + odig + "_" + datih + "_" + primljenih + "_" + bodovi + "_" + dateTimePicker1.Value.ToShortDateString();
                        sw.WriteLine(klubGost);
                        comboBox2.Items.Remove(comboBox2.SelectedItem);
                        comboBox2.Text = "Gost";
                    }
                }
                sr.Dispose();
                sw.Dispose();
            }
        }

        private void btnOtvori_Click(object sender, EventArgs e)
        {
            opd.Filter = "Tekst datoteke(.txt) | *.txt";
            opd.FileName = "TabelaNovi";
            opd.RestoreDirectory = true;
            opd.Title = "Otvori datoteku";
            opd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string d, ime, odigranih, datih, primljenih, bodova, datum;
            string[] klub_stavka = new string[10];
            if (opd.ShowDialog() == DialogResult.OK)
            {

                StreamReader sr2 = new StreamReader(opd.FileName);
                while ((d = sr2.ReadLine()) != null)
                {
                    klub_stavka = d.Split('_');

                    ime = klub_stavka[0];
                    odigranih = klub_stavka[1].Trim();
                    datih = klub_stavka[2].Trim();
                    primljenih = klub_stavka[3].Trim();
                    bodova = klub_stavka[4].Trim();
                    datum = klub_stavka[5].Trim();

                    dataGridView1.Rows.Add(ime, odigranih, datih, primljenih, bodova, datum);
                }
                sr2.Dispose();
            }
        }
    }
}
