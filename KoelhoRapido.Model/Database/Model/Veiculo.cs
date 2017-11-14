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
    public class Veiculo
    {
        public virtual Guid Id { get; set; }
        public virtual TipoVeiculoEnum Type { get; set; }
        public virtual String CarPlate { get; set; }
        public virtual double PriceMinute { get; set; }
        public virtual double PriceKm { get; set; }
        public virtual double Volume { get; set; }
        public virtual IList<Entrega> Entregas { get; set; }
    }

    public class VeiculoMap : ClassMapping<Veiculo>
    {
        public VeiculoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Type);
            Property(x => x.CarPlate);
            Property(x => x.PriceMinute);
            Property(x => x.PriceKm);
            Property(x => x.Volume);

            Bag<Entrega>(x => x.Entregas, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idVeiculo"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());
        }
    }
}
