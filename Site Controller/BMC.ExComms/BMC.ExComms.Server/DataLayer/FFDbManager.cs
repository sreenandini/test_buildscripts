using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.SQLite.DBObjects;

namespace BMC.ExComms.Server.DataLayer
{
    internal sealed class FFDbManager 
        : SQLiteDataManager
    {
        public FFDbManager()
            : base("ExComms.DB")
        {
            this.Initialize();
        }

        //public override ISQLiteDbTable[] Tables
        //{
        //    get
        //    {
        //        return new ISQLiteDbTable[] {
        //            (this.PriorityMessages_GIM = new DbTblFreeformMessages(this, "PriorityMessages_GIM")),
        //            (this.PriorityMessages = new DbTblFreeformMessages(this, "PriorityMessages")),
        //            (this.NonPriorityMessages = new DbTblFreeformMessages(this, "NonPriorityMessages")),
        //        };
        //    }
        //}

        //public DbTblFreeformMessages PriorityMessages_GIM { get; private set; }

        //public DbTblFreeformMessages PriorityMessages { get; private set; }

        //public DbTblFreeformMessages NonPriorityMessages { get; private set; }

        public override ISQLiteDbTable[] Tables
        {
            get { throw new NotImplementedException(); }
        }
    }
}
