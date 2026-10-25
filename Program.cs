using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using PhoneBook.Models;

HttpClient client = new HttpClient();
client.BaseAddress = new Uri("http://localhost:5153/");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

GetAllContacts(client).Wait();
GetByName(client, "Mickey Joe").Wait();
GetByNumber(client, "01555999").Wait();

PhoneBookEntry entry = new PhoneBookEntry() {Number="089-1235676", 
            Name="Molly Malone", Address="78 Chancellors Road" };
AddAPhoneBookEntry(client, entry).Wait();



static async Task GetAllContacts(HttpClient _client)
{
    HttpResponseMessage response = await _client.GetAsync("Contacts");
    if (response.IsSuccessStatusCode)
    {
        var contacts = await response.Content.ReadFromJsonAsync<IEnumerable<PhoneBookEntry>>();
        foreach (PhoneBookEntry p in contacts)
        {
            Console.WriteLine(p);
        }    
    }
    
}

static async Task GetByName(HttpClient _client, string _name)
{
    HttpResponseMessage response = await _client.GetAsync("Contacts/"+_name);
    if (response.IsSuccessStatusCode)
    {
        var returnedEntries = await response.Content.ReadFromJsonAsync<IEnumerable<PhoneBookEntry>>();

        Console.WriteLine("By Name...");
        foreach (PhoneBookEntry pbe in returnedEntries)
        {
            Console.WriteLine(pbe.Name + " | " + pbe.Number);
        }
    }
}

static async Task GetByNumber(HttpClient _client, string _number)
{
    HttpResponseMessage response = await _client.GetAsync("Contacts/GetByNumber/"+_number);
    if (response.IsSuccessStatusCode)
    {
        var returnedEntries = await response.Content.ReadFromJsonAsync<IEnumerable<PhoneBookEntry>>();

        Console.WriteLine("By Number...");

        foreach (PhoneBookEntry pbe in returnedEntries)
        {
            Console.WriteLine(pbe.Name + " | " + pbe.Number);
        }
    }
}

static async Task AddAPhoneBookEntry(HttpClient _client, PhoneBookEntry payload)
{
    HttpResponseMessage response = await _client.PostAsJsonAsync("Contacts", payload);
    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("Record added!");
    }
    else
    {
        Console.WriteLine(response.StatusCode.ToString());
    }
}