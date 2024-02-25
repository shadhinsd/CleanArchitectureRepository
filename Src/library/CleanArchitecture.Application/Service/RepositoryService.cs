
using AutoMapper;
using CleanArchitecture.Infructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Service;

public class RepositoryService<TEntity, IModel> : IRepositoryService<TEntity, IModel> where TEntity : class, new() where IModel : class
{
    private readonly ApplicationDbContext dbContext;
    private readonly IMapper mapper;

    public RepositoryService(ApplicationDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        Dbset=dbContext.Set<TEntity>();
    }
    public DbSet<TEntity> Dbset { get; }
    public async Task<IModel> DeleteAsync(long id, CancellationToken cancellationToken)
    {
        var entity=await Dbset.FindAsync(id);
        if (entity == null)  return null; 
        Dbset.Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        var deleteModel=mapper.Map<TEntity,IModel>(entity);
        return deleteModel;
    }

    public async Task<IEnumerable<IModel>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entity=await Dbset.AsNoTracking().ToListAsync(cancellationToken);
        if (entity == null)  return null;
        var model = mapper.Map<IEnumerable<IModel>>(entity);
        return model;
    }

    public async Task<IModel> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        var entity = await Dbset.FindAsync(id);
        if (entity == null) return null;
        var map=mapper.Map<TEntity,IModel>(entity);
        return map;
    }

    public async Task<IModel> InsertAsync(IModel model, CancellationToken cancellationToken)
    {
        var entity=mapper.Map<IModel,TEntity>(model);
        if (entity == null) return null;
        Dbset.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        var insertModel = mapper.Map<TEntity, IModel>(entity);
        return insertModel;
    }

    public async Task<IModel> UpdateAsync(long id, IModel model, CancellationToken cancellationToken)
    {
        var entity = await Dbset.FindAsync(id);
        if (entity == null) return null;
        mapper.Map(model,entity );
        Dbset.Update(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        var updateModel = mapper.Map<TEntity, IModel>(entity);
        return updateModel;
    }
}
