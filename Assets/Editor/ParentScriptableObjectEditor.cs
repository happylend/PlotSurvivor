using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeaponData))]
public class ParentScriptableObjectEditor : Editor
{
    private SerializedProperty childData;
    private bool showChildData;

    private void OnEnable()
    {
        childData = serializedObject.FindProperty("BulletType");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawProperties(serializedObject.GetIterator());

        EditorGUILayout.Space();

        showChildData = EditorGUILayout.Foldout(showChildData, "Bullet Data");
        
        //EditorGUILayout.PropertyField(childData);
        
        if(showChildData)
        {
            if (childData.objectReferenceValue != null)
            {
                EditorGUI.indentLevel++;
                DrawProperties(new SerializedObject(childData.objectReferenceValue).GetIterator());
                EditorGUI.indentLevel--;
            }
        }
   
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawProperties(SerializedProperty property)
    {
        SerializedProperty iterator=property.Copy();
        bool enterChildren = true;

        while(iterator.NextVisible(enterChildren))
        {
            if(ShouldDrawProperty(iterator))
            {
                EditorGUILayout.PropertyField(iterator, true);
            }

            enterChildren = false;

            if(iterator.hasVisibleChildren && ShouldEnterChildren(iterator))
            {
                EditorGUI.indentLevel++;
                DrawProperties(iterator.Copy());
                EditorGUI.indentLevel--;
            }
        }
    }

    private bool ShouldDrawProperty(SerializedProperty property)
    {
        //自定义不显示内容，即只在Buttle中修改的内容
        if(property.name=="m_Script")
        {
            return false;
        }
        
        return true;
    }

    private bool ShouldEnterChildren(SerializedProperty property)
    {
        //自定义子对象显示类型
        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue != null)
        {
            return property.objectReferenceValue.GetType().IsSubclassOf(typeof(ScriptableObject));
        }

        return false;
    }

}
