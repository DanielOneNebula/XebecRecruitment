using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace XebecPortal.Client.Data_Analytics_Tool
{
    public class Dashboard : ComponentBase
    {
        public string PageName { get; set; } = "";
        public bool menu { get; set; } = true;
        public bool jobBoard { get; set; } = true;
        public bool dataAnalytics { get; set; } = true;
        public bool candidateAnalytics { get; set; } = true;
        public bool department { get; set; } = false;
        public bool registration { get; set; } = false;
        public bool traffic { get; set; } = false;
        public bool fill { get; set; } = false;
        public bool hire { get; set; } = false;

        public bool candidateIdeal { get; set; }
        public bool candidateCandidates { get; set; }
        public bool d1 { get; set; } = true;
        public bool d2 { get; set; } = false;
        public bool d3 { get; set; } = false;
        public bool d4 { get; set; } = false;
        public bool d5 { get; set; } = false;
        public bool d6 { get; set; } = false;
        public bool d7 { get; set; } = false;
        public bool d8 { get; set; } = false;
    }
}
