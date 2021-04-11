using System.IO;
using MsgPack.Serialization;

namespace Stateflow.Serializers
{
    public static class MessagePack
    {
        public static string Serialize(object obj)
        {
            var context = new SerializationContext
            {
                SerializationMethod = SerializationMethod.Map
            };

            var serializer = MessagePackSerializer.Get<object>(context);

            using var byteStream = new MemoryStream();
            serializer.Pack(byteStream, obj);
            return byteStream.ToString();
        }
    }
}