using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {
    public Transform LookAtPosition = null;

	void Start () {
        Debug.Assert(LookAtPosition != null);
	}
	Vector3 delta = Vector3.zero;
    Vector3 mouseDownPos = Vector3.zero;
	// Update is called once per frame
	void Update () {


        // Viewing vector is from transform.localPosition to the lookat position
        Vector3 V = LookAtPosition.localPosition - transform.localPosition;
        Vector3 W = Vector3.Cross(-V, Vector3.up);
        Vector3 U = Vector3.Cross(W, -V);
        // transform.localRotation = Quaternion.LookRotation(V, U);
        transform.localRotation = Quaternion.FromToRotation(Vector3.up, U);
        Quaternion alignU = Quaternion.FromToRotation(transform.forward, V);
        transform.localRotation = alignU * transform.localRotation;

        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            ProcesssZoom(Input.mouseScrollDelta.y);
        }
	}

    public void ProcesssZoom(float delta)
    {
        Vector3 v = LookAtPosition.localPosition - transform.localPosition;
        float dist = v.magnitude;
        dist += delta;
        transform.localPosition = LookAtPosition.localPosition - dist * v.normalized;
    }
}

