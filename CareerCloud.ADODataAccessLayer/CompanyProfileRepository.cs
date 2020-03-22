﻿using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
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
                foreach (CompanyProfilePoco Poco in items)
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

                    comm.Parameters.AddWithValue("@Id", Poco.Id);
                    comm.Parameters.AddWithValue("@Registration_Date", Poco.RegistrationDate);
                    comm.Parameters.AddWithValue("@Company_Website", Poco.CompanyWebsite);
                    comm.Parameters.AddWithValue("@Contact_Phone", Poco.ContactPhone);
                    comm.Parameters.AddWithValue("@Contact_Name", Poco.ContactName);

                    comm.Parameters.AddWithValue("@Company_Logo", Poco.CompanyLogo);
                    
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
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Registration_Date]
      ,[Company_Website]
      ,[Contact_Phone]
      ,[Contact_Name]
      ,[Company_Logo]
      ,[Time_Stamp]
  FROM [dbo].[Company_Profiles]";

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyProfilePoco[] pocos = new CompanyProfilePoco[500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();

                    poco.Id = reader.GetGuid(0);
                    poco.RegistrationDate = reader.GetDateTime(1);
                    poco.CompanyWebsite =reader.IsDBNull(2) ? (String?)null : reader.GetString(2);
                    poco.ContactPhone = reader.GetString(3);
                    poco.ContactName = reader.IsDBNull(4) ? (String?)null : reader.GetString(4);
                    poco.CompanyLogo = (byte[])reader[6]; 
                    poco.TimeStamp = (byte[])reader[6];

                    pocos[index] = poco;
                    index++;


                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }

        }

        private byte[] Parse(byte[] v)
        {
            throw new NotImplementedException();
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
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Profiles
                                    WHERE ID=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }


        public void Update(params CompanyProfilePoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (var poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
   SET [Registration_Date] = @Registration_Date
      ,[Company_Website] = @Company_Website
      ,[Contact_Phone] = @Contact_Phone
      ,[Contact_Name] = @Contact_Name
      ,[Company_Logo] = @Company_Logo
 WHERE [Id]=@Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                    
                    conn.Open();
                    int count = cmd.ExecuteNonQuery();
                    if (count != 1)
                    {
                        throw new Exception();
                    }
                    conn.Close();

                }
            }
        }
    }
}

