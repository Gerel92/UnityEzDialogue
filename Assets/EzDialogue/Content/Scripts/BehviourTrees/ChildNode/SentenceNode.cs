using UnityEditor;
using UnityEngine;

namespace GMDialogue
{
    public class SentenceNode : DecoratorNode
    {
        public Sentence sentence;

        private bool replicaEnded;

        protected override void OnStart()
        {
            replicaEnded = false;
            SentenceDisplay.Instance.OnTextEnd += OnTextEnd;
            SentenceDisplay.Instance.SetText(sentence.text);
        }

        protected override void OnStop() { Debug.Log("end"); }

        protected override NodeState OnUpdate()
        {
            return replicaEnded ? NodeState.SUCCESS : NodeState.RUNNING;
        }
        
        private void OnTextEnd()
        {
            SentenceDisplay.Instance.OnTextEnd -= OnTextEnd;
            replicaEnded = true;
        }
    }
}