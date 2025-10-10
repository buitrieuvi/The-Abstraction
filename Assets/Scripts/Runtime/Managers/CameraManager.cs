using Unity.Cinemachine;
using UnityEngine;

namespace Abstraction
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] CinemachineInputAxisController _cam3;
        [SerializeField] CinemachineInputAxisController _cam1;

        public void SetLook(bool b) 
        {
            _cam3.enabled = b;
            _cam1.enabled = b;
        }

        public void SetOrbitX(float x)
        {
            foreach (var cam in _cam3.Controllers)
            {
                if (cam.Name == "Look Orbit X")
                {
                    cam.Input.Gain = x;
                    break;
                }
            }
        }

        public void SetOrbitY(float y)
        {
            foreach (var cam in _cam3.Controllers)
            {
                if (cam.Name == "Look Orbit Y")
                {
                    cam.Input.Gain = -y;

                    break;
                }
            }
        }

        public void SetPan(float x)
        {
            foreach (var cam in _cam1.Controllers)
            {
                if (cam.Name == "Look X (Pan)")
                {
                    cam.Input.Gain = x;
                    break;
                }
            }
        }

        public void SetTilt(float y)
        {
            foreach (var cam in _cam1.Controllers)
            {
                if (cam.Name == "Look Y (Tilt)")
                {
                    cam.Input.Gain = -y;

                    break;
                }
            }
        }
    }
}