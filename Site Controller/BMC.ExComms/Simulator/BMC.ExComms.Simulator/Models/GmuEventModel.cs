using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Contracts.DTO.Monitor;

namespace BMC.ExComms.Simulator.Models
{
    public class GmuEventCategoryModel
    {
        public GmuEventCategoryModel(string description)
        {
            this.Description = description;
            this.EventTypes = new GmuEventTypeModelCollection();
        }

        public string Description { get; set; }

        public GmuEventTypeModelCollection EventTypes { get; private set; }

        public GmuEventTypeModel AddEventType(FF_AppId_GMUEvent_XCodes exceptionCode, string description)
        {
            GmuEventTypeModel model = new GmuEventTypeModel(exceptionCode, description);
            this.EventTypes.AddWithSNo(model);
            return model;
        }

        public override string ToString()
        {
            return this.Description;
        }
    }

    public class GmuEventCategoryModelCollection : ObservableCollection<GmuEventCategoryModel>
    {
        public GmuEventCategoryModelCollection()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            using (ILogMethod method = Log.LogMethod("GmuEventCategoryModelCollection", "Initialize"))
            {
                try
                {
                    foreach (var category in ErrorEventCategoryFactory.Categories)
                    {
                        GmuEventCategoryModel modelCategory = new GmuEventCategoryModel(category.Key);
                        foreach (var item in category.Value)
                        {
                            modelCategory.AddEventType(item.ExceptionCode, item.Description);
                        }
                        this.Add(modelCategory);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }
    }

    public class GmuEventTypeModel
    {
        public GmuEventTypeModel(FF_AppId_GMUEvent_XCodes exceptionCode, string description)
        {
            this.ExceptionCode = exceptionCode;
            this.Description = description;
        }

        public int SNo { get; set; }

        public FF_AppId_GMUEvent_XCodes ExceptionCode { get; private set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return this.Description;
        }
    }

    public class GmuEventTypeModelCollection : ObservableCollection<GmuEventTypeModel>
    {
        public void AddWithSNo(GmuEventTypeModel model)
        {
            model.SNo = (this.Count + 1);
            this.Add(model);
        }
    }
}
