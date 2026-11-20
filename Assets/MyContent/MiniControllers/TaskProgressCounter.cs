using UnityEngine;
using UnityEngine.Events;

namespace MiniControllers
{
    public class TaskProgressCounter : MonoBehaviour
    {
        [SerializeField, Min(1)] private int requiredCompletions = 2;
        [SerializeField] private UnityEvent completed;

        private int _completionCount;
        private bool _completed;

        public void RegisterCompletion()
        {
            if (_completed)
                return;

            _completionCount++;

            if (_completionCount < requiredCompletions)
                return;

            _completed = true;
            completed.Invoke();
        }
    }
}