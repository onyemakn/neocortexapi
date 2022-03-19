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
        /// Test SegmentActivity using Active and Potential Synapses
        /// Apply Serialization and Deserialization 
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
            #region Serialization
            using (StreamWriter sw = new StreamWriter($"ser_{nameof(SerializeSegmentActivityTest)}.txt"))
            {
                segment.Serialize(sw);
            }
            #endregion
            #region Deserialization
            using (StreamReader sr = new StreamReader($"ser_{nameof(SerializeSegmentActivityTest)}.txt"))

            {
                SegmentActivity segment1 = SegmentActivity.Deserialize(sr);

                Assert.IsTrue(segment1.Equals(segment));
            }
            #endregion
        }

        /// <summary>
        /// Test DistalDendrite using class Cell
        /// Apply Serialization and Deserialization and compare their valiues 
        /// </summary>

        [TestClass]
        public class DistalDendriteSerializationTest
        {
            [TestMethod]
            public void TestDistalDendriteSerialization()
            {
                Cell c1 = new Cell(1, 1, 10, 1, NeoCortexEntities.NeuroVisualizer.CellActivity.ActiveCell);

                DistalDendrite d1 = new DistalDendrite(c1, 1, 1, 1, 0.5, 10);
                #region Serialization
                using (StreamWriter sw = new StreamWriter("dist_ser.txt"))
                {
                    d1.Serialize(sw);
                }
                #endregion
                #region Deserialization
                DistalDendrite d2;
                using (StreamReader sr = new StreamReader("dist_ser.txt"))
                {
                    d2 = DistalDendrite.Deserialize(sr);
                }
                
                var result = HtmSerializer2.IsEqual(d1, d2);
                Assert.IsTrue(result);
            }
            #endregion 
        }

        /// <summary>
        /// Test Proximal Dendrite using class Cell
        /// Apply Serialization and Deserialization and compare their valiues 
        /// </summary>


        [TestClass]
        public class ProximalDendriteSerializationTest
        {
            [TestMethod]
            public void TestProximalDendriteSerialization()
            {
                Cell c1 = new Cell(1, 1, 10, 1, NeoCortexEntities.NeuroVisualizer.CellActivity.ActiveCell);

                ProximalDendrite p1 = new ProximalDendrite(1, 1.2, 2);
                #region Serialization
                using (StreamWriter sw = new StreamWriter("prox_ser.txt"))
                {
                    p1.Serialize(sw);
                }
                #endregion

                #region Derialization
                ProximalDendrite p2;
                using (StreamReader sr = new StreamReader("prox_ser.txt"))
                {
                    p2 = ProximalDendrite.Deserialize(sr);
                }
                #endregion
                var result = HtmSerializer2.IsEqual(p1, p2);
                Assert.IsTrue(result);


            }

        }



    }
}