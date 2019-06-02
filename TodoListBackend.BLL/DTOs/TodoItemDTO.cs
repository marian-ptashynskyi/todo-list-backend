using System;
using System.Collections.Generic;
using System.Text;

namespace TodoListBackend.BLL.DTOs
{
    public class TodoItemDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
    }
}
