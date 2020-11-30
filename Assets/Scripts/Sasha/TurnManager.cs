using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    static Dictionary<string, List<TacticMove>> units = new Dictionary<string, List<TacticMove>>();
    static Queue<string> turnKey = new Queue<string>();
    static Queue<TacticMove> turnTeam = new Queue<TacticMove>();


    void Start()
    {
        if (turnTeam.Count == 0)
        {
            InitTeamTurnQueue();
        }
    }

    void Update()
    {

    }

    static void InitTeamTurnQueue()
    {
        List<TacticMove> teamList = units[turnKey.Peek()];

        foreach (TacticMove unit in teamList)
        {
            turnTeam.Enqueue(unit);
        }

        StartTurn();
    }

    static void StartTurn()
    {
        if (turnTeam.Count > 0)
        {
            //turnTeam.Peek().BeginTurn();

        }
    }
    public static void EndTurn()
    {
        TacticMove unit = turnTeam.Dequeue();
        //unit.EndTurn();
        if (turnTeam.Count > 0)
        {
            StartTurn();
        }
        else
        {
            string team = turnKey.Dequeue();
            turnKey.Enqueue(team);
            InitTeamTurnQueue();
        }
    }

    public static void AddUnit(TacticMove unit)
    {
        List<TacticMove> list;
        if (!units.ContainsKey(unit.tag))
        {
            list = new List<TacticMove>();
            units[unit.tag] = list;

            if (!turnKey.Contains(unit.tag))
            {
                turnKey.Enqueue(unit.tag);
            }


        }
        else
        {
            list = units[unit.tag];
        }

        list.Add(unit);
    }
}
