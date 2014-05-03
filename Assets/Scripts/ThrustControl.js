#pragma strict

public var thrust : float = 20f;
private var u = 0;
private var h : float;
private var v : float;

/*function FixedUpdate() {
	rigidbody.AddForce(Vector3.right * h * hThrust);
   	rigidbody.AddForce(Vector3.forward * v * hThrust);
   	rigidbody.AddForce(Vector3.up * u * vThrust);
}*/

function Update() {
	//if(Input.GetAxis("Horizontal") != 0) h = Input.GetAxis("Horizontal");
	//else h = 0;
	//if(Input.GetAxis("Vertical") != 0) v = Input.GetAxis("Vertical");
	//else v = 0;
	if(Input.GetKey("space")) u = 1;
	else u = 0;
}

function FixedUpdate() {
	h = Input.GetAxis("Horizontal");
	v = Input.GetAxis("Vertical");
	var movement : Vector3 = new Vector3(h, u, v);
	rigidbody.AddForce(movement * thrust * Time.deltaTime);
}