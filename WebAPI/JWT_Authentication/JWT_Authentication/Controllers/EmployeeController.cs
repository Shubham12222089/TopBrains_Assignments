using JWT_Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/employees")]
[ApiController]
[Authorize]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _repo;

    public EmployeeController(IEmployeeRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _repo.GetAllEmployeesAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Employee emp)
    {
        await _repo.AddEmployeeAsync(emp);
        return Ok(emp);
    }
}