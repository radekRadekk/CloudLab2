var serverAddress = "https://chmuralab2function.azurewebsites.net/api/examplefunction";
var name = "Radek";

HttpClient client = new HttpClient();
// Call asynchronous network methods in a try/catch block to handle exceptions.
try
{
    HttpResponseMessage response = await client.GetAsync($"{serverAddress}?name={name}");
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();

    Console.WriteLine(responseBody);
}
catch (HttpRequestException e)
{
    Console.WriteLine("\nException Caught!");
    Console.WriteLine("Message :{0} ", e.Message);
}