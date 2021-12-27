using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SampleApartmentSample.Data.Abstractions
{
    public interface IUploadIndexer
    {
        public Task<string> Upload(Stream body);
    }
}
