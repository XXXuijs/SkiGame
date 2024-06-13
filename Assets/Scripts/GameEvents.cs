using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    // Define a delegate for race actions
    public delegate void RaceAction();

    // Declare the events for RaceStart and RaceStop
    public static event RaceAction OnRaceStart;
    public static event RaceAction OnRaceStop;

    // Method to invoke the OnRaceStart event
    public static void StartRace()
    {
        // Check if there are any subscribers to the OnRaceStart event
        if (OnRaceStart != null)
        {
            // Invoke the event, notifying all subscribers
            OnRaceStart();
        }
    }

    // Method to invoke the OnRaceStop event
    public static void StopRace()
    {
        // Check if there are any subscribers to the OnRaceStop event
        if (OnRaceStop != null)
        {
            // Invoke the event, notifying all subscribers
            OnRaceStop();
        }
    }
}