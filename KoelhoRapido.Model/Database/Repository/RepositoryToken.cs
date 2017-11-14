using System;
using KoelhoRapido.Model.Database.Model;
using NHibernate;
using System.Linq;

namespace KoelhoRapido.Model.Database.Repository
{
    public class RepositoryToken : RepositoryBase<Token>
    {
        public RepositoryToken(ISession session) : base(session)
        {
        }

        public Token FindByValue(string value)
        {
            var tk = this.Session.Query<Token>().FirstOrDefault(f => f.Value == value);
            if (tk != null && tk.ExpirationDateTime > DateTime.Now)
                return tk;

            return null;
        }
        

        public Token FindByCliente(Cliente c)
        {
            var tk = this.Session.Query<Token>().FirstOrDefault(f => f.Cliente.Id == c.Id);
            if (tk != null && tk.ExpirationDateTime > DateTime.Now)
                return tk;
            return null;
        }

        public Token AssignNewToken(Cliente c)
        {
            var tk = this.Session.Query<Token>().FirstOrDefault(f => f.Cliente.Id == c.Id);
            if (tk == null || tk.ExpirationDateTime < DateTime.Now)
            {
                tk = new Token(c);
                this.Save(tk);
            }
            return tk;
        }
    }
}
