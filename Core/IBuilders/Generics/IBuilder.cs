using Core.Entities.Bases;

namespace Core.IBuilders.Generics;

public interface IBuilder<TEntity> where TEntity : BaseEntity
{
    TEntity Build();
}
