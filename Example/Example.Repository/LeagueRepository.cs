using Example.Model;
using Example.Repository.Common;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Example.Repository
{

    public class LeagueRepository : ILeagueRepository
    {
        private static readonly string connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=root;Database=playerdb;";


        public async Task<List<League>> Get()
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
                                League league = new League();

                                league.Id = (int)reader["id"];
                                league.Division = (string)reader["division"];
                                league.Commissioner = (string)reader["commisioner"];

                                leagueTeams.Add(league);
                            }
                        }
                    }
                }
                connection.Close();

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
                        int Newid = maxId ++;
                        connection.Open();
                        NpgsqlTransaction transaction = connection.BeginTransaction();


                        NpgsqlCommand cmd = new NpgsqlCommand($"INSERT INTO nba_league (id, division, commissioner) VALUES (@id, @division, @commissioner)", connection);

                        cmd.Parameters.AddWithValue("Id", Newid);
                        cmd.Parameters.AddWithValue("FirstName", league.Division);
                        cmd.Parameters.AddWithValue("LastName", league.Commissioner);


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
                    connection.Open();
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
    }

}
