using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour {

    public GameObject[] nodes;
    public float[] distance;
    private GameObject first;
    private GameObject last;
    private Vector3 lastPos;

    private LineRenderer lineRenderer;


    // Use this for initialization
    void Start() {

        if (nodes.Length < 2) {

            Debug.Log("Erreur, chaine impossible");
            Application.Quit();
        }
        else {
            first = nodes[0];
            last = nodes[nodes.Length - 1];
            lastPos = last.transform.position;

            distance = new float[nodes.Length - 1];

            init();
        }
    }

    // Update is called once per frame
    void Update() {
        drawLineRenderer();
        movement();
    }

    public void init() {

        for (int i = 0; i < nodes.Length - 1; i++) {

            lineRenderer = nodes[i].GetComponentInChildren<LineRenderer>();

            //lineRenderer.positionCount = nodes.Length;
            //lineRenderer.setPositions();

            lineRenderer.SetPosition(0, nodes[i].transform.position);
            lineRenderer.SetPosition(1, nodes[i + 1].transform.position);

            //Get distance between each nodes
            distance[i] = Vector3.Distance(nodes[i].transform.position, nodes[i + 1].transform.position);

        }
    }

    public void drawLineRenderer() {
        for (int i = 0; i < nodes.Length - 1; i++) {

            lineRenderer = nodes[i].GetComponentInChildren<LineRenderer>();

            //lineRenderer.positionCount = nodes.Length;
            //lineRenderer.setPositions();

            lineRenderer.SetPosition(0, nodes[i].transform.position);
            lineRenderer.SetPosition(1, nodes[i + 1].transform.position);
        }
    }

    public void movement() {

        Vector3 direction;
        float constraint;

        //From first to last
        for (int i = 0; i < nodes.Length - 2; i++) {

            direction = nodes[i + 1].transform.position - nodes[i].transform.position;
            direction = direction.normalized;

            //Constraint
            constraint = nodes[i].GetComponent<Node>().constraint;
            if (constraint != 0) {
                //Direction : H -> I 
                Vector3 dir = nodes[i].transform.position - nodes[i + 1].transform.position; 
                //Direction : I -> J
                Vector3 dir2 = nodes[i - 1].transform.position - nodes[i].transform.position;
                dir = dir.normalized;
                dir2 = dir2.normalized;

                float angle = Vector3.Angle(dir, dir2);
                Debug.Log("Angle : " + angle.ToString());

                if (angle > constraint) {

                }
                else {
                    nodes[i + 1].transform.position = nodes[i].transform.position + (distance[i] * direction);
                }
            }
        }

        //From last to first
        for (int i = nodes.Length - 1; i > 0; i--) {
            direction = nodes[i - 1].transform.position - nodes[i].transform.position;
            direction = direction.normalized;

            //Constraint
            constraint = nodes[i].GetComponent<Node>().constraint;

            if (constraint != 0) {
                //Direction : I - > J
                Vector3 dir = nodes[i].transform.position - nodes[i + 1].transform.position;
                dir = dir.normalized;

                float angle = Vector3.Angle(dir, direction);

                if (angle > constraint) {

                    //Debug.Log("Angle : " + angle.ToString());
                }
                else {
                    nodes[i - 1].transform.position = nodes[i].transform.position + (distance[i - 1] * direction);
                }
            }
        }

        //nodes[nodes.Length-1].transform.position = lastPos;

    }


}
