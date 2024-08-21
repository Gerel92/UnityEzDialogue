using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace GMDialogue.EditorUI
{
    public class BehaviourTreeEditor : EditorWindow
    {
        private BehaviourTreeView treeView;
        private InspectorView inspectorView;

        [MenuItem("BehaviourTreeEditor/Editor")]
        public static void OpenWindow()
        {
            BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>(); // opens this class window
            wnd.titleContent = new GUIContent("BehaviourTreeEditor"); // sets the name of the window
        }

        private void CreateGUI()
        {
            Debug.Log("Create GUI");

            // Build a clone of BehaviourTreeEditor.uxml in rootVisualElement
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/EzDialogue/Content/Editor/BehaviourTreeEditor.uxml");
            Debug.Log("visualTree: " + visualTree);
            visualTree.CloneTree(rootVisualElement);

            // Add BehaviourTreeEditor.uss as a style for the root
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/EzDialogue/Content/Editor/BehaviourTreeEditor.uss");
            Debug.Log("styleSheet: " + styleSheet);
            rootVisualElement.styleSheets.Add(styleSheet);

            treeView = rootVisualElement.Q<BehaviourTreeView>();
            Debug.Log("treeView: " +  treeView);
            treeView.Init();
            inspectorView = rootVisualElement.Q<InspectorView>();
            Debug.Log("inspectorView: " + inspectorView);

            OnSelectionChange();
        }

        private void OnSelectionChange()
        {
            DialogueTree tree = Selection.activeObject as DialogueTree;

            if (tree && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
                treeView.PopulateView(tree);
        }
    }
}