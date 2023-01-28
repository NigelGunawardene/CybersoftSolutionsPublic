using Cybersoft.ApplicationCore.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Cybersoft.ApplicationCore.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderDetailRepository OrderDetails { get; }
        ICartRepository Cart { get; }

        //IAsyncRepository Async { get; }
        //IProjectRepository Projects { get; }
        int Complete();
    }
}






//    https://medium.com/@rudyzio92/net-core-using-the-specification-pattern-alongside-a-generic-repository-318cd4eea4aa

//public interface IUnitOfWork : IDisposable
//{
//    IAsyncRepository<TEntity, TKey> IRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
//    int Complete();
//}

//public class UnitOfWork : IUnitOfWork
//{
//    private readonly CyberSoftContext _context;
//    private Hashtable _repositories;

//    public UnitOfWork(CyberSoftContext context)
//    {
//        _context = context;
//    }

//    public int Complete()
//    {
//        return _context.SaveChanges();
//    }

//    public IAsyncRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
//    {
//        if (_repositories == null)
//            _repositories = new Hashtable();

//        var type = typeof(TEntity).Name;

//        if (!_repositories.ContainsKey(type))
//        {
//            var repositoryType = typeof(Repository<>);

//            var repositoryInstance =
//                Activator.CreateInstance(repositoryType
//                    .MakeGenericType(typeof(TEntity)), _context);

//            _repositories.Add(type, repositoryInstance);
//        }

//        return (IRepository<TEntity>)_repositories[type];
//    }

//    public void Dispose()
//    {
//        _context.Dispose();
//    }
//}
//public class UsersWithRecipesAndIngredientsSpecification : BaseSpecification<User>
//{
//    public UsersWithRecipesAndIngredientsSpecification() : base()
//    {
//        AddInclude(x => x.Recipes);
//        AddInclude($"{nameof(User.Recipes)}.{nameof(Recipe.Ingredients)}");
//    }

//    public UsersWithRecipesAndIngredientsSpecification(int id) : base(x => x.Id == id)
//    {
//        AddInclude(x => x.Recipes);
//        AddInclude($"{nameof(User.Recipes)}.{nameof(Recipe.Ingredients)}");
//    }
//}