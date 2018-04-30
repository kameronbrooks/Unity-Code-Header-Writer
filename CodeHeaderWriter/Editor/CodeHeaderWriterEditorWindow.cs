using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CodeHeaderWriterEditorWindow : EditorWindow {
    private static CodeHeaderWriterEditorWindow _instance;
    public static CodeHeaderWriterEditorWindow instance
    {
        get
        {
            return _instance;
        }
    }

    [MenuItem("Open", menuItem = "Window/Code Headers")]
    public static void Open()
    {
        _instance = GetWindow<CodeHeaderWriterEditorWindow>();
  
    }

    private string _currentHeader = CodeHeaderWriter.DefaultHeader;
    private bool _includeSubdirectories;

    private void OnGUI()
    {
        _currentHeader = EditorGUILayout.TextArea(_currentHeader);
        _includeSubdirectories = EditorGUILayout.Toggle("Include Subdirectories", _includeSubdirectories);
        if(GUILayout.Button(new GUIContent("Add Headers", "Adds the specified header to all script files in a chosen directory that do not already contain it"))) {
            AddHeaders();
        }
    }
    private void AddHeaders()
    {
        string folderName = EditorUtility.OpenFolderPanel("Add Script Headers", Application.dataPath, "");
        if(folderName != "")
        {
            CodeHeaderWriter.UpdateAllScriptsInDirectory(_currentHeader, folderName, _includeSubdirectories);
        }
    }
    private void OnEnable()
    {
        string[] filenames = CodeHeaderWriter.GetScriptNamesInDirectory(Application.dataPath);

        
    }
    private void OnDisable()
    {
        
    }
}
