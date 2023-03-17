using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyOutlineHandler : MonoBehaviour
{
    [SerializeField, Tooltip("Whether the sprite starts outlined")] private bool isOutlined = false;

    [Tooltip("Path to folder containing the material references")] private string materialPath = "Dummy/MaterialTesting/";
    [Tooltip("Scriptable object containing the basic Unity material")] private SOMatKeeper basicMaterial;
    [Tooltip("Scriptable object containing the custom outlined material")] private SOMatKeeper outlinedMaterial;
    [Tooltip("Name of the basic Unity material scriptable object")] private string basicName = "BasicMaterial";
    [Tooltip("Name of the custom outlined material scriptable object")] private string outlinedName = "OutlinedMaterial";

    // Awake is the first function called
    private void Awake()
    {
        basicMaterial = Resources.Load<SOMatKeeper>(materialPath + basicName);
        outlinedMaterial = Resources.Load<SOMatKeeper>(materialPath + outlinedName);

        ChangeOutline(isOutlined);
    }

    /// <summary>
    /// Switches the outlined status
    /// </summary>
    public void ChangeOutline()
    {
        ChangeOutline(!isOutlined);
    }

    /// <summary>
    /// Enables or disables the outline based on the input
    /// </summary>
    /// <param name="activateOutline"></param>
    public void ChangeOutline(bool activateOutline)
    {
        isOutlined = activateOutline;

        GetComponent<Renderer>().material = isOutlined ? outlinedMaterial.Material : basicMaterial.Material;
    }
}
