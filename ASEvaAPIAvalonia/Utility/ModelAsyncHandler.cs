using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ASEva;
using ASEva.Utility;

namespace ASEva.UIAvalonia
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:avalonia=1.3.0) The base class of the view model that supports asynchronous operations
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.3.0) 支持异步操作的视图模型基类
    /// </summary>
    public class AsyncViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs args) => PropertyChanged?.Invoke(this, args);
    }

    /// \~English
    /// <summary>
    /// (api:avalonia=1.3.0) Let the property field of the model support asynchronous operations
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.3.0) 让模型中的属性字段支持异步操作
    /// </summary>
    public class PropertyAsyncHandler<T>
    {
        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="model">The view model</param>
        /// <param name="propertyName">The bound property name</param>
        /// <param name="relatedPropertyNames">The related property names</param>
        /// <param name="getter">While refreshing, the function for getting the property value from the system asynchronously</param>
        /// <param name="setter">While the property value changes, the function for setting the property value to the system asynchronously</param>
        /// <param name="minInterval">The minimum interval for refreshing</param>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="model">视图模型</param>
        /// <param name="propertyName">绑定的属性名</param>
        /// <param name="relatedPropertyNames">相关的属性名列表</param>
        /// <param name="getter">循环刷新时，用于从系统异步获取属性值的函数</param>
        /// <param name="setter">属性值变化时，用于将其异步设置给系统的函数</param>
        /// <param name="minInterval">循环刷新的最小间隔</param>
        public PropertyAsyncHandler(AsyncViewModel model, String propertyName, String[] relatedPropertyNames = null, Func<Task<(bool, T)>> getter = null, Action<T> setter = null, double minInterval = 0)
        {
            this.model = model;
            this.propertyName = propertyName;
            this.relatedPropertyNames = relatedPropertyNames;
            this.getter = getter;
            this.setter = setter;
            this.taskBeat = new TaskBeat() { MinInterval = minInterval };
        }

        /// \~English
        /// <summary>
        /// Update the property value directly
        /// </summary>
        /// <param name="value">The property value</param>
        /// \~Chinese
        /// <summary>
        /// 直接更新属性值
        /// </summary>
        /// <param name="value">属性值</param>
        public void SetToModel(T value)
        {
            var propertyInfo = model.GetType().GetProperty(propertyName);
            if (propertyInfo == null) return;

            var curValue = (T)propertyInfo.GetValue(model);
            if (value.Equals(curValue)) return;

            settingProperty = true;
            propertyInfo.SetValue(model, value);
            settingProperty = false;

            model.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            foreach (var relatedPropertyName in (relatedPropertyNames ?? [])) model.OnPropertyChanged(new PropertyChangedEventArgs(relatedPropertyName));
        }

        /// \~English
        /// <summary>
        /// Refresh and update the property value from the system
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 循环刷新，从系统获取并更新属性值
        /// </summary>
        public void UpdateToModel()
        {
            if (getter == null) return;

            taskBeat.Handle(async () =>
            {
                var result = await getter.Invoke();
                if (!result.Item1) return;

                var propertyInfo = model.GetType().GetProperty(propertyName);
                if (propertyInfo == null) return;

                if (setter != null && !keeper.CanUpdate) return;

                var curValue = (T)propertyInfo.GetValue(model);
                if (result.Item2.Equals(curValue)) return;

                settingProperty = true;
                propertyInfo.SetValue(model, result.Item2);
                settingProperty = false;

                model.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
                foreach (var relatedPropertyName in (relatedPropertyNames ?? [])) model.OnPropertyChanged(new PropertyChangedEventArgs(relatedPropertyName));
            });
        }

        /// \~English
        /// <summary>
        /// Respond to the change of the property value, and set the value to the system
        /// </summary>
        /// <param name="fromRelatedProperties">Whether it is a related property whose value changes</param>
        /// \~Chinese
        /// <summary>
        /// 响应属性值变化，将值设置给系统
        /// </summary>
        /// <param name="fromRelatedProperties">是否为相关属性值的变化</param>
        public void UpdateFromModel(bool fromRelatedProperties = false)
        {
            if (setter == null || settingProperty) return;

            var propertyInfo = model.GetType().GetProperty(propertyName);
            if (propertyInfo == null) return;

            if (fromRelatedProperties) model.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            foreach (var relatedPropertyName in (relatedPropertyNames ?? [])) model.OnPropertyChanged(new PropertyChangedEventArgs(relatedPropertyName));

            if (AgencyLocal.ClientSide) keeper.Set();
            setter.Invoke((T)propertyInfo.GetValue(model));
        }

        /// \~English
        /// <summary>
        /// (api:app=1.3.1) The time to pause refreshing after an operation, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=1.3.1) 操作后暂停刷新的时间，单位毫秒
        /// </summary>
        public int KeepTime
        {
            get => keeper.KeepTime;
            set => keeper.KeepTime = value;
        }

        private AsyncViewModel model;
        private String propertyName;
        private String[] relatedPropertyNames;
        private Func<Task<(bool, T)>> getter;
        private Action<T> setter;
        private bool settingProperty = false;
        private TaskBeat taskBeat;
        private UpdateKeeper keeper = new();
    }

    /// \~English
    /// <summary>
    /// (api:avalonia=1.3.0) Let an element of the ObservableCollection in the model support asynchronous operations
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.3.0) 让模型中ObservableCollection的元素字段支持异步操作
    /// </summary>
    public class ElementAsyncHandler<T>
    {
        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="model">The view model</param>
        /// <param name="propertyName">The bound property name</param>
        /// <param name="elementIndex">The index of the element in the ObservableCollection</param>
        /// <param name="getter">While refreshing, the function for getting the element value from the system asynchronously</param>
        /// <param name="setter">While the element value changes, the function for setting the element value to the system asynchronously</param>
        /// <param name="minInterval">The minimum interval for refreshing</param>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="model">视图模型</param>
        /// <param name="propertyName">绑定的属性名</param>
        /// <param name="elementIndex">ObservableCollection中元素的序号</param>
        /// <param name="getter">循环刷新时，用于从系统异步获取元素值的函数</param>
        /// <param name="setter">元素值变化时，用于将其异步设置给系统的函数</param>
        /// <param name="minInterval">循环刷新的最小间隔</param>
        public ElementAsyncHandler(AsyncViewModel model, String propertyName, int elementIndex, Func<int, Task<(bool, T)>> getter = null, Action<int, T> setter = null, double minInterval = 0)
        {
            this.model = model;
            this.propertyName = propertyName;
            this.elementIndex = elementIndex;
            this.getter = getter;
            this.setter = setter;
            taskBeat = new(){ MinInterval = minInterval };

            var propertyInfo = model.GetType().GetProperty(propertyName);
            if (propertyInfo == null) return;

            var collection = propertyInfo.GetValue(model) as ObservableCollection<T>;
            if (collection == null) return;

            collection.CollectionChanged += (o, args) =>
            {
                if (args.NewStartingIndex != elementIndex) return;
                if (args.Action != System.Collections.Specialized.NotifyCollectionChangedAction.Replace) return;

                if (setter == null || settingProperty) return;

                if (AgencyLocal.ClientSide) keeper.Set();
                setter.Invoke(elementIndex, collection[elementIndex]);
            };
        }

        /// \~English
        /// <summary>
        /// Update the element value directly
        /// </summary>
        /// <param name="value">The element value</param>
        /// \~Chinese
        /// <summary>
        /// 直接更新元素值
        /// </summary>
        /// <param name="value">元素值</param>
        public void SetToModel(T value)
        {
            var propertyInfo = model.GetType().GetProperty(propertyName);
            if (propertyInfo == null) return;

            var collection = propertyInfo.GetValue(model) as ObservableCollection<T>;
            if (collection == null) return;

            var curValue = collection[elementIndex];
            if (value.Equals(curValue)) return;

            settingProperty = true;
            collection[elementIndex] = value;
            settingProperty = false;
        }

        /// \~English
        /// <summary>
        /// Refresh and update the element value from the system
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 循环刷新，从系统获取并更新元素值
        /// </summary>
        public void UpdateToModel()
        {
            if (getter == null) return;

            taskBeat.Handle(async () =>
            {
                var result = await getter.Invoke(elementIndex);
                if (!result.Item1) return;

                var propertyInfo = model.GetType().GetProperty(propertyName);
                if (propertyInfo == null) return;

                var collection = propertyInfo.GetValue(model) as ObservableCollection<T>;
                if (collection == null) return;

                if (setter != null && !keeper.CanUpdate) return;

                var curValue = collection[elementIndex];
                if (result.Item2.Equals(curValue)) return;

                settingProperty = true;
                collection[elementIndex] = result.Item2;
                settingProperty = false;
            });
        }

        /// \~English
        /// <summary>
        /// (api:app=1.3.1) The time to pause refreshing after an operation, in milliseconds
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:app=1.3.1) 操作后暂停刷新的时间，单位毫秒
        /// </summary>
        public int KeepTime
        {
            get => keeper.KeepTime;
            set => keeper.KeepTime = value;
        }

        private object model;
        private String propertyName;
        private int elementIndex;
        private Func<int, Task<(bool, T)>> getter;
        private Action<int, T> setter;

        private bool settingProperty = false;
        private TaskBeat taskBeat;
        private UpdateKeeper keeper = new();
    }

    /// \~English
    /// <summary>
    /// (api:avalonia=1.3.0) Let the ObservableCollection in the model support asynchronous operations
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.3.0) 让模型中ObservableCollection支持异步操作
    /// </summary>
    public class CollectionAsyncHandler<T>
    {
        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="model">The view model</param>
        /// <param name="propertyName">The bound property name</param>
        /// <param name="relatedPropertyNames">The related property names</param>
        /// <param name="getter">While refreshing, the function for getting the collection value from the system asynchronously</param>
        /// <param name="minInterval">The minimum interval for refreshing</param>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="model">视图模型</param>
        /// <param name="propertyName">绑定的属性名</param>
        /// <param name="relatedPropertyNames">相关的属性名列表</param>
        /// <param name="getter">循环刷新时，用于从系统异步获取集合值的函数</param>
        /// <param name="minInterval">循环刷新的最小间隔</param>
        public CollectionAsyncHandler(AsyncViewModel model, String propertyName, String[] relatedPropertyNames = null, Func<Task<(bool, T[])>> getter = null, double minInterval = 0)
        {
            this.model = model;
            this.propertyName = propertyName;
            this.relatedPropertyNames = relatedPropertyNames;
            this.getter = getter;
            this.taskBeat = new TaskBeat() { MinInterval = minInterval };
        }

        /// \~English
        /// <summary>
        /// Refresh and update the collection value from the system
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 循环刷新，从系统获取并更新集合值
        /// </summary>
        public void UpdateToModel()
        {
            if (getter == null) return;

            taskBeat.Handle(async () =>
            {
                var result = await getter.Invoke();
                if (!result.Item1 || result.Item2 == null) return;

                var propertyInfo = model.GetType().GetProperty(propertyName);
                if (propertyInfo == null) return;

                var collection = propertyInfo.GetValue(model) as ObservableCollection<T>;
                if (collection == null) return;

                var elements = result.Item2;
                var tempList = elements.ToList();
                var collectionModified = false;

                var toRemove = new List<T>();
                foreach (var item in collection)
                {
                    if (!tempList.Contains(item)) toRemove.Add(item);
                }
                foreach (var item in toRemove)
                {
                    collection.Remove(item);
                    collectionModified = true;
                }

                tempList.RemoveAll((item) => !collection.Contains(item));
                var commonElements = tempList.ToArray();
                
                toRemove.Clear();
                for (int i = 0; i < commonElements.Length; i++)
                {
                    if (!collection[i].Equals(commonElements[i])) toRemove.Add(collection[i]);
                }
                foreach (var item in toRemove)
                {
                    collection.Remove(item);
                    collectionModified = true;
                }

                for (int i = 0; i < elements.Length; i++)
                {
                    if (i >= collection.Count)
                    {
                        collection.Add(elements[i]);
                        collectionModified = true;
                    }
                    else if (!collection[i].Equals(elements[i]))
                    {
                        collection.Insert(i, elements[i]);
                        collectionModified = true;
                    }
                }

                if (collectionModified)
                {
                    foreach (var relatedPropertyName in (relatedPropertyNames ?? [])) model.OnPropertyChanged(new PropertyChangedEventArgs(relatedPropertyName));
                }
            });
        }

        private AsyncViewModel model;
        private String propertyName;
        private String[] relatedPropertyNames;
        private Func<Task<(bool, T[])>> getter;
        private TaskBeat taskBeat;
    }
}