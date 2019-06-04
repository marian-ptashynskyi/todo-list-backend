using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListBackend.BLL.DTOs;
using TodoListBackend.DAL.Entities;

namespace TodoListBackend.Test
{
    public static class DBContextMocker
    {
        public static EFTodoItemsContext GetMockedContext()
        {
            var options = new DbContextOptionsBuilder<EFTodoItemsContext>()
                .UseInMemoryDatabase("TodoItems")
                .Options;

            var dbContext = new EFTodoItemsContext(options);

            dbContext.Set<TodoItem>().Add(new TodoItem { Text = "Task 1", IsCompleted = true });
            dbContext.Set<TodoItem>().Add(new TodoItem { Text = "Task 2", IsCompleted = false });
            dbContext.Set<TodoItem>().Add(new TodoItem { Text = "Task 3", IsCompleted = false });
            dbContext.Set<TodoItem>().Add(new TodoItem { Text = "Task 4", IsCompleted = true });
            dbContext.Set<TodoItem>().Add(new TodoItem { Text = "Task 5", IsCompleted = false });

            return dbContext;
        }

        public static List<TodoItemDTO> GetAllDTOsList()
        {
            return new List<TodoItemDTO>()
            {
                new TodoItemDTO { Text = "Task 1", IsCompleted = true },
                new TodoItemDTO { Text = "Task 2", IsCompleted = false },
                new TodoItemDTO { Text = "Task 3", IsCompleted = false },
                new TodoItemDTO { Text = "Task 4", IsCompleted = true },
                new TodoItemDTO { Text = "Task 5", IsCompleted = false }
            };
        }
    }
}
