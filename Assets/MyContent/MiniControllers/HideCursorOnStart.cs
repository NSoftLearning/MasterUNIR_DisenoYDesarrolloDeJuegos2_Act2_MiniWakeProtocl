using UnityEngine;

namespace MiniControllers
{
    public class HideCursorOnStart : MonoBehaviour
    {
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}