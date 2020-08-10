using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StajProje
{
	public class AutoCompleteService : IAutoCompleteService
	{
		ElasticClient client;
		ISearchResponse<Kisi> searchResponse;
		public AutoCompleteService(ConnectionSettings settings)
		{
			client = new ElasticClient(settings);
		}
		public bool CreateIndexAsync(string IndexName)
		{
			var createIndex = client.Indices.CreateAsync(IndexName, index => index
			 .Map<Kisi>(m => m
			 .AutoMap().Properties(ps => ps
			 .Completion(c => c
			 .Name(p => p
			 .suggest).Analyzer("standard")))));
			return true;
		}

		public async Task IndexAsync(string IndexName, List<Kisi> kisiler)
		{
			await client.IndexManyAsync(kisiler, IndexName);
		}

		public KisiSuggestResponse SuggestAsync(string indexName, string Keyword,string AranacakKolon)
		{
			
					searchResponse = client.Search<Kisi>(s => s
					.Index(indexName)
					.Suggest(su => su
					.Completion("suggestions", c => c
					 .Field(f => f.suggest)
					 .Prefix(Keyword)
					 .Fuzzy(F => F
					 .Fuzziness(Fuzziness.Auto)).Size(50))).TrackTotalHits(true));
					
			var suggests = from suggest in searchResponse.Suggest["suggestions"]
						   from option in suggest.Options
						   select new KisiSuggest
						   {
							   Ad = option.Source.Ad,
							   Soyad = option.Source.Soyad,
							   Numara = option.Source.Numara,
							   Sehir = option.Source.Sehir,
							   Telefon = option.Source.Telefon,
							   Bolum = option.Source.Bolum,
							   Score = option.Score,
							   SuggestedKisi = option.Text,
						   };

			return new KisiSuggestResponse
			{
				suggests = suggests
			};
		}
	}
}
