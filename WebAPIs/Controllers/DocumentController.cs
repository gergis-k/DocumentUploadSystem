using Application.Builders;
using Application.Utilities;
using Core.Constants;
using Core.IRepositories;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIs.Controllers.Bases;
using WebAPIs.DTOs.Requests;
using WebAPIs.DTOs.Responses;

namespace WebAPIs.Controllers;

public sealed class DocumentController : ApiBaseController
{
    private readonly ILogger<DocumentController> logger;
    private readonly IUnitOfWork unitOfWork;
    private readonly IDataProtector dataProtector;

    public DocumentController(
        ILogger<DocumentController> logger,
        IUnitOfWork unitOfWork,
        IDataProtectionProvider dataProtectionProvider)
    {
        this.logger = logger;
        this.unitOfWork = unitOfWork;
        dataProtector = dataProtectionProvider.CreateProtector(nameof(DocumentController));
    }

    [HttpPost]
    public async Task<ActionResult> UploadAsync([FromForm] UploadDocumentDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!Enum.TryParse(typeof(PriorityTypes), model.priority.ToUpper(), out var priority))
        {
            ModelState.AddModelError(nameof(UploadDocumentDto.priority), "Not a valid priority");
            return BadRequest(ModelState);
        }

        var allowedExtensions = await unitOfWork.AllowedExtensionRepository
            .GetAllAsNoTracking()
            .Select(e => e.Extension)
            .ToListAsync();

        var documentBuilder = new DocumentBuilder();

        documentBuilder
            .SetName(model.name)
            .WithDirectory(model.directoryName)
            .WithPriority((PriorityTypes)priority)
            .WithDueDate(model.dueDate);

        foreach (var file in model.files)
        {
            var extension = Path.GetExtension(file.FileName).ToUpper();

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError(file.FileName, "The file extension is not supported");
                continue;
            }

            if (file.Length > 4194304)
            {
                ModelState.AddModelError(file.FileName, "The file is too large");
                continue;
            }

            var uploadedFileBuilder = new UploadedFileBuilder();

            if (await unitOfWork.UploadedFileRepository.GetByFileNameAsync(file.FileName) != null)
            {
                uploadedFileBuilder.SetFileName($"{file.FileName}-{Guid.NewGuid().ToString().Split("-")[1]}");
            }

            uploadedFileBuilder
                .WithFakeName()
                .WithContentType(file.ContentType);

            var builtFile = uploadedFileBuilder.Build();

            var directoryPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                documentBuilder.TemporarilyGetDirectory());

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var fullPath = Path.Combine(directoryPath, builtFile.FakeName);

            try
            {
                using var fileStream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(fileStream);
                documentBuilder.AddFile(builtFile);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ModelState.AddModelError(file.FileName, "An error occurred while uploading the file");
            }
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var builtDocument = documentBuilder.Build();

        var resault = await unitOfWork.DocumentRepository.AddAsync(builtDocument);

        if (resault is null)
            return BadRequest("An error occurred");

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> UpdateAsync(UpdateDocumentDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string normalStringId;
        try
        {
            normalStringId = dataProtector.Unprotect(model.id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return NotFound();
        }

        if (!int.TryParse(normalStringId, out var normalId))
        {
            return NotFound();
        }

        var entity = await unitOfWork.DocumentRepository.GetByIdAsync(normalId);
        if (entity is null)
            return NotFound();

        if (!string.IsNullOrEmpty(model.priority))
        {
            if (!Enum.TryParse(typeof(PriorityTypes), model.priority.ToUpper(), out var priority))
            {
                ModelState.AddModelError(nameof(UploadDocumentDto.priority), "Not a valid priority");
                return BadRequest(ModelState);
            }

            entity.Priority = (PriorityTypes)priority;
        }

        if (model.dueDate is not null)
        {
            entity.DueDate = model.dueDate;
        }

        if (!string.IsNullOrEmpty(model.name))
        {
            entity.Name = NameHelpers.CleanName(model.name);
        }

        var resault = await unitOfWork.DocumentRepository.UpdateAsync(entity);

        if (!resault)
            return BadRequest();

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentDetailsDto>> GetByIdAsync(string? id = null)
    {
        if (!ModelState.IsValid || string.IsNullOrEmpty(id))
            return BadRequest(ModelState);

        string normalStringId;
        try
        {
            normalStringId = dataProtector.Unprotect(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return NotFound();
        }

        if (!int.TryParse(normalStringId, out var normalId))
        {
            return NotFound();
        }

        var entity = await unitOfWork.DocumentRepository.GetByIdAsNoTrackingAsync(normalId);

        if (entity is null)
        {
            return NotFound();
        }

        var documentDetailsDto = new DocumentDetailsDto
        {
            id = id,
            name = entity.Name,
            priority = entity.Priority.ToString(),
            dueDate = entity.DueDate!.Value,
        };

        return Ok(documentDetailsDto);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteByIdAsync(string? id = null)
    {
        if (!ModelState.IsValid || string.IsNullOrEmpty(id))
            return BadRequest(ModelState);

        string normalStringId;
        try
        {
            normalStringId = dataProtector.Unprotect(id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return NotFound();
        }

        if (!int.TryParse(normalStringId, out var normalId))
        {
            return NotFound();
        }

        var entity = await unitOfWork.DocumentRepository.GetByIdAsync(normalId);

        if (entity is null)
        {
            return NotFound();
        }

        var directoryPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            entity.Directory!);

        var resualt = await unitOfWork.DocumentRepository.RemoveAsync(entity);

        if (resualt)
        {
            try
            {
                Directory.Delete(directoryPath, true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        else
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DocumentDetailsDto>>> GetAllAsync()
    {
        var entities = (await unitOfWork.DocumentRepository
            .GetAllAsNoTracking()
            .Include(e => e.UploadedFiles)
            .Select(e => new DocumentDetailsInListDto
            {
                dueDate = e.DueDate!.Value,
                id = dataProtector.Protect(e.Id.ToString()),
                name = e.Name,
                priority = e.Priority.ToString(),
                files = e.UploadedFiles.Select(f => new FileDetailsInListDto
                {
                    fileName = f.FileName!,
                    id = dataProtector.Protect(f.Id.ToString())
                }).ToList(),
            })
            .ToListAsync())
            .AsReadOnly();

        return Ok(entities);
    }
}
