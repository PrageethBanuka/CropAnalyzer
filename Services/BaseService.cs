using Microsoft.Data.Sqlite;

public abstract class BaseService<T>
{
    protected SqliteConnection GetConnection()
    {
        return DatabaseHelper.GetConnection();
    }

    public abstract void Add(T entity);
    public abstract T GetById(int id);
    public abstract void Update(T entity);
    public abstract void Delete(int id);
}
