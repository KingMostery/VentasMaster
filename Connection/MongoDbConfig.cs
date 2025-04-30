using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterVentas.ModelViews.User;

namespace MasterVentas.Connection
{
    internal class MongoDbConfig
    {
        public string ConnectionUri { get; set; } = "mongodb://192.168.20.74:27017/?retryWrites=true&loadBalanced=false&serverSelectionTimeoutMS=5000&connectTimeoutMS=10000";
        public string DatabaseName { get; set; } = "MasterVenta";
    }
}
