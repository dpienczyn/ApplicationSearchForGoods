using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceMethod
{
    class DataBase
    {
        private static string constringCBMAX = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;

        private SqlConnection sqlCon;

        public DataBase()
        {
            this.sqlCon = new SqlConnection(constringCBMAX);
        }

        public void ConnectCBMAX()
        {
            if (this.sqlCon.State == System.Data.ConnectionState.Closed)
            {
                try
                {
                    this.sqlCon.Open();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void DisconnectCBMAX()
        {
            if (this.sqlCon.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    this.sqlCon.Close();
                }
                catch (Exception ex)
                {

                }
            }
        }

            public DataTable InvoiceData(string number)
            {
                DataTable dtbl = new DataTable();

                this.ConnectCBMAX();

                if (this.sqlCon.State == System.Data.ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand("SELECT a.adr_Nazwa as Dostawca, dk.dok_NrPelny as NumerDokumentu, dk.dok_NrPelnyOryg as NumerFaktury, dk.dok_DataWyst as DataWystawienia, dk.dok_DataOtrzym as DataOtrzymania, il.ob_CenaWaluta as CenaWaluta, dk.dok_Waluta as Waluta FROM dbo.dok__Dokument as dk LEFT JOIN dbo.dok_Pozycja as il on dk.dok_Id = il.ob_DokHanId LEFT JOIN adr__Ewid a ON dk.dok_PlatnikId = a.adr_IdObiektu WHERE il.ob_TowId in (select tw_id from tw__Towar where tw_Symbol=@number) AND dk.dok_Typ = 1 AND a.adr_TypAdresu = 1; ", this.sqlCon);
                    cmd.Parameters.AddWithValue("@number", number);
                    SqlDataAdapter data = new SqlDataAdapter(cmd);
                    data.Fill(dtbl);
                }
                DisconnectCBMAX();
                return dtbl;
            }
        }
    } 
