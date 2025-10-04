using UnityEngine;

namespace Abstraction
{
    public class InputController
    {
        public InputSystem_Actions InputActions { get; private set; }

        public InputController()
        {
            Debug.Log("Input Init");
            InputActions = new InputSystem_Actions();
            InputActions.Enable();

            //IsCurrsor(false);
        }

        public void IsCurrsor(bool status)
        {
            Cursor.visible = status;

            if (status)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

        }
    } 
}
