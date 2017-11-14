
using GoogleMaps.LocationServices;
using KoelhoRapido.Model.Database.Model.Enum;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace KoelhoRapido.Model.Database.Model
{
    public class Endereco
    {
        public virtual Guid Id { get; set; }
        public virtual String Address { get; set; }
        public virtual String Complements { get; set; }
        public virtual String Zip { get; set; }
        public virtual string Country { get; set; }
        public virtual String City { get; set; }
        public virtual String State { get; set; }
        public virtual TipoEnderecoEnum Type { get; set; }
        public virtual Boolean Preferencial { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Entrega Entrega { get; set; }
        public virtual Checkpoint Checkpoint { get; set; }
        public virtual Double Latitude { get; set; }
        public virtual Double Longitude { get; set; }

        public Endereco()
        {

        }

        public Endereco(string address, string city, string state, string country, string zip)
        {
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.Zip = zip;

            var locationService = new GoogleLocationService();
            var point = locationService.GetLatLongFromAddress(address);

            Latitude = point.Latitude;
            Longitude = point.Longitude;
        }

        public Endereco(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            var locationService = new GoogleLocationService();
            var point = locationService.GetAddressFromLatLang(latitude, longitude);

            this.Address = point.Address;
            this.City = point.City;
            this.State = point.State;
            this.Country = point.Country;
            this.Zip = point.Zip;
        }

        public static string DistanceBetween(Endereco e1, Endereco e2)
        {
            var locationService = new GoogleLocationService();
            var address1 = new AddressData()
            {
                Address = e1.Address,
                City = e1.City,
                State = e1.State,
                Country = e1.Country,
                Zip = e1.Zip
            };
            var address2 = new AddressData()
            {
                Address = e2.Address,
                City = e2.City,
                State = e2.State,
                Country = e2.Country,
                Zip = e2.Zip
            };
            try
            {
                var direction = locationService.GetDirections(address1, address2);
                return direction.Distance;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public class EnderecoMap : ClassMapping<Endereco>
    {
        public EnderecoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Address);
            Property(x => x.City);
            Property(x => x.Complements);
            Property(x => x.Zip);
            Property(x => x.City);
            Property(x => x.State);
            Property(x => x.Latitude);
            Property(x => x.Longitude);
            //Tipo é um Enum, não sei se está mapeado corretamente
            Property(x => x.Type);
            //Preferencial é um booleano (não sei se está mapeado corretamente)
            Property(x => x.Preferencial, m =>
            {
                m.Type(NHibernateUtil.Boolean);
            });

            ManyToOne(x => x.Cliente, m =>
            {
                m.Lazy(LazyRelation.Proxy);
                m.Column("idCliente");
            });

            ManyToOne(x => x.Entrega, map =>
            {
                map.Column("idEntrega");
                map.Lazy(LazyRelation.Proxy);
            });

            ManyToOne(x => x.Checkpoint, map =>
            {
                map.Column("idCheckpoint");
                map.Lazy(LazyRelation.Proxy);
            });


        }
    }
}
