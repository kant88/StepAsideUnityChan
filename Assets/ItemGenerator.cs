using UnityEngine;
using System.Collections;

public class ItemGenerator : MonoBehaviour {
	//carPrefabを入れる
	public GameObject carPrefab;
	//coinPrefabを入れる
	public GameObject coinPrefab;
	//cornPrefabを入れる
	public GameObject conePrefab;
	public GameObject unitychan;
	//スタート地点
	private int startPos = -160;
	private int itemPos = -115;
	//アイテムが出る前方距離
	private int itemDis = 40;
	//ゴール地点
	//private int goalPos = 120;
	//アイテムを出すx方向の範囲
	private float posRange = 3.4f;
	//unitychanの現在位置(初期値は最初にアイテム生成をはじめる地点)
	private int posUnity = -145;


	// Use this for initialization
	void Start () {
		//一定の距離ごとにアイテムを生成
		for (int i = startPos; i <= itemPos; i+=15) {
			//どのアイテムを出すのかをランダムに設定
			int num = Random.Range (0, 10);
			if (num <= 1) {
				//コーンをx軸方向に一直線に生成
				for (float j = -1; j <= 1; j += 0.4f) {
					GameObject cone = Instantiate (conePrefab) as GameObject;
					cone.transform.position = new Vector3 (4 * j, cone.transform.position.y, i);
				}
			} else {

				//レーンごとにアイテムを生成
				for (int j = -1; j < 2; j++) {
					//アイテムの種類を決める
					int item = Random.Range (1, 11);
					//アイテムを置くZ座標のオフセットをランダムに設定
					int offsetZ = Random.Range(-5, 6);
					//60%コイン配置:30%車配置:10%何もなし
					if (1 <= item && item <= 6) {
						//コインを生成
						GameObject coin = Instantiate (coinPrefab) as GameObject;
						coin.transform.position = new Vector3 (posRange * j, coin.transform.position.y, i + offsetZ);
					} else if (7 <= item && item <= 9) {
						//車を生成
						GameObject car = Instantiate (carPrefab) as GameObject;
						car.transform.position = new Vector3 (posRange * j, car.transform.position.y, i + offsetZ);
					}
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		this.unitychan = GameObject.Find ("unitychan");
		GameObject cone = GameObject.FindGameObjectWithTag("TrafficConeTag");
		GameObject car = GameObject.FindGameObjectWithTag ("CarTag");
		GameObject coin = GameObject.FindGameObjectWithTag ("CoinTag");
	// 通り過ぎたアイテムを消す
		if (cone != null) {
			if (cone.transform.position.z < this.unitychan.transform.position.z - 5) {
				Destroy (cone.gameObject);
			}
		}
		if (car != null) {
			if (car.transform.position.z < this.unitychan.transform.position.z - 5) {					
				Destroy (car.gameObject);
			}
		}
		if (coin != null) {
			if (coin.transform.position.z < this.unitychan.transform.position.z - 5) {
				Destroy (coin.gameObject);
			}
		}
		//アイテムを動的に生成
		if (this.unitychan.transform.position.z > posUnity) {
			//位置更新（15進むごとにアイテム生成）
			posUnity += 15;
			int num = Random.Range (0, 10);
			if (num <= 1) {
				//コーンをx軸方向に一直線に生成
				for (float j = -1; j <= 1; j += 0.4f) {
					GameObject cone2 = Instantiate (conePrefab) as GameObject;
					cone2.transform.position = new Vector3 (4 * j, cone2.transform.position.y, posUnity + itemDis);
				}
			} else {

				//レーンごとにアイテムを生成
				for (int j = -1; j < 2; j++) {
					//アイテムの種類を決める
					int item = Random.Range (1, 11);
					//アイテムを置くZ座標のオフセットをランダムに設定
					int offsetZ = Random.Range(-5, 6);
					//60%コイン配置:30%車配置:10%何もなし
					if (1 <= item && item <= 6) {
						//コインを生成
						GameObject coin2 = Instantiate (coinPrefab) as GameObject;
						coin2.transform.position = new Vector3 (posRange * j, coin2.transform.position.y, posUnity + itemDis + offsetZ);
					} else if (7 <= item && item <= 9) {
						//車を生成
						GameObject car2 = Instantiate (carPrefab) as GameObject;
						car2.transform.position = new Vector3 (posRange * j, car2.transform.position.y, posUnity + itemDis + offsetZ);
					}
				}
			}
		}
	}
}