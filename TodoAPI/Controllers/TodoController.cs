using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DataObjects;
using TodoAPI.DataTransferObjects;
using TodoAPI.DataTransferObjects.Todo;
using TodoAPI.DataTransferObjects.User;
using TodoAPI.Models;
using TodoAPI.Repository.Interfaces;
using static TodoAPI.Helpers.EnumApp;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/todo")] 
    public class TodoController : ControllerBase
    {

        private readonly ITodoRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TodoController(ITodoRepository repository, IUserRepository userRepository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _userRepository = userRepository;
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(TodoAddDTO todo)
        {
            if (todo == null) return BadRequest(new ErrorDTO(false, "Dados inválidos"));

            var todoAdd= _mapper.Map<Todo>(todo);

            _repository.Add(todoAdd);

            return await _repository.SaveChangesAsync()
                ? Ok(_mapper.Map<TodoGetDTO>(todoAdd))
                : BadRequest(new ErrorDTO(false, "Erro ao criar Todo"));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _repository.GetTodoByIdAsync (id);

            var todoData = _mapper.Map<TodoGetDTO>(todo);

            todoData.Users = new List<UserGetDTO> ();
            foreach (var item in todo.TodoUsers)
            {
                UserGetDTO user = new UserGetDTO();
                user.Id = item.UserId;
                user.Name = item.User.Name;
                user.Email = item.User.Email;

                todoData.Users.Add(user) ;
            }

            return todoData != null
                    ? Ok(todoData)
                    : BadRequest(new ErrorDTO(false, "Registro não encontrado"));

        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, TodoPutDTO todo)
        {
            if (id <= 0) return BadRequest(new ErrorDTO(false, "Todo inválido"));

            var todoRepo = await _repository.GetTodoByIdAsync(id);

            if (todoRepo == null) return BadRequest(new ErrorDTO(false, "Todo não encontrado no banco de dados"));

            var todoUpdate = _mapper.Map(todoRepo, todo);

            _repository.Update(todoUpdate);

            return await _repository.SaveChangesAsync()
                ? Ok(_mapper.Map<TodoGetDTO>(todoUpdate))
                : BadRequest(new ErrorDTO(false, "Erro ao atualizar o Todo"));

        }

        [HttpPut("update-status/{id}")]
        [Authorize]
        public async Task<IActionResult> PutStatus(int id, TodoPutStatusDTO statusTodo)
        {
            if (id <= 0) return BadRequest(new ErrorDTO(false, "Todo inválido"));

            Todo todoRepo = await _repository.GetTodoByIdAsync(id);

            if (todoRepo == null) return BadRequest(new ErrorDTO(false, "Todo não encontrado no banco de dados"));

            todoRepo.Status = statusTodo.Status;

            _repository.Update(todoRepo);

            return await _repository.SaveChangesAsync()
                ? Ok(_mapper.Map<TodoGetDTO>(todoRepo))
                : BadRequest(new ErrorDTO(false, "Erro ao atualizar o Todo"));

        }

        [HttpPut("update-sequence/{id}")]
        [Authorize]
        public async Task<IActionResult> PutSequence(int id, TodoPutSequenceDTO sequence)
        {
            if (id <= 0) return BadRequest(new ErrorDTO(false, "Todo inválido"));

            Todo todoRepo = await _repository.GetTodoByIdAsync(id);

            if (todoRepo == null) return BadRequest(new ErrorDTO(false, "Todo não encontrado no banco de dados"));

            todoRepo.Sequence = sequence.Sequence;

            _repository.Update(todoRepo);

            return await _repository.SaveChangesAsync()
                ? Ok(_mapper.Map<TodoGetDTO>(todoRepo))
                : BadRequest(new ErrorDTO(false, "Erro ao atualizar o Todo"));

        }

        [HttpPost("add-user")]
        [Authorize]
        public async Task<IActionResult> PostUserTodo(TodoUserDTO todoUser)
        {
            int todoId = todoUser.IdTodo;
            int userId = todoUser.IdUser;

            if (todoId <= 0 || userId <= 0) return BadRequest(new ErrorDTO(false, "Dados inválidos"));

            var todoCheck = await _repository.GetTodoUser(todoId, userId);

            if (todoCheck != null) return Ok(new ErrorDTO(false, "Usuário já incluido a essa tarefa"));

            var todoData = new TodoUser
            {
                TodoId = todoId,
                UserId = userId
            };

            // Verifica se Existe o Usuário
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return Ok(new ErrorDTO(false, "Esse usuário não existe."));

            //Verifica se existe o Todo
            var todo = await _repository.GetTodoByIdAsync(todoId);
            if (todo == null) return Ok(new ErrorDTO(false, "Esse todo não existe."));

            _repository.Add(todoData);

            return await _repository.SaveChangesAsync()
                ? Ok(new { message = "Usuário adicionado com sucesso" })
                : BadRequest(new ErrorDTO(false, "Erro ao adicionar usuário na terefa"));
        }

        [HttpPost("remove-user")]
        [Authorize]
        public async Task<IActionResult> RemoveUserTodo(TodoUserDTO todoUser)
        {
            int todoId = todoUser.IdTodo;
            int userId = todoUser.IdUser;

            if (todoId <= 0 || userId <= 0) return BadRequest(new ErrorDTO(false, "Dados inválidos"));

            var todoCheck = await _repository.GetTodoUser(todoId, userId);

            if (todoCheck == null) return BadRequest(new ErrorDTO(false, "Usuário não existe nesse Todo"));
      
            _repository.Delete<TodoUser>(todoCheck);

            return await _repository.SaveChangesAsync()
                ? Ok(new { message = "Usuário removido com sucesso" })
                : BadRequest(new ErrorDTO(false, "Erro ao deletar usuário desse todo"));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest(new ErrorDTO(false, "Todo não Encontrado"));

            var userDelete = await _repository.GetTodoByIdAsync(id);

            if (userDelete == null) return NotFound(new ErrorDTO(false, "Todo não Encontrado"));

            _repository.Delete(userDelete);

            return await _repository.SaveChangesAsync()
                 ? Ok(new { message = "Todo deletado com sucesso." })
                 : BadRequest(new ErrorDTO(false, "Erro ao deletar Todo"));

        }

        [HttpGet("list")]
        public async Task<IActionResult> GetTodo()
        {
            var listTodo = await _repository.GetTodoAsync();

            List<TodoGetDTO> listResponse= new List<TodoGetDTO>();

            foreach (var todo in listTodo)
            {
                TodoGetDTO todoTemp = new TodoGetDTO();

                todoTemp.Id = todo.Id;
                todoTemp.Category = todo.Category;
                todoTemp.Title = todo.Title;
                todoTemp.Project = todo.Project;
                todoTemp.Description = todo.Description;
                todoTemp.CreatedDate = todo.CreatedDate;
                todoTemp.ExpectedDate = todo.ExpectedDate;
                todoTemp.Status = todo.Status;
                todoTemp.Sequence = todo.Sequence;
                todoTemp.Time = todo.Time;

                todoTemp.Users = new List<UserGetDTO>();
                foreach (var item in todo.TodoUsers)
                {
                    UserGetDTO user = new UserGetDTO();
                    user.Id = item.UserId;
                    user.Name = item.User.Name;
                    user.Email = item.User.Email;

                    todoTemp.Users.Add(user);
                }

                listResponse.Add(todoTemp);
            }

            return listResponse != null
                    ? Ok(listResponse)
                    : BadRequest(new ErrorDTO(false, "Registro não encontrado"));

        }

    }
}
