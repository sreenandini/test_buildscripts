using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using BMC.CoreLib.Diagnostics;

namespace BMC.CoreLib
{
    public class TransactionHelper : DisposableObject
    {
        private TransactionScope _scope = null;

        public TransactionHelper(TransactionScopeOption option, IsolationLevel isolationLevel)
        {
            this.Init(option, isolationLevel);
        }

        private void Init(TransactionScopeOption option, IsolationLevel isolationLevel)
        {
            _scope = new TransactionScope(option, new TransactionOptions()
            {
                IsolationLevel = isolationLevel
            });
        }

        public void Save(bool rollback)
        {
            ModuleProc PROC = new ModuleProc(this.DYN_MODULE_NAME, "SaveChanges");

            try
            {
                if (_scope != null)
                {
                    if (!rollback)
                    {
                        _scope.Complete();
                    }
                    else
                    {
                        Transaction.Current.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                if (_scope != null)
                {
                    _scope.Dispose();
                }
            }
        }

        public void SaveAndDispose(bool rollback)
        {
            this.Save(rollback);
            this.Dispose();
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
        }
    }
}
