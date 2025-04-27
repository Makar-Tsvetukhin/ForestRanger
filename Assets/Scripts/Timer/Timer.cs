using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Timer
{
	public float MaxTime { get; private set; }
	public float CurrentTime { get; private set; }
	public bool TimerIsEnd { get; private set; }
	public bool Pause { get; private set; }

	public event Action OnTick;
	public event Action OnTimerEnd;

	public Timer(float maxtime, float startvalue = 0, bool pause = false)
	{
		MaxTime = maxtime;
		CurrentTime = startvalue;
		Pause = pause;

		if (CurrentTime >= MaxTime) TimerIsEnd = true;
	}

	public void ResetTimer(bool pause)
	{
		TimerIsEnd = false;
		CurrentTime = 0;
		Pause = pause;
	}

	public void SetMaxTimeAndReset(float maxtime)
	{
		TimerIsEnd = false;
		CurrentTime = 0;
		MaxTime = maxtime;
	}

	public void SetPause() => Pause = true;

	public void Continue() => Pause = false;

	public void Tick(float time)
	{
		if (TimerIsEnd || Pause) return;

		UpdateTimer(time);
		OnTick?.Invoke();
	}

	public void UpdateTimer(float time)
	{
		CurrentTime += time;
		CurrentTime = Mathf.Clamp(CurrentTime, 0, MaxTime);

		if (CurrentTime >= MaxTime)
		{
			TimerIsEnd = true;
			OnTimerEnd?.Invoke();
		}
	}
}
