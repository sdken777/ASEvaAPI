using System;
using System.Collections.Generic;
using System.Linq;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 数据样本
    /// </summary>
    public class Sample : IComparable<Sample>
    {
        /// <summary>
        /// 所属session ID
        /// </summary>
        public DateTime Base { get; set; }
        /// <summary>
        /// 相对时间戳（session开始时间为0），单位秒
        /// </summary>
        public double Offset { get; set; }
        /// <summary>
        /// 时间线，单位秒
        /// </summary>
        public double Timeline { get; set; }

        /// <summary>
        /// 绝对时间戳（本地时间），含年月日时分秒，毫秒
        /// </summary>
        public DateTime? TimestampLocal
        {
            get
            {
                return Agency.GetTimestampLocal(Base, Offset);
            }
        }

        /// <summary>
        /// 绝对时间戳（UTC时间），含年月日时分秒，毫秒
        /// </summary>
        public DateTime? TimestampUTC
        {
            get
            {
                return Agency.GetTimestampUTC(Base, Offset);
            }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Sample()
        {
            Offset = 0;
            Timeline = 0;
        }

        /// <summary>
        /// 带时间戳初始化的构造函数
        /// </summary>
        /// <param name="bas">所属session ID</param>
        /// <param name="offset">相对时间戳</param>
        /// <param name="timeline">时间线</param>
        public Sample(DateTime bas, double offset, double timeline)
        {
            Base = bas;
            Offset = offset;
            Timeline = timeline;
        }

        /// <summary>
        /// 时间线比较
        /// </summary>
        /// <param name="other">另一个数据样本</param>
        /// <returns>比较结果</returns>
        public int CompareTo(Sample other)
        {
            return Timeline.CompareTo(other.Timeline);
        }

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

        /// <summary>
        /// [支持通用样本转换时必须实现] 特化样本转为 ASEva.GeneralSample 的协议名称
        /// </summary>
        /// <returns></returns>
        public virtual String GetGeneralSampleProtocol()
        {
            return null;
        }

        /// <summary>
        /// [支持通用样本转换时必须实现] 从 ASEva.GeneralSample 转为特化样本时支持的协议名称列表
        /// </summary>
        /// <returns>支持的协议名称列表</returns>
        public virtual String[] GetGeneralSampleProtocols()
        {
            return null;
        }

        /// <summary>
        /// [支持通用样本转换时必须实现] 从 ASEva.GeneralSample 转为特化样本，需实现时间戳拷贝(Base, Offset, Timeline)
        /// </summary>
        /// <param name="sample">通用样本</param>
        /// <returns>转换是否成功</returns>
        public virtual bool FromGeneralSample(GeneralSample sample)
        {
            return false;
        }

        /// <summary>
        /// [支持通用样本转换时可选实现] 特化样本转为 ASEva.GeneralSample ，需实现时间戳拷贝(Base, Offset, Timeline)
        /// </summary>
        /// <returns>通用样本</returns>
        public virtual GeneralSample ToGeneralSample()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// [支持通用样本转换时可选实现] 特化样本转为 ASEva.GeneralSample ，需实现时间戳拷贝(Base, Offset, Timeline)以及通道赋值
        /// </summary>
        /// <returns>通用样本</returns>
        public virtual GeneralSample ToGeneralSample(int channel)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// [可选实现] 返回是否支持样本间插值
        /// </summary>
        /// <returns>是否支持样本间插值，默认为false</returns>
        public virtual bool SupportInterpolation()
        {
            return false;
        }

        /// <summary>
        /// [SupportInterpolation返回true时必须实现] 基于 ASEva.Sample.Search 返回的搜索结果进行样本插值，无需输出时间戳
        /// </summary>
        /// <param name="input">样本缓存的搜索结果</param>
        /// <returns>返回插值后的样本</returns>
        protected virtual Sample Interpolate(SearchResult input)
        {
            return null;
        }

        /// <summary>
        /// 判断目标时间线是否在样本缓存范围内
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">目标时间线</param>
        /// <returns>是否在范围内</returns>
        public static bool IsInRange(List<Sample> samples, double targetTimeline)
        {
            if (samples == null || samples.Count < 2) return false;
            return targetTimeline >= samples[0].Timeline && targetTimeline <= samples.Last().Timeline;
        }

        /// <summary>
        /// 判断目标时间线是否在样本缓存范围外，且比所有样本都更早
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">目标时间线</param>
        /// <returns>是否在范围外，且比所有样本都更早</returns>
        public static bool IsOutRangeLower(List<Sample> samples, double targetTimeline)
        {
            if (samples == null || samples.Count == 0) return false;
            return targetTimeline < samples[0].Timeline;
        }

        /// <summary>
        /// 判断目标时间线是否在样本缓存范围外，且比所有样本都更晚
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">目标时间线</param>
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

        /// <summary>
        /// 按目标时间戳搜索缓存列表
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">目标时间线</param>
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

        /// <summary>
        /// 按目标时间戳搜索缓存列表，自动进行插值并返回插值后样本（需要样本支持插值，且目标时间在缓存范围内，默认最大时间间隔为10秒）
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">目标时间线</param>
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

                buf.Base = result.s1.Base;
                buf.Offset = result.s1.Offset * result.w1 + result.s2.Offset * result.w2;
                buf.Timeline = targetTimeline;
                return buf;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 在样本缓存列表中搜索最近样本
        /// </summary>
        /// <param name="samples">样本缓存</param>
        /// <param name="targetTimeline">目标时间线</param>
        /// <param name="targetBase">目标的session ID</param>
        /// <returns>最近样本，若无则返回null</returns>
        public static Sample SearchAndGetNearest(List<Sample> samples, double targetTimeline, DateTime targetBase)
        {
            if (samples.Count == 0) return null;
            if (samples.Count == 1)
            {
                if (samples[0].Base == targetBase) return samples[0];
                else return null;
            }

            var result = Search(samples, targetTimeline, 10/* support 10 second gap */);
            if (result == null) return null;

            if (result.s1.Base != targetBase) return null;

            if (Math.Abs(result.s1.Timeline - targetTimeline) < Math.Abs(result.s2.Timeline - targetTimeline)) return result.s1;
            else return result.s2;
        }

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
    }

    /// <summary>
    /// (api:app=2.0.0) 通用样本中值的类型
    /// </summary>
    public enum GeneralSampleValueMode
    {
        /// <summary>
        /// 无效，采集为"na"
        /// </summary>
        Invalid,
        /// <summary>
        /// 数值
        /// </summary>
        Number,
        /// <summary>
        /// 字符串
        /// </summary>
        Text,
    }

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

    /// <summary>
    /// (api:app=2.0.0) 通用样本
    /// </summary>
    public class GeneralSample : Sample
    {
        /// <summary>
        /// 样本协议
        /// </summary>
        public String Protocol { get; set; }
        /// <summary>
        /// 样本所属通道，null表示无通道信息（唯一通道样本）
        /// </summary>
        public int? Channel { get; set; }
        /// <summary>
        /// 值列表
        /// </summary>
        public List<GeneralSampleValue> Values { get; set; }
        /// <summary>
        /// 值列表中关键数值的个数
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
