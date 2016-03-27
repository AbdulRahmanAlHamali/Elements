using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;





public class mapDesignController : MonoBehaviour {
	
	public GameObject mainCamera;
	public GameObject sectionObj,section1,section2;
	public GameObject grid;
	ArrayList sections;
	public Button addSectionBtn;
	public Button showGridButton;
	public Button slideLeftBtn;
	public Button slideRightBtn;
	public GameObject sectionSpawner;
	bool slideSections = false;
	public float speed = 1;

	bool moveCamera = false;

	//already have 2 sections 
	private int sid = 2;

	//position to where the camera needs to go
	private Vector3 newCameraPos;
	// Use this for initialization
	void Start () {


		sections = new ArrayList ();
		sections.Add (section1);
		sections.Add (section2);



		addSectionBtn.onClick.AddListener (() => {
			//instantiate a section at the position of the spawner
			GameObject sectionClone = Instantiate(sectionObj,sectionSpawner.transform.position,Quaternion.identity) as GameObject;
			//make the new section a child of the map
			sectionClone.transform.parent = transform;
			//add section to the array
			sections.Add(sectionClone);

			//assign new ID to the section
			SectionController sectionController = sectionClone.GetComponent<SectionController>();
			sectionController.id = sid;
			sid++;

			//calculate the position to where camera needs to go
			newCameraPos = new Vector3(sectionClone.transform.position.x-0.5f,0,-10);
			moveCamera = true;
			//move the spawn to next spawn location
			sectionSpawner.transform.position+= new Vector3(9,0,0);


		});

		showGridButton.onClick.AddListener (() => {

			SpriteRenderer renderer =  grid.GetComponent<SpriteRenderer>();
			if(renderer.enabled){
				renderer.enabled=false;
			}else{

				renderer.enabled=true;
			}

		});


		slideLeftBtn.onClick.AddListener (() => {

			newCameraPos = mainCamera.transform.position + new Vector3(-9,0,-10);
			moveCamera = true;


		});

		slideRightBtn.onClick.AddListener (() => {
			
			newCameraPos = mainCamera.transform.position + new Vector3(9,0,-10);
			moveCamera = true;
			
			
		});
		
	}
	
	// Update is called once per frame
	void Update () {




		
	}

	void FixedUpdate(){

		    // move the camera to new position at defined speed 
		if (moveCamera) {
			this.mainCamera.transform.position = Vector3.MoveTowards (this.mainCamera.transform.position, newCameraPos, Time.deltaTime * speed);
			if(mainCamera.transform.position.Equals(newCameraPos)){
				moveCamera=false;
			}

		}


	}

}
