using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    public CameraShaker cameraShaker;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            PowerCombo combo = new();
            foreach (PowerBehaviour power in ObjectStore.powers)
            {
                if (power.IsSelected())
                {
                    combo.anyPower = true;
                    if (power.color != null) combo.colors.Add((GameColor)power.color);
                    combo.isMultiShot |= power.isMultiShot;
                }
            }

            if (!combo.anyPower) return;
            var comboColor = (GameColor)GameColorUtils.MixColors(combo.colors);

            int selectedEnemyCount = 0;
            List<EnemyBehaviour> matchedEnemies = new();
            foreach (EnemyBehaviour enemy in ObjectStore.enemies)
            {
                if (enemy.IsSelected())
                {
                    selectedEnemyCount++;
                    if (enemy.color == comboColor) matchedEnemies.Add(enemy);
                }
            }

            if ((!combo.isMultiShot && selectedEnemyCount == 1) || (combo.isMultiShot && selectedEnemyCount > 1))
            {
                foreach (EnemyBehaviour enemy in matchedEnemies) Destroy(enemy.gameObject);
                cameraShaker.Trigger();
            }
        }
    }
}

class PowerCombo
{
    public bool anyPower = false;
    public List<GameColor> colors = new();
    public bool isMultiShot = false;
}

public static class ObjectStore
{
    public static List<PowerBehaviour> powers = new();
    public static List<EnemyBehaviour> enemies = new();
}