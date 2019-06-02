using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoListBackend.DAL.Entities;

namespace TodoListBackend.DAL.Interfaces
{
    public interface ITodoItemRepository
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem GetById(int id);
        TodoItem Create(TodoItem item);
        TodoItem Update(TodoItem item);
        void Delete(int id);
    }
}
