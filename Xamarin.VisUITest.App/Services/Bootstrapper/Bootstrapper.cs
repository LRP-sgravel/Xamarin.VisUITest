using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.VisUITest.App.Utilities;

namespace Xamarin.VisUITest.App.Services
{
    class Bootstrapper
    {
        public event Action<string> BootTextChanged;
        public event Action<float> BootProgressed;
        public event Action BootCompleted;

        private Queue<IBootstrapStep> StartupSteps { get; set; }
        private List<IBootstrapStep> AsyncSteps { get; set; }
        private List<IBootstrapStep> CompletedSteps { get; set; }
        private int SyncStepsCount { get; set; }
        private int AsyncStepsCount { get; set; }
        private bool BootSequenceStarted { get; set; }

        private float mCurrentProgress;
        private float CurrentProgress
        {
            get { return mCurrentProgress; }
            set
            {
                if (value != CurrentProgress)
                {
                    mCurrentProgress = value;

                    if (BootProgressed != null)
                    {
                        MainThread.Context.Post((sender) => { BootProgressed(CurrentProgress); }, null);
                    }
                }
            }
        }

        public Bootstrapper()
        {
            BootSequenceStarted = false;
            SyncStepsCount = 0;
            AsyncStepsCount = 0;
            StartupSteps = new Queue<IBootstrapStep>();
            AsyncSteps = new List<IBootstrapStep>();
            CompletedSteps = new List<IBootstrapStep>();
        }

        public void QueueStep(IBootstrapStep newStep)
        {
            if (!BootSequenceStarted && newStep != null)
            {
                StartupSteps.Enqueue(newStep);
            }
        }

        public void QueueStep(Action newStep)
        {
            QueueStep(new SimpleActionBootstrapStep(newStep));
        }

        public void AddAsyncStep(IBootstrapStep newStep)
        {
            if (!BootSequenceStarted && newStep != null)
            {
                AsyncSteps.Add(newStep);
            }
        }

        public void Boot()
        {
            BootSequenceStarted = true;
            SyncStepsCount = StartupSteps.Count;
            AsyncStepsCount = AsyncSteps.Count;

            foreach (IBootstrapStep asyncStep in AsyncSteps)
            {
                StartStep(asyncStep);
            }

            if (SyncStepsCount > 0)
            {
                StartNextSyncStep();
            }
            else if (AsyncSteps.Count == 0 && BootCompleted != null)
            {
                MainThread.Context.Post(sender => { BootCompleted(); }, null);
            }
        }

        private void StartNextSyncStep()
        {
            IBootstrapStep nextStep = null;

            lock (StartupSteps)
            {
                if (StartupSteps.Count > 0)
                {
                    nextStep = StartupSteps.Dequeue();
                }
            }

            if (nextStep != null)
            {
                StartStep(nextStep);
            }
        }

        private void StartStep(IBootstrapStep step)
        {
            step.Progressed += OnCurrentStepProgressed;
            step.Completed += OnCurrentStepCompleted;

            if (BootTextChanged != null)
            {
                BootTextChanged(step.StepActionText);
            }

            Task.Factory.StartNew(step.Start);
        }

        private void OnCurrentStepProgressed(IBootstrapStep step, float progress)
        {
            UpdateProgressValue(step);
        }

        private void OnCurrentStepCompleted(IBootstrapStep completedStep)
        {
            bool asyncCompleted = false;

            lock (CompletedSteps)
            {
                CompletedSteps.Add(completedStep);
            }
            lock (AsyncSteps)
            {
                if (AsyncSteps.Contains(completedStep))
                {
                    AsyncSteps.Remove(completedStep);
                    asyncCompleted = true;
                }
            }
            UpdateProgressValue(completedStep);

            if (CompletedSteps.Count == (SyncStepsCount + AsyncStepsCount) && BootCompleted != null)
            {
                MainThread.Context.Post(sender => { BootCompleted(); }, null);
            }
            else if (!asyncCompleted && StartupSteps.Count > 0)
            {
                StartNextSyncStep();
            }
        }

        private void UpdateProgressValue(IBootstrapStep step = null)
        {
            lock (CompletedSteps)
            {
                float syncProgress = 0;
                float currentSyncProgress = 0;

                if (SyncStepsCount > 0)
                {
                    syncProgress = (float)CompletedSteps.Count / SyncStepsCount;

                    if (!AsyncSteps.Contains(step))
                    {
                        currentSyncProgress = step.Progress * (1.0f / SyncStepsCount);
                    }
                }

                CurrentProgress = (float)MathUtils.Clamp(syncProgress + currentSyncProgress + AsyncSteps.Sum(s => s.Progress), 0, 1);
            }
        }
    }
}
