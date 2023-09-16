using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public string teamName;
    public Player[] players;
    public float teamOffenseStrength;
    public float teamDefenseStrength;

    [Range(1, 15)]
    public int trainingFacilities;
    [Range(1, 100000000), Tooltip("Max wage they can give a player")]
    public float wagesCap;
}
