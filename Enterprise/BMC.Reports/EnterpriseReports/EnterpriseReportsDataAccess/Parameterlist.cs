using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.DataAccess;
using System.Data.SqlClient;
using System.Data;
using BMC.Common.ExceptionManagement;
using BMC.EnterpriseReportsTransport;
using System.Reflection;
using BMC.Reports;

namespace BMC.EnterpriseReportsDataAccess
{
  public class Parameterlist
    {
      public List<SqlParameter> GetParameters(string SPName)
      { SqlParameter[] oParamCollection = null;
              List<SqlParameter> oParamReturn = new List<SqlParameter>();
              try
              {
                      oParamCollection = SqlHelperParameterCache.GetSpParameterSet(DbConnection.EnterpriseConnectionString, SPName, false);
                      foreach (SqlParameter item in oParamCollection)
                      {
                          if (item.Direction == ParameterDirection.InputOutput || item.Direction == ParameterDirection.Output ||
                              item.Direction == ParameterDirection.ReturnValue)
                              continue;
                          else
                              oParamReturn.Add(item);
                      }                
              }
              catch (Exception ex)
              {
                  ExceptionManager.Publish(ex);
              }
              return oParamReturn.ToList();
          }


      public bool ExecuteQueryForRowCount(string ProcedureName,  clsSPParams spParams, bool SDSReports)
      {
          bool bRowCount = false;
          DataTable dtValue = null;
          using (SqlConnection con = new SqlConnection(DbConnection.EnterpriseConnectionString))
          {

              try
              {
                  SqlParameter[] paramas = SqlHelperParameterCache.GetSpParameterSet(DbConnection.EnterpriseConnectionString, ProcedureName);
                  if (spParams == null) 
                  {
                      dtValue = SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, ProcedureName).Tables[0];
                  }
                  else
                  {
                      for (int count = 0; count < paramas.Count(); count++)
                      {
                          foreach (PropertyInfo info in spParams.GetType().GetProperties())
                          {
                              if (info.Name.ToUpper() == paramas[count].ParameterName.Replace("@", "").ToUpper())
                              {
                                  object objValue = info.GetValue(spParams, null);
                                  if (objValue != null)
                                  {
                                      paramas[count].Value = objValue;
                                  }
                                  break;
                              }
                          }
                      }
                      dtValue = SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, ProcedureName, paramas).Tables[0];
                  }
                      

                  if (dtValue != null)
                      if (SDSReports)
                      {
                          if (dtValue.Rows.Count > 1)
                              bRowCount = true;
                      }
                      else
                      {
                          if (dtValue.Rows.Count > 0)
                              bRowCount = true;
                      }


              }
              catch (Exception exp)
              {
                  bRowCount = false;
                  ExceptionManager.Publish(exp);
              }
              return bRowCount;
          }
      }


      //public bool ExecuteQueryForRowCount(string ProcedureName, ListReportparameters lstParams, bool SDSReports)
      //{
      //    bool bRowCount = false;
      //    DataTable dtValue = null;
      //    using (SqlConnection con = new SqlConnection(DbConnection.EnterpriseConnectionString))
      //    {

      //        try
      //        {
      //            if (lstParams != null)
      //            {
      //                SqlParameter[] paramas = SqlHelperParameterCache.GetSpParameterSet(DbConnection.EnterpriseConnectionString, ProcedureName);

      //                for (int count = 0; count < paramas.Count(); count++)
      //                {
      //                    foreach (PropertyInfo info in lstParams.GetType().GetProperties())
      //                    {
      //                          if (info.Name.ToUpper() == paramas[count].ParameterName.Replace("@", "").ToUpper())
      //                          {
      //                              object objValue = info.GetValue(lstParams, null);
      //                              if (objValue != null)
      //                              {
      //                                  paramas[count].Value = objValue;
      //                              }
      //                              break;
      //                          }
      //                    }
      //                }
      //                dtValue = SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, ProcedureName, paramas).Tables[0];
      //            }
      //            else                 
      //                dtValue = SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, ProcedureName).Tables[0];
                  
      //            if (dtValue != null)
      //                if (SDSReports)
      //                {
      //                    if (dtValue.Rows.Count > 1)
      //                        bRowCount = true;
      //                }
      //                else
      //                {
      //                    if (dtValue.Rows.Count > 0)
      //                        bRowCount = true;
      //                }
                     
                  
      //        }
      //        catch (Exception exp)
      //        {
      //            bRowCount = false;                  
      //            ExceptionManager.Publish(exp);
      //        }
      //        return bRowCount;
      //    }
      //}


      public DataSet ExecuteDataSet(string ProcedureName, clsSPParams lstParams)
      {
         
          using (SqlConnection con = new SqlConnection(DbConnection.EnterpriseConnectionString))
          {

              try
              {
                  if (lstParams != null)
                  {
                      SqlParameter[] paramas = SqlHelperParameterCache.GetSpParameterSet(DbConnection.EnterpriseConnectionString, ProcedureName);

                      for (int count = 0; count < paramas.Count(); count++)
                      {
                            foreach (PropertyInfo info in lstParams.GetType().GetProperties())
                            {
                                if (info.Name.ToUpper() == paramas[count].ParameterName.Replace("@", "").ToUpper())
                                {
                                    object objValue = info.GetValue(lstParams, null);
                                    if (objValue != null)
                                    {
                                        paramas[count].Value = objValue;
                                    }

                                    break;
                                }
                            }
                      }
                      return SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, ProcedureName, paramas);
                      
                  }
                  else
                      return SqlHelper.ExecuteDataset(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, ProcedureName);


              }
              catch (Exception exp)
              {
                 
                  ExceptionManager.Publish(exp);
              }
              return null;
          }
      }


      public int GetReadException(string ProcedureName, clsSPParams lstParams)
      {
          int iValue = 0;
          try
          {          
              SqlParameter[] paramas = null;

              if (lstParams != null)
              {
                 paramas = SqlHelperParameterCache.GetSpParameterSet(DbConnection.EnterpriseConnectionString, ProcedureName);

                  for (int count = 0; count < paramas.Count(); count++)
                  {
                        foreach (PropertyInfo info in lstParams.GetType().GetProperties())
                        {
                            if (info.Name.ToUpper() == paramas[count].ParameterName.Replace("@", "").ToUpper())
                            {
                                object objValue = info.GetValue(lstParams, null);
                                if (objValue != null)                                  
                                    paramas[count].Value = objValue; 
                                break;
                            }
                        }
                  }
                  var val = SqlHelper.ExecuteNonQuery(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, ProcedureName, paramas);
                  iValue = (val != 0 ? val : 0);                  
              }
              else
              {
                  var val = SqlHelper.ExecuteNonQuery(DbConnection.EnterpriseConnectionString, CommandType.StoredProcedure, ProcedureName, paramas);
                  iValue = (val != 0 ? val : 0);                 
              }  
          }
          catch (Exception exp)
          {
              ExceptionManager.Publish(exp);
          }
          return iValue;
      }
  }
   
}
