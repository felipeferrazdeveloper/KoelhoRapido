using KoelhoRapido.Model.Database.Model;
using NHibernate;

namespace KoelhoRapido.Model.Database.Repository
{
    public class RepositoryEntrega : RepositoryBase<Entrega>
    {
        public RepositoryEntrega(ISession session) : base(session)
        {
        }
    }
}
