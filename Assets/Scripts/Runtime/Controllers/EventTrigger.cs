using UnityEngine;

namespace Abstraction
{
    public abstract class EventTrigger : MonoBehaviour, IEventTrigger
    {
        
        public virtual void Enter()
        {

        }

        public virtual void Exit()
        {
            
        }

        public void OnTriggerEnter(Collider other)
        {
            Enter();
        }

        public void OnTriggerExit(Collider other)
        {
            Enter();
        }

    }
}
