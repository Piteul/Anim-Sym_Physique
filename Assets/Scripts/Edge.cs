using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour {

    private LineRenderer lineRenderer;

    private Node parent;
    public Node target;

    // Use this for initialization
    void Start() {
        parent = gameObject.GetComponentInParent<Node>();
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        if (!(target == null)) {

            lineRenderer.SetPosition(0, parent.transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
        }

    }

    // Update is called once per frame
    void Update() {

        if (!(target == null)) {

            lineRenderer.SetPosition(0, parent.transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
        }
    }
}
