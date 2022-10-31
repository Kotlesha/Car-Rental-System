using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Car_Rental_System
{
    static class Serialize
    {
        public static void Serialization<T>(List<T> objects)
        {
            DataContractJsonSerializer serializer = new(typeof(List<T>));
            MemoryStream stream = new();
            serializer.WriteObject(stream, objects);
            stream.Seek(0, SeekOrigin.Begin);
            string serialization;
            StreamReader reader = new(stream);
            serialization = reader.ReadToEnd();
            string path = string.Empty;
            if (typeof(T) == typeof(Customer))
            {
                path = @"C:\Labs\Customers.json";
            }
            else if (typeof(T) == typeof(RentalPointAdministrator))
            {
                path = @"C:\Labs\Administrators.json";
            }

            StreamWriter writer = new(path);
            writer.WriteLine(serialization);
            writer.Close();
        }

        public static List<T> Deserialization<T>()
        {
            string path = string.Empty;

            if (typeof(T) == typeof(Customer))
            {
                path = @"C:\Labs\Customers.json";
            }
            else if (typeof(T) == typeof(RentalPointAdministrator))
            {
                path = @"C:\Labs\Administrators.json";
            }

            StreamReader reader = new(path);
            string jsonString = reader.ReadToEnd();
            reader.Close();
            DataContractJsonSerializer deserializer = new(typeof(List<T>));
            MemoryStream stream = new(Encoding.UTF8.GetBytes(jsonString));
            return deserializer.ReadObject(stream) as List<T>;
        }
    }
}
