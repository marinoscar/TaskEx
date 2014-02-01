using System;
using System.Linq;
using DataEx;
using TaskEx.Model;

namespace TaskEx
{
    public class TaskRunTimeRepository : ITaskRunTimeRepository
    {
        private readonly Database _db;

        public TaskRunTimeRepository()
        {
            _db = new Database();
        }

        public void Persist(TaskRunTime item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.SpecifiactionName) || string.IsNullOrWhiteSpace(item.TaskName)) return;
            var dbItem = Get(item.SpecifiactionName, item.TaskName);
            if(dbItem == null || dbItem.Id == 0)
                Insert(item);
            else
                Update(item);
        }

        public TaskRunTime GetFromStorage(string specificationName, string taskName)
        {
            var result =  Get(specificationName, taskName);
            if(result == null)
                return new TaskRunTime()
                    {
                        Id = 0, SpecifiactionName = specificationName, TaskName = taskName, UtcLastRunAt = DateTime.Parse("2000-01-01")
                    };
            return result;
        }

        private TaskRunTime Get(string specificationName, string taskName)
        {
            return
                _db.Select<TaskRunTime>(i => i.SpecifiactionName == specificationName && i.TaskName == taskName)
                   .FirstOrDefault();
        }

        private void Update(TaskRunTime taskRunTime)
        {
            _db.Update(taskRunTime);
        }

        private void Insert(TaskRunTime taskRunTime)
        {
            _db.Insert(taskRunTime);
        }
    }
}
