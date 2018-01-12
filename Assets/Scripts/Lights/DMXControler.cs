using UnityEngine;

public class DMXControler : MonoBehaviour {

    public static DMXControler Instance;
    public MainAppGateway MainApp;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// verander de lichten met SetPreset met de volgende waardes
    /// 0 = idle (dit zet de lampen in de start positie met het licht uit)
    /// 1 = word nog veranderd
    /// 2 = word nog veranderd
    /// 3 = rood
    /// 4 = blauw
    /// 5 = groen
    /// 6 = geel
    /// </summary>
    /// <param name="number"></param>
    public void SetPreset(int number)
    {
        if(number < 7)
        {
            MainApp.SendMessageToMain("DMX" + number);
        }
    }
}
