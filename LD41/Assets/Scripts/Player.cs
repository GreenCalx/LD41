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
            // Poll for construction if in build mode
            if (__isInBuildMode)
            {
                BuilderManager bm = GetComponent<BuilderManager>();
                bm.PollConstruct();
            }

            // Build Mode
            if  (Input.GetKeyDown(KeyCode.B))
                __isInBuildMode = !__isInBuildMode; 

        }//! Update

    }
}
