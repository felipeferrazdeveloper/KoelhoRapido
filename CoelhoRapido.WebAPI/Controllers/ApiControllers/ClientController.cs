using KoelhoRapido.Model.Database;
using KoelhoRapido.Model.Database.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CoelhoRapido.WebAPI.Controllers
{
    [RoutePrefix("api/Client")]
    [Serializable]
    public class ClientController : ApiController
    {
        #region Client CRUD 
        /// <summary>
        /// GET method who returns all clientes on database
        /// </summary>
        /// <returns>List<Cliente></returns>
        [AcceptVerbs("GET")]
        [Route("GetList")]
        public IEnumerable<Cliente> Get() => DBConfig.Instance.RepositoryCliente.FindAll() ?? new List<Cliente>();

        /// <summary>
        /// POST method that receive a token and Return a user if it's logged and token is valid
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Cliente</returns>
        [AcceptVerbs("POST")]
        [Route("Get")]
        public object Get(string token)
        {
            try
            {
                var tk = DBConfig.Instance.RepositoryToken.FindByValue(token);
                return tk.Cliente;
            } catch (Exception)
            {
                return new Exception("Token Inválido");
            }
        }

        /// <summary>
        /// Receive a Cliente by post with user and password params 
        /// </summary>
        /// <param name="c"></param>
        /// <returns>Token Value</returns>
        [AcceptVerbs("POST")]
        [Route("Login")]
        public object Login(Cliente c)
        {
            try
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
            } catch (Exception ex)
            {
                return new Exception("Não foi possível logar", ex);
            }

        }


        [AcceptVerbs("POST")]
        [Route("New")]
        public void NewClient(Cliente c)
        {
            if (c != null)
                DBConfig.Instance.RepositoryCliente.Save(c);
        }

        /// <summary>
        /// Receive user id and valid token to delete it
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        [AcceptVerbs("DELETE")]
        [Route("Delete")]
        public void Delete(Guid id, string token)
        {
            Cliente c = DBConfig.Instance.RepositoryCliente.FindById(id);
            Token tk = DBConfig.Instance.RepositoryToken.FindByValue(token);
            if (tk != null && c != null)
                if (tk.Cliente.Id.Equals(c.Id))
                {
                    DBConfig.Instance.RepositoryToken.Delete(tk);
                    DBConfig.Instance.RepositoryCliente.Delete(c);
                }
        }

        [AcceptVerbs("POST")]
        [Route("Logout")]
        public void Deslogar(string token)
        {
            Token tk = DBConfig.Instance.RepositoryToken.FindByValue(token);
            if (tk != null)
                DBConfig.Instance.RepositoryToken.Delete(tk);
        }

        [AcceptVerbs("POST")]
        [Route("NewAddress")]
        public void NewAddress(Endereco e, string token)
        {
            var tk = DBConfig.Instance.RepositoryToken.FindByValue(token);
            if (tk == null)
                throw new Exception("Token Invalido");
            Cliente c = tk.Cliente;
            c.Enderecos.Add(e);
            DBConfig.Instance.RepositoryCliente.Save(c);
        }
        #endregion

        #region Client Services

        [AcceptVerbs("POST")]
        [Route("RequestDeliveryBudget")]
        public ValueTuple<double, int> RequestDeliveryBudget(Entrega e)
        {
            int prazo = 0;
            double budget = 0;
            if (e.Origem != null && e.Destino != null)
            {
                prazo = e.DeliveryTime();
                budget = 0; Math.Round(e.Budget(), 2);
            }

            return (budget: budget, prazo: prazo);
        }

        #endregion

    }
}
