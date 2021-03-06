﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TestApp
{
    // This is a more specialized version of the xamarin button, so we can add attributes to it that might be button-specific
    public class GameButton : Button
    {
        
        // The key to define what the button does (could just refer to text in a database?)
        public string Key { get; set; }

        // A reference to the associated option for each event
        public Option buttonOption { get; set; }

        public GameButton copy()
        {
            return (GameButton)MemberwiseClone();
        }
    }
}
