using KoelhoRapido.Model.Database;
using KoelhoRapido.Model.Database.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CoelhoRapido.WebAPI.Controllers
{
    [RoutePrefix("api/Client")]
    public class ClientController : ApiController
    {
        [AcceptVerbs("GET")]
        [Route("GetList")]
        public IEnumerable<Cliente> Get()
        {
            var list = DBConfig.Instance.RepositoryCliente.FindAll();
            return list;
        }

        [AcceptVerbs("POST")]
        [Route("Get")]
        public object Get(string token)
        {
            try
            {
                var tk = DBConfig.Instance.RepositoryToken.FindByValue(token);
                return tk.Cliente;
            }catch(Exception ex)
            {
                return ex;
            }           
        }

        [AcceptVerbs("POST")]
        [Route("Login")]
        public string Login(Cliente c)
        {
            var cliente = DBConfig.Instance.RepositoryCliente.LoginUser(c.User, c.Password);
            var token = DBConfig.Instance.RepositoryToken.FindByCliente(cliente);
            if (token != null)
                return token.Value;
            else
            {
                var tk = DBConfig.Instance.RepositoryToken.AssignNewToken(cliente);
                return tk.Value;
            }
            return null;
        }

        [AcceptVerbs("POST")]
        [Route("New")]
        public void NewClient(Cliente c)
        {
            DBConfig.Instance.RepositoryCliente.Save(c);
        }

        //[AcceptVerbs("POST")]
        //[Route("NewAddress")]
        //public void NewAddress(Endereco e, string token)
        //{
        //    var c = DBConfig.Instance.RepositoryCliente.FindByToken(token);
        //    c.Enderecos.Add(e);
        //    DBConfig.Instance.RepositoryCliente.Save(c);
        //}

        [AcceptVerbs("DELETE")]
        [Route("Delete")]
        public void Delete(Guid id, string token)
        {
            Cliente c = DBConfig.Instance.RepositoryCliente.FindById(id);
            Token tk = DBConfig.Instance.RepositoryToken.FindByValue(token);
            if (tk.Cliente.Equals(c))
                DBConfig.Instance.RepositoryCliente.Delete(c);
        }

        //public void Deslogar(Guid id, string token)
        //{
        //    DBConfig.Instance.RepositoryCliente.LogoutUser(id, token);
        //}
    }
}
