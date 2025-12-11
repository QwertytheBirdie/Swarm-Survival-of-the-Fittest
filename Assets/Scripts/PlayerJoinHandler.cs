using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinHandler : MonoBehaviour
{
    public GameObject player1Prefab;
    public GameObject player2Prefab;

    private PlayerInputManager pim;

    void Awake()
    {
        pim = GetComponent<PlayerInputManager>();
    }

    void OnEnable()
    {
        pim.onPlayerJoined += OnPlayerJoined;
    }

    void OnDisable()
    {
        pim.onPlayerJoined -= OnPlayerJoined;
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerInput.playerIndex == 0)
        {
            ReplacePlayerObject(playerInput, player1Prefab, new Vector3(-2, 0, 0));
        }
        else if (playerInput.playerIndex == 1)
        {
            ReplacePlayerObject(playerInput, player2Prefab, new Vector3(2, 0, 0));
        }
        else
        {
            Debug.Log("Ignoring extra player join attempt.");
        }
    }

    void ReplacePlayerObject(PlayerInput playerInput, GameObject prefab, Vector3 startPos)
    {
        var devices = playerInput.devices.ToArray();

        GameObject oldObj = playerInput.gameObject;

        GameObject newObj = Instantiate(prefab, startPos, Quaternion.identity);

        PlayerInput newInput = newObj.GetComponent<PlayerInput>();

        foreach (var d in devices)
        {
            newInput.SwitchCurrentControlScheme(d);
        }

        Destroy(oldObj);
    }
}
