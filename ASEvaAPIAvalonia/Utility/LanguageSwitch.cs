using System;
using ASEva.Utility;
using Avalonia.Controls;

namespace ASEva.UIAvalonia
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:avalonia=1.3.2) For using multilingual text from axaml resources
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:avalonia=1.3.2) 方便使用axaml资源中的多语言文本
    /// </summary>
    public class LanguageSwitch
    {
        /// \~English
        /// <summary>
        /// For using multilingual text from axaml resources
        /// </summary>
        /// <param name="mainResource">The main resource of UI element</param>
        /// <param name="initialLanguage">Initial language</param>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// <param name="mainResource">界面元素的主资源对象</param>
        /// <param name="initialLanguage">初始语言</param>
        /// </summary>
        public LanguageSwitch(IResourceDictionary mainResource, String initialLanguage = "en")
        {
            this.mainResource = mainResource;
            if (mainResource != null)
            {
                currentLanguageDict = mainResource[initialLanguage ?? "en"] as IResourceDictionary;
                if (currentLanguageDict != null) mainResource.MergedDictionaries.Add(currentLanguageDict);
            }
        }

        /// \~English
        /// <summary>
        /// Switch language
        /// </summary>
        /// <param name="language">Target language</param>
        /// \~Chinese
        /// <summary>
        /// 切换语言
        /// <param name="language">目标语言</param>
        /// </summary>
        public void SwitchTo(String language)
        {
            if (mainResource == null) return;
            if (currentLanguageDict != null) mainResource.MergedDictionaries.Remove(currentLanguageDict);
            currentLanguageDict = mainResource[language ?? "en"] as IResourceDictionary;
            if (currentLanguageDict != null) mainResource.MergedDictionaries.Add(currentLanguageDict);
        }

        /// \~English
        /// <summary>
        /// (api:avalonia=1.3.3) Get the text with the ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.3.3) 获取指定ID对应的文本
        /// </summary>
        public string this[string id]
        {
            get
            {
                if (currentLanguageDict != null && currentLanguageDict.ContainsKey(id))
                {
                    return currentLanguageDict[id] as String;
                }
                return null;
            }
        }

        /// \~English
        /// <summary>
        /// (api:avalonia=1.3.3) Get the format text with the ID and use it to create the final text
        /// </summary>
        /// <param name="id">Text ID</param>
        /// <param name="args">Arguments for the formats</param>
        /// <returns>Output text</returns>
        /// \~Chinese
        /// <summary>
        /// (api:avalonia=1.3.3) 以指定ID对应的文本作为格式描述，输出文本
        /// </summary>
        /// <param name="id">指定ID</param>
        /// <param name="args">格式描述中的参数值</param>
        /// <returns>输出文本</returns>
        public String Format(String id, params object[] args)
        {
            if (currentLanguageDict != null && currentLanguageDict.ContainsKey(id))
            {
                var format = currentLanguageDict[id] as String;
                if (format != null) return String.Format(format, args);
            }
            return null;
        }

        private IResourceDictionary mainResource;
        private IResourceDictionary currentLanguageDict;
    }
}