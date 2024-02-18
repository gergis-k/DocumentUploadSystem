using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace WebAPIs.DTOs.Requests;

public sealed class UpdateDocumentDto
{
    [Required]
    public string id { get; set; } = string.Empty;

    [StringLength(ConfigurationsConstants.Document.NameMaxLength, MinimumLength = ConfigurationsConstants.Document.NameMinLength)]
    public string? name { get; set; } = string.Empty;

    public string? priority { get; set; } = string.Empty;

    public DateTime? dueDate { get; set; }
}