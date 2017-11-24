using KoelhoRapido.Model.Database.Model;
using NHibernate;

namespace KoelhoRapido.Model.Database.Repository
{
    public class RepositoryTipoVeiculo : RepositoryBase<TipoVeiculo>
    {
        public RepositoryTipoVeiculo(ISession session) : base(session) { }
    }
}
