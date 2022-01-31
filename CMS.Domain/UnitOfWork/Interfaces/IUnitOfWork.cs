using System;
using System.Threading.Tasks;
using CMS.Domain.Repositories.Interface.EntityTypeRepositories;

namespace CMS.Domain.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAppUserRepository AppUserRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IPageRepository PageRepository { get; }
        IProductRepository ProductRepository { get; }

        // Birden fazla repository' den gelen bilgileri tek seferde db' ye göndereceğiz.
        Task Commit();
        Task ExecuteSqlRaw(string sql,params object[] parameters);
    }
}
