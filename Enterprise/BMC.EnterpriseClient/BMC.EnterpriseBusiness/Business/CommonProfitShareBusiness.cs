using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.EnterpriseBusiness.Entities;
using BMC.EnterpriseDataAccess;
using BMC.Common.ExceptionManagement;

namespace BMC.EnterpriseBusiness.Business
{
    public enum CommonProfitShareType
    {
        ProfitShare = 0,
        ExpenseShare
    }

    public class CommonProfitShareBusiness : DisposableObject
    {
        public CommonProfitShareBusiness() { }

        public List<CommonProfitShareGroupEntity> GetShareGroups(CommonProfitShareType commonType)
        {
            List<CommonProfitShareGroupEntity> result = new List<CommonProfitShareGroupEntity>();

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    foreach (var entity in context.rsp_GetCommonProfitShareGroups((short)commonType))
                    {
                        result.Add(new CommonProfitShareGroupEntity()
                        {
                            Id = entity.Id,
                            Name = entity.Name,
                            Percentage = entity.Percentage,
                            Description = entity.Description
                        });
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            return result;
        }

        public List<CommonProfitShareEntity> GetShares(CommonProfitShareType commonType, int parentGroupId)
        {
            List<CommonProfitShareEntity> result = new List<CommonProfitShareEntity>();

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    foreach (var entity in context.rsp_GetCommonProfitShares((short)commonType, parentGroupId))
                    {
                        result.Add(new CommonProfitShareEntity()
                        {
                            Id = entity.Id,
                            Percentage = entity.Percentage,
                            Description = entity.Description,
                            ShareHolder = new ShareHolderEntity()
                            {
                                Id = entity.ShareHolderId,
                                Name = entity.ShareHolderName
                            }
                        });
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            return result;
        }

        public List<ShareHolderEntity> GetShareHolders(CommonProfitShareType commonType, int shareGroupId, int shareId)
        {
            List<ShareHolderEntity> result = new List<ShareHolderEntity>();

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    foreach (var entity in context.rsp_GetCommonShareHolders((short)commonType, shareGroupId, shareId))
                    {
                        result.Add(new ShareHolderEntity()
                        {
                            Id = entity.Id,
                            Name = entity.Name
                        });
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            return result;
        }

        public double GetProfitSharePercentage(CommonProfitShareType commonType, int parentGroupId, int shareId)
        {
            double result = 0;

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    foreach (var entity in context.rsp_GetCommonProfitSharePercentage((short)commonType, parentGroupId, shareId))
                    {
                        if (entity.TotalPercentage != null &&
                            entity.TotalPercentage.HasValue)
                        {
                            result = entity.TotalPercentage.Value;
                        }
                        break;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            return result;
        }

        public bool DeleteShareGroup(CommonProfitShareType commonType, int shareGroupId)
        {
            int? result = null;

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    switch (commonType)
                    {
                        case CommonProfitShareType.ExpenseShare:
                            context.usp_DeleteExpenseShareGroup (shareGroupId, ref result);
                            break;

                        default:
                            context.usp_DeleteProfitShareGroup (shareGroupId, ref result);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            return (result != null && result.HasValue && result.Value > 0);
        }

        public bool DeleteShare(CommonProfitShareType commonType, int shareGroupId, int shareId)
        {
            int? result = null;

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    switch (commonType)
                    {
                        case CommonProfitShareType.ExpenseShare:
                            context.usp_DeleteExpenseShare(shareGroupId, shareId, ref result);
                            break;

                        default:
                            context.usp_DeleteProfitShare(shareGroupId, shareId, ref result);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            return (result != null && result.HasValue && result.Value > 0);
        }

        public bool ModifyShareGroup(CommonProfitShareType commonType, CommonProfitShareGroupEntity entity)
        {
            bool result = false;
            int? resultId = 0;

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    switch (commonType)
                    {
                        case CommonProfitShareType.ExpenseShare:
                            context.usp_UpdateExpenseShareGroupDetails(entity.Id, entity.Name,
                             Math.Round((float)entity.Percentage, 2), entity.Description, ref resultId);
                            break;

                        case CommonProfitShareType.ProfitShare:
                            context.usp_UpdateProfitShareGroupDetails(entity.Id, entity.Name,
                                Math.Round((float)entity.Percentage, 2), entity.Description, ref resultId);
                            break;

                        default:
                            break;
                    }
                    if (resultId != null && resultId.HasValue)
                    {
                        result = (resultId.Value > 0);
                        entity.Id = resultId.Value;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            return result;
        }

        public bool ModifyShare(CommonProfitShareType commonType, CommonProfitShareEntity entity)
        {
            bool result = false;

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    switch (commonType)
                    {
                        case CommonProfitShareType.ExpenseShare:
                            context.usp_UpdateExpenseShareDetails(entity.ShareHolder.Id, entity.Parent.Id,
                                entity.Id,Math.Round( (float)entity.Percentage,2), entity.Description);
                            break;

                        default:
                            context.usp_UpdateProfitShareDetails(entity.ShareHolder.Id, entity.Parent.Id,
                                entity.Id,Math.Round( (float)entity.Percentage,2), entity.Description);
                            break;
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            return result;
        }

        public bool IsShareGroupAlreadyExists(CommonProfitShareType commonType, string name, int entityId)
        {
            int? result = null;

            using (EnterpriseDataContext context = EnterpriseDataContextHelper.GetDataContext())
            {
                try
                {
                    switch (commonType)
                    {
                        case CommonProfitShareType.ExpenseShare:
                            context.rsp_CheckExpenseShareGroupNameExists(name, entityId, ref result);
                            break;

                        default:
                            context.rsp_CheckProfitShareGroupNameExists(name, entityId, ref result);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ExceptionManager.Publish(ex);
                }
            }

            return (result != null && result.HasValue && result.Value > 0);
        }
    }
}
