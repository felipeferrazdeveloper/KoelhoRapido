using KoelhoRapido.Model.Database;
using KoelhoRapido.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CoelhoRapido.WebAPI.Controllers
{
    public class VeiculoController : ApiController
    {
        // GET: api/Veiculo
        public IEnumerable<Veiculo> Get() =>  DBConfig.Instance.RepositoryVeiculo.FindAll();

        // GET: api/Veiculo/5
        public Veiculo Get(Guid id) => DBConfig.Instance.RepositoryVeiculo.FindById(id);
            
       

        // POST: api/Veiculo
        public void New([FromBody]Veiculo value)
        {

        }

        // PUT: api/Veiculo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Veiculo/5
        public void Delete(int id)
        {

        }
    }
}
