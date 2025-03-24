using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using crm_csh.Models;

namespace crm_csh.Controllers;

public class DashboardController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl = "http://localhost:8080/api/dashboard";

    public DashboardController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        DashboardDto dashboardData = new();

        var response = await _httpClient.GetAsync($"{_apiUrl}/datas1");
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            dashboardData = JsonConvert.DeserializeObject<DashboardDto>(jsonResponse);
        }
        else
        {
            ViewBag.ErrorMessage = "Unable to retrieve dashboard data.";
        }

        return View(dashboardData);
    }
}
