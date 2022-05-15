using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using StatefulWebComparer.Model;
using System.Text;

namespace StatefulWebComparer.Formatter
{
    // Class must inherit from TextInputFormatter to allow formatting of incomming data in controller
    public class CustomTypeFormatter : TextInputFormatter
    {
        public CustomTypeFormatter()
        {
            // Register type
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/custom"));

            // Register incomming message encodding
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        // Can read data if content type is application/custom
        public override bool CanRead(InputFormatterContext context)
        {
            return context.HttpContext.Request.ContentType != null
                && context.HttpContext.Request.ContentType.Contains("application/custom");
        }

        // Process incomming message and return decoded data
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var httpContext = context.HttpContext;
            var body = httpContext.Request.Body;

            var stream = new StreamReader(body);
            var bodyString = await stream.ReadToEndAsync();

            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(bodyString));
            var input = JsonConvert.DeserializeObject<CustomType>(decoded);

            return await InputFormatterResult.SuccessAsync(input);
        }
    }
}
