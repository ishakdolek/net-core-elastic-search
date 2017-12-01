using System.Collections.Generic;
using elasticSearch.BussinessLayer.Common;
using elasticSearch.BussinessLayer.Model;

namespace elasticSearch.BussinessLayer.ControllerHandler
{
    public interface ILogControllerHandler
    {
        ResultModel<List<LogModel>> GetLogListModel(string input);
        ResultModel<bool> InsertLog(LogModel logModel);
        ResultModel<List<LogModel>> GetLogAll();
    }
}