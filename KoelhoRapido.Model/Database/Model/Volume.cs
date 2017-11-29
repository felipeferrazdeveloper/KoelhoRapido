using Newtonsoft.Json;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace KoelhoRapido.Model.Database.Model
{
    [JsonObject]
    public class Volume
    {   
        public virtual Guid Id { get; set; }
        public virtual String Descricao { get; set; }
        public virtual Double ValorDeclarado { get; set; }
        public virtual Double Peso { get; set; }

        /// <summary>
        /// Centimeters Height
        /// </summary>
        public virtual Double Altura { get; set; }
        public virtual Double Largura { get; set; }
        public virtual Double Profundidade { get; set; }
        public virtual Entrega Entrega { get; set; }

        /// <summary>
        /// This function returns an integer for each modality of deliver 
        /// its conforms with TipoVeiculoUtilizadoEnum
        /// </summary>
        /// <returns>0 to motorcycle deliver
        /// 1 to car deliver
        /// 2 to truck deliver</returns>
        public virtual int PorteVolume()
        {
            if (Altura > 110 || Largura > 105 || Profundidade > 150)
            {
                return 2;
            }
            else if (Altura <= 44 && Largura <= 50 && Profundidade <= 43)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public virtual double Vol()
        {
            return Altura * Largura * Profundidade;
        }
    }

    public class VolumeMap : ClassMapping<Volume>
    {
        public VolumeMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Descricao);
            Property(x => x.ValorDeclarado);
            Property(x => x.Peso);
            Property(x => x.Altura);
            Property(x => x.Largura);
            Property(x => x.Profundidade);

            ManyToOne(x => x.Entrega, m =>
            {
                m.Column("idEntrega");
            });

        }
    }
}
