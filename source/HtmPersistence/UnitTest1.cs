using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoCortexApi.Entities;
using Newtonsoft.Json;
using System.IO;

namespace HtmPersistence
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Create SParse BinarySparseMatrix
            // either by dicrect creation or running experiment
            int[] dimensions = { 10, 10 };

            //IDistributedDictionary<int, int[]> dict = new();

            SparseObjectMatrix<int[]> matrix = new(dimensions, false);

            // Serialize 
            using (StreamWriter sw = new StreamWriter("ser.txt"))
            {
                var json = JsonConvert.SerializeObject(matrix);
                sw.WriteLine(json.ToString());
            }
            // Deserizlize
            SparseObjectMatrix<int[]> matrixNew = new();
            using (StreamReader sr = new StreamReader("ser.txt"))
            {
                var json = sr.ReadToEnd();
                matrixNew = JsonConvert.DeserializeObject<SparseObjectMatrix<int[]>>(json);
                Assert.AreEqual(matrix, matrixNew);
            }
        }
    }
}