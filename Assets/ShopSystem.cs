using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public GameObject[] characters; // الشخصيات المتاحة
    private int selectedCharacter = 0;

    public void SelectCharacter(int index)
    {
        selectedCharacter = index;
        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        // هنا يمكنك إضافة منطق لتحميل الشخصية
    }

    public void LoadCharacter()
    {
        int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        // تحميل الشخصية بناءً على index
        Debug.Log("Loading character " + selectedCharacter);
    }
}
