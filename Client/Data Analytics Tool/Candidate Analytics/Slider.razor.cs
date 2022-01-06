using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace XebecPortal.Client.Data_Analytics_Tool.Candidate_Analytics
{
    public partial class Slider : ComponentBase
    {
        [Parameter]
        public int Min { get; set; } = 0;
        [Parameter]
        public int Max { get; set; } = 100;

        [Parameter]
        public bool IsImportant { get; set; } = true;

        [Parameter]
        public int SliderValue { get; set; } = 0;

        [Parameter]
        public bool Lock { get; set; }
    }

   
}
