using UnityEditor;
using UnityEngine;

namespace GMDialogue.EditorUI
{
    public class SentenceNodeView : DecoratorNodeView
    {
        public SentenceNodeView(Node node) : base(node) { }

        public override void OnNodeValidate()
        {
            base.OnNodeValidate();
            SetNameFromText();
        }

        private void SetNameFromText()
        {
            string text = ((SentenceNode)node).sentence.text;
            int lengthMax = 30;

            if (text.Length > lengthMax)
                text = text.Remove(lengthMax - 3) + "...";

            title = node.name + " :\n   " + text;
        }
    }
}