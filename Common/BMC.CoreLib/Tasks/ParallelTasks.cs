using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BMC.CoreLib.Diagnostics;
using System.Threading;
using BMC.CoreLib.Concurrent;
using System.Threading.Tasks;

namespace BMC.CoreLib.Tasks
{
    public struct ParallelTaskWorkChunkStartArgs
    {
        public ParallelTaskOptions Options;
        public int ThreadIndex;
        public int ChunkStart;
        public int ChunkEnd;
        public int SeqStart;
        public int SeqEnd;
    }
    public struct ParallelTaskWorkArgs
    {
        public ParallelTaskOptions Options;
        public int ThreadIndex;
        public int ChunkProgress;
        public int ChunkSeqProgress;
        public int OverallProgress;
        public int Total;
        public string ProgressText;
    }
    public struct ParallelTaskWorkChunkCompletedArgs
    {
        public ParallelTaskOptions Options;
        public int ThreadIndex;
        public int ChunkProgress;
        public int ChunkSeqProgress;
        public int OverallProgress;
    }
    public struct ParallelTaskWorkCompletedArgs
    {
        public ParallelTaskResult Result;
    }
    public class ParallelTasksStaticWorkerForInput : DisposableObject
    {
        public IExecutorService Executor { get; set; }
        public ParallelTaskWorkHandler Work { get; set; }
        public ParallelTaskWorkCompletedHandler WorkCompleted { get; set; }
        public ParallelTaskWorkChunkStartedHandler WorkChunkStarted { get; set; }
        public ParallelTaskWorkChunkCompletedHandler WorkChunkCompleted { get; set; }
        public int TasksLow { get; set; }
        public int TasksHigh { get; set; }
        public int WorkerCount { get; set; }
        public int Chunk { get; set; }
        public int SleepInMilliseconds { get; set; }
    }

    public delegate void ParallelTaskWorkChunkStartedHandler(ParallelTaskWorkChunkStartArgs e);
    public delegate void ParallelTaskWorkHandler(ParallelTaskWorkArgs e);
    public delegate void ParallelTaskWorkChunkCompletedHandler(ParallelTaskWorkChunkCompletedArgs e);
    public delegate void ParallelTaskWorkCompletedHandler(ParallelTaskWorkCompletedArgs e);

    public static class ParallelTasks
    {
        private class WorkerItem
        {
            public volatile int CurrentCount = 0;
            public IExecutorService ExecutorService { get; set; }
            public ParallelTaskResult Result { get; set; }
            public ParallelTaskWorkCompletedHandler Completed { get; set; }
            public CountdownEvent EventHandle { get; set; }
        }

        #region Static For
        public static ParallelTaskResult StaticFor(int low, int high,
            Action<ParallelTaskOptions, int, int> work)
        {
            return StaticFor(low, high, work, Environment.ProcessorCount, 0);
        }

        public static ParallelTaskResult StaticFor(int low, int high,
            Action<ParallelTaskOptions, int, int> work, int workerCount)
        {
            return StaticFor(low, high, work, workerCount, 0);
        }

