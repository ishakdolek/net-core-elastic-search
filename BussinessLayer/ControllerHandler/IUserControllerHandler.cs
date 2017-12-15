using System.Collections.Generic;
using elasticSearch.BussinessLayer.Common;
using elasticSearch.BussinessLayer.Model;
using elasticSearch.BussinessLayer.User;

namespace elasticSearch.BussinessLayer.ControllerHandler
{
    public interface IUserControllerHandler
    {
         ResultModel<bool> InsertUser(UserModel userModel);
     }
}