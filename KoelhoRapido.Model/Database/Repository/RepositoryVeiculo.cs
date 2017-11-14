using KoelhoRapido.Model.Database.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoelhoRapido.Model.Database.Repository
{
    public class RepositoryVeiculo : RepositoryBase<Veiculo>
    {
        public RepositoryVeiculo(ISession session) : base(session)
        {
        }
    }
}
