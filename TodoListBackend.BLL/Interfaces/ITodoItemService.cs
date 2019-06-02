using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoListBackend.BLL.DTOs;

namespace TodoListBackend.BLL.Interfaces
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDTO>> GetAllAsync();
        Task<TodoItemDTO> GetByIdAsync(int id);
        Task<TodoItemDTO> CreateAsync(TodoItemDTO item);
        Task<TodoItemDTO> UpdateAsync(TodoItemDTO item);
        Task<TodoItemDTO> DeleteAsync(int id);
    }
}
