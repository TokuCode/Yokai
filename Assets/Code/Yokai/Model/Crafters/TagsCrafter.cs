using System;

namespace Systems.Yokai
{
    [Serializable]
    public class TagsCrafter : ICrafter<Tags>
    {
        public string[] tags;
        
        public Tags Craft()
        {
            return new Tags(tags);
        }
    }
}