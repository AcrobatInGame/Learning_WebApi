using System.Text.Json.Serialization;
namespace Learning_WebApi.Model;
public class Operation
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Name { get; set; }
    public double Sum { get; set; }
    public string Category { get; set; }
}