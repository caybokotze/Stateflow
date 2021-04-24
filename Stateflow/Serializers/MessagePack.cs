using System.IO;
using System.Text;
using MsgPack.Serialization;

namespace Stateflow.Serializers
{
    public static class MessagePack
    {
        public static string Serialize<T>(object obj)
        {
            var context = new SerializationContext
            {
                SerializationMethod = SerializationMethod.Map
            };

            var serializer = MessagePackSerializer.Get<T>(context);

            using var byteStream = new MemoryStream();
            serializer.Pack(byteStream, obj);
            var streamReader = new StreamReader(byteStream);
            return streamReader.ReadToEnd();
        }

        public static object Deserialize(string obj)
        {
            var context = new SerializationContext
            {
                SerializationMethod = SerializationMethod.Map
            };

            var serializer = MessagePackSerializer.Get<object>(context);
            var byteArray = Encoding.ASCII.GetBytes(obj);
            var stream = new MemoryStream(byteArray);
            return serializer.Unpack(stream);
        }
    }
}