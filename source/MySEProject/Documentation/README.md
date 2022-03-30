# Introduction

This Repository contains a Serialization and Deserialization Project which is created as part of the NeoCortexApi ASP.NET libraries to initiate a serialization and the saving of the instances of some of the HTM module to the stream of which it can also create new instances from the saved stream.

In this project, the system uses the data from the previously applied experiments to train itself and can start of from where it stopped. The goal is the implement a system of Persistence in the HTM to enable it have the functionality of remembrance similar to the Human Brain. The persistence is designed as an implementation of a custom serializer / desterilizer. The serializer saves the instance of some HTM module to the stream and deserializer is responsible to create the instance from the stream.

# Getting Started 

The NeoCortexApi built today runs on .NET Standard 2.2 library and also It contains the libraries required to run SpatialPooler, TemoralPooler and few encoders. The NeoCortexApi was develpoed based on the idealogy of NeoCortex of the Human Brain. It already uses runs on some of the Functionality of the Hierarchical Temporal Memory (HTM) developed by Numenta

Hierarchical temporal memory (HTM) is a biologically constrained machine intelligence technology developed by Numenta. Originally described in the 2004 book On Intelligence by Jeff Hawkins with Sandra Blakeslee, HTM is primarily used today for anomaly detection in streaming data. The technology is based on neuroscience and the physiology and interaction of pyramidal neurons in the neocortex of the mammalian (in particular, human) brain. 

The persistence is designed as implementation of a custom serializer/deserilizer. The serializer saves the instance of some HTM module to the stream and deserializer is responsible to create the instance from the stream.

# Use Of Project

With this Project, at the running of the NeoCortexApi, the instances of the HTM Modules that are called can be saved into a stream and the continued whenever.

The Project enables the capability of Remembrance at the which the saved states and instances of the HTM Modules in the NeoCortexApi are not lost at the termination of the Program.


# Classes Used

The following Classes are part of the existing NeoCortexApi libraries in which the "Serialization and Deserialization" are implemented.

### 1. SparseObjectMatrix 

Here, using StreamWriter, the instances of the SparseObjaectMatrix class are saved and written in a text file "ser.txt"

Here, using StreamReader, the instances of the SparseObjaectMatrix class are read and saved in a text file "ser.txt"

~~~csharp
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
~~~

### 2. InMemoryDistributedDictionary

Here, the StreamWriter saves the instances of the keys and key values of the InMemoryDistributedDictionary class and writes into a file "InMem.txt"

Here, the StreamReader reads the instances of the keys and key values of the InMemoryDistributedDictionary class and saves it into a file "InMem.txt"

~~~csharp
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
~~~

### 3. SparseBinaryMatrix

Here, using StreamWriter, the instances of the SparseBinaryMatrix class are saved and written in a text file "ser.txt"

Here, using StreamReader, the instances of the SparseBinaryMatrix class are read and saved in a text file "ser.txt"

~~~csharp
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
~~~


# Results



# How To Run Project

This project runs as part of the libraries of the NeoCortexApi, First to run the project you'd need to fork the NeoCortexApi Repository and run the Project from the HTM Persistence folder. 
