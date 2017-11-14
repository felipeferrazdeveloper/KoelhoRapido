using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace KoelhoRapido.Model.Database.Model
{

    public class Entrega
    {
        public virtual Guid Id { get; set; }
        public virtual Endereco Origem { get; set; }
        public virtual Endereco Destino { get; set; }
        public virtual DateTime DataRetirada { get; set; }
        public virtual DateTime DataEntrega { get; set; }
        public virtual DateTime DataPrazo { get; set; }
        public virtual IList<Volume> Volumes { get; set; }
        public virtual IList<Checkpoint> CheckPoints { get; set; }
        public virtual String Registro
        {
            get
            {
                return Registro;
            }
            set
            {

            }
        }
        public virtual Veiculo Veiculo { get; set; }

        public Entrega()
        {
            Registro = new Random().ToString();
        }

    }

    public class EntregaMap : ClassMapping<Entrega>
    {
        public EntregaMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            OneToOne(x => x.Origem, m =>
            {
                m.Lazy(LazyRelation.NoLazy); // or .NoProxy, .NoLazy
                m.PropertyReference(typeof(Endereco).GetProperty("Entrega"));
            });
            OneToOne(x => x.Destino, m =>
            {
                m.Lazy(LazyRelation.NoLazy); // or .NoProxy, .NoLazy
                m.PropertyReference(typeof(Endereco).GetProperty("Entrega"));
            });
            Property(x => x.DataRetirada, m =>
            {
                m.Type(NHibernateUtil.Date);
            });
            Property(x => x.DataEntrega, m =>
            {
                m.Type(NHibernateUtil.Date);
            });
            Property(x => x.DataPrazo, m =>
            {
                m.Type(NHibernateUtil.Date);
            });
            Property(x => x.Registro);

            ManyToOne(x => x.Veiculo, m =>
            {
                m.Lazy(LazyRelation.Proxy);
                m.Column("idVeiculo");
            });

            Bag<Volume>(x => x.Volumes, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idEntrega"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());


            Bag<Checkpoint>(x => x.CheckPoints, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idCheckpoint"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());
        }
    }
}
