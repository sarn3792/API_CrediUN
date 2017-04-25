using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace API_CrediUN.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class OpportunityController : ApiController
    {
        [HttpGet]
        [Route("api/Opportunity/GetByContractName/{contractNumber}")]
        public HttpResponseMessage GetByContractNumber(string contractNumber)
        {
            try
            {
                string result = string.Empty;
                if(result == string.Empty)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "test");
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Opportunity with contract number {0} was not found", contractNumber));
                }
            } catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
