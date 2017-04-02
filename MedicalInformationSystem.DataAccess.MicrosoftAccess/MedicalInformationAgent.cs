using MedicalInformationSystem.DataAccess.Interfaces;
using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalInformationSystem.DomainModel;
using MedicalInformationSystem.DataAccess.MicrosoftAccess.OleDbClient;
using MedicalInformationSystem.Common.Threading;
using MedicalInformationSystem.Common;
using System.Reflection;

namespace MedicalInformationSystem.DataAccess.MicrosoftAccess
{
    public class MedicalInformationAgent : IMedicalInformationAgent
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Persist Security Info=False;Data Source=" + 
            System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), "BD_Diplom.accdb");

        private const string GetUserDataQuery = @"select * from SystemUser where ([Login]) = (?)";

        private OleDbConnection _connection;
        private readonly IAsyncResourceLocker _asyncResourceLocker;

        public MedicalInformationAgent() {
            _asyncResourceLocker = new AsyncResourceLocker();
        }

        public async Task<UserData> GetUserDataAsync(string login)
        {
            var parameters = new[] { login.AsOleDbInputParameter("@Login") };

            var userDataOperationResult = await ExecuteReaderAsync<UserData>(GetUserDataQuery, parameters);
            if (userDataOperationResult.IsSuccessful)
            {
                var result = userDataOperationResult.Result;

                return result.Single();
            }

            return null;
        }

        private async Task<OleDbConnection> GetConnectionAsync()
        {
            if(_connection == null)
            {
                _connection = new OleDbConnection(connectionString);
                await _connection.OpenAsync();
            }

            return _connection;
        }

        private static OleDbCommand CreateQueryCommand(string queryString, OleDbConnection connection)
            => new OleDbCommand(queryString, connection) { CommandType = CommandType.Text };

        private async Task<OperationResult<IReadOnlyCollection<T>>> ExecuteReaderAsync<T>(string queryString, params OleDbParameter[] parameters)
        {
            using (await _asyncResourceLocker.TryGetAccessAsync())
            {
                var connection = await GetConnectionAsync();
                using (var command = CreateQueryCommand(queryString, connection))
                {
                    try
                    {
                        var resultSet = new List<T>();
                        var columnProperties = typeof(T).GetTypeInfo().GetProperties()
                              .Select(p => new { Property = p, Column = p.GetCustomAttribute<ColumnAttribute>() })
                              .Where(p => p.Column != null).ToList();

                        command.Parameters.AddRange(parameters);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var obj = CreateInstance<T>();
                                columnProperties.ForEach(columnProperty =>
                                {
                                    var ordinal = reader.GetOrdinal(columnProperty.Column.Name);
                                    var value = reader.GetValue(ordinal);

                                    columnProperty.Property.SetValue(obj, value);
                                });
                                resultSet.Add(obj);
                            }

                            return OperationResult.CreateSuccessful((IReadOnlyCollection<T>)resultSet);
                        }
                    }
                    catch (OleDbException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        return OperationResult<IReadOnlyCollection<T>>.CreateUnsuccessful();
                    }
                }
            }
        }

        private async Task<bool> ExecuteNonQueryAsync(string queryString, params OleDbParameter[] parameters)
        {
            using (await _asyncResourceLocker.TryGetAccessAsync())
            {
                var connection = GetConnectionAsync();
                using (var command = CreateQueryCommand(queryString, _connection))
                {
                    try
                    {
                        command.Parameters.AddRange(parameters);
                        command.ExecuteNonQuery();

                        return true;
                    }
                    catch (OleDbException)
                    {
                        throw;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                }
            }
        }

        private static T CreateInstance<T>()
            => Activator.CreateInstance<T>();
    }
}
