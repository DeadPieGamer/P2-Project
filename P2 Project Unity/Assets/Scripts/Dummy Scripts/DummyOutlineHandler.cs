using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Felix.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DummyOutlineHandler : MonoBehaviour
{
    [SerializeField, Tooltip("Whether the sprite starts outlined")] public bool isOutlined = false;

    // For finding the basic materials
    [Tooltip("Path to folder containing the material references")] private string materialPath = "Dummy/MaterialTesting/";
    [Tooltip("Name of the basic Unity material scriptable object")] private string basicName = "BasicMaterial";
    [Tooltip("Name of the custom outlined material")] private string outlinedName = "Mat_FelixOutline";

    // Whether the advanced options should be used or not
    [Space(), SerializeField, Tooltip("Whether to use advanced options")] private bool useAdvanced = false;
    
    // Things changable in the Advanced Options menu
    [HideInInspector, SerializeField, Tooltip("The custom outlined material")] private Material outlinedMaterial;
    [HideInInspector, SerializeField, Tooltip("Scriptable object containing the basic Unity material")] private Material basicMaterial;
    [HideInInspector, SerializeField, Tooltip("Functions to be called when this becomes outlined")] private BoolEvent outlineActions;

    // I don't care about the warning Unity wants to give me here, so I'll just ignore it
#pragma warning disable 0414
#if UNITY_EDITOR
    [HideInInspector, SerializeField, Tooltip("Whether to show advanced options or not")] private bool showAdvanced = false;
#endif
#pragma warning restore 0414

    // Awake is the first function called
    private void Awake()
    {
        if (!useAdvanced || basicMaterial == null)
        {
            basicMaterial = Resources.Load<SOMatKeeper>(materialPath + basicName).Material;
        }

        // Load the basic material if no custom one is assigned
        if (!useAdvanced || outlinedMaterial == null)
        {
            outlinedMaterial = Resources.Load<Material>(materialPath + outlinedName);
        }

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

        if (GetComponent<Renderer>() != null)
        {
            GetComponent<Renderer>().material = isOutlined ? outlinedMaterial : basicMaterial;
        }
        else
        {
            GetComponent<Image>().material = isOutlined ? outlinedMaterial : basicMaterial;
        }

        // If using advanced, trigger outlineActions
        if (useAdvanced)
        {
            outlineActions?.Invoke(isOutlined);
        }
    }

#region Editor
#if UNITY_EDITOR

    // Custom editor inspired by Kap Koder
    // https://www.youtube.com/watch?v=RImM7XYdeAc

    [CustomEditor(typeof(DummyOutlineHandler))]
    public class DummyOutlineHandlerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Renders the base thing
            base.OnInspectorGUI();

            // Get the DummyOutlineHandler to check whether advanced should be used or not
            DummyOutlineHandler dummyOutlineHandler = (DummyOutlineHandler)target;

            // If the player wishes to use advanced options, make them interactable
            EditorGUI.BeginDisabledGroup(!dummyOutlineHandler.useAdvanced);
            DrawAdvanced(serializedObject, dummyOutlineHandler.useAdvanced);
            // Don't make anything else uninteractable
            if (!dummyOutlineHandler.useAdvanced)
            {
                EditorGUI.EndDisabledGroup();
            }
        }

        /// <summary>
        /// Draws the Advanced Menu in the inspector
        /// </summary>
        /// <param name="serializedObject"></param>
        private static void DrawAdvanced(SerializedObject serializedObject, bool useAdvanced)
        {
            // Some space
            EditorGUILayout.Space();

            // Get the bool
            bool showAdvanced = serializedObject.FindProperty("showAdvanced").boolValue;

            // Begin checking for changes in the foldout
            EditorGUI.BeginChangeCheck();
            // Begins a field for dropdown. If using advanced options it is toggleable, else it is collapsed
            showAdvanced = EditorGUILayout.Foldout(useAdvanced ? showAdvanced : false, "Advanced", true);

            // If the foldout is changed, register it
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.FindProperty("showAdvanced").boolValue = showAdvanced;
            }

            // If the ShowAdvanced dropdown menu is to be open, show the following
            if (showAdvanced)
            {
                EditorGUI.indentLevel++;

                // Property field to assign and unassign materials to the outlinedMaterial
                EditorGUILayout.PropertyField(serializedObject.FindProperty("outlinedMaterial"));
                // Property field to assign and unassign materials to the outlinedMaterial
                EditorGUILayout.PropertyField(serializedObject.FindProperty("basicMaterial"));
                // Some spacing
                EditorGUILayout.Space();
                // Show the Outline Actions
                EditorGUILayout.PropertyField(serializedObject.FindProperty("outlineActions"));

                EditorGUI.indentLevel--;
            }

            // Apply all changes made
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
#endregion
}
