using System;
using System.Collections.Generic;
using ASEva.Samples;

namespace ASEva
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.7.3) Output sample of processor
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.3) 数据处理的输出样本
    /// </summary>
    public class ProcessorOutputSample
    {
        /// \~English
        /// <summary>
        /// Output sample. If "timeRef" is null, you should set timestamp and timeline point manually
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 输出样本，若timeRef为空则需要手动对时间戳和时间线位置赋值
        /// </summary>
        public Sample Sample { get; set; }

        /// \~English
        /// <summary>
        /// Time reference sample, "sample"'s timestamp and timeline point will be the same as this sample
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 时间参考样本，sample的时间戳和时间线位置将被赋值为与此样本一致
        /// </summary>
        public Sample? TimeRef { get; set; }

        /// \~English
        /// <summary>
        /// Constructor only based on the output sample
        /// </summary>
        /// <param name="sample">Output sample</param>
        /// \~Chinese
        /// <summary>
        /// 仅基于输出样本构造
        /// </summary>
        /// <param name="sample">输出样本</param>
        public ProcessorOutputSample(Sample sample)
        {
            Sample = sample;
        }

        /// \~English
        /// <summary>
        /// Constructor based on the output sample and time reference sample
        /// </summary>
        /// <param name="sample">Output sample</param>
        /// <param name="timeRef">Time reference sample</param>
        /// \~Chinese
        /// <summary>
        /// 基于输出样本和时间参考样本构造
        /// </summary>
        /// <param name="sample">输出样本</param>
        /// <param name="timeRef">时间参考样本</param>
        public ProcessorOutputSample(Sample sample, Sample timeRef)
        {
            Sample = sample;
            TimeRef = timeRef;
        }
    }

    /// \~English
    /// <summary>
    /// (api:app=3.7.3) Processor. Created while session starting, released while session stopping
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.7.3) 数据处理对象。在每轮Session开始时创建，结束时销毁
    /// </summary>
    public class Processor
    {
        /// \~English
        /// <summary>
        /// [Optional] Called after created while session starting
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在Session开始时，Processor对象创建后被调用
        /// </summary>
        public virtual void OnInit() { }

        /// \~English
        /// <summary>
        /// [Required] Called while a new sample arrived
        /// </summary>
        /// <param name="sample">New sample, include ASEva.GeneralSample , ASEva.Samples.ManualTriggerSample , ASEva.Samples.VideoFrameSample , ASEva.Samples.AudioFrameSample , ASEva.Samples.PointCloudSample , etc.</param>
        /// <returns>List of output samples</returns>
        /// \~Chinese
        /// <summary>
        /// [必须实现] 在到来新数据样本时被调用，进行数据处理
        /// </summary>
        /// <param name="sample">新数据样本，包括通用样本 ASEva.GeneralSample , 手动触发器样本 ASEva.Samples.ManualTriggerSample , 视频帧样本 ASEva.Samples.VideoFrameSample , 音频帧样本 ASEva.Samples.AudioFrameSample , 点云帧样本 ASEva.Samples.PointCloudSample 等类型</param>
        /// <returns>返回的样本列表</returns>
        public virtual List<ProcessorOutputSample> OnProcessSample(Sample sample) { return []; }

        /// \~English
        /// <summary>
        /// [Optional] Called while new signal data arrived
        /// </summary>
        /// <param name="signals">New signal data</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在到来新信号数据时被调用，进行数据处理
        /// </summary>
        /// <param name="signals">新信号数据</param>
        public virtual void OnInputSignalsData(SignalsData signals) { }

        /// \~English
        /// <summary>
        /// [Optional] Called while new bus raw data arrived
        /// </summary>
        /// <param name="busData">New bus raw data (Be aware that if the length is longer than 64 bytes, the extra part will be removed)</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在到来新总线数据时被调用，进行数据处理
        /// </summary>
        /// <param name="busData">新总线数据（需要注意，报文长度超过64字节的数据将被截去多出部分）</param>
        public virtual void OnInputBusDataPack(BusDataPack busData) { }

        /// \~English
        /// <summary>
        /// [Optional] Called while new event data arrived
        /// </summary>
        /// <param name="eventData">New event data</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在到来新事件数据时被调用，进行数据处理
        /// </summary>
        /// <param name="eventData">新事件数据</param>
        public virtual void OnInputEventData(EventData eventData) { }

        /// \~English
        /// <summary>
        /// [Optional] Called before released while session stopping
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在Session结束时，Processor对象销毁前被调用
        /// </summary>
        public virtual void OnRelease() { }
    }
}
