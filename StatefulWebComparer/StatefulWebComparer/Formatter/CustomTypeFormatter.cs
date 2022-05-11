using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using StatefulWebComparer.Model;
using System.Text;

namespace StatefulWebComparer.Formatter
{
    public class CustomTypeFormatter : TextInputFormatter
    {
        public CustomTypeFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/custom"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override bool CanRead(InputFormatterContext context)
        {
            return context.HttpContext.Request.ContentType != null
                && context.HttpContext.Request.ContentType.Contains("application/custom");
        }

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
