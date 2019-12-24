using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

namespace MqdSessionStateStoreProvider
{
    public class MqdSessionStateStoreProvider : SessionStateStoreProviderBase
    {
        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            LogUtils.WriteInfo("------------------ CreateNewStoreData 1 ------------------");
            LogUtils.WriteInfo(context);
            LogUtils.WriteInfo(timeout);

            SessionStateStoreData data = CreateSessionStoreData(context, null, null, timeout);
            LogUtils.WriteInfo(data);

            LogUtils.WriteInfo("------------------ CreateNewStoreData 2 ------------------\r\n");

            return data;
        }

        internal static SessionStateStoreData CreateSessionStoreData(HttpContext context, ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
        {
            if (sessionItems == null)
            {
                sessionItems = new SessionStateItemCollection();
            }

            if (staticObjects == null && context != null)
            {
                staticObjects = SessionStateUtility.GetSessionStaticObjects(context);
            }

            return new SessionStateStoreData(sessionItems, staticObjects, timeout);
        }

        public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
        {
            LogUtils.WriteInfo("------------------ CreateUninitializedItem 1 ------------------");
            LogUtils.WriteInfo(context);
            LogUtils.WriteInfo(id);
            LogUtils.WriteInfo(timeout);

            MqdSessionState state = new MqdSessionState(null, null, timeout);

            LogUtils.WriteInfo(state);

            RedisUtils.SetString(id, state.ToJson(), timeout);

            LogUtils.WriteInfo("------------------ CreateUninitializedItem 2 ------------------\r\n");
        }

        private SessionStateStoreData DoGet(HttpContext context, string id, bool exclusive, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
        {
            locked = false;
            lockId = null;
            lockAge = TimeSpan.Zero;
            actionFlags = SessionStateActions.None;

            string data = RedisUtils.GetString(id);
            MqdSessionState state = MqdSessionState.FromJson(data);
            if (state == null)
            {
                return null;
            }

            RedisUtils.SetExpire(id, state._timeout);
            return CreateSessionStoreData(context, state._sessionItems, state._staticObjects, state._timeout);
        }

        public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
        {
            LogUtils.WriteInfo("------------------ GetItem 1 ------------------");
            LogUtils.WriteInfo(context);
            LogUtils.WriteInfo(id);


            SessionStateStoreData data = this.DoGet(context, id, false, out locked, out lockAge, out lockId, out actionFlags);

            LogUtils.WriteInfo(locked);
            LogUtils.WriteInfo(lockAge);
            LogUtils.WriteInfo(lockId);
            LogUtils.WriteInfo(actionFlags);
            LogUtils.WriteInfo(data);
            LogUtils.WriteInfo("------------------ GetItem 2 ------------------\r\n");

            return data;
        }

        public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actionFlags)
        {
            LogUtils.WriteInfo("------------------ GetItemExclusive 1 ------------------");

            LogUtils.WriteInfo(context);
            LogUtils.WriteInfo(id);

            SessionStateStoreData data = this.DoGet(context, id, true, out locked, out lockAge, out lockId, out actionFlags);
            LogUtils.WriteInfo(locked);
            LogUtils.WriteInfo(lockAge);
            LogUtils.WriteInfo(lockId);
            LogUtils.WriteInfo(actionFlags);
            LogUtils.WriteInfo(data);

            LogUtils.WriteInfo("------------------ GetItemExclusive 2 ------------------\r\n");

            return data;
        }

        public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
        {
            LogUtils.WriteInfo("------------------ SetAndReleaseItemExclusive 1 ------------------");

            ISessionStateItemCollection sessionItems = null;
            HttpStaticObjectsCollection staticObjects = null;

            if (item.Items.Count > 0)
            {
                sessionItems = item.Items;
            }

            if (!item.StaticObjects.NeverAccessed)
            {
                staticObjects = item.StaticObjects;
            }

            LogUtils.WriteInfo(context);
            LogUtils.WriteInfo(id);
            LogUtils.WriteInfo(item);
            LogUtils.WriteInfo(lockId);
            LogUtils.WriteInfo(newItem);
            LogUtils.WriteInfo(staticObjects);
            LogUtils.WriteInfo(sessionItems);


            MqdSessionState state2 = new MqdSessionState(sessionItems, staticObjects, item.Timeout);

            LogUtils.WriteInfo(state2);

            RedisUtils.SetString(id, state2.ToJson(), item.Timeout);

            LogUtils.WriteInfo("------------------ SetAndReleaseItemExclusive 2 ------------------\r\n");
        }

        public override void Dispose()
        {
            LogUtils.WriteInfo("------------------ Dispose 1 ------------------");

            LogUtils.WriteInfo("------------------ Dispose 2 ------------------\r\n");
        }

        public override void EndRequest(HttpContext context)
        {
            LogUtils.WriteInfo("------------------ EndRequest 1 ------------------");

            LogUtils.WriteInfo(context);

            LogUtils.WriteInfo("------------------ EndRequest 2 ------------------\r\n");
        }

        public override void InitializeRequest(HttpContext context)
        {
            LogUtils.WriteInfo("------------------ InitializeRequest 1 ------------------");

            LogUtils.WriteInfo(context);

            LogUtils.WriteInfo("------------------ InitializeRequest 2 ------------------\r\n");
        }

        public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
        {
            LogUtils.WriteInfo("------------------ ReleaseItemExclusive 1 ------------------");

            LogUtils.WriteInfo(context);
            LogUtils.WriteInfo(id);
            LogUtils.WriteInfo(lockId);

            LogUtils.WriteInfo("------------------ ReleaseItemExclusive 2 ------------------\r\n");
        }

        public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
        {
            LogUtils.WriteInfo("------------------ RemoveItem 1 ------------------");

            LogUtils.WriteInfo(context);
            LogUtils.WriteInfo(id);
            LogUtils.WriteInfo(lockId);
            LogUtils.WriteInfo(item);

            LogUtils.WriteInfo("------------------ RemoveItem 2 ------------------\r\n");
        }

        public override void ResetItemTimeout(HttpContext context, string id)
        {
            LogUtils.WriteInfo("------------------ ResetItemTimeout 1 ------------------");

            LogUtils.WriteInfo(context);
            LogUtils.WriteInfo(id);

            LogUtils.WriteInfo("------------------ ResetItemTimeout 2 ------------------\r\n");
        }

        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            LogUtils.WriteInfo("------------------ SetItemExpireCallback 1 ------------------");

            LogUtils.WriteInfo(expireCallback);

            LogUtils.WriteInfo("------------------ SetItemExpireCallback 2 ------------------\r\n");

            return true;
        }
    }
}
