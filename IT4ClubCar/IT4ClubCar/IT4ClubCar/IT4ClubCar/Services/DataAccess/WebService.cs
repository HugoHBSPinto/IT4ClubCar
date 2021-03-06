﻿using IT4ClubCar.IT4ClubCar.Excepcoes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.DataAccess
{
    class WebService : BaseWebService
    {

        public WebService() { }



        /// <summary>
        ///Obtém uma string que contém os dados requisitados podendo ser convertida num JArray.
        /// </summary>
        /// <param name="dadosRequisitados">Dados a serem obtidos</param>
        /// <returns>String que pode ser convertida em json</returns>
        public async Task<string> ObterDadosJson(string dadosRequisitados,CancellationToken token = new CancellationToken())
        {
            return await base.GetStringJson("http://192.168.9.94/it4clubcar/it4clubcarWebService.php?pedido=", dadosRequisitados, token);
        }
    }
}
