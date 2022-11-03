using System;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 事件基础信息
    /// </summary>
    public class EventData
    {
        /// <summary>
        /// 事件名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 所属session ID
        /// </summary>
        public DateTime Base { get; set; }

        /// <summary>
        /// (api:app=2.0.5) 在所属session中的时间偏置
        /// </summary>
        public double? Offset { get; set; }

        /// <summary>
        /// 事件发生时刻（本地时间，精确至毫秒）
        /// </summary>
        public DateTime TimeStampLocal { get; set; }

        /// <summary>
        /// 事件触发条件ID
        /// </summary>
        public String ConfigID { get; set; }

        /// <summary>
        /// 事件触发条件描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 事件对象
        /// </summary>
        public object EventHandle { get; set; }
    }

    /// <summary>
    /// (api:app=2.0.0) 事件session记录状态
    /// </summary>
    public enum EventSessionRecordStatus
    {
        /// <summary>
        /// 未启用事件session记录
        /// </summary>
        Disabled,
        
        /// <summary>
        /// 记录中
        /// </summary>
        Recording,

        /// <summary>
        /// 已记录至本地存储
        /// </summary>
        Recorded,
    }

    /// <summary>
    /// (api:app=2.0.0) 事件完整信息
    /// </summary>
    public class EventInfo
    {
        /// <summary>
        /// 基础信息
        /// </summary>
        public EventData Data { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public String Comment { get; set; }

        /// <summary>
        /// 根路径
        /// </summary>
        public String RootPath { get; set; }

        /// <summary>
        /// info.xml路径
        /// </summary>
        public String InfoXmlDataPath { get; set; }

        /// <summary>
        /// 快照信号表路径
        /// </summary>
        public String SnapshotTableDataPath { get; set; }

        /// <summary>
        /// 快照城市尺度地图图像路径
        /// </summary>
        public String SnapshotLocationCityImagePath { get; set; }

        /// <summary>
        /// 快照街道尺度地图图像路径
        /// </summary>
        public String SnapshotLocationRoadImagePath { get; set; }

        /// <summary>
        /// 快照视频图像路径
        /// </summary>
        public String SnapshotVideoImagePath { get; set; }

        /// <summary>
        /// 快照俯瞰图图像路径
        /// </summary>
        public String SnapshotBirdViewImagePath { get; set; }

        /// <summary>
        /// 事件session记录路径
        /// </summary>
        public String SessionRecordPath { get; set; }

        /// <summary>
        /// 事件session记录状态
        /// </summary>
        public EventSessionRecordStatus SessionRecordStatus { get; set; }
    }
}
