using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers.Security;

namespace BMC.ExComms.Server.ExecutionSteps
{
    internal abstract class ExecStep_KeyExchange_GmuInitiated
        : ExecutionStep { }

    [ExecutionStep(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_KeyExchange_PartialKey_H2G_GMU),
                    },
                    PostTypeG2H = ExecutionStepPostTypes.ProcessInCurrentChannel,
                    AllowedMessageDirectionAll = FF_FlowDirection.G2H,
                    AllowedMessages = new Type[] 
                    { 
                        typeof(FFTgt_B2B_Security_KeyExchange_Request) 
                    },
                    AllowedReplyMessages = new Type[] 
                    { 
                        typeof(FFTgt_B2B_Security_KeyExchange_PartialKey) 
                    })]
    internal class ExecStep_KeyExchange_Start_G2H_GMU
        : ExecStep_KeyExchange_GmuInitiated
    {
        protected override void PrepareMessageToProcessInCurrentChannel(IFreeformEntity_Msg request, ref IFreeformEntity_Msg response)
        {
            response = this.Factory.SecurityTables.InitKeyExchangePartialKeyH2G_GMU(request);
        }
    }

    [ExecutionStep(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_KeyExchange_End_G2H_GMU),
                    },
                    PostTypeH2G = ExecutionStepPostTypes.ProcessInExternalChannel,
                    AllowedMessageDirectionAll = FF_FlowDirection.H2G,
                    AllowedMessages = new Type[] 
                    { 
                        typeof(FFTgt_B2B_Security_KeyExchange_PartialKey) 
                    })]
    internal class ExecStep_KeyExchange_PartialKey_H2G_GMU
        : ExecStep_KeyExchange_GmuInitiated
    {
    }

    [ExecutionStep(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_KeyExchange_Status_H2G_GMU),
                    },
                    PostTypeG2H = ExecutionStepPostTypes.ProcessInCurrentChannel,
                    AllowedMessageDirectionAll = FF_FlowDirection.G2H,
                    AllowedMessages = new Type[] 
                    { 
                        typeof(FFTgt_B2B_Security_KeyExchange_End) 
                    },
                    AllowedReplyMessages = new Type[] 
                    { 
                        typeof(FFTgt_B2B_Security_KeyExchange_Status) 
                    })]
    internal class ExecStep_KeyExchange_End_G2H_GMU
        : ExecStep_KeyExchange_GmuInitiated
    {
        protected override void PrepareMessageToProcessInCurrentChannel(IFreeformEntity_Msg request, ref IFreeformEntity_Msg response)
        {
            response = this.Factory.SecurityTables.InitKeyExchangeStatusH2G_GMU(request);
        }
    }

    [ExecutionStep(NextSteps = new Type[] 
                    { 
                        typeof(ExecStep_NoStep),
                    },
                    PostTypeH2G = ExecutionStepPostTypes.ProcessInExternalChannel,
                    AllowedMessageDirectionAll = FF_FlowDirection.H2G,
                    AllowedMessages = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Status) })]
    internal class ExecStep_KeyExchange_Status_H2G_GMU
        : ExecStep_KeyExchange_GmuInitiated
    {
        public override bool CanExecute(ExecutionStepKeyValue pair, IFreeformEntity_Msg request)
        {
            return base.CanExecute(pair, request);
        }
    }

    [ExecutionStepSimuator(NextSteps = new Type[] 
                            { 
                                typeof(ExecStep_KeyExchange_PartialKey_G2H_SIM),
                            },
                            PostTypeG2H = ExecutionStepPostTypes.PrepareAndProcessInExternalChannel,
                            AllowedMessageDirectionAll = FF_FlowDirection.G2H,
                            AllowedMessages = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Request) })]
    internal class ExecStep_KeyExchange_Start_G2H_SIM
        : ExecStep_KeyExchange_GmuInitiated
    {
        public override bool CanExecute(ExecutionStepKeyValue pair, IFreeformEntity_Msg request)
        {
            return base.CanExecute(pair, request);
        }
    }

    [ExecutionStepSimuator(NextSteps = new Type[] 
                            { 
                                typeof(ExecStep_KeyExchange_End_G2H_SIM),
                            },
                            PostTypeH2G = ExecutionStepPostTypes.ProcessInCurrentChannel,
                            AllowedMessageDirectionAll = FF_FlowDirection.H2G,
                            AllowedMessages = new Type[] 
                            { 
                                typeof(FFTgt_B2B_Security_KeyExchange_PartialKey) 
                            },
                            AllowedReplyMessages = new Type[] 
                            { 
                                typeof(FFTgt_B2B_Security_KeyExchange_End) 
                            })]
    internal class ExecStep_KeyExchange_PartialKey_G2H_SIM
        : ExecStep_KeyExchange_GmuInitiated
    {
        protected override void PrepareMessageToProcessInCurrentChannel(IFreeformEntity_Msg request, ref IFreeformEntity_Msg response)
        {
            response = this.Factory.SecurityTables.InitKeyExchangeEndG2H_SIM(request);
        }
    }

    [ExecutionStepSimuator(NextSteps = new Type[] 
                            { 
                                typeof(ExecStep_KeyExchange_Status_G2H_SIM),
                            },
                            PostTypeH2G = ExecutionStepPostTypes.ProcessInExternalChannel,
                            AllowedMessageDirectionAll = FF_FlowDirection.G2H,
                            AllowedMessages = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_End) })]
    internal class ExecStep_KeyExchange_End_G2H_SIM
        : ExecStep_KeyExchange_GmuInitiated
    {
    }

    [ExecutionStepSimuator(NextSteps = new Type[] 
                            { 
                                typeof(ExecStep_NoStep),
                            },
                            PostTypeH2G = ExecutionStepPostTypes.ProcessInCurrentChannel,
                            AllowedMessageDirectionAll = FF_FlowDirection.H2G,
                            AllowedMessages = new Type[] { typeof(FFTgt_B2B_Security_KeyExchange_Status) })]
    internal class ExecStep_KeyExchange_Status_G2H_SIM
        : ExecStep_KeyExchange_GmuInitiated
    {
        public override bool CanExecute(ExecutionStepKeyValue pair, IFreeformEntity_Msg request)
        {
            return base.CanExecute(pair, request);
        }
    }
}
