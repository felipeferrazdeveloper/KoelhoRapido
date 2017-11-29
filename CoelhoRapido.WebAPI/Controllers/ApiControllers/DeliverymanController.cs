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
    [RoutePrefix("api/Deliveryman")]
    public class DeliverymanController : ApiController
    {

        [Route("SetCheckPoint")]
        public void SetCheckPoint(Endereco endereco, Guid idEntrega)
        {
            var entrega = DBConfig.Instance.RepositoryEntrega.FindById(idEntrega);
            var checkpoint = new Checkpoint();
            checkpoint.Date = DateTime.Now;
            checkpoint.Local = endereco;
            entrega.CheckPoints.Add(checkpoint);
            DBConfig.Instance.RepositoryEntrega.Save(entrega);

        }

        [Route("StartDelivery")]
        public void StartDelivery(Guid idEntrega, Veiculo v, Endereco origem)
        {
            var entrega = DBConfig.Instance.RepositoryEntrega.FindById(idEntrega);
            entrega.Start(v, origem);
            DBConfig.Instance.RepositoryEntrega.Save(entrega);
        }

        [Route("SetDeliveredStatus")]
        public void SetDeliveredStatus(Boolean status, int attempts, Guid idEntrega)
        {
            var entrega = DBConfig.Instance.RepositoryEntrega.FindById(idEntrega);
            if (status)
                entrega.DataEntrega = DateTime.Now;
            else
                entrega.DeliveryAttempt++;
            
            DBConfig.Instance.RepositoryEntrega.Save(entrega);
        }

    }
}
