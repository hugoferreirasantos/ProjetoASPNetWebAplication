using EMDomain;
using System.Collections.Generic;

namespace EMRepository
{
    public abstract class RepositorioAbstrato<T> where T : class
    {
        protected Banco banco;

        public RepositorioAbstrato()
        {
            banco = new Banco();
        }

        public abstract void Add(T entity);
        public abstract void Remove(T entity);
        public abstract void Update(T entity);
        public abstract IEnumerable<T> GetAll();
        public abstract Aluno Get(string predicate, params object[] parameters);

    }
}
