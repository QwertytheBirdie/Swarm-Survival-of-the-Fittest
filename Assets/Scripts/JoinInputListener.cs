using UnityEngine;
using UnityEngine.InputSystem;

public class JoinInputListener : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame ||
            Gamepad.current?.startButton.wasPressedThisFrame == true)
        {
            CoopGameManager.Instance.TryJoinPlayer();
        }
    }
}
