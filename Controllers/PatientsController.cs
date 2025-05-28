using Apbd3.Services;
using Microsoft.AspNetCore.Mvc;

namespace Apbd3.Controllers;

[ApiController]
[Route("[controller]")]
public class PatientsController(IDbService dbService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPatientsAsync()
    {
        return Ok(await dbService.GetPatientsAsync());
    }
    
}