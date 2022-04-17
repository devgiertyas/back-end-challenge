using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DataObjects;
using TodoAPI.DataTransferObjects.User;
using TodoAPI.Helpers;
using TodoAPI.Models;
using TodoAPI.Repository.Interfaces;
using TodoAPI.Service;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(UserAddTDO user)
        {
            try
            {
                if (user == null) return BadRequest( new ErrorDTO (false, "Dados inválidos"));

                if (string.IsNullOrEmpty(user.Name))
                    return BadRequest(new ErrorDTO(false, "Informe o Nome"));

                if (string.IsNullOrEmpty(user.Email))
                    return BadRequest(new ErrorDTO(false, "Informe o E-mail"));

                //Check By Email
                if(await _repository.CheckUserByEmailAsync(user.Email))
                {
                    return BadRequest(new ErrorDTO(false, "E-mail já cadastrado"));
                }

                // Generate hash

                user.Password = CriptoHelper.GetMD5(user.Email + user.Password);

                var userAdd = _mapper.Map<User>(user);

                _repository.Add(userAdd);

                return await _repository.SaveChangesAsync()
                    ? Ok(_mapper.Map<UserGetDTO>(userAdd))
                    : BadRequest(new ErrorDTO(false, "Erro ao criar usuário"));

            }
            catch (Exception ex)
            {
               return  BadRequest(new ErrorDTO(false, ex.Message));
               
            }

        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repository.GetUserByIdAsync(id);

            var userData = _mapper.Map<UserGetDTO>(user);

            return userData != null
                    ? Ok(userData)
                    : BadRequest(new ErrorDTO(false, "Usuário não Encontrado"));

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UserPutDTO user)
        {
            if (id <= 0) return BadRequest(new ErrorDTO(false, "Usuário inválido"));

            var userRepo = await _repository.GetUserByIdAsync(id);

            if (userRepo == null) return BadRequest(new ErrorDTO(false, "Usuário não encontrado no banco de dados"));

            var userUpdate = _mapper.Map(user, userRepo);

            _repository.Update(userUpdate);

            return await _repository.SaveChangesAsync()
                ? Ok(_mapper.Map<UserGetDTO>(userRepo))
                : BadRequest(new ErrorDTO(false, "Erro ao atualizar o Usuário"));

        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest(new ErrorDTO(false, "Usuário não Encontrado"));

            var userDelete = await _repository.GetUserByIdAsync(id);

            if (userDelete == null) return NotFound(new ErrorDTO(false, "Usuário não Encontrado"));

            _repository.Delete(userDelete);

            return await _repository.SaveChangesAsync()
                 ? Ok(new {message=  "Usuário deletado com sucesso."})
                 : BadRequest(new ErrorDTO(false, "Erro ao deletar usuário"));

        }

        [HttpPost]
        [Route("auth")]
        public async Task<IActionResult> HandleLogin(LoginDTO login)
        {
            try
            {
                if (login == null) return BadRequest(new ErrorDTO(false, "Dados inválidos"));

                if (string.IsNullOrEmpty(login.Email))
                    return BadRequest(new ErrorDTO(false, "Informe o Nome"));

                if (string.IsNullOrEmpty(login.Password))
                    return BadRequest(new ErrorDTO(false, "Informe o E-mail"));

                User user = await _repository.GetUserByEmailAndPasswordAsync(login.Email, CriptoHelper.GetMD5(login.Email + login.Password));

                if (user == null) return BadRequest(new ErrorDTO(false, "E-mail ou Senha incorretos"));

                Token tokenOp = new Token();

                var token = tokenOp.GenerateToken(user);

                return Ok(new { user = user, token = token });

              
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDTO(false, "Erro ao fazer login " + ex.Message));

            }

        }

        [HttpGet("list")]
        public async Task<IActionResult> GetUser()
        {
            var listUser = await _repository.GetUserAsync();

            return listUser != null
                    ? Ok(listUser)
                    : BadRequest(new ErrorDTO(false, "Registro não encontrado"));

        }

    }
}
