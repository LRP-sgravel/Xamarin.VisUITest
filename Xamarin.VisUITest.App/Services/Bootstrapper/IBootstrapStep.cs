using System;

namespace Xamarin.VisUITest.App.Services
{
    interface IBootstrapStep
    {
        event Action<IBootstrapStep, float> Progressed;
        event Action<IBootstrapStep> Completed;

        string StepActionText { get; }
        float Progress { get; }

        void Start();
    }
}
