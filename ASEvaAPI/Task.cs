using System;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Standalone task's status
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 独立任务状态
    /// </summary>
    public enum TaskState
	{
        /// \~English
        /// <summary>
        /// Not started yet
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 任务还未开始
        /// </summary>
		Unknown = 0,

        /// \~English
        /// <summary>
        /// Finished
        /// </summary> 
        /// \~Chinese
        /// <summary>
        /// 任务完成
        /// </summary>      
		Finished = 1,

        /// \~English
        /// <summary>
        /// Failed or canceled
        /// </summary>  
        /// \~Chinese
        /// <summary>
        /// 任务失败或被中止
        /// </summary>        
		Failed = 2,

        /// \~English
        /// <summary>
        /// Task is running
        /// </summary> 
        /// \~Chinese
        /// <summary>
        /// 任务进行中
        /// </summary>       
		Progressing = 3,
	};

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Standalone task's result
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 独立任务运行结果
    /// </summary>
    public enum TaskResult
    {
        /// \~English
        /// <summary>
        /// Success
        /// </summary> 
        /// \~Chinese
        /// <summary>
        /// 运行成功
        /// </summary>  
        RunOK = 0,

        /// \~English
        /// <summary>
        /// Failed
        /// </summary>  
        /// \~Chinese
        /// <summary>
        /// 运行失败
        /// </summary>  
        RunFailed = 1,

        /// \~English
        /// <summary>
        /// Can't find task by ID
        /// </summary> 
        /// \~Chinese
        /// <summary>
        /// 无法找到任务类别
        /// </summary> 
        TaskClassNotFound = 2,

        /// \~English
        /// <summary>
        /// Failed to initialize the task
        /// </summary> 
        /// \~Chinese
        /// <summary>
        /// 任务初始化失败
        /// </summary> 
        TaskInitFailed = 3,

        /// \~English
        /// <summary>
        /// Can't run because the application status is not idle
        /// </summary> 
        /// \~Chinese
        /// <summary>
        /// 非空闲状态无法运行
        /// </summary> 
        NotIdle = 4,

        /// \~English
        /// <summary>
        /// (api:app=3.4.4) Unknown (For circumstances like getting failed)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=3.4.4) 未知（获取失败等情况）
        /// </summary>
        Unknown = 5,
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Standalone task object
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 独立任务对象
    /// </summary>
    public class StandaloneTask
    {
        /// \~English
        /// <summary>
        /// [Required] Run the task. The thread calling this function is different from others, be aware of multithread safety
        /// </summary>
        /// <param name="config">Configuration string</param>
        /// <param name="shouldEnd">Whether to abort, if it's true the function should return immediately</param>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 运行任务。本方法的调用线程与其他方法的不一致，需要注意线程安全
        /// </summary>
        /// <param name="config">配置的字符串描述</param>
        /// <param name="shouldEnd">是否应中断，若为true应无条件结束本方法</param>
        public virtual void RunTask(String config, ref bool shouldEnd) { }

        /// \~English
        /// <summary>
        /// [Optional] Get task's state
        /// </summary>
        /// <returns>Task's state</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取任务运行状态
        /// </summary>
        /// <returns>任务运行状态</returns>
        public virtual TaskState GetState() { return TaskState.Unknown; }

        /// \~English
        /// <summary>
        /// [Optional] Get description of task's state
        /// </summary>
        /// <returns>Description of task's state</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取任务状态描述
        /// </summary>
        /// <returns>任务状态描述</returns>
        public virtual String GetDescription() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Get progress
        /// </summary>
        /// <returns>Progress in percentages, ranges 0~100</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取任务运行进度
        /// </summary>
        /// <returns>任务运行进度，单位%，范围0~100</returns>
        public virtual double GetProgress() { return 0; }

        /// \~English
        /// <summary>
        /// [Optional] Get return value
        /// </summary>
        /// <returns>The return value</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取任务返回值信息
        /// </summary>
        /// <returns>任务返回值</returns>
        public virtual String GetReturnValue() { return null; }
    }
}
