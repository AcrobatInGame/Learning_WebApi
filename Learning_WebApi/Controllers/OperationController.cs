using Learning_WebApi.Data;
using Learning_WebApi.Data.Entity;
using Learning_WebApi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// ReSharper disable All
[ApiController]
[Route("api/[controller]")]
public class OperationController: ControllerBase
{
    private readonly AppDbContext _context;
    
    public OperationController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOperation([FromBody] OperationDto operationDto)
    {
        List<string> operationErrors = OperationValidator.ValidateOperation(operationDto).ToList();
        if (operationErrors.Any())
        {
            return BadRequest(operationErrors);
        }

        int displayId;
        int maxDisplayId = await _context.Operations
            .Where(o => o.UserId == operationDto.UserId) //помощь джемини, надо доразобрать SQL
            .MaxAsync(o => (int?)o.DisplayId) ?? 0;
        displayId = maxDisplayId + 1;
        
        var operationEntity = new Operation {
            Name = operationDto.Name,
            Sum = operationDto.Sum,
            Category = operationDto.Category,
            UserId = operationDto.UserId,
            DisplayId = displayId};
        
        _context.Operations.Add(operationEntity);
        _context.SaveChanges();
        
        return Ok($"Operation was successfully created.\nOperation number is №{operationEntity.DisplayId}");
    }

    [HttpDelete]
    public ActionResult DeleteOperation(int id, int userId)
    {
        var deleteOperation = _context.Operations.FirstOrDefault(op => op.DisplayId == id && op.UserId == userId);
        if (deleteOperation == null)
        {
            return NotFound("Operation with this id doesn't exist");
        }
    
        _context.Operations.Remove(deleteOperation);
        _context.SaveChanges();
        
        return Ok("Operation was successfully deleted");
    }

    
    [HttpGet]
    public ActionResult ShowOperations(int userId) //начинается с последних операций
    {
        if (!_context.Operations.Any(u => u.UserId == userId))
        {
            return NotFound("There is no operations in your account history");
        }
        var answers = new List<string>();
        foreach(var op in  _context.Operations.ToArray().Reverse().Where(op => op.UserId == userId))
        {
            answers.Add($"Name: {op.Name},Sum: {op.Sum}, Category: {op.Category}, Id: {op.DisplayId}");
        }
        
        return Ok(answers);
    }

    [HttpPut]
    public ActionResult ModerateOperation(int id, [FromBody] OperationDto operationDto)
    {
        var operation = _context.Operations.FirstOrDefault(op => op.DisplayId == id);
        if (operation == null)
        {
            return NotFound("There is no operation with this id");
        }
        
        operation.Name = operationDto.Name;
        operation.Sum = operationDto.Sum;
        operation.Category = operationDto.Category;
        
        _context.SaveChanges();
        
        return Ok("Operation was successfully modified");
    }
}