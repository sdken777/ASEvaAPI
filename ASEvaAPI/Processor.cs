using System;
using System.Collections.Generic;
using ASEva.Samples;

namespace ASEva
{
    /// <summary>
    /// (api:app=2.0.0) 数据处理的输出样本
    /// </summary>
    public struct ProcessorOutputSample
    {
        /// <summary>
        /// 输出样本，若timeRef为空则需要对Base, Offset, Timeline赋值
        /// </summary>
        public Sample sample;

        /// <summary>
        /// 时间戳参考样本，sample的时间戳将被赋值为与此样本一致
        /// </summary>
        public Sample timeRef;

        /// <summary>
        /// 仅基于输出样本构造
        /// </summary>
        /// <param name="sample">输出样本</param>
        public ProcessorOutputSample(Sample sample)
        {
            this.sample = sample;
            this.timeRef = null;
        }

        /// <summary>
        /// 基于输出样本和时间戳参考样本构造
        /// </summary>
        /// <param name="sample">输出样本</param>
        /// <param name="timeRef">时间戳参考样本</param>
        public ProcessorOutputSample(Sample sample, Sample timeRef)
        {
            this.sample = sample;
            this.timeRef = timeRef;
        }
    }

    /// <summary>
    /// (api:app=2.0.0) 数据处理对象。在每轮开始时创建，结束时销毁
    /// </summary>
    public class Processor
    {
        /// <summary>
        /// [可选实现] 在Session开始时，Processor对象创建后被调用
        /// </summary>
        public virtual void OnInit() { }

        /// <summary>
        /// [必须实现] 在到来新数据样本时被调用，进行数据处理
        /// </summary>
        /// <param name="sample">新数据样本，包括通用样本 ASEva.GeneralSample , 手动触发器样本 ASEva.Samples.ManualTriggerSample , 音频帧样本 ASEva.Samples.AudioFrameSample , 点云帧样本 ASEva.Samples.PointCloudSample 等类型</param>
        /// <returns>返回的样本列表</returns>
        public virtual List<ProcessorOutputSample> OnProcessSample(Sample sample) { return null; }

        /// <summary>
        /// [可选实现] 在到来新信号数据时被调用，进行数据处理
        /// </summary>
        /// <param name="signals">新信号数据</param>
        public virtual void OnInputSignalsData(SignalsData signals) { }

        /// <summary>
        /// [可选实现] 在到来新总线数据时被调用，进行数据处理
        /// </summary>
        /// <param name="busData">新总线数据（需要注意，报文长度超过64字节的数据将被截去多出部分）</param>
        public virtual void OnInputBusDataPack(BusDataPack busData) { }

        /// <summary>
        /// [可选实现] 在到来新事件数据时被调用，进行数据处理
        /// </summary>
        /// <param name="eventData">新事件数据</param>
        public virtual void OnInputEventData(EventData eventData) { }

        /// <summary>
        /// [可选实现] 在Session结束时，Processor对象销毁前被调用
        /// </summary>
        public virtual void OnRelease() { }
    }
}
