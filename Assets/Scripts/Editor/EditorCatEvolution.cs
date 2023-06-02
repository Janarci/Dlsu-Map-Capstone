using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CatEvolutionRequirement.EvolutionRequirement))]
public class EditorCatEvolution : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        //base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(position, label, property);
        
        SerializedProperty item = property.FindPropertyRelative("item");
        SerializedProperty amount = property.FindPropertyRelative("amount");

        Rect labelPosition = new Rect(position.x, position.y, position.width, position.height);
        position = EditorGUI.PrefixLabel(
            labelPosition,
            EditorGUIUtility.GetControlID(FocusType.Passive),
            new GUIContent(
                "Item and Amount")
            );
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float widthSize = position.width / 3;
        float offsetSize = 2;

        Rect pos1 = new Rect(position.x, position.y, widthSize, position.height);
        Rect pos2 = new Rect(position.x + widthSize, position.y, widthSize, position.height);

        EditorGUI.PropertyField(pos1, item, GUIContent.none);
        EditorGUI.PropertyField(pos2, amount, GUIContent.none);

        EditorGUI.indentLevel = indent;


        //for (int i = 0; i < 5; i++)
        //{
        //    EditorGUILayout.BeginHorizontal();
        //    EditorGUILayout.TextField("new");

        //    EditorGUILayout.EndHorizontal();
        //}
    }
}
