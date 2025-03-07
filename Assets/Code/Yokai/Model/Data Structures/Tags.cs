using System.Collections.Generic;

namespace Systems.Yokai
{
    public interface ITags
    {
        void AddTag(string tag);
        void RemoveTag(string tag);
        bool HasTag(string tag);
    }
    
    public class Tags : ITags
    {
        private HashSet<string> tags;
        
        bool this[string tag] => HasTag(tag);
        
        public Tags(IList<string> startingTags)
        {
            tags = new HashSet<string>();
            
            foreach (var tag in startingTags)
            {
                AddTag(tag);
            }
        }
        
        public void AddTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)) return;
            
            tags.Add(tag);
        }
        
        public void RemoveTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)) return;
            
            tags.Remove(tag);
        }
        
        public bool HasTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)) return false;
            
            return tags.Contains(tag);
        }
    }
}