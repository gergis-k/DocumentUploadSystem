using Core.Constants;
using Core.Entities;
using Core.IBuilders.Generics;

namespace Core.IBuilders;

public interface IDocumentBuilder : IBuilder<Document>
{
    IDocumentBuilder SetName(string name);

    IDocumentBuilder WithDirectory(string? directory = null);

    IDocumentBuilder WithPriority(PriorityTypes priority);

    IDocumentBuilder WithDueDate(DateTime dueDate);

    IDocumentBuilder AddFile(UploadedFile file);

    string TemporarilyGetDirectory();
}
