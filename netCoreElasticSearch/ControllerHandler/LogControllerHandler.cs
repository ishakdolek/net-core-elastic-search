using System;
using System.Collections.Generic;
using netCoreElasticSearch.Common;
using netCoreElasticSearch.Model;
using Nest;

namespace netCoreElasticSearch.ControllerHandler
{
    public class LogControllerHandler:ILogControllerHandler
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
                var response =  ElasticClient.Search<LogModel>(p => p
                    .Query(q => q
                        .Match(m => m
                            .Field(f => f.Message)
                            .Query(input)
                            .Operator(Operator.And)
                        )
                    ).Sort(s => s.Descending(f => f.DateTime))
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
    }
}


//[HttpGet("{error}")]
//public async Task<List<Log>> Get(string error)
//{
///*var response2 = elasticClient.Search<Log>(p => p
//  .From(0)
//  .Size(10)
//  .Query(q =>
//  q.Term(f => f.UserID, 1)
//)
//);*/

//var response = await elasticClient.SearchAsync<Log>(p => p
//    .Query(q => q
//        .Match(m => m
//            .Field(f => f.message)
//            .Query(error)
//            .Operator(Operator.And)
//        )
//    )
//    .Sort(s => s.Descending(f => f.PostDate))
//);

//var result = new List<Log>();
//    foreach (var document in response.Documents)
//{
//    result.Add(document);
//}
//return result;
//}