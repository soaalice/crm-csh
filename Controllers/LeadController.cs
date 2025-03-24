using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using crm_csh.Models;

namespace crm_csh.Controllers;

public class LeadController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "http://localhost:8080/api/lead";

    public LeadController (HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [Authorize]
    public async Task<IActionResult> Leads()
    {
        var response = await _httpClient.GetAsync($"{_apiUrl}/leads");
        if (response.IsSuccessStatusCode)
        {
            if (response.Content.Headers.ContentType.MediaType == "application/json")
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var leads = JsonConvert.DeserializeObject<List<Lead>>(jsonResponse);
                return View("Leads", leads);
            }
            else
            {
                ViewBag.ErrorMessage = "Unexpected response format from the API.";
                return View("Leads", new List<Lead>());
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
            return View("Leads", new List<Lead>());
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateLead(int id, double expense)
    {
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("expense", expense.ToString())
        });

        var response = await _httpClient.PostAsync($"{_apiUrl}/update/{id}", content);
        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Lead #" + id + " updated successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update the lead.";
        }
        return RedirectToAction("Leads");
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeleteLead(int id)
    {
        var response = await _httpClient.PostAsync($"{_apiUrl}/delete/{id}", null);
        if (response.IsSuccessStatusCode)
        {
            TempData["SuccessMessage"] = "Lead #" + id + " deleted successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to delete the lead.";
        }
        return RedirectToAction("Leads");
    }
}