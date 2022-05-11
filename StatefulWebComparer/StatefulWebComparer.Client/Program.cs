// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using StatefulWebComparer.Client;
using System.Text;
using System.Text.Json.Serialization;

var result = await Test.Invoke("Some Value", "Some Value");
Test.Assert("Inputs were equal.", result);

result = await Test.Invoke("leva nota", "leva bota");
Console.WriteLine(result);

result = await Test.Invoke("leva", "prava");
Test.Assert("Inputs are of different size!", result);

Console.WriteLine("Press any key ...");
Console.ReadKey();