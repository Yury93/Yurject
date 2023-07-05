using System.Collections.Generic;
using UnityEngine;

namespace Yurject
{
    public class SceneContext : Context
    {
        [SerializeField] private List<GameObject> monoInstallers;
   
        public override void OnRegister()
        {
            foreach (var monoInstaller in monoInstallers)
            {
                Register(monoInstaller);
            }
        }
    }
}


