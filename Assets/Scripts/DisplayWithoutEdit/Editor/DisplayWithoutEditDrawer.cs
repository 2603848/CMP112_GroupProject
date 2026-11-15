using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DisplayWithoutEdit))]
public class DisplayWithoutEditDrawer : PropertyDrawer
{
    //NOT OUR CODE. FOUND ONLINE FOR BETTER ORGANISATION IN THE INSPECTOR
    //To use you simply write '[SerializeField, DisplayWithoutEdit]' before public or private
    //This allows variables to be visible inside the inspector but not editable.
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
