using SampleApartmentSample.Data.Models;
using SmartApartmentSample.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleApartmentSample.Data.Abstractions
{
    public interface IMgmtElasticSearch: IUploadIndexer
    {
        public Task<IEnumerable<Mgmt>> Search(SearchInputModel search);
    }
}
