using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HearthScript
{
    public class HearthUnity : MonoBehaviour
    {

        public static void Invade () {
            SceneMgr.Get().gameObject.AddComponent<HearthUnity>();
        }


        public void OnGUI() {
            GUI.Label(new Rect(10, 20, 180, 160), "Patch Success！ ");
        }

    }
}
