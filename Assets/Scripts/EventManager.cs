using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
	#region Actions
	public static event UnityAction OnSimulationStart;
    public static event UnityAction OnSimulationStop;

	//	public static event UnityAction<Vector3> CoinCollected;

	#endregion

	public static void SimulationStart() => OnSimulationStart?.Invoke();
	public static void SimulationStop() => OnSimulationStop?.Invoke();
}