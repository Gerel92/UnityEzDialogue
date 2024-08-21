using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace GMDialogue.EditorUI
{
    public class ChoiceNodeView : NodeView
    {
        public ChoiceNodeView(Node node) : base(node) { }

        public override void OnNodeValidate()
        {
            base.OnNodeValidate();
            CreateOutputPorts();
        }

        protected override void CreateOutputPorts()
        {
            string[] choices = ((ChoiceNode)node).choices.choices;
            Port _output;

            if (choices == null)
                return;

            // add ports
            while (outputs.Count < choices.Length)
            {
                _output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
                outputs.Add(_output);
                outputContainer.Add(_output);
            }

            // remove ports
            while (outputs.Count > choices.Length)
            {
                foreach (var connection in outputs[outputs.Count - 1].connections)
                    connection.parent.Remove(connection);

                outputs[outputs.Count - 1].Clear();
                outputContainer.Remove(outputs[outputs.Count - 1]);
                outputs.Remove(outputs[outputs.Count - 1]);
            }

            // set ports name
            for (int i = 0; i < outputs.Count; i++)
                outputs[i].portName = choices[i];
        }
    }
}