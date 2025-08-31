using easyInputs;
using JupiterX.Classes;
using MelonLoader.ICSharpCode.SharpZipLib.GZip;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using static JupiterX.Menu.Buttons;
using static JupiterX.Settings;
using Button = JupiterX.Classes.Button;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Menu
{
	// READ ME
	/*
	 NOTHING HERE IS SKIDDED
	 I HAVE GAVE CREDITS TO EVERYONE I NEED TO
     Extra Credits: Dom - Soundboard 
	 If you need help with building plz dm me (@s1lnt)
	 If you would like to become a contributer clone this src and help out
	 PLEASE Do not skid anythere here
	 Follow the GPL Licence
	 Have a good day
	 IF YOU DO TAKE CODE
	 Make sure to credit me :)
	 */

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

			gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

			UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
			gameObject.transform.parent = menu.transform;
			gameObject.transform.rotation = Quaternion.identity;
			gameObject.transform.localScale = Utility.PageObjScale;
			gameObject.transform.localPosition = Utility.PageObjectPosRight;
			gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
			gameObject.AddComponent<Classes.Button>().relatedText = "PreviousPage"; // NextPage

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
					TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
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

        public static bool smoothLines;

        private static List<float> volumeArchive = new List<float> { };
        private static Vector3 GunPositionSmoothed = Vector3.zero;

        private static GameObject GunPointer;
        private static LineRenderer GunLine;

        public static (RaycastHit Ray, GameObject NewPointer) RenderGun(int? overrideLayerMask = null)
        {
            GunSpawned = true;
            Transform GunTransform = SwapGunHand ? GorillaTagger.Instance.leftHandTransform : GorillaTagger.Instance.rightHandTransform;

            Vector3 StartPosition = GunTransform.position;
            Vector3 Direction = GunTransform.forward;

            Vector3 Up = -GunTransform.up;
            Vector3 Right = GunTransform.right;

            switch (GunDirection)
            {
                case 1:
                    Up = GunTransform.forward;
                    Direction = -GunTransform.up;
                    break;
                case 2:
                    Up = GunTransform.forward;
                    Right = -GunTransform.up;
                    Direction = GunTransform.right * (SwapGunHand ? 1f : -1f);
                    break;
                case 3:
                    Up = SwapGunHand ? TrueLeftHand().up : TrueRightHand().up;
                    Right = SwapGunHand ? TrueLeftHand().right : TrueRightHand().right;
                    Direction = SwapGunHand ? TrueLeftHand().forward : TrueRightHand().forward;
                    break;
                case 4:
                    Up = GorillaTagger.Instance.headCollider.transform.up;
                    Right = GorillaTagger.Instance.headCollider.transform.right;
                    Direction = GorillaTagger.Instance.headCollider.transform.forward;
                    StartPosition = GorillaTagger.Instance.headCollider.transform.position + (Up * 0.1f);
                    break;
            }

            if (giveGunTarget != null)
            {
                GunTransform = SwapGunHand ? giveGunTarget.leftHandTransform : giveGunTarget.rightHandTransform;

                StartPosition = GunTransform.position;
                Direction = GunTransform.up;

                Up = GunTransform.forward;
                Right = GunTransform.right;
            }

            Physics.Raycast(StartPosition + ((Direction / 4f) * (scaleWithPlayer ? GorillaLocomotion.Player.Instance.transform.localScale.magnitude : 1f)), Direction, out var Ray, 512f, overrideLayerMask ?? NoInvisLayerMask());

            Vector3 EndPosition = gunLocked ? lockTarget.headMesh.transform.position : Ray.point;

            if (EndPosition == Vector3.zero)
                EndPosition = StartPosition + (Direction * 512f);

            if (SmoothGunPointer)
            {
                GunPositionSmoothed = Vector3.Lerp(GunPositionSmoothed, EndPosition, Time.deltaTime * 6f);
                EndPosition = GunPositionSmoothed;
            }

            if (GunPointer == null)
                GunPointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            GunPointer.SetActive(true);
            GunPointer.transform.localScale = (smallGunPointer ? new Vector3(0.1f, 0.1f, 0.1f) : new Vector3(0.2f, 0.2f, 0.2f)) * (scaleWithPlayer ? GorillaLocomotion.Player.Instance.transform.localScale.magnitude : 1f);
            GunPointer.transform.position = EndPosition;

            Renderer PointerRenderer = GunPointer.GetComponent<Renderer>();
            PointerRenderer.material.shader = Shader.Find("GUI/Text Shader");
            PointerRenderer.material.color = (gunLocked || GetGunInput(true)) ? Color.red : Color.cyan;

            if (disableGunPointer)
                PointerRenderer.enabled = false;

            if (GunParticles && (GetGunInput(true) || gunLocked))
            {
                GameObject Particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Particle.transform.position = EndPosition;
                Particle.transform.localScale = Vector3.one * 0.025f * (scaleWithPlayer ? GorillaLocomotion.Player.Instance.transform.localScale.magnitude : 1f);
                Particle.GetComponent<Renderer>().material.shader = Shader.Find("GUI/Text Shader");
                GameObject.Destroy(Particle.GetComponent<Collider>());
            }

            GameObject.Destroy(GunPointer.GetComponent<Collider>());

            if (!disableGunLine)
            {
                if (GunLine == null)
                {
                    GameObject line = new GameObject("iiMenu_GunLine");
                    GunLine = line.AddComponent<LineRenderer>();
                }

                GunLine.gameObject.SetActive(true);
                GunLine.material.shader = Shader.Find("GUI/Text Shader");
                GunLine.startColor = Color.gray;
                GunLine.endColor = Color.gray;
                GunLine.startWidth = 0.025f * (scaleWithPlayer ? GorillaLocomotion.Player.Instance.transform.localScale.magnitude : 1f);
                GunLine.endWidth = 0.025f * (scaleWithPlayer ? GorillaLocomotion.Player.Instance.transform.localScale.magnitude : 1f);
                GunLine.positionCount = 2;
                GunLine.useWorldSpace = true;
                if (smoothLines)
                {
                    GunLine.numCapVertices = 10;
                    GunLine.numCornerVertices = 5;
                }
                GunLine.SetPosition(0, StartPosition);
                GunLine.SetPosition(1, EndPosition);

                int Step = GunLineQuality;
                switch (gunVariation)
                {
                    case 1: // Lightning
                        if (GetGunInput(true) || gunLocked)
                        {
                            GunLine.positionCount = Step;
                            GunLine.SetPosition(0, StartPosition);

                            for (int i = 1; i < (Step - 1); i++)
                            {
                                Vector3 Position = Vector3.Lerp(StartPosition, EndPosition, i / (Step - 1f));
                                GunLine.SetPosition(i, Position + (UnityEngine.Random.Range(0f, 1f) > 0.75f ? new Vector3(UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f), UnityEngine.Random.Range(-0.1f, 0.1f)) : Vector3.zero));
                            }

                            GunLine.SetPosition(Step - 1, EndPosition);
                        }
                        break;
                    case 2: // Wavy
                        if (GetGunInput(true) || gunLocked)
                        {
                            GunLine.positionCount = Step;
                            GunLine.SetPosition(0, StartPosition);

                            for (int i = 1; i < (Step - 1); i++)
                            {
                                float value = ((float)i / (float)Step) * 50f;

                                Vector3 Position = Vector3.Lerp(StartPosition, EndPosition, i / (Step - 1f));
                                GunLine.SetPosition(i, Position + (Up * Mathf.Sin((Time.time * -10f) + value) * 0.1f));
                            }

                            GunLine.SetPosition(Step - 1, EndPosition);
                        }
                        break;
                    case 3: // Blocky
                        if (GetGunInput(true) || gunLocked)
                        {
                            GunLine.positionCount = Step;
                            GunLine.SetPosition(0, StartPosition);

                            for (int i = 1; i < (Step - 1); i++)
                            {
                                Vector3 Position = Vector3.Lerp(StartPosition, EndPosition, i / (Step - 1f));
                                GunLine.SetPosition(i, new Vector3(Mathf.Round(Position.x * 25f) / 25f, Mathf.Round(Position.y * 25f) / 25f, Mathf.Round(Position.z * 25f) / 25f));
                            }

                            GunLine.SetPosition(Step - 1, EndPosition);
                        }
                        break;
                    case 4: // Sinewave
                        Step = GunLineQuality / 2;

                        if (GetGunInput(true) || gunLocked)
                        {
                            GunLine.positionCount = Step;
                            GunLine.SetPosition(0, StartPosition);

                            for (int i = 1; i < (Step - 1); i++)
                            {
                                Vector3 Position = Vector3.Lerp(StartPosition, EndPosition, i / (Step - 1f));
                                GunLine.SetPosition(i, Position + (Up * Mathf.Sin(Time.time * 10f) * (i % 2 == 0 ? 0.1f : -0.1f)));
                            }

                            GunLine.SetPosition(Step - 1, EndPosition);
                        }
                        break;
                    case 5: // Spring
                        if (GetGunInput(true) || gunLocked)
                        {
                            GunLine.positionCount = Step;
                            GunLine.SetPosition(0, StartPosition);

                            for (int i = 1; i < (Step - 1); i++)
                            {
                                float value = ((float)i / (float)Step) * 50f;

                                Vector3 Position = Vector3.Lerp(StartPosition, EndPosition, i / (Step - 1f));
                                GunLine.SetPosition(i, Position + (Right * Mathf.Cos((Time.time * -10f) + value) * 0.1f) + (Up * Mathf.Sin((Time.time * -10f) + value) * 0.1f));
                            }

                            GunLine.SetPosition(Step - 1, EndPosition);
                        }
                        break;
                    case 6: // Bouncy
                        if (GetGunInput(true) || gunLocked)
                        {
                            GunLine.positionCount = Step;
                            GunLine.SetPosition(0, StartPosition);

                            for (int i = 1; i < (Step - 1); i++)
                            {
                                float value = ((float)i / (float)Step) * 15f;
                                GunLine.SetPosition(i, Vector3.Lerp(StartPosition, EndPosition, i / (Step - 1f)) + (Up * Mathf.Abs(Mathf.Sin((Time.time * -10f) + value)) * 0.3f));
                            }

                            GunLine.SetPosition(Step - 1, EndPosition);
                        }
                        break;
                    case 7: // Audio
                        if (GetGunInput(true) || gunLocked)
                        {

                            volumeArchive.Insert(0, volumeArchive.Count == 0 ? 0 : (5 - volumeArchive[0] * 0.1f));

                            if (volumeArchive.Count > Step)
                                volumeArchive.Remove(Step);

                            GunLine.positionCount = Step;
                            GunLine.SetPosition(0, StartPosition);

                            for (int i = 1; i < (Step - 1); i++)
                            {
                                Vector3 Position = Vector3.Lerp(StartPosition, EndPosition, i / (Step - 1f));
                                GunLine.SetPosition(i, Position + (Up * (i >= volumeArchive.Count ? 0 : volumeArchive[i]) * (i % 2 == 0 ? 1f : -1f)));
                            }

                            GunLine.SetPosition(Step - 1, EndPosition);
                        }
                        break;
                    case 8: // Bezier, credits to Crisp / Kman / Steal / Untitled One of those 4 I don't really know who
                        Vector3 BaseMid = Vector3.Lerp(StartPosition, EndPosition, 0.5f);

                        float angle = Time.time * 3f;
                        Vector3 wobbleOffset = Mathf.Sin(angle) * Up * 0.15f + Mathf.Cos(angle * 1.3f) * Right * 0.15f;
                        Vector3 targetMid = BaseMid + wobbleOffset;

                        if (MidPosition == Vector3.zero) MidPosition = targetMid;

                        Vector3 force = (targetMid - MidPosition) * 40f;
                        MidVelocity += force * Time.deltaTime;
                        MidVelocity *= Mathf.Exp(-6f * Time.deltaTime);
                        MidPosition += MidVelocity * Time.deltaTime;

                        GunLine.positionCount = Step;
                        GunLine.SetPosition(0, StartPosition);

                        Vector3[] points = new Vector3[Step];
                        for (int i = 0; i < Step; i++)
                        {
                            float t = (float)i / (Step - 1);
                            points[i] = Mathf.Pow(1 - t, 2) * StartPosition +
                                        2 * (1 - t) * t * MidPosition +
                                        Mathf.Pow(t, 2) * EndPosition;
                        }

                        GunLine.positionCount = Step;
                        GunLine.SetPositions(points);
                        break;
                }
            }

            return (Ray, GunPointer);
        }

		public static void DestroyGun()
		{
			if (GunPointer != null)
				GameObject.Destroy(GunPointer);
			if (GunLine != null)
				GameObject.Destroy(GunLine);
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
