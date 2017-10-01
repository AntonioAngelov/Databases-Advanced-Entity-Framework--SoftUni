namespace AddMinion
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
            Console.Write("Minion: ");
            string[] input= Console.ReadLine().Split(' ').ToArray();
            Console.Write("Villain: ");
            string vName = Console.ReadLine();

            manageTown(input[2]);
            manageVillain(vName);
            addMInion(input[0], int.Parse(input[1]), input[2]);
            addMinonToVillain(input[0], vName);
        }

        private static void addMinonToVillain(string mName, string vName)
        {
            SqlConnection connection = new SqlConnection("Server=ANTONIO; " + "Database=MinionsDB; " + "Integrated Security=true");
            connection.Open();

            using (connection)
            {
                //getting the Id of the villain
                string getVId = @"SELECT Id
                                 FROM Villains
                                 WHERE Name = @VillainName";
                SqlCommand commandGetVId = new SqlCommand(getVId, connection);
                commandGetVId.Parameters.AddWithValue("@VillainName", vName);

                int villainId = (int)commandGetVId.ExecuteScalar();

                //getting the Id of the minion
                string getMId = @"SELECT Id
                                 FROM Minions
                                 WHERE Name = @MinionName";
                SqlCommand commandGetMId = new SqlCommand(getMId, connection);
                commandGetMId.Parameters.AddWithValue("@MinionName", mName);

   
                int minionId = (int)commandGetMId.ExecuteScalar();

                string addMtoV = @"INSERT INTO VillainsMinions(VillainId, MinionsId)
                                   VALUES (@vId, @mId)";
                SqlCommand command = new SqlCommand(addMtoV, connection);
                command.Parameters.AddWithValue("@vId", villainId);
                command.Parameters.AddWithValue("@mId", minionId);
                command.ExecuteNonQuery();
                Console.WriteLine("Successfully added {0} to be minion of {1}", mName, vName);

            }
        }

        private static void addMInion(string mName, int mAge, string mTown)
        {
            SqlConnection connection = new SqlConnection("Server=ANTONIO; " + "Database=MinionsDB; " + "Integrated Security=true");
            connection.Open();

            using (connection)
            {
                string query = @"SELECT Id
                                 FROM Towns
                                 WHERE Name = @TownName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TownName", mTown);

                int mTownId = (int)command.ExecuteScalar();

             
                string inserMinion = @"INSERT INTO Minions(Name, Age, TownId)
                                         VALUES (@mName, @mAge, @mTownId)";
                SqlCommand insertM = new SqlCommand(inserMinion, connection);
                insertM.Parameters.AddWithValue("@mName", mName);
                insertM.Parameters.AddWithValue("@mAge", mAge);
                insertM.Parameters.AddWithValue("@mTownId", mTownId);
                insertM.ExecuteNonQuery();
                    
                Console.WriteLine("Minion {0} was added to the database!", mName);
            }
         }
        

        private static void manageVillain(string vName)
        {
            SqlConnection connection = new SqlConnection("Server=ANTONIO; " + "Database=MinionsDB; " + "Integrated Security=true");
            connection.Open();

            bool present = true;

            using (connection)
            {
                string query = @"SELECT *
                                 FROM Villains
                                 WHERE Name = @VillainName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@VillainName", vName);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        present = false;
                    }
                }

                if (!present)
                {
                    string inserVillain = @"INSERT INTO Villains(Name, Evilness)
                                                 VALUES (@Name, @evilness)";
                    SqlCommand insertV = new SqlCommand(inserVillain, connection);
                    insertV.Parameters.AddWithValue("@Name", vName);
                    insertV.Parameters.AddWithValue("@evilness", "evil");
                    insertV.ExecuteNonQuery();
                    Console.WriteLine("Villain {0} was added to the database.", vName);
                }
            }
        }

        private static void manageTown(string townName)
        {
            SqlConnection connection = new SqlConnection("Server=ANTONIO; " + "Database=MinionsDB; " + "Integrated Security=true");
            connection.Open();
            bool present = true;
            using (connection)
            {
                string query = @"SELECT *
                                 FROM Towns
                                 WHERE Name = @TownName";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@TownName", townName);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    if (!reader.HasRows)
                    {
                        present = false;
                    }
                }

                if (!present)
                {
                    string inserTown = @"INSERT INTO Towns(Name)
                                                 VALUES (@TownName)";
                    SqlCommand insertT = new SqlCommand(inserTown, connection);
                    insertT.Parameters.AddWithValue("@TownName", townName);
                    insertT.ExecuteNonQuery();
                    Console.WriteLine("Town {0} was added to the database.", townName);
                }
            }
        }
    }
}
