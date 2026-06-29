using Learning_WebApi.Data;
using Learning_WebApi.Data.Entity;
using Learning_WebApi.Model;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable All

namespace Learning_WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationController: ControllerBase
{
    private readonly AppDbContext _context;
    
    public OperationController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpPost]
    public ActionResult CreateOperation([FromBody] OperationDto operationDto)
    {
        List<string> operationErrors = OperationExamination.ValidateOperation(operationDto).ToList();
        if (operationErrors.Any())
        {
            return BadRequest(operationErrors);
        }
        
        var operationEntity = new Operation {
            Name = operationDto.Name,
            Sum = operationDto.Sum,
            Category = operationDto.Category };
        
        _context.Operations.Add(operationEntity);
        _context.SaveChanges();
        
        return Ok($"Operation was successfully created.\nOperation number is №{operationEntity.Id}");
    }

    [HttpDelete]
    public ActionResult DeleteOperation(int id)
    {
        var deleteOperation = _context.Operations.FirstOrDefault(op=> op.Id == id);
        if (deleteOperation == null)
        {
            return NotFound("Operation with this id doesn't exist");
        }
    
        _context.Operations.Remove(deleteOperation);
        _context.SaveChanges();
        
        return Ok("Operation was successfully deleted");
    }
    
    [HttpGet]
    public ActionResult ShowOperations() //начинается с последних операций
    {
        if (!_context.Operations.Any())
        {
            return NotFound("There is no operations in your account history");
        }
        
        var answers = new List<string>();
        foreach(var op in  _context.Operations.ToArray().Reverse())
        {
            answers.Add($"Name: {op.Name},Sum: {op.Sum}, Category: {op.Category}, Id: {op.Id}");
        }
        
        return Ok(answers);
    }
    
    [HttpPut]
    public ActionResult ModerateOperation(int id, [FromBody] OperationDto operationDto)
    {
        var operation = _context.Operations.FirstOrDefault(op => op.Id == id);
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