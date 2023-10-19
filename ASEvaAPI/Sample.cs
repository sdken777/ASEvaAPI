using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=2.7.0) Sync status of time offset
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.7.0) 时间偏置同步状态
    /// </summary>
    public enum TimeOffsetSync
    {
        /// \~English
        /// <summary>
        /// Not synchronized or the sync source is unknown
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 未同步或同步源未知
        /// </summary>
		None = 0,

        /// \~English
        /// <summary>
        /// Synchronized with time server
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已与授时服务器时间同步
        /// </summary>
		Server = 1,

        /// \~English
        /// <summary>
        /// Synchronized with satellite time
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 已与卫星时间同步
        /// </summary>
		Gnss = 2,
    }

    /// \~English
    /// <summary>
    /// (api:app=2.7.0) Session ID
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.7.0) Session标识符
    /// </summary>
    public struct SessionIdentifier
    {
        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public SessionIdentifier(int year, int month, int day, int hour, int minute, int second)
        {
            if (year >= 1900 && year < 10000 && month >= 1 && month <= 12 && day >= 1 && day <= 31 &&
		        hour >= 0 && hour < 24 && minute >= 0 && minute < 60 && second >= 0 && second < 60)
            {
                ulong yearCode = (ulong)year << 48;
                ulong monthCode = (ulong)month << 40;
                ulong dayCode = (ulong)day << 32;
                ulong hourCode = (ulong)hour << 16;
                ulong minuteCode = (ulong)minute << 8;
                ulong secondCode = (ulong)second;
                value = yearCode | monthCode | dayCode | hourCode | minuteCode | secondCode;
            }
            else value = 0;
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.2) Create from string
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.2) 通过字符串创建
        /// </summary>
        public static SessionIdentifier FromString(String str)
        {
            DateTime dateTime;
            if (DateTime.TryParseExact(str, "yyyy-MM-dd-HH-mm-ss", null, DateTimeStyles.None, out dateTime))
            {
                return FromDateTime(dateTime);
            }
            else return new SessionIdentifier(0, 0, 0, 0, 0, 0);
        }

        /// \~English
        /// <summary>
        /// Create by date and time
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 通过日期时间创建
        /// </summary>
        public static SessionIdentifier FromDateTime(DateTime dateTime)
        {
            return new SessionIdentifier(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.3) Whether it's valid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.3) 是否有效
        /// </summary>
        public bool IsValid()
        {
            return value != 0;
        }

        /// \~English
        /// <summary>
        /// Convert to date and time
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 转为日期时间
        /// </summary>
        public DateTime ToDateTime()
        {
            if (value == 0) return new DateTime(1900, 1, 1, 0, 0, 0);
            else
            {
                int year = (int)((value & 0xffff000000000000) >> 48);
                int month = (int)((value & 0x0000ff0000000000) >> 40);
                int day = (int)((value & 0x000000ff00000000) >> 32);
                int hour = (int)((value & 0x0000000000ff0000) >> 16);
                int minute = (int)((value & 0x000000000000ff00) >> 8);
                int second = (int)(value & 0x00000000000000ff);
                return new DateTime(year, month, day, hour, minute, second);
            }
        }

        public override string ToString()
        {
            if (value == 0) return "1900-01-01-00-00-00";
            else
            {
                var dateTime = ToDateTime();
                return String.Format("{0:D4}-{1:D2}-{2:D2}-{3:D2}-{4:D2}-{5:D2}", dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
            }
        }

        public override bool Equals(object obj)
        {
            var target = (SessionIdentifier)obj;
            return value == target.value;
        }

        public override int GetHashCode()
        {
            return (int)value;
        }

        public static bool operator ==(SessionIdentifier id1, SessionIdentifier id2)
        {
            return id1.value == id2.value;
        }

        public static bool operator !=(SessionIdentifier id1, SessionIdentifier id2)
        {
            return id1.value != id2.value;
        }

        public static bool operator <(SessionIdentifier id1, SessionIdentifier id2)
        {
            return id1.value < id2.value;
        }

        public static bool operator >(SessionIdentifier id1, SessionIdentifier id2)
        {
            return id1.value > id2.value;
        }

        private ulong value;
    }

    /// \~English
    /// <summary>
    /// (api:app=2.7.0) Session independent time info
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.7.0) Session无关时间信息
    /// </summary>
    public class IndependentTimeInfo
    {
        /// \~English
        /// <summary>
        /// Time offset synchronization status
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 时间偏置同步状态
        /// </summary>
        public TimeOffsetSync OffsetSync { get { return offsetSync; }}

        /// \~English
        /// <summary>
        /// CPU tick while data arrived, 0 means invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 到达时CPU计数，0表示无效
        /// </summary>
        public ulong CPUTick { get { return cpuTick; }}

        /// \~English
        /// <summary>
        /// Host machine's posix time while data arrived, in nanoseconds, 0 means invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 到达时主机Posix时间，单位纳秒，0表示无效
        /// </summary>
        public ulong HostPosix { get { return hostPosix; }}

        /// \~English
        /// <summary>
        /// Guest machine's posix time while data sampling, in nanoseconds, 0 means invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 采样时客机Posix时间，单位纳秒，0表示无效
        /// </summary>
        public ulong GuestPosix { get { return guestPosix; }}

        /// \~English
        /// <summary>
        /// Time server's posix time while data sampling, in nanoseconds, 0 means invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 采样时授时服务器Posix时间，单位纳秒，0表示无效
        /// </summary>
        public ulong ServerPosix { get { return serverPosix; }}

        /// \~English
        /// <summary>
        /// Satellite posix time while data sampling, in nanoseconds, 0 means invalid
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 采样时卫星Posix时间，单位纳秒，0表示无效
        /// </summary>
        public ulong GNSSPosix { get { return gnssPosix; }}

        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IndependentTimeInfo(TimeOffsetSync offsetSync, ulong cpuTick, ulong hostPosix, ulong guestPosix, ulong serverPosix, ulong gnssPosix)
        {
            this.offsetSync = offsetSync;
            this.cpuTick = cpuTick;
            this.hostPosix = hostPosix;
            this.guestPosix = guestPosix;
            this.serverPosix = serverPosix;
            this.gnssPosix = gnssPosix;
        }

        private TimeOffsetSync offsetSync;
        private ulong cpuTick, hostPosix, guestPosix, serverPosix, gnssPosix;
    }

    /// \~English
    /// <summary>
    /// (api:app=2.7.0) Timestamp
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.7.0) 时间戳
    /// </summary>
    public struct Timestamp
    {
        /// \~English
        /// <summary>
        /// Session ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Session标识符
        /// </summary>
        public SessionIdentifier Session { get { return session; }}

        /// \~English
        /// <summary>
        /// Time offset, in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 时间偏置，单位秒
        /// </summary>
        public double Offset { get { return offset; }}

        /// \~English
        /// <summary>
        /// Session independent time info
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// Session无关时间信息
        /// </summary>
        public IndependentTimeInfo TimeInfo { get { return timeInfo; }}

        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Timestamp(SessionIdentifier session, double offset, IndependentTimeInfo timeInfo)
        {
            this.session = session;
            this.offset = offset;
            this.timeInfo = timeInfo;
        }

        private SessionIdentifier session;
        private double offset;
        private IndependentTimeInfo timeInfo;
    }

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Base class of sample data
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 数据样本基类
    /// </summary>
    public class Sample : IComparable<Sample>
    {
        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Timestamp
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 时间戳
        /// </summary>
        public Timestamp Timestamp
        {
            get { return timestamp; }
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.2) Session belonged
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.2) 所属Session标识符
        /// </summary>
        public SessionIdentifier Session
        {
            get { return timestamp.Session; }
        }

        /// \~English
        /// <summary>
        /// Session belonged (Be aware that "set" operation will clear session independent time info)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 所属session ID（注意，set操作将清除Session无关时间信息）
        /// </summary>
        public DateTime Base
        {
            get { return timestamp.Session.ToDateTime(); }
            set { timestamp = new Timestamp(SessionIdentifier.FromDateTime(value), timestamp.Offset, null); }
        }

        /// \~English
        /// <summary>
        /// Time offset, in seconds (Be aware that "set" operation will clear session independent time info)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 时间偏置，单位秒（注意，set操作将清除Session无关时间信息）
        /// </summary>
        public double Offset
        {
            get { return timestamp.Offset; }
            set { timestamp = new Timestamp(timestamp.Session, value, null); }
        }

        /// \~English
        /// <summary>
        /// Timeline point, in seconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 在时间线上的位置，单位秒
        /// </summary>
        public double Timeline
        {
            get { return timeline; }
            set { timeline = value; }
        }

        /// \~English
        /// <summary>
        /// [Agency dependency] Calculate local date and time based on host machine posix time model
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [依赖Agency] 获取通过主机Posix时间模型计算得到的本地时间
        /// </summary>
        public DateTime? TimestampLocal
        {
            get
            {
                return Agency.GetLocalDateTime(Base, Offset, false);
            }
        }

        /// \~English
        /// <summary>
        /// [Agency dependency] Calculate UTC date and time based on satellite posix time model
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [依赖Agency] 获取通过卫星Posix时间模型得到的计算UTC时间
        /// </summary>
        public DateTime? TimestampUTC
        {
            get
            {
                return Agency.GetUTCDateTime(Base, Offset, true);
            }
        }

        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Sample()
        {
            timestamp = new Timestamp(new SessionIdentifier(0, 0, 0, 0, 0, 0), 0, null);
            timeline = 0;
        }

        /// \~English
        /// <summary>
        /// Constructor based on time information
        /// </summary>
        /// <param name="session">Session belonged</param>
        /// <param name="offset">Time offset, in seconds</param>
        /// <param name="timeline">Timeline point</param>
        /// \~Chinese
        /// <summary>
        /// 按指定时间信息进行初始化
        /// </summary>
        /// <param name="session">所属session ID</param>
        /// <param name="offset">时间偏置，单位秒</param>
        /// <param name="timeline">在时间线上的位置</param>
        public Sample(DateTime session, double offset, double timeline)
        {
            timestamp = new Timestamp(SessionIdentifier.FromDateTime(session), offset, null);
            this.timeline = timeline;
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Constructor based on time information
        /// </summary>
        /// <param name="session">Session belonged</param>
        /// <param name="offset">Time offset, in seconds</param>
        /// <param name="timeInfo">Session independent time info</param>
        /// <param name="timeline">Timeline point</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 按指定时间信息进行初始化
        /// </summary>
        /// <param name="session">所属session ID</param>
        /// <param name="offset">时间偏置，单位秒</param>
        /// <param name="timeInfo">Session无关时间信息</param>
        /// <param name="timeline">在时间线上的位置</param>
        public Sample(DateTime session, double offset, IndependentTimeInfo timeInfo, double timeline)
        {
            timestamp = new Timestamp(SessionIdentifier.FromDateTime(session), offset, timeInfo);
            this.timeline = timeline;
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Set time information
        /// </summary>
        /// <param name="session">Session belonged</param>
        /// <param name="offset">Time offset, in seconds</param>
        /// <param name="timeInfo">Session independent time info</param>
        /// <param name="timeline">Timeline point</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 设置当前样本的时间戳和时间线位置
        /// </summary>
        /// <param name="session">所属session ID</param>
        /// <param name="offset">时间偏置，单位秒</param>
        /// <param name="timeInfo">Session无关时间信息</param>
        /// <param name="timeline">在时间线上的位置</param>
        public void SetTime(DateTime session, double offset, IndependentTimeInfo timeInfo, double timeline)
        {
            timestamp = new Timestamp(SessionIdentifier.FromDateTime(session), offset, timeInfo);
            this.timeline = timeline;
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.2) Set time information
        /// </summary>
        /// <param name="timestamp">Timestamp</param>
        /// <param name="timeline">Timeline point</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.2) 设置当前样本的时间戳和时间线位置
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <param name="timeline">在时间线上的位置</param>
        public void SetTime(Timestamp timestamp, double timeline)
        {
            this.timestamp = timestamp;
            this.timeline = timeline;
        }

        /// \~English
        /// <summary>
        /// (api:app=2.7.0) Set time to the same as the sample
        /// </summary>
        /// <param name="timeRef">Time reference sample</param>
        /// \~Chinese
        /// <summary>
        /// (api:app=2.7.0) 按时间参考样本设置当前样本的时间戳和时间线位置
        /// </summary>
        /// <param name="timeRef">时间参考样本</param>
        public void SetTime(Sample timeRef)
        {
            timestamp = timeRef.timestamp;
            timeline = timeRef.timeline;
        }

        /// \~English
        /// <summary>
        /// Compare based on timeline point
        /// </summary>
        /// <param name="other">Another sample</param>
        /// <returns>Result</returns>
        /// \~Chinese
        /// <summary>
        /// 按时间线进行比较
        /// </summary>
        /// <param name="other">另一个数据样本</param>
        /// <returns>比较结果</returns>
        public int CompareTo(Sample other)
        {
            return Timeline.CompareTo(other.Timeline);
        }

        /// \~English
        /// <summary>
        /// Result of searching in sample buffer
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 对样本缓存的搜索结果
        /// </summary>
        public class SearchResult
        {
            public int s1Index { get; set; }
            public int s2Index { get; set; }
            public Sample s1 { get; set; }
            public Sample s2 { get; set; }
            public double w1 { get; set; }
            public double w2 { get; set; }
        }

        /// \~English
        /// <summary>
        /// [Required for conversion of general sample] The protocol of ASEva.GeneralSample after converted
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [支持通用样本转换时必须实现] 特化样本转为 ASEva.GeneralSample 的协议名称
        /// </summary>
        public virtual String GetGeneralSampleProtocol()
        {
            return null;
        }

        /// \~English
        /// <summary>
        /// [Required for conversion of general sample] Supported protocols of ASEva.GeneralSample that can be converted
        /// </summary>
        /// <returns>Supported protocols</returns>
        /// \~Chinese
        /// <summary>
        /// [支持通用样本转换时必须实现] 从 ASEva.GeneralSample 转为特化样本时支持的协议名称列表
        /// </summary>
        /// <returns>支持的协议名称列表</returns>
        public virtual String[] GetGeneralSampleProtocols()
        {
            return null;
        }

        /// \~English
        /// <summary>
        /// [Required for conversion of general sample] Convert from ASEva.GeneralSample , you should use SetTime to copy the time info
        /// </summary>
        /// <param name="sample">General sample</param>
        /// <returns>Whether conversion is successful</returns>
        /// \~Chinese
        /// <summary>
        /// [支持通用样本转换时必须实现] 从 ASEva.GeneralSample 转为特化样本，该实现需调用SetTime进行时间拷贝
        /// </summary>
        /// <param name="sample">通用样本</param>
        /// <returns>转换是否成功</returns>
        public virtual bool FromGeneralSample(GeneralSample sample)
        {
            return false;
        }

        /// \~English
        /// <summary>
        /// [Required for conversion of general sample] Convert to ASEva.GeneralSample , you should use SetTime to copy the time info
        /// </summary>
        /// <returns>General sample</returns>
        /// \~Chinese
        /// <summary>
        /// [支持通用样本转换时可选实现] 特化样本转为 ASEva.GeneralSample ，该实现需调用SetTime进行时间拷贝
        /// </summary>
        /// <returns>通用样本</returns>
        public virtual GeneralSample ToGeneralSample()
        {
            return null;
        }

        /// \~English
        /// <summary>
        /// [Required for conversion of general sample] Convert to ASEva.GeneralSample , you should use SetTime to copy the time info and set the channel
        /// </summary>
        /// <returns>General sample</returns>
        /// \~Chinese
        /// <summary>
        /// [支持通用样本转换时可选实现] 特化样本转为 ASEva.GeneralSample ，该实现需调用SetTime进行时间拷贝并赋值通道
        /// </summary>
        /// <returns>通用样本</returns>
        public virtual GeneralSample ToGeneralSample(int channel)
        {
            return null;
        }

        /// \~English
        /// <summary>
        /// [Optional] Get whether sample interpolation is supported
        /// </summary>
        /// <returns>Whether sample interpolation is supported, default is false</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 返回是否支持样本间插值
        /// </summary>
        /// <returns>是否支持样本间插值，默认为false</returns>
        public virtual bool SupportInterpolation()
        {
            return false;
        }

        /// \~English
        /// <summary>
        /// [Required if SupportInterpolation returns true] Implement sample interpolation based on the result of ASEva.Sample.Search , no need to output time info
        /// </summary>
        /// <param name="input">Result of searching in sample buffer</param>
        /// <returns>Interpolated sample</returns>
        /// \~Chinese
        /// <summary>
        /// [SupportInterpolation返回true时必须实现] 基于 ASEva.Sample.Search 返回的搜索结果进行样本插值，无需输出时间信息
        /// </summary>
        /// <param name="input">样本缓存的搜索结果</param>
        /// <returns>返回插值后的样本</returns>
        protected virtual Sample Interpolate(SearchResult input)
        {
            return null;
        }

        /// \~English
        /// <summary>
        /// Get whether the target timeline point is inside the sample buffer's range
        /// </summary>
        /// <param name="samples">Sample buffer</param>
        /// <param name="targetTimeline">Timeline point</param>
        /// <returns>Whether it's in the range</returns>
        /// \~Chinese
        /// <summary>
        /// 判断目标时间点是否在样本缓存范围内
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">在时间线上的目标时间点</param>
        /// <returns>是否在范围内</returns>
        public static bool IsInRange(List<Sample> samples, double targetTimeline)
        {
            if (samples == null || samples.Count < 2) return false;
            return targetTimeline >= samples[0].Timeline && targetTimeline <= samples.Last().Timeline;
        }

        /// \~English
        /// <summary>
        /// Get whether the target timeline point is outside the sample buffer's range, and earlier than all samples
        /// </summary>
        /// <param name="samples">Sample buffer</param>
        /// <param name="targetTimeline">Timeline point</param>
        /// <returns>Whether it's out of the range, and earlier than all samples</returns>
        /// \~Chinese
        /// <summary>
        /// 判断目标时间点是否在样本缓存范围外，且比所有样本都更早
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">在时间线上的目标时间点</param>
        /// <returns>是否在范围外，且比所有样本都更早</returns>
        public static bool IsOutRangeLower(List<Sample> samples, double targetTimeline)
        {
            if (samples == null || samples.Count == 0) return false;
            return targetTimeline < samples[0].Timeline;
        }

        /// \~English
        /// <summary>
        /// Get whether the target timeline point is outside the sample buffer's range, and later than all samples
        /// </summary>
        /// <param name="samples">Sample buffer</param>
        /// <param name="targetTimeline">Timeline point</param>
        /// <returns>Whether it's out of the range, and later than all samples</returns>
        /// \~Chinese
        /// <summary>
        /// 判断目标时间点是否在样本缓存范围外，且比所有样本都更晚
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">在时间线上的目标时间点</param>
        /// <returns>是否在范围外，且比所有样本都更晚</returns>
        public static bool IsOutRangeUpper(List<Sample> samples, double targetTimeline)
        {
            if (samples == null || samples.Count == 0) return false;
            return targetTimeline > samples.Last().Timeline;
        }

        private static int[] searchSample(List<Sample> samples, int middle, int left, int right, double interestTime)
        {
            if (left == middle) return new int[2] { middle, right };
            if (right == middle) return new int[2] { left, middle };
            if (samples[middle].Timeline > interestTime)
            {
                return searchSample(samples, (left + middle) / 2, left, middle, interestTime);
            }
            else
            {
                return searchSample(samples, (right + middle) / 2, middle, right, interestTime);
            }
        }

        /// \~English
        /// <summary>
        /// Search in the sample buffer
        /// </summary>
        /// <param name="samples">Sample buffer</param>
        /// <param name="targetTimeline">Timeline point</param>
        /// <param name="maxGap">Max time gap. The samples will be ignored if the interval between is larger than the time gap</param>
        /// <returns>Result</returns>
        /// \~Chinese
        /// <summary>
        /// 按目标时间点搜索缓存列表
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">在时间线上的目标时间点</param>
        /// <param name="maxGap">最大时间间隔，若最近样本的时间间隔大于此值，则不考虑</param>
        /// <returns>返回搜索结果</returns>
        public static SearchResult Search(List<Sample> samples, double targetTimeline, double maxGap = 1.0)
        {
            if (samples == null || samples.Count == 0) return null;

            var result = new SearchResult();
            if (samples.Count == 1)
            {
                if (Math.Abs(targetTimeline - samples[0].Timeline) > maxGap) return null;
                else
                {
                    result.s1Index = result.s2Index = 0;
                    result.s1 = result.s2 = samples[0];
                    result.w1 = 1;
                    result.w2 = 0;
                    return result;
                }
            }

            if (targetTimeline > samples[samples.Count - 1].Timeline + maxGap) return null;
            if (targetTimeline < samples[0].Timeline - maxGap) return null;

            if (targetTimeline > samples[samples.Count - 1].Timeline)
            {
                result.s1Index = result.s2Index = samples.Count - 1;
                result.s1 = result.s2 = samples[samples.Count - 1];
                result.w1 = 1;
                result.w2 = 0;
                return result;
            }
            if (targetTimeline < samples[0].Timeline)
            {
                result.s1Index = result.s2Index = 0;
                result.s1 = result.s2 = samples[0];
                result.w1 = 1;
                result.w2 = 0;
                return result;
            }

            int[] range = searchSample(samples, samples.Count / 2, 0, samples.Count - 1, targetTimeline);
            if (range[0] == range[1])
            {
                result.s1Index = result.s2Index = range[0];
                result.s1 = result.s2 = samples[range[0]];
                result.w1 = 1;
                result.w2 = 0;
                if (Math.Abs(result.s1.Timeline - targetTimeline) > maxGap) return null;
                else return result;
            }
            else
            {
                int s1Index = range[0];
                int s2Index = range[1];
                var s1 = samples[s1Index];
                var s2 = samples[s2Index];
                double t1 = s1.Timeline;
                double t2 = s2.Timeline;
                double t1Gap = Math.Abs(t1 - targetTimeline);
                double t2Gap = Math.Abs(t2 - targetTimeline);

                if (t2 <= t1 && t1Gap < maxGap)
                {
                    result.s1Index = result.s2Index = s1Index;
                    result.s1 = result.s2 = s1;
                    result.w1 = 1;
                    result.w2 = 0;
                    return result;
                }
                else if (t1Gap < maxGap && t2Gap < maxGap)
                {
                    if (s1.Base == s2.Base)
                    {
                        result.s1Index = s1Index;
                        result.s2Index = s2Index;
                        result.s1 = s1;
                        result.s2 = s2;
                        double timestampDelta = t2 - t1;
                        result.w1 = t2Gap / timestampDelta;
                        result.w2 = t1Gap / timestampDelta;
                        return result;
                    }
                    else
                    {
                        if (t1Gap < t2Gap)
                        {
                            result.s1Index = result.s2Index = s1Index;
                            result.s1 = result.s2 = s1;
                            result.w1 = 1;
                            result.w2 = 0;
                            return result;
                        }
                        else
                        {
                            result.s1Index = result.s2Index = s2Index;
                            result.s1 = result.s2 = s2;
                            result.w1 = 1;
                            result.w2 = 0;
                            return result;
                        }
                    }
                }
                else if (t1Gap < maxGap && t2Gap >= maxGap)
                {
                    result.s1Index = result.s2Index = s1Index;
                    result.s1 = result.s2 = s1;
                    result.w1 = 1;
                    result.w2 = 0;
                    return result;
                }
                else if (t1Gap >= maxGap && t2Gap < maxGap)
                {
                    result.s1Index = result.s2Index = s2Index;
                    result.s1 = result.s2 = s2;
                    result.w1 = 1;
                    result.w2 = 0;
                    return result;
                }
                else return null;
            }
        }

        /// \~English
        /// <summary>
        /// Search in the sample buffer, perform interpolation and return the interpolated sample (Interpolation should be supported and the timeline point must be in the range, the time gap is 10 seconds）
        /// </summary>
        /// <param name="samples">Sample buffer</param>
        /// <param name="targetTimeline">Timeline point</param>
        /// <returns>Interpolated sample</returns>
        /// \~Chinese
        /// <summary>
        /// 按目标时间点搜索缓存列表，自动进行插值并返回插值后样本（需要样本支持插值，且目标时间在缓存范围内，默认最大时间间隔为10秒）
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">在时间线上的目标时间点</param>
        /// <returns>插值后的样本</returns>
        public static Sample SearchAndInterpolate(List<Sample> samples, double targetTimeline)
        {
            if (samples.Count == 0) return null;
            if (!samples[0].SupportInterpolation()) return null;

            var result = Search(samples, targetTimeline, 10/* support 10 second gap */);
            if (result == null || result.s1 == result.s2) return null;

            try
            {
                var buf = samples[0].Interpolate(result);
                if (buf == null) return null;

                IndependentTimeInfo timeInfo = null;
                if (result.s1.timestamp.TimeInfo != null && result.s2.timestamp.TimeInfo != null)
                {
                    var t1 = result.s1.timestamp.TimeInfo;
                    var t2 = result.s2.timestamp.TimeInfo;
                    if (t1.OffsetSync == t2.OffsetSync)
                    {
                        var t1Comps = new ulong[] { t1.CPUTick, t1.HostPosix, t1.GuestPosix, t1.ServerPosix, t1.GNSSPosix };
                        var t2Comps = new ulong[] { t2.CPUTick, t2.HostPosix, t2.GuestPosix, t2.ServerPosix, t2.GNSSPosix };
                        var outComps = new ulong[5];
                        for (int i = 0; i < 5; i++)
                        {
                            long diffTime = (long)t2Comps[i] - (long)t1Comps[i];
                            outComps[i] = (ulong)((long)t1Comps[i] + (long)(result.w2 * diffTime));
                        }

                        timeInfo = new IndependentTimeInfo(t1.OffsetSync, outComps[0], outComps[1], outComps[2], outComps[3], outComps[4]);
                    }
                }

                buf.SetTime(result.s1.Base, result.s1.Offset * result.w1 + result.s2.Offset * result.w2, timeInfo, targetTimeline);
                return buf;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// \~English
        /// <summary>
        /// Search in the sample buffer and get the nearest sample (time gap is 10 seconds)
        /// </summary>
        /// <param name="samples">Sample buffer</param>
        /// <param name="targetTimeline">Timeline point</param>
        /// <param name="targetSession">Target session ID</param>
        /// <returns>Nearest sample, null if not found</returns>
        /// \~Chinese
        /// <summary>
        /// 在样本缓存列表中搜索最近样本（最大时间间隔10秒）
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">在时间线上的目标时间点</param>
        /// <param name="targetSession">目标的session ID</param>
        /// <returns>最近样本，若无则返回null</returns>
        public static Sample SearchAndGetNearest(List<Sample> samples, double targetTimeline, DateTime targetSession)
        {
            if (samples.Count == 0) return null;
            if (samples.Count == 1)
            {
                if (samples[0].Base == targetSession) return samples[0];
                else return null;
            }

            var result = Search(samples, targetTimeline, 10/* support 10 second gap */);
            if (result == null) return null;

            if (result.s1.Base != targetSession) return null;

            if (Math.Abs(result.s1.Timeline - targetTimeline) < Math.Abs(result.s2.Timeline - targetTimeline)) return result.s1;
            else return result.s2;
        }

        /// \~English
        /// <summary>
        /// Remove earliest samples, based on the global buffer range
        /// </summary>
        /// <param name="samples">Sample buffer</param>
        /// <param name="bufferBegin">Lower bound of global buffer range</param>
        /// \~Chinese
        /// <summary>
        /// 根据全局缓存范围，将样本缓存中较早样本移除
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="bufferBegin">全局缓存范围的下沿</param>
        public static void ClipWithBufferBegin(List<Sample> samples, double bufferBegin)
        {
            if (samples.Count == 0) return;

            int removeCount = 0;
            foreach (var s in samples)
            {
                if (s.Timeline < bufferBegin) removeCount++;
                else break;
            }

            if (removeCount > 0) samples.RemoveRange(0, removeCount);
        }

        private Timestamp timestamp;
        private double timeline;
    }

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) Type of an element in general sample
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 通用样本中值的类型
    /// </summary>
    public enum GeneralSampleValueMode
    {
        /// \~English
        /// <summary>
        /// Invalid, recorded as "na"
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 无效，采集为"na"
        /// </summary>
        Invalid,

        /// \~English
        /// <summary>
        /// Number
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 数值
        /// </summary>
        Number,

        /// \~English
        /// <summary>
        /// Text
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 字符串
        /// </summary>
        Text,
    }

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) General sample value
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 通用样本值
    /// </summary>
    public struct GeneralSampleValue
    {
        public GeneralSampleValueMode mode;
        public double number;
        public String text;

        public GeneralSampleValue(double number)
        {
            mode = GeneralSampleValueMode.Number;
            this.number = number;
            text = null;
        }

        public GeneralSampleValue(String text)
        {
            if (text == null)
            {
                mode = GeneralSampleValueMode.Invalid;
                number = 0;
                this.text = null;
            }
            else
            {
                mode = GeneralSampleValueMode.Text;
                number = 0;
                this.text = text;
            }
        }

        public bool IsValid()
        {
            return mode != GeneralSampleValueMode.Invalid;
        }

        public bool IsNumber()
        {
            return mode == GeneralSampleValueMode.Number;
        }

        public bool IsNotNumber()
        {
            return mode != GeneralSampleValueMode.Number;
        }

        public bool IsText()
        {
            return mode == GeneralSampleValueMode.Text;
        }

        public bool IsNotText()
        {
            return mode != GeneralSampleValueMode.Text;
        }

        public int? ToInt()
        {
            return mode == GeneralSampleValueMode.Number ? (int?)(int)number : null;
        }

        public float? ToFloat()
        {
            return mode == GeneralSampleValueMode.Number ? (float?)(float)number : null;
        }

        public double? ToDouble()
        {
            return mode == GeneralSampleValueMode.Number ? (double?)number : null;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=2.0.0) General sample
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=2.0.0) 通用样本
    /// </summary>
    public class GeneralSample : Sample
    {
        /// \~English
        /// <summary>
        /// Sample protocol
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 样本协议
        /// </summary>
        public String Protocol { get; set; }

        /// \~English
        /// <summary>
        /// Channel, null means without channel (single channel sample)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 样本所属通道，null表示无通道信息（唯一通道样本）
        /// </summary>
        public int? Channel { get; set; }

        /// \~English
        /// <summary>
        /// Values
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 值列表
        /// </summary>
        public List<GeneralSampleValue> Values { get; set; }
        
        /// \~English
        /// <summary>
        /// Number of significant values (Generally for recording, not significant values will not be recorded)
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 值列表中关键数值的个数（一般用于采集，非关键的将不保存）
        /// </summary>
        public int NumberOfSignificants { get; set; }

        public override string ToString()
        {
            if (Values != null && Values.Count > 0)
            {
                String text = null;
                foreach (var val in Values)
                {
                    String valText = null;
                    switch (val.mode)
                    {
                        case GeneralSampleValueMode.Number:
                            valText = val.number.ToString();
                            break;
                        case GeneralSampleValueMode.Text:
                            valText = val.text;
                            break;
                        default:
                            valText = "na";
                            break;
                    }
                    if (text == null) text = valText;
                    else text += "," + valText;
                }
                return text;
            }
            else return "";
        }
    }
}
