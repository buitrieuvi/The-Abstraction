using UnityEngine;
using Zenject;

namespace Abstraction
{
    public abstract class EventTrigger : MonoBehaviour, IEventTrigger
    {
        protected bool isActive;

        public virtual void Enter()
        {
            
        }

        public virtual void Exit()
        {
            
        }

    }
}
