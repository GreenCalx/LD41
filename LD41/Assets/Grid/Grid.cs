using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Grid
{
    public class Grid : MonoBehaviour
    {
        private const int MATRIX_N = 7;
        private const int MATRIX_M = 7;

        private List<List<Node>> __matrix;
        private List<Arc> __arcs;



        private void buildGrid()
        {
            // Build Node Matrix
            __matrix = new List<List<Node>>(MATRIX_N);
            for (int i = 0; i < MATRIX_N; ++i)
            {
                __matrix.Add( new List<Node>(MATRIX_M) );
                for (int j = 0; j < MATRIX_M; ++j)
                    __matrix[i].Add( new Node(i+j) );

                // Build Links Between Nodes
                foreach( Node node in __matrix[i])
                {
                    // TO Arcs [ add them to local list as it is enough to have complete coverage]
                    int to_id1 = node.id + 1;
                    if (to_id1 < MATRIX_N + MATRIX_M)
                    {
                        Node to1 = __matrix[i][to_id1];
                        Arc arc_to1 = new Arc(node, to1);
                        node.arcs.Add(arc_to1);
                        __arcs.Add(arc_to1);
                    }

                    int to_id2 = node.id + MATRIX_N;
                    if (to_id2 < MATRIX_N+ MATRIX_M)
                    {
                        Node to2 = __matrix[i][to_id2];
                        Arc arc_to2 = new Arc(node, to2);
                        node.arcs.Add(arc_to2);
                        __arcs.Add(arc_to2);
                    }

                    // FROM Arcs
                    int from_id1 = node.id - 1;
                    if (from_id1 > 0)
                    {
                        Node from1 = __matrix[i][from_id1];
                        Arc arc_from1 = new Arc(node, from1);
                        node.arcs.Add(arc_from1);
                    }

                    int from_id2 = node.id - MATRIX_N;
                    if (from_id2 > 0)
                    {
                        Node from2 = __matrix[i][from_id2];
                        Arc arc_from2 = new Arc(node, from2);
                        node.arcs.Add(arc_from2);
                    }
                }//! for __matrix[i]
            }//! for MATRIX_N

        }//! buildGrid


        // Use this for initialization
        void Start()
        {
            buildGrid();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}