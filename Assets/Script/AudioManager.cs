using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	
	public AudioClip oscar_pew, oscar_hit, oscar_hurt, oscar_defeat, oscar_victory;
	public AudioClip toki_pew, toki_hit, toki_hurt, toki_defeat, toki_victory;

	//AudioSource audio;

	// Use this for initialization
	void Start () {
		//audio = GetComponent<AudioSource>();

	}

	public void redFireSfx(Vector3 position){
		//audio.playcl(oscar_pew);
		AudioSource.PlayClipAtPoint(oscar_pew, position);
	}

	public void blueFireSfx(Vector3 position){
		//audio.PlayOneShot(toki_pew);
		AudioSource.PlayClipAtPoint(toki_pew, position);
	}
	
	public void redHitBlueSfx(Vector3 position){
		//if (Random.Range(0f, 1f) > 0.5f){
			//audio.PlayOneShot(oscar_hit);
		//	AudioSource.PlayClipAtPoint(oscar_hit, position);
		//}
		//else{
			//audio.PlayOneShot(toki_hurt);
			AudioSource.PlayClipAtPoint(toki_hurt, position);
		//}
	}

	public void blueHitRedSfx(Vector3 position){
		//if (Random.Range(0f, 1f) > 0.5f){
			//audio.PlayOneShot(oscar_hurt);
			AudioSource.PlayClipAtPoint(oscar_hurt, position);			
		//}
		//else{
			//audio.PlayOneShot(toki_hit);
			//AudioSource.PlayClipAtPoint(toki_hit, position);
		//}
	}

	public void redWinSfx(Vector3 position){
		//if (Random.Range(0f, 1f) > 0.5f){
			//audio.PlayOneShot(oscar_victory);
			AudioSource.PlayClipAtPoint(oscar_victory, position);
		//}
		//else{
			//audio.PlayOneShot(toki_defeat);
			//AudioSource.PlayClipAtPoint(toki_defeat, position);
		//}
	}

	public void blueWinSfx(Vector3 position){
		//if (Random.Range(0f, 1f) > 0.5f){
			//audio.PlayOneShot(oscar_defeat);
		//	AudioSource.PlayClipAtPoint(oscar_defeat, position);
		//}
		//else{
			//audio.PlayOneShot(toki_victory);
			AudioSource.PlayClipAtPoint(toki_victory, position);
		//}
	}


}
