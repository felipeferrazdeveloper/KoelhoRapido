using KoelhoRapido.Model.Database;
using KoelhoRapido.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoelhoRapido.WebAPI.Controllers
{
    [RoutePrefix("api/Delivery")]
    public class DeliveryController : ApiController
    {
        [AcceptVerbs("GET")]
        [Route("Get")]
        public IList<Entrega> GetTypes()
        {
            try
            {
                return DBConfig.Instance.RepositoryEntrega.FindAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        [Route("GetCheckpointsStops")]
        public IList<Checkpoint> GetCheckpoints(Guid idEntrega)
        {
            try
            {
                var entrega = DBConfig.Instance.RepositoryEntrega.FindById(idEntrega);
                return entrega.CheckPoints;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
