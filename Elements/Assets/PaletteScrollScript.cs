using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PaletteScrollScript : MonoBehaviour
{

	public RectTransform panel;
	public Button[] bttn;
	public RectTransform topSnap;
	public RectTransform botSnap;
	private float[] distance;
	private bool dragging = false;
	private int bttnDistance;
	private int minButtonNumTop, minButtonNumBot;
	public float snapSpeed=10;
	// Use this for initialization
	void Start ()
	{
	
		int bttnLength = bttn.Length;


		bttnDistance = (int)Mathf.Abs (bttn [1].GetComponent<RectTransform> ().anchoredPosition.y - bttn [0].GetComponent<RectTransform> ().anchoredPosition.y);

	}
	
	// Update is called once per frame
	void Update ()
	{
		float minDistanceTop = 0, minDistanceBot = 0;
		for (int i=0; i<bttn.Length; i++) {
			if (i == 0) {
				minDistanceBot = Mathf.Abs (botSnap.transform.position.y - bttn [i].transform.position.y);
				minDistanceTop = Mathf.Abs (topSnap.transform.position.y - bttn [i].transform.position.y);
				minButtonNumBot = i;
				minButtonNumTop = i;

			} else {
				float tmp = Mathf.Abs (botSnap.transform.position.y - bttn [i].transform.position.y);
				if (tmp < minDistanceBot) {

					minButtonNumBot = i;
					minDistanceBot = tmp;
				}

				tmp = Mathf.Abs (topSnap.transform.position.y - bttn [i].transform.position.y);
				if (tmp < minDistanceTop) {
					minButtonNumTop = i;
					minDistanceTop = tmp;

				}

			}
		

		
		}
		float newY = 0;
		if (!dragging) {
			if(minButtonNumTop==0){

				newY = Mathf.Lerp(panel.anchoredPosition.y,minButtonNumTop *  bttnDistance,Time.deltaTime*snapSpeed);
				Vector2 newPos = new Vector2(panel.anchoredPosition.x,newY);
				panel.anchoredPosition = newPos;



			}else if(minButtonNumBot==bttn.Length-1){
				print ("HERE");
				newY = Mathf.Lerp(panel.anchoredPosition.y,1 * - bttnDistance,Time.deltaTime*snapSpeed);
				Vector2 newPos = new Vector2(panel.anchoredPosition.x,newY);
				panel.anchoredPosition = newPos;


			}else{
			newY = Mathf.Lerp(panel.anchoredPosition.y,minButtonNumTop *  bttnDistance,Time.deltaTime*snapSpeed);
			Vector2 newPos = new Vector2(panel.anchoredPosition.x,newY);
			panel.anchoredPosition = newPos;
			//print (newY);
			}

		}
		if (dragging) {
			//print (minButtonNumTop);
		
		}

	}

	public void startDrag(){

		dragging = true;

	}
	public void stopDrag(){
		
		dragging = false;
		
	}

}
