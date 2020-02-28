//Name: Bhavesh Solanki
// Assignment 2 

using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    // Applicant_Educations
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        private readonly string _connStr;
        public ApplicantEducationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantEducationPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO[dbo].[Applicant_Educations]
                                     ([Id]
                                     ,[Applicant]
                                     ,[Major]
                                     ,[Certificate_Diploma]
                                     ,[Start_Date]
                                     ,[Completion_Date]
                                     ,[Completion_Percent])
                                     VALUES
                                     (@Id
                                     ,@Applicant
                                     ,@Major
                                     ,@Certificate_Diploma
                                     ,@Start_Date
                                     ,@Completion_Date
                                     ,@Completion_Percent)";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Major", poco.Major);
                    comm.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    comm.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    comm.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    comm.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Major]
                                  ,[Certificate_Diploma]
                                  ,[Start_Date]
                                  ,[Completion_Date]
                                  ,[Completion_Percent]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Educations]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                ApplicantEducationPoco[] pocos = new ApplicantEducationPoco[500];
                int index = 0;
                while(reader.Read())
                {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Applicant = Guid.Parse(reader["Applicant"].ToString());
                    poco.Major = reader.GetString(2);
                    poco.CertificateDiploma = reader.IsDBNull(3) ? null : reader.GetString(3);
                    poco.StartDate = reader.IsDBNull(4) ? (DateTime?) null : reader.GetDateTime(4);
                    poco.CompletionDate = reader.IsDBNull(5) ? (DateTime?) null : reader.GetDateTime(5);
                    poco.CompletionPercent = reader.IsDBNull(6) ? (Byte?) null : reader.GetByte(6);
                    poco.TimeStamp = (byte[])reader[7];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a!= null).ToList();
            }
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach(ApplicantEducationPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Applicant_Educations]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Educations]
                                       SET 
                                           [Applicant] = @Applicant
                                          ,[Major] = @Major
                                          ,[Certificate_Diploma] = @Certificate_Diploma
                                          ,[Start_Date] = @Start_Date
                                          ,[Completion_Date] = @Completion_Date
                                          ,[Completion_Percent] = @Completion_Percent
                                       WHERE [Id] = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Major", poco.Major);
                    comm.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    comm.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    comm.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    comm.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }

    //Applicant_Job_Applications
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string _connStr;
        public ApplicantJobApplicationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO[dbo].[Applicant_Job_Applications]
                                     ([Id]
                                     ,[Applicant]
                                     ,[Job]
                                     ,[Application_Date])
                                     VALUES
                                     (@Id
                                     ,@Applicant
                                     ,@Job
                                     ,@Applicant_Date)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Applicant_Date", poco.ApplicationDate);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Job]
                                  ,[Application_Date]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Job_Applications]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Applicant = Guid.Parse(reader["Applicant"].ToString());
                    poco.Job = reader.GetGuid(2);
                    poco.ApplicationDate = reader.GetDateTime(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Applicant_Job_Applications]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                       SET [Id] = @Id
                                          ,[Applicant] = @Applicant
                                          ,[Job] = @Job
                                          ,[Application_Date] = @Application_Date
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }

    //Applicant_Profiles
    public class ApplicantProfileRepository : IDataRepository<ApplicantProfilePoco>
    {
        private readonly string _connStr;
        public ApplicantProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantProfilePoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                       ([Id]
                                       ,[Login]
                                       ,[Current_Salary]
                                       ,[Current_Rate]
                                       ,[Currency]
                                       ,[Country_Code]
                                       ,[State_Province_Code]
                                       ,[Street_Address]
                                       ,[City_Town]
                                       ,[Zip_Postal_Code])
                                        VALUES
                                     (@Id
                                     ,@Login
                                     ,@Current_Salary
                                     ,@Current_Rate
                                     ,@Currency
                                     ,@Country_Code
                                     ,@State_Province_Code
                                     ,@Street_Address
                                     ,@City_Town
                                     ,@Zip_Postal_Code)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    comm.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    comm.Parameters.AddWithValue("@Currency", poco.Currency);
                    comm.Parameters.AddWithValue("@Country_Code", poco.Country);
                    comm.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    comm.Parameters.AddWithValue("@Street_Address", poco.Street);
                    comm.Parameters.AddWithValue("@City_Town", poco.City);
                    comm.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Current_Salary]
                                  ,[Current_Rate]
                                  ,[Currency]
                                  ,[Country_Code]
                                  ,[State_Province_Code]
                                  ,[Street_Address]
                                  ,[City_Town]
                                  ,[Zip_Postal_Code]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Profiles]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                ApplicantProfilePoco[] pocos = new ApplicantProfilePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantProfilePoco poco = new ApplicantProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Login = Guid.Parse(reader["Login"].ToString());
                    poco.CurrentSalary = reader.IsDBNull(2) ? (Decimal?) null : reader.GetDecimal(2);
                    poco.CurrentRate = reader.IsDBNull(3) ? (Decimal?) null : reader.GetDecimal(3);
                    poco.Currency = reader.IsDBNull(4) ? null : reader.GetString(4);
                    poco.Country = reader.IsDBNull(5) ? null : reader.GetString(5);
                    poco.Province = reader.IsDBNull(6) ? null : reader.GetString(6);
                    poco.Street = reader.IsDBNull(7) ? null : reader.GetString(7);
                    poco.City = reader.IsDBNull(8) ? null : reader.GetString(8);
                    poco.PostalCode = reader.IsDBNull(9) ? null : reader.GetString(9);
                    poco.TimeStamp = (byte[])reader[10];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (ApplicantProfilePoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Applicant_Profiles]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                       SET [Id] = @Id
                                          ,[Login] = @Login
                                          ,[Current_Salary] = @Current_Salary
                                          ,[Current_Rate] = @Current_Rate
                                          ,[Currency] = @Currency
                                          ,[Country_Code] = @Country_Code
                                          ,[State_Province_Code] = @State_Province_Code
                                          ,[Street_Address] = @Street_Address
                                          ,[City_Town] = @City_Town
                                          ,[Zip_Postal_Code] = @Zip_Postal_Code
                                     WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Current_Salary", poco.CurrentSalary);
                    comm.Parameters.AddWithValue("@Current_Rate", poco.CurrentRate);
                    comm.Parameters.AddWithValue("@Currency", poco.Currency);
                    comm.Parameters.AddWithValue("@Country_Code", poco.Country);
                    comm.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    comm.Parameters.AddWithValue("@Street_Address", poco.Street);
                    comm.Parameters.AddWithValue("@City_Town", poco.City);
                    comm.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }

    //Applicant_Resumes
    public class ApplicantResumeRepository : IDataRepository<ApplicantResumePoco>
    {
        private readonly string _connStr;
        public ApplicantResumeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantResumePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantResumePoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                                       ([Id]
                                       ,[Applicant]
                                       ,[Resume]
                                       ,[Last_Updated])
                                        VALUES
                                       (@Id
                                       ,@Applicant
                                       ,@Resume
                                       ,@Last_Updated)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Resume", poco.Resume);
                    comm.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Resume]
                                  ,[Last_Updated]
                                  FROM [dbo].[Applicant_Resumes]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                ApplicantResumePoco[] pocos = new ApplicantResumePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantResumePoco poco = new ApplicantResumePoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Applicant = Guid.Parse(reader["Applicant"].ToString());
                    poco.Resume = reader.GetString(2);
                    poco.LastUpdated = reader.IsDBNull(3) ? (DateTime?) null : reader.GetDateTime(3);

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantResumePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (ApplicantResumePoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Applicant_Resumes]
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                                       SET [Id] = @Id
                                          ,[Applicant] = @Applicant
                                          ,[Resume] = @Resume
                                          ,[Last_Updated] = @Last_Updated
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Resume", poco.Resume);
                    comm.Parameters.AddWithValue("@Last_Updated", poco.LastUpdated);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Applicant_Skills
    public class ApplicantSkillRepository : IDataRepository<ApplicantSkillPoco>
    {
        private readonly string _connStr;
        public ApplicantSkillRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantSkillPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                       ([Id]
                                       ,[Applicant]
                                       ,[Skill]
                                       ,[Skill_Level]
                                       ,[Start_Month]
                                       ,[Start_Year]
                                       ,[End_Month]
                                       ,[End_Year])
                                       VALUES
                                       (@Id
                                       ,@Applicant
                                       ,@Skill
                                       ,@Skill_Level
                                       ,@Start_Month
                                       ,@Start_Year
                                       ,@End_Month
                                       ,@End_Year)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Skill", poco.Skill);
                    comm.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    comm.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    comm.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    comm.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    comm.Parameters.AddWithValue("@End_Year", poco.EndYear);
                 
                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Skill]
                                  ,[Skill_Level]
                                  ,[Start_Month]
                                  ,[Start_Year]
                                  ,[End_Month]
                                  ,[End_Year]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Skills]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                ApplicantSkillPoco[] pocos = new ApplicantSkillPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantSkillPoco poco = new ApplicantSkillPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Applicant = Guid.Parse(reader["Applicant"].ToString());
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.StartMonth = reader.GetByte(4);
                    poco.StartYear = reader.GetInt32(5);
                    poco.EndMonth = reader.GetByte(6);
                    poco.EndYear = reader.GetInt32(7);
                    poco.TimeStamp = (byte[])reader[8];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (ApplicantSkillPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Applicant_Skills]
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                       SET [Id] = @Id
                                          ,[Applicant] = @Applicant
                                          ,[Skill] = @Skill
                                          ,[Skill_Level] = @Skill_Level
                                          ,[Start_Month] = @Start_Month
                                          ,[Start_Year] = @Start_Year
                                          ,[End_Month] = @End_Month
                                          ,[End_Year] = @End_Year
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Skill", poco.Skill);
                    comm.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    comm.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    comm.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    comm.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    comm.Parameters.AddWithValue("@End_Year", poco.EndYear);

                                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }

    //Applicant_Work_History
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        private readonly string _connStr;
        public ApplicantWorkHistoryRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                                       ([Id]
                                       ,[Applicant]
                                       ,[Company_Name]
                                       ,[Country_Code]
                                       ,[Location]
                                       ,[Job_Title]
                                       ,[Job_Description]
                                       ,[Start_Month]
                                       ,[Start_Year]
                                       ,[End_Month]
                                       ,[End_Year])
                                       VALUES
                                       (@Id
                                       ,@Applicant
                                       ,@Company_Name
                                       ,@Country_Code
                                       ,@Location
                                       ,@Job_Title
                                       ,@Job_Description
                                       ,@Start_Month
                                       ,@Start_Year
                                       ,@End_Month
                                       ,@End_Year)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    comm.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    comm.Parameters.AddWithValue("@Location", poco.Location);
                    comm.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    comm.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    comm.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    comm.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    comm.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    comm.Parameters.AddWithValue("@End_Year", poco.EndYear);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Company_Name]
                                  ,[Country_Code]
                                  ,[Location]
                                  ,[Job_Title]
                                  ,[Job_Description]
                                  ,[Start_Month]
                                  ,[Start_Year]
                                  ,[End_Month]
                                  ,[End_Year]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Applicant_Work_History]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                ApplicantWorkHistoryPoco[] pocos = new ApplicantWorkHistoryPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Applicant = Guid.Parse(reader["Applicant"].ToString());
                    poco.CompanyName = reader.GetString(2);
                    poco.CountryCode = reader.GetString(3);
                    poco.Location = reader.GetString(4);
                    poco.JobTitle = reader.GetString(5);
                    poco.JobDescription = reader.GetString(6);
                    poco.StartMonth = reader.GetInt16(7);
                    poco.StartYear = reader.GetInt32(8);
                    poco.EndMonth = reader.GetInt16(9);
                    poco.EndYear = reader.GetInt32(10);
                    poco.TimeStamp = (byte[])reader[11];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantWorkHistoryPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Applicant_Work_History]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                                       SET [Id] = @Id
                                          ,[Applicant] = @Applicant
                                          ,[Company_Name] = @Company_Name
                                          ,[Country_Code] = @Country_Code
                                          ,[Location] = @Location
                                          ,[Job_Title] = @Job_Title
                                          ,[Job_Description] = @Job_Description
                                          ,[Start_Month] = @Start_Month
                                          ,[Start_Year] = @Start_Year
                                          ,[End_Month] = @End_Month
                                          ,[End_Year] = @End_Year
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    comm.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    comm.Parameters.AddWithValue("@Location", poco.Location);
                    comm.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    comm.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    comm.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    comm.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    comm.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    comm.Parameters.AddWithValue("@End_Year", poco.EndYear);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }

    //Company_Descriptions
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        private readonly string _connStr;
        public CompanyDescriptionRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (CompanyDescriptionPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                       ([Id]
                                       ,[Company]
                                       ,[LanguageID]
                                       ,[Company_Name]
                                       ,[Company_Description])
                                       VALUES
                                       (@Id
                                       ,@Company
                                       ,@LanguageID
                                       ,@Company_Name
                                       ,@Company_Description)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Company", poco.Company);
                    comm.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    comm.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    comm.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Company]
                                  ,[LanguageID]
                                  ,[Company_Name]
                                  ,[Company_Description]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Company_Descriptions]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                CompanyDescriptionPoco[] pocos = new CompanyDescriptionPoco[650];
                int index = 0;
                while (reader.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Company = Guid.Parse(reader["Company"].ToString());
                    poco.LanguageId = reader.GetString(2);
                    poco.CompanyName = reader.GetString(3);
                    poco.CompanyDescription = reader.GetString(4);
                    poco.TimeStamp = (byte[])reader[5];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Company_Descriptions]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                       SET [Id] = @Id
                                          ,[Company] = @Company
                                          ,[LanguageID] = @LanguageID
                                          ,[Company_Name] = @Company_Name
                                          ,[Company_Description] = @Company_Description
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Company", poco.Company);
                    comm.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    comm.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    comm.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
                   
                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Company_Job_Educations
    public class CompanyJobEducationRepository : IDataRepository<CompanyJobEducationPoco>
    {
        private readonly string _connStr;
        public CompanyJobEducationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (CompanyJobEducationPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Company_Job_Educations]
                                       ([Id]
                                       ,[Job]
                                       ,[Major]
                                       ,[Importance])
                                       VALUES
                                       (@Id
                                       ,@Job
                                       ,@Major
                                       ,@Importance)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Major", poco.Major);
                    comm.Parameters.AddWithValue("@Importance", poco.Importance);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Job]
                                  ,[Major]
                                  ,[Importance]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Company_Job_Educations]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                CompanyJobEducationPoco[] pocos = new CompanyJobEducationPoco[1050];
                int index = 0;
                while (reader.Read())
                {
                    CompanyJobEducationPoco poco = new CompanyJobEducationPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Job = Guid.Parse(reader["Job"].ToString());
                    poco.Major = reader.GetString(2);
                    poco.Importance = reader.GetInt16(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobEducationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (CompanyJobEducationPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Company_Job_Educations]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Company_Job_Educations]
                                       SET [Id] = @Id
                                          ,[Job] = @Job
                                          ,[Major] = @Major
                                          ,[Importance] = @Importance
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Major", poco.Major);
                    comm.Parameters.AddWithValue("@Importance", poco.Importance);
                                       
                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Company_Job_Skills
    public class CompanyJobSkillRepository : IDataRepository<CompanyJobSkillPoco>
    {
        private readonly string _connStr;
        public CompanyJobSkillRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (CompanyJobSkillPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                                       ([Id]
                                       ,[Job]
                                       ,[Skill]
                                       ,[Skill_Level]
                                       ,[Importance])
                                       VALUES
                                       (@Id
                                       ,@Job
                                       ,@Skill
                                       ,@Skill_Level
                                       ,@Importance)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Skill", poco.Skill);
                    comm.Parameters.AddWithValue("@Skill_Level",poco.SkillLevel);
                    comm.Parameters.AddWithValue("@Importance", poco.Importance);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobSkillPoco> GetAll(params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Job]
                                  ,[Skill]
                                  ,[Skill_Level]
                                  ,[Importance]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Company_Job_Skills]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                CompanyJobSkillPoco[] pocos = new CompanyJobSkillPoco[5050];
                int index = 0;
                while (reader.Read())
                {
                    CompanyJobSkillPoco poco = new CompanyJobSkillPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Job = Guid.Parse(reader["Job"].ToString());
                    poco.Skill = reader.GetString(2);
                    poco.SkillLevel = reader.GetString(3);
                    poco.Importance = reader.GetInt32(4);
                    poco.TimeStamp = (byte[])reader[5];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobSkillPoco> GetList(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(Expression<Func<CompanyJobSkillPoco, bool>> where, params Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobSkillPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (CompanyJobSkillPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Company_Job_Skills]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                                       SET [Id] = @Id
                                          ,[Job] = @Job
                                          ,[Skill] = @Skill
                                          ,[Skill_Level] = @Skill_Level
                                          ,[Importance] = @Importance
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Skill", poco.Skill);
                    comm.Parameters.AddWithValue("@Skill_Level", poco.SkillLevel);
                    comm.Parameters.AddWithValue("@Importance", poco.Importance);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }

    //Company_Jobs
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        private readonly string _connStr;
        public CompanyJobRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (CompanyJobPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                                       ([Id]
                                       ,[Company]
                                       ,[Profile_Created]
                                       ,[Is_Inactive]
                                       ,[Is_Company_Hidden])
                                       VALUES
                                       (@Id
                                       ,@Company
                                       ,@Profile_Created
                                       ,@Is_Inactive
                                       ,@Is_Company_Hidden)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Company", poco.Company);
                    comm.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    comm.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    comm.Parameters.AddWithValue("@Is_Company_Hidden",poco.IsCompanyHidden);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Company]
                                  ,[Profile_Created]
                                  ,[Is_Inactive]
                                  ,[Is_Company_Hidden]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Company_Jobs]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                CompanyJobPoco[] pocos = new CompanyJobPoco[1001];
                int index = 0;
                while (reader.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Company = Guid.Parse(reader["Company"].ToString());
                    poco.ProfileCreated = reader.GetDateTime(2);
                    poco.IsInactive = reader.GetBoolean(3);
                    poco.IsCompanyHidden = reader.GetBoolean(4);
                    poco.TimeStamp = reader.IsDBNull(5) ? null : (byte[])reader[5];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (CompanyJobPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Company_Jobs]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Company_Jobs]
                                       SET [Id] = @Id
                                          ,[Company] = @Company
                                          ,[Profile_Created] =@Profile_Created
                                          ,[Is_Inactive] = @Is_Inactive
                                          ,[Is_Company_Hidden] = @Is_Company_Hidden
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Company", poco.Company);
                    comm.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    comm.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    comm.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Company_Jobs_Decriptions
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        private readonly string _connStr;
        public CompanyJobDescriptionRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                       ([Id]
                                       ,[Job]
                                       ,[Job_Name]
                                       ,[Job_Descriptions])
                                       VALUES
                                       (@Id
                                       ,@Job
                                       ,@Job_Name
                                       ,@Job_Descriptions)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    comm.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);
                    
                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Job]
                                  ,[Job_Name]
                                  ,[Job_Descriptions]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Company_Jobs_Descriptions]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                CompanyJobDescriptionPoco[] pocos = new CompanyJobDescriptionPoco[1050];
                int index = 0;
                while (reader.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Job = Guid.Parse(reader["Job"].ToString());
                    poco.JobName = reader.IsDBNull(2) ? null : reader.GetString(2);
                    poco.JobDescriptions = reader.IsDBNull(3) ? null : reader.GetString(3);
                    poco.TimeStamp = reader.IsDBNull(4) ? null : (byte[])reader[4];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyJobDescriptionPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                       SET [Id] = @Id
                                          ,[Job] = @Job
                                          ,[Job_Name] = @Job_Name
                                          ,[Job_Descriptions] = @Job_Descriptions
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    comm.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);
                    
                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Company_Locations
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        private readonly string _connStr;
        public CompanyLocationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (CompanyLocationPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                       ([Id]
                                       ,[Company]
                                       ,[Country_Code]
                                       ,[State_Province_Code]
                                       ,[Street_Address]
                                       ,[City_Town]
                                       ,[Zip_Postal_Code])
                                       VALUES
                                       (@Id
                                       ,@Company
                                       ,@Country_Code
                                       ,@State_Province_Code
                                       ,@Street_Address
                                       ,@City_Town
                                       ,@Zip_Postal_Code)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Company", poco.Company);
                    comm.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    comm.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    comm.Parameters.AddWithValue("@Street_Address", poco.Street);
                    comm.Parameters.AddWithValue("@City_Town", poco.City);
                    comm.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Company]
                                  ,[Country_Code]
                                  ,[State_Province_Code]
                                  ,[Street_Address]
                                  ,[City_Town]
                                  ,[Zip_Postal_Code]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Company_Locations]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                CompanyLocationPoco[] pocos = new CompanyLocationPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Company = Guid.Parse(reader["Company"].ToString());
                    poco.CountryCode = reader.GetString(2);
                    poco.Province = reader.IsDBNull(3) ? null: reader.GetString(3);
                    poco.Street = reader.IsDBNull(4) ? null : reader.GetString(4);
                    poco.City = reader.IsDBNull(5) ? null : reader.GetString(5);
                    poco.PostalCode = reader.IsDBNull(6) ? null : reader.GetString(6);
                    poco.TimeStamp = (byte[])reader[7];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (CompanyLocationPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Company_Locations]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Company_Locations]
                                       SET [Id] = @Id
                                          ,[Company] = @Company
                                          ,[Country_Code] = @Country_Code
                                          ,[State_Province_Code] = @State_Province_Code
                                          ,[Street_Address] = @Street_Address
                                          ,[City_Town] = @City_Town
                                          ,[Zip_Postal_Code] = @Zip_Postal_Code
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Company", poco.Company);
                    comm.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    comm.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    comm.Parameters.AddWithValue("@Street_Address", poco.Street);
                    comm.Parameters.AddWithValue("@City_Town", poco.City);
                    comm.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Company_Profiles
    public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        private readonly string _connStr;
        public CompanyProfileRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (CompanyProfilePoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                       ([Id]
                                       ,[Registration_Date]
                                       ,[Company_Website]
                                       ,[Contact_Phone]
                                       ,[Contact_Name]
                                       ,[Company_Logo])
                                       VALUES
                                       (@Id
                                       ,@Registration_Date
                                       ,@Company_Website
                                       ,@Contact_Phone
                                       ,@Contact_Name
                                       ,@Company_Logo)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    comm.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    comm.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    comm.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    comm.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Registration_Date]
                                  ,[Company_Website]
                                  ,[Contact_Phone]
                                  ,[Contact_Name]
                                  ,[Company_Logo]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Company_Profiles]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                CompanyProfilePoco[] pocos = new CompanyProfilePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.RegistrationDate = reader.GetDateTime(1);
                    poco.CompanyWebsite = reader.IsDBNull(2) ? null : reader.GetString(2);
                    poco.ContactPhone = reader.GetString(3);
                    poco.ContactName = reader.IsDBNull(4) ? null : reader.GetString(4);
                    poco.CompanyLogo = reader.IsDBNull(5) ? null : (byte[])reader[5];
                    poco.TimeStamp = reader.IsDBNull(6) ? null : (byte[])reader[6];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyProfilePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (CompanyProfilePoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                       SET [Id] = @Id
                                          ,[Registration_Date] = @Registration_Date
                                          ,[Company_Website] = @Company_Website
                                          ,[Contact_Phone] = @Contact_Phone
                                          ,[Contact_Name] = @Contact_Name
                                          ,[Company_Logo] = @Company_Logo
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    comm.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    comm.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    comm.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    comm.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                    
                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Security_Logins
    public class SecurityLoginRepository : IDataRepository<SecurityLoginPoco>
    {
        private readonly string _connStr;
        public SecurityLoginRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityLoginPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (SecurityLoginPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                       ([Id]
                                       ,[Login]
                                       ,[Password]
                                       ,[Created_Date]
                                       ,[Password_Update_Date]
                                       ,[Agreement_Accepted_Date]
                                       ,[Is_Locked]
                                       ,[Is_Inactive]
                                       ,[Email_Address]
                                       ,[Phone_Number]
                                       ,[Full_Name]
                                       ,[Force_Change_Password]
                                       ,[Prefferred_Language])
                                       VALUES
                                       (@Id
                                       ,@Login
                                       ,@Password
                                       ,@Created_Date
                                       ,@Password_Update_Date
                                       ,@Agreement_Accepted_Date
                                       ,@Is_Locked
                                       ,@Is_Inactive
                                       ,@Email_Address
                                       ,@Phone_Number
                                       ,@Full_Name
                                       ,@Force_Change_Password
                                       ,@Prefferred_Language)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Password", poco.Password);
                    comm.Parameters.AddWithValue("@Created_Date", poco.Created);
                    comm.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    comm.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    comm.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    comm.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    comm.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    comm.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    comm.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    comm.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    comm.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Password]
                                  ,[Created_Date]
                                  ,[Password_Update_Date]
                                  ,[Agreement_Accepted_Date]
                                  ,[Is_Locked]
                                  ,[Is_Inactive]
                                  ,[Email_Address]
                                  ,[Phone_Number]
                                  ,[Full_Name]
                                  ,[Force_Change_Password]
                                  ,[Prefferred_Language]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Security_Logins]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                SecurityLoginPoco[] pocos = new SecurityLoginPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SecurityLoginPoco poco = new SecurityLoginPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Login = reader.GetString(1);
                    poco.Password = reader.GetString(2);
                    poco.Created = reader.GetDateTime(3);
                    poco.PasswordUpdate = reader.IsDBNull(4) ? (DateTime?) null : reader.GetDateTime(4);
                    poco.AgreementAccepted = reader.IsDBNull(5) ? (DateTime?) null : reader.GetDateTime(5);
                    poco.IsLocked = reader.GetBoolean(6);
                    poco.IsInactive = reader.GetBoolean(7);
                    poco.EmailAddress = reader.GetString(8);
                    poco.PhoneNumber = reader.IsDBNull(9) ? null : reader.GetString(9);
                    poco.FullName = reader.IsDBNull(10) ? null : reader.GetString(10);
                    poco.ForceChangePassword = reader.GetBoolean(11);
                    poco.PrefferredLanguage = reader.IsDBNull(12) ? null : reader.GetString(12);
                    poco.TimeStamp = (byte[])reader[13];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (SecurityLoginPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Security_Logins]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Security_Logins]
                                       SET [Id] = @Id
                                          ,[Login] = @Login
                                          ,[Password] = @Password
                                          ,[Created_Date] = @Created_Date
                                          ,[Password_Update_Date] = @Password_Update_Date
                                          ,[Agreement_Accepted_Date] = @Agreement_Accepted_Date
                                          ,[Is_Locked] = @Is_Locked
                                          ,[Is_Inactive] = @Is_Inactive
                                          ,[Email_Address] = @Email_Address
                                          ,[Phone_Number] = @Phone_Number
                                          ,[Full_Name] = @Full_Name
                                          ,[Force_Change_Password] = @Force_Change_Password
                                          ,[Prefferred_Language] = @Prefferred_Language
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Password", poco.Password);
                    comm.Parameters.AddWithValue("@Created_Date", poco.Created);
                    comm.Parameters.AddWithValue("@Password_Update_Date", poco.PasswordUpdate);
                    comm.Parameters.AddWithValue("@Agreement_Accepted_Date", poco.AgreementAccepted);
                    comm.Parameters.AddWithValue("@Is_Locked", poco.IsLocked);
                    comm.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    comm.Parameters.AddWithValue("@Email_Address", poco.EmailAddress);
                    comm.Parameters.AddWithValue("@Phone_Number", poco.PhoneNumber);
                    comm.Parameters.AddWithValue("@Full_Name", poco.FullName);
                    comm.Parameters.AddWithValue("@Force_Change_Password", poco.ForceChangePassword);
                    comm.Parameters.AddWithValue("@Prefferred_Language", poco.PrefferredLanguage);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Security_Logins_Log
    public class SecurityLoginsLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        private readonly string _connStr;
        public SecurityLoginsLogRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
                                       ([Id]
                                       ,[Login]
                                       ,[Source_IP]
                                       ,[Logon_Date]
                                       ,[Is_Succesful])
                                       VALUES
                                       (@Id
                                       ,@Login
                                       ,@Source_IP
                                       ,@Logon_Date
                                       ,@Is_Succesful)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Source_IP", poco.SourceIP);
                    comm.Parameters.AddWithValue("@Logon_Date", poco.LogonDate);
                    comm.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Source_IP]
                                  ,[Logon_Date]
                                  ,[Is_Succesful]
                                  FROM [dbo].[Security_Logins_Log]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                SecurityLoginsLogPoco[] pocos = new SecurityLoginsLogPoco[1750];
                int index = 0;
                while (reader.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Login = reader.GetGuid(1);
                    poco.SourceIP = reader.GetString(2);
                    poco.LogonDate = reader.GetDateTime(3);
                    poco.IsSuccesful = reader.GetBoolean(4);

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsLogPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Security_Logins_Log]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Security_Logins_Log]
                                       SET [Id] = @Id
                                          ,[Login] = @Login
                                          ,[Source_IP] = @Source_IP
                                          ,[Logon_Date] = @Logon_Date
                                          ,[Is_Succesful] = @Is_Succesful
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Source_IP", poco.SourceIP);
                    comm.Parameters.AddWithValue("@Logon_Date", poco.LogonDate);
                    comm.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);
                   
                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Security_Logins_Roles
    public class SecurityLoginsRoleRepository : IDataRepository<SecurityLoginsRolePoco>
    {
        private readonly string _connStr;
        public SecurityLoginsRoleRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles]
                                       ([Id]
                                       ,[Login]
                                       ,[Role])
                                       VALUES
                                       (@Id
                                       ,@Login
                                       ,@Role)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Role", poco.Role);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Login]
                                  ,[Role]
                                  ,[Time_Stamp]
                                  FROM [dbo].[Security_Logins_Roles]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                SecurityLoginsRolePoco[] pocos = new SecurityLoginsRolePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Login = reader.GetGuid(1);
                    poco.Role = reader.GetGuid(2);
                    poco.TimeStamp = reader.IsDBNull(3) ? null : (byte[])reader[3];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityLoginsRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (SecurityLoginsRolePoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Security_Logins_Roles]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Security_Logins_Roles]
                                       SET [Id] = @Id
                                          ,[Login] = @Login
                                          ,[Role] = @Role
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Login", poco.Login);
                    comm.Parameters.AddWithValue("@Role", poco.Role);
                   
                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //Security_Roles
    public class SecurityRoleRepository : IDataRepository<SecurityRolePoco>
    {
        private readonly string _connStr;
        public SecurityRoleRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SecurityRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (SecurityRolePoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Security_Roles]
                                       ([Id]
                                       ,[Role]
                                       ,[Is_Inactive])
                                       VALUES
                                       (@Id
                                       ,@Role
                                       ,@Is_Inactive)";
                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Role", poco.Role);
                    comm.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Id]
                                  ,[Role]
                                  ,[Is_Inactive]
                                  FROM [dbo].[Security_Roles]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                SecurityRolePoco[] pocos = new SecurityRolePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SecurityRolePoco poco = new SecurityRolePoco();
                    poco.Id = reader.GetGuid(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Role = reader.GetString(1);
                    poco.IsInactive = reader.GetBoolean(2);

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            IQueryable<SecurityRolePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (SecurityRolePoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[Security_Roles]
                                         WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SecurityRolePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[Security_Roles]
                                       SET [Id] = @Id
                                          ,[Role] = @Role
                                          ,[Is_Inactive] = @Is_Inactive
                                       WHERE ID = @Id";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Role", poco.Role);
                    comm.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //System_Country_Codes
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        private readonly string _connStr;
        public SystemCountryCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (SystemCountryCodePoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[System_Country_Codes]
                                       ([Code]
                                       ,[Name])
                                       VALUES
                                       (@Code
                                       ,@Name)";
                    comm.Parameters.AddWithValue("@Code", poco.Code);
                    comm.Parameters.AddWithValue("@Name", poco.Name);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [Code]
                                   ,[Name]
                                   FROM [dbo].[System_Country_Codes]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                SystemCountryCodePoco[] pocos = new SystemCountryCodePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = reader.GetString(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Name = reader.GetString(1);

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemCountryCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (SystemCountryCodePoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[System_Country_Codes]
                                         WHERE CODE = @Code";

                    comm.Parameters.AddWithValue("@Code", poco.Code);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[System_Country_Codes]
                                       SET [Code] = @Code
                                          ,[Name] = @Name
                                       WHERE CODE = @Code";

                    comm.Parameters.AddWithValue("@Code", poco.Code);
                    comm.Parameters.AddWithValue("@Name", poco.Name);
                    
                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }


    //System_Language_Codes
    public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        private readonly string _connStr;
        public SystemLanguageCodeRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }
        public void Add(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (SystemLanguageCodePoco poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                       ([LanguageID]
                                       ,[Name]
                                       ,[Native_Name])
                                       VALUES
                                       (@LanguageID
                                       ,@Name
                                       ,@Native_Name)";
                    comm.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    comm.Parameters.AddWithValue("@Name", poco.Name);
                    comm.Parameters.AddWithValue("@Native_Name", poco.NativeName);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandText = @"SELECT [LanguageID]
                                  ,[Name]
                                  ,[Native_Name]
                                  FROM [dbo].[System_Language_Codes]";

                connection.Open();
                SqlDataReader reader = comm.ExecuteReader();
                SystemLanguageCodePoco[] pocos = new SystemLanguageCodePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = reader.GetString(0);
                    //poco.Id = Guid.Parse(reader["Id"].ToString());
                    poco.Name = reader.GetString(1);
                    poco.NativeName = reader.GetString(2);

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            IQueryable<SystemLanguageCodePoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    comm.CommandText = @"DELETE FROM [dbo].[System_Language_Codes]
                                         WHERE LANGUAGEID = @LanguageId";

                    comm.Parameters.AddWithValue("@LanguageId", poco.LanguageID);

                    connection.Open();
                    comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                foreach (var poco in items)
                {
                    comm.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                       SET [LanguageID] = @LanguageID
                                          ,[Name] = @Name
                                          ,[Native_Name] = @Native_Name
                                       WHERE LANGUAGEID = @LanguageID";

                    comm.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    comm.Parameters.AddWithValue("@Name", poco.Name);
                    comm.Parameters.AddWithValue("@Native_Name", poco.NativeName);

                    connection.Open();
                    int count = comm.ExecuteNonQuery();                  
                    connection.Close();
                    
                }
            }
        }
    }
}
