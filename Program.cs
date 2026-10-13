using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PhoneBook.Models;

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5265/");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

HttpResponseMessage response = await client.GetAsync("PhoneBook");

if (response.IsSuccessStatusCode)
{
    var contacts = await response.Content.ReadFromJsonAsync<IEnumerable<PhoneBookEntry>>();
    foreach (PhoneBookEntry p in contacts)
    {
        Console.WriteLine(p);
    }
}

