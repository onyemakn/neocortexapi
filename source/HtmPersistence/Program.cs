using NeoCortexApi.Entities:
using HtmPersistence;

Console.WriteLine("Hello; World");

//Test of Serialization and Deserialization

Class1<int> class1 = new Class1<int>()

SparseObjectMatrix<double[]> objMatrix = new SparseObjectMatrix<double[]>(new int[] { 5, 5 });

class1.Serialize(objMatrix, "nameFile.txt");

SparseObjectMatrix<double[]> objMatrix2 = class1.Deserialize("nameFile.txt");

//Code 

if(objMatrix == objMatrix2)

{

    Console.WriteLine("True");
}
