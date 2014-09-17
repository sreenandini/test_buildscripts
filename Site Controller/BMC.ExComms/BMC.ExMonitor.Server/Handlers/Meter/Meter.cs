using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Monitor;
using BMC.CoreLib;
using BMC.ExComms.DataLayer.MSSQL;
using BMC.Common.ExceptionManagement;

namespace BMC.ExMonitor.Server.Handlers.Meter
{
    internal class MonitorHandler_Meter
        : MonitorHandlerBase
    {
        protected override bool OnExecuteInternal(MonitorExecutionContext context, MonitorEntity_MsgTgt target)
        {
            using (ILogMethod method = Log.LogMethod("MonitorHandler_Meter", "ProcessG2HMessageInternal"))
            {
                try
                {
                    MonTgt_G2H_Meters monMeters = context.G2HMessage.Meters;
                    if (monMeters == null)
                    {
                        method.Info("Meters are null. No need to update the meters.");
                        return true;
                    }

                    if (ExCommsDataContext.Current.UpdateFloorFinancialsFromMeter(
                                                    context.G2HMessage.InstallationNo,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Cancelled_Credits].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_100000].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Coins_In].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Coins_Out].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Coins_Drop].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Jackpot].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Games_Bet].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Games_Won].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Door_Open].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Power_Reset].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_1].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_5].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_10].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_20].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_50].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_100].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_250].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_10000].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_20000].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_25000].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_50000].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Games_Lost].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Current_Credits].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.External_Coin_Amount].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.True_Coin_In].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.True_Coin_Out].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Handpay_Cancelled_Credits].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Tickets_Cashable_In_Value].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Tickets_Cashable_Out_Value].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Tickets_Noncashable_In_Value].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Tickets_Noncashable_Out_Value].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Tickets_Cashable_In_Qty].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Tickets_Cashable_Out_Qty].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Tickets_Noncashable_In_Qty].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Tickets_Noncashable_Out_Qty].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Progressive_Win_Value].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Progressive_Win_Handpay_Value].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Mystery_Machine_Paid].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Mystery_Attendant_Paid].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Promo_Cashable_EFT_In].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Promo_Cashable_EFT_Out].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Noncashable_EFT_In].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Noncashable_EFT_Out].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Cashable_EFT_In].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Cashable_EFT_Out].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_200].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_500].CoalesceIntValue,
                                                    null,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Bill_2].CoalesceIntValue,
                                                    monMeters.Meters[MonTgt_G2H_Meter_MeterType.Games_Since_Power_Up_Meter].CoalesceIntValue,
                                                    null,
                                                    monMeters.Source))
                        return true;
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
                return false;
            }
        }
    }
}
