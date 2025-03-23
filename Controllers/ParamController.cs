using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace crm_csh.Controllers;
public class ParamController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl = "http://localhost:8080/api/param";

    public ParamController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [Authorize]
    public IActionResult AlertRateForm()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateAlertRate(string paramValue)
    {
        var content = new StringContent($"paramValue={paramValue}", Encoding.UTF8, "application/x-www-form-urlencoded");

        try
        {
            var response = await _httpClient.PostAsync($"{_apiBaseUrl}/edit-alert-rate", content);

            if (response.IsSuccessStatusCode)
            {
                ViewData["Message"] = "Alert rate updated successfully!";
                return View("AlertRateForm");
            }
            else
            {
                ViewData["Error"] = "Error updating alert rate: " + await response.Content.ReadAsStringAsync();
                return View("AlertRateForm");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            ViewData["Error"] = "An error occurred: " + ex.Message;
            return View("AlertRateForm");
        }
    }

}