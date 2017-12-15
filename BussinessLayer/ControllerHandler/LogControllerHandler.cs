using System;
using System.Collections.Generic;
using System.Linq;
using elasticSearch.BussinessLayer.Common;
using elasticSearch.BussinessLayer.Model;
using Nest;
using NLog;

namespace elasticSearch.BussinessLayer.ControllerHandler
{
    public class LogControllerHandler : ILogControllerHandler
    {
    

        private static readonly ConnectionSettings connSettings =
            new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex(Constant.IndexName)
                //Optionally override the default index for specific types
                .MapDefaultTypeIndices(m => m
                    .Add(typeof(LogModel), Constant.IndexName));
        private static readonly ElasticClient ElasticClient = new ElasticClient(connSettings);

      
        public ResultModel<List<LogModel>> GetLogListModel(string input)
        {
         
            var result = new ResultModel<List<LogModel>>
            {
                Value = new List<LogModel>()
            };
            try
            {
                var response = ElasticClient.Search<LogModel>(p => p
                   .Query(q => q
                       .Match(m => m
                           .Field(f => f.Message)
                           .Query(input)
                           .Operator(Operator.And)
                       )
                   ).Sort(s => s.Descending(f => f.Id))
                );

                foreach (var document in response.Documents)
                {
                    result.Value.Add(document);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return result;

        }

        public ResultModel<bool> InsertLog(LogModel logModel)
        {

            if (!ElasticClient.IndexExists("error_log").Exists)
            {
                var indexSettings = new IndexSettings
                {
                    NumberOfReplicas = 1,
                    NumberOfShards = 3
                };


                var createIndexDescriptor = new CreateIndexDescriptor(Constant.IndexName)
                    .Mappings(ms => ms
                        .Map<LogModel>(m => m.AutoMap())
                    )
                    .InitializeUsing(new IndexState() { Settings = indexSettings })
                    .Aliases(a => a.Alias("error_log"));

                var response = ElasticClient.CreateIndex(createIndexDescriptor);


            }
            //Insert Data           

            ElasticClient.Index(logModel, idx => idx.Index(Constant.IndexName));

            return new ResultModel<bool>
            {
                Value = true,
                IsSuccess = true
            };
        }

        public ResultModel<List<LogModel>> GetLogAll()
        {
            var result = new ResultModel<List<LogModel>>
            {
                Value = new List<LogModel>()
            };
            try
            {

                var response =  ElasticClient.Search<LogModel>(p => p
                    //.Source(f=>f.Includes(p2=>p2.Field(f2=>f2.message)))  
                    .Query(q => q
                        .MatchAll()
                    )
                );

                foreach (var document in response.Documents)
                {
                    result.Value.Add(document);
                }
                result.Value.Distinct();
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            return result;
        }
    }
}
