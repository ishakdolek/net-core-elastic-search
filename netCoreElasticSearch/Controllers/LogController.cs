using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BussinessLayer.Common;
using BussinessLayer.ControllerHandler;
using Microsoft.AspNetCore.Mvc;
using netCoreElasticSearch.Model;
using Nest;

namespace netCoreElasticSearch.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogControllerHandler _logControllerHandler;

        public LogController(ILogControllerHandler logControllerHandler)
        {
            _logControllerHandler = logControllerHandler;
        }

        // GET api/values
        [HttpGet]
        public List<LogModel> GetAllErrors()
        {
            return _logControllerHandler.GetLogAll().Value;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // GET api/values/5
        [HttpGet("{input}")]
        public ResultModel<List<LogModel>> Get(string input)
        {
            return _logControllerHandler.GetLogListModel(input);
        }

        // POST api/values
        [HttpPost]
        public ResultModel<bool> InsertLog([FromBody] LogModel log)
        {
            return _logControllerHandler.InsertLog(log);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
