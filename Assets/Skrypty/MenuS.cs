using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuS : MonoBehaviour {

	Canvas menu;
	Image imedz;
	Color kolor;
	bool zmiana;
	GraczFPS gracz;
	public Slider slajd;
	

	void Start () {
		menu = GetComponent <Canvas> ();
		imedz = menu.GetComponent <Image> ();
		kolor = Color.black;
		kolor.a = 0;
		gracz = GameObject.FindGameObjectWithTag ("Player").GetComponent <GraczFPS> ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape) && !VendorS.open) { 
			menu.enabled = !menu.enabled;
			Time.timeScale = Time.timeScale == 0 ? 1 : 0;
			zmiana = !zmiana;
			gracz.playing = !gracz.playing;
		}
		if (zmiana) {
#if !UNITY_EDITOR
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
#endif
			kolor.a = Mathf.Lerp (kolor.a, 0.75f, 10 * Time.deltaTime);
			imedz.color = kolor;
		} else {
#if !UNITY_EDITOR
			Cursor.lockState = CursorLockMode.Confined;
			Cursor.visible = false;
#endif
			kolor.a = 0;
			imedz.color = kolor;
		}
		AudioListener.volume = slajd.value;
		//DEVELOPERSKIE


	}
	public void Quit () {
		Application.LoadLevel ("Credits");
	}
	public void Resume () {
		menu.enabled = !menu.enabled;
		Time.timeScale = Time.timeScale == 0 ? 1 : 0;
		zmiana = !zmiana;
		gracz.playing = !gracz.playing;
	}
}
