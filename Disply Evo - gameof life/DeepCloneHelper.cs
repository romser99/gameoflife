using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Disply_Evo
{
    public static class DeepCloneHelper
    {

        public static T DeepClone<T>(T obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                memoryStream.Position = 0;
                return (T)formatter.Deserialize(memoryStream);
            }
        }
    }

}
