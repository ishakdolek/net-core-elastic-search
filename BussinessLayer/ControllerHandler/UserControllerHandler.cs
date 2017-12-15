using System;
using elasticSearch.BussinessLayer.Common;
using elasticSearch.BussinessLayer.ElasticSearch;
using elasticSearch.BussinessLayer.User;

namespace elasticSearch.BussinessLayer.ControllerHandler
{
    public class UserControllerHandler : IUserControllerHandler
    {
        private readonly IElasticSearchSettings<UserModel> _searchSettings;

        public UserControllerHandler(IElasticSearchSettings<UserModel> searchSettings)
        {
            _searchSettings = searchSettings;
        }
        public ResultModel<bool> InsertUser(UserModel userModel)
        {
            return _searchSettings.Insert(userModel, "users");
        }
    }
}