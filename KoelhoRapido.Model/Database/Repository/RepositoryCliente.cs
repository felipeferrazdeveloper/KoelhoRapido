using KoelhoRapido.Model.Database.Model;
using NHibernate;
using System;
using System.Linq;

namespace KoelhoRapido.Model.Database.Repository
{
    public class RepositoryCliente : RepositoryBase<Cliente>
    {
        public RepositoryCliente(ISession session) : base(session)
        {
        }

        //public Cliente FindByToken(string token)
        //{
        //    return this.Session.Query<Cliente>().FirstOrDefault(f => f.Token == token);
        //}

        public Cliente LoginUser(string user, string password)
        {
            var c = this.Session.Query<Cliente>().FirstOrDefault(f => f.User == user && f.Password == password);
            if (c != null)
            {
                c.LastAuth = DateTime.Now;
                this.Save(c);
            }

            return c;
        }

        ///// <summary>
        ///// Logout user 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns>true for success</returns>
        //public void LogoutUser(Guid id, string token)
        //{
        //    var c = this.Session.Query<Cliente>().FirstOrDefault(f => f.Id == id && f.Token == token);
        //    if (c != null)
        //        c.Token = null;

        //    this.Save(c);
        //}

    }
}
