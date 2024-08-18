using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GMDialogue.EditorUI
{
    public class SequenceNodeView : NodeView
    {
        public SequenceNodeView(Node node) : base(node) { }

        public override void OnNodeValidate()
        {
            base.OnNodeValidate();
            CreateOutputPorts();
        }

        protected override void CreateOutputPorts()
        {
            List<Node> children = ((CompositeNode)node).children;
            Port _output;

            // add ports
            while (outputs.Count < children.Count + 1)
            {
                _output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                outputs.Add(_output);
                outputContainer.Add(_output);
                _output.portName = outputs.Count.ToString();
            }

            // remove ports
            while (outputs.Count > children.Count + 1)
            {
                foreach (var connection in outputs[outputs.Count - 1].connections)
                    connection.parent.Remove(connection);

                outputs[outputs.Count - 1].Clear();
                outputContainer.Remove(outputs[outputs.Count - 1]);
                outputs.Remove(outputs[outputs.Count - 1]);
            }
        }
    }
}