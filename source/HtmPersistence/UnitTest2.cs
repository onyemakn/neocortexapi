using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeoCortexApi;
using NeoCortexApi.Entities;
using Newtonsoft.Json;
using System.IO;

namespace HtmPersistence
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void SerializeSparseObjectMatrix()
        {
            // Create SparseObjectMatrix
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
            InMemoryDistributedDictionary<int,int> numNodes = new InMemoryDistributedDictionary<int, int>(3);
            // There are no Serialize of Dictionary in InMemoryDistributedDictionary
            numNodes.Add(145, 29);
            numNodes.Add(123, 26);
            numNodes.Add(531, 26);
            numNodes.Add(1536, 26);
            numNodes.Add(1529, 26);
            // Serialize 
            using (StreamWriter sw = new StreamWriter("InMem.txt"))
            {
                numNodes.Serialize(sw);
            }
            // Deserizlize
            InMemoryDistributedDictionary<int, int> newTest = new InMemoryDistributedDictionary<int, int>();
            using (StreamReader sr = new StreamReader("InMem.txt"))
            {
                newTest= InMemoryDistributedDictionary<int, int>.Deserialize(sr);

                HtmSerializer2.IsEqual(numNodes,newTest);
            }
        }

        [TestMethod]
        public void SerializeSparseBinaryMatrix()
        {
            // Create SParse BinarySparseMatrix
            // either by dicrect creation or running experiment
            int[] dimensions = { 100 , 100 };

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
