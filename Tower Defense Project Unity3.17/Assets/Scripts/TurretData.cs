using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData
{
    public TurretType turretType;
    public GameObject turretPrefab;
    public int cost;
    public GameObject turretUpgradePrefab;
    public int upgradeCost;
    public bool isUpgraded;
}

public enum TurretType
{
    LaserTurret,
    MissileTurret,
    StandardTurret,
}
