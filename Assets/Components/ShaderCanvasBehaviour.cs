using UnityEngine;
using UnityEngine.UI;

public class ShaderCanvasBehaviour : MonoBehaviour
{
    public Camera Camera;
    public Image Canvas;

    private int aspectRatioId;
    private int lowColorId;
    private int midColorId;
    private int hiColorId;

    private void Start()
    {
        aspectRatioId = Shader.PropertyToID("_aspectRatio");
        lowColorId = Shader.PropertyToID("_lowColor");
        midColorId = Shader.PropertyToID("_midColor");
        hiColorId = Shader.PropertyToID("_hiColor");
    }

    private void Update()
    {
        // Send the display's aspect ratio to the art shader
        var material = Canvas.material;
        var display = Display.displays[Camera.targetDisplay];
        material.SetFloat(aspectRatioId, display.renderingWidth <= display.renderingHeight
            ? (float)display.renderingWidth / (float)display.renderingHeight
            : (float)display.renderingHeight / (float)display.renderingWidth);

        // set colors by battery info
        Color cautionColor, safeColor;
        switch (SystemInfo.batteryStatus)
        {
            case BatteryStatus.Charging:
                material.SetColor(lowColorId, Color.green);
                cautionColor = Color.red;
                safeColor = Color.blue;
                break;
            case BatteryStatus.Discharging:
                material.SetColor(lowColorId, Color.red);
                cautionColor = Color.green;
                safeColor = Color.blue;
                break;
            default:
                material.SetColor(lowColorId, Color.blue);
                cautionColor = Color.red;
                safeColor = Color.green;
                break;
        }
        if (SystemInfo.batteryLevel == 1f)
        {
            material.SetColor(midColorId, safeColor);
            material.SetColor(hiColorId, cautionColor);
        }
        else
        {
            material.SetColor(midColorId, cautionColor);
            material.SetColor(hiColorId, safeColor);
        }
    }
}
