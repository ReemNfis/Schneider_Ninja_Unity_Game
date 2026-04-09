

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;


public class SkinManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> skin = new List<Sprite>();
    private int selectedSkin = 0;
    public GameObject playerskin;

    void Start()
    {

    }

    void Update()
    {

    }


    public void NextOption()
    {
        selectedSkin = selectedSkin + 1;
        if (selectedSkin == skin.Count)
        {
            selectedSkin = 0;
        }
        sr.sprite = skin[selectedSkin];
    }

    public void BackOption()
    {
        selectedSkin = selectedSkin - 1;
        if (selectedSkin > 0)
        {
            selectedSkin = skin.Count - 1;
        }
        sr.sprite = skin[selectedSkin];
    }

    public void GamePlay()
    {

        PrefabUtility.SaveAsPrefabAsset(playerskin, "Assest.Witch.prefab");
        SceneManager.LoadScene("The Flappy Witch");

    }

    public GameObject[] characters; // الشخصيات المتاحة
    private int selectedCharacter = 0;

    public void SelectCharacter(int index)
    {
        selectedCharacter = selectedSkin;
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        // هنا يمكنك إضافة منطق لتحميل الشخصية
    }

    public void LoadCharacter()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        // تحميل الشخصية بناءً على index
        Debug.Log("Loading character " + selectedCharacter);
    }


    public void UnlockLevel()
    {
        // منطق فتح المستوى الجديد
        Debug.Log("Level unlocked!");
    }
}

