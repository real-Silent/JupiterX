using easyInputs;
using JupiterX.Classes;
using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static JupiterX.Menu.Buttons;
using static JupiterX.Settings;

namespace JupiterX.Menu
{
	public class Main
	{
		public static void Prefix()
		{
			try
			{
				Utility.toOpen = (!rightHanded && Utility.LSec || (rightHanded && Utility.RSec));
				bool keyboardOpen = false;

				if (menu == null)
				{
					if (Utility.toOpen || keyboardOpen)
					{
                        Utility.PlayEmbeddedSoundOnHand("JupiterX.Resources.menuopen.wav");
                        CreateMenu();

                        RecenterMenu(rightHanded, keyboardOpen);
						if (reference == null)
						{
							CreateReference(rightHanded);
						}
					}
				}
				else
				{
					if ((Utility.toOpen || keyboardOpen))
					{
						RecenterMenu(rightHanded, keyboardOpen);
					}
					else
					{
						Rigidbody comp = menu.AddComponent<Rigidbody>();

						if (rightHanded)
						{
							comp.velocity = Utility.ThrowMenu(Utility.RightHand);
						}
						else
						{
                            comp.velocity = Utility.ThrowMenu(Utility.LeftHand);
                        }

                        Utility.PlayEmbeddedSoundOnHand("JupiterX.Resources.menuclose.wav");

                        UnityEngine.Object.Destroy(menu, 5);
                        menu = null;

                        UnityEngine.Object.Destroy(reference);
						reference = null;
					}
				}
			}
			catch (Exception exc)
			{
				UnityEngine.Debug.LogError(string.Format("{0} // Error initializing at {1}: {2}", Utility.name, exc.StackTrace, exc.Message));
			}

			Utility.RPrim = EasyInputs.GetPrimaryButtonDown(Utility.RightHand);
			Utility.RSec = EasyInputs.GetSecondaryButtonDown(Utility.RightHand);
			Utility.RGrip = EasyInputs.GetGripButtonDown(Utility.RightHand);
			Utility.RTrigger = EasyInputs.GetTriggerButtonDown(Utility.RightHand);
			Utility.RTriggerFloat = EasyInputs.GetTriggerButtonFloat(Utility.RightHand);
			Utility.RJoystick = EasyInputs.GetThumbStickButtonDown(Utility.RightHand);
			Utility.RJoystickAxis = EasyInputs.GetThumbStick2DAxis(Utility.RightHand);

			Utility.LPrim = EasyInputs.GetPrimaryButtonDown(Utility.LeftHand);
			Utility.LSec = EasyInputs.GetSecondaryButtonDown(Utility.LeftHand);
			Utility.LGrip = EasyInputs.GetGripButtonDown(Utility.LeftHand);
			Utility.LTrigger = EasyInputs.GetTriggerButtonDown(Utility.LeftHand);
			Utility.LTriggerFloat = EasyInputs.GetTriggerButtonFloat(Utility.LeftHand);
			Utility.LJoystick = EasyInputs.GetThumbStickButtonDown(Utility.LeftHand);
			Utility.LJoystickAxis = EasyInputs.GetThumbStick2DAxis(Utility.LeftHand);

            if (Utility.isTriggers)
            {
                if (menu != null)
				{
                    if (Utility.LTrigger)
                    {
                        if (!Utility.hasTriggeredOnceL)
                        {
                            Utility.hasTriggeredOnceL = true;
                            Toggle("PreviousPage");
                        }
                    }
                    else
                    {
                        Utility.hasTriggeredOnceL = false;
                    }
                    if (Utility.RTrigger)
                    {
                        if (!Utility.hasTriggeredOnceR)
                        {
                            Utility.hasTriggeredOnceR = true;
                            Toggle("NextPage");
                        }
                    }
                    else
                    {
                        Utility.hasTriggeredOnceR = false;
                    }
                }
            }

            // Join / leave room reminders
            try
            {
                if (PhotonNetwork.InRoom)
                {
                    lastRoom = PhotonNetwork.CurrentRoom.Name;
                }

                if (PhotonNetwork.InRoom && !lastInRoom)
                {
                    NotificationManager.SendNotification("blue", "JOIN ROOM", "Room Code: " + lastRoom + "");
                }
                if (!PhotonNetwork.InRoom && lastInRoom)
                {
                    NotificationManager.SendNotification("blue", "LEAVE ROOM", "Room Code: " + lastRoom + "");
                    lastMasterClient = false;
                }

                lastInRoom = PhotonNetwork.InRoom;
            }
            catch { }

            // Master client notification
            try
            {
                if (PhotonNetwork.InRoom)
                {
                    if (PhotonNetwork.LocalPlayer.IsMasterClient && !lastMasterClient)
                    {
                        NotificationManager.SendNotification("purple", "MASTER", "You are now master client.");
                    }
                    lastMasterClient = PhotonNetwork.LocalPlayer.IsMasterClient;
                }
            }
            catch { }

            // Constant
            try
            {
				// Execute Enabled mods
				foreach (ButtonInfo[] buttonlist in buttons)
				{
					foreach (ButtonInfo v in buttonlist)
					{
						if (v.enabled)
						{
							if (v.method != null)
							{
								try
								{
									v.method.Invoke();
								}
								catch (Exception exc)
								{
									UnityEngine.Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", Utility.name, v.buttonText, exc.StackTrace, exc.Message));
								}
							}
						}
					}
				}
			}
			catch (Exception exc)
			{
				UnityEngine.Debug.LogError(string.Format("{0} // Error with executing mods at {1}: {2}", Utility.name, exc.StackTrace, exc.Message));
			}
		}

