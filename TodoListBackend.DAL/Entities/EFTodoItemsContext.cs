using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListBackend.DAL.Entities
{
    public class EFTodoItemsContext: DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public EFTodoItemsContext(DbContextOptions<EFTodoItemsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
