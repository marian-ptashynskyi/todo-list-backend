using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoListBackend.DAL.Entities;
using TodoListBackend.DAL.Interfaces;

namespace TodoListBackend.DAL.Repositories
{
    public class TodoItemAsyncRepository : ITodoItemAsyncRepository
    {
        private EFTodoItemsContext _context;

        public TodoItemAsyncRepository(EFTodoItemsContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> CreateAsync(TodoItem item)
        {
            _context.Set<TodoItem>().Add(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<TodoItem> DeleteAsync(int id)
        {
            var todoItem = await _context.Set<TodoItem>().FindAsync(id);

            if (todoItem == null)
            {
                throw new NullReferenceException("TodoItem was not found");
            }

            _context.Set<TodoItem>().Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.Set<TodoItem>().ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(int id)
        {
            return await _context.Set<TodoItem>().FindAsync(id);
        }

        public async Task<TodoItem> UpdateAsync(TodoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return item;
        }
    }
}
