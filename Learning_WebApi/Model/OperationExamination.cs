namespace Learning_WebApi.Model;

public class OperationExamination
{
    public static IEnumerable<string> ValidateOperation(OperationDto operationDto)
    {
        if (string.IsNullOrWhiteSpace(operationDto.Category))
        {
            yield return ("Operation category was entered wrongly");
        }

        if (string.IsNullOrWhiteSpace(operationDto.Name))
        {
            yield return ("Operation name was entered wrongly");
        }
        if(operationDto.Sum < 0)
        {
            yield return ("Operation sum can't be negative");
        }
    }
}