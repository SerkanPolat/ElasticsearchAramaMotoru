using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajProje
{
	public class Kisi
	{
		public string Ad { get; set; }
		public string Soyad { get; set; }
		public string Bolum { get; set; }
		public string Sehir { get; set; }
		public string Numara { get; set; }
		public string Telefon { get; set; }

		public CompletionField suggest { get; set; }
	}
}
