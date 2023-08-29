using eShop.Models;
using System.Text.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Microsoft.IdentityModel.Tokens;

namespace eShop.Helpers
{
    public static class ConvertData<T>
    {
        public static async IAsyncEnumerable<T> ByteArrayToObjectList(byte[]? inputByteArray)
        {
            if (inputByteArray is null)
            {
                yield break;
            }
            IAsyncEnumerable<T?> deserializedList = JsonSerializer.DeserializeAsyncEnumerable<T>(new MemoryStream(inputByteArray));
            
            await foreach (T? _item in deserializedList)
            {
                if (_item is not null) yield return _item;
            }
        }

        public static async Task<byte[]> ObjectListToByteArray(List<T> inputList)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(memoryStream, inputList);
                return memoryStream.ToArray();
            }
                        
        }

        public static async Task<T> ByteArrayToObject(byte[] inputByteArray)
        {
#nullable disable
            return await JsonSerializer.DeserializeAsync<T>(new MemoryStream(inputByteArray));
#nullable enable
        }

        public static async Task<byte[]> ObjectToByteArray(T input)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await JsonSerializer.SerializeAsync(memoryStream, input);
                return memoryStream.ToArray();
            }
        }

    }
}
