using System.Collections.Generic;
using netCoreElasticSearch.Common;
using netCoreElasticSearch.Model;

namespace netCoreElasticSearch.ControllerHandler
{
    public interface ILogControllerHandler
    {
        ResultModel<List<LogModel>> GetLogListModel(string input);
    }
}