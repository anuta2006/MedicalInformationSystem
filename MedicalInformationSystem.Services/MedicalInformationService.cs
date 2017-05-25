using MedicalInformationSystem.Common;
using MedicalInformationSystem.Common.Threading;
using MedicalInformationSystem.Services.DataContracts;
using MedicalInformationSystem.Services.Interfaces;
using MedicalInformationSystem.Services.OleDbClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MedicalInformationSystem.Services
{
    public class MedicalInformationService : IMedicalInformationService
    {
        string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Persist Security Info=False;Data Source=..\\..\\..\\..\\MedicalInformationSystem.Services\\bin\\Debug\\BD_Diplom.accdb";
        //System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName), "BD_Diplom.accdb");

        //string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Persist Security Info=False;Data Source=D:\\Diplom\\MedicalInformationSystem\\MedicalInformationSystem.Services\\bin\\Debug\\BD_Diplom.accdb";

        private const string GetClassesQuery = @"SELECT * FROM Class";
        private const string GetClassDataQuery = @"SELECT * FROM Class WHERE ([Letter]) = (?) AND ([Number]) = (?)";
        private const string GetUserDataQuery = @"SELECT * FROM Users WHERE (([Login]) = (?))";
        private const string GetQualificationQuery = "SELECT * FROM Qualification WHERE (([Name]) = (?))";
        private const string GetUserDoctorDataQuery = @"
                    SELECT Doctor.FirstName, Doctor.LastName, Doctor.Patronymic, Qualification.Name AS Qualification, Users.Login
                    FROM (([Doctor] 
                    INNER JOIN [Users] ON Doctor.UserId = Users.Id) 
                    INNER JOIN [Qualification] ON Doctor.QualificationId = Qualification.Id) 
                    WHERE ([Login]) = (?)";
        private const string GetStudentsByClassQuery = @"
                    SELECT *
                    FROM ([Student]
                    INNER JOIN [Class] ON Student.ClassId = Class.Id)
                    WHERE ([Letter]) = (?) AND ([Number]) = (?)";
        private const string GetVaccinationForStudentQuery = @"
                    SELECT StudentVaccination.Date, Vaccination.Name
                    FROM (([Student]
                    INNER JOIN [StudentVaccination] ON StudentVaccination.StudentId = Student.Id)
                    INNER JOIN [Vaccination] ON Vaccination.Id = StudentVaccination.VaccinationId)
                    WHERE ([FirstName]) = (?) AND([LastName]) = (?) AND ([Patronymic]) = (?)";
        private const string GetDiseaseGroupForStudentQuery = @"
                    SELECT DiseaseGroup.Name
                    FROM ([Student]
                    INNER JOIN [DiseaseGroup] ON DiseaseGroup.Id = Student.DiseaseGroupId)
                    WHERE ([FirstName]) = (?) AND ([LastName]) = (?) AND ([Patronymic]) = (?)";

        private const string AddUserDataQuery = @"
                    INSERT INTO Users ([Login], [PasswordSalt], [PasswordHash])
                    VALUES((?), (?), (?))";
        private const string AddDoctorDataQuery = @"
                    INSERT INTO Doctor ([FirstName], [LastName], [Patronymic], [QualificationId], [UserId])
                    VALUES ((?), (?), (?), (?), (?))";
        private const string AddClassDataQuery = @"
                    INSERT INTO Class ([Number], [Letter])
                    VALUES ((?), (?))";

        private const string DeleteStudentQuery = @"
                    DELETE *
                    FROM [Student]
                    WHERE ([FirstName]) = (?) AND ([LastName]) = (?) AND ([Patronymic]) = (?)";
        private const string DeleteClassQuery = @"
                    DELETE *
                    FROM [Class]
                    WHERE ([Letter]) = (?) AND ([Number]) = (?)";

        private const string NextYearStudentTransferQuery = @"
                    UPDATE Student
                    SET [ClassId] = (?)
                    WHERE [FirstName] = (?) AND ([LastName]) = (?) AND ([Patronymic]) = (?)";
        private const string UpdateStudentQuery = @"
                    UPDATE Student
                    SET [FirstName] = (?), [LastName] = (?), [Patronymic] = (?), [ClassId] = (?), [Address] = (?), [Weight] = (?), [Height] = (?)
                    WHERE [FirstName] = (?) AND ([LastName]) = (?) AND ([Patronymic]) = (?)";


        private OleDbConnection _connection;
        private readonly IAsyncResourceLocker _asyncResourceLocker;

        public MedicalInformationService()
        {
            _asyncResourceLocker = new AsyncResourceLocker();
        }

        public async Task<UserDoctorData> GetUserDoctorDataAsync(string login)
        {
            var parameters = new[] { login.AsOleDbInputParameter("@Login") };

            var doctorDataOperationResult = await ExecuteReaderAsync<UserDoctorData>(GetUserDoctorDataQuery, parameters);

            return doctorDataOperationResult.IsSuccessful && doctorDataOperationResult.Result.Count > 0 
                ? doctorDataOperationResult.Result.Single() 
                : null;
        }

        public async Task<RegistrationResult> RegisterUserAsync(string login, string passwordSalt, string passwordHash, 
            string firstName, string lastName, string patronymic, string qualification)
        {
            if (await CheckIfUserExist(login))
            {
                return RegistrationResult.UserWithSuchLoginAlreadyExists;
            }
            else
            {
                if (await AddUserDataAsync(login, passwordSalt, passwordHash))
                {
                    var _qualification = await GetQualificationAsync(qualification);
                    var _user = await GetUserDataAsync(login);
                    if (_qualification.Id > 0 && _user.Id > 0)
                    {
                        var parameters = new[] { firstName.AsOleDbInputParameter("@FirstName"),
                            lastName.AsOleDbInputParameter("@LastName"),
                            patronymic.AsOleDbInputParameter("@Patronymic"),
                            _qualification.Id.AsOleDbInputParameter("@QualificationId"),
                            _user.Id.AsOleDbInputParameter("@UserId") };

                        if (await ExecuteNonQueryAsync(AddDoctorDataQuery, parameters))
                        {
                            return RegistrationResult.Success;
                        };
                    };
                }
            }

            return RegistrationResult.Failed;
        }

        public async Task<AuthorizationResult> AuthorizeUserAsync(string login, string passwordSalt, string passwordHash)
        {
            var _user= await GetUserDataAsync(login);

            if (_user.PasswordSalt == passwordSalt && _user.PasswordHash == passwordHash)
            {
                return AuthorizationResult.Success;
            }
            if (_user.PasswordSalt != passwordSalt || _user.PasswordHash != passwordHash)
            {
                return AuthorizationResult.IncorrectPassword;
            }

            return AuthorizationResult.Failed;
        }

        public async Task<UserData> GetUserDataAsync(string login)
        {
            var parameters = new[] { login.AsOleDbInputParameter("@Login") };

            var userOperationResult = await ExecuteReaderAsync<UserData>(GetUserDataQuery, parameters);

            return userOperationResult.IsSuccessful && userOperationResult.Result.Count > 0 
                ? userOperationResult.Result.Single()
                : null;
        }

        public async Task<IReadOnlyCollection<ClassData>> GetAllClassesAsync()
        {
            var classesOperationResult = await ExecuteReaderAsync<ClassData>(GetClassesQuery, new OleDbParameter[0]);

            return classesOperationResult.IsSuccessful && classesOperationResult.Result.Count > 0 
                ? classesOperationResult.Result 
                : new List<ClassData>();
        }

        public async Task<IReadOnlyCollection<StudentData>> GetStudentsByClassAsync(string classLetter, int classNumber)
        {
            var parameters = new[] { classLetter.AsOleDbInputParameter("@Letter"), classNumber.AsOleDbInputParameter("@Number") };
            var studentsResult = await ExecuteReaderAsync<StudentData>(GetStudentsByClassQuery, parameters);

            return studentsResult.IsSuccessful && studentsResult.Result.Count > 0
                ? studentsResult.Result 
                : new List<StudentData>();
        }

        public async Task<bool> DeleteStudentAsync(string firstName, string lastName, string patronymic)
        {
            var parameters = new[] {
                firstName.AsOleDbInputParameter("@FirstName"),
                lastName.AsOleDbInputParameter("@LastName"),
                patronymic.AsOleDbInputParameter("@Patronymic") };

            return await ExecuteNonQueryAsync(DeleteStudentQuery, parameters);
        }

        public async Task<bool> TransferToNextYearAsync(string firstName, string lastName, string patronymic, string classLetter, int classNumber)
        {
            var checkClassResult = await GetClassAsync(classLetter, classNumber + 1);
            if (checkClassResult == null)
            {
                await AddClassDataAsync(classLetter, classNumber + 1);
            }

            var classResult = await GetClassAsync(classLetter, classNumber + 1);
            var parameters = new[] {
                classResult.Id.AsOleDbInputParameter("@ClassId"),
                firstName.AsOleDbInputParameter("@FirstName"),
                lastName.AsOleDbInputParameter("@LastName"),
                patronymic.AsOleDbInputParameter("@Patronymic") };

            await ExecuteNonQueryAsync(NextYearStudentTransferQuery, parameters);

            var leftStudentsResult = await GetStudentsByClassAsync(classLetter, classNumber);
            if (leftStudentsResult.Count == 0)
            {
                return await DeleteClassAsync(classLetter, classNumber);
            }

            return true;
        }

        public async Task<IReadOnlyCollection<VaccinationData>> GetVaccinationForStudentAsync(string firstName, string lastName, string patronymic)
        {
            var parameters = new[] {
                firstName.AsOleDbInputParameter("@FirstName"),
                lastName.AsOleDbInputParameter("@LastName"),
                patronymic.AsOleDbInputParameter("@Patronymic") };
            var vaccinationResult = await ExecuteReaderAsync<VaccinationData>(GetVaccinationForStudentQuery, parameters);

            return vaccinationResult.IsSuccessful && vaccinationResult.Result.Count > 0
                ? vaccinationResult.Result 
                : new List<VaccinationData>();
        }

        public async Task<IReadOnlyCollection<DiseaseGroupData>> GetDiseaseGroupForStudentAsync(string firstName, string lastName, string patronymic)
        {
            var parameters = new[] {
                firstName.AsOleDbInputParameter("@FirstName"),
                lastName.AsOleDbInputParameter("@LastName"),
                patronymic.AsOleDbInputParameter("@Patronymic") };
            var diseaseGroupResult = await ExecuteReaderAsync<DiseaseGroupData>(GetDiseaseGroupForStudentQuery, parameters);

            return diseaseGroupResult.IsSuccessful && diseaseGroupResult.Result.Count > 0
                ? diseaseGroupResult.Result
                : new List<DiseaseGroupData>();
        }

        private async Task<QualificationData> GetQualificationAsync(string name)
        {
            var parameters = new[] { name.AsOleDbInputParameter("@Name") };

            var qualifOperationResult = await ExecuteReaderAsync<QualificationData>(GetQualificationQuery, parameters);

            return qualifOperationResult.IsSuccessful && qualifOperationResult.Result.Count > 0
                ? qualifOperationResult.Result.Single()
                : null;
        }

        private async Task<bool> CheckIfUserExist(string login)
        {
            var parameters = new[] { login.AsOleDbInputParameter("@Login") };

            var userOperationResult = await ExecuteReaderAsync<UserDoctorData>(GetUserDataQuery, parameters);

            return userOperationResult.IsSuccessful && userOperationResult.Result.Count > 0;
        }

        private async Task<ClassData> GetClassAsync(string letter, int number)
        {
            var parameters = new[] { letter.AsOleDbInputParameter("@Letter"), number.AsOleDbInputParameter("@Number") };

            var classOperationResult = await ExecuteReaderAsync<ClassData>(GetClassDataQuery, parameters);

            return classOperationResult.IsSuccessful && classOperationResult.Result.Count > 0
                ? classOperationResult.Result.Single()
                : null;
        }

        private async Task<bool> DeleteClassAsync(string letter, int number)
        {
            var parameters = new[] 
            {
                letter.AsOleDbInputParameter("@Letter"),
                number.AsOleDbInputParameter("@Number")
            };

            return await ExecuteNonQueryAsync(DeleteClassQuery, parameters);
        }

        private async Task<bool> AddUserDataAsync(string ownerLogin, string passwordSalt, string passwordHash)
        {
            var parameters = new[]
            {
                ownerLogin.AsOleDbInputParameter("@Login"),
                passwordSalt.AsOleDbInputParameter("@PasswordSalt"),
                passwordHash.AsOleDbInputParameter(@"PasswordHash")
            };

            return await ExecuteNonQueryAsync(AddUserDataQuery, parameters);
        }

        private async Task<bool> AddClassDataAsync(string letter, int number)
        {
            var parameters = new[]
            {
                number.AsOleDbInputParameter("@Number"),
                letter.AsOleDbInputParameter("@Letter")
            };

            return await ExecuteNonQueryAsync(AddClassDataQuery, parameters);
        }

        private async Task<OleDbConnection> GetConnectionAsync()
        {
            if (_connection == null)
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
