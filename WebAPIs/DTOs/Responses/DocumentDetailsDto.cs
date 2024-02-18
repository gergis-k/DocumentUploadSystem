namespace WebAPIs.DTOs.Responses;

public sealed class DocumentDetailsDto
{
    public string id { get; set; } = string.Empty;

    public string name { get; set; } = string.Empty;

    public string priority { get; set; } = string.Empty;

    public DateTime dueDate { get; set; }
}