        // Functions
        public static void CreateMenu()
		{
			// Menu Holder
			menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
			UnityEngine.Object.Destroy(menu.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(menu.GetComponent<BoxCollider>());
			UnityEngine.Object.Destroy(menu.GetComponent<Renderer>());
			menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.3825f);

			// Menu Background
			menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
			UnityEngine.Object.Destroy(menuBackground.GetComponent<Rigidbody>());
			UnityEngine.Object.Destroy(menuBackground.GetComponent<BoxCollider>());
			menuBackground.transform.parent = menu.transform;
			menuBackground.transform.rotation = Quaternion.identity;
			menuBackground.transform.localScale = menuSize;
			menuBackground.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
			menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);

            RoundMenuObject(menuBackground);

			ColorChanger colorChanger = menuBackground.AddComponent<ColorChanger>();
			colorChanger.colorInfo = backgroundColor;
			colorChanger.Start();

			// Canvas
			canvasObject = new GameObject();
			canvasObject.transform.parent = menu.transform;
			Canvas canvas = canvasObject.AddComponent<Canvas>();
			CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
			canvasObject.AddComponent<GraphicRaycaster>();
			canvas.renderMode = RenderMode.WorldSpace;
			canvasScaler.dynamicPixelsPerUnit = 2500f;

			// Title and FPS
			Text text = new GameObject
			{
				transform =
					{
						parent = canvasObject.transform
					}
			}.AddComponent<Text>();
			text.font = currentFont;
			text.text = Utility.name + " <color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";
			text.fontSize = 1;
			text.color = textColors[0];
			text.supportRichText = true;
			text.fontStyle = FontStyle.Italic;
			text.alignment = TextAnchor.MiddleCenter;
			text.resizeTextForBestFit = true;
			text.resizeTextMinSize = 0;
			RectTransform component = text.GetComponent<RectTransform>();
			component.localPosition = Vector3.zero;
			component.sizeDelta = new Vector2(0.28f, 0.05f);
			component.position = new Vector3(0.06f, 0f, 0.165f);
			component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

			if (fpsCounter)
			{
				fpsObject = new GameObject
				{
					transform =
					{
						parent = canvasObject.transform
					}
				}.AddComponent<Text>();
				fpsObject.font = currentFont;
				fpsObject.text = "Version: " + Utility.version;
				fpsObject.color = textColors[0];
				fpsObject.fontSize = 1;
				fpsObject.supportRichText = true;
				fpsObject.fontStyle = FontStyle.Italic;
				fpsObject.alignment = TextAnchor.MiddleCenter;
				fpsObject.horizontalOverflow = UnityEngine.HorizontalWrapMode.Overflow;
				fpsObject.resizeTextForBestFit = true;
				fpsObject.resizeTextMinSize = 0;
				RectTransform component2 = fpsObject.GetComponent<RectTransform>();
				component2.localPosition = Vector3.zero;
				component2.sizeDelta = new Vector2(0.28f, 0.02f);
				component2.position = new Vector3(0.06f, 0f, 0.135f);
				component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			}

