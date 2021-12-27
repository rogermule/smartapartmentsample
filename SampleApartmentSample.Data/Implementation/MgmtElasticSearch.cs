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

namespace SampleApartmentSample.Data.Implementation
{
    public class MgmtElasticSearch : IMgmtElasticSearch
    {
        private const string INDEX = "mgmt";
        private readonly ElasticClient _client;

        public MgmtElasticSearch(ElasticClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Mgmt>> Search(SearchInputModel search)
        {
            ISearchResponse<Mgmt> results;
            bool isScoped = search.market == null ? false : true;
            int limit = search.limit != null ? (int)search.limit : 25;

            if (isScoped)
            {
                results = await _client.SearchAsync<Mgmt>(s => s.Size(limit)
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
                results = await _client.SearchAsync<Mgmt>(s => s.Size(limit)
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

            var jd = JsonConvert.DeserializeObject<MgmtHolder[]>(bd);

            foreach (var item in jd)
            {
                var index = new CustomIndex() { _index = "mgmt", _id = item.mgmt.mgmtID.ToString() };
                var indexHolder = new IndexHolder() { index = index };
                using StreamWriter file = new StreamWriter("mgmt.json", append: true);
                await file.WriteLineAsync(JsonConvert.SerializeObject(indexHolder));
                await file.WriteLineAsync(JsonConvert.SerializeObject(item.mgmt));
            }

            return "Mgmt Upload Indexed file created successfully";
        }
    }
}
