#region Imports

using Application.Dto;
using Application.Helper;
using Application.RepositoryInterfaces;
using Application.ServiceInterfaces.IUserServices;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace Application.Services.UserServices
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public UserDto GetUserByID(int id)
        {
            var user = _usersRepository.GetUserByID(id);
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public UserDto GetByEmail(string email)
        {
            var user = _usersRepository.GetUserByEmail(email).Result;
            return user != null ? _mapper.Map<UserDto>(user) : null;
        }

        public UserDto DeleteUser(int id)
        {
            User userDeleted = _usersRepository.GetFirst(c => c.ID == id, c => !c.Active && !c.IsDeleted);
            if (userDeleted != null)
            {
                userDeleted.IsDeleted = true;
                _usersRepository.Complete();
            }
            else
            {
                throw new BadRequestException($"User '{id}' not found");
            }
            return _mapper.Map<UserDto>(userDeleted);
        }

        public async Task<UserDto> Authenticate(string email, string password)
        {
            var user = await _usersRepository.Authenticate(email, password);
            UserDto statusVm = _mapper.Map<UserDto>(user);
            return statusVm;
        }

        public async Task<UserDto> UpdateUserPatchAsync(int id, JsonPatchDocument userDocument)
        {
            var user = _usersRepository.GetUserByID(id);
            if (user != null)
            {
                userDocument.ApplyTo(user);
                _usersRepository.Complete();
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Upsert(UserUpsertDto userUpsertDto)
        {
            User user = null;
            if (userUpsertDto.ID == 0)
            {
                var userToAdd = new User()
                {
                    FirstName = userUpsertDto.FirstName,
                    LastName = userUpsertDto.LastName,
                    Password = userUpsertDto.Password,
                    Email = userUpsertDto.Email,
                    Active = true,
                    UserToRoles = new List<UserToRole>(){
                        new UserToRole
                        {
                             fk_RoleID = userUpsertDto.RoleID
                        }
                    }
                };
                if (userToAdd.Password.IsNotNullOrWhiteSpace())
                {
                    userToAdd.Password = userToAdd.Password.WithBCrypt();
                }
                await _usersRepository.Add(userToAdd);
                user = userToAdd;
            }
            else
            {
                var userToUpdate = _usersRepository.GetFirst(c => c.ID == userUpsertDto.ID, c => c.UserToRoles);
                if (userToUpdate != null)
                {
                    userToUpdate.FirstName = userUpsertDto.FirstName;
                    userToUpdate.LastName = userUpsertDto.LastName;
                    userToUpdate.Password = userUpsertDto.Password;
                    if (userToUpdate.UserToRoles.Count > 0)
                    {
                        userToUpdate.UserToRoles.First().fk_RoleID = userUpsertDto.RoleID;
                    }
                    else
                    {
                        userToUpdate.UserToRoles.Add(new UserToRole
                        {
                            fk_RoleID = userUpsertDto.RoleID
                        });
                    }

                    if (userToUpdate.Password.IsNotNullOrWhiteSpace())
                    {
                        userToUpdate.Password = userToUpdate.Password.WithBCrypt();
                    }

                    _usersRepository.Update(userToUpdate);
                }
                else
                {
                    throw new BadRequestException($"User '{userUpsertDto.ID}' not found");
                }
                user = userToUpdate;
            }
            _usersRepository.Complete();

            return _mapper.Map<UserDto>(_usersRepository.GetUserByID(user.ID));
        }

        public RecordSet<GetUserDto> GetUsers(UserSearchDto userSearchDto)
        {
            return _usersRepository.GetUsers(userSearchDto);
        }
    }
}