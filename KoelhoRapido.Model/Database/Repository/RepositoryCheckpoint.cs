
using KoelhoRapido.Model.Database.Model;
using NHibernate;

namespace KoelhoRapido.Model.Database.Repository
{
    public class RepositoryCheckpoint : RepositoryBase<Checkpoint>
    {
        public RepositoryCheckpoint(ISession session) : base(session)
        {
        }
    }
}
