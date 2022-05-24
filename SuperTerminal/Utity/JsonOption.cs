using System.Text.Json;


namespace SuperTerminal.Utity
{
    public static class JsonOption
    {
        private static readonly JsonSerializerOptions jsonSerializerOptions;
        static JsonOption()
        {
            jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = null,//属性名称序列化为其他形式，null为不转换原样输出
                PropertyNameCaseInsensitive = false,//是否区分大小写的比较，false为比较，true为不比较
                MaxDepth = 1024,//对象循环深度，默认32 
                WriteIndented = true,//输出json是否带格式，默认不携带任何格式
            };
            jsonSerializerOptions.Converters.Add(new DatetimeJsonConverter());
        }
        /// <summary>
        /// 字符串转换为任意类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string source)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(source, jsonSerializerOptions);
            }
            catch
            {
                return default(T);
            }

        }
        /// <summary>
        /// 任意类型的序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T t)
        {
            return JsonSerializer.Serialize(t, jsonSerializerOptions);
        }
    }
}
