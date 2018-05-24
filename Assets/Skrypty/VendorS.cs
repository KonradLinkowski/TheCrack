using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Konrad.Weapons;

public class VendorS : MonoBehaviour {
	
	public SkryptGUI punkty;

	public GameObject panel1;
	public GameObject panel2;
	public GameObject panel3;
	public GameObject panel4;

	public Text hajs;
	public Image image;

	public GameObject [] slider = new GameObject[9];
	public Text [] tekst = new Text[9];

	public Canvas shop;
	public Text openVendor;

	public static bool open;
	
	public Text[] teksty = new Text[6];

	public Transform t_Display;
	public Transform t_Player;

	public Canvas score;

	public Spawn spawn;
	public Text time;

	GameObject child1;
	GameObject child2;

	bool aktywny;

	Color32 zolty1;
	Color32 zolty2;
	Color32 bialy1;
	Color32 bialy2;

	int weaponNumber;

	int test, test1;

	void Start () {
		child1 = transform.FindChild ("Model").gameObject;
		child2 = transform.FindChild ("Sklep").gameObject;
		zolty1 = Color.yellow;
		zolty1.a = 100;
		zolty2 = Color.yellow;
		zolty2.a = 200;
		bialy1 = Color.white;
		bialy1.a = 100;
		bialy2 = Color.white;
		bialy2.a = 200;

		for (int i = 0; i < 6; i++) {
			teksty[i].text = "";
		}
		for (int i = 0; i < 9; i++) {
			tekst[i].text = "";
		}
		hajs.text = "Money: 0";
		time.text = "";
		tekst [0].text = tekst [3].text = tekst [6].text = "Accuracy";
		tekst [1].text = tekst [4].text = tekst [7].text = "Reload speed";
		tekst [2].text = tekst [5].text = tekst [8].text = "Attack speed";
	}

	void Update () {
		if (spawn.pauseTimer == spawn.dlugoscPrzerwy) {
			aktywny = false;
			shop.enabled = false;
			open = false;
			score.enabled = true;
		} else
			aktywny = true;

		if (!aktywny) {
			child1.SetActive (false);
			child2.SetActive (false);
			openVendor.text = "";
			return;
		} else {
			child1.SetActive (true);
			child2.SetActive (true);
		}

		if (Vector3.Distance (transform.position, t_Player.position) < 5) {
			shop.enabled = true;
			openVendor.text = "Naciśnij E, aby przejść do sklepu.";
			if (Input.GetKeyDown (KeyCode.E)) {
				open = true;
			} else if (Input.GetKeyDown (KeyCode.Escape)) {
				open = false;
			}
			panel1.SetActive (open);
			panel2.SetActive (open);
			panel3.SetActive (open);
			panel4.SetActive (open);
			image.enabled = open;
		} else {
			shop.enabled = false;
			openVendor.text = "";
			open = false;
		}
		hajs.text = "Money: " + punkty.scoreNumber.ToString ();
		t_Display.LookAt (t_Player);
		t_Display.RotateAround (t_Display.position, Vector3.up, 180);
		t_Display.rotation = Quaternion.Euler (new Vector3 (0, t_Display.rotation.eulerAngles.y, 0));
		if (open) {
			score.enabled = false;
			openVendor.enabled = false;
			VendorOpen ();
		} else {
			score.enabled = true;
			openVendor.enabled = true;
		}
	}

	void VendorOpen () {
		if (Input.GetKeyDown (KeyCode.W)) {
			if (weaponNumber > 0) {
				weaponNumber--;
			}
		} else if (Input.GetKeyDown (KeyCode.S)) {
			if (weaponNumber < ListaBroni.listOfWeapons.Length - 1) {
				weaponNumber++;
			}
		} else if (Input.GetKeyDown (KeyCode.E)) {
			if (ListaBroni.listOfWeapons [weaponNumber].kupiona != true) {
				Kup ();
			}
		}

		Draw ();
	}

