using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib;
using BMC.CoreLib.Collections;
using BMC.ExComms.Contracts.DTO.Freeform;
using BMC.ExComms.Server.Handlers;

namespace BMC.ExComms.Server.ExecutionSteps
{
    internal static class ExecutionStepMappings
    {
        private const string DYN_MODULE_NAME = "ExecutionStepMappings";
        private static IExecutionStep _start = null;
        private static IExecutionStep _end = null;
        private static IDictionary<string, IExecutionStep> _es = new StringDictionary<IExecutionStep>();
        private static _MappingInfo[] _mappings = null;

        private class _MappingInfo
             : DisposableObject
        {
            public Type Type { get; set; }
            public ExecutionStepAttribute[] Attributes { get; set; }

            public override string ToString()
            {
                return this.Type.Name;
            }
        }

        static ExecutionStepMappings()
        {
            Initialize();
        }

        private static void Initialize()
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "Initialize"))
            {
                try
                {
                    _mappings = (from t in typeof(ExecutionStepMappings).Assembly.GetTypes()
                                 let j = t.GetCustomAttributes(typeof(ExecutionStepAttribute), true).OfType<ExecutionStepAttribute>().ToArray()
                                 where j.Length > 0
                                 select new _MappingInfo()
                                 {
                                     Type = t,
                                     Attributes = j
                                 }).ToArray();
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        internal static void CreateExecutionSteps(this _ExecutionStepFactory factory)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "CreateExecutionSteps"))
            {
                try
                {
                    ExecutionStepDictionary execSteps = factory.ExecutionSteps;
                    ExecutionStepDeviceTypes deviceType = ExecutionStepFactory.Current.DeviceType;
                    IDictionary<string, ExecutionStepCollection> messageWiseSteps = factory.MessageWiseSteps;
                    IDictionary<int, ExecutionStepCollection> groupedSteps = factory.GroupedSteps;

                    var mappings = (from m in _mappings
                                    from j in m.Attributes
                                    where j.DeviceType == deviceType
                                    select m).ToArray();

                    foreach (var mapping in mappings)//.AsParallel())
                    {
                        Type classType = mapping.Type;
                        IExecutionStep step = factory.AddOrUpdate(classType, classType.Name);

                        foreach (var mappingAttribute in mapping.Attributes
                                                        .Where(d => d.DeviceType == deviceType))
                        {
                            if (mappingAttribute.IsStart && (execSteps.Start == null)) execSteps.Start = step;
                            if (mappingAttribute.IsEnd && (execSteps.End == null)) execSteps.End = step;
                            step.PostTypeG2H = mappingAttribute.PostTypeG2H;
                            step.PostTypeH2G = mappingAttribute.PostTypeH2G;
                            step.MoveToNextStep = mappingAttribute.MoveToNextStep;

                            //if (mappingAttribute.Dependencies != null)
                            //{
                            //    foreach (var dependency in mappingAttribute.Dependencies)
                            //    {
                            //        IExecutionStep parentStep = execSteps.AddOrUpdate(dependency, dependency.Name);
                            //        if (!parentStep.NextSteps.Contains(step))
                            //            parentStep.NextSteps.Add(step);
                            //    }
                            //}

                            //if (mappingAttribute.NextDependency != null)
                            //{
                            //    var dependency = mappingAttribute.NextDependency;
                            //    IExecutionStep nextStep = execSteps.AddOrUpdate(dependency, dependency.Name);
                            //    if (!step.NextSteps.Contains(nextStep))
                            //        step.NextSteps.Add(nextStep);
                            //    step.NextStep = nextStep;
                            //    step.SetNextStep = mappingAttribute.SetNextStep;
                            //    nextStep.PrevStep = step;
                            //}
                            if (mappingAttribute.NextSteps != null)
                            {
                                foreach (var dependency in mappingAttribute.NextSteps)
                                {
                                    IExecutionStep nextStep = factory.AddOrUpdate(dependency, dependency.Name);
                                    //nextStep.Level = step.Level + 1;
                                    bool resetNextStep = false;

                                    if (!step.NextSteps.Contains(nextStep))
                                    {
                                        step.NextSteps.Add(nextStep);
                                        resetNextStep = (step.NextSteps.Count > 1);
                                    }
                                    step.NextStep = (resetNextStep ? null : nextStep);

                                    if (!nextStep.PrevSteps.Contains(step))
                                    {
                                        nextStep.PrevSteps.Add(step);
                                    }
                                    //step.SetNextStep = mappingAttribute.SetNextStep;
                                    //nextStep.PrevStep = step;
                                }
                            }

                            if (messageWiseSteps != null &&
                                mappingAttribute.AllowedMessages != null)
                            {
                                var direction = mappingAttribute.AllowedMessageDirectionAll;
                                int length = mappingAttribute.AllowedMessages.Length;

                                for (int i = 0; i < mappingAttribute.AllowedMessages.Length; i++)
                                {
                                    if (mappingAttribute.AllowedMessageDirections != null)
                                    {
                                        direction = mappingAttribute.AllowedMessageDirections[i];
                                    }

                                    ExecutionStepKeyValue pair = new ExecutionStepKeyValue(mappingAttribute.AllowedMessages[i].Name, direction);
                                    string pairKey = pair.FullKey;
                                    if (!step.AllowedMessages.ContainsKey(pair))
                                    {
                                        step.AllowedMessages.Add(pair, 1);
                                    }

                                    ExecutionStepCollection execSteps2 = null;
                                    if (messageWiseSteps.ContainsKey(pairKey))
                                    {
                                        execSteps2 = messageWiseSteps[pairKey];
                                    }
                                    else
                                    {
                                        messageWiseSteps.Add(pairKey, (execSteps2 = new ExecutionStepCollection()));
                                    }
                                    execSteps2.Add(step);
                                }

                                if (mappingAttribute.AllowedReplyMessages != null &&
                                    mappingAttribute.AllowedReplyMessages.Length == length)
                                {
                                    for (int j = 0; j < length; j++)
                                    {
                                        Type type1 = mappingAttribute.AllowedMessages[j];
                                        Type type2 = mappingAttribute.AllowedReplyMessages[j];
                                        if (type1 != null && type2 != null)
                                        {
                                            factory.RequestResponseMappings.Add(type1.Name, type2.Name, new RequestResponseMapItem());
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // execution step grouping
                    if (messageWiseSteps != null &&
                        groupedSteps != null)
                    {
                        execSteps.ArrangeExecutionSteps(messageWiseSteps, groupedSteps);
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private static void ArrangeExecutionSteps(this ExecutionStepDictionary execSteps,
                                                    IDictionary<string, ExecutionStepCollection> messageWiseSteps,
                                                    IDictionary<int, ExecutionStepCollection> groupedSteps)
        {
            using (ILogMethod method = Log.LogMethod(DYN_MODULE_NAME, "ArrangeExecutionSteps"))
            {
                try
                {
                    Stack<IExecutionStep> st = new Stack<IExecutionStep>();
                    st.Push(execSteps.Start);
                    IDictionary<string, IExecutionStep> steps = new StringDictionary<IExecutionStep>();

                    while (st.Count != 0)
                    {
                        IExecutionStep step = st.Pop();
                        int level = Math.Max(0, (from p in step.PrevSteps
                                                 select p.Level).DefaultIfEmpty().Max()) + 1;
                        if (!steps.ContainsKey(step.UniqueKey))
                        {
                            step.Level = level;
                            steps.Add(step.UniqueKey, step);
                        }

                        foreach (var step2 in step.NextSteps)
                        {
                            if (!steps.ContainsKey(step2.UniqueKey))
                            {
                                st.Push(step2);
                            }
                        }
                    }

                    // last step
                    execSteps.End.Level = Math.Max(0, (from p in steps.Values
                                                       select p.Level).DefaultIfEmpty().Max()) + 1;

                    // Ordered projects
                    groupedSteps.Clear();
                    var orderedSteps = (from p in execSteps.Values
                                        orderby p.Level
                                        group p by p.Level);
                    foreach (var orderedStep in orderedSteps)
                    {
                        ExecutionStepCollection stepsList = new ExecutionStepCollection();
                        groupedSteps.Add(orderedStep.Key, stepsList);

                        foreach (var step in orderedStep)
                        {
                            stepsList.Add(step);
                        }
                    }
                }
                catch (Exception ex)
                {
                    method.Exception(ex);
                }
            }
        }

        private static IExecutionStep AddOrUpdate(this _ExecutionStepFactory factory, Type dependency, string dependencyName)
        {
            IExecutionStep result = null;
            if (!factory.ExecutionSteps.ContainsKey(dependencyName))
            {
                factory.ExecutionSteps.Add(dependencyName, (result = Activator.CreateInstance(dependency) as IExecutionStep));
                result.Factory = factory;
                result.Level = 1;
            }
            else
            {
                result = factory.ExecutionSteps[dependencyName];
            }
            return result;
        }

        public static void Test() { }
    }
}
