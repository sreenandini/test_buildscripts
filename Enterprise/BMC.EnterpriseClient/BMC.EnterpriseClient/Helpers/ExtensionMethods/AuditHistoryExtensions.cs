using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Audit.Transport;
using BMC.Common.Utilities;
using BMC.EnterpriseBusiness.Business;

namespace BMC.EnterpriseClient.Helpers.ExtensionsMethods
{
    public static class AuditHistoryExtensions
    {
        private static AuditBusiness auditBusiness = new AuditBusiness(DatabaseHelper.GetEnterpriseConnectionString());

        /// <summary>
        /// Initialize Audit_History entity
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <returns>Audit_History</returns>
        public static Audit_History AddEntry(this Audit_History request)
        {
            request.Audit_User_ID = AppEntryPoint.Current.UserId;
            request.Audit_User_Name = AppEntryPoint.Current.UserName;
            return request;
        }

        /// <summary>
        /// Set module id and module name
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="moduleName">ModuleNameEnterprise</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetModule(this Audit_History request, ModuleNameEnterprise moduleName)
        {
            request.EnterpriseModuleName = moduleName;
            return request;
        }

        /// <summary>
        /// Set the screen name
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="screenName">string</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetScreen(this Audit_History request, string screenName)
        {
            request.Audit_Screen_Name = screenName;
            return request;
        }

        /// <summary>
        /// Set the modified field name
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="fieldName">string</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetField(this Audit_History request, string fieldName)
        {
            request.Audit_Field = fieldName;
            return request;
        }

        /// <summary>
        /// Set the unmodified value
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="oldValue">string</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetOldValue(this Audit_History request, string oldValue)
        {
            request.Audit_Old_Vl = oldValue;
            return request;
        }

        /// <summary>
        /// Set the modified value
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="newValue">string</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetNewValue(this Audit_History request, string newValue)
        {
            request.Audit_New_Vl = newValue;
            return request;
        }

        /// <summary>
        /// Set the description for modification
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="description">string</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetDescription(this Audit_History request, string description)
        {
            request.Audit_Desc = description;
            return request;
        }

        /// <summary>
        /// Set the description for modification
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="descriptionFormat"></param>
        /// <param name="newValue">string</param>
        /// <param name="oldValue">string</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetDescription(this Audit_History request, string descriptionFormat, string newValue, string oldValue)
        {
            if (!string.IsNullOrEmpty(request.Audit_Field))
            {
                return request.SetDescription(string.Format(descriptionFormat, request.Audit_Field, newValue, oldValue));
            }
            else
            {
                return request.SetDescription(string.Format(descriptionFormat, newValue, oldValue));
            }
        }

        /// <summary>
        /// Set the description for modification
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="newValue">string</param>
        /// <param name="oldValue">string</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetDescription(this Audit_History request, string newValue, string oldValue)
        {
            if (!string.IsNullOrEmpty(request.Audit_Field))
            {
                return request.SetDescription(string.Format(request.Audit_Desc, request.Audit_Field, newValue, oldValue));
            }
            else
            {
                return request.SetDescription(string.Format(request.Audit_Desc, newValue, oldValue));
            }
        }

        /// <summary>
        /// Set the type of modification(add/modify/delete)
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="type">OperationType</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetOperationType(this Audit_History request, OperationType type)
        {
            request.AuditOperationType = type;
            return request;
        }


        /// <summary>
        /// Set the slot details
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="slot">string</param>
        /// <returns>Audit_History</returns>
        public static Audit_History SetSlot(this Audit_History request, string slot)
        {
            request.Audit_Slot = slot;
            return request;
        }   


        
        /// <summary>
        /// Insert all modified or new changes to database
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="original">object</param>
        /// <param name="updated">object</param>
        /// <param name="fields">string[]</param>
        /// <param name="isInsert">bool</param>
        public static void InsertAuditEntries(this Audit_History request, object original, object updated, /*string[] fields,*/ bool isInsert, Func<string, string, bool> conditionalCheck = null, Func<object, object, OperationType> setOperationType = null)
        {
            List<Audit_History> auditHistories = new List<Audit_History>();
            string newValue, oldValue;
            if (isInsert || original.GetType() == updated.GetType() && !object.ReferenceEquals(original, updated))
            {
                PropertyInfo[] propertyInfo = (updated
                                                .GetType()
                                                .GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                                                .Where(p => !p.GetIndexParameters().Any())
                                                .Where(p => p.CanRead && p.CanWrite)).ToArray();
                                                //.Where(p => p.GetType().IsValueType))
                                                //.Where(p => p.GetValue(updated, null) != null))

                

                //string[] updatedProperties = (propertyInfo.Select(x => x.GetValue(updated, null)).Where(val => val != null)).Select(x => x.ToString()).ToArray();
                //string[] originalProperties = (propertyInfo.Select(x => x.GetValue(original, null)).Where(val => val != null)).Select(x => x.ToString()).ToArray();

                //if (fields.Count() == propertyInfo.Count())
                //{

                    for (int i = 0; i < propertyInfo.Count(); i++)
                    {
                        //if (propertyInfo[i].PropertyType.GetDefault() != propertyInfo[i].GetValue(updated, null))

                        {
                            newValue = updated != null ? propertyInfo[i].GetValue(updated, null) != null ? propertyInfo[i].GetValue(updated, null).ToString() : string.Empty : string.Empty;
                            oldValue = original != null ? propertyInfo[i].GetValue(original, null) != null ? propertyInfo[i].GetValue(original, null).ToString() : string.Empty : string.Empty;

                            if (conditionalCheck != null && !conditionalCheck(oldValue, newValue))
                            {
                                continue;
                            }

                            if (setOperationType != null)
                            {
                                request.SetOperationType(setOperationType(propertyInfo[i].GetValue(updated, null), propertyInfo[i].GetValue(original, null)));
                            }

                            //if (isInsert || newValue != oldValue)

                            if (newValue != oldValue)
                            {
                                request
                                    .Clone()
                                    .SetNewValue(newValue)
                                    .SetOldValue(oldValue)
                                    .SetField(propertyInfo[i].Name)
                                    .SetDescription(newValue, oldValue)
                                    .InsertEntry(true);
                            }
                        }
                        //auditHistories.Add(GetRequest(request, newValue, oldValue, fields[i]));
                    }
                    //auditBusiness.InsertAuditData(auditHistories.ToArray());
                //}
            }
        }


        /// <summary>
        /// Insert the Audit_History instance to database
        /// </summary>
        /// <param name="request">Audit_History</param>
        /// <param name="isValid">bool</param>
        public static void InsertEntry(this Audit_History request, bool isValid = true)
        {
            if (isValid)
            {
                if (request.Audit_Slot == null)
                {
                    request.SetSlot(string.Empty);
                }
                if (request.Audit_Field == null)
                {
                    request.SetField(string.Empty);
                }
                if (request.Audit_Old_Vl == null)
                {
                    request.SetOldValue(string.Empty);
                }
                if (request.Audit_New_Vl == null)
                {
                    request.SetNewValue(string.Empty);
                }
                auditBusiness.InsertAuditData(request, false);
            }
        }
    }
}
