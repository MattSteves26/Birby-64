// Smooth Follow from Standard Assets
// Converted to C# because I fucking hate UnityScript and it's inexistant C# interoperability
// If you have C# code and you want to edit SmoothFollow's vars ingame, use this instead.
using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{

	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 10.0f;
	// the height we want the camera to be above the target
	public float height = 4.0f;
	// How much we 
	public float heightDamping = 1.0f;
	public float rotationDamping = 1.0f;
    
    public float maxHeight = 7.0f;
    public float minHeight = 0.0f;
	[SerializeField] bool smoothLookAtTargetX = true;
	[SerializeField] bool smoothLookAtTargetY = false;
	[SerializeField] bool cameraChecksIsGrounded  = true;
	

	// Place the script in the Camera-Control group in the component menu
	[AddComponentMenu("Camera-Control/Smooth Follow")]

	void LateUpdate()
	{
		// Early out if we don't have a target
		if (!target) return;

		// Calculate the current rotation angles
		float wantedRotationAngle = target.eulerAngles.y;
		float wantedHeight = target.position.y + height;

		float currentRotationAngleY = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
		//currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp the height
		//I changed this so it only moves the camera height when the player is on the ground
		//if you don't want this, just turn off camerChecksIsGrounded and set heightDamping to 100
		if(cameraChecksIsGrounded){
			if(target.GetComponent<playerMovement>().isGrounded){
				currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
			}
		}
		else{
			currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		}
		
		
		
        // Clamp height to specifications
        currentHeight = Mathf.Clamp(currentHeight, minHeight, maxHeight);

		if(smoothLookAtTargetY){
				currentRotationAngleY = transform.eulerAngles.y;
				float wantedRotationAngleY = target.eulerAngles.y - 90f;
				Debug.Log("Wanted Rotation: " + wantedRotationAngleY);
				currentRotationAngleY = Mathf.LerpAngle(currentRotationAngleY, wantedRotationAngleY, rotationDamping * Time.deltaTime);
				Quaternion currentRotationY = Quaternion.Euler(transform.eulerAngles.x, currentRotationAngleY, transform.eulerAngles.z);
				Debug.Log("CurRotY: " + currentRotationY);
				transform.rotation = currentRotationY;
		}
		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngleY, 0);

		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		
		
		//Smooth Look At Target (Camera angle)
		//This rotates the camera with a set speed to look at the player when their up high or falling.
		//It rotates the camera back to zero at a set speed when the player is low
		//just turn off smoothLookAtTarget if you don't want to use this ~Dévon
		if(smoothLookAtTargetX){
			if(wantedHeight >= maxHeight || target.gameObject.GetComponent<Rigidbody>().velocity.y < -10){
				Vector3 relativePos = new Vector3(target.position.x, currentHeight, target.position.z) - transform.position;
				Quaternion wantedRotationAngleX = Quaternion.LookRotation(relativePos);
				wantedRotationAngleX = new Quaternion(wantedRotationAngleX.x * -100.0f,wantedRotationAngleX.y,wantedRotationAngleX.z,wantedRotationAngleX.w);
				float currentRotationAngleX = transform.eulerAngles.x;
				currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, wantedRotationAngleX.x, rotationDamping * Time.deltaTime);
				Quaternion currentRotationX = Quaternion.Euler(currentRotationAngleX, transform.eulerAngles.y, transform.eulerAngles.z);
				transform.rotation = currentRotationX;			
			}
			else{
				float currentRotationAngleX = transform.eulerAngles.x;
				currentRotationAngleX = Mathf.LerpAngle(currentRotationAngleX, 0, rotationDamping * Time.deltaTime);
				Quaternion currentRotationX = Quaternion.Euler(currentRotationAngleX, transform.eulerAngles.y, transform.eulerAngles.z);
				transform.rotation = currentRotationX;	
			}
		}
		// Set the height of the camera
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

	}
}