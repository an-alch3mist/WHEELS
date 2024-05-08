using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG_ForceAtPosition : MonoBehaviour
{

	bool _start = true;

	private void Update()
	{
		if(_start)
		{
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			_start = false;
		}

		if (Input.GetMouseButton(1))
		{
			StopAllCoroutines();
			StartCoroutine(STIMULATE());
			//
		}
	}


	[Range(0, 1f)]
	public float child_index_t = 0;
	public float force_mag = 100;


	public Transform ray_T;

	IEnumerator STIMULATE()
	{
		#region frame_rate
		QualitySettings.vSyncCount = 2;
		yield return null;
		yield return null;
		//
		#endregion

		Rigidbody rb = gameObject.GetComponent<Rigidbody>();

		Transform[] T_w = new Transform[transform.childCount];
		//
		for ( int i0 = 0; i0 < transform.childCount; i0 += 1)
		{
			T_w[i0] = transform.GetChild(i0);
		}

		rb.isKinematic = false;






		while (true)
		{

			#region RayCast check
			//Ray ray = new Ray(ray_T.position, new Vector3(0, -1, 0));
			//RaycastHit hit;


			//if (Physics.Raycast(ray, out hit, 10f, 1 << 10))
			//{
			//	//
			//	Debug.Log(" hit _ ");
			//	DRAW.LINE(ray.origin, hit.point);
			//	// rb.AddForceAtPosition(100 * -ray.direction, T_w[i0].position);
			//	//
			//} 
			#endregion




			if (Input.GetKey(KeyCode.Space))
			{
				int child_index = (int)(this.child_index_t * (T_w.Length - 1));
				Vector3 pos		= T_w[child_index].position,
						force	= this.force_mag * T_w[child_index].up; 
				//
				rb.AddForceAtPosition( force , pos);
			}

			//
			// DRAW.LINE(new Vector3(1, 1, 1), new Vector3(1, 0, 1));

			
			for(int i0 = 0; i0 < T_w.Length; i0 += 1)
			{
				Ray ray = new Ray(T_w[i0].position , -T_w[i0].up);
				RaycastHit hit;


				if(Physics.Raycast(ray, out hit, 1f, 1 << 10))
				{
					//
					Debug.Log(" hit _ " + i0);
					DRAW.LINE(ray.origin, hit.point);
					rb.AddForceAtPosition( 5 * -ray.direction , T_w[i0].position);

					//
				}

			}

		


			yield return null;
		}







		yield return null;
	}




	#region DRAW
	public static class DRAW
	{
		public static Color col = Color.red;
		public static float dt = Time.deltaTime;

		public static void LINE(Vector3 a , Vector3 b , float e = 1f/100)
		{
			Camera cam = Camera.main;
			Vector3 dir = cam.transform.right;

			Debug.DrawLine(a - dir * 1f / 100, b - dir * 1f / 100, DRAW.col, DRAW.dt);
			Debug.DrawLine(a + dir * 1f / 100, b + dir * 1f / 100, DRAW.col, DRAW.dt);
		}

	}
	#endregion

}
