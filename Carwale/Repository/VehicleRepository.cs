using System.Security.Cryptography.X509Certificates;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Carwale.Models;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc;

namespace Carwale.Repository
{
    public class VehicleRepository
    {

        public AppDb Db { get; }
        public string ConnectionString { get; set; }
        public VehicleRepository(AppDb db)
        {
            Db = db;

        }

        public async Task<bool> AddNewVehicle(VehicleModel vehicle)
        {
            try
            {

                using var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = "insert into vehicles (modelName,brand,price,mileage,engine,transmission,fuelType,capacity,vehiclePhoto) values(@modelName,@brand,@price,@mileage,@engine,@transmission,@fuelType,@capacity,@vehiclePhoto)";


                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@modelName",
                    DbType = DbType.String,
                    Value = vehicle.modelName
                });

                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@brand",
                    DbType = DbType.String,
                    Value = vehicle.brand
                });
                cmd.Parameters.Add(
                    new MySqlParameter()
                    {
                        ParameterName = "@price",
                        DbType = DbType.Int32,
                        Value = vehicle.price.HasValue ? vehicle.price : 0
                    }
                );
                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@mileage",
                    DbType = DbType.String,
                    Value = vehicle.mileage
                });

                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@engine",
                    DbType = DbType.String,
                    Value = vehicle.engine
                });
                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@transmission",
                    DbType = DbType.String,
                    Value = vehicle.transmission
                });

                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@fuelType",
                    DbType = DbType.String,
                    Value = vehicle.fuelType
                });
                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@capacity",
                    DbType = DbType.String,
                    Value = vehicle.capacity
                });

                cmd.Parameters.Add(new MySqlParameter()
                {
                    ParameterName = "@vehiclePhoto",
                    DbType = DbType.String,
                    Value = vehicle.vehicleURI
                });


                await cmd.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;

            }

        }



        public async Task<List<VehicleModel>> GetAllVehicles()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "select * from vehicles";
            return await ReadAll(await cmd.ExecuteReaderAsync());
        }
        public async Task<VehicleModel> GetVehicle(int id)
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "select * from vehicles where Id = @id";
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = id
            });

            var result = await ReadAll(await cmd.ExecuteReaderAsync());
            return result.Count > 0 ? result[0] : null;
        }
        public async Task<List<VehicleModel>> ReadAll(MySqlDataReader reader)
        {

            List<VehicleModel> data = new List<VehicleModel>();

            using (reader)
            {
                while (await reader.ReadAsync())
                {
                    data.Add(new VehicleModel()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        modelName = reader["modelName"].ToString(),
                        brand = reader["brand"].ToString(),
                        price = Convert.ToInt32(reader["price"]),
                        mileage = reader["mileage"].ToString(),
                        engine = reader["engine"].ToString(),
                        transmission = reader["transmission"].ToString(),
                        fuelType = reader["fuelType"].ToString(),
                        capacity = reader["capacity"].ToString(),
                        vehicleURI = reader["vehiclePhoto"].ToString()
                    });

                }
            }


            return data;

        }

        // public VehicleModel GetVehicleById(int id)
        // {
        //     return DataSource().Where(x => x.Id == id).FirstOrDefault();
        // }




    }

}