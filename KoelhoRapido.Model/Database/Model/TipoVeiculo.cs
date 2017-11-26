using KoelhoRapido.Model.Database.Model.Enum;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoelhoRapido.Model.Database.Model
{
    public class TipoVeiculo
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual TipoVeiculoEnum Type { get; set; }
        public virtual double PriceMinute { get; set; }
        public virtual double PriceKm { get; set; }
        public virtual double Volume { get; set; }
        public virtual IList<Veiculo> Veiculos { get; set; }
    }

    public class TipoVeiculoMap : ClassMapping<TipoVeiculo>
    {
        public TipoVeiculoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Name);
            Property(x => x.Type);
            Property(x => x.PriceMinute);
            Property(x => x.PriceKm);
            Property(x => x.Volume);
            Bag<Veiculo>(x => x.Veiculos, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idTipoVeiculo"));
                m.Lazy(CollectionLazy.NoLazy);
            },
            r => r.OneToMany());
        }
    }
}
