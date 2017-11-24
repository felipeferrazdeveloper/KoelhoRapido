using KoelhoRapido.Model.Database;
using KoelhoRapido.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CoelhoRapido.WebAPI.Controllers
{
    [RoutePrefix("api/Vehicles")]
    public class VeiculoController : ApiController
    {
        [AcceptVerbs("POST")]
        [Route("NewType")]
        public void NewType(TipoVeiculo type)
        {
            try
            {
                DBConfig.Instance.RepositoryTipoVeiculo.Save(type);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        [Route("GetTypes")]
        public IList<TipoVeiculo> GetTypes()
        {
            try
            {
                return DBConfig.Instance.RepositoryTipoVeiculo.FindAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("POST")]
        [Route("NewVehicle")]
        public void NewVehicle(Veiculo v)
        {
            try
            {
                DBConfig.Instance.RepositoryVeiculo.Save(v);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("POST")]
        [Route("NewVehicleWithTypeId")]
        public void NewVehicleWithType(Veiculo v, Guid idType)
        {
            try
            {
                var type = DBConfig.Instance.RepositoryTipoVeiculo.FindById(idType);
                v.Type = type;
                DBConfig.Instance.RepositoryVeiculo.Save(v);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        [Route("GetVehicles")]
        public IList<Veiculo> GetVehicles()
        {
            try
            {
                return DBConfig.Instance.RepositoryVeiculo.FindAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
