using System.Collections.Generic;
using elasticSearch.BussinessLayer.Common;
using elasticSearch.BussinessLayer.ControllerHandler;
using elasticSearch.BussinessLayer.Model;
using Microsoft.AspNetCore.Mvc;

namespace elasticSearch.WebApi.Controllers
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
            //_logger.Info("Başladı....");
            return _logControllerHandler.GetLogListModel(input);
        }

        // POST api/values
        [HttpPost]
        public ResultModel<bool> InsertLog([FromBody] LogModel log)
        {
            //_logger.Info("Bitti");
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
