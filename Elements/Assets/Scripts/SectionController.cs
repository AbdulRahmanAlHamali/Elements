using UnityEngine;
using System.Collections;

public class SectionController : MonoBehaviour {

	public GameObject guidePrefab;
	public int id;
	public ArrayList guides;

	// Use this for initialization
	void Start () {

		guides = new ArrayList ();
		//create guides as children of the current section , and assign them IDs 
		//Position of guides can be deduced of their IDs and that of the section.
		int gid = 0;
		for(int j=0;j<10;j++){

			for(int i=0;i<9;i++){
				//create guide
				GameObject guide = Instantiate(guidePrefab,transform.position+new Vector3(i,j,0),Quaternion.identity) as GameObject;
				//make guide child of section so it moves with it
				guide.transform.parent = transform;
				GuideScript guideScript = guide.GetComponent<GuideScript>();
				//assign IDs and make guide free
				guideScript.id = gid;
				guideScript.taken = false;
				gid++;
				//add guide to array
				guides.Add(guide);

			}

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
