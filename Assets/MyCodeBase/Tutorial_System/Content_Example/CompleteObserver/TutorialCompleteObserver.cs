using System;
using Game.Tutorial;

namespace _Tutorial.Content
{
    public sealed class TutorialCompleteObserver : IDisposable
    {
        private readonly TutorialManager _tutorialManager;
        // private readonly UpgradeTriggerPoint _upgradeTriggerPoint;

        // public TutorialCompleteObserver(TutorialManager tutorialManager, UpgradeTriggerPoint upgradeTriggerPoint)
        public TutorialCompleteObserver(TutorialManager tutorialManager)
        {
            _tutorialManager = tutorialManager;


            _tutorialManager.OnCompleted += OnTutorialCompleted;

            if (_tutorialManager.IsCompleted)
            {
                OnTutorialCompleted();
            }
        }

        private void OnTutorialCompleted()
        {
            //    _upgradeTriggerPoint.gameObject.SetActive(true);
        }

        public void Dispose()
        {
            _tutorialManager.OnCompleted -= OnTutorialCompleted;
        }
    }
}
