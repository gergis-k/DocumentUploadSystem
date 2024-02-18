using Application.Utilities;
using Core.Constants;
using Core.Entities;
using Core.IBuilders;

namespace Application.Builders;

public sealed class DocumentBuilder : IDocumentBuilder
{
    private readonly Document document = new Document();

    public IDocumentBuilder AddFile(UploadedFile file)
    {
        document.UploadedFiles.Add(file);
        return this;
    }

    public Document Build()
    {
        return document;
    }

    public IDocumentBuilder SetName(string name)
    {
        document.Name = NameHelpers.CleanName(name);
        return this;
    }

    public IDocumentBuilder WithDirectory(string? directory = null)
    {
        if (string.IsNullOrEmpty(directory))
        {
            document.Directory = document.Name;
            return this;
        }

        document.Directory = NameHelpers.CleanName(directory);
        return this;
    }

    public IDocumentBuilder WithDueDate(DateTime dueDate)
    {
        document.DueDate = dueDate;
        return this;
    }

    public IDocumentBuilder WithPriority(PriorityTypes priority)
    {
        document.Priority = priority;
        return this;
    }

    public string TemporarilyGetDirectory()
    {
        if (string.IsNullOrEmpty(document.Directory))
        {
            return NameHelpers.CleanName(document.Name);
        }

        return NameHelpers.CleanName(document.Directory);
    }
}
