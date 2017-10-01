namespace GetMinionNames
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
            int villainId = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection("Server=ANTONIO; " + "Database=MinionsDB; " + "Integrated Security=true");

            connection.Open();

            using (connection)
            {
                string query = @"SELECT v.Name AS [VName],
                               m.Name AS [MName],
	                           m.Age AS [MAge]
                               FROM Villains AS v
                               LEFT OUTER JOIN 
	                           VillainsMinions as vm
	                           ON vm.VillainId = v.Id
	                           LEFT OUTER JOIN  
	                           Minions AS m
	                           ON m.Id = vm.MinionsId
                               WHERE v.Id = @vId";
        
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@vId", villainId);

                SqlDataReader reader = command.ExecuteReader();
                int count = 1;
                bool nameWritten = false;
                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No villain with ID {0} exists in the database.", villainId);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            if (!nameWritten)
                            {
                                Console.WriteLine("Villain: " + reader["VName"]);
                                nameWritten = true;
                            }

                            if (reader["MName"] == DBNull.Value)
                            {
                                Console.WriteLine("(no minions)");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("{0}. {1} {2}", count, reader["MName"], reader["MAge"]);
                                ++count;
                            }

                        }
                    }


                }
            }
        }
    }
}
