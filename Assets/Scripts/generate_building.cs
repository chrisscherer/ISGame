using UnityEngine;
using System.Collections;

public class generate_building : MonoBehaviour {

	// Use this for initialization
	void Start () {
//		make_structure(5, 5, 6, new Vector3(0,0,0));
//		make_structure(7, 7, 3, new Vector3(15,15,0));
//		make_structure(3, 3, 9, new Vector3(15,-15,0));
//		make_structure(5, 5, 15, new Vector3(-15,15,0));
//		make_structure(7, 5, 30, new Vector3(-15,-15,0));
//		make_wall_segment(0, new Vector3(0,0,0));
//		make_structure2(8,12,0,8, new Vector3(0,0,2));
		make_neighborhood(4, 16, 1, new Vector3(0,0,2));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void make_neighborhood (int min, int max, int size, Vector3 mp) {
		for(int x = -size; x <= size; x++){
			for(int y = -size; y <= size; y++){
				make_structure2((Random.Range(min,max / 2) * 2),(Random.Range(min,max / 2) * 2), 0, Random.Range(min,max), new Vector3(mp.x +(float)( x * max * 1.25), mp.y + (float)( y * max * 1.25), mp.z));
			}
		}
	}

	void make_wall_segment (int dimension, Vector3 mp, Vector3 rotation){
		int check = Random.Range(0,4);
		if(check > 0){
			GameObject wall_section = (GameObject)Instantiate(Resources.Load("wall_square"));
			wall_section.transform.Rotate(rotation);
			wall_section.transform.position = mp;
			wall_section.transform.localScale += new Vector3(dimension, 0, 0);
		}
		else
		{
			GameObject wall_section = (GameObject)Instantiate(Resources.Load("wall_square"));
			wall_section.transform.Rotate(rotation);
			wall_section.transform.position = mp;
			wall_section.transform.localScale += new Vector3(dimension, 0, 0);
		}
	}

	void make_floor(int length, int width, int h, int hole,  Vector3 mp){
		for(int x = -length; x<= length; x++){
			for(int y = -width; y<= width; y++){
				if(y == hole && x == hole && h != 0){

				}
				else{
					GameObject wall_section = (GameObject)Instantiate(Resources.Load("floor_square"));
					wall_section.transform.position = new Vector3(mp.x + x, mp.z + (float)(h - .5), mp.y + y);
					wall_section.transform.Rotate(Vector3.right * 90);
					wall_section.tag = "floor_square";
				}
			}
		}
	}

	void make_structure2 (int length, int width, int start_height, int height, Vector3 mp){
		for(int h = start_height;h <= height;h++){
			make_wall_segment(length, new Vector3(mp.x, mp.z + h, mp.y - (float)(width * .5 + .5)), Vector3.zero);
			make_wall_segment(length, new Vector3(mp.x, mp.z + h, mp.y + (float)(width * .5 + .5)), Vector3.zero);
          	make_wall_segment(width, new Vector3(mp.x + (float)(length * .5 + .5), mp.z + h, mp.y), (Vector3.up * 90));
			make_wall_segment(width, new Vector3(mp.x - (float)(length * .5 + .5), mp.z + h, mp.y), (Vector3.up * 90));
			if(h % 4 == 0)
			{
				make_floor(length - (int)(length * .5), width - (int)(width * .5), h, h % 2, mp);
			}
		}
	}

	void make_structure (int length, int width, int height, Vector3 mp){

		for(int h = 0; h <= height;h++){
			for(int l = -length; l <= length; l++){
				GameObject wall_section = (GameObject)Instantiate(Resources.Load("wall_square"));
				wall_section.transform.position = new Vector3(mp.x + l, mp.z + h, mp.y - (float)(width + .5));
				wall_section.tag = "wall_square";
				GameObject wall_section2 = (GameObject)Instantiate(Resources.Load("wall_square"));
				wall_section2.transform.position = new Vector3(mp.x + l, mp.z + h, mp.y + (float)(width + .5));
				wall_section2.tag = "wall_square";
			}
			for(int w = -width; w <= width; w++){
				GameObject wall_section = (GameObject)Instantiate(Resources.Load("wall_square"));
				wall_section.transform.Rotate(Vector3.up * 90);
				wall_section.transform.position = new Vector3(mp.x + (float)(length + .5), mp.z + h, mp.y + w);
				wall_section.tag = "wall_square";
				GameObject wall_section2 = (GameObject)Instantiate(Resources.Load("wall_square"));
				wall_section2.transform.Rotate(Vector3.up * 90);
				wall_section2.transform.position = new Vector3(mp.x - (float)(length + .5), mp.z + h, mp.y + w);
				wall_section2.tag = "wall_square";
			}
			if(h % 3 == 0){
				make_floor(length, width, h, h % 2, mp);
			}
		}
	}
}
