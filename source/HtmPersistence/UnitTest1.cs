using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoCortexApi;
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
               matrix.Serialize(sw);
            }
            // Deserizlize
            SparseObjectMatrix<int[]> matrixNew = new();
            using (StreamReader sr = new StreamReader("ser.txt"))
            {
                matrixNew = SparseObjectMatrix<int[]>.Deserialize(sr);

                HtmSerializer2.IsEqual(matrix, matrixNew);
            }
        }

        [TestMethod]
        public void SerializeInMemoryDistributedDictionary()
        {
            InMemoryDistributedDictionary<string,int> numNodes = new InMemoryDistributedDictionary<string, int>(2);
            // There are no Serialize of Dictionary in InMemoryDistributedDictionary
            numNodes.Add("Kizito", 29);
            numNodes.Add("Daniel", 26);
            numNodes.Add("Thahn", 25);
            numNodes.Add("Mr.Dobric", 35);

            // Serialize 
            using (StreamWriter sw = new StreamWriter("InMem.txt"))
            {
                numNodes.Serialize(sw);
            }
            // Deserizlize
            InMemoryDistributedDictionary<string, int> newTest = new InMemoryDistributedDictionary<string, int>();
            using (StreamReader sr = new StreamReader("InMem.txt"))
            {
                newTest= InMemoryDistributedDictionary<string, int>.Deserialize(sr);

                HtmSerializer2.IsEqual(numNodes,newTest);
            }
        }

        [TestMethod]
        public void SerializeSparseBinaryMatrix()
        {
            // Create SParse BinarySparseMatrix
            // either by dicrect creation or running experiment
            int[] dimensions = { 10, 10 };

            //IDistributedDictionary<int, int[]> dict = new();

            SparseBinaryMatrix binaryMatrix = new(dimensions, false);

            // Serialize 
            using (StreamWriter sw = new StreamWriter("Binary.txt"))
            {
                binaryMatrix.Serialize(sw);
            }
            // Deserizlize
            SparseBinaryMatrix newBinary = new();
            using (StreamReader sr = new StreamReader("Binary.txt"))
            {
                newBinary = SparseBinaryMatrix.Deserialize(sr);

                HtmSerializer2.IsEqual(binaryMatrix, newBinary);
            }
        }
    }
    
}