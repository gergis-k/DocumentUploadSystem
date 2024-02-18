using Core.Constants;
using System.ComponentModel.DataAnnotations;

namespace WebAPIs.DTOs.Requests;

public sealed class UploadDocumentDto
{
    [Required]
    [StringLength(ConfigurationsConstants.Document.NameMaxLength, MinimumLength = ConfigurationsConstants.Document.NameMinLength)]
    public string name { get; set; } = string.Empty;

    [StringLength(ConfigurationsConstants.Document.DirectoryMaxLength, MinimumLength = ConfigurationsConstants.Document.DirectoryMinLength)]
    public string? directoryName { get; set; }

    [Required]
    public string priority { get; set; } = string.Empty;

    [Required]
    public DateTime dueDate { get; set; }

    [Required]
    public ICollection<IFormFile> files { get; set; } = [];
}
