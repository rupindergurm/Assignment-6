﻿using CareerCloud.DataAccessLayer;
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
                foreach (CompanyLocationPoco Poco in items)
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

                    comm.Parameters.AddWithValue("@Id", Poco.Id);
                    comm.Parameters.AddWithValue("@Company", Poco.Company);
                    comm.Parameters.AddWithValue("@Country_Code", Poco.CountryCode);
                    comm.Parameters.AddWithValue("@State_Province_Code", Poco.Province);
                    comm.Parameters.AddWithValue("@Street_Address", Poco.Street);
                    comm.Parameters.AddWithValue("@City_Town", Poco.City);
                    comm.Parameters.AddWithValue("@Zip_Postal_Code", Poco.PostalCode);

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
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Company]
      ,[Country_Code]
      ,[State_Province_Code]
      ,[Street_Address]
      ,[City_Town]
      ,[Zip_Postal_Code]
      ,[Time_Stamp]
  FROM [dbo].[Company_Locations]";

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyLocationPoco[] pocos = new CompanyLocationPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Company = reader.GetGuid(1);
                    poco.CountryCode = reader.GetString(2);
                    
                    poco.Province = reader.GetString(3);

                    poco.Street = reader.GetString(4);
                    poco.City = reader.IsDBNull(5) ? (String)null : reader.GetString(5);
                    poco.PostalCode =reader.IsDBNull(6)?(String)null : reader.GetString(6);
                    poco.TimeStamp = (byte[])reader[7];

                    pocos[index] = poco;
                    index++;


                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }

        }



        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT [Id]
      ,[Company]
      ,[Country_Code]
      ,[State_Province_Code]
      ,[Street_Address]
      ,[City_Town]
      ,[Zip_Postal_Code]
      ,[Time_Stamp]
  FROM [dbo].[Company_Locations]";

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                CompanyLocationPoco[] pocos = new CompanyLocationPoco[500];
                int index = 0;
                while (reader.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();

                    poco.Id = reader.GetGuid(0);
                    poco.Company = Guid.Parse((string)reader["Company"]);
                    poco.CountryCode = reader.GetString(2);
                    poco.Province = reader.GetString(3);
                    poco.Street = reader.GetString(4);
                    poco.City = reader.GetString(5);
                    poco.PostalCode = reader.GetString(6);
                    poco.TimeStamp = (byte[])reader[7];

                    pocos[index] = poco;
                    index++;


                }

                conn.Close();
                return pocos.Where(a => a != null).ToList();
            }

        }


        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            IQueryable<CompanyLocationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }


        public void Remove(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Locations
                                    WHERE ID=@Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }


        public void Update(params CompanyLocationPoco[] items)
        {
            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (var poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Company_Locations]
   SET [Company] = @Company
      ,[Country_Code] = @Country_Code
      ,[State_Province_Code] = @State_Province_Code
      ,[Street_Address] = @Street_Address
      ,[City_Town] = @City_Town
      ,[Zip_Postal_Code] = @Zip_Postal_Code
 WHERE [Id]=@Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_PRovince_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);
                                       
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

