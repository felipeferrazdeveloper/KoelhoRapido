using KoelhoRapido.Model.Database.Model;
using NHibernate;

namespace KoelhoRapido.Model.Database.Repository
{
    public class RepositoryVolume : RepositoryBase<Volume>
    {
        public RepositoryVolume(ISession session) : base(session)
        {
        }
    }
}
