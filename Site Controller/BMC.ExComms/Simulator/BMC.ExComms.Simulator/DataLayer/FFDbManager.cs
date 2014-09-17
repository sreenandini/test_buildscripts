using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.SQLite.DBObjects;

namespace BMC.ExComms.Simulator.DataLayer
{
    internal sealed class FFDbManager
        : SQLiteDataManager
    {
        public FFDbManager()
            : base("Simulator.DB")
        {
            this.Initialize();
        }

        public override ISQLiteDbTable[] Tables
        {
            get
            {
                return new ISQLiteDbTable[] {
                    (this.GIMInformation = new FFTblGIMInformation(this, "GIMInformation")),
                    (this.CardInformation = new FFTblCardInformation(this, "CardInformation")),
                    (this.Settings = new FFTblSettings(this, "Settings")),
                };
            }
        }

        public FFTblGIMInformation GIMInformation { get; private set; }

        public FFTblCardInformation CardInformation { get; private set; }

        public FFTblSettings Settings { get; private set; }
    }
}
