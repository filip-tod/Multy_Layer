using Example.Model;
using Example.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Repository
{

        public class LeagueRepository : ILeagueRepository
        {
            private static readonly string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=playerdb;";
        

            public List<League> Get()
            {
                List<League> leagueTeams = new List<League>();
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    {
                        string query = $"SELECT * FROM nba_league";
                        using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                        {
                            using (NpgsqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int Id = reader.GetInt32(reader.GetOrdinal("id"));
                                    string division = reader.GetString(reader.GetOrdinal("division"));
                                    string commissioner = reader.GetString(reader.GetOrdinal("commissioner"));


                                    var leagueTeam = new League

                                    {
                                        Id = Id,
                                        Division = division,
                                        Commissioner = commissioner,

                                    };
                                    leagueTeams.Add(leagueTeam);
                                }
                            }
                        }
                    }
                    connection.Close();

                }
                return leagueTeams;
            }
        }
}
