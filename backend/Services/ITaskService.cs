using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public interface ITaskService
    {
        Task<(IEnumerable<TaskItem>, int)> GetAllTasksAsync(int page, int pageSize);
        Task<TaskItem> GetTaskByIdAsync(int id);
        Task<TaskItem> AddTaskAsync(TaskItem taskItem);
        Task<bool> UpdateTaskAsync(TaskItem taskItem);
        Task<bool> DeleteTaskAsync(int id);
        Task<(IEnumerable<TaskItem>, int)> SearchTasksAsync(string description, int page, int pageSize);
    }
}