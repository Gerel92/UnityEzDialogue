using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using GMDialogue;
using System.Collections.Generic;

public class NodeView : UnityEditor.Experimental.GraphView.Node
{
    public GMDialogue.Node node;
    public Port input;
    public List<Port> outputs = new List<Port>();

    public static Action<GMDialogue.Node> OnNodeSelected;

    public NodeView(GMDialogue.Node node)
    {
        this.node = node;
        title = node.name;
        viewDataKey = node.guid;

        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();

        node.onValidate += OnNodeValidate;
        OnNodeValidate();
    }

    protected virtual void CreateInputPorts()
    {
        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        input.portName = "";
        inputContainer.Add(input);
    }

    protected virtual void CreateOutputPorts() { }

    public virtual void OnNodeValidate() { }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        node.position = newPos.position;
    }

    public override void OnSelected()
    {
        base.OnSelected();
        OnNodeSelected?.Invoke(node);
    }

    public virtual void OnDestroy()
    {
        node.onValidate -= OnNodeValidate;
    }
}