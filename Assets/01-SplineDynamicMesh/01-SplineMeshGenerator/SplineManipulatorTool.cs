using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using UnityEditor.Splines;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.Splines;
using static UnityEditor.Splines.SplineHandles;

[EditorTool("Spline Manipulator Tool", typeof(SplineContainer))]
public class SplineManipulatorTool : EditorTool
{
    public override GUIContent toolbarIcon => EditorGUIUtility.IconContent("AvatarPivot");

    [Shortcut("Activate Spline Manipulator Tool", KeyCode.U)]
    static void SplineManipulatorToolShortcut()
    {
        if(Selection.GetFiltered<SplineContainer>(SelectionMode.TopLevel).Length > 0)
        {
            ToolManager.SetActiveTool<SplineManipulatorTool>();
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        
        if (!(window is SceneView))
            return;

        
        SplineContainer spline = (SplineContainer)target;
        EditorGUI.BeginChangeCheck();
        Vector3 position = Handles.DoPositionHandle(spline.transform.position,
                                                    spline.transform.rotation);
        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(spline, "Values Changed");
            spline.transform.position = position;
        }


    }


}
