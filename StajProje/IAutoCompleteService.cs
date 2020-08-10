using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StajProje
{
	public interface IAutoCompleteService
	{
		bool CreateIndexAsync(string IndexName);
		Task IndexAsync(string IndexName, List<Kisi> kisiler);
		KisiSuggestResponse SuggestAsync(string indexName, string Keyword,string AranacakKolon);
	}
}
