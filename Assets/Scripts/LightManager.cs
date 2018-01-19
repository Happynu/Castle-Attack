using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{

    public DMXControler lightController;

    public void ChangeTeamLight(Team currentTeam)
    {
        switch (currentTeam.color)
        {
            case "red":
                lightController.SetPreset(3);
                break;

            case "blue":
                lightController.SetPreset(4);
                break;
        }
    }
}
