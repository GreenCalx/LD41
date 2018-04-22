using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Grid
{
    class Node : MonoBehaviour
    {
        private const string NODE_STRING_ID = "Node " /* + int id */; // Ex: <Node 1>
        private const string AREA_STRING_ID = "Area " /* + int id */; // Ex: <Node 1>

        public int id { get; set; }
        public List<Arc> arcs;
        public NodeArea nodeArea;
        public GameObject node;

        public void setNode( GameObject iNode ) { this.node = iNode; }
        public GameObject getNode() { return node; }

        public Node(int nodeID)
        {
            arcs = new List<Arc>();
            id = nodeID;
            node = GameObject.Find(NODE_STRING_ID + id);
        }
    }
}
