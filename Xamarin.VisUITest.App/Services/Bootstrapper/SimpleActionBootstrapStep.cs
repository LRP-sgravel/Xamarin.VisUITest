using System;

namespace Xamarin.VisUITest.App.Services
{
    class SimpleActionBootstrapStep : IBootstrapStep
    {
        public event Action<IBootstrapStep, float> Progressed;
        public event Action<IBootstrapStep> Completed;

        public string StepActionText { get; private set; }
        public float Progress { get; private set; }

        private Action Task { get; set; }

        public SimpleActionBootstrapStep(Action task)
        {
            Task = task;
        }

        public void Start()
        {
            Task?.Invoke();

            Completed?.Invoke(this);
        }
    }
}