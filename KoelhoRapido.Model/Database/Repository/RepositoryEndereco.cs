using KoelhoRapido.Model.Database.Model;
using NHibernate;

namespace KoelhoRapido.Model.Database.Repository
{
    public class RepositoryEndereco : RepositoryBase<Endereco>
    {
        public RepositoryEndereco(ISession session) : base(session)
        {
        }
    }
}
