using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTerrain : MonoBehaviour {
	public const float maxViewDst = 350;
	public Transform target;

	public static Vector2 viewerPosition;

	public int chunkSize;
	public GameObject obstacleSpawnerPrefab;

	int chunksVisibleInViewDst;

	List<Terrain> terrainChunkList;
	List<Terrain> invisibleChunk = new List<Terrain> ();


	void Start() {
		terrainChunkList = new List<Terrain> ();
		chunksVisibleInViewDst = Mathf.RoundToInt(maxViewDst / chunkSize);
	}

	void Update() {
		viewerPosition = new Vector2 (target.position.x, target.position.z);
		UpdateVisibleChunks ();

	}

	void UpdateVisibleChunks() {
		
		int currentChunkCoordX = Mathf.RoundToInt (viewerPosition.x / chunkSize);
		int currentChunkCoordY = Mathf.RoundToInt (viewerPosition.y / chunkSize);

		int terrainNo = 0;


		for (int i = 0; i < terrainChunkList.Count; i++) {
			Terrain t = terrainChunkList [i];

			// collect all the chunk which is not in visible range so that it can be updated ..
			if (!t.IsInViewDistance ()) {
				invisibleChunk.Add (t);
			}
		}


		for (int yOffset = -chunksVisibleInViewDst; yOffset <= chunksVisibleInViewDst; yOffset++) {
			for (int xOffset = -chunksVisibleInViewDst; xOffset <= chunksVisibleInViewDst; xOffset++) {
				Vector2 viewedChunkCoord = new Vector2 (currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

				if (terrainNo < terrainChunkList.Count) {  // if there is no terrain chunk we creat a new one 
					
					if (invisibleChunk.Count > 0) { // if there is some invisible chunk then we update it to new position else we create a new chunk
						
						if (!IsChunkAvailable (viewedChunkCoord * chunkSize)) {
							Terrain t = invisibleChunk [invisibleChunk.Count - 1];
							invisibleChunk.RemoveAt (invisibleChunk.Count - 1);
							t.SetPosition (viewedChunkCoord, chunkSize);
						}
					} else {
						if (!IsChunkAvailable (viewedChunkCoord * chunkSize)) {
							Terrain terrain = new Terrain (viewedChunkCoord, chunkSize, this.transform, obstacleSpawnerPrefab);
							terrainChunkList.Add (terrain);
						}
					}

				} else {
					Terrain terrain = new Terrain (viewedChunkCoord, chunkSize, this.transform,obstacleSpawnerPrefab);
					terrainChunkList.Add (terrain);
				}
				terrainNo++;
			}
		}
	}

	bool IsChunkAvailable (Vector3 pos) {
		pos = new Vector3(pos.x,0,pos.y);
		return Physics.Raycast (pos + Vector3.up * 30 , Vector3.down, int.MaxValue);
	}

	public class Terrain {

		GameObject meshObject;
		Vector2 position;
		Bounds bounds;

		public Terrain(Vector2 coord, int size, Transform parent,GameObject obstacleSpawnerGo) {
			position = coord * size;
			bounds = new Bounds(position,Vector2.one * size);
			Vector3 positionV3 = new Vector3(position.x,0,position.y);

			meshObject = Instantiate(obstacleSpawnerGo) as GameObject;
			meshObject.transform.position = positionV3;
			meshObject.transform.localScale = Vector3.one;
			meshObject.transform.parent = parent;
			SetVisible(false);
		}

		public bool IsInViewDistance() {
			float viewerDstFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance (viewerPosition));
			bool visible = viewerDstFromNearestEdge <= maxViewDst;
			SetVisible (visible);
			return visible;
		}

		public void SetVisible(bool visible) {
			meshObject.SetActive (visible);
		}

		public void SetPosition (Vector2 coord,int size) {
			position = coord * size;
			bounds = new Bounds(position,Vector2.one * size);
			meshObject.transform.position = new Vector3(position.x,0,position.y);
			IsInViewDistance ();
		}
	}
}


