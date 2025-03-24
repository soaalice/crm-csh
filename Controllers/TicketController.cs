using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using crm_csh.Models;

namespace crm_csh.Controllers;

public class TicketController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "http://localhost:8080/api/ticket";

    public TicketController (HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [Authorize]
    public async Task<IActionResult> Tickets()
    {
        var response = await _httpClient.GetAsync($"{_apiUrl}/tickets");
        if (response.IsSuccessStatusCode)
        {
            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonResponse);
                return View("Tickets", tickets);
            }
            else
            {
                ViewBag.ErrorMessage = "Unexpected response format from the API.";
                return View("Tickets", new List<Ticket>());
            }

        }
        else
        {
            string errorMessage = response.StatusCode switch
            {
                System.Net.HttpStatusCode.Unauthorized => "You must be logged in to perform this action.",
                System.Net.HttpStatusCode.Forbidden => "You are not authorized to perform this action.",
                System.Net.HttpStatusCode.BadRequest => "There was an error with your request.",
                _ => "An unexpected error occurred."
            };
            ViewBag.ErrorMessage = errorMessage;
            return View("Tickets", new List<Ticket>());
        }
    }
}
