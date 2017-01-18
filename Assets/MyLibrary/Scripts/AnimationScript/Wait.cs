using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OranUnityUtils 
{
    public class Wait : AnimationScript {

        public Wait(GameObject coroHost, float time) {
            Init(coroHost, this.WaitForSeconds_Coro(time));
        }

    }
}