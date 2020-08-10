using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StajProje
{
	public partial class Form1 : Form
	{
		ConnectionSettings setting;
		IAutoCompleteService autoCompleteService;
		string indexName;
		string AramaKolonu;
		public Form1()
		{
			InitializeComponent();
			setting = new ConnectionSettings(new Uri("http://localhost:9200"));
			autoCompleteService = new AutoCompleteService(setting);
			indexName = "ogrenciarama";
			AramaKolonu = "AdSoyad";


			bool isCreated = autoCompleteService.CreateIndexAsync(indexName);

			if (isCreated)
			{

			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{



		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			Stopwatch watch = new Stopwatch();
			watch.Start();
			var s = autoCompleteService.SuggestAsync(indexName, textBox1.Text, AramaKolonu).suggests;

			watch.Stop();
			label3.Text = watch.Elapsed.TotalMilliseconds.ToString()+" ms";
			foreach (var item in s)
			{
				listBox1.Items.Add(item.Ad+" "+item.Soyad+" "+item.Telefon);
			}
		}
		private void button1_Click(object sender, EventArgs e)
		{
			

		}

	}
}
