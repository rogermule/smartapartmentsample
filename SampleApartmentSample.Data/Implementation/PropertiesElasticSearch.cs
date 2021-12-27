using Nest;
using Newtonsoft.Json;
using SampleApartmentSample.Data.Abstractions;
using SampleApartmentSample.Data.Models;
using SmartApartmentSample.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Properties = SmartApartmentSample.Core.Models.Properties;

namespace SampleApartmentSample.Data.Implementation
{
    public class PropertiesElasticSearch : IPropertiesElasticSearch
    {
        private const string INDEX = "properties";
        private readonly ElasticClient _client;

        public PropertiesElasticSearch(ElasticClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Properties>> Search(SearchInputModel search)
        {
            ISearchResponse<Properties> results;
            bool isScoped = search.market == null ? false : true;
            int limit = search.limit != null ? (int)search.limit : 25;

            if (isScoped)
            {
                results = await _client.SearchAsync<Properties>(s => s.Size(limit)
                            .Index(INDEX)
                            .Query(q => q
                            .Bool(b => b
                               .Must(mu => mu
                                .Match(t => t
                                       .Field(f => f.name)
                                       .Query(search.searchkey)
                                       .Operator(Operator.And)
                                   ), mu => mu
                                   .Match(sc => sc
                                       .Field(f => f.market)
                                       .Query(search.market)
                                       .Strict(true)
                                   )
                            )))
                        );
            }
            else
            {
                results = await _client.SearchAsync<Properties>(s => s.Size(limit)
                      .Index(INDEX)
                      .Query(q => q
                        .Match(t => t
                            .Field(f => f.name)
                            .Query(search.searchkey)
                            .Operator(Operator.And)
                        )
                      )
                  );
            }

            return results?.Documents;
        }

        public async Task<string> Upload(Stream body)
        {
            using var reader = new StreamReader(body);
            var bd = await reader.ReadToEndAsync();

            var jd = JsonConvert.DeserializeObject<PropertyHolder[]>(bd);

            foreach (var item in jd)
            {
                var index = new CustomIndex() { _index = "properties", _id = item.property.propertyID.ToString() };
                var indexHolder = new IndexHolder() { index = index };
                using StreamWriter file = new StreamWriter("properties.json", append: true);
                await file.WriteLineAsync(JsonConvert.SerializeObject(indexHolder));
                await file.WriteLineAsync(JsonConvert.SerializeObject(item.property));
            }

            return "Properties Upload Indexed file created successfully";
        }
    }
}
