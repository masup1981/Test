using Newtonsoft.Json;
using StatefulWebComparer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatefulWebComparer.Client
{
    internal class Test
    {
        public async static Task<string> Invoke(string left, string right, string id = "id")
        {
            // Must run everything under one client to maintain session, otherwise the data to diff would not survive the endpoint call.
            // Data is stored in Session, to maintain data over session lifetime it would require persistent storage - databes, filesystem, ...
            var client = new HttpClient() { BaseAddress = new Uri("http://localhost:5292") };

            var leftEncoded = EncodeData(left);
            var rightEncoded = EncodeData(right);

            await client.PostAsync($"v1/diff/{id}/left", new StringContent(leftEncoded, Encoding.UTF8, "application/custom"));
            await client.PostAsync($"v1/diff/{id}/right", new StringContent(rightEncoded, Encoding.UTF8, "application/custom"));

            var result = await client.GetAsync($"v1/diff/{id}");
            return await result.Content.ReadAsStringAsync();
        }

        // Compare values and print out message.
        public static void Assert(string expected, string result)
        {
            if (expected != result)
            {
                Console.WriteLine($"Expected value: '{expected}', current value '{result}'.");
            }
            else
            {
                Console.WriteLine($"Expected value received: '{result}'.");
            }
        }

        // Encode data as Json int Base64
        private static string EncodeData(string input)
        {
            var inputObject = new CustomType(input);
            var inputJson = JsonConvert.SerializeObject(inputObject);

            var b64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(inputJson));

            return b64String;
        }
    }
}
