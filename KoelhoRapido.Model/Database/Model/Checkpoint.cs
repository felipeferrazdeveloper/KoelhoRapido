using Newtonsoft.Json;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoelhoRapido.Model.Database.Model
{
    [JsonObject]
    public class Checkpoint
    {
        public virtual Guid Id { get; set; }
        public virtual Entrega Entrega { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Endereco Local { get; set; }
    }

    public class CheckpointMap: ClassMapping<Checkpoint>
    {
        public CheckpointMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Date, m => m.Type(NHibernateUtil.DateTime));
            OneToOne(x => x.Local, m =>
            {
                m.Lazy(LazyRelation.NoLazy); // or .NoProxy, .NoLazy
                m.PropertyReference(typeof(Endereco).GetProperty("Checkpoint"));
            });
            ManyToOne(x => x.Entrega, m =>
            {
                m.Lazy(LazyRelation.Proxy);
                m.Column("idCheckpoint");
            });
        }
        
    }
}
