using System;

namespace ASEva
{
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 事件基础信息
    /// </summary>
    public class EventData
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 事件名称
        /// </summary>
        public String Name { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 所属session ID
        /// </summary>
        public DateTime Base { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:app=2.0.5) 在所属session中的时间偏置
        /// </summary>
        public double? Offset { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 事件发生时刻（本地时间，精确至毫秒）
        /// </summary>
        public DateTime TimeStampLocal { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 事件触发条件ID
        /// </summary>
        public String ConfigID { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 事件触发条件描述
        /// </summary>
        public String Description { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 事件对象
        /// </summary>
        public object EventHandle { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Record status of event session
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 事件session记录状态
    /// </summary>
    public enum EventSessionRecordStatus
    {
        /// \~English
        /// <summary>
        /// Disabled
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未启用事件session记录
        /// </summary>
        Disabled,
        
        /// \~English
        /// <summary>
        /// Recording
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 记录中
        /// </summary>
        Recording,

        /// \~English
        /// <summary>
        /// Recorded
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已记录至本地存储
        /// </summary>
        Recorded,
    }

    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 事件完整信息
    /// </summary>
    public class EventInfo
    {
        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 基础信息
        /// </summary>
        public EventData Data { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 注释
        /// </summary>
        public String Comment { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 根路径
        /// </summary>
        public String RootPath { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// info.xml路径
        /// </summary>
        public String InfoXmlDataPath { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 快照信号表路径
        /// </summary>
        public String SnapshotTableDataPath { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 快照城市尺度地图图像路径
        /// </summary>
        public String SnapshotLocationCityImagePath { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 快照街道尺度地图图像路径
        /// </summary>
        public String SnapshotLocationRoadImagePath { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 快照视频图像路径
        /// </summary>
        public String SnapshotVideoImagePath { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 快照俯瞰图图像路径
        /// </summary>
        public String SnapshotBirdViewImagePath { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 事件session记录路径
        /// </summary>
        public String SessionRecordPath { get; set; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// 事件session记录状态
        /// </summary>
        public EventSessionRecordStatus SessionRecordStatus { get; set; }
    }
}
