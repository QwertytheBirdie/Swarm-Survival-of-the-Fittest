using UnityEngine;

public class AxisDebugger : MonoBehaviour
{
    void Update()
    {
        for (int axis = 1; axis <= 28; axis++)
        {
            // Unity joystick axes have names like: "P1_Aim_H", not "Axis n"
            // Do NOT guess axis names. Only check axes you actually defined.
        }
    }
}
