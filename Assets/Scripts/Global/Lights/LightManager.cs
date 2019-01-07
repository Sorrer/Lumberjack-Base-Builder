using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{

	public GameObject NightLight;
	public GameObject DayLight;

	[ColorUsage(false, hdr: true)]
	public Color DayAmbientLight;
	[ColorUsage(false, hdr: true)]
	public Color NightAmbientLight;

	public GameObject CurrentLight;

	void Start()
    {
    }

    void Update()
    {
        
    }

	public void ChangeLight(GameState state) {
		switch (state) {
			case GameState.Loading:

				break;
			case GameState.Day:
				Destroy(this.CurrentLight);
				this.CurrentLight = Instantiate(this.DayLight);
				RenderSettings.ambientLight = this.DayAmbientLight;
				
				break;
			case GameState.Night:
				Destroy(this.CurrentLight);
				this.CurrentLight = Instantiate(this.NightLight);
				RenderSettings.ambientLight = this.NightAmbientLight;
				break;
			case GameState.Dead:

				break;
		}
	}
}
