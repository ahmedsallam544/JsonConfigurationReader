using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ReadQueryFromJsonFile
{
    class Program
    {
        static void Main(string[] args)
        {

            string realPath = "F:\\Materials\\ITI\\ReadQueryFromJsonFile\\file.json";
            string json = File.ReadAllText(realPath);
            var array = JArray.Parse(json);
            var query = array.FirstOrDefault(p => p["rowId"].Value<string>() == "10")["query"].ToString();
            var connectionString = "Data Source=efg-dbstg07;Initial Catalog=QA-EGY3;Integrated Security=True;MultipleActiveResultSets=True";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                var WE = cmd.ExecuteReader();
                con.Close();
            }

            int savedcount = array.Count(i => i["status"].Value<string>() == "10");
            int failedcount = array.Count(i => i["status"].Value<string>() == "failed");

        }
    }
}
