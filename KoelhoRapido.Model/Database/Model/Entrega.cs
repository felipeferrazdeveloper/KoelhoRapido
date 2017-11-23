using KoelhoRapido.Model.Database.Model.Enum;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace KoelhoRapido.Model.Database.Model
{

    public class Entrega
    {
        #region PROPERTIES
        public virtual Guid Id { get; set; }
        public virtual Endereco Origem { get; set; }
        public virtual Endereco Destino { get; set; }
        public virtual DateTime DataRetirada { get; set; }
        public virtual DateTime DataEntrega { get; set; }
        public virtual DateTime DataPrazo { get; set; }
        public virtual IList<Volume> Volumes { get; set; }
        public virtual IList<Checkpoint> CheckPoints { get; set; }
        public virtual String Registro { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual int DeliveryAttempt { get; set; }
        
        #endregion

        public Entrega()
        {
            int x = Guid.NewGuid().ToString().GetHashCode();
            x = x >= 0 ? x : -x;
            Registro = Guid.NewGuid().ToString();
        }

        public virtual void Start(Veiculo veiculo, Endereco inicio)
        {
            var checkpoint = new Checkpoint();
            checkpoint.Date = DateTime.Now;
            checkpoint.Local = inicio;

            if(this.Veiculo == null)
                this.Veiculo = veiculo;

            this.CheckPoints.Add(checkpoint);

        }

        public virtual int DeliveryTime() => Endereco.EstimatedTimeBetween(Origem, Destino);

        public virtual double Budget()
        {
            double pricePerKilometer = Veiculo.PriceKm;
            double pricePerMinute = Veiculo.PriceMinute;
            if (pricePerKilometer == 0)
                pricePerKilometer = 1.5;
            if (pricePerMinute == 0)
                pricePerKilometer = 0.21;
            double multiplicador=1;
            switch (Veiculo.Type)
            {
                case TipoVeiculoEnum.Caminhão: multiplicador = 1.6;
                    break;
                case TipoVeiculoEnum.Carro: multiplicador = 1.0;
                    break;
                case TipoVeiculoEnum.Moto: multiplicador = 0.8;
                    break;
            }

            var time = Endereco.TimeBetweenInMinutes(this.Origem, this.Destino);
            var distance = Endereco.DistanceBetween(this.Origem, this.Destino);

            return ((distance * pricePerKilometer) + (time * pricePerMinute))*multiplicador;

        }

        public virtual void AssignVehicle()
        {
            var rnd = new Random();
            this.Veiculo = new Veiculo((TipoVeiculoEnum)rnd.Next(TipoVeiculoEnum.GetNames(typeof(TipoVeiculoEnum)).Length)); 
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
            Property(x => x.DeliveryAttempt);

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
