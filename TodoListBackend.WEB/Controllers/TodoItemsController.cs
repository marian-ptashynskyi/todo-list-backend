using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoListBackend.BLL.Interfaces;
using TodoListBackend.BLL.DTOs;
using Microsoft.AspNetCore.Http;


namespace TodoListBackend.WEB.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _service;

        public TodoItemsController(ITodoItemService service)
        {
            _service = service;
        }

        // GET api/todoitems
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAllAsync()
        {
            try
            {
                IEnumerable<TodoItemDTO> items = await _service.GetAllAsync();

                if (items == null)
                {
                    return NotFound();
                }

                return StatusCode(200, items);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // GET api/todoitems/5
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetByIdAsync(int id)
        {
            try
            {
                TodoItemDTO item = await _service.GetByIdAsync(id);

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // POST api/todoitems
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostAsync(TodoItemDTO item)
        {
            try
            {
                await _service.CreateAsync(item);

                return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id }, item);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // PUT api/todoitems/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItemDTO>> PutAsync(int id, TodoItemDTO item)
        {
            try
            {
                if (id != item.Id)
                {
                    return BadRequest();
                }

                await _service.UpdateAsync(item);

                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        // DELETE api/todoitems/5
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(NullReferenceException), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Exception), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItemDTO>> DeleteAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return NoContent();
            }
            catch (NullReferenceException e)
            {
                return NotFound(e);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
