using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EpatecService
{
    public class DataBase
    {
        public static SqlConnection connect()
        {
            SqlConnection connection = new SqlConnection("Server=tcp:epateccr.database.windows.net,1433;Initial Catalog=EPATEC_2;Persist Security Info=False;User ID=juanp1995;Password=Gremory@1212951995;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //SqlConnection connection = new SqlConnection("Data source=PABLO-WINDOWS\\SQL2014; Initial Catalog=EPATEC; User Id=sa; Password=Gremory1212951995");

            return connection;
        }
    }
}
