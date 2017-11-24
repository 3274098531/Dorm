using System.Collections.Generic;

namespace MyFramework.Domain
{
    public class MenuGroup
    {
        public MenuGroup()
        {
            Functions = new List<Function>();
        } 
        public string Name { get; set; }
        public string Ico { get; set; }
        public List<Function> Functions { get; set; }
    }
}