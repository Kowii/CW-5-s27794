using Apbd3.DTOs;
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientsAsyncById([FromRoute] int id)
    {
        return Ok(await dbService.GetPatientsAsyncById(id));
    }
    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] PrescriptionCreateDto prescriptionData)
    {
        try
        {
            var prescription = await dbService.CreatePrescriptionAsync(prescriptionData);
            return Ok(prescription);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    // [HttpPut]
    // public async Task<IActionResult> UpdatePrescription(int id, [FromBody] PrescriptionCreateDto prescriptionData)
    // {
    //     return Ok(await dbService.UpdatePrescriptionAsync(id, prescriptionData));
    // }
    // [HttpDelete]
    
}