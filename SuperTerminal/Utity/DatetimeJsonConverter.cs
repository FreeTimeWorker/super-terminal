using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SuperTerminal.Utity
{
    public class DatetimeJsonConverter : JsonConverter<DateTime>
    {
        public DatetimeJsonConverter()
        {
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                {
                    return date;
                }

            }
            return reader.GetDateTime();
        }

        //大师序列化不能解析毫秒的字段，需要去掉
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
