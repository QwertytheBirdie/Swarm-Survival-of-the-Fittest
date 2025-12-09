using UnityEngine;

public class ControllerDebugger : MonoBehaviour
{
    void Update()
    {
        for (int i = 1; i <= 20; i++)
        {
            float v = 0f;
            string axisName = "Axis " + i;

            try
            {
                v = Input.GetAxis(axisName);
            }
            catch { }

            if (Mathf.Abs(v) > 0.1f)
            {
                Debug.Log(axisName + " = " + v);
            }
        }
    }
}
