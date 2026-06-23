using Learning_WebApi.Model;
using Microsoft.AspNetCore.Mvc;
// ReSharper disable All

namespace Learning_WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OperationController: ControllerBase
{
    private static List<Operation> _operations = [];

    [HttpPost]
    public ActionResult CreateOperation([FromBody] Operation operation)
    {
        int id = _operations.Count + 1;
        operation.Id = id;
        _operations.Add(operation);
        return Ok($"Operation was successfully created.\nOperation number is №{operation.Id}");
    }

    [HttpDelete]
    public ActionResult DeleteOperation(int id)
    {
        var deleteOperation = _operations.FirstOrDefault(op=> op.Id == id);

        if (deleteOperation == null)
        {
            return NotFound("Operation with this id doesn't exist");
        }

        int counter = 1;
        foreach (var operation in _operations)
        {
            operation.Id = counter;
            counter++;
        }

        _operations.Remove(deleteOperation);
        return Ok("Operation was successfully deleted");
    }

    [HttpGet]
    public ActionResult ShowOperations() //начинается с последних операций
    {
        if (_operations.Count == 0)
        {
            return NotFound("There is no operations in your account history");
        }
        var answers = new List<string>();
        
        for (var i = _operations.Count - 1; i >= 0; i--)
        {
            var op = _operations[i];
            answers.Add($"Name: {op.Name},Sum: {op.Sum}, Category: {op.Category}, Id: {op.Id}");
        }

        return Ok(answers);
    }
    
    [HttpPut]
    public ActionResult ModerateOperation(int id, [FromBody] Operation operation)
    {
        if (id - 1 > _operations.Count || id < 0)
        {
            return NotFound("There is no operation with this id");
        }
        _operations[id-1] = operation;
        _operations[id-1].Id = id;
        return Ok("Operation was successfully modified");
    }
}