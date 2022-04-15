using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TodoAPI.DataObjects;
using TodoAPI.Models;
using TodoAPI.Repository.Interfaces;

namespace TodoAPI.Controllers
{
    [ApiController]
    [Route("api/todo")] 
    public class TodoController : ControllerBase
    {

        private readonly ITodoRepository _repository;
        private readonly IMapper _mapper;

        public TodoController(ITodoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<IActionResult> Post(TodoDTO todo)
        {
            if (todo == null) return BadRequest("Dados Inválidos");

            var todoAdd= _mapper.Map<Todo>(todo);

            _repository.Add(todoAdd);

            return await _repository.SaveChangesAsync()
                ? Ok("Todo Adicionado com Sucesso")
                : BadRequest("Erro ao adiconar Tudo");

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var todo = await _repository.GetTodoByIdAsync (id);

            var todoData = _mapper.Map<TodoDTO>(todo);

            return todoData != null
                    ? Ok(todoData)
                    : BadRequest("Não achou nada");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TodoDTO todo)
        {
           

            return todo != null
                    ? Ok(todo)
                    : BadRequest("Não achou nada");

        }

        [HttpPut("{id}/update-status/")]
        public async Task<IActionResult> UpdateStatus(int id, TodoDTO todo)
        {


            return todo != null
                    ? Ok(todo)
                    : BadRequest("Não achou nada");

        }

        [HttpPut("{id}/update-sequence/")]
        public async Task<IActionResult> UpdateSequence(int id, TodoDTO todo)
        {


            return todo != null
                    ? Ok(todo)
                    : BadRequest("Não achou nada");

        }

    }
}
