using System;
using Newtonsoft.Json;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;

namespace NotiPet.UnitTest.Fixtures.User
{
    public class UserRoleFixture:IBuilder
    {
        public static implicit operator UserRole(UserRoleFixture userRoleFixture) => userRoleFixture.Build();
        public static implicit operator UserRoleDto(UserRoleFixture userRoleFixture) => userRoleFixture.BuildDto();
        public UserRole Build() => new UserRole(_create,_update,_active,_username,_password,_email,_userId,_businessId,_roleId,_ratingId,_user);
        public UserRoleDto BuildDto() => JsonConvert.DeserializeObject<UserRoleDto>("");

        public UserRoleFixture WithCreate(DateTime create) => this.With(ref _create, create);
        public UserRoleFixture WithUpdate(DateTime update) => this.With(ref _update, update);
        public UserRoleFixture WithActive(bool active) => this.With(ref _active, active);
        public UserRoleFixture WithUsername(string username) => this.With(ref _username, username);
        public UserRoleFixture WithPassword(string password) => this.With(ref _password, password);
        
        public UserRoleFixture WithEmail(string email) => this.With(ref _email, email);
        public UserRoleFixture WithUserId(int userId) => this.With(ref _userId, userId);
        public UserRoleFixture WithBusinessId(int businessId) => this.With(ref _businessId, businessId);
        public UserRoleFixture WithRoleId(int roleId) => this.With(ref _roleId, roleId);
        public UserRoleFixture WithRatingId(int ratingId) => this.With(ref _ratingId, ratingId);
        public UserRoleFixture WithUser(Domain.Models.User user) => this.With(ref _user, user);
        private DateTime _create;
        private DateTime _update;
        private bool _active ;
        private string _username ;
        private string  _password;
        private string _email ;
        private int  _userId ;
        private int _businessId ;
        private int _roleId ;
        private int _ratingId ;
        private Domain.Models.User _user ;
    }
}