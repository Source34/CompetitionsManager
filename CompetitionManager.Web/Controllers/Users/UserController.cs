using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CompetitionManager.Core.Domains.Users;
using CompetitionManager.Web.Controllers.Users.Dto;
using CompetitionManager.Core.Domains.Users.Services;


namespace CompetitionManager.Web.Controllers.Users
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task CreateUser(CreateUpdateUserDto userDto, CancellationToken cancellationToken = default)
        {
            await _userService.CreateAsync(_mapper.Map<User>(userDto), cancellationToken);
        }

        [HttpPut("{id}")]
        public async Task UpdateUser(int id, CreateUpdateUserDto userDto, CancellationToken cancellationToken = default)
        {
            userDto.UserId = id;
            await _userService.UpdateAsync(_mapper.Map<User>(userDto), cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteUser(int id, CancellationToken cancellationToken = default)
        {
            await _userService.DeleteAsync(id, cancellationToken);
        }

        [HttpGet]
        public async Task<List<UserDto>> GetAllUsers(CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<UserDto>>(await _userService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUserById(int id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<UserDto>(await _userService.GetByIdAsync(id, cancellationToken));
        }

        [HttpGet("{id}/TotalPoint")]
        public async Task<double> GetUserTotalPointsById(int id, CancellationToken cancellationToken = default)
        {
            return await _userService.GetTotalResultPointsAsync(id, cancellationToken);
        }

        [HttpGet("byFio/{fio}")]
        public async Task<List<UserDto>> GetByFioUsers(string fio, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<List<UserDto>>(await _userService.GetByFioAsync(fio, cancellationToken));
        }
    }
}
