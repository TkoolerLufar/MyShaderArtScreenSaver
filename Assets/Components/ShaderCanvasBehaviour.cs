using UnityEngine;

public class ShaderCanvasBehaviour : MonoBehaviour
{
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        var camera = this.transform.parent.GetComponent<Camera>();
    }
}
