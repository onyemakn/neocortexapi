using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoCortexApi.Entities;
using System.Collections.Generic;
using System.IO;

namespace HTMPersistanceUnitTests
{
    [TestClass]
    public class SerializeSegmentActivityTest
    {
        /// <summary>
        /// Test SegmentActivity.
        /// </summary>
        [TestMethod]
        public void TestSegmentActivitySErialization()
        {

            SegmentActivity segment = new SegmentActivity();

            segment.ActiveSynapses = new Dictionary<int, int>();
            segment.ActiveSynapses.Add(23, 1);
            segment.ActiveSynapses.Add(24, 2);
            segment.ActiveSynapses.Add(35, 3);
            segment.PotentialSynapses = new Dictionary<int, int>();
            segment.PotentialSynapses.Add(2, 56);
            segment.PotentialSynapses.Add(22, 6);

            segment.PotentialSynapses.Add(24, 26);

            using (StreamWriter sw = new StreamWriter($"ser_{nameof(SerializeSegmentActivityTest)}.txt"))
            {
                segment.Serialize(sw);
            }
            using (StreamReader sr = new StreamReader($"ser_{nameof(SerializeSegmentActivityTest)}.txt"))

            {
                SegmentActivity segment1 = SegmentActivity.Deserialize(sr);

                Assert.IsTrue(segment1.Equals(segment));
            }
        }

        /*private object SerializeSegmentActivityTest()
        {
            throw new NotImplementedException();
        }
        */
        [TestClass]
        public class DistalDendriteSerializationTest
        {
            [TestMethod]
            public void TestDistalDendriteSerialization()
            {
                Cell c1 = new Cell(1, 1, 10, 1, NeoCortexEntities.NeuroVisualizer.CellActivity.ActiveCell);

                DistalDendrite d1 = new DistalDendrite(c1, 1, 1, 1, 0.5, 10);

                using (StreamWriter sw = new StreamWriter("dist_ser.txt"))
                {
                    d1.Serialize(sw);
                }

                DistalDendrite d2;
                using (StreamReader sr = new StreamReader("dist_ser.txt"))
                {
                    d2 = DistalDendrite.Deserialize(sr);
                }

                var result = HtmSerializer2.IsEqual(d1, d2);
                Assert.IsTrue(result);
            }
        }


        [TestClass]
        public class ProximalDendriteSerializationTest
        {
            [TestMethod]
            public void TestProximalDendriteSerialization()
            {
                Cell c1 = new Cell(1, 1, 10, 1, NeoCortexEntities.NeuroVisualizer.CellActivity.ActiveCell);

                ProximalDendrite p1 = new ProximalDendrite(1, 1.2, 2);

                using (StreamWriter sw = new StreamWriter("prox_ser.txt"))
                {
                    p1.Serialize(sw);
                }

                ProximalDendrite p2;
                using (StreamReader sr = new StreamReader("prox_ser.txt"))
                {
                    p2 = ProximalDendrite.Deserialize(sr);
                }

                var result = HtmSerializer2.IsEqual(p1, p2);
                Assert.IsTrue(result);


                /*
                
               #region ProximalDendrite
               public void SerializeProximalDendrite(ProximalDendrite p1, string $"ser_{nameof(SerializeArrayDouble)}.txt")
               {
                   using (StreamWriter sw = new StreamWriter($"ser_{nameof(SerializeArrayDouble)}.txt"))
                   {
                       p1.Serialize(sw);
                   }
               }
               public ProximalDendrite DeserializeProximalDendrite(string $"ser_{nameof(SerializeArrayDouble)}.txt")
               {
                   using (StreamReader sr = new StreamReader($"ser_{nameof(SerializeArrayDouble)}.txt"filePath))
                   {
                       return ProximalDendrite.Deserialize(sr);
                   }
               }
               #endregion*/
            }

        }



        /*
       #region ProximalDendrite
       public void SerializeProximalDendrite(ProximalDendrite pd, string filePath)
       {
           using (StreamWriter sw = new StreamWriter(filePath))
           {
               pd.Serialize(sw);
           }
       }
       public ProximalDendrite DeserializeProximalDendrite(string filePath)
       {
           using (StreamReader sr = new StreamReader(filePath))
           {
               return ProximalDendrite.Deserialize(sr);
           }
       }
       #endregion*/
    }
}