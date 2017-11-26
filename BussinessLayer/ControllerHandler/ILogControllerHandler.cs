using System.Collections.Generic;
using BussinessLayer.Common;
using netCoreElasticSearch.Model;

namespace BussinessLayer.ControllerHandler
{
    public interface ILogControllerHandler
    {
        ResultModel<List<LogModel>> GetLogListModel(string input);
        ResultModel<bool> InsertLog(LogModel logModel);
        ResultModel<List<LogModel>> GetLogAll();
    }
}