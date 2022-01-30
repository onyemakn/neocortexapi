using AkkaSb.Net;
using Microsoft.Extensions.Logging;
using NeoCortexApi.DistributedComputeLib;
using NeoCortexApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmPersistence
{
    public class Class1<T>
    {

        public SparseObjectMatrix<Column> Deserialize(string v)
        {
            //Read File
            // read to int[] the dimension and boll the useColumnMajorOrdering
            int[] dimension = new int[42]; // code goes here
            bool useColumnMajorOrdering = false; //code goes here
            //Read HTMConfig from some file
            //
            HtmConfig htmConfig = new HtmConfig();
            /*
            DistributedMemory distMem = new DistributedMemory();
            if (htmConfig == null)
            {
               distMem = new DistributedMemory()
                {
                    ColumnDictionary = new InMemoryDistributedDictionary<int, NeoCortexApi.Entities.Column>(1),
                    //PoolDictionary = new InMemoryDistributedDictionary<int, NeoCortexApi.Entities.Pool>(1),
                };
            }
            else
            {
                var cfg = DefaultSbConfig;
                distMem = new DistributedMemory()
                {
                    ColumnDictionary = new ActorSbDistributedDictionaryBase<Column>(cfg, GetLogger()),

                    //ColumnDictionary = new HtmSparseIntDictionary<Column>(cfg),
                    //PoolDictionary = new HtmSparseIntDictionary<Pool>(cfg),
                }; ;
            }
            */
            DistributedMemory distMem = new DistributedMemory();
            distMem.ColumnDictionary.htmConfig = htmConfig;
            
            SparseObjectMatrix<Column> a =  new SparseObjectMatrix<Column>(dimension, useColumnMajorOrdering ,distMem.ColumnDictionary);
            return a;
        }
        #region CopiedFunction
        // Function from UnitTestHelper copied
        public static ActorSbConfig DefaultSbConfig
        {
            get
            {
                ActorSbConfig cfg = new ActorSbConfig
                {
                    SbConnStr = Environment.GetEnvironmentVariable("SbConnStr"),
                    ReplyMsgQueue = "actorsystem/rcvlocal",
                    RequestMsgTopic = "actorsystem/actortopic",
                    NumOfElementsPerPartition = -1, // This means, number of partitions equals number of nodes.
                    NumOfPartitions = 35,// Should be uniformly distributed across nodes.
                    BatchSize = 1000,
                    ConnectionTimeout = TimeSpan.FromMinutes(5),

                    //Nodes = new List<string>() { "node1", "node2", "node3" }
                    Nodes = new List<string>() { "node1" }
                };

                return cfg;
            }
        }


        public static ILogger GetLogger(string logger = "UnitTest")
        {
            ILoggerFactory factory = LoggerFactory.Create(logBuilder =>
            {
                logBuilder.AddDebug();
                logBuilder.AddConsole();
            });

            return factory.CreateLogger(logger);
        }
        // 
        #endregion
        public void Serialize<T>(SparseObjectMatrix<T> a) where T : class
        {
            int[] arrayToBeSerialized = a.GetDimensions();
            bool useColumnMajorOrdering = a.ModuleTopology.IsMajorOrdering;
            //HtmConfig from a ?
            //Serialize the variables
        }

    }

    public class Class2<T>
    {

        public void Deserialize(string v)
        {

        }

        public void Serialize(SparseObjectMatrix<T[]> a)
        {

        }

    }


}
