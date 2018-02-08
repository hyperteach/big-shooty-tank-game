using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EaseType {
	In,
	Out,
	InOut
}
public enum EaseFunc {
	Quad,
	Quart,
	Quint,
	Cos,
	Curve,
	Break,
	Log,
	Power,
	Sqrt,
	Terror
}

public class MoveLeftAndRight : MonoBehaviour {

	public float time;
	public EaseType easeType;
	public EaseFunc easeFunc;
	public float parameter = 10;

	public AnimationCurve curve;

	float start;
	float end;
	float current;
	float t;

	//float velocity;


	// Use this for initialization
	void Start () {
		t = 0;
		start = transform.position.x;
		end = -transform.position.x;
		current = start;
	}
	
	// Update is called once per frame
	void Update () {
		// THE EASY WAYS
		//current = Mathf.Lerp (current, end, Time.deltaTime * 2);
		//current = lerp (current, end, Time.deltaTime * 2);
		//current = Mathf.SmoothDamp (current, end, ref velocity, 1);


		//t = Mathf.Clamp (t + Time.deltaTime, 0, 1);
		t = Mathf.Clamp01 (t + Time.deltaTime);
		current = lerp (start, end, ease(t*time));


		Apply ();
	}

	void Apply(){
		transform.position = new Vector3 (
			current,
			transform.position.y,
			transform.position.z
		);
	}

	float ease(float t){
		switch (easeType) {

		default:
		case EaseType.In:
			return doEaseFunc(t);

		case EaseType.Out:
			return 1 - doEaseFunc(1 - t);

		case EaseType.InOut:
			if (t < 0.5) {
				return 0.5f * doEaseFunc(t * 2);
			} else {
				return 1 + 0.5f * -doEaseFunc((1 - t)*2); 
			}

		}
	}

	float doEaseFunc (float t){
		switch (easeFunc) {

		default:
		case EaseFunc.Quad:
			return quad (t);

		case EaseFunc.Quart:
			return quart (t);

		case EaseFunc.Quint:
			return quint (t);

		case EaseFunc.Cos:
			return cosEase (t);

		case EaseFunc.Curve:
			return curveEase (t);

		case EaseFunc.Break:
			return breakEase (t);

		case EaseFunc.Log:
			return logInv (t, parameter);

		case EaseFunc.Power:
			return powerEase(t, parameter);

		case EaseFunc.Sqrt:
			return sqrtEase(t);

		case EaseFunc.Terror:
			return terrorEase(t);



		}
			
	}

	float quad(float t){
		return t * t;
	}
	float quart(float t){
		return t * t * t;
	}
	float quint(float t){
		return t * t * t * t;
	}
	float powerEase(float t, float p){
		return Mathf.Pow(t, p);
	}
	float cosEase(float t){
		return 1 - Mathf.Cos (t * Mathf.PI / 2);
	}

	float curveEase(float t){
		return curve.Evaluate (t);
	}
	float breakEase(float t){
		// x<.5 ?  16*x^5   :    1+3*(--x)*x^4*cos(x)
		if (t<0.5f){
			// 16*x^5
			return Mathf.Pow(16*t, 5);
		} else {
			// 1+3*(x-1)*((x-1)^4)*cos(x-1)
			return 1+3*(t-1)*Mathf.Pow(t-1,4)*Mathf.Cos(t-1);
		}
	}
	float logInv(float t, float p){
		// log(x+1)*3.321928095
		return Mathf.Log(t+1, p)/Mathf.Log(2, p);
	}
	float sqrtEase (float t) {
        return -Mathf.Sqrt(1-t*t)+1;
    }
    float terrorEase(float t){
    	// (((tan(x^3))/sin(x)4-sin(x)4)/3)/1.25
    	return (((Mathf.Tan(Mathf.Pow(t,3)))/Mathf.Sin(t)*4-Mathf.Sin(t)*4)/3)/1.348f;
    }


	float lerp(float a, float b, float t){
		// return a + (b - a) * t;
		return a * (1 - t) + b * t;
	}

	public void Swap(){
		t = 0;
		float store = start;
		start = end;
		end = store;
	}

}
