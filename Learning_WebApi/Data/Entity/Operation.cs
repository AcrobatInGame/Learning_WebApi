namespace Learning_WebApi.Data.Entity;

public class Operation
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Sum { get; set; }
    public string Category { get; set; }
    public int UserId { get; set; }
    public int DisplayId { get; set; }
}