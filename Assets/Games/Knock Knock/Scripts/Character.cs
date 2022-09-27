using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnockKnock
{
    [Serializable]
    public class Character
    {
        public string name;
        public int DiscoveryTime;
        public int TravelTime;
        public int CheckTime;
        public float timeBeforeSpeaking;
    }
}
