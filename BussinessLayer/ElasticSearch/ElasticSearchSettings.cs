using System;
using System.Collections.Generic;
using System.Linq;
using elasticSearch.BussinessLayer.Common;
using elasticSearch.BussinessLayer.Model;
using Nest;

namespace elasticSearch.BussinessLayer.ElasticSearch
{
    public class ElasticSearchSettings<TModel> : IElasticSearchSettings<TModel> where TModel : class
    {
        public static Uri EsUri;
        public static ConnectionSettings EsConfig;
        public static ElasticClient EsClient;

        public ElasticSearchSettings()
        {
            EsUri = new Uri("http://localhost:9200/");
            EsConfig = new ConnectionSettings(EsUri);
            EsClient = new ElasticClient(EsConfig);
        }


        public ResultModel<bool> CreateIndex(TModel model, string indexName)
        {
            var result = new ResultModel<bool>();
            try
            {
                var settings = new IndexSettings { NumberOfReplicas = 1, NumberOfShards = 3 };

                var indexConfig = new IndexState
                {
                    Settings = settings
                };

                if (!EsClient.IndexExists(indexName).Exists)
                {
                    EsClient.CreateIndex(indexName, c => c
                        .InitializeUsing(indexConfig)
                        .Mappings(m => m.Map<TModel>(mp => mp.AutoMap())));
                }
                result.Value = true;
                result.IsSuccess = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           return result;
        }

        public ResultModel<bool> Insert(TModel model,string indexName)
        {
            var result = new ResultModel<bool>();
            try
            {
                EsClient.Index(model, idx => idx.Index(indexName));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return result;
        }

        public ResultModel<List<TModel>> GetRecord(TModel model)
        {
            var result = new ResultModel<List<TModel>>
            {
                Value = new List<TModel>()
            };
            try
            {

                var response = EsClient.Search<TModel>(p => p
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