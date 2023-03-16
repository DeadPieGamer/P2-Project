using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Felix.Variables
{
    [Serializable]
    public class DummyCardSet
    {
        public int SetTypeInt { get { return SetTypeInt; } set { SetTypeEnum = (SetTypes)value; SetTypeInt = value; } }
        public SetTypes SetTypeEnum { get { return (SetTypes)SetTypeInt; } set { SetTypeEnum = value; } }
    }
}
