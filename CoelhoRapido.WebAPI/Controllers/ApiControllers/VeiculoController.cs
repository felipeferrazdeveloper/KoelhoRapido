﻿using KoelhoRapido.Model.Database;
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
                DBConfig.Instance.RepositoryVeiculo.Save(v);
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        [AcceptVerbs("POST")]
        [Route("NewVehicleWithTypeId")]
        public void NewVehicleWithType(Veiculo veiculo, Guid idType)
        {
            try
            {
                var type = DBConfig.Instance.RepositoryTipoVeiculo.FindById(idType);
                veiculo.Type = type;
                DBConfig.Instance.RepositoryVeiculo.Save(veiculo);
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
