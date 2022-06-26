namespace Livros.Data.Interface
{
    public interface Icrud 
    {
        void add<T>(T @params) where T : class;
        void Update<T>(T @params) where T: class;
        void Delete<T>(T @params) where T: class;
        void Delete<T>(T[] @params) where T: class;
        Task<bool> SaveChangesAsync();
    }
}