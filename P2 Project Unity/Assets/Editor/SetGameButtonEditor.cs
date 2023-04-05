using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SetGameButton))]
[CanEditMultipleObjects]
[System.Serializable]

public class SetGameButtonEditor : Editor
{
    public override void OnInspectorGUI() //overridning the function from the Editor 
    {
        DrawDefaultInspector();

        SetGameButton myScript = target as SetGameButton;
        switch (myScript.ButtonType)
        {
            case SetGameButton.EButtonType.PairNumberBtn:
                myScript.PairNumber = (GameSettings.EPairNumber)EditorGUILayout.EnumPopup("Pair Numbers", myScript.PairNumber);
                break; 
            case SetGameButton.EButtonType.PuzzleCategoryBtn:
                myScript.PuzzleCategories = (GameSettings.EPuzzleCategories)EditorGUILayout.EnumPopup ("Puzzle Categories", myScript.PuzzleCategories);
                break;
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
           
    }
}