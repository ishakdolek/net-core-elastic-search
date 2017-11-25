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

        private static readonly ConnectionSettings connSettings =
            new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex(Constant.IndexName)
                //Optionally override the default index for specific types
                .MapDefaultTypeIndices(m => m
                    .Add(typeof(LogModel), Constant.IndexName));
        private static readonly ElasticClient ElasticClient = new ElasticClient(connSettings);


        public LogController(ILogControllerHandler logControllerHandler)
        {
            _logControllerHandler = logControllerHandler;
        }
        
        // GET api/values
        [HttpGet]
        public async Task<List<string>> GetAllErrors()
        {
            _logControllerHandler.GetLogListModel("Aaa");
            var response = await ElasticClient.SearchAsync<LogModel>(p => p
                //.Source(f=>f.Includes(p2=>p2.Field(f2=>f2.message)))  
                .Query(q => q
                    .MatchAll()
                )
            );
            var result = new List<string>();
            foreach (var document in response.Documents)
            {
                result.Add(document.Message);
            }
            return result.Distinct().ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public ResultModel<bool> InsertLog([FromBody] LogModel  log)
        {
            //elasticClient.DeleteIndex("log_history");

            //if (!ElasticClient.IndexExists("error_log").Exists)
            //{
            //    var indexSettings = new IndexSettings
            //    {
            //        NumberOfReplicas = 1,
            //        NumberOfShards = 3
            //    };


            //    var createIndexDescriptor = new CreateIndexDescriptor("log_history")
            //        .Mappings(ms => ms
            //            .Map<LogModel>(m => m.AutoMap())
            //        )
            //        .InitializeUsing(new IndexState() { Settings = indexSettings })
            //        .Aliases(a => a.Alias("error_log"));

            //    var response = ElasticClient.CreateIndex(createIndexDescriptor);


            //}
            ////Insert Data           

            //ElasticClient.Index<LogModel>(log, idx => idx.Index(Constant.IndexName));

            //return new ResultModel<bool>
            //{
            //    Value = true,
            //    IsSuccess = true
            //};

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
