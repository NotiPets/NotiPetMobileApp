using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AutoMapper;
using DynamicData;
using ImTools;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class UserService:IUserService
    {
        private readonly IUserServiceApi _userServiceApi;
        private readonly IMapper _mapper;
        public UserService(IUserServiceApi userServiceApi,IMapper mapper)
        {
            _userServiceApi = userServiceApi;
            _mapper = mapper;
        }


        public IObservable<User> GetUserById(string username)
        {
            return _userServiceApi.GetUserById(username).
                    Select(_mapper.Map<User>);
        }
        
    }
}