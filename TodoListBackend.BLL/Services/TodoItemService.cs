using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoListBackend.BLL.DTOs;
using TodoListBackend.BLL.Interfaces;
using TodoListBackend.DAL.Entities;
using TodoListBackend.DAL.Interfaces;
using AutoMapper;

namespace TodoListBackend.BLL.Services
{
    public class TodoItemService : ITodoItemService
    {
        private ITodoItemAsyncRepository _repository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemAsyncRepository repo)
        {
            _repository = repo;
            _mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoItem, TodoItemDTO>()).CreateMapper();
        }

        public async Task<TodoItemDTO> CreateAsync(TodoItemDTO item)
        {
            TodoItem todoItem = _mapper.Map<TodoItemDTO, TodoItem>(item);
            return await _mapper.Map<Task<TodoItem>, Task<TodoItemDTO>>(_repository.CreateAsync(todoItem));
        }

        public async Task<TodoItemDTO> DeleteAsync(int id)
        {
            return await _mapper.Map<Task<TodoItem>, Task<TodoItemDTO>>(_repository.DeleteAsync(id));
        }

        public async Task<IEnumerable<TodoItemDTO>> GetAllAsync()
        {
            return await _mapper.Map<Task<IEnumerable<TodoItem>>, Task<IEnumerable<TodoItemDTO>>>(_repository.GetAllAsync());
        }

        public async Task<TodoItemDTO> GetByIdAsync(int id)
        {
            return await _mapper.Map<Task<TodoItem>, Task<TodoItemDTO>>(_repository.GetByIdAsync(id));
        }

        public async Task<TodoItemDTO> UpdateAsync(TodoItemDTO item)
        {
            TodoItem todoItem = _mapper.Map<TodoItemDTO, TodoItem>(item);
            return await _mapper.Map<Task<TodoItem>, Task<TodoItemDTO>>(_repository.UpdateAsync(todoItem));
        }
    }
}
