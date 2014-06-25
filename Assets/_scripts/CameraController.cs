using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Transform Target;
	public float XSpeed;
	public float YSpeed;
	public float ZoomSpeed;

	public float YMinLimit;
	public float YMaxLimit;

	public float MinDistance;
	public float MaxDistance;

	private Transform myTransform;
	private float distance;
	private float x;
	private float y;
	private float prevPinch;
	private Vector2 prevTouchPos;

	private void Awake ()
	{
		// lock fps to prevent battery wasting
		Application.targetFrameRate = 60;

		myTransform = transform;
		distance = MaxDistance;
	}

	private void Start ()
	{
		Vector3 angles = myTransform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}

	private void LateUpdate ()
	{
		if (Target)
		{
			#region MOUSE_INPUT
			if (Application.isEditor)
			{
				if (Input.GetMouseButton(1))
				{
					Screen.lockCursor = true;
					x += Input.GetAxis("Mouse X") * XSpeed * distance;
					y -= Input.GetAxis("Mouse Y") * YSpeed;
				}
				else Screen.lockCursor = false;

				distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed, MinDistance, MaxDistance);
			}
			#endregion

			#region TOUCH_INPUT
			if (!Application.isEditor)
			{
				// swipe moving
				if (Input.touchCount == 1)
				{
					Touch touch = Input.GetTouch(0);
					if (touch.phase == TouchPhase.Moved)
					{
						if (prevTouchPos == Vector2.zero) prevTouchPos = touch.position;
						x += (touch.position.x - prevTouchPos.x) * XSpeed / 10 * distance;
						y -= (touch.position.y - prevTouchPos.y) * YSpeed / 10;
						prevTouchPos = touch.position;
					}
					else prevTouchPos = Vector2.zero;
				}

				// pinch zooming
				if (Input.touchCount == 2)
				{
					Touch touch1 = Input.GetTouch(0), touch2 = Input.GetTouch(1);
					if (touch1.phase == TouchPhase.Moved && touch2.phase == TouchPhase.Moved)
					{
						float pinch = Vector2.Distance(touch1.position, touch2.position);
						if (prevPinch == 0) prevPinch = pinch;
						distance -= (pinch - prevPinch) * ZoomSpeed / 100;
						distance = Mathf.Clamp(distance, MinDistance, MaxDistance);
						prevPinch = pinch;
					}
					else prevPinch = 0;
				}
			}
			#endregion

			y = ClampAngle(y, YMinLimit, YMaxLimit);
			Quaternion rotation = Quaternion.Euler(y, x, 0);

			Vector3 negDistance = new Vector3(0, 0, -distance);
			Vector3 position = rotation * negDistance + Target.position;

			myTransform.rotation = rotation;
			myTransform.position = position;
		}
	}

	private float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F) angle += 360F;
		if (angle > 360F) angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}
}