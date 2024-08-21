using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using System;
using System.Collections.Generic;
using GMDialogue;
using UnityEngine;
using GMDialogue.EditorUI;
using UnityEditor.UIElements;

public class BehaviourTreeView : GraphView
{
    private DialogueTree tree;

    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits> { }

    public BehaviourTreeView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DialoguePlugin/Editor/BehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }

    public void Init()
    {
        this.Q<ToolbarButton>("B_Recenter").clickable.clicked += Recenter;
        this.Q<ToolbarButton>("B_Recenter2").clickable.clicked += Recenter2;
    }

    public void PopulateView(DialogueTree tree)
    {
        if (this.tree == tree) return;
        this.tree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        if (tree.rootNode == null)
        {
            tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }

        tree.nodes.ForEach(CreateNodeView);

        tree.nodes.ForEach(node =>
        {
            NodeView parentView = FindNodeView(node);
            List<GMDialogue.Node> children = node.GetChildren();

            for (int i = 0; i < children.Count; i++)
            {
                NodeView childView = FindNodeView(children[i]);
                Edge edge = parentView.outputs[i].ConnectTo(childView.input);
                AddElement(edge);
            }
        });
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        List<Port> compatiblePorts = new List<Port>();

        foreach (var endPort in ports)
            if (endPort.direction != startPort.direction && endPort.node != startPort.node)
                compatiblePorts.Add(endPort);

        return compatiblePorts;
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            foreach (var elem in graphViewChange.elementsToRemove)
            {
                NodeView nodeView = elem as NodeView;
                if (nodeView != null)
                {
                    tree.DeleteNode(nodeView.node);
                    nodeView.OnDestroy();
                }

                Edge edge = elem as Edge;
                if (edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    parentView.node.RemoveChild(childView.node);
                    parentView.OnNodeValidate();
                }

            }
        }

        if (graphViewChange.edgesToCreate != null)
        {
            foreach (var edge in graphViewChange.edgesToCreate)
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;
                parentView.node.AddChild(childView.node);
                parentView.OnNodeValidate();
            }
        }

        return graphViewChange;
    }

    public void Recenter()
    {
        Rect rect;
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        foreach (var node in nodes)
        {
            rect = node.GetPosition();

            if (rect.xMin < minX) minX = rect.xMin;
            if (rect.xMax > maxX) maxX = rect.xMax;
            if (rect.yMin < minY) minY = rect.yMin;
            if (rect.yMax > maxY) maxY = rect.yMax;
        }

        float scale = Mathf.Min(
            contentRect.width / (maxX - minX) * .9f, 
            contentRect.height / (maxY - minY) * .8f);

        viewTransform.scale = Vector2.one * Mathf.Min(scale, 1);

        viewTransform.position = new Vector2(
            -(minX + maxX) / 2 + contentRect.width * viewTransform.scale.x / 2,
            -(minY + maxY) / 2 + contentRect.height * viewTransform.scale.x / 2);
    }
    public void Recenter2()
    {
        Rect rect;
        float minX = Mathf.Infinity;
        float maxX = Mathf.NegativeInfinity;
        float minY = Mathf.Infinity;
        float maxY = Mathf.NegativeInfinity;

        foreach (var node in nodes)
        {
            rect = node.GetPosition();

            if (rect.xMin < minX) minX = rect.xMin;
            if (rect.xMax > maxX) maxX = rect.xMax;
            if (rect.yMin < minY) minY = rect.yMin;
            if (rect.yMax > maxY) maxY = rect.yMax;
        }

        float scale = Mathf.Min(
            contentRect.width / (maxX - minX) * .9f,
            contentRect.height / (maxY - minY) * .8f);

        viewTransform.scale = Vector2.one * Mathf.Min(scale, 1);

        viewTransform.position = new Vector2(
            -(minX + maxX) / 2 + contentRect.width / 2,
            -(minY + maxY) / 2 + contentRect.height / 2);
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        {
            var types = TypeCache.GetTypesDerivedFrom<ActionNode>();

            foreach (var type in types)
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
        }
        {
            var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();

            foreach (var type in types)
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
        }
        {
            var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();

            foreach (var type in types)
                evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
        }
    }

    private void CreateNode(Type type)
    {
        Recenter();
        GMDialogue.Node node = tree.CreateNode(type);
        CreateNodeView(node);
    }

    private void CreateNodeView(GMDialogue.Node node)
    {
        Type nodeViewType = NodeToNodeViewType.GetNodeViewType(node.GetType());
        object nodeView = nodeViewType.GetConstructor(new Type[] { typeof(GMDialogue.Node) }).Invoke(new object[] { node });
        AddElement(nodeView as NodeView);
    }

    public NodeView FindNodeView(GMDialogue.Node node)
    {
        return GetNodeByGuid(node.guid) as NodeView;
    }
}