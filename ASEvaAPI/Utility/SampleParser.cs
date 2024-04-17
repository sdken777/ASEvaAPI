using System;
using System.Collections.Generic;

namespace ASEva.Utility
{
    #pragma warning disable CS1571

    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Sample parser, automatically convert ASEva.GeneralSample to sample of any type
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 样本解析，自动将 ASEva.GeneralSample 转化为特化样本
    /// </summary>
    public class SampleParser
    {
        /// \~English
        /// <summary>
        /// Register protocol of ASEva.GeneralSample that needed to be parsed
        /// </summary>
        /// <param name="type">Sample protocol (not including channel info, like'@1')</param>
        /// \~Chinese
        /// <summary>
        /// 注册需要解析的 ASEva.GeneralSample 协议
        /// </summary>
        /// <param name="type">样本协议（不包括通道信息，如'@1'）</param>
        public void Register(Type type)
        {
            if (type.BaseType.ToString() != "ASEva.Sample") return;

            var obj = type.Assembly.CreateInstance(type.ToString());
            if (obj == null || !(obj is Sample)) return;

            var protocols = (obj as Sample).GetGeneralSampleProtocols();
            if (protocols == null || protocols.Length == 0)
            {
                var protocol = (obj as Sample).GetGeneralSampleProtocol();
                if (protocol == null || protocol.Length == 0) return;
                protocols = new string[] { protocol };
            }

            foreach (var protocol in protocols)
            {
                table[protocol] = type;
            }
        }

        /// \~English
        /// <summary>
        /// Parse sample
        /// </summary>
        /// <param name="input">ASEva.GeneralSample to be parsed</param>
        /// <returns>The corresponding sample object, null if failed</returns>
        /// \~Chinese
        /// <summary>
        /// 解析样本
        /// </summary>
        /// <param name="input">待解析的 ASEva.GeneralSample 样本</param>
        /// <returns>若解析成功，则返回对应样本，若失败则返回空</returns>
        public Sample Parse(GeneralSample input)
        {
            if (input == null) return null;
            if (input.Protocol == null) return null;
            if (!table.ContainsKey(input.Protocol)) return null;

            var obj = table[input.Protocol].Assembly.CreateInstance(table[input.Protocol].ToString());
            if (obj == null || !(obj is Sample)) return null;

            var output = obj as Sample;
            output.SetTime(input);

            try
            {
                if (!output.FromGeneralSample(input)) return null;
            }
            catch (Exception)
            {
                return null;
            }

            return output;
        }

        private Dictionary<String, Type> table = new Dictionary<string, Type>();
    }
}
