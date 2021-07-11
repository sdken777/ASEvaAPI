using System;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 独立任务状态
    /// </summary>
    public enum TaskState
	{
		/// 任务还未开始
		Unknown = 0,

		/// 任务完成
		Finished = 1,

		/// 任务失败或被中止
		Failed = 2,

		/// 任务进行中
		Progressing = 3,
	};

    /// <summary>
    /// (api:app=2.0.0) 独立任务运行结果
    /// </summary>
    public enum TaskResult
    {
        /// 运行成功
        RunOK = 0,

        /// 运行失败
        RunFailed = 1,

        /// 无法找到任务类别
        TaskClassNotFound = 2,

        /// 任务初始化失败
        TaskInitFailed = 3,

        /// 非空闲状态无法运行
        NotIdle = 4,
    }

    /// <summary>
    /// (api:app=2.0.0) 独立任务对象
    /// </summary>
    public class Task
    {
        /// <summary>
        /// [必须实现] 运行任务。本方法的调用线程与其他方法的不一致，需要注意线程安全
        /// </summary>
        /// <param name="config">配置的字符串描述</param>
        /// <param name="shouldEnd">是否应中断，若为true应无条件结束本方法</param>
        public virtual void RunTask(String config, ref bool shouldEnd) { }

        /// <summary>
        /// [可选实现] 获取任务运行状态
        /// </summary>
        /// <returns>任务运行状态</returns>
        public virtual TaskState GetState() { return TaskState.Unknown; }

        /// <summary>
        /// [可选实现] 获取任务状态描述
        /// </summary>
        /// <returns>任务状态描述</returns>
        public virtual String GetDescription() { return null; }

        /// <summary>
        /// [可选实现] 获取任务运行进度
        /// </summary>
        /// <returns>任务运行进度，单位%，范围0~100</returns>
        public virtual double GetProgress() { return 0; }

        /// <summary>
        /// [可选实现] 获取任务返回值信息
        /// </summary>
        /// <returns>任务返回值</returns>
        public virtual String GetReturnValue() { return null; }
    }
}
