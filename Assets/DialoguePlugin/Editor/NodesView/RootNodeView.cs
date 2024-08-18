using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GMDialogue.EditorUI
{
    public class RootNodeView : NodeView
    {
        public RootNodeView(Node node) : base(node) { }

        protected override void CreateOutputPorts()
        {
            Port _output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            _output.portName = "";
            outputs.Add(_output);
            outputContainer.Add(_output);
        }

        protected override void CreateInputPorts() { }
    }
}