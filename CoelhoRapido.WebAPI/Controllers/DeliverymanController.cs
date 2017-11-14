using KoelhoRapido.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoelhoRapido.WebAPI.Controllers
{
    [RoutePrefix("api/Deliveryman")]
    public class DeliverymanController : ApiController
    {

        [Route("SetCheckPoint")]
        public void SetCheckPoint(string endereco, Guid idEntrega)
        {
            var entrega = DBConfig.Instance.RepositoryEntrega.FindById(idEntrega);

        }

        [Route("StartDelivery")]
        public void StartDelivery()
        {

        }

        [Route("SetDeliveredStatus")]
        public void SetDeliveredStatus(Boolean status, int attempts)
        {

        }

    }
}
