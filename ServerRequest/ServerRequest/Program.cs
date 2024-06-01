using System;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    // without async
    /*static void Main(string[] args)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response_1 = client.GetAsync("https://evilinsult.com/generate_insult.php?lang=en&type=json").Result;
                HttpResponseMessage response_2 = client.GetAsync("https://api.chucknorris.io/jokes/random").Result;
                HttpResponseMessage response_3 = client.GetAsync("https://api.adviceslip.com/advice").Result;
                response_1.EnsureSuccessStatusCode();
                response_2.EnsureSuccessStatusCode();
                response_3.EnsureSuccessStatusCode();

                string jsonString_1 = response_1.Content.ReadAsStringAsync().Result;
                string jsonString_2 = response_2.Content.ReadAsStringAsync().Result;
                string jsonString_3 = response_3.Content.ReadAsStringAsync().Result;
                dynamic json_1 = JsonConvert.DeserializeObject(jsonString_1);
                dynamic json_2 = JsonConvert.DeserializeObject(jsonString_2);
                dynamic json_3 = JsonConvert.DeserializeObject(jsonString_3);

                string value_1 = json_1["insult"];
                string value_2 = json_2["value"];
                dynamic temp = json_3["slip"];
                string value_3 = temp["advice"];

                Console.WriteLine("Оскорбление: " + value_1);
                Console.WriteLine("От Чак Нориса: " + value_2);
                Console.WriteLine("Цитата или совет: " + value_3);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
    }*/

    // with async
    static async Task Main(string[] args)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response_1 = await client.GetAsync("https://evilinsult.com/generate_insult.php?lang=en&type=json");
                HttpResponseMessage response_2 = await client.GetAsync("https://api.chucknorris.io/jokes/random");
                HttpResponseMessage response_3 = await client.GetAsync("https://api.adviceslip.com/advice");
                response_1.EnsureSuccessStatusCode();
                response_2.EnsureSuccessStatusCode();
                response_3.EnsureSuccessStatusCode();

                string jsonString_1 = await response_1.Content.ReadAsStringAsync();
                string jsonString_2 = await response_2.Content.ReadAsStringAsync();
                string jsonString_3 = await response_3.Content.ReadAsStringAsync();
                dynamic json_1 = JsonConvert.DeserializeObject(jsonString_1);
                dynamic json_2 = JsonConvert.DeserializeObject(jsonString_2);
                dynamic json_3 = JsonConvert.DeserializeObject(jsonString_3);

                string value_1 = json_1["insult"];
                string value_2 = json_2["value"];
                dynamic temp = json_3["slip"];
                string value_3 = temp["advice"];

                Console.WriteLine("Оскорбление: " + value_1);
                Console.WriteLine("От Чак Нориса: " + value_2);
                Console.WriteLine("Цитата или совет: " + value_3);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
        }
    }
}