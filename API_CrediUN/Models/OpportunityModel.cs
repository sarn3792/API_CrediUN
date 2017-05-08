using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace API_CrediUN.Models
{
    public class OpportunityModel
    {
        private Opportunity opportunity;
        public OpportunityModel(Opportunity opportunity)
        {
            this.opportunity = opportunity;
        }

        public string GetByContractNumber()
        {
            try
            {
                String query = String.Format(@"SELECT o.new_NumContrato 'NumeroContrato' , o.OpportunityId 'Oportunidad'
                                            FROM CREDIJAL_MSCRM.dbo.Opportunity o
                                            WHERE o.new_NumContrato = '{0}'", opportunity.contractNumber);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);
                if(aux.Rows.Count > 0)
                {
                    return aux.Rows[0]["Oportunidad"].ToString();
                }

                return string.Empty;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCutOffDate()
        {
            try
            {
                String query = String.Format(@"SELECT TOP 1 CONVERT(varchar(2),AD.DueDate, 103) + '/' + CONVERT(varchar(2), AD.DueDate, 101) 'DiaDeCorte' , po.PKOportunity 'Oportunidad'
                                            FROM PaymentsOportunity po
                                            INNER JOIN PaymentsCustomer pc ON PO.FKCliente = PC.PKCustomer
                                            INNER JOIN ARDoc AD ON AD.CustId = PC.PKCustomerSL
                                            WHERE po.PKOportunity = '{0}' AND AD.DocType IN ('IN','DM','FI','NC','AD') --AND AD.DocBal > 0
                                            ORDER BY AD.DueDate DESC", opportunity.idOpportunity);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);
                if (aux.Rows.Count > 0)
                {
                    return aux.Rows[0]["DiaDeCorte"].ToString();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetBalance()
        {
            try
            {
                String query = String.Format(@"SELECT SUM(CASE WHEN AD.DocType IN ('IN','DM','FI','NC','AD') THEN 1 ELSE -1 END * AD.DocBal) 'SaldoDelDia', PO.PKOportunity 'Oportunidad'
                                            FROM PaymentsOportunity po
                                            INNER JOIN PaymentsCustomer pc ON PO.FKCliente = PC.PKCustomer
                                            INNER JOIN ARDoc AD ON AD.CustId = PC.PKCustomerSL
                                            WHERE po.PKOportunity = '{0}' AND AD.DocBal > 0
                                            GROUP BY PO.PKOportunity", opportunity.idOpportunity);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);
                if (aux.Rows.Count > 0)
                {
                    return aux.Rows[0]["SaldoDelDia"].ToString();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetNextPaymentDue()
        {
            try
            {
                String query = String.Format(@"SELECT ad.DueDate 'ProximaFechaAVencer', po.PKOportunity 'Oportunidad'
                                            FROM PaymentsOportunity po
                                            INNER JOIN PaymentsCustomer pc ON PO.FKCliente = PC.PKCustomer
                                            INNER JOIN ARDoc AD ON AD.CustId = PC.PKCustomerSL
                                            INNER JOIN (SELECT MIN(AD2.DueDate) 'Fecha', AD2.User5 
			                                            FROM ARDoc AD2 
			                                            WHERE AD2.DocBal > 0 AND AD2.DueDate >= GETDATE() AND AD2.DocType IN ('IN','DM','FI','NC','AD')
			                                            GROUP BY AD2.User5) AD2 ON AD2.Fecha = AD.DueDate AND AD2.User5 = AD.User5
                                            WHERE po.PKOportunity = '{0}'", opportunity.idOpportunity);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);
                if (aux.Rows.Count > 0)
                {
                    DateTime date = Convert.ToDateTime(aux.Rows[0]["ProximaFechaAVencer"].ToString());
                    return date.ToString("dd/MM/yyyy");
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetCurrentMonthly()
        {
            try
            {
                String query = String.Format(@"SELECT AD.InstallNbr 'MensualidadActual', po.NumeroMaxMensualidades 'TotalMensualidades', po.PKOportunity 'Oportunidad'
                                            FROM PaymentsOportunity po
                                            INNER JOIN PaymentsCustomer pc ON PO.FKCliente = PC.PKCustomer
                                            INNER JOIN ARDoc AD ON AD.CustId = PC.PKCustomerSL
                                            WHERE po.PKOportunity = '{0}' AND AD.DocType IN ('IN','DM','FI','NC','AD') AND AD.DocBal > 0
		                                            AND AD.DueDate BETWEEN GETDATE() AND DATEADD(dd, 30, GETDATE())", opportunity.idOpportunity);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);
                if (aux.Rows.Count > 0)
                {
                    return string.Format("{0}/{1}",aux.Rows[0]["MensualidadActual"].ToString(), aux.Rows[0]["TotalMensualidades"].ToString());
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetOverdueBalance()
        {
            try
            {
                String query = String.Format(@"SELECT SUM(AD.DocBal) 'SaldoVencido', COUNT(*) 'NumDocumentosVencidos', PO.PKOportunity 'Oportunidad'
                                            FROM PaymentsOportunity po
                                            INNER JOIN PaymentsCustomer pc ON PO.FKCliente = PC.PKCustomer
                                            INNER JOIN ARDoc AD ON AD.CustId = PC.PKCustomerSL
                                            WHERE po.PKOportunity = '{0}' AND  AD.DocBal > 0 AND GETDATE() > AD.DueDate AND AD.DocType  IN('IN', 'DM', 'FI', 'NC', 'AD')
                                            GROUP BY PO.PKOportunity", opportunity.idOpportunity);
                DataBaseSettings db = new DataBaseSettings();
                DataTable aux = db.GetDataTable(query);
                if (aux.Rows.Count > 0)
                {
                    return aux.Rows[0]["SaldoVencido"].ToString();
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class Opportunity
    {
        public string idOpportunity;
        public string contractNumber;

        public Opportunity()
        {

        }
    }
}