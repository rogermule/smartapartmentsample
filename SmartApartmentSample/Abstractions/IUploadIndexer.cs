using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentSample.Abstractions
{
    public interface IUploadIndexer
    {
        public Task<IActionResult> Upload();
    }
}
