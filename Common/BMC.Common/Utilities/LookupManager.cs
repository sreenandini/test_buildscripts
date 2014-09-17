using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace BMC.Common.Utilities
{
    public class LookupManager
    {
        private readonly LookupManagerDataContext _lookupDataContext;

        public LookupManager(string connectionString)
        {
            _lookupDataContext = new LookupManagerDataContext(connectionString);
        }

        public IList<LookupObject> GetLookupObject(string languageCode, string lookupCode)
        {
            return (from context in _lookupDataContext.LookupMasters
                    join languageLookup in _lookupDataContext.LanguageLookups on context.DisplayText equals
                        languageLookup.LookupMasterID into leftJoin
                    from subset in leftJoin.DefaultIfEmpty()
                    select
                        new LookupObject
                            {
                                CodeID = context.ID,
                                DisplayText =
                                    string.IsNullOrEmpty(subset.ForeignText) ? context.DisplayText : subset.ForeignText,
                                    CodeValue = context.Code,
                                    parentID = context.ParentId

                            }).
                ToList();
        }

        public IList<LookupObject> GetLookupObject(string lookupCode)
        {
            return (from context in _lookupDataContext.LookupMasters
                    where context.Code == lookupCode
                    select new LookupObject {CodeID = context.ID, DisplayText = context.DisplayText, parentID = context.ParentId}).ToList();
        }

        public IList<LookupObject> GetLookupObject(int codeID)
        {
            return (from context in _lookupDataContext.LookupMasters
                    where context.ID == codeID
                    select new LookupObject { CodeID = context.ID, DisplayText = context.DisplayText, parentID = context.ParentId }).ToList();
        }

        public IList<LookupObject> GetLookupObject(int? parentID)
        {
            return (from context in _lookupDataContext.LookupMasters
                    where context.ParentId == parentID
                    select new LookupObject { CodeID = context.ID, DisplayText = context.DisplayText, parentID = context.ParentId }).ToList();
        }
        
        public void InsertLookupObject(string lookupCode, string displayText, int? parentID)
        {
            InsertLookupObject(lookupCode, displayText, "en-US", displayText, parentID);
        }

        public void InsertLookupObject(string lookupCode, string displayText, string languageCode, string foreignText, int? parentID)
        {
            if ((from lookupMaster in _lookupDataContext.LookupMasters.ToList()
                 where lookupMaster.DisplayText.ToLower() == displayText.ToLower() && lookupMaster.Code.ToLower() == lookupCode.ToLower()
                 && lookupMaster.ParentId == parentID
                 select lookupMaster.ID).Count() == 0)
            {
                _lookupDataContext.LookupMasters.InsertOnSubmit(new LookupMaster
                                                                    {
                                                                        Code = lookupCode,
                                                                        Description = displayText,
                                                                        DisplayText = displayText,
                                                                        ParentId = parentID,
                                                                        ID = 0
                                                                    });
                _lookupDataContext.SubmitChanges();

                int retId = (from lookupMaster in _lookupDataContext.LookupMasters
                             where lookupMaster.DisplayText == displayText && lookupMaster.Code == lookupCode
                             select lookupMaster.ID).FirstOrDefault();

                InsertIntoExportHistory("LOOKUPMASTER", retId.ToString());
            }

            LanguageLookup k = (from languageLookup in _lookupDataContext.LanguageLookups
                                where
                                    languageLookup.LookupMasterID == displayText &&
                                    languageLookup.LanguageCode == languageCode
                                select languageLookup).FirstOrDefault();

            if (k != null) return;

            _lookupDataContext.LanguageLookups.InsertOnSubmit(new LanguageLookup
                                                                  {
                                                                      ForeignText = foreignText,
                                                                      ID = 0,
                                                                      LanguageCode = languageCode,
                                                                      LookupMasterID = displayText
                                                                  });
            _lookupDataContext.SubmitChanges();

            int returnId = (from languageLookup in _lookupDataContext.LanguageLookups
                            where
                                languageLookup.LookupMasterID == displayText &&
                                languageLookup.LanguageCode == languageCode
                            select languageLookup.ID).FirstOrDefault();

            InsertIntoExportHistory("LANGUAGELOOKUP", returnId.ToString());
        }

        public void UpdateLookupObject(int id, string displayText)
        {
            UpdateLookupObject(id, displayText, "en-US", displayText);
        }

        public void UpdateLookupObject(int id, string displayText, string languageCode, string foreignText)
        {
            LookupMaster k = (from context in _lookupDataContext.LookupMasters
                              where context.ID == id
                              select context).FirstOrDefault();
            if (k != null)
            {
                k.DisplayText = displayText;
                _lookupDataContext.SubmitChanges();
            }

            LanguageLookup i = (from languageLookup in _lookupDataContext.LanguageLookups
                                where
                                    languageLookup.LookupMasterID == displayText &&
                                    languageLookup.LanguageCode == languageCode
                                select languageLookup).FirstOrDefault();

            if (i == null) return;

            i.ForeignText = foreignText;
            _lookupDataContext.SubmitChanges();
        }

        public LookupObject GetCodeDescription(string code)
        {
            LookupObject k = (from codeMaster in _lookupDataContext.CodeMasters
                              where codeMaster.Code == code
                              select
                                  new LookupObject()
                                      {
                                          CodeID = (int) codeMaster.ID,
                                          CodeValue = codeMaster.Code,
                                          DisplayText = codeMaster.Description,
                                          parentID = codeMaster.ParentID
                                      }).FirstOrDefault();

            return k ?? new LookupObject();
        }

        public LookupObject GetCodeDescription(int codeID)
        {
            LookupObject k = (from codeMaster in _lookupDataContext.CodeMasters
                              where codeMaster.ID == codeID
                              select
                                  new LookupObject()
                                  {
                                      CodeID = (int)codeMaster.ID,
                                      CodeValue = codeMaster.Code,
                                      DisplayText = codeMaster.Description,
                                      parentID = codeMaster.ParentID
                                  }).FirstOrDefault();

            return k ?? new LookupObject();
        }
        
        public void RemoveLookupObject(int codeID)
        {
            _lookupDataContext.LookupMasters.DeleteOnSubmit(
                _lookupDataContext.LookupMasters.Where(x => x.ID == codeID).Select(x => x).FirstOrDefault());
            _lookupDataContext.SubmitChanges();
            InsertIntoExportHistory("LOOKUPMASTER", codeID.ToString());
        }

        public void InsertIntoExportHistory(string exportType, string referenceId)
        {
            foreach (Site site in _lookupDataContext.Sites)
                _lookupDataContext.usp_Export_History(referenceId, exportType, site.Site_ID);
        }
    }

    public class LookupObject
    {
        public int CodeID { get; set; }
        public string CodeValue { get; set; }
        public string DisplayText { get; set; }
        public int? parentID { get; set; }
    }

}