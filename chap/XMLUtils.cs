
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TrivialPursuit.chap

{
    public static class XMLUtils
    {
    
        public static T Deserialization<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(filePath))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
        public static void Serialization<T>(T obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }
    
    
    
    }
    
    
}

