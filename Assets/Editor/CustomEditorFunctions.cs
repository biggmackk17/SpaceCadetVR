using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(EventManager), true)]
public class CustinEditorFunctions : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EventManager _manager = (EventManager)target;
        if (GUILayout.Button("Enter Boot Sequence"))
        {
            _manager.BootUpSequence();
        }

        if (GUILayout.Button("PressBootButtons"))
        {
            _manager.PressAllStartUp();
        }

        if (GUILayout.Button("Warp1"))
        {
            _manager.SkipToWarp1();
        }

        if (GUILayout.Button("Warp2"))
        {
            _manager.SkipToWarp2();
        }

        if (GUILayout.Button("Warp3"))
        {
            _manager.SkipToWarp3();
        }

        if (GUILayout.Button("FreeFly"))
        {
            _manager.FreeFly();
        }


        if (GUILayout.Button("PowerDown"))
        {
            _manager.PowerDown();
        }


        if (GUILayout.Button("PowerUp"))
        {
            _manager.PowerUp();
        }
    }


}

[CustomEditor(typeof(HyperDrive), true)]
public class HypeDriveCustomEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        HyperDrive _hyperDrive = (HyperDrive)target;
        if (GUILayout.Button("BreakReactor"))
        {
            _hyperDrive.BreakReactor();
        }


    }



}

[CustomEditor(typeof(Switches), true)]
public class SwitchesEditorButton : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Switches switches = (Switches)target;
        if (GUILayout.Button("Press Switch"))
        {
            switches.Switch();
        }


    }


}


