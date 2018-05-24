using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Konrad.Weapons;
using Konrad.Utility;

public class SkryptGUI : MonoBehaviour {

	public Text score;
	public int scoreNumber;

	public GraczFPS gracz;

	public Slider slajder;

	public Spawn spawn;

	public Text fale;

	public Text naboje;

	void Start () {
		if (PlayerPrefs.HasKey ("Punkty"))
			scoreNumber = (int) PlayerPrefs.GetFloat ("Punkty");
		score.text = "0$";
		slajder.maxValue = gracz.startingHealth;
		fale.text = "";
		naboje.text = "";
	}

	void Update () {
		slajder.value = gracz.currentHealth;
		if (spawn.pauseTimer != spawn.dlugoscPrzerwy)
			fale.text = spawn.pauseTimer.ToString ("F2") + "s";
		else
			fale.text = (spawn.wave + 1) + "/" + (spawn.numberOfEnemies.Length);
		naboje.text = ListaBroni.listOfWeapons [ListaBroni.boughtItems [gracz.equipedWeaponNumber]].heldInMagizine
			+ "/" + ListaBroni.listOfWeapons [ListaBroni.boughtItems [gracz.equipedWeaponNumber]].magazineCapacity;
	}

	public void Scores (int number) {
		scoreNumber += number;
		score.text = scoreNumber + "$";
	}

	public void Replay () {
		Application.LoadLevel (Application.loadedLevelName);
	}

	public void Quit () {
		Application.LoadLevel ("Credits");
	}
}