			// Buttons
			// Disconnect
			if (disconnectButton)
			{
				GameObject disconnectbutton = GameObject.CreatePrimitive(PrimitiveType.Cube);

				UnityEngine.Object.Destroy(disconnectbutton.GetComponent<Rigidbody>());
				disconnectbutton.GetComponent<BoxCollider>().isTrigger = true;
				disconnectbutton.transform.parent = menu.transform;
				disconnectbutton.transform.rotation = Quaternion.identity;
				disconnectbutton.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
				disconnectbutton.transform.localPosition = new Vector3(0.56f, 0f, 0.6f);
				disconnectbutton.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
				disconnectbutton.AddComponent<Classes.Button>().relatedText = "Disconnect";

                RoundMenuObject(disconnectbutton);

                colorChanger = disconnectbutton.AddComponent<ColorChanger>();
				colorChanger.colorInfo = buttonColors[0];
				colorChanger.Start();

				Text discontext = new GameObject
				{
					transform =
							{
								parent = canvasObject.transform
							}
				}.AddComponent<Text>();
				discontext.text = "Disconnect";
				discontext.font = currentFont;
				discontext.fontSize = 1;
				discontext.color = textColors[0];
				discontext.alignment = TextAnchor.MiddleCenter;
				discontext.resizeTextForBestFit = true;
				discontext.resizeTextMinSize = 0;

				RectTransform rectt = discontext.GetComponent<RectTransform>();
				rectt.localPosition = Vector3.zero;
				rectt.sizeDelta = new Vector2(0.2f, 0.03f);
				rectt.localPosition = new Vector3(0.064f, 0f, 0.23f);
				rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			}

			// Page Buttons
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

			UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
			gameObject.transform.parent = menu.transform;
			gameObject.transform.rotation = Quaternion.identity;
			gameObject.transform.localScale = Utility.PageObjScale;
			gameObject.transform.localPosition = Utility.PageObjectPosLeft;
			gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
			gameObject.AddComponent<Classes.Button>().relatedText = "NextPage"; // PreviousPage

            colorChanger = gameObject.AddComponent<ColorChanger>();
			colorChanger.colorInfo = buttonColors[0];
			colorChanger.Start();

			text = new GameObject
			{
				transform =
						{
							parent = canvasObject.transform
						}
			}.AddComponent<Text>();
			text.font = currentFont;
			text.text = "<";
			text.fontSize = 1;
			text.color = textColors[0];
			text.alignment = TextAnchor.MiddleCenter;
			text.resizeTextForBestFit = true;
			text.resizeTextMinSize = 0;
			component = text.GetComponent<RectTransform>();
			component.localPosition = Vector3.zero;
			component.sizeDelta = new Vector2(0.2f, 0.03f);
			component.localPosition = Utility.PageTextPosLeft; ;
			component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

			GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Cube);

			UnityEngine.Object.Destroy(gameObject2.GetComponent<Rigidbody>());
            gameObject2.GetComponent<BoxCollider>().isTrigger = true;
            gameObject2.transform.parent = menu.transform;
            gameObject2.transform.rotation = Quaternion.identity;
            gameObject2.transform.localScale = Utility.PageObjScale;
            gameObject2.transform.localPosition = Utility.PageObjectPosRight;
            gameObject2.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            gameObject2.AddComponent<Classes.Button>().relatedText = "PreviousPage"; // NextPage

            colorChanger = gameObject2.AddComponent<ColorChanger>();
			colorChanger.colorInfo = buttonColors[0];
			colorChanger.Start();

			text = new GameObject
			{
				transform =
						{
							parent = canvasObject.transform
						}
			}.AddComponent<Text>();
			text.font = currentFont;
			text.text = ">";
			text.fontSize = 1;
			text.color = textColors[0];
			text.alignment = TextAnchor.MiddleCenter;
			text.resizeTextForBestFit = true;
			text.resizeTextMinSize = 0;
			component = text.GetComponent<RectTransform>();
			component.localPosition = Vector3.zero;
			component.sizeDelta = new Vector2(0.2f, 0.03f);
			component.localPosition = Utility.PageTextPosRight;
			component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            RoundMenuObject(gameObject);
            RoundMenuObject(gameObject2);

