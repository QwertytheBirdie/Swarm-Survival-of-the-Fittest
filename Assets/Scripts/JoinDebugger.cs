using UnityEngine;
using UnityEngine.InputSystem;

public class JoinDebugger : MonoBehaviour
{
    void OnPlayerJoined(PlayerInput player)
    {
        Debug.Log("Player joined: " + player.playerIndex +
                  " Device: " + player.devices[0].displayName);
    }
}
