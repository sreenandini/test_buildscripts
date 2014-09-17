namespace BMC.ExComms.Server.Executor
{
    using System;
    using System.Threading.Tasks;

    public static class FreeformTaskHelper
    {
        public static Task CreateTask(Action BodyMethod, TaskCreationOptions t_Options = TaskCreationOptions.None)
        {
            return new Task(BodyMethod, t_Options);
        }

        public static Task CreateTaskContinueWith(Action BodyMethod, Action ContinueMethod, TaskCreationOptions t_Options = TaskCreationOptions.None)
        {
            Task t_task = new Task(BodyMethod, t_Options);
            t_task.ContinueWith(_ => ContinueMethod);
            return t_task;
        }

        public static Task CreateAndExecuteTask(Action BodyMethod, TaskCreationOptions t_Options = TaskCreationOptions.None)
        {
            Task t_task = new Task(BodyMethod, t_Options);            
            t_task.Start();
            return t_task;
        }

        public static Task CreateAndExecuteTaskContinueWith(Action BodyMethod, Action ContinueMethod,  TaskCreationOptions t_Options = TaskCreationOptions.None)
        {
            Task t_task = new Task(BodyMethod, t_Options);
            t_task.ContinueWith(_ => ContinueMethod);
            t_task.Start();
            return t_task;
        }
    }
}
