using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.ExComms.Contracts.DTO.Freeform;

namespace BMC.ExComms.Server.ExecutionSteps
{
    public enum ExecutionStepDeviceTypes
    {
        GMU = 0,
        Simulator = 1,
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ExecutionStepAttribute : Attribute
    {
        public ExecutionStepAttribute()
        {
            this.DeviceType = ExecutionStepDeviceTypes.GMU;
            //this.Dependencies = dependencies;
        }

        //public ExecutionStepAttribute(params Type[] dependencies)
        //{
        //    this.DeviceType = ExecutionStepDeviceTypes.GMU;
        //    this.Dependencies = dependencies;
        //}

        public ExecutionStepDeviceTypes DeviceType { get; set; }

        public Type[] Dependencies { get; set; }

        public bool IsStart { get; set; }

        public bool IsEnd { get; set; }

        public Type NextDependency { get; set; }

        public Type[] PrevSteps { get; set; }

        public Type[] NextSteps { get; set; }

        public bool SetNextStep { get; set; }

        public bool MoveToNextStep { get; set; }

        public ExecutionStepPostTypes PostTypeG2H { get; set; }

        public ExecutionStepPostTypes PostTypeH2G { get; set; }

        public bool IsSimulator { get; set; }

        //public Type RootMessage { get; set; }

        //public FF_FlowDirection MessageDirection { get; set; }

        public FF_FlowDirection AllowedMessageDirectionAll { get; set; }

        public FF_FlowDirection[] AllowedMessageDirections { get; set; }

        public Type[] AllowedMessages { get; set; }

        public Type[] AllowedReplyMessages { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ExecutionStepSimuatorAttribute : ExecutionStepAttribute
    {
        public ExecutionStepSimuatorAttribute()
            : base()
        {
            this.DeviceType = ExecutionStepDeviceTypes.Simulator;
        }

        //public ExecutionStepSimuatorAttribute(params Type[] dependencies)
        //    : base(dependencies)
        //{
        //    this.DeviceType = ExecutionStepDeviceTypes.Simulator;
        //}
    }
}
