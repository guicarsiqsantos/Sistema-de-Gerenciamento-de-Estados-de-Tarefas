using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_Gerenciamento_de_Estados_de_Tarefas.Models;
using Sistema_de_Gerenciamento_de_Estados_de_Tarefas.Context;
using Sistema_de_Gerenciamento_de_Estados_de_Tarefas.Models.ENUM;

namespace Sistema_de_Gerenciamento_de_Estados_de_Tarefas.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase
	{
		private readonly TaskContext _context;

		public TasksController(TaskContext context)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<ActionResult<Tasks>> PostTask(Tasks tasks)
		{
			tasks.State = TaskState.Created; // Define o estado inicial como "Created"
			_context.Tasks.Add(tasks);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetTask", new { id = tasks.Id }, tasks);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Tasks>> GetTask(int id)
		{
			var tasks = await _context.Tasks.FindAsync(id);

			if (tasks == null)
			{
				return NotFound();
			}

			return tasks;
		}

		[HttpPut("{id}/start")]
		public async Task<IActionResult> StartTask(int id)
		{
			var tasks = await _context.Tasks.FindAsync(id);

			if (tasks == null)
			{
				return NotFound();
			}

			if (tasks.State == TaskState.Created) // Apenas permite alterar o estado se estiver "Created"
			{
				tasks.State = TaskState.InProgress;
				_context.Entry(tasks).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}
			else
			{
				return BadRequest("Task cannot be started from its current state.");
			}

			return NoContent();
		}

		[HttpPut("{id}/complete")]
		public async Task<IActionResult> CompleteTask(int id)
		{
			var tasks = await _context.Tasks.FindAsync(id);

			if (tasks == null)
			{
				return NotFound();
			}

			if (tasks.State == TaskState.InProgress) // Apenas permite alterar o estado se estiver "InProgress"
			{
				tasks.State = TaskState.Completed;
				_context.Entry(tasks).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}
			else
			{
				return BadRequest("Task cannot be completed from its current state.");
			}

			return NoContent();
		}

		[HttpPut("{id}/cancel")]
		public async Task<IActionResult> CancelTask(int id)
		{
			var tasks = await _context.Tasks.FindAsync(id);

			if (tasks == null)
			{
				return NotFound();
			}

			if (tasks.State == TaskState.Created || tasks.State == TaskState.InProgress) // Permite cancelar de "Created" ou "InProgress"
			{
				tasks.State = TaskState.Canceled;
				_context.Entry(tasks).State = EntityState.Modified;
				await _context.SaveChangesAsync();
			}
			else
			{
				return BadRequest("Task cannot be canceled from its current state.");
			}

			return NoContent();
		}
	}
}
