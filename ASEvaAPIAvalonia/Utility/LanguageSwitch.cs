using System;
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
            currentLanguageDict = (IResourceDictionary)mainResource[initialLanguage];
            mainResource.MergedDictionaries.Add(currentLanguageDict);
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
            mainResource.MergedDictionaries.Remove(currentLanguageDict);
            currentLanguageDict = (IResourceDictionary)mainResource[language];
            mainResource.MergedDictionaries.Add(currentLanguageDict);
        }

        private IResourceDictionary mainResource;
        private IResourceDictionary currentLanguageDict;
    }
}