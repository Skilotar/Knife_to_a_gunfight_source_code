using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Knives
{
	public class Commands : ETGModule
	{
		// Token: 0x06000516 RID: 1302 RVA: 0x000301ED File Offset: 0x0002E3ED
		public override void Exit()
		{
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000301F0 File Offset: 0x0002E3F0
		public override void Start()
		{
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x000301F4 File Offset: 0x0002E3F4
		public override void Init()
		{
			ETGModConsole.Commands.AddGroup("rage_quit", delegate (string[] args)
			{
				ETGModConsole.Log("Goodbye :) ", false);
				Application.Quit();
			});
			
			ETGModConsole.Commands.AddGroup("zoomout", delegate (string[] args)
			{
				GameManager.Instance.MainCameraController.OverrideZoomScale *= 0.66f;
				
			});

			ETGModConsole.Commands.AddGroup("zoomin", delegate (string[] args)
			{
				GameManager.Instance.MainCameraController.OverrideZoomScale /= 0.66f;

			});
			
		}

	}

	

}
