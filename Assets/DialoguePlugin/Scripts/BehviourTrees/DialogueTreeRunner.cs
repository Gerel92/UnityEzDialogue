using UnityEngine;

namespace GMDialogue
{
    public class DialogueTreeRunner : MonoBehaviour
    {
        [SerializeField] private DialogueTree tree;

        private void Start()
        {
            // prevents multiple runner to execute the same nodes
            tree = tree.Clone();
        }

        private void Update()
        {
            if (tree.state == NodeState.RUNNING) 
                tree.Update();
        }
    } 
}