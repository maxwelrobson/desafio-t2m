using GerenciadorDeTarefas.Application;
using GerenciadorDeTarefas.Application.DTOs;
using Microsoft.AspNetCore.Mvc;



namespace GerenciadorDeTarefas.Api.Controllers
{
    [ApiController] //habilita comportamentos específicos para APIs
    [Route("api/[controller]")] //Define a rota base para o controller
    public class TarefasController : ControllerBase
    {
        //Injeção de dependência do serviço de tarefas
        private readonly ITarefaService _tarefaService;

        public TarefasController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        //GET: api/tarefas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tarefas = await _tarefaService.ObterTodasAsTarefasAsync();
            return Ok(tarefas); // Retorna 200 com a lista de tarefas
        }

        // GET: api/tarefas/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tarefa = await _tarefaService.ObterTarefaPorIdAsync(id);
            if (tarefa == null)
            {
                return NotFound(); // Retorna 404 se a tarefa não for encontrada
            }
            return Ok(tarefa); // Retorna 200 com a tarefa encontrada
        }

        //POST: api/tarefas
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CriarTarefaRequestDTO criarTarefaDto) //FromBody indica que o DTO vem no corpo da requisição, ele "converte" um JSON em um objeto C#
        {
            if (!ModelState.IsValid) 
            {
               return BadRequest(ModelState); // Retorna 400 se o modelo não for válido
            }

            var novaTarefa = await _tarefaService.CriarTarefaAsync(criarTarefaDto);

            return CreatedAtAction(nameof(GetById), new {id = novaTarefa.Id }, novaTarefa); // Retorna 201 com a nova tarefa
        }

        //PUT: api/taredas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AtualizarTarefaRequestDTO atualizarTarefaDto)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState); // Retorna 400 se o modelo não for válido
            }

            var tarefaAtualizada = await _tarefaService.AtualizarTarefaAsync(id, atualizarTarefaDto);
            if (tarefaAtualizada == null)
            { 
                return NotFound(); // Retorna 404 se a tarefa não for encontrada
            }

            return Ok(tarefaAtualizada); // Retorna 200 com a tarefa atualizada
        }

        //DELTE: api/tarefas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sucesso = await _tarefaService.RemoverTarefaAsync(id);
            if (!sucesso)
            {
                return NotFound(); // Retorna 404 se a tarefa não for encontrada
            }

            return NoContent(); // Retorna 204 sem conteúdo se a tarefa foi removida com sucesso
        }
            



    }
}
