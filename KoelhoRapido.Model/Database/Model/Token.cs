using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;

namespace KoelhoRapido.Model.Database.Model
{
    public class Token
    {
        public virtual Guid Id { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual String Value { get; set; }
        public virtual DateTime ExpirationDateTime { get; set; }

        public Token()
        {
            ExpirationDateTime = DateTime.Now;
        }

        public Token(Cliente cliente)
        {
            this.Cliente = cliente;
            this.RenewToken();
            this.ExpirationDateTime = DateTime.Now.AddMinutes(25);
        }

        public virtual Boolean IsValid()
        {
            var now = new DateTime();
            return now < ExpirationDateTime ? true : false;
        }

        public virtual void RenewToken() => Value = Guid.NewGuid().ToString("n");
    }

    public class TokenMap : ClassMapping<Token>
    {
        public TokenMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));
            Property(x => x.Value);
            Property(x => x.ExpirationDateTime, m => m.Type(NHibernateUtil.DateTime));
            ManyToOne(x => x.Cliente, map =>
            {
                map.Column("idCliente");
                map.Lazy(LazyRelation.NoLazy);
            });
        }
    }
}
