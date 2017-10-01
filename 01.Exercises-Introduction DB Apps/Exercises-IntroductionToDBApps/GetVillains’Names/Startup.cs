using System.Data.SqlClient;
using System.Xml;

namespace GetVillains_Names
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Data.SqlClient;


    class Startup
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection("Server=ANTONIO; " + "Database=MinionsDB; " + "Integrated Security=true");

            connection.Open();

            using (connection)
            {
                string query = @"SELECT v.Name,
                               COUNT(vm.MinionsId) AS[Count]
                               FROM Villains AS v
                               INNER JOIN
                               VillainsMinions as vm
                               ON
                               v.Id = vm.VillainId
                               GROUP BY v.Name
                               HAVING  COUNT(vm.MinionsId) > 3
                               ORDER BY COUNT(vm.MinionsId) DESC";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["Name"] + " " + reader["Count"]);
                    }
                }
            }
        }
    }
}
