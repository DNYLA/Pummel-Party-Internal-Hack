using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack
{
    public class Loader
    {
        private static GameObject Load;
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<Main>();
            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void Unload()
        {
            //GameObject.Destroy(Loader.Load);
            UnityEngine.Object.DestroyImmediate(Load);
        }
    }
}
