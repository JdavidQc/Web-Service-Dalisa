using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebDalisa.libreria
{
    public class SqlServerConnection
    {

        public static SqlConnection getConnection()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["cnx"].ConnectionString);
                
            }catch(Exception e)
            {
                throw e;
            }


            return con;
        }

    }
}