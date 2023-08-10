using demo1.model;

namespace demo1.Interface
{
    public interface IRepository<T> where T : Person
    {
        public Task<T> Create(T _object);

        public void Update(T _object);

        public IEnumerable<T> GetAll();

        public T GetById(int Id);

        public void Delete(T _object);
    }

    public class Repository<T> : IRepository<T> where T : Person
    {
        public Task<T> Create(T _object)
        {
            throw new NotImplementedException();
        }

        public void Delete(T _object)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return base.ToString();
        }

        public void Update(T _object)
        {
            throw new NotImplementedException();
        }
    }

}
