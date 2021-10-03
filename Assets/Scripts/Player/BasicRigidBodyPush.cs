using UnityEngine;

//Unity Starter Assets script (edited)
public class BasicRigidBodyPush : MonoBehaviour
{
	[SerializeField] private float _strength = 1.1f;
	[SerializeField] private LayerMask pushLayers;
	
	public bool canPush;

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (canPush)
		{
			PushRigidBodies(hit);
		}
	}

	private void PushRigidBodies(ControllerColliderHit hit)
	{
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic) return;

		var bodyLayerMask = 1 << body.gameObject.layer;

		if ((bodyLayerMask & pushLayers.value) == 0) return;

		if (hit.moveDirection.y < -0.3f) return;

		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

		body.AddForce(pushDir * _strength, ForceMode.Impulse);
	}
}