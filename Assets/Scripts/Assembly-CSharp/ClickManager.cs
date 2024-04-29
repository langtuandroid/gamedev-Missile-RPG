using UnityEngine;

public class ClickManager : MonoBehaviour
{
	private void Awake()
	{
		GameCenter_RTM.ActionDataReceived += HandleActionDataReceived;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 1f;
			PTPGameController.instance.createGreenSphere(pos);
			ObjectCreatePackage objectCreatePackage = new ObjectCreatePackage(pos.x, pos.y);
			objectCreatePackage.send();
		}
	}

	private void HandleActionDataReceived(GK_Player player, byte[] data)
	{
		ByteBuffer byteBuffer = new ByteBuffer(data);
		int num = byteBuffer.readInt();
		int num2 = num;
		if (num2 == 1)
		{
			Debug.Log("Sphere pack");
			Vector3 pos = new Vector3(0f, 0f, 1f);
			pos.x = byteBuffer.readFloat();
			pos.y = byteBuffer.readFloat();
			PTPGameController.instance.createRedSphere(pos);
		}
		else
		{
			Debug.Log("Got pack wit id: " + num);
		}
	}
}
