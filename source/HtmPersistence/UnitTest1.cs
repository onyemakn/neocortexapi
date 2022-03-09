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
        public void SerializeSparseObjectMatrix()
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

                var dimMatrix = matrix.GetDimensions();
                var dimMatrixNew = matrixNew.GetDimensions();
                Assert.AreEqual(dimMatrix[0], dimMatrixNew[0]);
                Assert.AreEqual(dimMatrix[1], dimMatrixNew[1]);
                Assert.AreEqual(matrix.ModuleTopology.IsMajorOrdering, matrixNew.ModuleTopology.IsMajorOrdering);
            }
        }

        [TestMethod]
        public void SerializeAbstractFlatMatrix()
        {
            // Create SParse BinarySparseMatrix
            // either by dicrect creation or running experiment
            int[] dimensions = { 10, 10 };

            //IDistributedDictionary<int, int[]> dict = new();

            AbstractFlatMatrix<int[]> matrix = new(dimensions, false);

            // Serialize 
            using (StreamWriter sw = new StreamWriter("ser.txt"))
            {
                var json = JsonConvert.SerializeObject(matrix);
                sw.WriteLine(json.ToString());
            }
            // Deserialize
            AbstractFlatMatrix<int[]> matrixNew = new();
            using (StreamReader sr = new StreamReader("ser.txt"))
            {
                var json = sr.ReadToEnd();
                matrixNew = JsonConvert.DeserializeObject<AbstractFlatMatrix<int[]>>(json);

                var dimMatrix = matrix.GetDimensions();
                var dimMatrixNew = matrixNew.GetDimensions();
                Assert.AreEqual(dimMatrix[0], dimMatrixNew[0]);
                Assert.AreEqual(dimMatrix[1], dimMatrixNew[1]);
                Assert.AreEqual(matrix.ModuleTopology.IsMajorOrdering, matrixNew.ModuleTopology.IsMajorOrdering);
            }
        }

    }
}