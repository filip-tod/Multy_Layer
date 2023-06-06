using Example.Model;
using Example.Repository.Common;
using Example.Service.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Example.Repository
{

    public class LeagueRepository : ILeagueRepository
    {
        //constructor
        public LeagueRepository(ILeagueService context)
        {
            Context = context;
        }
        public ILeagueService Context { get; private set; }


        private static readonly string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=playerdb;";
        public async Task<List<League>> GetAll()
        {
            List<League> leagueTeams = new List<League>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                {
                    string query = $"SELECT * FROM nba_league";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
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

            }
            return leagueTeams;
        }
        public async Task<List<League>> GetById(int id)
        {
            List<League> leagueTeams = new List<League>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                {
                    string query = $"SELECT * FROM nba_league WHERE id = @id ";
                    using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (reader.Read())
                            {

                                League league = new League();

                                league.Id = (int)reader["id"];
                                league.Division = (string)reader["division"];
                                league.Commissioner = (string)reader["commisioner"];


                                leagueTeams.Add(league);
                            }
                        }

                    }

                }

            }
            return leagueTeams;
        }
        public async Task<bool> Post(League league)
        {
            List<League> leagueTeams = new List<League>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO nba_league  (id, division, commissioner) VALUES (@id, @division, @commissioner)";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    List<League> leagueData = new List<League>();
                    try
                    {
                        int maxId = leagueData.Max(p => p.Id);
                        int Newid = maxId++;
                        NpgsqlTransaction transaction = connection.BeginTransaction();


                        NpgsqlCommand cmd = new NpgsqlCommand($"INSERT INTO nba_league (id, division, commissioner) VALUES (@id, @division, @commissioner)", connection);

                        cmd.Parameters.AddWithValue("id", Newid);
                        cmd.Parameters.AddWithValue("division", league.Division);
                        cmd.Parameters.AddWithValue("commisioner", league.Commissioner);


                        int affectedRowsPerson = await cmd.ExecuteNonQueryAsync();
                        transaction.Commit();

                        if (affectedRowsPerson > 0)
                        {
                            return true;
                        }

                        transaction.Rollback();
                        return false;

                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }

            }
        }
        public async Task<bool> Put(int id, League leaguedata)
        {
            List<League> queryUpdate = new List<League>();
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO nba_league  (id, division, commissioner) VALUES (@id, @division, @commissioner)";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {

                    string querySet = "UPDATE nba_league SET ";
                    List<string> updateFields = new List<string>();

                    if (leaguedata.Division != "")
                    {
                        updateFields.Add("division = @division");
                        command.Parameters.AddWithValue("@pricediscount", leaguedata.Division);
                    }

                    if (leaguedata.Commissioner != "")
                    {
                        updateFields.Add("commissioner = @commissioner");
                        command.Parameters.AddWithValue("@couponvalidation", leaguedata.Division);
                    }

                    query += string.Join(", ", updateFields);
                    query += " WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    await command.ExecuteNonQueryAsync();
                }
                return true;
            }
        }
        public async Task<bool> Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string query = "DELETE FROM nba_league WHERE id = @id";
                using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                    {
                        return false;
                    }
                    return true;
                }
            }
        }
    
        public async Task<List<League>> GetLeagues(int pageNumber, int pageSize, string sortBy)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var query = $"SELECT * FROM nba_league ORDER BY {sortBy} LIMIT {pageSize} OFFSET {(pageNumber - 1) * pageSize}";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        List<League> leagues = new List<League>();

                        while (await reader.ReadAsync())
                        {
                            League league = new League
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Division = reader.GetString(reader.GetOrdinal("division")),
                                Commissioner = reader.GetString(reader.GetOrdinal("commissioner"))
                            };

                            leagues.Add(league);
                        }

                        return leagues;
                    }
                }

            }

        }
        public async Task<List<League>> SortLeague(string divisionFilter, string commissionerFilter)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM nba_league WHERE 1=1";

                if (!string.IsNullOrEmpty(divisionFilter))
                {
                    query += " AND division = @divisionFilter";
                }

                if (!string.IsNullOrEmpty(commissionerFilter))
                {
                    query += " AND commissioner = @commissionerFilter";
                }

                using (var command = new NpgsqlCommand(query, connection))
                {
                    if (!string.IsNullOrEmpty(divisionFilter))
                    {
                        command.Parameters.AddWithValue("@divisionFilter", divisionFilter);
                    }

                    if (!string.IsNullOrEmpty(commissionerFilter))
                    {
                        command.Parameters.AddWithValue("@commissionerFilter", commissionerFilter);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        List<League> leagues = new List<League>();

                        while (await reader.ReadAsync())
                        {
                            League league = new League
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("id")),
                                Division = reader.GetString(reader.GetOrdinal("division")),
                                Commissioner = reader.GetString(reader.GetOrdinal("commissioner"))
                            };

                            leagues.Add(league);
                        }

                        return leagues;
                    }
                }
            }
        }
       

    }
}