        public static ParallelTaskResult StaticFor(int low, int high,
            Action<ParallelTaskOptions, int, int> work, int workerCount, int chunk)
        {
            if (low < 0 || high < low) return new ParallelTaskResult();

            ModuleProc PROC = new ModuleProc("ParallelTasks", "StaticFor");
            ParallelTaskResult result = new ParallelTaskResult(ParallelTaskResultStatus.Created);
            if (workerCount <= 0) workerCount = 1;
            if (chunk <= 0) chunk = 1;
            if (high < chunk) chunk = ((high - low) / workerCount);

            CountdownEvent cde = new CountdownEvent(workerCount);
            Thread[] threads = new Thread[workerCount];
            int currentCount = 0;
            ParallelTaskOptions options = new ParallelTaskOptions();

            try
            {
                for (int i = 0; i < workerCount; i++)
                {
                    threads[i] = Extensions.CreateThreadAndStart((o) =>
                    {
                        int k = (int)o;
                        int start = low + (k * chunk);
                        int end = ((k == (workerCount - 1)) ? high : (start + chunk));

                        for (int j = start; j < end; j++)
                        {
                            if (options.IsCancelled) break;

                            try
                            {
                                work(options, j, currentCount);
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                                result.Exceptions.Add(ex);
                            }
                            finally
                            {
                                Interlocked.Increment(ref currentCount);
                            }
                        }

                        cde.Signal();
                    }, i, "StaticFor_" + i.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                cde.Wait();
                result.Status = (options.IsCancelled ? ParallelTaskResultStatus.Canceled : ParallelTaskResultStatus.Completed);
            }

            return result;
        }

        public static void StaticWorkerFor(ParallelTasksStaticWorkerForInput input)
        {
            if (input.TasksLow < 0 || input.TasksHigh < input.TasksLow) return;

            ModuleProc PROC = new ModuleProc("ParallelTasks", "StaticFor");
            if (input.WorkerCount <= 0)
            {
                if (input.Chunk > 0) { input.WorkerCount = (int)Math.Max(Math.Ceiling(Convert.ToDouble(input.TasksHigh) / Convert.ToDouble(input.Chunk)), 1); }
                else { input.WorkerCount = 1; }
            }
            if (input.Chunk <= 0)
            {
                input.Chunk = (input.TasksHigh / input.WorkerCount);
            }
            if (input.Chunk <= 0) input.Chunk = 1;
            if (input.TasksHigh < input.Chunk) input.Chunk = ((input.TasksHigh - input.TasksLow) / input.WorkerCount);

            CountdownEvent cde = new CountdownEvent(input.WorkerCount);
            Thread[] threads = new Thread[input.WorkerCount];
            int currentCount = 0;
            ParallelTaskOptions options = new ParallelTaskOptions();
            WorkerItem item = new WorkerItem()
            {
                ExecutorService = input.Executor,
                Result = new ParallelTaskResult(ParallelTaskResultStatus.Created),
                Completed = input.WorkCompleted,
                EventHandle = cde,
            };

            try
            {
                for (int i = 0; i < input.WorkerCount; i++)
                {
                    threads[i] = Extensions.CreateThreadAndStart((o) =>
                    {
                        int k = (int)o;
                        int start = input.TasksLow + (k * input.Chunk);
                        int end = ((k == (input.WorkerCount - 1)) ? input.TasksHigh : (start + input.Chunk));

                        // work input.Chunk started
                        if (input.WorkChunkStarted != null)
                        {
                            try
                            {
                                input.WorkChunkStarted(new ParallelTaskWorkChunkStartArgs()
                                {
                                    Options = options,
                                    ThreadIndex = i,
                                    ChunkStart = start,
                                    ChunkEnd = end,
                                    SeqStart = 0,
                                    SeqEnd = (end - 1 - start),
                                });
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                                item.Result.Exceptions.Add(ex);
                            }
                        }

                        // work
                        int chunkProgress = start;
                        for (int j = start, sj = 0; j < end; j++, sj++)
                        {
                            if ((input.Executor != null && input.Executor.IsShutdown)
                                || options.IsCancelled) break;
                            chunkProgress = j;

                            try
                            {
                                int proIdx = Interlocked.Increment(ref currentCount);
                                int proPerc = (int)(((float)proIdx / (float)input.TasksHigh) * 100.0);
                                string text = string.Format("{0:D} of {1:D} ({2:D} %)", proIdx, input.TasksHigh, proPerc);
                                input.Work(new ParallelTaskWorkArgs()
                                {
                                    Options = options,
                                    ThreadIndex = k,
                                    ChunkProgress = j,
                                    ChunkSeqProgress = sj,
                                    OverallProgress = proIdx,
                                    Total = input.TasksHigh,
                                    ProgressText = text,
                                });
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                                item.Result.Exceptions.Add(ex);
                            }
                            finally
                            {
                                Interlocked.Increment(ref currentCount);
                            }

                            Thread.Sleep(input.SleepInMilliseconds);
                        }

                        // work input.Chunk completed
                        if (input.WorkChunkCompleted != null)
                        {
                            try
                            {
                                input.WorkChunkCompleted(new ParallelTaskWorkChunkCompletedArgs()
                                {
                                    Options = options,
                                    ThreadIndex = i,
                                    ChunkProgress = chunkProgress,
                                    OverallProgress = currentCount,
                                });
                            }
                            catch (Exception ex)
                            {
                                Log.Exception(PROC, ex);
                                item.Result.Exceptions.Add(ex);
                            }
                        }

                        cde.Signal();
                    }, i, "StaticFor_" + i.ToString() + "_");
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                Extensions.CreateThreadAndStart((o) =>
                {
                    WorkerItem wi = o as WorkerItem;
                    wi.EventHandle.Wait();
                    wi.Result.Status = (((input.Executor != null && input.Executor.IsShutdown)
                                        || options.IsCancelled) ? ParallelTaskResultStatus.Canceled : ParallelTaskResultStatus.Completed);
                    if (wi.Completed != null)
                    {
                        wi.Completed(new ParallelTaskWorkCompletedArgs()
                        {
                            Result = wi.Result,
                        });
                    }
                }, item, "StaticFor_Wait_");
            }
        }

        public static ParallelTaskResult StaticInvoke(params Action[] actions)
        {
            if (actions == null) return new ParallelTaskResult();
            return StaticFor(0, actions.Length,
                (o, i, j) =>
                {
                    actions[i]();
                });
        }

        public static ParallelTaskResult StaticInvoke(int workerCount, int chunk, params Action[] actions)
        {
            if (actions == null) return new ParallelTaskResult();
            return StaticFor(0, actions.Length,
                (o, i, j) =>
                {
                    actions[i]();
                }, workerCount, chunk);
        }
        #endregion

        #region Dynamic For
        public static ParallelTaskResult DynamicFor(int low, int high,
            Action<ParallelTaskOptions, int, int> work, int workerCount)
        {
            if (low < 0 || high < low) return new ParallelTaskResult();

            ModuleProc PROC = new ModuleProc("ParallelTasks", "For");
            ParallelTaskResult result = new ParallelTaskResult(ParallelTaskResultStatus.Created);
            if (workerCount <= 0) workerCount = 1;
            const int chunk = 16;

            CountdownEvent cde = new CountdownEvent(workerCount);
            Thread[] threads = new Thread[workerCount];
            int currentCount = 0;
            int currentValue = low;
            ParallelTaskOptions options = new ParallelTaskOptions();

            try
            {
                for (int i = 0; i < workerCount; i++)
                {
                    threads[i] = Extensions.CreateThreadAndStart((o) =>
                    {
                        int j = 0;
                        int currentChunk = 1;

                        while (true)
                        {
                            if (options.IsCancelled) break;
                            if ((currentValue + currentChunk) > Int32.MaxValue) break;
                            j = Interlocked.Add(ref currentValue, currentChunk) - currentChunk;
                            if (j >= high) break;

                            for (int k = 0; (k < currentChunk) && ((j + k) < high); k++)
                            {
                                if (options.IsCancelled) break;

                                try
                                {
                                    work(options, j, currentCount);
                                }
                                catch (Exception ex)
                                {
                                    Log.Exception(PROC, ex);
                                }
                                finally
                                {
                                    Interlocked.Increment(ref currentCount);
                                }
                            }
                            if (currentChunk < chunk) currentChunk *= 2;
                        }

                        cde.Signal();
                    }, i);
                }
            }
            catch (Exception ex)
            {
                Log.Exception(PROC, ex);
            }
            finally
            {
                cde.Wait();
                result.Status = (options.IsCancelled ? ParallelTaskResultStatus.Canceled : ParallelTaskResultStatus.Completed);
            }

            return result;
        }
        #endregion
    }
}
