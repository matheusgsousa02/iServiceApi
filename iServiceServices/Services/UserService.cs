﻿using iServiceRepositories.Repositories;
using iServiceRepositories.Repositories.Models;
using iServiceRepositories.Repositories.Models.Request;
using iServiceServices.Services.Models;
using Microsoft.Extensions.Configuration;

namespace iServiceServices.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(IConfiguration configuration)
        {
            _userRepository = new UserRepository(configuration);
        }

        public Result<List<User>> GetAllUsers()
        {
            try
            {
                var users = _userRepository.Get();
                return Result<List<User>>.Success(users);
            }
            catch (Exception ex)
            {
                return Result<List<User>>.Failure($"Falha ao obter os usuários: {ex.Message}");
            }
        }

        public Result<User> GetUserById(int userId)
        {
            try
            {
                var user = _userRepository.GetById(userId);

                if (user == null)
                {
                    return Result<User>.Failure("Usuário não encontrado.");
                }

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Falha ao obter o usuário: {ex.Message}");
            }
        }

        public Result<User> GetUserByEmail(string email)
        {
            try
            {
                var user = _userRepository.GetByEmail(email);

                if (user == null)
                {
                    return Result<User>.Failure("Usuário não encontrado.");
                }

                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Falha ao obter o usuário: {ex.Message}");
            }
        }

        public Result<User> AddUser(UserModel userModel)
        {
            try
            {
                var newUser = _userRepository.Insert(userModel);
                return Result<User>.Success(newUser);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Falha ao inserir o usuário: {ex.Message}");
            }
        }

        public Result<User> UpdateUser(User user)
        {
            try
            {
                var updatedUser = _userRepository.Update(user);
                return Result<User>.Success(updatedUser);
            }
            catch (Exception ex)
            {
                return Result<User>.Failure($"Falha ao atualizar o usuário: {ex.Message}");
            }
        }

        public Result<bool> DeleteUser(int userId)
        {
            try
            {
                bool success = _userRepository.Delete(userId);

                if (!success)
                {
                    return Result<bool>.Failure("Falha ao deletar o usuário ou usuário não encontrado.");
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Falha ao deletar o usuário: {ex.Message}");
            }
        }
    }
}