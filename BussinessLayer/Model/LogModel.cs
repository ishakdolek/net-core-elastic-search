using System;

namespace netCoreElasticSearch.Model
{
    public class LogModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}