            // Mod Buttons
            ButtonInfo[] activeButtons = buttons[buttonsType].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
			for (int i = 0; i < activeButtons.Length; i++)
			{
				CreateButton(i * 0.1f, i, activeButtons[i]);
			}
		}

		public static void CreateButton(float offset, int buttonIndex, ButtonInfo method)
		{
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

			UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
			gameObject.transform.parent = menu.transform;
			gameObject.transform.rotation = Quaternion.identity;
			gameObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.08f);
			gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
			gameObject.AddComponent<Classes.Button>().relatedText = method.buttonText;

            RoundMenuObject(gameObject);

            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
			if (method.enabled)
			{
				colorChanger.colorInfo = buttonColors[1];
			}
			else
			{
				colorChanger.colorInfo = buttonColors[0];
			}
			colorChanger.Start();

			Text text = new GameObject
			{
				transform =
				{
					parent = canvasObject.transform
				}
			}.AddComponent<Text>();
			text.font = currentFont;
			text.text = method.buttonText;
			if (method.overlapText != null)
			{
				text.text = method.overlapText;
			}
			text.supportRichText = true;
			text.fontSize = 1;
			if (method.enabled)
			{
				text.color = textColors[1];
			}
			else
			{
				text.color = textColors[0];
			}
			text.alignment = TextAnchor.MiddleCenter;
			text.fontStyle = FontStyle.Italic;
			text.resizeTextForBestFit = true;
			text.resizeTextMinSize = 0;
			RectTransform component = text.GetComponent<RectTransform>();
			component.localPosition = Vector3.zero;
			component.sizeDelta = new Vector2(.2f, .03f);
			component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
			component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }
        public static void RecreateMenu()
		{
			if (menu != null)
			{
				UnityEngine.Object.Destroy(menu);
				menu = null;

				CreateMenu();
				RecenterMenu(rightHanded, false);
			}
		}

		public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
		{
			if (!isKeyboardCondition)
			{
				if (!isRightHanded)
				{
					menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
					menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
				}
				else
				{
					menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
					Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
					rotation += new Vector3(0f, 0f, 180f);
					menu.transform.rotation = Quaternion.Euler(rotation);
				}
			}
			else
			{
				try
				{
					TPC = GameObject.Find("Third Person Camera/Shoulder Camera").GetComponent<Camera>();
				}
				catch { }
				if (TPC != null)
				{
					TPC.transform.position = new Vector3(-999f, -999f, -999f);
					TPC.transform.rotation = Quaternion.identity;
					GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
					bg.transform.localScale = new Vector3(10f, 10f, 0.01f);
					bg.transform.transform.position = TPC.transform.position + TPC.transform.forward;
					bg.GetComponent<Renderer>().material.color = new Color32((byte)(backgroundColor.colors[0].color.r * 50), (byte)(backgroundColor.colors[0].color.g * 50), (byte)(backgroundColor.colors[0].color.b * 50), 255);
					GameObject.Destroy(bg, Time.deltaTime);
					menu.transform.parent = TPC.transform;
					menu.transform.position = (TPC.transform.position + (Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)))) + (Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f)));
					Vector3 rot = TPC.transform.rotation.eulerAngles;
					rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
					menu.transform.rotation = Quaternion.Euler(rot);

					if (reference != null)
					{
						if (Mouse.current.leftButton.isPressed)
						{
							Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
							RaycastHit hit;
							bool worked = Physics.Raycast(ray, out hit, 100);
							if (worked)
							{
								Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
								if (collide != null)
								{
									collide.OnTriggerEnter(buttonCollider);
								}
							}
						}
						else
						{
							reference.transform.position = new Vector3(999f, -999f, -999f);
						}
					}
				}
			}
		}

		public static void CreateReference(bool isRightHanded)
		{
			reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			if (isRightHanded)
			{
				reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
			}
			else
			{
				reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
			}
			reference.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
			reference.transform.localPosition = new Vector3(0.013f, -0.025f, 0.1f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			buttonCollider = reference.GetComponent<SphereCollider>();

			ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
			colorChanger.colorInfo = backgroundColor;
			colorChanger.Start();
		}

        public static void Toggle(string buttonText)
		{
			int lastPage = ((buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage) - 1;
			if (buttonText == "PreviousPage")
			{
				pageNumber--;
				if (pageNumber < 0)
				{
					pageNumber = lastPage;
				}
			}
			else
			{
				if (buttonText == "NextPage")
				{
					pageNumber++;
					if (pageNumber > lastPage)
					{
						pageNumber = 0;
					}
				}
				else
				{
					ButtonInfo target = GetIndex(buttonText);
					if (target != null)
					{
						if (target.isTogglable)
						{
							target.enabled = !target.enabled;
							if (target.enabled)
							{
                                NotificationManager.SendNoti("<color=grey>[</color><color=green>ENABLED</color><color=grey>]</color> " + target.toolTip);
								if (target.enableMethod != null)
								{
									try { target.enableMethod.Invoke(); } catch { }
								}
							}
							else
							{
                                NotificationManager.SendNoti("<color=grey>[</color><color=red>DISABLED</color><color=grey>]</color> " + target.toolTip);
                                if (target.disableMethod != null)
								{
									try { target.disableMethod.Invoke(); } catch { }
								}
							}
						}
						else
						{
                            NotificationManager.SendNoti("<color=grey>[</color><color=green>ENABLED</color><color=grey>]</color> " + target.toolTip);
                            if (target.method != null)
							{
								try { target.method.Invoke(); } catch { }
							}
						}
					}
					else
					{
						UnityEngine.Debug.LogError(buttonText + " does not exist");
					}
				}
			}
			RecreateMenu();
		}

        public static string lastClickedName = "";
        public static GradientColorKey[] GetSolidGradient(Color color)
		{
			return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
		}

		public static ButtonInfo GetIndex(string buttonText)
		{
			foreach (ButtonInfo[] buttons in Menu.Buttons.buttons)
			{
				foreach (ButtonInfo button in buttons)
				{
					if (button.buttonText == buttonText)
					{
						return button;
					}
				}
			}

			return null;
		}

		// Variables
		// Important
		// Objects
		public static GameObject menu;
		public static GameObject menuBackground;
		public static GameObject reference;
		public static GameObject canvasObject;

		public static SphereCollider buttonCollider;
		public static Camera TPC;
		public static Text fpsObject;

		// Data
		public static int pageNumber = 0;
		public static int buttonsType = 0;
        public static int framePressCooldown;

        public static bool lastInRoom = false;
        public static bool lastMasterClient = false;
        public static string lastRoom = "";




        public static bool scaleWithPlayer;

        public static Vector3 MidPosition;
        public static Vector3 MidVelocity;

        public static bool SmoothGunPointer;
        public static bool smallGunPointer;
        public static bool disableGunPointer;
        public static bool disableGunLine;
        public static bool SwapGunHand;
        public static bool GriplessGuns;
        public static bool TriggerlessGuns;
        public static bool HardGunLocks;
        public static bool GunSounds;
        public static bool GunParticles;
        public static int gunVariation;
        public static int GunDirection;
        public static int GunLineQuality = 50;

        public static bool GunSpawned;
        public static bool gunLocked;
        public static VRRig lockTarget;

        public static bool lastGunSpawned;
        public static bool lastGunTrigger;

        public static void RoundMenuObject(GameObject toRound, float Bevel = 0.02f)
        {
            if (toRound.transform.parent != menu?.transform)
            {
                RoundObject(toRound, Bevel);
                return;
            }

            Renderer ToRoundRenderer = toRound.GetComponent<Renderer>();
            GameObject BaseA = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseA.GetComponent<Renderer>().enabled = ToRoundRenderer.enabled;
            GameObject.Destroy(BaseA.GetComponent<Collider>());

            BaseA.transform.parent = menu.transform;
            BaseA.transform.rotation = Quaternion.identity;
            BaseA.transform.localPosition = toRound.transform.localPosition;
            BaseA.transform.localScale = toRound.transform.localScale + new Vector3(0f, Bevel * -2.55f, 0f);

            GameObject BaseB = GameObject.CreatePrimitive(PrimitiveType.Cube);
            BaseB.GetComponent<Renderer>().enabled = ToRoundRenderer.enabled;
            GameObject.Destroy(BaseB.GetComponent<Collider>());

            BaseB.transform.parent = menu.transform;
            BaseB.transform.rotation = Quaternion.identity;
            BaseB.transform.localPosition = toRound.transform.localPosition;
            BaseB.transform.localScale = toRound.transform.localScale + new Vector3(0f, 0f, -Bevel * 2f);

            GameObject RoundCornerA = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerA.GetComponent<Renderer>().enabled = ToRoundRenderer.enabled;
            GameObject.Destroy(RoundCornerA.GetComponent<Collider>());

            RoundCornerA.transform.parent = menu.transform;
            RoundCornerA.transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);

            RoundCornerA.transform.localPosition = toRound.transform.localPosition + new Vector3(0f, toRound.transform.localScale.y / 2f - Bevel * 1.275f, toRound.transform.localScale.z / 2f - Bevel);
            RoundCornerA.transform.localScale = new Vector3(Bevel * 2.55f, toRound.transform.localScale.x / 2f, Bevel * 2f);

            GameObject RoundCornerB = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerB.GetComponent<Renderer>().enabled = ToRoundRenderer.enabled;
            GameObject.Destroy(RoundCornerB.GetComponent<Collider>());

            RoundCornerB.transform.parent = menu.transform;
            RoundCornerB.transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);

            RoundCornerB.transform.localPosition = toRound.transform.localPosition + new Vector3(0f, -(toRound.transform.localScale.y / 2f) + Bevel * 1.275f, toRound.transform.localScale.z / 2f - Bevel);
            RoundCornerB.transform.localScale = new Vector3(Bevel * 2.55f, toRound.transform.localScale.x / 2f, Bevel * 2f);

            GameObject RoundCornerC = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerC.GetComponent<Renderer>().enabled = ToRoundRenderer.enabled;
            GameObject.Destroy(RoundCornerC.GetComponent<Collider>());

            RoundCornerC.transform.parent = menu.transform;
            RoundCornerC.transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);

            RoundCornerC.transform.localPosition = toRound.transform.localPosition + new Vector3(0f, toRound.transform.localScale.y / 2f - Bevel * 1.275f, -(toRound.transform.localScale.z / 2f) + Bevel);
            RoundCornerC.transform.localScale = new Vector3(Bevel * 2.55f, toRound.transform.localScale.x / 2f, Bevel * 2f);

            GameObject RoundCornerD = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            RoundCornerD.GetComponent<Renderer>().enabled = ToRoundRenderer.enabled;
            GameObject.Destroy(RoundCornerD.GetComponent<Collider>());

            RoundCornerD.transform.parent = menu.transform;
            RoundCornerD.transform.rotation = Quaternion.identity * Quaternion.Euler(0f, 0f, 90f);

            RoundCornerD.transform.localPosition = toRound.transform.localPosition + new Vector3(0f, -(toRound.transform.localScale.y / 2f) + Bevel * 1.275f, -(toRound.transform.localScale.z / 2f) + Bevel);
            RoundCornerD.transform.localScale = new Vector3(Bevel * 2.55f, toRound.transform.localScale.x / 2f, Bevel * 2f);

            GameObject[] ToChange = {
                BaseA,
                BaseB,
                RoundCornerA,
                RoundCornerB,
                RoundCornerC,
                RoundCornerD
            };

            foreach (GameObject Changed in ToChange)
            {
                ClampColor TargetChanger = Changed.AddComponent<ClampColor>();
                TargetChanger.targetRenderer = ToRoundRenderer;
            }

            ToRoundRenderer.enabled = false;
        }

        public static void RoundObject(GameObject toRound, float bevel = 0.02f)
        {
            static GameObject CreatePrimitive(PrimitiveType type, Transform parent, bool rendererEnabled)
            {
                GameObject obj = GameObject.CreatePrimitive(type);
                obj.GetComponent<Renderer>().enabled = rendererEnabled;

                Collider collider = obj.GetComponent<Collider>();
                if (collider != null)
                    GameObject.Destroy(collider);

                obj.transform.SetParent(parent, false);
                return obj;
            }

            Renderer renderer = toRound.GetComponent<Renderer>();
            if (renderer == null) return;

            Transform parent = toRound.transform;
            Vector3 scale = parent.localScale;
            bool rendererEnabled = renderer.enabled;

            GameObject baseA = CreatePrimitive(PrimitiveType.Cube, parent, rendererEnabled);
            baseA.transform.localPosition = Vector3.zero;
            baseA.transform.localRotation = Quaternion.identity;
            baseA.transform.localScale = new Vector3(scale.x, scale.y - bevel * 2f, scale.z);

            GameObject baseB = CreatePrimitive(PrimitiveType.Cube, parent, rendererEnabled);
            baseB.transform.localPosition = Vector3.zero;
            baseB.transform.localRotation = Quaternion.identity;
            baseB.transform.localScale = new Vector3(scale.x, scale.y, scale.z - bevel * 2f);

            GameObject[] corners = new GameObject[4];
            Vector3[] cornerOffsets = {
                new Vector3(0f, scale.y / 2f - bevel, scale.z / 2f - bevel),
                new Vector3(0f, -scale.y / 2f + bevel, scale.z / 2f - bevel),
                new Vector3(0f, scale.y / 2f - bevel, -scale.z / 2f + bevel),
                new Vector3(0f, -scale.y / 2f + bevel, -scale.z / 2f + bevel)
            };

            for (int i = 0; i < 4; i++)
            {
                corners[i] = CreatePrimitive(PrimitiveType.Cylinder, parent, rendererEnabled);
                corners[i].transform.localPosition = cornerOffsets[i];
                corners[i].transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                corners[i].transform.localScale = new Vector3(bevel * 2f, scale.x / 2f, bevel * 2f);
            }

            GameObject[] allObjects = { baseA, baseB, corners[0], corners[1], corners[2], corners[3] };
            foreach (GameObject obj in allObjects)
            {
                ClampColor clampColor = obj.AddComponent<ClampColor>();
                clampColor.targetRenderer = renderer;
            }
            renderer.enabled = false;
        }

        private static VRRig _giveGunTarget;
        public static VRRig giveGunTarget
        {
            get
            {
                if (!GorillaParent.instance.vrrigs.Contains(_giveGunTarget))
                    _giveGunTarget = null;

                return _giveGunTarget;
            }
            set => _giveGunTarget = value;
        }

        public static int TransparentFX = LayerMask.NameToLayer("TransparentFX");
        public static int IgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
        public static int Zone = LayerMask.NameToLayer("Zone");
        public static int GorillaTrigger = LayerMask.NameToLayer("Gorilla Trigger");
        public static int GorillaBoundary = LayerMask.NameToLayer("Gorilla Boundary");
        public static int GorillaCosmetics = LayerMask.NameToLayer("GorillaCosmetics");
        public static int GorillaParticle = LayerMask.NameToLayer("GorillaParticle");

        public static int NoInvisLayerMask() =>
            ~(1 << TransparentFX | 1 << IgnoreRaycast | 1 << Zone | 1 << GorillaTrigger | 1 << GorillaBoundary | 1 << GorillaCosmetics | 1 << GorillaParticle);

        public static (Vector3 position, Quaternion rotation, Vector3 up, Vector3 forward, Vector3 right) TrueLeftHand()
        {
            Quaternion rot = GorillaTagger.Instance.leftHandTransform.rotation * GorillaLocomotion.Player.Instance.leftHandTransform.rotation;
            return (GorillaTagger.Instance.leftHandTransform.position + GorillaTagger.Instance.leftHandTransform.rotation * (GorillaLocomotion.Player.Instance.leftHandOffset * (scaleWithPlayer ? GorillaLocomotion.Player.Instance.transform.localScale.magnitude : 1f)), rot, rot * Vector3.up, rot * Vector3.forward, rot * Vector3.right);
        }

        public static (Vector3 position, Quaternion rotation, Vector3 up, Vector3 forward, Vector3 right) TrueRightHand()
        {
            Quaternion rot = GorillaTagger.Instance.rightHandTransform.rotation * GorillaLocomotion.Player.Instance.rightHandTransform.rotation;
            return (GorillaTagger.Instance.rightHandTransform.position + GorillaTagger.Instance.rightHandTransform.rotation * (GorillaLocomotion.Player.Instance.rightHandOffset * (scaleWithPlayer ? GorillaLocomotion.Player.Instance.transform.localScale.magnitude : 1f)), rot, rot * Vector3.up, rot * Vector3.forward, rot * Vector3.right);
        }

        public static void DestroyGun()
        {
            if (GunPointer != null)
            {
                UnityEngine.Object.Destroy(GunPointer);
                GunPointer = null;
            }
            if (GunLine != null)
            {
                UnityEngine.Object.Destroy(GunLine.gameObject);
                GunLine = null;
            }
        }


        public static GameObject GunPointer;
        private static LineRenderer GunLine;
        public static (RaycastHit Ray, GameObject NewPointer) RenderGun(int? overrideLayerMask = null)
        {
            GunSpawned = true;
			Transform gunTransform = SwapGunHand ? GorillaTagger.Instance.leftHandTransform : GorillaTagger.Instance.rightHandTransform;

			Vector3 startPos = gunTransform.position;
			Vector3 direction = gunTransform.forward;

			Vector3 up = gunTransform.up;
			Vector3 right = gunTransform.right;

			if (giveGunTarget != null)
			{
				gunTransform = SwapGunHand ? giveGunTarget.leftHandTransform : giveGunTarget.rightHandTransform;

				startPos = gunTransform.position;
				direction = gunTransform.forward;

				up = gunTransform.up;
				right = gunTransform.right;
			}

			Physics.Raycast(startPos, Quaternion.AngleAxis(45f, right) * direction, out var Ray, 512f);

			Vector3 endPos = gunLocked ? lockTarget.headMesh.transform.position : Ray.point;

			if (GunPointer == null)
				GunPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);

			GunPointer.SetActive(true);
			GunPointer.transform.localScale = smallGunPointer ? new Vector3(0.1f, 0.1f, 0.1f) : new Vector3(0.2f, 0.2f, 0.2f);
			GunPointer.transform.position = endPos;

			Renderer pointerRend = GunPointer.GetComponent<Renderer>();
			pointerRend.material.shader = Shader.Find("GUI/Text Shader");
			pointerRend.material.color = gunLocked || GetGunInput(true) ? buttonColors[1].GetCurrentColor() : buttonColors[0].GetCurrentColor();

			if (disableGunPointer)
				pointerRend.enabled = false;

            if (GunPointer.GetComponent<Collider>() != null)
                UnityEngine.Object.Destroy(GunPointer.GetComponent<Collider>());


            if (disableGunLine) return (Ray, GunPointer);
			if (GunLine == null)
			{
				GameObject line = new GameObject();
				GunLine = line.AddComponent<LineRenderer>();
			}

			GunLine.gameObject.SetActive(true);
			GunLine.material.shader = Shader.Find("GUI/Text Shader");
			GunLine.startColor = backgroundColor.GetCurrentColor();
			GunLine.endColor = backgroundColor.GetCurrentColor(0.5f);
			GunLine.startWidth = 0.02f;
			GunLine.endWidth = 0.02f;
			GunLine.useWorldSpace = true;

			GunLine.positionCount = 2;

			GunLine.SetPosition(0, startPos);
			GunLine.SetPosition(1, GunPointer.transform.position);

			return (Ray, GunPointer);
        }


        public static bool GetGunInput(bool isShooting)
        {
            if (giveGunTarget != null)
            {
                if (isShooting)
                    return TriggerlessGuns || (SwapGunHand ? giveGunTarget.leftIndex.calcT > 0.5f : giveGunTarget.rightIndex.calcT > 0.5f);
                else
                    return GriplessGuns || (SwapGunHand ? giveGunTarget.leftMiddle.calcT > 0.5f : giveGunTarget.rightMiddle.calcT > 0.5f);
            }
            if (isShooting)
                return TriggerlessGuns || (SwapGunHand ? Utility.LTrigger : Utility.RTrigger) || Mouse.current.leftButton.isPressed;
            else
                return GriplessGuns || (SwapGunHand ? Utility.LGrip : Utility.RGrip) || (HardGunLocks && gunLocked && !Utility.RSec) || Mouse.current.rightButton.isPressed;
        }

        public static Vector3 GetGunDirection(Transform transform) =>
            new[] { transform.forward, -transform.up, transform == GorillaTagger.Instance.rightHandTransform ? TrueRightHand().forward : TrueLeftHand().forward, GorillaTagger.Instance.headCollider.transform.forward }[GunDirection];
    }
}
