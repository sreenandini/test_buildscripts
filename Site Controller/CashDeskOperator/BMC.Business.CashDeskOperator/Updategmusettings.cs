using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.Transport;
using BMC.Common.ExceptionManagement;
using BMC.DBInterface.CashDeskOperator;
using System.Data;
using BMC.Common.LogManagement;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Threading;
using System.Windows.Threading;


namespace BMC.Business.CashDeskOperator
{
    public class UpdateGMUsettingBiz
    {
        GMUSettingsDataAccess dbAccess;

        public UpdateGMUsettingBiz()
        {
            dbAccess = new GMUSettingsDataAccess(ConnectionStringHelper.ExchangeConnectionString);

        }

        public List<GetGMUPosDetailsResult> GetUpdateGMISetting(string IPList)
        {
            List<GetGMUPosDetailsResult> obcoll = new List<GetGMUPosDetailsResult>();
            try
            {
                obcoll = dbAccess.GetGMUPosDetails(IPList).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return obcoll;
        }

        public class InnerWork
        {
            public SynchronizationContext Context { get; set; }
            public Func<GetGMUPosDetailsResult, bool> DoWork { get; set; }
            public Action AfterWork { get; set; }
        }

        public void GetUpdateGMISetting(string IPList, ObservableCollection<GetGMUPosDetailsResult> result,
            Func<GetGMUPosDetailsResult, bool> doWork, Action afterWork)
        {
            try
            {
                SynchronizationContext ctx = SynchronizationContext.Current;
                InnerWork work = new InnerWork()
                {
                    Context = ctx,
                    DoWork = doWork,
                    AfterWork = afterWork
                };

                new Thread(new ParameterizedThreadStart((o) =>
                {
                    InnerWork innerWork = (InnerWork)o;

                    try
                    {
                        ISingleResult<GetGMUPosDetailsResult> collection = dbAccess.GetGMUPosDetails(IPList);

                        if (collection != null)
                        {
                            foreach (var item in collection)
                            {
                                try
                                {
                                    if (innerWork.DoWork(item))
                                    {
                                        innerWork.Context.Send((c) =>
                                        {
                                            result.Add((GetGMUPosDetailsResult)item);
                                        }, null);
                                    }
                                    Thread.Sleep(100);
                                }
                                catch (Exception ex)
                                {
                                    ExceptionManager.Publish(ex);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.Publish(ex);
                    }
                    finally
                    {
                        if (innerWork.AfterWork != null)
                        {
                            innerWork.Context.Send((c) =>
                            {
                                innerWork.AfterWork();
                            }, null);
                        }
                    }
                }
                )).Start(work);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}

