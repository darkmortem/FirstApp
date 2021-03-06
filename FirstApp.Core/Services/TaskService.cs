﻿using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using System.Collections.Generic;

namespace FirstApp.Core.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void AddTaskToTable(TaskEntity task)
        {
            if (task.Id == default(int))
            {
                _taskRepository.Insert(task);

                return;
            }

            _taskRepository.Update(task);
        }

        public void DeleteTask(int taskId)
        {
            if (taskId < default(int))
            {
                return;
            }

            _taskRepository.DeleteTask(taskId);
        }

        public List<TaskEntity> GetListAllTasks()
        {
            return _taskRepository.GetAllTasks();
        }

        public List<TaskEntity> LoadListItemsTask(int userId)
        {
            return _taskRepository.GetUserTasks(userId);
        }
    }
}
