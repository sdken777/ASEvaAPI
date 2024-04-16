using System;

namespace ASEva
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Basic info of event
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 事件基础信息
    /// </summary>
    public class EventData
    {
        /// \~English
        /// <summary>
        /// Event name
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 事件名称
        /// </summary>
        public String Name { get; set; }

        /// \~English
        /// <summary>
        /// The session that event belongs to
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属session ID
        /// </summary>
        public DateTime Base { get; set; }

        /// \~English
        /// <summary>
        /// Time offset in the session
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 在所属session中的时间偏置
        /// </summary>
        public double? Offset { get; set; }

        /// \~English
        /// <summary>
        /// Triggered time (Local date and time, accurate to millisecond)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 事件发生时刻（本地时间，精确至毫秒）
        /// </summary>
        public DateTime TimeStampLocal { get; set; }

        /// \~English
        /// <summary>
        /// Trigger condition ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 事件触发条件ID
        /// </summary>
        public String ConfigID { get; set; }

        /// \~English
        /// <summary>
        /// Description of trigger condition
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 事件触发条件描述
        /// </summary>
        public String Description { get; set; }

        /// \~English
        /// <summary>
        /// Event handle
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 事件对象
        /// </summary>
        public object EventHandle { get; set; }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Record status of event session
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 事件session记录状态
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
    /// <summary>
    /// (api:app=3.0.0) Full info of event
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 事件完整信息
    /// </summary>
    public class EventInfo
    {
        /// \~English
        /// <summary>
        /// Basic info
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 基础信息
        /// </summary>
        public EventData Data { get; set; }

        /// \~English
        /// <summary>
        /// Comment
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 注释
        /// </summary>
        public String Comment { get; set; }

        /// \~English
        /// <summary>
        /// Root folder of event data
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 事件数据的根路径
        /// </summary>
        public String RootPath { get; set; }

        /// \~English
        /// <summary>
        /// Path of info.xml
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// info.xml路径
        /// </summary>
        public String InfoXmlDataPath { get; set; }

        /// \~English
        /// <summary>
        /// Path of snapshot signal table
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 快照信号表路径
        /// </summary>
        public String SnapshotTableDataPath { get; set; }

        /// \~English
        /// <summary>
        /// Path of snapshot city-level map image
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 快照城市尺度地图图像路径
        /// </summary>
        public String SnapshotLocationCityImagePath { get; set; }

        /// \~English
        /// <summary>
        /// Path of snapshot road-level map image
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 快照街道尺度地图图像路径
        /// </summary>
        public String SnapshotLocationRoadImagePath { get; set; }

        /// \~English
        /// <summary>
        /// Path of snapshot video image
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 快照视频图像路径
        /// </summary>
        public String SnapshotVideoImagePath { get; set; }

        /// \~English
        /// <summary>
        /// Path of snapshot bird view image
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 快照俯瞰图图像路径
        /// </summary>
        public String SnapshotBirdViewImagePath { get; set; }

        /// \~English
        /// <summary>
        /// Path of event session
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 事件session记录路径
        /// </summary>
        public String SessionRecordPath { get; set; }

        /// \~English
        /// <summary>
        /// Status of event session recording
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 事件session记录状态
        /// </summary>
        public EventSessionRecordStatus SessionRecordStatus { get; set; }
    }
}
