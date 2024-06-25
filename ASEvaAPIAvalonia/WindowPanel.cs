using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace ASEva.UIAvalonia
{
    #pragma warning disable CS1571
    
    /// \~English
    /// <summary>
    /// (api:avalonia=1.0.0) UI panel for child window
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.0.0) 窗口控件，用于实现窗口组件的实际功能
    /// </summary>
    public class WindowPanel : Panel
    {
        /// \~English
        /// <summary>
        /// [Optional] Called while initializing panel's size
        /// </summary>
        /// <param name="config">Configuration string</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在初始化控件尺寸时被调用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public virtual void OnInitSize(String config) { }

        /// \~English
        /// <summary>
        /// [Optional] Called while initializing the panel
        /// </summary>
        /// <param name="config">Configuration string</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在配置界面控件初始化时被调用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public virtual void OnInit(String config) { }

        /// \~English
        /// <summary>
        /// [Optional] Called while getting the configuration string
        /// </summary>
        /// <returns>Configuration string</returns>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取配置字符串时被调用
        /// </summary>
        /// <returns>配置字符串</returns>
        public virtual String OnGetConfig() { return null; }

        /// \~English
        /// <summary>
        /// [Optional] Called while new data arrived. If there's a sample buffer, use ASEva.Sample.ClipWithBufferBegin to remove data outside the buffer range after adding new sample. In addition, since ASEva.WindowPanel.OnUpdateUI won't be called while the child window is hidden, you can use ASEva.Samples.ManualTriggerSample to perform background processing (ManualTriggerSample's frequency is 50Hz)
        /// </summary>
        /// <param name="data">New data, can be ASEva.GeneralSample , ASEva.Samples.ManualTriggerSample , ASEva.Samples.AudioFrameSample , ASEva.Samples.PointCloudSample , ASEva.Samples.BusDataPack , ASEva.Samples.SignalsData , ASEva.EventData , ASEva.SceneData , etc.</param>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在获得新数据时被调用。如果存在样本缓存，则需要在添加至缓存后使用 ASEva.Sample.ClipWithBufferBegin 将缓存范围外数据移除。另外，由于窗口隐藏时将不会调用 ASEva.WindowPanel.OnUpdateUI ，所以可以在此函数中的data为 ASEva.Samples.ManualTriggerSample 时进行后台处理（ManualTriggerSample频率为50Hz）
        /// </summary>
        /// <param name="data">新数据，包括通用样本 ASEva.GeneralSample , 手动触发器样本 ASEva.Samples.ManualTriggerSample , 音频帧样本 ASEva.Samples.AudioFrameSample , 点云帧样本 ASEva.Samples.PointCloudSample , 总线数据 ASEva.Samples.BusDataPack , 信号数据 ASEva.Samples.SignalsData , 事件数据 ASEva.EventData , 场景片段描述 ASEva.SceneData 等类型</param>
        public virtual void OnInputData(object data) { }

        /// \~English
        /// <summary>
        /// [Optional] Called while needed to reset data buffer
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在需要重置数据缓存时被调用
        /// </summary>
        public virtual void OnResetData() { }

        /// \~English
        /// <summary>
        /// [Optional] Called while starting a session
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在每轮开始时被调用
        /// </summary>
        public virtual void OnStartSession() { }

        /// \~English
        /// <summary>
        /// [Optional] Called while stopping a session
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在每轮结束时被调用
        /// </summary>
        public virtual void OnStopSession() { }

        /// \~English
        /// <summary>
        /// [Optional] Called while updating the panel's UI. All UI related updating should be put here. Note that this function won't be called when the child window is hidden
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在需要更新界面时被调用。所有与界面相关的更新应全部放在此函数内实现。需要注意，窗口隐藏时将不会调用此函数
        /// </summary>
        public virtual void OnUpdateUI() { }

        /// \~English
        /// <summary>
        /// (api:avalonia=1.0.13) [Optional] Called in the main loop, for running modal dialog
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.0.13) [可选实现] 在主循环中被调用，可进行模态对话
        /// </summary>
        public virtual Task OnHandleModal() { return Task.CompletedTask; }

        /// \~English
        /// <summary>
        /// (api:avalonia=1.0.15) [Optional] Called in the main loop, for asynchronous calls
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.0.15) [可选实现] 在主循环中被调用，可进行异步调用
        /// </summary>
        public virtual Task OnHandleAsync() { return Task.CompletedTask; }

        /// \~English
        /// <summary>
        /// [Optional] Called before destroying the panel
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在窗口控件销毁前被调用
        /// </summary>
        public virtual void OnRelease() { }
    }
}
