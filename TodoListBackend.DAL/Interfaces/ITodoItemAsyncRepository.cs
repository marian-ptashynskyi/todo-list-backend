using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoListBackend.DAL.Entities;

namespace TodoListBackend.DAL.Interfaces
{
    public interface ITodoItemAsyncRepository
    {
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> GetByIdAsync(int id);
        Task<TodoItem> CreateAsync(TodoItem item);
        Task<TodoItem> UpdateAsync(TodoItem item);
        Task<TodoItem> DeleteAsync(int id);
    }
}
