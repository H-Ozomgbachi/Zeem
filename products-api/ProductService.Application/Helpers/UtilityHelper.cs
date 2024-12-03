namespace ProductService.Application.Helpers
{
    public class UtilityHelper(ILogger<UtilityHelper> logger)
    {
        private readonly ILogger<UtilityHelper> _logger = logger;

        public static string Serializer(object obj)
        {
            JsonSerializerSettings settings = new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
            return JsonConvert.SerializeObject(obj, settings);
        }
        public T? Deserializer<T>(string json)
        {
            T? result = default;
            try
            {
                result = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Deserialization error occurred with - {json} :: {ex.Message}\n");
            }

            return result;
        }

        public static bool ShouldMapMember(object srcMember)
        {
            return srcMember != null && !IsDefaultValue(srcMember);
        }

        static bool IsDefaultValue(object srcMember)
        {
            if (srcMember is string str)
            {
                return string.IsNullOrEmpty(str);
            }

            Type type = srcMember.GetType();

            if (type.IsValueType)
            {
                object? defaultValue = Activator.CreateInstance(type);

                // Avoid bypassing the default value check for boolean values
                if (type == typeof(bool))
                {
                    return false;
                }

                if (defaultValue != null)
                {
                    return defaultValue.Equals(srcMember);
                }
            }

            return false;
        }
    }
}
