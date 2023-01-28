using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.Models
{
    public class OkObjectBulkResult : OkObjectResult
    {
        public OkObjectBulkResult(string message, int code, object failed)
            :base (new {
                Code = code,
                Messsage = message,
                Failed = failed
        })
        {

        }
    }
}
