using UnityEngine;

[DisallowMultipleComponent]
public class RobotController : MonoBehaviour
{
    [Header("=== 可控部件 Transform ===")]
    public Transform bodyTransform;       // 中部：控制 Y 轴抬升/下降
    public Transform headPitchTransform;  // 头部俯仰：绕 X 轴
    public Transform headYawTransform;    // 头部左右转：绕 Y 轴

    [Header("=== 限制范围 (米 / 度) ===")]
    public float bodyMinY = 0.005f;
    public float bodyMaxY = 0.05f;

    public float headPitchMin = -10f;
    public float headPitchMax =  10f;

    public float headYawMin   = -60f;
    public float headYawMax   =  60f;

    [Header("=== 实时参数（滑块调节） ===")]
    [Range(0.005f, 0.05f)]   public float bodyY = 0.005f;
    [Range(-10f, 10f)]      public float headPitchAngle = 0f;
    [Range(-60f, 60f)]      public float headYawAngle   = 0f;

    [Header("=== 全局速度（米/秒 或 度/秒） ===")]
    [Tooltip("按键微调速度，单位：m/s 或 °/s")]
    public float speed = 10f;

    void Update()
    {
        ApplyTransforms();
        HandleKeyboardInput();
    }

    void ApplyTransforms()
    {
        if (bodyTransform)
        {
            float y = Mathf.Clamp(bodyY, bodyMinY, bodyMaxY);
            Vector3 p = bodyTransform.localPosition;
            bodyTransform.localPosition = new Vector3(p.x, y, p.z);
        }

        if (headPitchTransform)
        {
            float x = Mathf.Clamp(headPitchAngle, headPitchMin, headPitchMax);
            Vector3 e = headPitchTransform.localEulerAngles;
            headPitchTransform.localEulerAngles = new Vector3(x, e.y, e.z);
        }

        if (headYawTransform)
        {
            float y = Mathf.Clamp(headYawAngle, headYawMin, headYawMax);
            Vector3 e = headYawTransform.localEulerAngles;
            headYawTransform.localEulerAngles = new Vector3(e.x, y, e.z);
        }
    }

    void HandleKeyboardInput()
    {
        float d = speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            headYawAngle = Mathf.Clamp(headYawAngle + d, headYawMin, headYawMax);
            Debug.Log("Pressed A → headYawAngle = " + headYawAngle);
        }
        if (Input.GetKey(KeyCode.D))
        {
            headYawAngle = Mathf.Clamp(headYawAngle - d, headYawMin, headYawMax);
            Debug.Log("Pressed D → headYawAngle = " + headYawAngle);
        }
        if (Input.GetKey(KeyCode.W))
        {
            headPitchAngle = Mathf.Clamp(headPitchAngle - d, headPitchMin, headPitchMax);
            Debug.Log("Pressed W → headPitchAngle = " + headPitchAngle);
        }
        if (Input.GetKey(KeyCode.S))
        {
            headPitchAngle = Mathf.Clamp(headPitchAngle + d, headPitchMin, headPitchMax);
            Debug.Log("Pressed S → headPitchAngle = " + headPitchAngle);
        }
        if (Input.GetKey(KeyCode.R))
        {
            bodyY = Mathf.Clamp(bodyY + 0.01f*d, bodyMinY, bodyMaxY);
            Debug.Log("Pressed R → bodyY = " + bodyY);
        }
        if (Input.GetKey(KeyCode.F))
        {
            bodyY = Mathf.Clamp(bodyY - 0.01f*d, bodyMinY, bodyMaxY);
            Debug.Log("Pressed F → bodyY = " + bodyY);
        }
    }
}