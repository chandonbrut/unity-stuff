using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemController : MonoBehaviour {

    public string LayerName = "Particles";

	void Start () {

        GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = LayerName;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
