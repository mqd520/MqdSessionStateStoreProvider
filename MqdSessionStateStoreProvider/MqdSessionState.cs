using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace MqdSessionStateStoreProvider
{
    internal sealed class MqdSessionState
    {
        internal ISessionStateItemCollection _sessionItems;
        internal HttpStaticObjectsCollection _staticObjects;
        internal int _timeout;

        internal MqdSessionState(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
        {
            this.Copy(sessionItems, staticObjects, timeout);
        }

        internal void Copy(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
        {
            this._sessionItems = sessionItems;
            this._staticObjects = staticObjects;
            this._timeout = timeout;
        }

        public string ToJson()
        {
            if (_sessionItems == null || _sessionItems.Count == 0)
            {
                return null;
            }

            Dictionary<string, SessionStateItemValue> dict = new Dictionary<string, SessionStateItemValue>(_sessionItems.Count);

            NameObjectCollectionBase.KeysCollection keys = _sessionItems.Keys;
            string key;
            object objectValue = string.Empty;
            Type type = null;
            for (int i = 0; i < keys.Count; i++)
            {
                key = keys[i];
                objectValue = _sessionItems[key];
                if (objectValue != null)
                {
                    type = objectValue.GetType();
                }
                else
                {
                    type = typeof(object);
                }
                dict.Add(key, new SessionStateItemValue { Value = objectValue, Type = type });
            }

            SessionStateItem item = new SessionStateItem { Dict = dict, Timeout = this._timeout };

            return JsonConvert.SerializeObject(item);
        }

        public static MqdSessionState FromJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            try
            {
                SessionStateItem item = JsonConvert.DeserializeObject<SessionStateItem>(json);

                SessionStateItemCollection collections = new SessionStateItemCollection();

                SessionStateItemValue objectValue = null;

                JsonSerializer serializer = new JsonSerializer();
                StringReader sr = null;
                JsonTextReader tReader = null;

                foreach (KeyValuePair<string, SessionStateItemValue> kvp in item.Dict)
                {
                    objectValue = kvp.Value as SessionStateItemValue;
                    if (objectValue.Value == null)
                    {
                        collections[kvp.Key] = null;
                    }
                    else
                    {
                        if (!IsValueType(objectValue.Type))
                        {
                            sr = new StringReader(objectValue.Value.ToString());
                            tReader = new JsonTextReader(sr);
                            collections[kvp.Key] = serializer.Deserialize(tReader, objectValue.Type);
                        }
                        else
                        {
                            collections[kvp.Key] = objectValue.Value;
                        }
                    }
                }

                return new MqdSessionState(collections, null, item.Timeout);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsValueType(Type type)
        {
            if (type.IsValueType)
            {
                return true;
            }

            //基础数据类型
            if (type == typeof(string) 
                || type == typeof(char)
                || type == typeof(byte)
                || type == typeof(ushort) || type == typeof(short)
                || type == typeof(uint) || type == typeof(int)
                || type == typeof(Int64) || type == typeof(UInt64)
                || type == typeof(ulong) || type == typeof(long)
                || type == typeof(double) || type == typeof(float)
                || type == typeof(decimal)
                || type == typeof(bool)
                )
            {
                return true;
            }

            //可为null的基础数据类型
            if (type.IsGenericType && !type.IsGenericTypeDefinition)
            {
                Type genericType = type.GetGenericTypeDefinition();

                if (Object.ReferenceEquals(genericType, typeof(Nullable<>)))
                {
                    var actualType = type.GetGenericArguments()[0];
                    return IsValueType(actualType);

                }
            }
            return false;
        }
    }
}
