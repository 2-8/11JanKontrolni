using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        OpenFileDialog opd = new OpenFileDialog();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAzuriraj_Click(object sender, EventArgs e)
        {
            StreamReader sr;
            string stavka, prezime, opstina;
            string[] prezime_opstina=new string[10];
            opd.Filter = "Tekst datoteke(.txt) | *.txt";
            opd.RestoreDirectory=true;
            opd.Title = "Otvori datoteku";
            opd.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            opd.FileName = "Prijave.txt";
            if(opd.ShowDialog()==DialogResult.OK)
            {
                sr = new StreamReader(opd.FileName);
                while((stavka=sr.ReadLine())!=null)
                {
                    prezime_opstina = stavka.Split(',');
                    prezime = prezime_opstina[0];
                    opstina = prezime_opstina[1];
                    opstina = opstina.TrimStart();
                    comboBox1.Items.Add(prezime);
                    comboBox2.Items.Add(opstina);

                }
                sr.Dispose();
            }
            btnPotvrdi.Enabled = btnOdustani.Enabled = true;
            
        }

        private void btnPotvrdi_Click(object sender, EventArgs e)
        {
            TimeSpan ts = new TimeSpan(2190,0,0,0,0);
            DateTime dtm = DateTime.Today;
            if (DateTime.Compare(dtm, dateTimePicker1.Value) < 0)
                MessageBox.Show("Detetove godine ne mogu biti negativne. Unesite ispravna datum");
            else
            {
                if (dtm.Subtract(dateTimePicker1.Value) > ts)
                    MessageBox.Show("Dete ne moze biti starije od 6 godina");
                else
                {
                    string dete;
                    dete = tbPrezIme.Text + ", " + tbOpstina.Text + ", " + dateTimePicker1.Value.ToShortDateString();
                    StreamWriter sw = new StreamWriter(opd.FileName.Remove(opd.FileName.Length - 4, 4) + "Novi.txt", true);
                    sw.WriteLine(dete);
                    sw.Dispose();
                    tbPrezIme.Text = tbOpstina.Text = tbGrupa.Text = "";
                    dateTimePicker1.Value = DateTime.Today;
                    comboBox1.Text = "Spisak dece"; comboBox2.Text = "Spisak opstina";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            tbPrezIme.Text = comboBox1.SelectedItem.ToString();
            tbOpstina.Text = comboBox2.Items[i].ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            tbGrupa.Text = dateTimePicker1.Value.Year.ToString();
        }

        private void btnOtvori_Click(object sender, EventArgs e)
        {

            string d,imePrezime,opstina,datum,grupa;
            string[] dete_stavka = new string[10];
            if(opd.ShowDialog()==DialogResult.OK)
            {
                DateTime dt;
                StreamReader sr = new StreamReader(opd.FileName);
                while((d=sr.ReadLine())!=null)
                {
                    dete_stavka = d.Split(',');
                    imePrezime = dete_stavka[0];
                    opstina = dete_stavka[1];
                    opstina = opstina.Trim();
                    datum = dete_stavka[2];
                    datum = datum.Trim();
                    dt = DateTime.Parse(datum);
                    grupa = dt.Year.ToString();
                    dataGridView1.Rows.Add(imePrezime, datum, opstina, grupa);

                }
                sr.Dispose();
            }
            generisanjeDijagrama();
        }

        protected void generisanjeDijagrama()
        {
            int a2012=0, a2013=0, a2014=0, a2015=0, a2016=0, a2017=0;
            chart1.Series.Clear();
            chart1.Text = "Spisak dece";
            chart1.Series.Add("2012");
            chart1.Series["2012"].Points.Clear();
            chart1.Series["2012"].ChartType = SeriesChartType.Column;
            chart1.Series.Add("2013");
            chart1.Series["2013"].Points.Clear();
            chart1.Series["2013"].ChartType = SeriesChartType.Column;
            chart1.Series.Add("2014");
            chart1.Series["2014"].Points.Clear();
            chart1.Series["2014"].ChartType = SeriesChartType.Column;
            chart1.Series.Add("2015");
            chart1.Series["2015"].Points.Clear();
            chart1.Series["2015"].ChartType = SeriesChartType.Column;
            chart1.Series.Add("2016");
            chart1.Series["2016"].Points.Clear();
            chart1.Series["2016"].ChartType = SeriesChartType.Column;
            chart1.Series.Add("2017");
            chart1.Series["2017"].Points.Clear();
            chart1.Series["2017"].ChartType = SeriesChartType.Column;
            for(int k=0;k<dataGridView1.Rows.Count-1;k++)
            {
                DataGridViewRow red = dataGridView1.Rows[k];
                if (red.Cells[3].Value.ToString() == "2017")
                    a2017++;
                else if (red.Cells[3].Value.ToString() == "2016")
                    a2016++;
                else if (red.Cells[3].Value.ToString() == "2015")
                    a2015++;
                else if (red.Cells[3].Value.ToString() == "2014")
                    a2014++;
                else if (red.Cells[3].Value.ToString() == "2013")
                    a2013++;
                else if (red.Cells[3].Value.ToString() == "2012")
                    a2012++;
            }
            chart1.Series["2012"].Points.Add(a2012);
            chart1.Series["2013"].Points.Add(a2013);
            chart1.Series["2014"].Points.Add(a2014);
            chart1.Series["2015"].Points.Add(a2015);
            chart1.Series["2016"].Points.Add(a2016);
            chart1.Series["2017"].Points.Add(a2017);


        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            tbGrupa.Text = tbOpstina.Text = tbPrezIme.Text = "";
            dateTimePicker1.Value = DateTime.Today;
            comboBox1.Items.Clear(); comboBox2.Items.Clear();
            comboBox1.Text = "Spisak dece"; comboBox2.Text = "Spisak opstina";
        }
    }
}
