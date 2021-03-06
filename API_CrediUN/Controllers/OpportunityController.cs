﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using API_CrediUN.Models;

namespace API_CrediUN.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    public class OpportunityController : ApiController
    {
        [HttpGet]
        [Route("api/Opportunity/GetByContractNumber/{contractNumber}")]
        public HttpResponseMessage GetByContractNumber(string contractNumber)
        {
            try
            {
                Opportunity op = new Opportunity();
                op.contractNumber = contractNumber;
                OpportunityModel opModel = new OpportunityModel(op);
                string result = "{" + opModel.GetByContractNumber() + "}";

                if(result != string.Empty)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Oportunidad con número de contrato {0} no fue encontrada", contractNumber));
                }
            } catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Opportunity/GetCutOffDate/{idOpportunity}")]
        public HttpResponseMessage GetCutOffDate(string idOpportunity)
        {
            try
            {
                Opportunity op = new Opportunity();
                op.idOpportunity = idOpportunity;
                OpportunityModel opModel = new OpportunityModel(op);
                string result = opModel.GetCutOffDate();

                if (result != string.Empty)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Fecha de corte para la oportunidad {0} no fue encontrada", idOpportunity));
                }
            } catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Opportunity/GetBalance/{idOpportunity}")]
        public HttpResponseMessage GetBalance(string idOpportunity)
        {
            try
            {
                Opportunity op = new Opportunity();
                op.idOpportunity = idOpportunity;
                OpportunityModel opModel = new OpportunityModel(op);
                string result = opModel.GetBalance();

                if (result != string.Empty)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Saldo al día para la oportunidad {0} no fue encontrado", idOpportunity));
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Opportunity/GetNextPaymentDue/{idOpportunity}")]
        public HttpResponseMessage GetNextPaymentDue(string idOpportunity)
        {
            try
            {
                Opportunity op = new Opportunity();
                op.idOpportunity = idOpportunity;
                OpportunityModel opModel = new OpportunityModel(op);
                string result = opModel.GetNextPaymentDue();

                if (result != string.Empty)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Mensualidad actual para la oportunidad {0} no fue encontrada", idOpportunity));
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Opportunity/GetCurrentMonthly/{idOpportunity}")]
        public HttpResponseMessage GetCurrentMonthly(string idOpportunity)
        {
            try
            {
                Opportunity op = new Opportunity();
                op.idOpportunity = idOpportunity;
                OpportunityModel opModel = new OpportunityModel(op);
                string result = opModel.GetCurrentMonthly();

                if (result != string.Empty)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Fecha de corte para la oportunidad {0} no fue encontrada", idOpportunity));
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpGet]
        [Route("api/Opportunity/GetOverdueBalance/{idOpportunity}")]
        public HttpResponseMessage GetOverdueBalance(string idOpportunity)
        {
            try
            {
                Opportunity op = new Opportunity();
                op.idOpportunity = idOpportunity;
                OpportunityModel opModel = new OpportunityModel(op);
                string result = opModel.GetOverdueBalance();

                if (result != string.Empty)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, String.Format("Saldo vencido para la oportunidad {0} no fue encontrado", idOpportunity));
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
