using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;

public class InspectorView : VisualElement
{
    private GMDialogue.Node selectedNode;
    private Editor editor;

    public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }

    public InspectorView()
    {
        NodeView.OnNodeSelected += UpdateSelection;
    }

    private void UpdateSelection(GMDialogue.Node node)
    {
        if (selectedNode == node) return;
        selectedNode = node;
        Clear();

        Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(selectedNode);
        IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
        Add(container);
    }
}