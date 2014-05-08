#pragma strict

var doorRightT : ParticleSystem;
var doorLeftT : ParticleSystem;
var endRightT : ParticleSystem;
var endLeftT : ParticleSystem;

function Start () {

}

function Update () {
	if (Input.GetAxis ("Horizontal") > 0) { //checking for right arrow
		doorRightT.Play();
		endRightT.Play();
	}
	if (Input.GetAxis ("Horizontal") < 0) { //checking for left arrow

	}
	if (Input.GetAxis ("Horizontal") == 0) { //checking if no Horiz keys are down
		doorRightT.Stop();
		endRightT.Stop();
	}

	if(Input.GetAxis("Vertical") > 0) { //checking for up arrow

	}
	if(Input.GetAxis("Vertical") < 0) { //checking for down arrow

	}
	if(Input.GetAxis("Vertical") == 0) { //checking if no Vert keys are down
			
	}
}