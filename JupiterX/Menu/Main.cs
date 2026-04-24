using easyInputs;
using JupiterX.Classes;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
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
				Utility.toOpen = bothHands ? (Utility.LSec || Utility.RSec) : (!RightHanded && Utility.LSec || (RightHanded && Utility.RSec));
				bool keyboardOpen = false;

				if (menu == null)
				{
					if (Utility.toOpen || keyboardOpen)
					{
                        Utility.PlayEmbeddedSoundOnHand("JupiterX.Resources.menuopen.wav");
                        CreateMenu();

                        RecenterMenu(RightHanded, keyboardOpen);
						if (reference == null)
						{
							CreateReference(RightHanded);
						}
					}
				}
				else
				{
					if ((Utility.toOpen || keyboardOpen))
					{
						RecenterMenu(RightHanded, keyboardOpen);
					}
					else
					{
						Rigidbody comp = menu.AddComponent<Rigidbody>();

                        switch (Utility.MainDropType) 
                        {
                            case 0:
                                UnityEngine.Object.Destroy(menu, Time.deltaTime);
                                menu = null;

                                UnityEngine.Object.Destroy(reference);
                                reference = null;
                                break; // Destroy
                            case 1: // Drop
                                comp.velocity = Vector3.zero;
                                UnityEngine.Object.Destroy(menu, 5);
                                menu = null;

                                UnityEngine.Object.Destroy(reference);
                                reference = null;
                                break;
                            case 2: // Drop
                                comp.useGravity = false;
                                comp.velocity = RightHanded ? Utility.ThrowMenu(Utility.RightHand) : Utility.ThrowMenu(Utility.LeftHand);
                                UnityEngine.Object.Destroy(menu, 5);
                                menu = null;

                                UnityEngine.Object.Destroy(reference);
                                reference = null;
                                break;
                            case 3: // Throw
                                comp.velocity = RightHanded ? Utility.ThrowMenu(Utility.RightHand) : Utility.ThrowMenu(Utility.LeftHand);
                                UnityEngine.Object.Destroy(menu, 5);
                                menu = null;

                                UnityEngine.Object.Destroy(reference);
                                reference = null;
                                break;
                        }

                        Utility.PlayEmbeddedSoundOnHand("JupiterX.Resources.menuclose.wav");
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
                    if (!disableRoomNotifications)
                        NotificationManager.SendNotification("blue", "JOIN ROOM", "Room Code: " + lastRoom + "");
                }
                if (!PhotonNetwork.InRoom && lastInRoom)
                {
                    if (clearNotificationsOnDisconnect)
                        NotificationManager.ClearAllNotifications();

                    if (!disableRoomNotifications)
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
                    if (!PhotonNetwork.LocalPlayer.IsMasterClient)
                        GetIndex("MasterLabel").overlapText = "You are not master client.";
                    else
                        GetIndex("MasterLabel").overlapText = "You are master client.";

                    if (PhotonNetwork.LocalPlayer.IsMasterClient && !lastMasterClient)
                    {
                        if (disableMasterClientNotifications)
                            return;
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

            if (menuTrail)
            {
                try
                {
                    TrailRenderer trail = menu.AddComponent<TrailRenderer>();

                    trail.startColor = backgroundColor.GetColor(0);
                    trail.endColor = backgroundColor.GetColor(1);
                    trail.startWidth = 0.015f;
                    trail.endWidth = 0f;
                    trail.minVertexDistance = 0.05f;

                    trail.material.shader = Shader.Find("Sprites/Default");
                    trail.time = 2f;
                }
                catch { }
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
			menuBackground.transform.position = new Vector3(0.05f, 0f, 0f);

            if (Rounding)
                RoundMenuObject(menuBackground);

            ColorChanger colorChanger = menuBackground.AddComponent<ColorChanger>();
            colorChanger.colors = backgroundColor;
            colorChanger.Start();

            if (menuoutline)
            {
                GameObject outlineMenu = GameObject.CreatePrimitive(PrimitiveType.Cube);
                outlineMenu.transform.parent = menu.transform;
                UnityEngine.Object.Destroy(outlineMenu.GetComponent<Rigidbody>());
                UnityEngine.Object.Destroy(outlineMenu.GetComponent<BoxCollider>());
                outlineMenu.transform.localScale = new Vector3(0.08f, 1.05f, 1.05f);
                outlineMenu.transform.position = new Vector3(0.05f, 0f, 0f);
                outlineMenu.GetComponent<Renderer>().material.color = buttonColors[0].GetCurrentColor();
            }

			// Canvas
			canvasObject = new GameObject();
			canvasObject.transform.parent = menu.transform;
			Canvas canvas = canvasObject.AddComponent<Canvas>();
			CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
			canvasObject.AddComponent<GraphicRaycaster>();
			canvas.renderMode = RenderMode.WorldSpace;
			canvasScaler.dynamicPixelsPerUnit = lowqualttext ? 1000f : 2500f;

			// Title and FPS
			Text text = new GameObject
			{
				transform =
					{
						parent = canvasObject.transform
					}
			}.AddComponent<Text>();
			text.font = currentFont;

            if (CustomMenuTitle)
            {
                if (MenuTitle)
                {
                    string path = Path.Combine(Application.persistentDataPath, "JupiterX/CustomTitle.txt");
                    string CustomTitle;
                    if (File.Exists(path))
                    {
                        CustomTitle = File.ReadAllText(path);
                    }
                    else
                    {
                        CustomTitle = "Your Text Here";
                        Directory.CreateDirectory(Path.GetDirectoryName(path));
                        File.WriteAllText(path, CustomTitle);
                    }
                    text.text = CustomTitle + " <color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";
                }
                else
                    text.text = "";
            }
            else
            {
                if (MenuTitle)
                    text.text = Utility.name + " <color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";
                else
                    text.text = "";
            }

            if (lowercaseMode)
                text.text = text.text.ToLower();
            if (uppercaseMode)
                text.text = text.text.ToUpper();

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
            if (NoAutoSizeText)
                component.sizeDelta = new Vector2(9f, 0.015f);
            component.position = new Vector3(0.06f, 0f, 0.165f);
			component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

			if (VersionText)
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

                if (lowercaseMode)
                    fpsObject.text = fpsObject.text.ToLower();
                if (uppercaseMode)
                    fpsObject.text = fpsObject.text.ToUpper();

                RectTransform component2 = fpsObject.GetComponent<RectTransform>();
				component2.localPosition = Vector3.zero;
				component2.sizeDelta = new Vector2(0.28f, 0.02f);
                if (NoAutoSizeText)
                    component2.sizeDelta = new Vector2(9f, 0.015f);
                component2.position = new Vector3(0.06f, 0f, 0.135f);
				component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
			}

            float hkbStartTime = -0.3f;

            if (!DisconnectButton)
            {
                AddButton(-0.3f, -1, GetIndex("Disconnect"));
                hkbStartTime -= 0.1f;
            }


            if (quickActions.Count > 0)
            {
                foreach (string action in quickActions)
                {
                    ButtonInfo button = GetIndex(action);
                    if (button == null)
                    {
                        quickActions.Remove(action);
                        continue;
                    }

                    AddButton(hkbStartTime, -1, button);
                    hkbStartTime -= 0.1f;
                }
            }

            // Buttons
            
			// Page Buttons
			GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

			UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
			gameObject.GetComponent<BoxCollider>().isTrigger = true;
			gameObject.transform.parent = menu.transform;
			gameObject.transform.rotation = Quaternion.identity;
			gameObject.transform.localScale = Utility.PageObjScale;
			gameObject.transform.localPosition = Utility.PageObjectPosLeft;
            gameObject.AddComponent<ButtonCollider>().relatedText = "NextPage"; // PreviousPage

            colorChanger = gameObject.AddComponent<ColorChanger>();
			colorChanger.colors = buttonColors[0];
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
            if (NoAutoSizeText)
                component.sizeDelta = new Vector2(9f, 0.015f);
            component.localPosition = Utility.PageTextPosLeft; ;
			component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

			GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Cube);

			UnityEngine.Object.Destroy(gameObject2.GetComponent<Rigidbody>());
            gameObject2.GetComponent<BoxCollider>().isTrigger = true;
            gameObject2.transform.parent = menu.transform;
            gameObject2.transform.rotation = Quaternion.identity;
            gameObject2.transform.localScale = Utility.PageObjScale;
            gameObject2.transform.localPosition = Utility.PageObjectPosRight;
            gameObject2.AddComponent<ButtonCollider>().relatedText = "PreviousPage"; // NextPage

            colorChanger = gameObject2.AddComponent<ColorChanger>();
			colorChanger.colors = buttonColors[0];
			colorChanger.Start();

            if (lowercaseMode)
                text.text = text.text.ToLower();
            if (uppercaseMode)
                text.text = text.text.ToUpper();

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
            if (NoAutoSizeText)
                component.sizeDelta = new Vector2(9f, 0.015f);
            component.localPosition = Utility.PageTextPosRight;
			component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

			if (Rounding)
			{
				RoundMenuObject(gameObject);
				RoundMenuObject(gameObject2);
			}

            int buttonIndexOffset = 0;
            ButtonInfo[] renderButtons = new ButtonInfo[] { };

            if (CurrentPrompt != null)
                RenderPrompt();
            else
            {
                if (Buttons.CurrentCategoryName == "Favorite")
                {
                    foreach (string favoriteMod in favorites)
                    {
                        if (GetIndex(favoriteMod) == null)
                            favorites.Remove(favoriteMod);
                    }

                    renderButtons = StringsToInfos(favorites.ToArray());
                }
                else if (Buttons.CurrentCategoryName == "Enabled")
                {
                    List<ButtonInfo> enabledMods = new List<ButtonInfo>() { };
                    int categoryIndex = 0;
                    foreach (ButtonInfo[] buttonlist in Buttons.buttons)
                    {
                        foreach (ButtonInfo v in buttonlist)
                        {
                            if (v.enabled && (!Buttons.categoryNames[categoryIndex].Contains("Settings")))
                                enabledMods.Add(v);
                        }
                        categoryIndex++;
                    }
                    enabledMods = enabledMods.OrderBy(v => v.buttonText).ToList();
                    enabledMods.Insert(0, GetIndex("Exit Enabled"));

                    renderButtons = enabledMods.ToArray();
                }
                else
                    renderButtons = Buttons.buttons[Buttons.CurrentCategoryIndex];

                renderButtons = renderButtons.Skip(pageNumber * (buttonsPerPage - buttonIndexOffset)).Take(buttonsPerPage - buttonIndexOffset).ToArray();

                // Mod Buttons
                for (int i = 0; i < renderButtons.Length; i++)
                    AddButton((i + buttonIndexOffset + 0.1f) * 0.1f, i, renderButtons[i]);
            }
        }

        private static void AddButton(float offset, int buttonIndex, ButtonInfo method)
        {
            if (!method.label)
            {
                GameObject buttonObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                buttonObject.GetComponent<BoxCollider>().isTrigger = true;
                buttonObject.transform.parent = menu.transform;
                buttonObject.transform.rotation = Quaternion.identity;

                buttonObject.transform.localScale = new Vector3(0.09f, 0.9f, 0.1f * 0.8f);

                buttonObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);

                ButtonCollider Button = buttonObject.AddComponent<ButtonCollider>();
                Button.relatedText = method.buttonText;

                ColorChanger colorChanger = buttonObject.AddComponent<ColorChanger>();
                colorChanger.Start();

                if (incrementalButtons)
                {
                    if (method.incremental)
                    {
                        buttonObject.transform.localScale -= new Vector3(0f, 0.254f, 0f);
                        GameObject.Destroy(Button);

                        RenderIncrementalButton(false, offset, buttonIndex, method);
                        RenderIncrementalButton(true, offset, buttonIndex, method);
                    }
                }

                if (lastClickedName != method.buttonText)
                {
                    if (method.enabled)
                    {
                        buttonObject.GetComponent<Renderer>().material.color = buttonColors[1].GetCurrentColor();
                    }
                    else
                    {
                        buttonObject.GetComponent<Renderer>().material.color = buttonColors[0].GetCurrentColor();
                    }
                }

                if (Rounding)
                    RoundMenuObject(buttonObject);
            }

            Text buttonText = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();

            buttonText.font = currentFont;
            buttonText.text = method.buttonText;

            if (lowercaseMode)
                buttonText.text = buttonText.text.ToLower();
            if (uppercaseMode)
                buttonText.text = buttonText.text.ToUpper();

            if (method.overlapText != null)
                buttonText.text = method.overlapText;

            if (favorites.Contains(method.buttonText))
                buttonText.text += " ✦";

            buttonText.supportRichText = true;
            buttonText.fontSize = 1;
            buttonText.color = method.enabled ? textColors[1] : textColors[0];

            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.fontStyle = FontStyle.Italic;
            buttonText.resizeTextForBestFit = true;
            buttonText.resizeTextMinSize = 0;

            RectTransform textTransform = buttonText.GetComponent<RectTransform>();
            textTransform.localPosition = Vector3.zero;
            textTransform.sizeDelta = new Vector2(method.incremental && incrementalButtons ? .18f : .2f, .03f * (0.1f / 0.1f));
            if (NoAutoSizeText)
                textTransform.sizeDelta = new Vector2(9f, 0.015f);

            textTransform.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            textTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        private static void RenderIncrementalButton(bool increment, float offset, int buttonIndex, ButtonInfo method)
        {
            if (!method.label)
            {
                GameObject buttonObject = GameObject.CreatePrimitive(PrimitiveType.Cube);

                buttonObject.GetComponent<BoxCollider>().isTrigger = true;
                buttonObject.transform.parent = menu.transform;
                buttonObject.transform.rotation = Quaternion.identity;

                buttonObject.transform.localScale = new Vector3(0.09f, 0.102f, 0.1f * 0.8f);
                buttonObject.transform.localPosition = new Vector3(0.56f, 0.399f, 0.28f - offset);

                ButtonCollider button = buttonObject.AddComponent<ButtonCollider>();
                button.relatedText = method.buttonText;
                button.incremental = true;
                button.positive = increment;

                if (increment)
                    buttonObject.transform.localPosition = new Vector3(buttonObject.transform.localPosition.x, -buttonObject.transform.localPosition.y, buttonObject.transform.localPosition.z);

                if (lastClickedName != method.buttonText + (increment ? "+" : "-"))
                {
                    ColorChanger colorChanger = buttonObject.AddComponent<ColorChanger>();
                    colorChanger.colors = buttonColors[0];
                }
            }

            RenderIncrementalText(increment, offset);
        }

        public static void RenderIncrementalText(bool increment, float offset)
        {
            Text buttonText = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();

            buttonText.font = currentFont;
            buttonText.text = increment ? "+" : "-";
            buttonText.supportRichText = true;
            buttonText.fontSize = 1;
            buttonText.color = textColors[1];

            buttonText.alignment = TextAnchor.MiddleCenter;
            buttonText.fontStyle = FontStyle.Italic;
            buttonText.resizeTextForBestFit = true;
            buttonText.resizeTextMinSize = 0;

            RectTransform textTransform = buttonText.GetComponent<RectTransform>();
            textTransform.localPosition = Vector3.zero;
            textTransform.sizeDelta = new Vector2(.2f, .03f * (0.1f / 0.1f));

            textTransform.localPosition = new Vector3(.064f, increment ? -0.12f : 0.12f, .111f - offset / 2.6f);
            textTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void ReloadMenu()
		{
			if (menu != null)
			{
				UnityEngine.Object.Destroy(menu);
				menu = null;

				CreateMenu();
				RecenterMenu(RightHanded, false);
			}
		}

		public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
		{
			if (!isKeyboardCondition)
			{
				if (isRightHanded || (bothHands && Utility.RSec))
				{
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
				}
				else
				{
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                if (flipMenu)
                {
                    Vector3 rotation = menu.transform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }
            }
			else
			{
				try
				{
					TPC = GameObject.Find("Shoulder Camera").GetComponent<Camera>();
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
								ButtonCollider collide = hit.transform.gameObject.GetComponent<ButtonCollider>();
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
            if (bothHands)
            {
                if (Utility.RSec)
                    reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
                else
                    reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
            }
			else if (isRightHanded)
			{
				reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
			}
			else
			{
				reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
			}
			reference.transform.localPosition = new Vector3(0.013f, -0.025f, 0.1f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
			buttonCollider = reference.GetComponent<SphereCollider>();

			ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
			colorChanger.colors = backgroundColor;
			colorChanger.Start();
		}

        public static void Toggle(string buttonText, bool fromMenu = false, bool ignoreForce = false)
        {
            int lastPage = ((Buttons.buttons[Buttons.CurrentCategoryIndex].Length + buttonsPerPage - 1) / buttonsPerPage) - 1;

            switch (Buttons.CurrentCategoryName)
            {
                case "Favorite":
                    lastPage = ((favorites.Count + buttonsPerPage - 1) / buttonsPerPage) - 1;
                    break;

                case "Enabled":
                    List<string> enabledMods = new List<string>() { "Exit Enabled" };
                    int categoryIndex = 0;

                    foreach (ButtonInfo[] buttonlist in Buttons.buttons)
                    {
                        foreach (ButtonInfo v in buttonlist)
                        {
                            if (v.enabled && !Buttons.categoryNames[categoryIndex].Contains("Settings"))
                                enabledMods.Add(v.buttonText);
                        }
                        categoryIndex++;
                    }

                    lastPage = ((enabledMods.Count + buttonsPerPage - 1) / buttonsPerPage) - 1;
                    break;
            }

            switch (buttonText)
            {
                case "Accept Prompt":
                    if (CurrentPrompt != null)
                    {
                        CurrentPrompt.AcceptAction?.Invoke();
                        if (prompts.Count > 0) prompts.RemoveAt(0);
                        ReloadMenu();
                    }
                    return;

                case "Decline Prompt":
                    if (CurrentPrompt != null)
                    {
                        CurrentPrompt.DeclineAction?.Invoke();
                        if (prompts.Count > 0) prompts.RemoveAt(0);
                        ReloadMenu();
                    }
                    return;

                case "Disconnect":
                    PhotonNetwork.Disconnect();
                    break;

                case "Home":
                    Buttons.CurrentCategoryName = "Main";
                    pageNumber = 0;
                    break;

                case "PreviousPage":
                    pageNumber = (pageNumber - 1 < 0) ? lastPage : pageNumber - 1;
                    break;

                case "NextPage":
                    pageNumber = (pageNumber + 1 > lastPage) ? 0 : pageNumber + 1;
                    break;

                default:
                    HandleButtonAction(buttonText, fromMenu, ignoreForce);
                    break;
            }

            ReloadMenu();
        }

        private static void HandleButtonAction(string buttonText, bool fromMenu, bool ignoreForce)
        {
            ButtonInfo target = GetIndex(buttonText);
            if (target == null)
            {
                MelonLoader.MelonLogger.Msg($"{buttonText} does not exist");
                return;
            }
            string newIndicator = " <color=grey>[</color><color=green>New</color><color=grey>]</color>";
            if (target.overlapText != null && target.overlapText.Contains(newIndicator))
            {
                target.overlapText = target.overlapText.Replace(newIndicator, "");
            }
            bool gripHeld = Utility.LGrip || (Utility.RJoystickAxis.y > 0.5f && Utility.LTriggerFloat > 0.5f);
            bool triggerHeld = Utility.LTriggerFloat > 0.5f;

            switch (true)
            {
                case true when fromMenu && !ignoreForce && gripHeld:
                    if (target.buttonText == "Exit Favorite") return;

                    if (favorites.Contains(target.buttonText))
                    {
                        favorites.Remove(target.buttonText);
                        NotificationManager.SendNotification2("<color=grey>[</color><color=yellow>FAVORITES</color><color=grey>]</color> Removed from favorites.");
                    }
                    else
                    {
                        favorites.Add(target.buttonText);
                        NotificationManager.SendNotification2("<color=grey>[</color><color=yellow>FAVORITES</color><color=grey>]</color> Added to favorites.");
                    }
                    break;
                case true when fromMenu && !ignoreForce && triggerHeld:
                    if (!quickActions.Contains(target.buttonText))
                    {
                        quickActions.Add(target.buttonText);
                        NotificationManager.SendNotification2("<color=grey>[</color><color=purple>QUICK ACTIONS</color><color=grey>]</color> Added quick action button.");
                    }
                    else
                    {
                        quickActions.Remove(target.buttonText);
                        NotificationManager.SendNotification2("<color=grey>[</color><color=purple>QUICK ACTIONS</color><color=grey>]</color> Removed quick action button.");
                    }
                    break;
                default:
                    if (target.isTogglable)
                    {
                        target.enabled = !target.enabled;

                        if (target.enabled)
                        {
                            if (fromMenu)
                                NotificationManager.SendNotification2($"<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> {target.toolTip}");

                            try { target.enableMethod?.Invoke(); }
                            catch (Exception exc)
                            {
                                MelonLoader.MelonLogger.Msg($"Error enabling {target.buttonText}: {exc.Message}");
                            }
                        }
                        else
                        {
                            if (fromMenu)
                                NotificationManager.SendNotification2($"<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> {target.toolTip}");
                            try { target.disableMethod?.Invoke(); }
                            catch (Exception exc)
                            {
                                MelonLoader.MelonLogger.Msg($"Error disabling {target.buttonText}: {exc.Message}");
                            }
                        }
                    }
                    else
                    {
                        if (fromMenu)
                            NotificationManager.SendNotification2($"<color=grey>[</color><color=green>RUN</color><color=grey>]</color> {target.toolTip}");

                        try { target.method?.Invoke(); }
                        catch (Exception exc)
                        {
                            MelonLoader.MelonLogger.Msg($"Error running {target.buttonText}: {exc.Message}");
                        }
                    }
                    break;
            }
        }

        public static void ToggleIncremental(string buttonText, bool increment, bool fromMenu = false, bool ignoreForce = false, bool reload = true)
        {
            ButtonInfo target = Buttons.GetIndex(buttonText);
            if (target == null)
            {
                Utility.Log($"{buttonText} does not exist");
                return;
            }
            string newIndicator = " <color=grey>[</color><color=green>New</color><color=grey>]</color>";
            if (target.overlapText != null && target.overlapText.Contains(newIndicator))
            {
                target.overlapText = target.overlapText.Replace(newIndicator, "");
            }
            if (target.label)
                return;
            bool triggerHeld = Utility.LTriggerFloat > 0.5f;
            switch (true)
            {
                case true when fromMenu && !ignoreForce && triggerHeld:
                    if (!quickActions.Contains(target.buttonText))
                    {
                        quickActions.Add(target.buttonText);
                        NotificationManager.SendNotification2("<color=grey>[</color><color=purple>QUICK ACTIONS</color><color=grey>]</color> Added quick action button.");
                    }
                    else
                    {
                        quickActions.Remove(target.buttonText);
                        NotificationManager.SendNotification2("<color=grey>[</color><color=purple>QUICK ACTIONS</color><color=grey>]</color> Removed quick action button.");
                    }
                    break;
                default:
                    if (increment)
                    {
                        NotificationManager.SendNotification2($"<color=grey>[</color><color=green>INCREMENT</color><color=grey>]</color> {target.toolTip}");
                        try { target.enableMethod?.Invoke(); }
                        catch (Exception exc)
                        {
                            Utility.Log($"Error enabling {target.buttonText}: {exc.Message}");
                        }
                    }
                    else
                    {
                        NotificationManager.SendNotification2($"<color=grey>[</color><color=red>DECREMENT</color><color=grey>]</color> {target.toolTip}");
                        try { target.disableMethod?.Invoke(); }
                        catch (Exception exc)
                        {
                            Utility.Log($"Error disabling {target.buttonText}: {exc.Message}");
                        }
                    }
                    break;
            }
            if (reload)
                ReloadMenu();
        }

        public static Material promptMat;

        public static string lastClickedName = "";
        public static GradientColorKey[] GetSolidGradient(Color color)
		{
			return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
		}

        // Prompt Stuff
        public static string ExtractPromptImage(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;
            var match = System.Text.RegularExpressions.Regex.Match(input, @"<(?<url>https?://[^>]+)>");
            if (match.Success)
                return match.Groups["url"].Value;
            return null;
        }

        public static string GetFileExtension(string fileName) =>
            fileName.ToLower().Split('.')[fileName.Split('.').Length - 1];

        private static void RenderPrompt()
        {
            if (CurrentPrompt == null)
                return;

            Text promptText = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            promptText.font = currentFont;
            promptText.text = CurrentPrompt.Message;

            string promptImageUrl = ExtractPromptImage(CurrentPrompt.Message);
            if (promptImageUrl != null)
                promptText.text = promptText.text.Replace($"<{promptImageUrl}>", "");

            promptText.fontSize = 1;
            promptText.lineSpacing = 0.8f;
            promptText.color = textColors[0];

            promptText.supportRichText = true;
            promptText.fontStyle = FontStyle.Normal;
            promptText.alignment = TextAnchor.MiddleCenter;
            promptText.resizeTextForBestFit = true;
            promptText.resizeTextMinSize = 0;
            RectTransform component = promptText.GetComponent<RectTransform>();
            component.sizeDelta = new Vector2(0.28f, CurrentPrompt.IsText ? 0.25f : 0.28f);

            component.localPosition = new Vector3(0.06f, 0f, CurrentPrompt.IsText ? -0.025f : 0f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (promptImageUrl != null)
            {
                string fileName = promptImageUrl.Split('/')[^1];
                string fileExtension = GetFileExtension(fileName);

                Image promptImage = new GameObject
                {
                    transform =
                    {
                        parent = canvasObject.transform
                    }
                }.AddComponent<Image>();

                component.sizeDelta = new Vector2(component.sizeDelta.x, 0.03f);
                component.localPosition = new Vector3(0.06f, 0f, 0.1f);

                if (promptMat == null)
                    promptMat = new Material(promptImage.material);

                promptImage.material = promptMat;

                RectTransform imageTransform = promptImage.GetComponent<RectTransform>();
                imageTransform.localPosition = Vector3.zero;
                imageTransform.sizeDelta = new Vector2(.2f, .2f);

                imageTransform.localPosition = new Vector3(0.06f, 0f, string.IsNullOrEmpty(promptText.text) ? 0f : -0.03f);
                imageTransform.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            }
            {
                GameObject button = GameObject.CreatePrimitive(PrimitiveType.Cube);

                button.GetComponent<BoxCollider>().isTrigger = true;
                button.transform.parent = menu.transform;
                button.transform.rotation = Quaternion.identity;
                button.transform.localScale = new Vector3(0.09f, CurrentPrompt.DeclineText == null ? 0.9f : 0.4375f, 0.08f);
                button.transform.localPosition = new Vector3(0.56f, CurrentPrompt.DeclineText == null ? 0f : 0.2375f, -0.43f);

                button.AddComponent<ButtonCollider>().relatedText = "Accept Prompt";

                if (lastClickedName != "Accept Prompt")
                {
                    ColorChanger colorChanger = button.AddComponent<ColorChanger>();
                    colorChanger.colors = buttonColors[0];
                }

                Text text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<Text>();
                text.font = currentFont;
                text.fontStyle = FontStyle.Normal;
                text.text = CurrentPrompt.AcceptText;
                text.fontSize = 1;
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;
                text.color = textColors[1];

                RectTransform textRect = text.GetComponent<RectTransform>();
                textRect.sizeDelta = new Vector2(0.2f, 0.03f);
                textRect.localPosition = new Vector3(0.064f, CurrentPrompt.DeclineText != null ? 0.075f : 0f, -0.16f);
                textRect.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                if (Rounding)
                    RoundMenuObject(button);
            }

            if (CurrentPrompt.DeclineText != null)
            {
                GameObject button = GameObject.CreatePrimitive(PrimitiveType.Cube);

                button.GetComponent<BoxCollider>().isTrigger = true;
                button.transform.parent = menu.transform;
                button.transform.rotation = Quaternion.identity;
                button.transform.localScale = new Vector3(0.09f, 0.4375f, 0.08f);
                button.transform.localPosition = new Vector3(0.56f, -0.2375f, -0.43f);

                button.AddComponent<ButtonCollider>().relatedText = "Decline Prompt";

                if (lastClickedName != "Decline Prompt")
                {
                    ColorChanger colorChanger = button.AddComponent<ColorChanger>();
                    colorChanger.colors = buttonColors[0];
                }
                Text text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<Text>();
                text.font = currentFont;
                text.fontStyle = FontStyle.Normal;
                text.text = CurrentPrompt.DeclineText;
                text.fontSize = 1;
                text.alignment = TextAnchor.MiddleCenter;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 0;

                text.color = textColors[1];

                RectTransform textRect = text.GetComponent<RectTransform>();
                textRect.sizeDelta = new Vector2(0.2f, 0.03f);

                textRect.localPosition = new Vector3(0.064f, -0.075f, -0.16f);
                textRect.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

                if (Rounding)
                    RoundMenuObject(button);
            }
        }

        public class PromptData
        {
            public bool IsText;
            public string Message;

            public string AcceptText;
            public string DeclineText;

            public Action AcceptAction;
            public Action DeclineAction;
        }

        public static List<PromptData> prompts = new List<PromptData>();

        public static PromptData CurrentPrompt
        {
            get
            {
                if (prompts.Count > 0)
                    return prompts[0];
                else
                    return null;
            }
        }

        public static Material promptMaterial;
        public static void Prompt(string Message, Action Accept = null, Action Decline = null, string AcceptButton = "Yes", string DeclineButton = "No")
        {
            prompts.Add(new PromptData
            {
                Message = Message,
                AcceptAction = Accept ?? (() => { }),
                DeclineAction = Decline ?? (() => { }),
                AcceptText = AcceptButton,
                DeclineText = DeclineButton,
                IsText = false
            });

            if (menu != null && prompts.Count <= 1)
                ReloadMenu();
        }

        public static void PromptSingle(string Message, Action Accept = null, string AcceptButton = "Yes")
        {
            prompts.Add(new PromptData
            {
                Message = Message,
                AcceptAction = Accept ?? (() => { }),
                DeclineAction = null,
                AcceptText = AcceptButton,
                DeclineText = null,
                IsText = false
            });

            if (menu != null && prompts.Count <= 1)
                ReloadMenu();
        }

        public static void PromptText(string Message, Action Accept = null, Action Decline = null, string AcceptButton = "Yes", string DeclineButton = "No")
        {
            prompts.Add(new PromptData
            {
                Message = Message,
                AcceptAction = Accept ?? (() => { }),
                DeclineAction = Decline ?? (() => { }),
                AcceptText = AcceptButton,
                DeclineText = DeclineButton,
                IsText = true
            });

            if (menu != null && prompts.Count <= 1)
                ReloadMenu();
        }

        public static void PromptSingleText(string Message, Action Accept = null, string AcceptButton = "Yes")
        {
            prompts.Add(new PromptData
            {
                Message = Message,
                AcceptAction = Accept ?? (() => { }),
                DeclineAction = null,
                AcceptText = AcceptButton,
                DeclineText = null,
                IsText = true
            });

            if (menu != null && prompts.Count <= 1)
                ReloadMenu();
        }

        public static string[] InfosToStrings(ButtonInfo[] array) =>
            array.Select(button => button.buttonText).ToArray();

        public static ButtonInfo[] StringsToInfos(string[] array) =>
            array.Select(Buttons.GetIndex).ToArray();

        public static void SetupAdminPanel(string playername)
        {
            List<ButtonInfo> buttons = Buttons.buttons[0].ToList();
            buttons.Add(new ButtonInfo { buttonText = "Admin", method = () => Buttons.CurrentCategoryName = "Admin", isTogglable = false, toolTip = "Opens the admin mods." });
            Buttons.buttons[0] = buttons.ToArray();
            NotificationManager.SendNotification2($"<color=grey>[</color><color=cyan>{(playername == "NOVA" ? "OWNER" : "ADMIN")}</color><color=grey>]</color> Welcome, {playername}! Admin mods have been enabled.");
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
        public static int framePressCooldown;

        public static bool lastInRoom = false;
        public static bool lastMasterClient = false;
        public static string lastRoom = "";

        public static bool disableMasterClientNotifications;
        public static bool disableRoomNotifications;
        public static bool clearNotificationsOnDisconnect;

        public static List<string> quickActions = new List<string> { };

        public static List<string> favorites = new List<string> { "Exit Favorite" };

        public static int _currentCategoryIndex;

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

        public static bool incrementalButtons = true;

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

        private static int? noInvisLayerMask;
        public static int NoInvisLayerMask()
        {
            noInvisLayerMask ??= ~(
                1 << LayerMask.NameToLayer("TransparentFX") |
                1 << LayerMask.NameToLayer("Ignore Raycast") |
                1 << LayerMask.NameToLayer("Zone") |
                1 << LayerMask.NameToLayer("Gorilla Trigger") |
                1 << LayerMask.NameToLayer("Gorilla Boundary") |
                1 << LayerMask.NameToLayer("GorillaCosmetics") |
                1 << LayerMask.NameToLayer("GorillaParticle"));

            return noInvisLayerMask ?? GorillaLocomotion.Player.Instance.locomotionEnabledLayers;
        }

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


        public static GameObject Pointer;
        public static RaycastHit Ray;
        public static LineRenderer line;
        public static (RaycastHit Ray, GameObject Pointer) RenderGun()
        {
            if (Pointer == null)
            {
                Pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Pointer.transform.localScale = Vector3.one * 0.1f;
                var renderer = Pointer.GetComponent<Renderer>();
                renderer.material.shader = Shader.Find("GUI/Text Shader");
                GameObject.Destroy(Pointer.GetComponent<Collider>());
                line = Pointer.AddComponent<LineRenderer>();
                line.useWorldSpace = true;
                line.material = new Material(Shader.Find("GUI/Text Shader"));
                line.positionCount = 2;
                line.startWidth = 0.02f;
                line.endWidth = 0.02f;
            }
            Pointer.GetComponent<Renderer>().material.color = gunLocked || GetGunInput(true) ? buttonColors[1].GetCurrentColor() : buttonColors[0].GetCurrentColor();
            Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.forward + -GorillaTagger.Instance.rightHandTransform.up, out Ray, float.PositiveInfinity);
            Pointer.transform.position = Ray.point;
            line.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
            line.SetPosition(1, gunLocked ? lockTarget.headMesh.transform.position : Pointer.transform.position);
            line.startColor = Pointer.GetComponent<Renderer>().material.color;
            line.endColor = Pointer.GetComponent<Renderer>().material.color;
            return (Ray, Pointer);
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
