using UnityEngine;
using System.Collections;

namespace AttackScript {

	public class Attacks {
		Sprite mPlayerSprite;
		Transform mPlayerTransform;
		GameObject mPlayerProjectile;
		float mBulletSpeed;

		public Attacks(Sprite playerSprite, Transform playerTransform, GameObject playerProjectile, float bulletSpeed){
			mPlayerSprite = playerSprite;
			mPlayerTransform = playerTransform;
			mPlayerProjectile = playerProjectile;
			mBulletSpeed = bulletSpeed;
		}

		public void Shoot(int powerLevel){
			switch (powerLevel){
			case 1:
				Shoot1();
				break;
			case 2:
				Shoot2();
				break;
			case 3:
				Shoot3();
				break;
			}
		}

		public void Shoot1(Vector3 extraOffset, float extraRotation){
			Vector3 spawnOffset = new Vector3(0, mPlayerSprite.bounds.size.y / 4f, 0);
			Vector3 unAdjustedOffset = mPlayerTransform.rotation * spawnOffset;
			spawnOffset = mPlayerTransform.rotation * (spawnOffset + extraOffset);
			Vector3 spawnPoint = mPlayerTransform.position - spawnOffset;
			GameObject newBullet = (GameObject)Object.Instantiate(mPlayerProjectile, spawnPoint, mPlayerTransform.localRotation);
			
			Vector2 bulletVelocity = new Vector2(unAdjustedOffset.x, unAdjustedOffset.y);
			bulletVelocity = Quaternion.Euler(0, 0, extraRotation) * bulletVelocity.normalized * mBulletSpeed;
			newBullet.GetComponent<Rigidbody2D>().velocity = -bulletVelocity;
		}

		public void Shoot1(){
			Shoot1(Vector3.zero, 0);
		}

		public void Shoot2(){
			Vector3 offset1 = new Vector3(-1f, 0, 0);
			Vector3 offset2 = new Vector3(1f, 0, 0);
			Shoot1(offset1, 0);
			Shoot1(offset2, 0);
		}
		
		public void Shoot3(){
			Vector3 offset1 = new Vector3(-1f, 0, 0);
			Vector3 offset2 = new Vector3(1f, 0, 0);
			Vector3 offset3 = new Vector3(0, 0, 0);
			Shoot1(offset1, 10f);
			Shoot1(offset2, -10f);
			Shoot1(offset3, 0);
		}
	}

}