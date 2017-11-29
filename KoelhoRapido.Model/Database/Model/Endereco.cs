
using GoogleMaps.LocationServices;
using KoelhoRapido.Model.Database.Model.Enum;
using Newtonsoft.Json;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Text.RegularExpressions;

namespace KoelhoRapido.Model.Database.Model
{
    [JsonObject]
    public class Endereco
    {
        #region PROPERTIES
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
        #endregion

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

            //var locationService = new GoogleLocationService();
            //var point = locationService.GetLatLongFromAddress(address);

            //Latitude = point.Latitude;
            //Longitude = point.Longitude;
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

        public static double DistanceBetween(Endereco e1, Endereco e2)
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
            double distance, distanceKm=0, distanceMeters=0;

            try
            {
                var direction = locationService.GetDirections(address1, address2);
                var distanceKilometersRegex = Regex.Match(direction.Distance, @"\d+[,.]?\d+ km").Value;
                if(!String.IsNullOrEmpty(distanceKilometersRegex) && distanceKilometersRegex.LastIndexOf(',')>0)
                    distanceKilometersRegex.Remove(distanceKilometersRegex.LastIndexOf(','));
                var distanceMetersRegex = Regex.Match(direction.Distance, @"\d+[,.]?\d+ m").Value;

                if(!String.IsNullOrEmpty(distanceKilometersRegex))
                    distanceKm = Convert.ToDouble(Regex.Match(distanceKilometersRegex, @"\d+[.]?\d+").Value);
                if (!String.IsNullOrEmpty(distanceMetersRegex))
                    distanceMeters = Convert.ToDouble(Regex.Match(distanceMetersRegex, @"\d+[.]?\d+").Value);

                distance = ((distanceKm * 1000)+distanceMeters)/1000;
                return distance;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int EstimatedTimeBetween(Endereco e1, Endereco e2)
        {
            int days = 0;
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
                var dayRegex  = Regex.Match(direction.Duration, @"\d+ [Dd]ay\s?").Value;
                var hourRegex = Regex.Match(direction.Duration, @"\d+ [Hh]our\w?").Value;
                days = Convert.ToInt16(Regex.Match(dayRegex, @"\d+").Value);
                var hours = Convert.ToInt16(Regex.Match(hourRegex, @"\d+").Value);
                days *= 2;
                if (hours > 10)
                    days++;

                if (!address1.State.Equals(address2.State))
                    days++;
            }
            catch (Exception)
            {
                
            }
            return days;
        }

        public static int TimeBetweenInMinutes(Endereco e1, Endereco e2)
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

                var dayRegex = Regex.Match(direction.Duration, @"\d+ [Dd]ay\s?").Value;
                var hourRegex = Regex.Match(direction.Duration, @"\d+ [Hh]our\w?").Value;
                var minutesRegex = Regex.Match(direction.Duration, @"\d+ [Mm]in\w?").Value;

                dayRegex = Regex.Match(dayRegex, @"\d+").Value;
                hourRegex = Regex.Match(hourRegex, @"\d+").Value;
                minutesRegex = Regex.Match(minutesRegex, @"\d+").Value;
                int days = 0;
                int hours = 0;
                int minutes = 0;

                if (!string.IsNullOrEmpty(dayRegex))
                    days = Convert.ToInt16(dayRegex);
                if (!string.IsNullOrEmpty(hourRegex))
                    hours = Convert.ToInt16(hourRegex);
                if (!string.IsNullOrEmpty(minutesRegex))
                    minutes = Convert.ToInt16(minutesRegex);


                return (days * 24 * 60) + (hours * 60) + minutes;

            }
            catch (Exception)
            {
                return -1;
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
