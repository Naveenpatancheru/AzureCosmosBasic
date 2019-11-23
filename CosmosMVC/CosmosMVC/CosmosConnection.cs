using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmosMVC
{
    public static class CosmosConnection
    {
        public static CosmosClient Client { get; private set; }

        static CosmosConnection()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var endPoint = config["CosmosEndPoint"];
            var masterKey = config["CosmosMasterKey"];
            Client = new CosmosClient(endPoint, masterKey);
        }
    }
}