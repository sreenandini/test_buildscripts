using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.Handlers
{
    class RequestResponseMapItem
        : DisposableObject
    {
        //private readonly IDictionary<FF_AppId_SessionIds, int> _transactionIds = null;

        public RequestResponseMapItem()
        {
            //_transactionIds = new SortedDictionary<FF_AppId_SessionIds, int>();
        }

        internal void CopyTo(IFreeformEntity_Msg message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CopyTo"))
            {
                try
                {
                    //FF_AppId_SessionIds sessionId = message.SessionID;
                    //if (_transactionIds.ContainsKey(sessionId))
                    //{
                    //    int transactionId = _transactionIds[sessionId];
                    //    if (transactionId > 0)
                    //    {
                    //        message.TransactionID = transactionId;
                    //        _transactionIds[sessionId] = 0;
                    //    }
                    //}
                    if (this.IsValid)
                    {
                        message.SessionID = this.SessionID;
                        message.TransactionID = this.TransactionID;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        internal void CopyFrom(IFreeformEntity_Msg message)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "CopyFrom"))
            {
                try
                {
                    this.SessionID = message.SessionID;
                    this.TransactionID = message.TransactionID;
                    this.IsValid = true;
                    //_transactionIds.AddOrUpdate2(message.SessionID, message.TransactionID);
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        public FF_AppId_SessionIds SessionID { get; private set; }
        public int TransactionID { get; private set; }
        public bool IsValid { get; set; }

        public void Clear()
        {
            this.TransactionID = 0;
            this.IsValid = false;
        }

        protected override void ToString(StringBuilder sb)
        {
            //if (_transactionIds.Count > 0)
            //{
            //    foreach (var item in _transactionIds)
            //    {
            //        if (sb.Length > 0) sb.AppendLine();
            //        sb.Append(item.Key.ToString() + ", " + item.Value.ToString());
            //    }
            //}
            if (IsValid)
            {
                sb.Append(this.SessionID.ToString() + ", " + this.TransactionID.ToString());
            }
            else
            {
                sb.Append("{NOT SET}");
            }
        }
    }

    class RequestResponseMapItems 
        : DoubleKeyConcurrentDictionary<string, string, RequestResponseMapItem>
    {
        internal RequestResponseMapItems()
            : base(null, StringComparer.OrdinalIgnoreCase, StringComparer.OrdinalIgnoreCase) { }

        internal bool Persist(IFreeformEntity_Msg request, IFreeformEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod(this.DYN_MODULE_NAME, "PersistRequestResponseMapItem"))
            {
                try
                {
                    string typeName = FFMsgHandler.CreateSessionTargetKey((int)request.SessionID, target.TypeKey);
                    RequestResponseMapItem mapItem = null;
                    bool copyTo = false;

                    // save the request/response map item
                    if (request is FFMsg_G2H &&
                        this.IsKey1Exists(typeName))
                    {
                        mapItem = this.GetValueFromKey1(typeName);
                    }
                    // get the request/response map item
                    else if (request is FFMsg_H2G &&
                            this.IsKey2Exists(typeName))
                    {
                        mapItem = this.GetValueFromKey2(typeName);
                        copyTo = true;
                    }

                    // copy from/to request/response map item
                    if (mapItem != null)
                    {
                        if (copyTo)
                        {
                            mapItem.CopyTo(request);
                        }
                        else
                        {
                            mapItem.CopyFrom(request);
                        }
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }

                return false;
            }
        }
    }
}
