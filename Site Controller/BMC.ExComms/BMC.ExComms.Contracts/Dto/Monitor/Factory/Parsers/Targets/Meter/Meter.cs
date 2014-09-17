using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMC.CoreLib;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Contracts.DTO.Monitor
{
    [MonTgtParserMappingG2H(FF_FlowDirection.G2H,
                    typeof(FFTgt_G2H_AccountingMeters),
                    -1,
                    -1)]
    internal class MonTgtParser_MeterBlock_G2H
        : MonTgtParser_G2H
    {
        private static readonly IDictionary<FF_AppId_AccountingMeterIds, MonTgt_G2H_Meter_MeterType> _meterMappings = null;

        static MonTgtParser_MeterBlock_G2H()
        {
            _meterMappings = new ConcurrentDictionary<FF_AppId_AccountingMeterIds, MonTgt_G2H_Meter_MeterType>();

            _meterMappings.Add(FF_AppId_AccountingMeterIds.GeneralMeters_Plays, MonTgt_G2H_Meter_MeterType.Games_Bet);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.GeneralMeters_Bets, MonTgt_G2H_Meter_MeterType.Coins_In);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.GeneralMeters_Wins, MonTgt_G2H_Meter_MeterType.Coins_Out);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.GeneralMeters_CoinDrop, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_CoinsPurchased, MonTgt_G2H_Meter_MeterType.Coins_Out);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_CoinsCollected, MonTgt_G2H_Meter_MeterType.Coins_In);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_1, MonTgt_G2H_Meter_MeterType.Bill_1);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_2, MonTgt_G2H_Meter_MeterType.Bill_2);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_5, MonTgt_G2H_Meter_MeterType.Bill_5);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_10, MonTgt_G2H_Meter_MeterType.Bill_10);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_20, MonTgt_G2H_Meter_MeterType.Bill_20);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_25, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_50, MonTgt_G2H_Meter_MeterType.Bill_50);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_100, MonTgt_G2H_Meter_MeterType.Bill_100);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_200, MonTgt_G2H_Meter_MeterType.Bill_200);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_250, MonTgt_G2H_Meter_MeterType.Bill_250);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_500, MonTgt_G2H_Meter_MeterType.Bill_500);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_1000, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_2000, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_2500, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_5000, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_10000, MonTgt_G2H_Meter_MeterType.Bill_10000);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_20000, MonTgt_G2H_Meter_MeterType.Bill_20000);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_25000, MonTgt_G2H_Meter_MeterType.Bill_25000);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_50000, MonTgt_G2H_Meter_MeterType.Bill_50000);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_100000, MonTgt_G2H_Meter_MeterType.Bill_100000);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_200000, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_250000, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_500000, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_1000000, MonTgt_G2H_Meter_MeterType.Unknown);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_CouponsCount, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_BillCoinOut, MonTgt_G2H_Meter_MeterType.Dollar_Value_Of_Bills_meter);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.EFTIn_Cashable, MonTgt_G2H_Meter_MeterType.Cashable_EFT_In);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.EFTOut_Cashable, MonTgt_G2H_Meter_MeterType.Cashable_EFT_Out);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.EFTIn_NonCashable, MonTgt_G2H_Meter_MeterType.Noncashable_EFT_In);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.EFTOut_NonCashable, MonTgt_G2H_Meter_MeterType.Noncashable_EFT_Out);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.EFTIn_Promo_Cashable, MonTgt_G2H_Meter_MeterType.Promo_Cashable_EFT_In);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.EFTOut_Promo_Cashable, MonTgt_G2H_Meter_MeterType.Promo_Cashable_EFT_Out);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketIn_Cashable, MonTgt_G2H_Meter_MeterType.Tickets_Cashable_In_Value);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketOut_Cashable, MonTgt_G2H_Meter_MeterType.Tickets_Cashable_Out_Value);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketIn_NonCashable, MonTgt_G2H_Meter_MeterType.Tickets_Noncashable_In_Value);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketOut_NonCashable, MonTgt_G2H_Meter_MeterType.Tickets_Noncashable_Out_Value);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketIn_Qty_Cashable, MonTgt_G2H_Meter_MeterType.Tickets_Cashable_In_Qty);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketOut_Qty_Cashable, MonTgt_G2H_Meter_MeterType.Tickets_Cashable_In_Qty);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketIn_Qty_NonCashable, MonTgt_G2H_Meter_MeterType.Tickets_Noncashable_In_Qty);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketOut_Qty_NonCashable, MonTgt_G2H_Meter_MeterType.Tickets_Noncashable_Out_Qty);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketIn_Promo_Cashable, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.TicketIn_Qty_Promo_Cashable, MonTgt_G2H_Meter_MeterType.Unknown);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.SASJPMeters_TotJPHandpayCredits, MonTgt_G2H_Meter_MeterType.Jackpot);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.SASJPMeters_TotCancelledCredits, MonTgt_G2H_Meter_MeterType.Handpay_Cancelled_Credits);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.SASJPMeters_TotPRHandpayCredits, MonTgt_G2H_Meter_MeterType.Progressive_Win_Handpay_Value);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.SASJPMeters_TotPRMachpayCredits, MonTgt_G2H_Meter_MeterType.Progressive_Win_Value);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.SysGame_Cashable_EFTIn, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.SysGame_NonCashable_EFTIn, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.SysGame_Hand_pay_Jackpot, MonTgt_G2H_Meter_MeterType.Unknown);

            _meterMappings.Add(FF_AppId_AccountingMeterIds.Cashless_Used, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.Cashless_State, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.Cashless_Transactions, MonTgt_G2H_Meter_MeterType.Cancelled_Credits);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.Cashless_Configuration, MonTgt_G2H_Meter_MeterType.Coins_Drop);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.Unrestricted_Win, MonTgt_G2H_Meter_MeterType.Games_Won);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.BonusMeters_MachinePaid, MonTgt_G2H_Meter_MeterType.Mystery_Machine_Paid);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.BonusMeters_AttendantPaid, MonTgt_G2H_Meter_MeterType.Mystery_Attendant_Paid);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.SASJPMeters_CumulativeProgWin, MonTgt_G2H_Meter_MeterType.SASJPMeters_CumulativeProgWin);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_200_1, MonTgt_G2H_Meter_MeterType.Bill_200);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_500_1, MonTgt_G2H_Meter_MeterType.Bill_500);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_CoinAcceptorCredits, MonTgt_G2H_Meter_MeterType.True_Coin_In);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_HopperPaidDebits, MonTgt_G2H_Meter_MeterType.True_Coin_Out);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.AcceptorMeters_Bill_2_1, MonTgt_G2H_Meter_MeterType.Bill_2);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.EFT_Cashable_to_Ticket, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.EFT_Restricted_Promo_to_Ticket, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.Restricted_Bet_Amount, MonTgt_G2H_Meter_MeterType.Unknown);
            _meterMappings.Add(FF_AppId_AccountingMeterIds.Unrestricted_Bet_Amount, MonTgt_G2H_Meter_MeterType.Unknown);
        }

        protected override IMonitorEntity_MsgTgt CreateMonitorTarget(IMonitorEntity parent, IFreeformEntity_MsgTgt request)
        {
            MonTgt_G2H_Meters meters = parent as MonTgt_G2H_Meters;
            FFTgt_G2H_AccountingMeters tgtSrc = request as FFTgt_G2H_AccountingMeters;
            if (meters != null &&
                tgtSrc != null)
            {
                Parallel.ForEach<FFTgt_G2H_AccountingMeter>(tgtSrc.Meters,
                    (meter) =>
                    {
                        MonTgt_G2H_Meter_MeterType meterId = MonTgt_G2H_Meter_MeterType.Unknown;
                        long meterValue = 0;

                        if (_meterMappings.ContainsKey(meter.MeterId))
                        {
                            meterId = _meterMappings[meter.MeterId];
                            meterValue = (long)meter.Value;
                        }

                        if (meterId != MonTgt_G2H_Meter_MeterType.Unknown)
                        {
                            meters.Meters.Add(meterId, 
                                new MonTgt_G2H_Meter()
                                {
                                    Type = meterId,
                                    Value = meterValue,
                                });
                        }
                    });
            }
            return null;
        }
    }
}
