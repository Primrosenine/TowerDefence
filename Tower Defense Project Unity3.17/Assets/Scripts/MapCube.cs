using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretGo;
    public GameObject buildEffect;
    public TurretData turretData;
    private Renderer render;
    private Color color;

    private void Start()
    {
        render = GetComponent<Renderer>();
        color = render.material.color;
    }

    public void BuildTurret(TurretData turretData)
    {
        this.turretData = new TurretData()
        {
            turretType = turretData.turretType,
            turretPrefab = turretData.turretPrefab,
            cost = turretData.cost,
            turretUpgradePrefab = turretData.turretUpgradePrefab,
            upgradeCost = turretData.upgradeCost,
            isUpgraded = turretData.isUpgraded,
        };
        this.turretData.isUpgraded = false;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }

    public void UpgradeTurret()
    {
        Destroy(turretGo);
        turretData.isUpgraded = true;
        turretGo = GameObject.Instantiate(turretData.turretUpgradePrefab, transform.position, Quaternion.identity);
    }

    public void DestroyTurret()
    {
        Destroy(turretGo);
        turretData = null;
        turretGo = null;
    }

    private void OnMouseEnter()
    {
        if (turretData == null && !EventSystem.current.IsPointerOverGameObject())
        {
            render.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        render.material.color = color;
    }
}
