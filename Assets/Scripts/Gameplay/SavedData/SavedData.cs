using UnityEngine;

public class SavedData : Singleton<SavedData>
{
    private const string Money = "Money";

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public void SaveMoneyData(int amount)
    {
        PlayerPrefs.SetInt(Money, amount);
    }

    public int LoadMoneyData()
    {
        return PlayerPrefs.GetInt(Money, 0);
    }
}
