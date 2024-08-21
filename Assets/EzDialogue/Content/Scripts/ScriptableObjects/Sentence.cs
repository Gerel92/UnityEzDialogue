using System;

namespace GMDialogue
{
    [Serializable]
    public class Sentence
    {
        public string text = "Hi!";
        public Character speaker = default;
        public bool isLeft = false;
    }
}
