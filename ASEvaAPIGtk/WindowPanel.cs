using System;
using Gtk;
using ASEva;

namespace ASEva.UIGtk
{
    #pragma warning disable CS1591
    
    /// \~English
    /// 
    /// \~Chinese
    /// <summary>
    /// (api:gtk=2.0.0) 窗口控件，用于实现窗口组件的实际功能
    /// </summary>
    public class WindowPanel : Box
    {
        public WindowPanel(IntPtr raw) : base(raw)
        {}

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:gtk=2.1.1) [可选实现] 在初始化控件尺寸时被调用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public virtual void OnInitSize(String config) { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取默认大小时被调用（若不实现默认大小即为最小尺寸）
        /// </summary>
        /// <returns>默认大小</returns>
        public virtual IntSize OnGetDefaultSize() { return new IntSize(0, 0); }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在配置界面控件初始化时被调用
        /// </summary>
        /// <param name="config">配置字符串</param>
        public virtual void OnInit(String config) { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 获取配置字符串时被调用
        /// </summary>
        /// <returns>配置字符串</returns>
        public virtual String OnGetConfig() { return null; }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在获得新数据时被调用。如果存在样本缓存，则需要在添加至缓存后使用 ASEva.Sample.ClipWithBufferBegin 将缓存范围外数据移除。另外，由于窗口隐藏时将不会调用 ASEva.WindowPanel.OnUpdateUI ，所以可以在此函数中的data为 ASEva.Samples.ManualTriggerSample 时进行后台处理（ManualTriggerSample频率为50Hz）
        /// </summary>
        /// <param name="data">新数据，包括通用样本 ASEva.GeneralSample , 手动触发器样本 ASEva.Samples.ManualTriggerSample , 音频帧样本 ASEva.Samples.AudioFrameSample , 点云帧样本 ASEva.Samples.PointCloudSample , 总线数据 ASEva.Samples.BusDataPack , 信号数据 ASEva.Samples.SignalsData , 事件数据 ASEva.EventData , 场景片段描述 ASEva.SceneData 等类型</param>
        public virtual void OnInputData(object data) { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在需要重置数据缓存时被调用
        /// </summary>
        public virtual void OnResetData() { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在每轮开始时被调用
        /// </summary>
        public virtual void OnStartSession() { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在每轮结束时被调用
        /// </summary>
        public virtual void OnStopSession() { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在需要更新界面时被调用。所有与界面相关的更新应全部放在此函数内实现。需要注意，窗口隐藏时将不会调用此函数
        /// </summary>
        public virtual void OnUpdateUI() { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// (api:gtk=2.5.10) [可选实现] 在主循环中被调用，可进行模态对话
        /// </summary>
        public virtual void OnHandleModal() { }

        /// \~English
        /// 
        /// \~Chinese
        /// <summary>
        /// [可选实现] 在窗口控件销毁前被调用
        /// </summary>
        public virtual void OnRelease() { }
    }
}
