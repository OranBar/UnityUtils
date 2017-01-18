using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OranUnityUtils 
{
    public class EmptyAnim : Wait {

        public EmptyAnim(GameObject coroHost) : base(coroHost, 0f) {

        }
    }
}