using BMC.Common.ExceptionManagement;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BMC.EnterpriseBusiness.Business
{
    public class TermsProfilesBusiness
    {
        private static TermsProfilesBusiness _termsProfilesBusiness;

        public TermsProfilesBusiness() { }

        /// <summary>
        /// To create a new instance for TermsProfilesBusiness and return the created instance
        /// </summary>
        /// <returns></returns>
        public static TermsProfilesBusiness CreateInstance()
        {
            if (_termsProfilesBusiness == null)
                _termsProfilesBusiness = new TermsProfilesBusiness();

            return _termsProfilesBusiness;
        }

        public int InsertOrUpdateTermsGroup(string termGroup, int? termGroupID, ref int? termsGroupIDOut)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    return DataContext.InsertOrUpdateTermsGroup(termGroup, termGroupID, ref termsGroupIDOut);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return 0;
            }
        }

        public List<TermsGroupResult> GetTermsGroup()
        {
            List<TermsGroupResult> entityTermsGroup = new List<TermsGroupResult>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var entity in DataContext.GetTermsGroup())
                    {
                        entityTermsGroup.Add(new TermsGroupResult()
                        {   
                            Terms_Group_ID = entity.Terms_Group_ID,
                            Terms_Group_Name = entity.Terms_Group_Name
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return entityTermsGroup;
        }

        public void InsertTermsProfiles(int termsGroupID, int machineID)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.InsertTermsProfiles(termsGroupID, machineID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public List<TermsProfilesEntity> GetTermsProfileResultForGroupID(int selectedTermGroupID)
        {
            List<TermsProfilesEntity> getTermsProfileResult = new List<TermsProfilesEntity>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var termsProfileEntity in DataContext.GetTermsProfileResultForGroupID(selectedTermGroupID))
                    {
                        getTermsProfileResult.Add(new TermsProfilesEntity()
                        {
                            Machine_Type_ID = termsProfileEntity.Machine_Type_ID,
                            Terms_Profile_ID = termsProfileEntity.Terms_Profile_ID,
                            Machine_Type_Code = termsProfileEntity.Machine_Type_Code
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return getTermsProfileResult;
        }

        public List<UnAssignedMachineTypes> GetUnAssignedMachineTypes(int termGroupID)
        {
            List<UnAssignedMachineTypes> machineTypeTermsProfile = new List<UnAssignedMachineTypes>();
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    foreach (var machineTypeTerms in DataContext.GetUnassignedMachineTypes(termGroupID))
                    {
                        machineTypeTermsProfile.Add(new UnAssignedMachineTypes()
                        {
                            Machine_Type_ID = machineTypeTerms.Machine_Type_ID,
                            Machine_Type_Code = machineTypeTerms.Machine_Type_Code
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            return machineTypeTermsProfile;
        }

        public void DeleteTermsProfiles(int termProfileID)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.DeleteTermsProfiles(termProfileID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void CopyTermsGroupProfile(string termGroupNewName, int _termsGroupToCopy)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.CopyTermsGroupProfile(termGroupNewName, _termsGroupToCopy);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public BMC.EnterpriseDataAccess.EnterpriseDataContext.InstallationCount GetTermsGroupCountForInstallation(int termsGroupID)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    var installedMachines = DataContext.GetTermsGroupCountForInstallation(termsGroupID).ToList();
                    return installedMachines!= null && installedMachines.Count> 0 ? installedMachines[0] : null;
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return null;
            }
        }

        public void DeleteTermsGroup(int termsGroupID)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.DeleteTermsGroup(termsGroupID);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }

        public void AddOrCopyTermsProfileForMachineTypes(int machineID, int termGroupID, int termProfileID, string processType)
        {
            try
            {
                using (EnterpriseDataContext DataContext = EnterpriseDataContextHelper.GetDataContext())
                {
                    DataContext.AddOrCopyTermsProfileForMachineTypes(machineID, termGroupID, termProfileID, processType);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
        }   
    }
}
