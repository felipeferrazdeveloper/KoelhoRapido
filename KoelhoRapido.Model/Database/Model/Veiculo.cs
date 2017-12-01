﻿using KoelhoRapido.Model.Database.Model.Enum;
using Newtonsoft.Json;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace KoelhoRapido.Model.Database.Model
{
    [JsonObject]
    public class Veiculo
    {
        #region PROPERTIES
        public virtual int Id { get; set; }
        public virtual String CarPlate { get; set; }
        public virtual TipoVeiculo Type { get; set; }
        public virtual IList<Entrega> Entregas { get; set; }
        public virtual bool Available { get; set; }
        #endregion

        public Veiculo()
        {
            this.Type = new TipoVeiculo();
        }
    }

    public class VeiculoMap : ClassMapping<Veiculo>
    {
        public VeiculoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Increment));
            Property(x => x.CarPlate);

            Bag<Entrega>(x => x.Entregas, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("idVeiculo"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());

            ManyToOne(x => x.Type, m =>
            {
                m.Column("idTipoVeiculo");
                m.Lazy(LazyRelation.NoLazy);
            });
        }
    }
}
