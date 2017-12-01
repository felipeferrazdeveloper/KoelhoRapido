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
        public IEnumerable<TipoVeiculo> GetTypes()
        {
            try
            {
                var retorno = DBConfig.Instance.RepositoryTipoVeiculo.FindAll();
                return retorno;
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
                if (v.Type != null)
                    DBConfig.Instance.RepositoryVeiculo.Save(v);
                else
                    throw new Exception("Vehicle must have a type!");

            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("POST")]
        [Route("NewVehicleWithTypeId")]
        public void NewVehicleWithType(Veiculo veiculo, int idType)
        {
            try
            {
                var type = DBConfig.Instance.RepositoryTipoVeiculo.FindById(idType);
                veiculo.Type = type;
                this.NewVehicle(veiculo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("GET")]
        [Route("GetVehicles")]
        public IEnumerable<Veiculo> GetVehicles()
        {
            try
            {
                var veiculos = DBConfig.Instance.RepositoryVeiculo.FindAll();
                return veiculos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
