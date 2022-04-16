using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DataObjects;
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
        public async Task<IActionResult> Post(UserDTO user)
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
                    ? Ok(userAdd)
                    : BadRequest("Teste");

            }
            catch (Exception ex)
            {
               return  BadRequest("Erro ao adiconar Tudo" + ex.Message);
               
            }

        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _repository.GetUserByIdAsync(id);

            var todoData = _mapper.Map<UserDTO>(todo);

            return todoData != null
                    ? Ok(todoData)
                    : BadRequest("Não achou nada");

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, UserDTO todo)
        {


            return todo != null
                    ? Ok(todo)
                    : BadRequest("Não achou nada");

        }

        [HttpPut("patch")]
        [Authorize]
        public async Task<IActionResult> PutUsers(List<UserDTO> user)
        {


 
                   return BadRequest("Não achou nada");

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
                return BadRequest("Erro ao adiconar Tudo" + ex.Message);

            }

        }

    }
}
