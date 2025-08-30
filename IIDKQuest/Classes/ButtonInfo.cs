﻿using System;

namespace JupiterX.Classes
{
    public class ButtonInfo
    {
        public string buttonText = "-";
        public string overlapText;

        public string toolTip = ".";

        public Action method;
        public Action enableMethod;
        public Action disableMethod;

        public bool enabled;
        public bool isTogglable = true;
    }
}
