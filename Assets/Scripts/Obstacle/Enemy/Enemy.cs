using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

public class Enemy : Obstacle
{
    public int enemyID;
    public string enemyName;
    public EnemyAttribute attribute { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        LoadEnemyData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void onHitEvent()
    {
        battle();
        remove();
    }
    public void battle()
    {
        BraverStatus braverStatus = BraverStatus.GetInstance();
        braverStatus.getAttributes().TryGetValue(BraverAttribute.Health, out int braverHealth);
        int damage = EnemyProperty.GetInstance().DamageCalculate(this);
        if (braverHealth > damage && damage > 0)
        {
            braverStatus.UpdateStatus(BraverAttribute.Health, -1 * damage);
        }
    }

    private void LoadEnemyData()
    {
        if (GlobalVariables.enemyAttributeTempStore.ContainsKey(enemyID))
        {
            GlobalVariables.enemyAttributeTempStore.TryGetValue(enemyID, out EnemyAttribute at);
            attribute = at;
            enemyName = at.name;
        }
        else
        {
            JObject JsonData = Model.GetEnemyData(enemyID);
            int h = (int)JsonData.GetValue("Health");
            int a = (int)JsonData.GetValue("Attack");
            int d = (int)JsonData.GetValue("Defence");
            int g = (int)JsonData.GetValue("Gold");
            int e = (int)JsonData.GetValue("Experience");
            enemyName = (string)JsonData.GetValue("EnemyName");
            attribute = new EnemyAttribute(enemyID, enemyName, h, a, d, g, e);
            JArray skillListArray = (JArray)JsonData.GetValue("Skill");
            List<string> skillList = skillListArray.ToObject<List<string>>();
            List<EnemySkill> enemySkills = new List<EnemySkill>();
            if (skillList.Count > 0)
            {
                foreach (string strSkill in skillList)
                {
                    enemySkills.Add((EnemySkill)Enum.Parse(typeof(EnemySkill), strSkill));
                }
            }
            attribute.setSkill(enemySkills);
            GlobalVariables.enemyAttributeTempStore.Add(enemyID, attribute);
        }
        
    }
}
