using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;
    public GameObject upgradeCanvas;
    public Button upgradeButton;
    public Animation anim;
    public Text moneyText;

    private TurretData selectedTurretData;  //Select UI
    private MapCube selectedMapCube;  //select 3D Cube
    private int money = 1000;

    private void ChangeMoney(int change = 0)
    {
        money += change;
        moneyText.text = money.ToString();
    }

    private void Start()
    {
        ChangeMoney();
        selectedTurretData = laserTurretData;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollide = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollide)
                {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (mapCube.turretGo == null)
                    {
                        if (money >= selectedTurretData.cost)
                        {
                            ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData);
                        }
                        else
                        {
                            anim.Play();
                        }
                    }
                    else
                    {
                        if (selectedMapCube != mapCube || upgradeCanvas.activeInHierarchy == false)
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.turretData.isUpgraded);
                            selectedMapCube = mapCube;
                        }
                        else
                        {
                            HideUpgradeUI();
                        }
                    }
                }
            }
        }
    }

    public void OnLaserSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = laserTurretData;
        }
    }


    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = missileTurretData;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            selectedTurretData = standardTurretData;
        }
    }

    private void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        upgradeButton.interactable = !isDisableUpgrade;
    }

    private void HideUpgradeUI()
    {
        upgradeCanvas.SetActive(false);
    }

    public void OnUpgradeButtonDown()
    {
        if (money >= selectedMapCube.turretData.upgradeCost)
        {
            ChangeMoney(-selectedMapCube.turretData.upgradeCost);
            selectedMapCube.UpgradeTurret();
            HideUpgradeUI();
        }
        else
        {
            anim.Play();
        }
    }

    public void OnDestroyButtonDown()
    {
        selectedMapCube.DestroyTurret();
        HideUpgradeUI();
    }
}
