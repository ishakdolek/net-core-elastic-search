using System.Collections.Generic;
using elasticSearch.BussinessLayer.Common;
using Nest;

namespace elasticSearch.BussinessLayer.ElasticSearch
{
    public interface IElasticSearchSettings<TModel>
    {
        ResultModel<bool> CreateIndex(TModel model,string indexName);
        ResultModel<bool> Insert(TModel model,string indexName);
        ResultModel<List<TModel>> GetRecord(TModel model);
    }
}