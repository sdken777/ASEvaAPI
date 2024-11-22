using System;

namespace ASEva.Utility
{
    /// \~English
    /// <summary>
    /// (api:app=3.0.0) Convert object's info to string
    /// </summary>
    /// \~Chinese
    /// <summary>
    /// (api:app=3.0.0) 递归的将对象信息转化为字符串
    /// </summary>
    public class ContentToString
    {
        public static String Do(object obj)
        {
            var type = obj.GetType();
            if (type.IsArray)
            {
                if (type.GetArrayRank() == 1)
                {
                    Array arr = obj as Array;
                    var text = "[";
                    for (int i = 0; i < arr.Length; i++)
                    {
                        var elem = arr.GetValue(i);
                        var elemType = elem.GetType();
                        text += elemType.IsClass && !elemType.IsArray ? Do(elem) : elem.ToString();
                        if (i != arr.Length - 1) text += ", ";
                    }
                    text += "]";

                    return text;
                }
                else return obj.ToString();
            }
            else if (type.IsValueType)
            {
                var text = "{ ";
                var fields = type.GetFields();
                for (int i = 0; i < fields.Length; i++)
                {
                    var f = fields[i];
                    var fobj = f.GetValue(obj);
                    text += f.Name + "=" + (fobj.GetType().IsArray ? Do(fobj) : fobj.ToString());
                    if (i != fields.Length - 1) text += ", ";
                }
                text += " }";

                return text;
            }
            else if (type.IsClass)
            {
                var text = "{ ";
                var props = type.GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    var p = props[i];
                    var pobj = p.GetValue(obj, null);
                    text += p.Name + "=" + (pobj.GetType().IsArray ? Do(pobj) : pobj.ToString());
                    if (i != props.Length - 1) text += ", ";
                }
                text += " }";

                return text;
            }
            else return obj.ToString();
        }
    }
}
