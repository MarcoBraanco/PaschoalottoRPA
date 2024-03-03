using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace PaschoalottoRPA
{
    internal class DatabaseActions
    {

        private string connectionString;
        private readonly IConfiguration configuration;
        public DatabaseActions()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\.."))
                .AddJsonFile("appconfig.json", optional: true, reloadOnChange: true)
                .Build();

            connectionString = configuration.GetConnectionString("ConnectionString");
        }

        public NpgsqlConnection OpenConnection()
        {
            try
            {
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Conectado ao PostgreSQL!");
                return connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar: {ex.Message}");
                return null;
            }
        }

        public void InsetDataToTable(int wpmData, int keyStrokesData, float accuracyData, int correctWordsData, int wrongWordsData)
        {
            using (NpgsqlConnection connection = OpenConnection())
            {
                if (connection != null)
                {
                    string sql = "INSERT INTO FastFingersData (wpm, keystrokes, accuracy, correct_words, wrong_words) " +
                                 "VALUES (@WpmData, @KeyStrokesData, @AccuracyData, @CorrectWordsData, @WrongWordsData)";

                    using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@WpmData", wpmData);
                        command.Parameters.AddWithValue("@KeyStrokesData", keyStrokesData);
                        command.Parameters.AddWithValue("@AccuracyData", accuracyData);
                        command.Parameters.AddWithValue("@CorrectWordsData", correctWordsData);
                        command.Parameters.AddWithValue("@WrongWordsData", wrongWordsData);

                        command.ExecuteNonQuery();
                        Console.WriteLine("Dados inseridos com sucesso!");
                    }
                }
            }
        }

        public void CloseConnection(NpgsqlConnection connection)
        {
            try
            {
                connection.Close();
                Console.WriteLine("Conexão fechada.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fechar a conexão: {ex.Message}");
            }
        }

    }
}
