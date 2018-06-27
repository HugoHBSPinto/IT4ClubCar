using IT4ClubCar.IT4ClubCar.Services.DataAccess;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IT4ClubCar.IT4ClubCar.Services.BuggyBar
{
    class BuggyBarService : IBuggyBarService
    {
        private WebService _webService;



        public BuggyBarService(WebService webService)
        {
            _webService = webService;
        }



        public async Task<string> ObterNumeroTelemovel()
        {
            string dataJson = await _webService.GetStringJson("GetNumeroTelemovelBuggyBar");

            JObject numeroTelemovelObject = JObject.Parse(dataJson);

            string numeroTelemovel = numeroTelemovelObject["NumeroTelemovel"].ToString();

            return numeroTelemovel;
        }

    }
}
