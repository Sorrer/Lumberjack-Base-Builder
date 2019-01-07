using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform Target;


	public float TargetClamp;

	public float MinRadius;
	public float MinRadiusClamp;

	//% of  distance to travel towards completion or clamp
	public float OutsideAccelPercentage;
	public float InnerAccelPercentage;



    void Start()
    {
        
    }
	
    void FixedUpdate()
    {
		Vector3 targetPos = Target.position;
		

		Vector3 curPos = this.transform.position;
		float distanceTo = Vector3.Distance(targetPos, curPos);
		Vector3 offset = curPos - targetPos;
		//Clamp
		float distanceToMin = distanceTo - MinRadius;
		
			//If distance is negative, than ignore clamp for min else, otherwise
		if(distanceToMin < 0) {
			if(distanceTo <= TargetClamp) {
				//Should be clamped to target pos
				this.transform.position = targetPos;
				return;
			}
		} else {
			if(distanceToMin <= MinRadiusClamp) {
				//Should be clamped to minradius
				this.transform.position = Vector3.ClampMagnitude(offset, MinRadius);
			}
		}

		//MoveTowards

		Vector3 movement = new Vector3();
		
		

	}

	
}
