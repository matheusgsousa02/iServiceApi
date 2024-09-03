﻿using Dapper;
using iServiceRepositories.Repositories.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iServiceRepositories.Repositories
{
    public class EstablishmentEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public EstablishmentEmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        private async Task<MySqlConnection> OpenConnectionAsync()
        {
            var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            return connection;
        }

        public async Task<List<EstablishmentEmployee>> GetAsync()
        {
            using (var connection = await OpenConnectionAsync())
            {
                var queryResult = await connection.QueryAsync<EstablishmentEmployee>("SELECT * FROM EstablishmentEmployee");
                return queryResult.ToList();
            }
        }

        public async Task<EstablishmentEmployee> GetByIdAsync(int establishmentEmployeeId)
        {
            using (var connection = await OpenConnectionAsync())
            {
                return await connection.QueryFirstOrDefaultAsync<EstablishmentEmployee>(
                    "SELECT * FROM EstablishmentEmployee WHERE EstablishmentEmployeeId = @EstablishmentEmployeeId", new { EstablishmentEmployeeId = establishmentEmployeeId });
            }
        }

        public async Task<EstablishmentEmployee> InsertAsync(EstablishmentEmployee establishmentEmployeeModel)
        {
            using (var connection = await OpenConnectionAsync())
            {
                var id = await connection.QuerySingleAsync<int>(
                    "INSERT INTO EstablishmentEmployee (EstablishmentUserProfileId, Name, Document, DateOfBirth, EmployeeImage) VALUES (@EstablishmentUserProfileId, @Name, @Document, @DateOfBirth, @EmployeeImage); SELECT LAST_INSERT_ID();", establishmentEmployeeModel);
                return await GetByIdAsync(id);
            }
        }

        public async Task<EstablishmentEmployee> UpdateAsync(EstablishmentEmployee establishmentEmployeeUpdateModel)
        {
            using (var connection = await OpenConnectionAsync())
            {
                await connection.ExecuteAsync(
                    "UPDATE EstablishmentEmployee SET EstablishmentUserProfileId = @EstablishmentUserProfileId, Name = @Name, Document = @Document, DateOfBirth = @DateOfBirth, EmployeeImage = @EmployeeImage, LastUpdateDate = NOW() WHERE EstablishmentEmployeeId = @EstablishmentEmployeeId", establishmentEmployeeUpdateModel);
                return await GetByIdAsync(establishmentEmployeeUpdateModel.EstablishmentEmployeeId);
            }
        }

        public async Task<EstablishmentEmployee> UpdateImageAsync(int establishmentEmployeeId, string path)
        {
            using (var connection = await OpenConnectionAsync())
            {
                await connection.ExecuteAsync(
                    "UPDATE EstablishmentEmployee SET EmployeeImage = @EmployeeImage, LastUpdateDate = NOW() WHERE EstablishmentEmployeeId = @EstablishmentEmployeeId", new { EstablishmentEmployeeId = establishmentEmployeeId, EmployeeImage = path });
                return await GetByIdAsync(establishmentEmployeeId);
            }
        }

        public async Task SetActiveStatusAsync(int establishmentEmployeeId, bool isActive)
        {
            using (var connection = await OpenConnectionAsync())
            {
                await connection.ExecuteAsync(
                    "UPDATE EstablishmentEmployee SET Active = @IsActive WHERE EstablishmentEmployeeId = @EstablishmentEmployeeId", new { IsActive = isActive, establishmentEmployeeId = establishmentEmployeeId });
            }
        }

        public async Task SetDeletedStatusAsync(int establishmentEmployeeId, bool isDeleted)
        {
            using (var connection = await OpenConnectionAsync())
            {
                await connection.ExecuteAsync(
                    "UPDATE EstablishmentEmployee SET Deleted = @IsDeleted WHERE EstablishmentEmployeeId = @EstablishmentEmployeeId", new { IsDeleted = isDeleted, EstablishmentEmployeeId = establishmentEmployeeId });
            }
        }
    }
}
