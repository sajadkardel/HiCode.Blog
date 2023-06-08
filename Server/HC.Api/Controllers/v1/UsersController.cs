using HC.Common.Exceptions;
using HC.DataAccess.Contracts;
using HC.Entity.Identity;
using HC.Infrastructure.Api;
using HC.Service;
using HC.Service.Services;
using HC.Shared.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HC.Api.Controllers.v1
{
    [ApiVersion("1")]
    public class UsersController : BaseController
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<User> _repository;

        public UsersController(IRepository<User> repository, IJwtService jwtService, UserManager<User> userManager)
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _repository = repository;
        }

        /// <summary>
        /// This method generate JWT Token
        /// </summary>
        /// <param name="tokenRequest">The information of token request</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public virtual async Task<ApiResult<AccessToken>> Token([FromForm] TokenRequest tokenRequest, CancellationToken cancellationToken)
        {
            if (!tokenRequest.grant_type.Equals("password", StringComparison.OrdinalIgnoreCase))
                throw new Exception("OAuth flow is not password.");

            //var user = await userRepository.GetByUserAndPass(username, password, cancellationToken);
            var user = await _userManager.FindByNameAsync(tokenRequest.username);
            if (user == null)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, tokenRequest.password);
            if (!isPasswordValid)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            var jwt = await _jwtService.GenerateAsync(user);
            return jwt;
        }

        /// <summary>
        /// Get User By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public virtual async Task<ApiResult<UserSelectDto>> GetByUserName([FromQuery] string userName, CancellationToken cancellationToken)
        {
            var model = await _repository.TableNoTracking.SingleOrDefaultAsync(p => p.UserName.Equals(userName), cancellationToken);

            UserSelectDto selectDto = new()
            {
                UserName = userName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Age = model.Age,
                Gender = model.Gender,
                IsActive = model.IsActive,
                LastLoginDate = model.LastLoginDate,
            };

            return selectDto;
        }

        /// <summary>
        /// Delete User By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("[action]")]
        public virtual async Task<IActionResult> DeleteByUserName([FromQuery] string userName, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(user1 => user1.UserName == userName, cancellationToken: cancellationToken);
            if (user is null) return NotFound();
            await _userManager.DeleteAsync(user);
            return Ok();
        }


        /// <summary>
        /// Update By UserName
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public virtual async Task<ApiResult<UserSelectDto>> UpdateByUserName([FromQuery] string userName, UserDto userDto, CancellationToken cancellationToken)
        {
            var model = await _userManager.Users.SingleOrDefaultAsync(user => user.UserName == userName, cancellationToken: cancellationToken);

            model = new()
            {
                UserName = userName,
                FullName = userDto.FullName,
                Email = userDto.Email,
                Age = userDto.Age,
                Gender = userDto.Gender,
            };

            await _repository.UpdateAsync(model, cancellationToken);

            UserSelectDto selectDto = new()
            {
                UserName = userName,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Age = model.Age,
                Gender = model.Gender,
                IsActive = model.IsActive,
                LastLoginDate = model.LastLoginDate,
            };

            return selectDto;
        }
    }
}
