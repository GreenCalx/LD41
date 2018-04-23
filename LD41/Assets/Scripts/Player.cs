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
        private SpriteRenderer buildMode = null;

        void Start()
        {
            __isInBuildMode = false;
            buildMode = GameObject.Find("buildModePanel").GetComponent<SpriteRenderer>();
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
            if (!!buildMode)
            {
                buildMode.color = (__isInBuildMode) ?
                    new Color(255, 255, 255, 255) :
                    new Color(255, 255, 255, 0);
            }

        }//! Update

    }
}
