using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GMDialogue
{
    public class DecoratorNodeView : NodeView
    {
        public DecoratorNodeView(Node node) : base(node) { }

        protected override void CreateOutputPorts()
        {
            if (outputs.Count > 0)
                return;

            Port _output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            outputs.Add(_output);
            outputContainer.Add(_output);
            _output.portName = "";
        }
    }
}