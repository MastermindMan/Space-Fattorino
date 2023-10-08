using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	[Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
	[Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
	[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;
	[SerializeField] Transform fowardPointer;

	public Vector2 cameraRotation = Vector2.zero;

	private float PlayerRotationY => Player.Instance.transform.rotation.eulerAngles.y;
	public Transform FowardPointer => fowardPointer;

	public float Sensitivity
	{
		get { return sensitivity; }
		set { sensitivity = value; }
	}

    private void Start()
    {
        SubscribeToEvents();
    }

	//public float playerInclinationAngle;
	public Quaternion yQuat;
	private void RotateCamera(Vector2 rot)
    {
		cameraRotation.x += rot.x * sensitivity;
		cameraRotation.y += rot.y * sensitivity;
		//playerInclinationAngle = Vector3.SignedAngle(Player.Instance.transform.up, Vector3.up, Vector3.Cross(Player.Instance.transform.up, Vector3.up));
		cameraRotation.y = Mathf.Clamp(cameraRotation.y, -yRotationLimit, yRotationLimit);
		var xQuat = Quaternion.AngleAxis(cameraRotation.x, Vector3.up);
		yQuat = Quaternion.AngleAxis(cameraRotation.y, Vector3.left);
		//yQuat = MyMathStuff.ClampRotation(yQuat, yRotationLimit, 360, 360);

		transform.localRotation = yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);
		fowardPointer.localRotation = xQuat;
	}

	public void SubscribeToEvents()
	{
		EventsManager.MouseMovement.AddListener(RotateCamera);
	}
	public void UnsubscribeToEvents()
	{
		EventsManager.MouseMovement.RemoveListener(RotateCamera);
	}

}
