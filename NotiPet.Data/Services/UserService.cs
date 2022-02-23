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
    public class UserService:IUserService,IDisposable
    {
        private readonly IUserServiceApi _userServiceApi;
        private readonly IMapper _mapper;
        private SourceCache<UserRole, int> _sourceCache = new SourceCache<UserRole, int>(e=>e.UserId);
        public SourceCache<UserRole, int> UserRoleSource => _sourceCache;
        public UserService(IUserServiceApi userServiceApi,IMapper mapper)
        {
            _userServiceApi = userServiceApi;
            _mapper = mapper;
        }

        public IObservable<IEnumerable<UserRole>> GetVeterinarians()
        {

          return  _userServiceApi.GetVeterinarians()
                .Select(_mapper.Map<IEnumerable<UserRole>>)
                .Do(_sourceCache.AddOrUpdate);
        }



        public void Dispose()
        {
            _sourceCache?.Dispose();
        }
    }
}