	void Draw () {
		if (weaponNumber > 0 && weaponNumber <= ListaBroni.listOfWeapons.Length) {
			teksty [0].text = ListaBroni.listOfWeapons [weaponNumber - 1].nazwa;
			teksty [1].text = ListaBroni.listOfWeapons [weaponNumber - 1].cena.ToString ();
			tekst [0].enabled = true;
			tekst [1].enabled = true;
			tekst [2].enabled = true;
			slider [0].GetComponent <Slider> ().value = ListaBroni.listOfWeapons [weaponNumber - 1].celnosc / 10;
			slider [1].GetComponent <Slider> ().value = ListaBroni.listOfWeapons [weaponNumber - 1].szybkoscPrzeladowania / 10;
			slider [2].GetComponent <Slider> ().value = ListaBroni.listOfWeapons [weaponNumber - 1].szybkoscAtaku / 10;
			slider [0].SetActive (true);
			slider [1].SetActive (true);
			slider [2].SetActive (true);

			if (ListaBroni.listOfWeapons [weaponNumber - 1].kupiona) {
				panel1.GetComponent <Image> ().color = zolty1;
			} else {
				panel1.GetComponent <Image> ().color = bialy1;
			}
		} else {
			teksty [0].text = "";
			teksty [1].text = "";
			tekst [0].enabled = false;
			tekst [1].enabled = false;
			tekst [2].enabled = false;
			slider [0].SetActive (false);
			slider [1].SetActive (false);
			slider [2].SetActive (false);
			panel1.GetComponent <Image> ().color = bialy1;
		}

		teksty [2].text = ListaBroni.listOfWeapons [weaponNumber].nazwa;
		teksty [3].text = ListaBroni.listOfWeapons [weaponNumber].cena.ToString ();
		tekst [3].enabled = true;
		tekst [4].enabled = true;
		tekst [5].enabled = true;
		slider [3].GetComponent <Slider> ().value = ListaBroni.listOfWeapons [weaponNumber].celnosc / 10;
		slider [4].GetComponent <Slider> ().value = ListaBroni.listOfWeapons [weaponNumber].szybkoscPrzeladowania / 10;
		slider [5].GetComponent <Slider> ().value = ListaBroni.listOfWeapons [weaponNumber].szybkoscAtaku / 10;
		slider [3].SetActive (true);
		slider [4].SetActive (true);
		slider [5].SetActive (true);

		if (ListaBroni.listOfWeapons [weaponNumber].kupiona) {
			panel2.GetComponent <Image> ().color = zolty2;
		} else {
			panel2.GetComponent <Image> ().color = bialy2;
		}

		if (weaponNumber >= 0 && weaponNumber < ListaBroni.listOfWeapons.Length - 1) {
			teksty [4].text = ListaBroni.listOfWeapons [weaponNumber + 1].nazwa;
			teksty [5].text = ListaBroni.listOfWeapons [weaponNumber + 1].cena.ToString ();
			tekst [6].enabled = true;
			tekst [7].enabled = true;
			tekst [8].enabled = true;
			slider [6].GetComponent <Slider> ().value = ListaBroni.listOfWeapons [weaponNumber + 1].celnosc / 10;
			slider [7].GetComponent <Slider> ().value = ListaBroni.listOfWeapons [weaponNumber + 1].szybkoscPrzeladowania / 10;
			slider [8].GetComponent <Slider> ().value = ListaBroni.listOfWeapons [weaponNumber + 1].szybkoscAtaku / 10;
			slider [6].SetActive (true);
			slider [7].SetActive (true);
			slider [8].SetActive (true);

			if (ListaBroni.listOfWeapons [weaponNumber + 1].kupiona) {
				panel3.GetComponent <Image> ().color = zolty1;
			} else {
				panel3.GetComponent <Image> ().color = bialy1;
			}

		} else {
			teksty [4].text = "";
			teksty [5].text = "";
			tekst [6].enabled = false;
			tekst [7].enabled = false;
			tekst [8].enabled = false;
			slider [6].SetActive (false);
			slider [7].SetActive (false);
			slider [8].SetActive (false);
			panel3.GetComponent <Image> ().color = bialy1;
		}
		time.text = "Czas: " + (int)spawn.pauseTimer + "s";
	}

	void Kup () {
		if (ListaBroni.listOfWeapons [weaponNumber].cena <= punkty.scoreNumber) {
			ListaBroni.listOfWeapons [weaponNumber].kupiona = true;
			punkty.scoreNumber -= ListaBroni.listOfWeapons [weaponNumber].cena;
			ListaBroni.listOfWeapons [weaponNumber].obrazek.enabled = true;
			ListaBroni.boughtItems.Add (weaponNumber);
			ListaBroni.boughtItems.Sort ();
		}
	}
}
