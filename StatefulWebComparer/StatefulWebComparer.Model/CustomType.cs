using System.Text.Json.Serialization;

namespace StatefulWebComparer.Model
{
    public class CustomType
    {
        public CustomType()
        {  }

        public CustomType(string input)
        {
            this.Input = input;
        }

        [JsonPropertyName("input")]
        public string Input { get; set; }
    }
}
