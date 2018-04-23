using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Player : MonoBehaviour
    {
        private bool __isInBuildMode { get; set; }

        void Start()
        {
            __isInBuildMode = false;
        }

        void Update()
        {
            // Build Mode
            if  (Input.GetKey(KeyCode.B))
                __isInBuildMode = !__isInBuildMode; 

            if (__isInBuildMode)
            {
                BuilderManager bm = GetComponent<BuilderManager>();
                bm.PollConstruct();
            }

        }//! Update

    }
}
