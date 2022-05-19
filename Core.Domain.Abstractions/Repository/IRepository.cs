namespace Signaturit.Domain.Abstractions
{
    public interface IRepository<T> : ICommandRepository<T>, IQueryRepository<T> where T : class { }
}