using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : ThirdPlantStage
{
    /// <summary>
    /// Called by animator.
    /// </summary>
    public override void OnAttack()
    {
        findTriggers = true;
        GameObject projectile = PrefabManager.instance.InitPrefab("Test Projectile", transform.position);
        //projectile.transform.localScale = new Vector3(1, 1, 1);
        
    }


}
