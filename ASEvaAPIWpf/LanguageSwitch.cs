using System;
using System.Collections.Generic;
using System.Windows;

namespace ASEva.UIWpf
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:wpf=2.2.1) For using multilingual text from xaml resources
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:wpf=2.2.1) 方便使用xaml资源中的多语言文本
    /// </summary>
    public class LanguageSwitch
    {
        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="mainResource">The main resource of UI element</param>
        /// <param name="initialLanguage">Initial language</param>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// <param name="mainResource">界面元素的主资源对象</param>
        /// <param name="initialLanguage">初始语言</param>
        /// </summary>
        public LanguageSwitch(ResourceDictionary mainResource, String initialLanguage = "en")
        {
            this.mainResource = mainResource;
            if (mainResource != null)
            {
                currentLanguageDict = mainResource[initialLanguage ?? "en"] as ResourceDictionary;
                if (currentLanguageDict != null) mainResource.MergedDictionaries.Add(currentLanguageDict);
                lock (objs)
                {
                    objs.Add(new WeakReference<LanguageSwitch>(this));
                }
            }
        }

        /// \~English
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="mainResource">The main resource of UI element</param>
        /// <param name="initialLanguage">Initial language</param>
        /// \~Chinese
        /// <summary>
        /// 默认构造函数
        /// <param name="mainResource">界面元素的主资源对象</param>
        /// <param name="initialLanguage">初始语言</param>
        /// </summary>
        public LanguageSwitch(ResourceDictionary mainResource, Language initialLanguage)
        {
            this.mainResource = mainResource;
            if (mainResource != null)
            {
                var languageCode = initialLanguage switch
                {
                    Language.English => "en",
                    Language.Chinese => "zh",
                    _ => "en",
                };

                currentLanguageDict = mainResource[languageCode] as ResourceDictionary;
                if (currentLanguageDict != null) mainResource.MergedDictionaries.Add(currentLanguageDict);
                lock (objs)
                {
                    objs.Add(new WeakReference<LanguageSwitch>(this));
                }
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
            currentLanguageDict = mainResource[language ?? "en"] as ResourceDictionary;
            if (currentLanguageDict != null) mainResource.MergedDictionaries.Add(currentLanguageDict);
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
        public void SwitchTo(Language language)
        {
            var languageCode = language switch
            {
                Language.English => "en",
                Language.Chinese => "zh",
                _ => "en",
            };
            SwitchTo(languageCode);
        }

        /// \~English
        /// <summary>
        /// Get the text with the ID
        /// </summary>
        /// \~Chinese
        /// <summary>
        /// 获取指定ID对应的文本
        /// </summary>
        public string this[string id]
        {
            get
            {
                if (currentLanguageDict != null && currentLanguageDict.Contains(id))
                {
                    return currentLanguageDict[id] as String;
                }
                return null;
            }
        }

        /// \~English
        /// <summary>
        /// Get the format text with the ID and use it to create the final text
        /// </summary>
        /// <param name="id">Text ID</param>
        /// <param name="args">Arguments for the formats</param>
        /// <returns>Output text</returns>
        /// \~Chinese
        /// <summary>
        /// 以指定ID对应的文本作为格式描述，输出文本
        /// </summary>
        /// <param name="id">指定ID</param>
        /// <param name="args">格式描述中的参数值</param>
        /// <returns>输出文本</returns>
        public String Format(String id, params object[] args)
        {
            if (currentLanguageDict != null && currentLanguageDict.Contains(id))
            {
                var format = currentLanguageDict[id] as String;
                if (format != null) return String.Format(format, args);
            }
            return null;
        }

        /// \~English
        /// <summary>
        /// Switch language (for all valid objects)
        /// </summary>
        /// <param name="language">Target language</param>
        /// \~Chinese
        /// <summary>
        /// 切换语言（针对所有有效对象）
        /// <param name="language">目标语言</param>
        /// </summary>
        public static void SwitchAllTo(String language)
        {
            GC.Collect();

            List<LanguageSwitch> targets = new();
            lock (objs)
            {
                var validObjs = new List<WeakReference<LanguageSwitch>>();
                foreach (var reference in objs)
                {
                    LanguageSwitch obj = null;
                    if (reference.TryGetTarget(out obj))
                    {
                        validObjs.Add(reference);
                        targets.Add(obj);
                    }
                }
                objs.Clear();
                objs.AddRange(validObjs);
            }

            foreach (var target in targets)
            {
                target.SwitchTo(language);
            }
        }

        /// \~English
        /// <summary>
        /// Switch language (for all valid objects)
        /// </summary>
        /// <param name="language">Target language</param>
        /// \~Chinese
        /// <summary>
        /// 切换语言（针对所有有效对象）
        /// <param name="language">目标语言</param>
        /// </summary>
        public static void SwitchAllTo(Language language)
        {
            var languageCode = language switch
            {
                Language.English => "en",
                Language.Chinese => "zh",
                _ => "en",
            };
            SwitchAllTo(languageCode);
        }

        private ResourceDictionary mainResource;
        private ResourceDictionary currentLanguageDict;

        private static List<WeakReference<LanguageSwitch>> objs = new();
    }
}