using JupiterX.Classes;
using JupiterX.Managers;
using JupiterX.Mods;
using JupiterX.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static JupiterX.Menu.Main;
using static JupiterX.Settings;

namespace JupiterX.Menu
{
    public class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods | 0
                new ButtonInfo { buttonText = "Settings", method =() => CurrentCategoryName = "Settings", isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "Players", method =() => Players(), isTogglable = false, toolTip = "Opens the player mods page for the menu."},

                new ButtonInfo { buttonText = "Favorite", method =() => CurrentCategoryName = "Favorite", isTogglable = false, toolTip = "Opens the favorite mods page for the menu."},
                new ButtonInfo { buttonText = "Enabled", method =() => CurrentCategoryName = "Enabled", isTogglable = false, toolTip = "Opens the enabled mods page for the menu."},

                new ButtonInfo { buttonText = "Important", method =() => CurrentCategoryName = "Important", isTogglable = false, toolTip = "Opens the important mods page for the menu."},
                new ButtonInfo { buttonText = "Safety", method =() => CurrentCategoryName = "Safety", isTogglable = false, toolTip = "Opens the safety mods page for the menu."},
                new ButtonInfo { buttonText = "Computer", method =() => CurrentCategoryName = "Computer", isTogglable = false, toolTip = "Opens the computer mods page for the menu."},
                new ButtonInfo { buttonText = "Movement", method =() => CurrentCategoryName = "Movement", isTogglable = false, toolTip = "Opens the movement mods page for the menu."},
                new ButtonInfo { buttonText = "Advantage", method =() => CurrentCategoryName = "Advantage", isTogglable = false, toolTip = "Opens the advantage mods page for the menu."},
                new ButtonInfo { buttonText = "VRRig", method =() => CurrentCategoryName = "VRRig", isTogglable = false, toolTip = "Opens the vrrig mods page for the menu."},
                new ButtonInfo { buttonText = "Visual", method =() => CurrentCategoryName = "Visual", isTogglable = false, toolTip = "Opens the visual mods page for the menu."},
                new ButtonInfo { buttonText = "Fun", method =() => CurrentCategoryName = "Fun", isTogglable = false, toolTip = "Opens the fun mods page for the menu."},
                new ButtonInfo { buttonText = "Name", method =() => CurrentCategoryName = "Name", isTogglable = false, toolTip = "Opens the name mods page for the menu."},
                new ButtonInfo { buttonText = "Prefabs", method =() => CurrentCategoryName = "Prefabs", isTogglable = false, toolTip = "Opens the prefab mods page for the menu."},
                new ButtonInfo { buttonText = "Overpowered", method =() => CurrentCategoryName = "Overpowered", isTogglable = false, toolTip = "Opens the overpowered page for the menu."},
                new ButtonInfo { buttonText = "Experimental", method =() => CurrentCategoryName = "Experimental", isTogglable = false, toolTip = "Opens the experimental mods page for the menu."},
                new ButtonInfo { buttonText = "Master", method =() => CurrentCategoryName = "Master", isTogglable = false, toolTip = "Opens the master mods page for the menu."},
                new ButtonInfo { buttonText = "GTH", method =() => CurrentCategoryName = "GTH", isTogglable = false, toolTip = "Opens the master mods page for the menu."},

                new ButtonInfo { buttonText = "Soundboard", method =() => SoundBoard.LoadSoundboard(), isTogglable = false, toolTip = "Opens the soundboard page for the menu."},
            },

            new ButtonInfo[] { // Settings | 1
                new ButtonInfo { buttonText = "Exit Settings", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Menu Settings", method =() => CurrentCategoryName = "Menu Settings", isTogglable = false, toolTip = "Opens the menu settings page for the menu." },
                new ButtonInfo { buttonText = "Movement Settings", method =() => CurrentCategoryName = "Movement Settings", isTogglable = false, toolTip = "Opens the movement settings page for the menu." },
                new ButtonInfo { buttonText = "Gun Settings", method =() => CurrentCategoryName = "Gun Settings", isTogglable = false, toolTip = "Opens the gun settings page for the menu." },
                new ButtonInfo { buttonText = "Plugin Settings", method =() => CurrentCategoryName = "Plugin Settings", isTogglable = false, toolTip = "Opens the settings for the plugins."},
            },

            new ButtonInfo[] { // Menu Settings | 17
                new ButtonInfo { buttonText = "Exit Menu Settings", method =() => CurrentCategoryName = "Settings", isTogglable = false, toolTip = "Returns to the settings page of the menu." },
                new ButtonInfo { buttonText = "Right Hand", enableMethod =() => RightHanded = true, disableMethod =() => RightHanded = false, toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "Both Hands", enableMethod =() => bothHands = true, disableMethod =() => bothHands = false, toolTip = "Puts the menu on your both of your hands."},
                
                new ButtonInfo { buttonText = "Freeze Player In Menu", method =() => Utility.FreezePlayerInMenu(), isTogglable = true, toolTip = "Lets you float while the menu is open."},
                new ButtonInfo { buttonText = "Ghost In Menu", method =() => Utility.GhostInMenu(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Makes you have ghost monke when menu is open."},
                new ButtonInfo { buttonText = "Invis In Menu", method =() => Utility.InvisInMeun(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Makes you have invis monke when menu is open."},
                new ButtonInfo { buttonText = "Round Menu", enableMethod =() => Rounding = true, disableMethod =() => Rounding = false, isTogglable = true, toolTip = "Toggle the menu rounding [<color=red>CAN CAUSE LAG</color>]."},

                new ButtonInfo { buttonText = "Flip Menu", enableMethod =() => flipMenu = true, disableMethod =() => flipMenu = false, toolTip = "Flips the menu to the back of your hand."},
                new ButtonInfo { buttonText = "Menu Trail", enableMethod =() => menuTrail = true, disableMethod =() => menuTrail = false, toolTip = "Gives the menu a trail when you drop."},
                new ButtonInfo { buttonText = "Hide Pointer", enableMethod =() => hidepointer = true, disableMethod =() => hidepointer = false, toolTip = "Hides the menu pointer."},
                new ButtonInfo { buttonText = "See Others Menus", enableMethod =() => networkedmenu = true, disableMethod =() => networkedmenu = false, toolTip = "Lets you see other menu users menus."},

                new ButtonInfo { buttonText = "Disable Menu Sounds", enableMethod =() => DisableMenuSounds = true, disableMethod =() => DisableMenuSounds = false, toolTip = "Disables the menu open and close sounds."},
                new ButtonInfo { buttonText = "Disable Button Sounds", enableMethod =() => DisableButtonSounds = true, disableMethod =() => DisableButtonSounds = false, toolTip = "Disables the button sounds."},
                new ButtonInfo { buttonText = "Disable Disconnect Button", enableMethod =() => DisconnectButton = true, disableMethod =() => DisconnectButton = false, enabled = DisconnectButton, toolTip = "Toggles the disconnect button."},
                new ButtonInfo { buttonText = "Disable Incremental Buttons", enableMethod =() => incrementalButtons = false, disableMethod =() => incrementalButtons = true, toolTip = "Disables the buttons with the increment and decrement buttons next to it."},
                new ButtonInfo { buttonText = "Disable Menu Title", enableMethod =() => MenuTitle = false, disableMethod =() => MenuTitle = true, toolTip = "Toggles the menu title."},
                new ButtonInfo { buttonText = "Disable Page Number", enableMethod =() => DisablePageNumber = true, disableMethod =() => DisablePageNumber = false, toolTip = "Disables the page number on the title."},
                new ButtonInfo { buttonText = "Custom Menu Title", enableMethod =() => CustomMenuTitle = true, disableMethod =() => CustomMenuTitle = false, toolTip = "Gives the menu a custom title you choose inside a txt."},
                new ButtonInfo { buttonText = "Change Page Type", method =() => Utility.ChangePageType(), enableMethod =() => Utility.ChangePageType(), disableMethod =() => Utility.ChangePageType(false), incremental = true, overlapText = "Change Page Type <color=cyan>[Side]</color>", isTogglable = false, toolTip = "Changes the page type." },

                //new ButtonInfo { buttonText = "Disable Search Button", enableMethod =() => disableSearchButton = true, disableMethod =() => disableSearchButton = false, toolTip = "Disables the search button at the bottom of the menu."},
                new ButtonInfo { buttonText = "Disable Return Button", enableMethod =() => disableReturnButton = true, disableMethod =() => disableReturnButton = false, toolTip = "Disables the return button at the bottom of the menu."},

                new ButtonInfo { buttonText = "Change Menu Theme", method =() => Utility.ChangeMenuTheme(), enableMethod =() => Utility.ChangeMenuTheme(), disableMethod =() => Utility.ChangeMenuTheme(false), incremental = true, overlapText = "Change Menu Theme <color=cyan>[Default]</color>", isTogglable = false, toolTip = "Changes the menu theme." },
                new ButtonInfo { buttonText = "Menu Outline", enableMethod =() => menuoutline = true, disableMethod =() => menuoutline = false, isTogglable = true, toolTip = "Gives the menu a outline." },
                new ButtonInfo { buttonText = "Change Drop Type", method =() => Utility.ChangeDropType(), enableMethod =() => Utility.ChangeDropType(), disableMethod =() => Utility.ChangeDropType(false), incremental = true, overlapText = "Change Drop Type <color=cyan>[Destroy]</color>", isTogglable = false, toolTip = "Changes the drop type for the menu." },

                new ButtonInfo { buttonText = "Advanced Arraylist", enableMethod =() => Settings.advancedArraylist = true, disableMethod =() => Settings.advancedArraylist = false, toolTip = "Updates the FPS Counter less, making it easier to read."},
                new ButtonInfo { buttonText = "Flip Arraylist", enableMethod =() => Settings.flipArraylist = true, disableMethod =() => Settings.flipArraylist = false, toolTip = "Flips the arraylist at the top of the screen."},

                new ButtonInfo { buttonText = "FPS Overlay", method =() => NotifiLib.information["FPS"] = Utility.lastDeltaTime.ToString(), disableMethod =() => NotifiLib.information.Remove("FPS"), toolTip = "Displays your FPS on your screen."},
                new ButtonInfo { buttonText = "Ping Overlay", method = Utility.PingOverlay, disableMethod =() => NotifiLib.information.Remove("Ping"), toolTip = "Displays the server's ping on your screen."},
                new ButtonInfo { buttonText = "Time Overlay", method =() => NotifiLib.information["Time"] = DateTime.Now.ToString("hh:mm tt"), disableMethod =() => NotifiLib.information.Remove("Time"), toolTip = "Displays your current time on your screen."},
                new ButtonInfo { buttonText = "Velocity Overlay", method =() => NotifiLib.information["Velocity"] = $"{GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity.magnitude:F1}m/s", disableMethod =() => NotifiLib.information.Remove("Velocity"), toolTip = "Displays your velocity on your screen."},
                new ButtonInfo { buttonText = "Nearby Overlay", method = Utility.NearbyTaggerOverlay, disableMethod =() => NotifiLib.information.Remove("Nearby"), toolTip = "Displays the distance to the nearest tagger/target on your screen."},

                new ButtonInfo { buttonText = "Disable Arraylist GUI", enableMethod =() => Settings.showEnabledModsVR = false, disableMethod =() => Settings.showEnabledModsVR = true, toolTip = "Disables the GUI that shows the enabled mods."},
                new ButtonInfo { buttonText = "Disable Notifications", enableMethod =() => { Settings.Notifications = false; NotifiLib.ClearAllNotifications(); }, disableMethod =() => Settings.Notifications = true, toolTip = "Toggles the Notifcations."},
                new ButtonInfo { buttonText = "Disable Master Client Notifications", enableMethod =() => disableMasterClientNotifications = true, disableMethod =() => disableMasterClientNotifications = false, toolTip = "Disables all notifications regarding master client."},
                new ButtonInfo { buttonText = "Disable Room Notifications", enableMethod =() => disableRoomNotifications = true, disableMethod =() => disableRoomNotifications = false, toolTip = "Disables all notifications regarding the room."},
                new ButtonInfo { buttonText = "Disable Player Notifications", enableMethod =() => disablePlayerNotifications = true, disableMethod =() => disablePlayerNotifications = false, toolTip = "Disables all notifications regarding players."},
                new ButtonInfo { buttonText = "Clear Notifications on Disconnect", enableMethod =() => clearNotificationsOnDisconnect = true, disableMethod =() => clearNotificationsOnDisconnect = false, toolTip = "Clears all notifications on disconnect."},

                new ButtonInfo { buttonText = "Low Quality Text", enableMethod =() => lowqualttext = true, disableMethod =() => lowqualttext = false, toolTip = "Makes the menu text low quality."},
                new ButtonInfo { buttonText = "Lowercase Mode", enableMethod =() => lowercaseMode = true, disableMethod =() => lowercaseMode = false, toolTip = "Makes the entire menu's text lowercase."},
                new ButtonInfo { buttonText = "Uppercase Mode", enableMethod =() => uppercaseMode = true, disableMethod =() => uppercaseMode = false, toolTip = "Makes the entire menu's text uppercase."},
                new ButtonInfo { buttonText = "Overflow Mode", enableMethod =() => NoAutoSizeText = true, disableMethod =() => NoAutoSizeText = false, toolTip = "Makes the entire menu's text overflow."},

                new ButtonInfo { buttonText = "Custom Boards", enableMethod =() => CustomBoards = true, disableMethod =() => CustomBoards = false, enabled = CustomBoards, isTogglable = true, toolTip = "Enables the custom boards in stump." },
                new ButtonInfo { buttonText = "Move Stump Text Gun", method =() => Utility.MoveStumpTextGun(), isTogglable = true, toolTip = "Lets you move the stump text with a gun." },
                new ButtonInfo { buttonText = "Version Text", enableMethod =() => VersionText = true, disableMethod =() => VersionText = false, enabled = VersionText, toolTip = "Toggles the Version Text."},
                new ButtonInfo { buttonText = "Stump Text", enableMethod =() => StumpText = true, disableMethod =() => StumpText = false, enabled = StumpText, toolTip = "Toggles the stump text."},

                new ButtonInfo { buttonText = "Save Preferences", method =() => Utility.SavePreferences(), isTogglable = false, toolTip = "Saves your enabled mods to file." },
                new ButtonInfo { buttonText = "Load Preferences", method =() => Utility.LoadPreferences(), isTogglable = false, toolTip = "Loads your saved mods from a file." },
            },

            new ButtonInfo[] { // Movement Settings | 18
                new ButtonInfo { buttonText = "Exit Movement Settings", method =() => CurrentCategoryName = "Settings", isTogglable = false, toolTip = "Returns to the settings page of the menu." },
                new ButtonInfo { buttonText = "Change Fly Speed", method =() => Movement.ChangeFlySpeed(), enableMethod =() => Movement.ChangeFlySpeed(), disableMethod =() => Movement.ChangeFlySpeed(false), incremental = true, overlapText = "Change Fly Speed <color=cyan>[Very Slow]</color>", isTogglable = false, toolTip = "Changes the current fly speed." },
                new ButtonInfo { buttonText = "Change Arm Length", method =() => Movement.ChangeArmLength(), enableMethod =() => Movement.ChangeArmLength(), disableMethod =() => Movement.ChangeArmLength(false), incremental = true, overlapText = "Change Arm Length <color=cyan>[Stean]</color>", isTogglable = false, toolTip = "Changes your arm length." },
            },

            new ButtonInfo[] { // Gun Settings | 19
                new ButtonInfo { buttonText = "Exit Gun Settings", method =() => CurrentCategoryName = "Settings", isTogglable = false, toolTip = "Returns to the settings page of the menu." },
                new ButtonInfo { buttonText = "Disable Gun Pointer", enableMethod =() => disableGunPointer = true, disableMethod =() => disableGunPointer = false, isTogglable = true, toolTip = "Disables the gun pointer." },
                new ButtonInfo { buttonText = "Small Gun Pointer", enableMethod =() => smallGunPointer = true, disableMethod =() => smallGunPointer = false, isTogglable = true, toolTip = "Makes the gun pointer smaller." },
                new ButtonInfo { buttonText = "Disable Gun Line", enableMethod =() => disableGunLine = true, disableMethod =() => disableGunLine = false, isTogglable = true, toolTip = "Disables the gun line." },
                new ButtonInfo { buttonText = "Swap Gun Hand", enableMethod =() => SwapGunHand = true, disableMethod =() => SwapGunHand = false, isTogglable = true, toolTip = "Swaps the hand of the gun is on." },
                //new ButtonInfo { buttonText = "Gripless Guns", enableMethod =() => GriplessGuns = true, disableMethod =() => GriplessGuns = false, isTogglable = true, toolTip = "Makes the gun work without holding grip." },
                new ButtonInfo { buttonText = "Triggerless Guns", enableMethod =() => TriggerlessGuns = true, disableMethod =() => TriggerlessGuns = false, isTogglable = true, toolTip = "Makes the gun shoot without holding trigger." },
            },

            new ButtonInfo[] { // Important | 2
                new ButtonInfo { buttonText = "Exit Important", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Quit Game", method =() => Important.QuitGame(), isTogglable = false, toolTip = "Quits your game." },
                new ButtonInfo { buttonText = "Anti AFK", method =() => Important.AntiAFK(), isTogglable = true, toolTip = "Disables the afk kick you get." },
                new ButtonInfo { buttonText = "Clear Notifcations", method =() => NotifiLib.ClearAllNotifications(), isTogglable = false, toolTip = "Clears all the notifications." },

                new ButtonInfo { buttonText = "Turning", method =() => Important.Turning(), isTogglable = true, toolTip = "Lets you turn." },
            },

            new ButtonInfo[] { // Safety | 3
                new ButtonInfo { buttonText = "Exit Safety", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Panic", method =() => Utility.Panic(), isTogglable = false, toolTip = "Disables every mod you have enabled." },
                new ButtonInfo { buttonText = "Anti Report [<color=yellow>Disconnect</color>]", method =() => Utility.BetaAntiReport(false, true), isTogglable = true, toolTip = "Disconnects you when someone is close to the report button." },
                new ButtonInfo { buttonText = "Anti Report [<color=yellow>Crash</color>]", method =() => Utility.BetaAntiReport(true, false), isTogglable = true, toolTip = "Crashes the person who tries to report you when someone is close to the report button." },
                new ButtonInfo { buttonText = "Anti Moderator", method =() => Utility.BetaAntiCosmetic("LBAAK."), isTogglable = true, toolTip = "Disconnects you when someone has the moderator stick." },
                new ButtonInfo { buttonText = "Anti Admin", method =() => Utility.BetaAntiCosmetic("LBAAD."), isTogglable = true, toolTip = "Disconnects you when someone has the admin badge." },
                new ButtonInfo { buttonText = "Anti Finger Painter", method =() => Utility.BetaAntiCosmetic("LBADE."), isTogglable = true, toolTip = "Disconnects you when someone has the finger painter." },
            },

            new ButtonInfo[] { // Computer | 4
                new ButtonInfo { buttonText = "Exit Computer", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Disconnect", method =() => Computer.Leave(), isTogglable = false, toolTip = "Disconnects you from the lobby." },
                new ButtonInfo { buttonText = "Reconnect", method =() => Important.Reconnect(), isTogglable = false, toolTip = "Reconnects you to the current lobby." },
                new ButtonInfo { buttonText = "Lobby Hop", method =() => Important.LobbyHop(), isTogglable = false, toolTip = "Lets you lobby hop." },
                new ButtonInfo { buttonText = "Join Random", method =() => Computer.Jrr(), isTogglable = false, toolTip = "Lets you join a random room." },
                new ButtonInfo { buttonText = "Join Code '1'", method =() => Computer.JoinCode("1"), isTogglable = false, toolTip = "Lets you join the code \"1\"." },
                new ButtonInfo { buttonText = "Join Code 'JupiterX'", method =() => Computer.JoinCode("_@-JupiterX-@_"), isTogglable = false, toolTip = "Lets you join the code \"JupiterX\"."  },
                new ButtonInfo { buttonText = "Join Code 'Mods'", method =() => Computer.JoinCode("MODS"), isTogglable = false, toolTip = "Lets you join the code \"MODS\"."  },
                new ButtonInfo { buttonText = "Join Code 'Mod'", method =() => Computer.JoinCode("MOD"), isTogglable = false, toolTip = "Lets you join the code \"MOD\"."  },
                new ButtonInfo { buttonText = "Join Code 'Pbbv'", method =() => Computer.JoinCode("PBBV"), isTogglable = false, toolTip = "Lets you join the code \"PBBV\"."  },
                new ButtonInfo { buttonText = "Join Code 'Daisy'", method =() => Computer.JoinCode("DAISY"), isTogglable = false, toolTip = "Lets you join the code \"DAISY\"."  },
            },

            new ButtonInfo[] { // Movement | 5
                new ButtonInfo { buttonText = "Exit Movement", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Fly <color=cyan>[A]</color>", method =() => Movement.Fly(), isTogglable = true, toolTip = "Lets you fly while holding your right primary." },
                new ButtonInfo { buttonText = "TFly <color=cyan>[<color=cyan>RT</color>]</color>", method =() => Movement.TFly(), isTogglable = true, toolTip = "Lets you fly while holding your right trigger."  },
                new ButtonInfo { buttonText = "Excel Fly", method =() => Movement.ExcelFly(), isTogglable = true, toolTip = "Lets you fly like iron man."  },
                new ButtonInfo { buttonText = "Slingshot Fly <color=cyan>[A]</color>", method =() => Movement.SlingShotFly(), isTogglable = true, toolTip = "Lets you fly like a slingshot while hold your right primary."  },
                new ButtonInfo { buttonText = "Long Arms", method =() => Movement.LongArms(false), disableMethod =() => Movement.LongArms(true), isTogglable = true, toolTip = "Gives you long arms."  },
                new ButtonInfo { buttonText = "Platforms", method =() => Movement.Platforms(), isTogglable = true, toolTip = "Lets you walk on air while holding grip."  },
                new ButtonInfo { buttonText = "SpeedBoost", method =() => Movement.SpeedBoost(), isTogglable = true, toolTip = "Gives you a speed boost."  },
                new ButtonInfo { buttonText = "Mosa Boost", method =() => Movement.Mosaboost(), isTogglable = true, toolTip = "Gives you a slight speed boost."  },
                new ButtonInfo { buttonText = "No Tag Freeze", method =() => Movement.NoTagFreeze(0), isTogglable = true, toolTip = "Removes the tag freeze you get when you get tagged."  },
                new ButtonInfo { buttonText = "Tag Freeze", method =() => Movement.NoTagFreeze(1), disableMethod =() => Movement.NoTagFreeze(0),  isTogglable = true, toolTip = "Lets you act like you have tag freeze."  },
                new ButtonInfo { buttonText = "TP Gun", method =() => Movement.TPGun(), isTogglable = true, toolTip = "Lets you teleport with a gun."  },
                new ButtonInfo { buttonText = "Car Monke <color=cyan>[Triggers]</color>", method =() => Movement.CarMonke(), isTogglable = true, toolTip = "Drive around while holding your triggers."  },
                new ButtonInfo { buttonText = "NoClip <color=cyan>[RT]</color>", method =() => Movement.NoClip(Utility.RTrigger), isTogglable = true, toolTip = "Removes object colliders when you hold right trigger."  },
                new ButtonInfo { buttonText = "Follow Player Gun", method =() => Movement.FollowPlayerGun(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you follow someone with a gun."  },
                new ButtonInfo { buttonText = "Checkpoint <color=cyan>[G]</color>", method =() => Movement.Checkpoint(), isTogglable = true, toolTip = "Lets you a place a checkpoint to go back to later."  },
                new ButtonInfo { buttonText = "C4 <color=cyan>[G]</color>", method =() => Movement.C4(), isTogglable = true, toolTip = "Lets you a place a checkpoint to go back to later."  },
            },

            new ButtonInfo[] { // Advantage | 6
                new ButtonInfo { buttonText = "Exit Advantage", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Tag All", method =() => Advantage.TagAll(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you tag everyone in the lobby."  },
                new ButtonInfo { buttonText = "Tag Aura", method =() => Advantage.TagAura(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you tag someone when they come close to you."  },
                new ButtonInfo { buttonText = "Tag Gun", method =() => Advantage.TagGun(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you tag someone with a gun."  },
                new ButtonInfo { buttonText = "Flick Tag Gun", method =() => Advantage.FlickTagGun(), isTogglable = true, toolTip = "Lets you flick tag someone with a gun."  },
            },

            new ButtonInfo[] { // VRRig | 7
                new ButtonInfo { buttonText = "Exit VRRig", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Ghost Monke <color=cyan>[A]</color>", method =() => vRRig.GhostMonke(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you become a ghost."  },
                new ButtonInfo { buttonText = "Invis Monke <color=cyan>[B]</color>", method =() => vRRig.InvisMonke(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you become invisable."  },
                new ButtonInfo { buttonText = "Grab Rig <color=cyan>[Grips]</color>", method =() => vRRig.GrabRig(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you grab your rig while holding right grip."  },
                new ButtonInfo { buttonText = "Spaz Rig", method =() => vRRig.SpazRig(), disableMethod = vRRig.FixSpazRig, isTogglable = true, toolTip = "Makes your rig spazz out."  },
                new ButtonInfo { buttonText = "Strobe", method =() => vRRig.Strobe(), isTogglable = true, toolTip = "Makes your color go crazy."  },
                new ButtonInfo { buttonText = "Move Rig Gun", method =() => vRRig.MoveRigGun(), isTogglable = true, toolTip = "Moves your rig to the gun point."  },
                new ButtonInfo { buttonText = "Bees", method =() => vRRig.Bees(), disableMethod =() => Utility.FixGhostRig(), isTogglable = true, toolTip = "Makes your rig teleport to other players."  },
            },

            new ButtonInfo[] { // Visual | 8
                new ButtonInfo { buttonText = "Exit Visual", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Chams", method =() => Visual.Chams(true), disableMethod =() => Visual.Chams(false), isTogglable = true, toolTip = "Lets you see players through walls."  },
                new ButtonInfo { buttonText = "Full Bright", method =() => Visual.fullBright(), disableMethod =() => Visual.fulldrak(), isTogglable = true, toolTip = "Lets you see in the dark."  },
                new ButtonInfo { buttonText = "Tracers", method =() => Visual.Tracers(), isTogglable = true, toolTip = "Points lines at other players."  },
                new ButtonInfo { buttonText = "Box ESP", method =() => Visual.BoxESP(), isTogglable = true, toolTip = "Lets you see players through walls."  },
                new ButtonInfo { buttonText = "Capsule ESP", method =() => Visual.CapsuleESP(), isTogglable = true, toolTip = "Lets you see players through walls."  },
                new ButtonInfo { buttonText = "Sphere ESP", method =() => Visual.SphereESP(), isTogglable = true, toolTip = "Lets you see players through walls."  },
                new ButtonInfo { buttonText = "Name Tags", method =() => Visual.NameTagESP(), isTogglable = true, toolTip = "Lets you see player info above there head with a name tag."  },
                new ButtonInfo { buttonText = "Player Info Tags", method =() => Visual.PlayerInfoTags(), isTogglable = true, toolTip = "Lets you see player info above there head with a name tag."  },
                new ButtonInfo { buttonText = "Velocity Label", method =() => Visual.VelocityLabel(), isTogglable = true, toolTip = "Lets you see your velocity with a label on your right hand."  },
                new ButtonInfo { buttonText = "Player Count Label", method =() => Visual.LeftTaggedLabel(), isTogglable = true, toolTip = "Lets you see how manu tagged people are left."  },
            },

            new ButtonInfo[] { // Fun | 9
                new ButtonInfo { buttonText = "Exit Fun", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "No Tap Cooldown", method =() => Fun.NoTapCooldown(), disableMethod =() => Fun.ResetTapcooldown(), isTogglable = true, toolTip = "Remoevs the hand tap cooldown."  },
                new ButtonInfo { buttonText = "Loud Hand Taps", method =() => Fun.LoadHandTaps(), disableMethod =() => Fun.FixHandTaps(), isTogglable = true, toolTip = "Makes the hand tap sound very loud."  },
                new ButtonInfo { buttonText = "Silent Hand Taps", method =() => Fun.SilentHandTaps(), disableMethod =() => Fun.FixHandTaps(), isTogglable = true, toolTip = "Remoevs the hand tap sound."  },

                new ButtonInfo { buttonText = "Auto Clicker <color=cyan>[T]</color>", method =() => Fun.AutoClicker(), isTogglable = true, toolTip = "Automatically presses trigger for you when holding <color=cyan>trigger</color>."  },

                new ButtonInfo { buttonText = "Mute Gun", method =() => Fun.MuteGun(), isTogglable = true, toolTip = "Lets you mute the player you shoot the gun at." },
                new ButtonInfo { buttonText = "Mute All", method =() => Fun.MuteAll(), isTogglable = false, toolTip = "Lets you mute everyone in the current room." },

                new ButtonInfo { buttonText = "UnMute Gun", method =() => Fun.UnMuteGun(), isTogglable = true, toolTip = "Lets you unmute the player you shoot the gun at." },
                new ButtonInfo { buttonText = "UnMute All", method =() => Fun.UnMuteAll(), isTogglable = false, toolTip = "Lets you unmute everyone in the current room." },

                new ButtonInfo { buttonText = "Report Gun", method =() => Fun.ReportGun(), isTogglable = false, toolTip = "Reports the person who you shoot at for cheating." },
                new ButtonInfo { buttonText = "Report All", method =() => Fun.ReportAll(), isTogglable = false, toolTip = "Reports everyone in the lobby for cheating." },

                new ButtonInfo { buttonText = "Fix Mic", method =() => Fun.FixMic(), isTogglable = false, toolTip = "Fixes your mic."  },
                new ButtonInfo { buttonText = "Low Quality Mic", method =() => Fun.LowQualityMic(), isTogglable = false, toolTip = "Makes your micrphone low quality."  },
                new ButtonInfo { buttonText = "High Quality Mic", method =() => Fun.LowQualityMic(), isTogglable = false, toolTip = "Makes your micrphone high quality."  },

                new ButtonInfo { buttonText = "Bass Boosted Mic", method =() => Fun.BassBoostMic(), isTogglable = false, toolTip = "Makes your micrphone bass boosted."  },
                new ButtonInfo { buttonText = "Extreme Bass Boosted Mic", method =() => Fun.BassBoostMicExtreme(), isTogglable = false, toolTip = "Makes your micrphone hextremely bass boosted."  },

                new ButtonInfo { buttonText = "Get ID Self", method =() => Fun.GetIdSelf(), isTogglable = false, toolTip = "Gets your own userid and writes it to a file."  },
                new ButtonInfo { buttonText = "Get ID Gun", method =() => Fun.GetIdGun(), isTogglable = true, toolTip = "Gets the person you shoot userid and writes it to a file."  },
                new ButtonInfo { buttonText = "Get ID All", method =() => Fun.GetIdAll(), isTogglable = true, toolTip = "Gets everyone in the room userid and writes it to a file."  },

                new ButtonInfo { buttonText = "Grab Player Info", method =() => Fun.GrabRoomInfo(), isTogglable = true, toolTip = "Gets the room info and writes it to a file."  },
            },

            new ButtonInfo[] { // Name | 10
                new ButtonInfo { buttonText = "Exit Name", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Menu Name Tag", method =() => Name.MenuNameTag(), isTogglable = true, toolTip = "Sets your name to the menu name."  },
                new ButtonInfo { buttonText = "Rainbow Owner Name", method =() => Name.ChangeNameSpaz("\nOwner", new string[] { "red", "yellow", "cyan", "blue", "magenta", "lime", "green" }), isTogglable = true, toolTip = "Sets your name as Owner with rainbow text."  },
                new ButtonInfo { buttonText = "Owner Name", method =() => Name.ChangeName("\nOwner", "white"), isTogglable = true, toolTip = "Sets your name as Owner."  },
                new ButtonInfo { buttonText = "Moderator Name", method =() => Name.ChangeName("\nModerator", "white"), isTogglable = true, toolTip = "Sets your name as Moderator."  },
                new ButtonInfo { buttonText = "BSU Skids Name", method =() => Name.ChangeName("BSU Menu is skidded\nBSU Skids", "blue"), isTogglable = true, toolTip = "Sets your name as BSU Skids."  },
                new ButtonInfo { buttonText = "PBBV Name", method =() => Name.ChangeName("\nPBBV", "white"), isTogglable = true, toolTip = "Sets your name as PBBV."  },
                new ButtonInfo { buttonText = "ECHO Name", method =() => Name.ChangeName("\nECHO", "white"), isTogglable = true, toolTip = "Sets your name as ECHO."  },
                new ButtonInfo { buttonText = "DAISY09 Name", method =() => Name.ChangeName("\nDAISY09", "white"), isTogglable = true, toolTip = "Sets your name as DAISY09."  },
                new ButtonInfo { buttonText = "Custom Name", method =() => Name.CustomName(), isTogglable = true, toolTip = "Makes your name be the custom name you set in a file."  },

                new ButtonInfo { buttonText = "No Name", method =() => Name.ChangeName("\n\n", "white"), isTogglable = true, toolTip = "Makes you have no name."  },
                new ButtonInfo { buttonText = "Emoji Name (1)", method =() => Utility.BetaEmojiName(0), isTogglable = true, toolTip = "Sets your name as a emoji."  },
                new ButtonInfo { buttonText = "Emoji Name (2)", method =() => Utility.BetaEmojiName(1), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (3)", method =() => Utility.BetaEmojiName(2), isTogglable = true , toolTip = "Sets your name as a emoji."},
                new ButtonInfo { buttonText = "Emoji Name (4)", method =() => Utility.BetaEmojiName(3), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (5)", method =() => Utility.BetaEmojiName(4), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (6)", method =() => Utility.BetaEmojiName(5), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (7)", method =() => Utility.BetaEmojiName(6), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (8)", method =() => Utility.BetaEmojiName(7), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (9)", method =() => Utility.BetaEmojiName(8), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (10)", method =() => Utility.BetaEmojiName(9), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (11)", method =() => Utility.BetaEmojiName(10), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (12)", method =() => Utility.BetaEmojiName(11), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (13)", method =() => Utility.BetaEmojiName(12), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (14)", method =() => Utility.BetaEmojiName(13), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (15)", method =() => Utility.BetaEmojiName(14), isTogglable = true, toolTip = "Sets your name as a emoji." },
                new ButtonInfo { buttonText = "Emoji Name (16)", method =() => Utility.BetaEmojiName(15), isTogglable = true, toolTip = "Sets your name as a emoji." },
            },

            new ButtonInfo[] { // Prefabs | 11
                new ButtonInfo { buttonText = "Exit Prefabs", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Get Fucked Spawn [Forest, Targets]", method =() => Experimental.GetFucked(), isTogglable = false, toolTip = "Spawns the word 'Get Fucked' using stickable targets in forest."  },
                new ButtonInfo { buttonText = "Clear Prefabs", method =() => Prefabs.ClearPrefabs(), isTogglable = false, toolTip = "Clears every prefab with a photon view."  },

                new ButtonInfo { buttonText = "Cube All [<color=cyan>RT</color>]", method =() => Fun.CubeAll(), isTogglable = true, toolTip = "Lets you spawn cubes at others while holding right trigger."  },

                new ButtonInfo { buttonText = "Cube Spam [<color=cyan>Grips</color>]", method =() => Prefabs.CubeSpam(), isTogglable = true, toolTip = "Lets you spam cubes while holding grip."  },
                new ButtonInfo { buttonText = "Give Cube Spam Gun", method =() => Prefabs.GiveSpamGun(0), isTogglable = true, toolTip = "Lets you give someone cube spam when they hold grip."  },
                new ButtonInfo { buttonText = "Target Spam [<color=cyan>Grips</color>]", method =() => Prefabs.TargetSpam(), isTogglable = true, toolTip = "Lets you spam stickable targets while holding grip."  },
                new ButtonInfo { buttonText = "Give Target Spam Gun", method =() => Prefabs.GiveSpamGun(1), isTogglable = true, toolTip = "Lets you give someone stickable target spam when they hold grip."  },
                new ButtonInfo { buttonText = "Network Player Spam [<color=cyan>Grips</color>]", method =() => Prefabs.NetworkPlayerSpam(), isTogglable = true, toolTip = "Lets you spam network players when holding grip."  },
                new ButtonInfo { buttonText = "Give Network Player Spam Gun", method =() => Prefabs.GiveSpamGun(2), isTogglable = true, toolTip = "Lets you give someone network player spam when they are holding grip."  },
                new ButtonInfo { buttonText = "Enemy Spam [<color=cyan>Grips</color>]", method =() => Prefabs.EnemySpam(), isTogglable = true, toolTip = "Lets you spam enemys when holding grip."  },
                new ButtonInfo { buttonText = "Give Enemy Spam Gun", method =() => Prefabs.GiveSpamGun(3), isTogglable = true, toolTip = "Lets you give someone enemy spam when they are holding grip."  },
                new ButtonInfo { buttonText = "Scoreboard Spam [<color=cyan>Grips</color>]", method =() => Prefabs.SpamScoreboard(), isTogglable = true, toolTip = "Lets you spam enemys when holding grip."  },

                new ButtonInfo { buttonText = "Cube Gun", method =() => Prefabs.CubeGun(), isTogglable = true, toolTip = "Lets you shoot cubes with a gun."  },
                new ButtonInfo { buttonText = "Target Gun", method =() => Prefabs.TargetGun(), isTogglable = true, toolTip = "Lets you shoot targets with a gun."  },
                new ButtonInfo { buttonText = "Network Player Gun", method =() => Prefabs.NetworkPlayerGun(), isTogglable = true, toolTip = "Lets you shoot network players with a gun."  },
                new ButtonInfo { buttonText = "Enemy Gun", method =() => Prefabs.EnemyGun(), isTogglable = true, toolTip = "Lets you shoot enemys with a gun."  },
                new ButtonInfo { buttonText = "Scoreboard Gun", method =() => Prefabs.ScoreboardGun(), isTogglable = true, toolTip = "Lets you shoot scoreboard with a gun."  },

                new ButtonInfo { buttonText = "Cube Launcher [<color=cyan>Grips</color>]", method =() => Prefabs.PrefabLuancher("bulletPrefab"), isTogglable = true, toolTip = "Lets you launch cubes while holding grips."  },
                new ButtonInfo { buttonText = "Target Launcher [<color=cyan>Grips</color>]", method =() => Prefabs.PrefabLuancher("STICKABLE TARGET"), isTogglable = true, toolTip = "Lets you launch targets while holding grips."  },
                new ButtonInfo { buttonText = "Target Dick Spawner [<color=cyan>Grips</color>]", method =() => Utility.DickSpawn(), isTogglable = true, toolTip = "Lets you spawn a dick out of targets."  },
            },

            new ButtonInfo[] { // Overpowered | 12
                new ButtonInfo { buttonText = "Exit Overpowered", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "MasterLabel", label = true },

                new ButtonInfo { buttonText = "Cosmetix", method =() => Utility.UnlockAll(), isTogglable = false, toolTip = "Gives you every cosmetic in the game."  },

                new ButtonInfo { buttonText = "Unban Self", method =() => Experimental.Unban(), isTogglable = false, toolTip = "Unbans yourself if you get banned."  },
                new ButtonInfo { buttonText = "Set Master", method =() => Utility.MakeMeMaster(), isTogglable = true, toolTip = "Sets you as master client."  },
                new ButtonInfo { buttonText = "Always Set Master", method =() => Overpowered.AlawysMaster(), isTogglable = true, toolTip = "Always sets you as master no matter who tries to take it."  },

                new ButtonInfo { buttonText = "Rig Spam [<color=cyan>RT</color>]", method =() => Overpowered.RigSpam(), isTogglable = true, toolTip = "Lets you spam rigs."  },

                new ButtonInfo { buttonText = "Material Spam All", method =() => Overpowered.MatSpamAll(), isTogglable = true, toolTip = "Lets you spaz out the infection material on others."  },
                new ButtonInfo { buttonText = "Material Spam Gun", method =() => Overpowered.MatSpamGun(), isTogglable = true, toolTip = "Lets you spaz out the infection material on who you shoot."  },

                new ButtonInfo { buttonText = "Kick Gun [<color=cyan>Private</color>]", method =() => Overpowered.KickGun(), isTogglable = true, toolTip = "Lets you kick someone with a gun in stump."  },
                new ButtonInfo { buttonText = "Kick All [<color=cyan>Private</color>]", method =() => Overpowered.KickAll(), isTogglable = true, toolTip = "Lets you kick everyone in stump."  },

                new ButtonInfo { buttonText = "Spaz Shiny Rock Count", method =() => Overpowered.SpazRocks(), isTogglable = true, toolTip = "Spazes your shiny rocks."  },
                new ButtonInfo { buttonText = "Remove All Shiny Rocks", method =() => Overpowered.BetaChangeShinyRock(0), isTogglable = false, toolTip = "Gives you 0 shiny rocks."  },
                new ButtonInfo { buttonText = "Max Shiny Rocks", method =() => Overpowered.BetaChangeShinyRock(int.MaxValue), isTogglable = false, toolTip = "Gives you infinite shiny rocks."  },

                new ButtonInfo { buttonText = "Create Symbol Name Public", method =() => Overpowered.CreatePublic("<>{][]()@.,/?!"), isTogglable = true, toolTip = "Creates a public room with a symbols as the name." },
                new ButtonInfo { buttonText = "Create Dot Name Public", method =() => Overpowered.CreatePublic("."), isTogglable = true, toolTip = "Creates a public room with a dot name." },
                new ButtonInfo { buttonText = "Create Short Name Public", method =() => Overpowered.CreatePublic("1"), isTogglable = true, toolTip = "Creates a public room with a short name." },
                new ButtonInfo { buttonText = "Create Long Name Public", method =() => Overpowered.LongNamePub(), isTogglable = true, toolTip = "Creates a public room with a long name." },
                new ButtonInfo { buttonText = "Create Modded Public", method =() => Overpowered.CreatePublic("\n\n\n\nMODDED\n\n\n\n"), isTogglable = true, toolTip = "Creates a public room that is modded." },
                new ButtonInfo { buttonText = "Create JupiterX Public", method =() => Overpowered.CreatePublic("\n\nJupiterX on top\n\n\n"), isTogglable = true, toolTip = "Creates a public room that says jupiterx on top." },

                new ButtonInfo { buttonText = "Create 255 Player Room", method =() => Overpowered.CreatePublic(255), isTogglable = true, toolTip = "Creates a public room that has max players allowed." },
                new ButtonInfo { buttonText = "Create 1 Player Room", method =() => Overpowered.CreatePublic(1), isTogglable = true, toolTip = "Creates a public room that has 1 players allowed." },
                new ButtonInfo { buttonText = "Create 5 Player Room", method =() => Overpowered.CreatePublic(5), isTogglable = true, toolTip = "Creates a public room that has 5 players allowed." },
                new ButtonInfo { buttonText = "Create 50 Player Room", method =() => Overpowered.CreatePublic(50), isTogglable = true, toolTip = "Creates a public room that has 50 players allowed." },
                new ButtonInfo { buttonText = "Create 100 Player Room", method =() => Overpowered.CreatePublic(100), isTogglable = true, toolTip = "Creates a public room that has 100 players allowed." },

                new ButtonInfo { buttonText = "Float Gun", method =() => Overpowered.FloatGun(), isTogglable = true, toolTip = "Attempts to make the person you shoot at float."  },

                new ButtonInfo { buttonText = "Crash Gun", method =() => Overpowered.CrashGun(), isTogglable = true, toolTip = "Lets you crash someone you shoot at."  },
                new ButtonInfo { buttonText = "Crash Gun V2", method =() => Overpowered.CrashGunV2(), isTogglable = true, toolTip = "Lets you crash someone you shoot at."  },
                new ButtonInfo { buttonText = "Crash Gun V3", overlapText = "Insta Crash Gun", method =() => Overpowered.CrashGunV3(), isTogglable = true, toolTip = "Lets you crash someone you shoot at."  },
                new ButtonInfo { buttonText = "Crash Gun V4", method =() => Overpowered.CrashGunV4(), isTogglable = true, toolTip = "Lets you crash someone you shoot at."  },
                new ButtonInfo { buttonText = "Crash Gun V5", method =() => Overpowered.CrashGunV5(), isTogglable = true, toolTip = "Lets you crash someone you shoot at."  },

                new ButtonInfo { buttonText = "Crash All [<color=cyan>RT</color>]", method =() => Overpowered.CrashAll(), isTogglable = true, toolTip = "Lets you crash all while holding right trigger."  },
                new ButtonInfo { buttonText = "Crash All V2 [<color=cyan>RT</color>]", method =() => Overpowered.CrashAllV2(), isTogglable = true, toolTip = "Lets you crash all while holding right trigger."  },
                new ButtonInfo { buttonText = "Crash All V3 [<color=cyan>RT</color>]", method =() => Overpowered.CrashAllV3(), isTogglable = true, toolTip = "Lets you crash all while holding right trigger."  },
                new ButtonInfo { buttonText = "Crash All V4 [<color=cyan>RT</color>]", method =() => Overpowered.CrashAllV4(), isTogglable = true, toolTip = "Lets you crash all while holding right trigger."  },
                new ButtonInfo { buttonText = "Crash All V5 [<color=cyan>RT</color>]", method =() => Overpowered.CrashAllV5(), isTogglable = true, toolTip = "Lets you crash all while holding right trigger."  },

                new ButtonInfo { buttonText = "Ban All", method =() => Utility.BanAll(), isTogglable = true, toolTip = "Lets you ban everyone in the current room." },
                new ButtonInfo { buttonText = "Ban Gun", method =() => Overpowered.BanGun(), isTogglable = true, toolTip = "Lets you ban someone you shoot at." },

                new ButtonInfo { buttonText = "Set GameMode [<color=yellow>CASUAL</color>]", method =() => Experimental.SetGameMode("CASUAL"), isTogglable = false, toolTip = "Sets the game mode to casual."  },
                new ButtonInfo { buttonText = "Set GameMode [<color=yellow>INFECTION</color>]", method =() => Experimental.SetGameMode("INFECTION"), isTogglable = false, toolTip = "Sets the game mode to infection."  },
                new ButtonInfo { buttonText = "Set GameMode [<color=yellow>HUNT</color>]", method =() => Experimental.SetGameMode("HUNT"), isTogglable = false, toolTip = "Sets the game mode to hunt."  },
                new ButtonInfo { buttonText = "Set GameMode [<color=yellow>PAINTBRAWL</color>]", method =() => Experimental.SetGameMode("PAINTBRAWL"), isTogglable = false, toolTip = "Sets the game mode to battle."  },
                new ButtonInfo { buttonText = "Set GameMode [<color=yellow>ERROR</color>]", method =() => Experimental.SetGameMode("ERROR"), isTogglable = false, toolTip = "Sets the game mode to battle."  },
            },

            new ButtonInfo[] { // Experimental | 13
                new ButtonInfo { buttonText = "Exit Experimental", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Spaz Forest Targets", method =() => Experimental.SpazForestTargets(), isTogglable = true, toolTip = "Spazzes out the targets that are in forest."  },

                new ButtonInfo { buttonText = "Spam Pop & Unpop Balloon [<color=cyan>RT</color>]", method =() => Experimental.BalloonSpam(), isTogglable = true, toolTip = "Spams everyones balloon while holding right trigger."  },

                new ButtonInfo { buttonText = "Spam Mute All", method =() => Utility.PacketStresser(), isTogglable = true, toolTip = "Spams all the report and mute buttons." },
                new ButtonInfo { buttonText = "Spam Mute All V2", method =() => Utility.BetaSpamMuteAll(), isTogglable = true, toolTip = "Spams all the report and mute buttons." },
            },

            new ButtonInfo[] { // Master | 14
                new ButtonInfo { buttonText = "Exit Master", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },

                new ButtonInfo { buttonText = "Set Master Gun", method =() => Overpowered.SetMasterGun(), isTogglable = true, toolTip = "Lets you set someone as master client."  },

                new ButtonInfo { buttonText = "Slow Gun", method =() => Overpowered.SlowGun(), isTogglable = true, toolTip = "Lets you slow someone with a gun."  },
                new ButtonInfo { buttonText = "Slow All", method =() => Overpowered.SlowAll(), isTogglable = true, toolTip = "Lets you slow everyone in the lobby."  },

                new ButtonInfo { buttonText = "Spawn Lucy", method =() => Utility.BetaSpawnLucy(HalloweenGhostChaser.ChaseState.Gong, true, Color.cyan), isTogglable = false, toolTip = "Spawns the ghost Lucy in forest."  },
                new ButtonInfo { buttonText = "Spawn Blue Lucy", method =() => Utility.BetaSpawnLucy(HalloweenGhostChaser.ChaseState.Gong, true, Color.blue), isTogglable = false, toolTip = "Spawns the ghost Lucy in forest." },
                new ButtonInfo { buttonText = "Spawn Red Lucy", method =() => Utility.BetaSpawnLucy(HalloweenGhostChaser.ChaseState.Gong, true, Color.red), isTogglable = false, toolTip = "Spawns the ghost Lucy in forest." },
                new ButtonInfo { buttonText = "Spawn Black Lucy", method =() => Utility.BetaSpawnLucy(HalloweenGhostChaser.ChaseState.Gong, true, Color.black), isTogglable = false, toolTip = "Spawns the ghost Lucy in forest." },
                new ButtonInfo { buttonText = "Spawn Yellow Lucy", method =() => Utility.BetaSpawnLucy(HalloweenGhostChaser.ChaseState.Gong, true, Color.yellow), isTogglable = false, toolTip = "Spawns the ghost Lucy in forest." },
                new ButtonInfo { buttonText = "Spawn RGB Lucy", method =() => Utility.BetaSpawnLucy(HalloweenGhostChaser.ChaseState.Gong, true, Utility.DoRGBColor(), true), isTogglable = false, toolTip = "Spawns the ghost Lucy in forest." },
                new ButtonInfo { buttonText = "Despawn Lucy", method =() => Utility.BetaSpawnLucy(HalloweenGhostChaser.ChaseState.Dormant, true, Color.cyan), isTogglable = false, toolTip = "Despawns the ghost Lucy in forest." },

                new ButtonInfo { buttonText = "Orbit Lucy Self", method =() => Utility.LucyOrbitSelf(), isTogglable = true, toolTip = "Makes Lucy orbit around your head."  },
                new ButtonInfo { buttonText = "Fling Lucy", method =() => Utility.LucyFlingGun(), isTogglable = false, toolTip = "Flings Lucy info the sky."  },

                new ButtonInfo { buttonText = "Move Lucy Gun", method =() => Utility.MoveLucyGun(), isTogglable = true, toolTip = "Lets you move Lucy with a gun."  },
                new ButtonInfo { buttonText = "Lucy Chase Gun", method =() => Utility.LucyAttackGun(), isTogglable = true, toolTip = "Lets you change Lucys target."  },
                new ButtonInfo { buttonText = "Lucy Spaz Attack", method =() => Utility.LucySpazAttack(), isTogglable = true, toolTip = "Makes Lucy spazz out when attacking someone."  },

                new ButtonInfo { buttonText = "Spaz Lucy", method =() => Utility.SpazLucy(), isTogglable = true, toolTip = "Spazzes Lucy when she tries to spawn."  },

                new ButtonInfo { buttonText = "Very Slow Lucy", method =() => Utility.BetaSetLucySpeed(0.1f), isTogglable = true, toolTip = "Changes Lucys speed."  },
                new ButtonInfo { buttonText = "Slow Lucy", method =() => Utility.BetaSetLucySpeed(0.5f), isTogglable = true, toolTip = "Changes Lucys speed."  },
                new ButtonInfo { buttonText = "Medium Lucy", method =() => Utility.BetaSetLucySpeed(0.7f), isTogglable = true, toolTip = "Changes Lucys speed."  },
                new ButtonInfo { buttonText = "Fast Lucy", method =() => Utility.BetaSetLucySpeed(5f), isTogglable = true, toolTip = "Changes Lucys speed."  },
                new ButtonInfo { buttonText = "Very Fast Lucy", method =() => Utility.BetaSetLucySpeed(15f), isTogglable = true, toolTip = "Changes Lucys speed."  },
                new ButtonInfo { buttonText = "Instant Kill Lucy", method =() => Utility.BetaSetLucySpeed(float.MaxValue), isTogglable = true, toolTip = "Changes Lucys speed."  },
                new ButtonInfo { buttonText = "Force Lucy Target", method =() => Utility.LucyTargetGun(), isTogglable = true, toolTip = "Changes Lucys target."  },
            },

            new ButtonInfo[] { // Soundboard | 15
                new ButtonInfo { buttonText = "Exit Soundboard", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
            },

            new ButtonInfo[] { // Players | 16
                new ButtonInfo { buttonText = "Exit Players", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
            },

            new ButtonInfo[] { // GTH Mods | 17
                new ButtonInfo { buttonText = "Exit GTH", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the settings page of the menu." },
                new ButtonInfo { buttonText = "Spawn Timmy", method =() => GTH.SpawnTimmy(), toolTip = "Spawns a timmy above your head.", isTogglable = true },
                new ButtonInfo { buttonText = "Spawn Stalker", method =() => GTH.SpawnStalker(), toolTip = "Spawns a stalker above your head.", isTogglable = true },
                new ButtonInfo { buttonText = "Timmy Spam [<color=cyan>G</color>]", method =() => GTH.TimmySpam(), toolTip = "Lets you spam timmys while holding your grips.", isTogglable = true },
                new ButtonInfo { buttonText = "Stalker Spam [<color=cyan>G</color>]", method =() => GTH.StalkerSpam(), toolTip = "Lets you spam stalkers while holding your grips.", isTogglable = true },
                new ButtonInfo { buttonText = "Timmy Gun", method =() => GTH.TimmyGun(), toolTip = "Lets you spawn timmys with a gun.", isTogglable = true },
                new ButtonInfo { buttonText = "Stalker Gun", method =() => GTH.StalkerGun(), toolTip = "Lets you spawn stalkers with a gun.", isTogglable = true },

                new ButtonInfo { buttonText = "Timmy ESP", method =() => GTH.TimmyESP(false), disableMethod =() => GTH.TimmyESP(true), toolTip = "Lets you find timmys through walls.", isTogglable = true },
                new ButtonInfo { buttonText = "Timmy Tracers", method =() => GTH.TimmyTracers(), toolTip = "Puts tracers on the timmys.", isTogglable = true },
                new ButtonInfo { buttonText = "Stalker ESP", method =() => GTH.StalkerESP(false), disableMethod =() => GTH.StalkerESP(true), toolTip = "Lets you find stalkers through walls.", isTogglable = true },
                new ButtonInfo { buttonText = "Stalker Tracers", method =() => GTH.StalkerTracers(), toolTip = "Puts tracers on the stalkers.", isTogglable = true },

                new ButtonInfo { buttonText = "Kill Gun", method =() => GTH.KillGun(), toolTip = "Lets you kill the person you shoot at with a gun.", isTogglable = true },
                new ButtonInfo { buttonText = "Kill All", method =() => GTH.KillAll(), toolTip = "Lets you kill everyone in the current room.", isTogglable = true },

                new ButtonInfo { buttonText = "Timmy Rape Gun", method =() => GTH.TimmyRapeGun(), toolTip = "Lets you rape someone using the timmys.", isTogglable = true },
                new ButtonInfo { buttonText = "Stalker Rape Gun", method =() => GTH.StalkerRapeGun(), toolTip = "Lets you rape someone using the stalkers.", isTogglable = true },

                new ButtonInfo { buttonText = "Fling Timmy Gun", method =() => GTH.FlingGun("timmy"), toolTip = "Lets you fling the timmy you shoot at.", isTogglable = true },
                new ButtonInfo { buttonText = "Fling Stalker Gun", method =() => GTH.FlingGun("stalker"), toolTip = "Lets you fling the stalker you shoot at.", isTogglable = true },
                new ButtonInfo { buttonText = "Fling Monter Gun", method =() => GTH.FlingGunComponent("EnemyController"), toolTip = "Lets you fling all the monsters you shoot at.", isTogglable = true },

                new ButtonInfo { buttonText = "Bring All Monsters", method =() => GTH.BringAllMonsters(), toolTip = "Lets you bring all the monsters to you.", isTogglable = false },
                new ButtonInfo { buttonText = "Bring Monsters Gun", method =() => GTH.BringMonstersGun(), toolTip = "Lets you bring all the monsters to the gun.", isTogglable = true },

                new ButtonInfo { buttonText = "Kill Timmy Gun", method =() => GTH.KillTimmyGun(), toolTip = "Lets you kill the timmy you shoot at.", isTogglable = true },
                new ButtonInfo { buttonText = "Kill Stalker Gun", method =() => GTH.KillStalkerGun(), toolTip = "Lets you kill the stalker you shoot at.", isTogglable = true },
                new ButtonInfo { buttonText = "Kill Monster Gun", method =() => GTH.KillMonsterGun(), toolTip = "Lets you kill the monster you shoot at.", isTogglable = true },

                new ButtonInfo { buttonText = "Kill All Timmys", method =() => GTH.KillAllTimmys(), toolTip = "Lets you kill all the timmys.", isTogglable = true },
                new ButtonInfo { buttonText = "Kill All Stalkers", method =() => GTH.KillAllStalkers(), toolTip = "Lets you kill all the stalkers.", isTogglable = true },
                new ButtonInfo { buttonText = "Kill All Monsters", method =() => GTH.KillAllMonsters(), toolTip = "Lets you kill all the monsters.", isTogglable = true },

                new ButtonInfo { buttonText = "Explode Timmy Gun", method =() => GTH.ExplodeTimmyGun(), toolTip = "Spawns timmys at the gun and explodes them.", isTogglable = true },
                new ButtonInfo { buttonText = "Explode Stalker Gun", method =() => GTH.ExplodeStalkerGun(), toolTip = "Spawns stalkers at the gun and explodes them.", isTogglable = true },
                new ButtonInfo { buttonText = "Explode Monsters", method =() => GTH.ExplodeMonsters(), toolTip = "Explodes all the monsters.", isTogglable = true },

                new ButtonInfo { buttonText = "Become Timmy", method =() => GTH.BecomeTimmy(), toolTip = "Lets you become the timmy in forest.", isTogglable = true },
                new ButtonInfo { buttonText = "Become Stalker", method =() => GTH.BecomeStalker(), toolTip = "Lets you become the stalkers.", isTogglable = true },

                new ButtonInfo { buttonText = "Fast Timmys", method =() => GTH.FastTimmys(), disableMethod =() => GTH.ResetTimmy(), toolTip = "Makes the timmys faster.", isTogglable = true },
                new ButtonInfo { buttonText = "Slow Timmys", method =() => GTH.SlowTimmys(), disableMethod =() => GTH.ResetTimmy(), toolTip = "Makes the timmys slower.", isTogglable = true },

                new ButtonInfo { buttonText = "Spaz Timmys", method =() => GTH.SpazTimmys(), toolTip = "Spazes the timmys in forest", isTogglable = true },
                new ButtonInfo { buttonText = "Spaz Stalkers", method =() => GTH.SpazStalkers(), toolTip = "Spazes all the stalkers", isTogglable = true },

                new ButtonInfo { buttonText = "Place Trap [<color=cyan>RT</color>]", method =() => GTH.PlaceTrap(), toolTip = "Places down a trap when you press right trigger.", isTogglable = true },
                new ButtonInfo { buttonText = "Destroy Trap", method =() => GTH.DestroyTrap(), toolTip = "Destroys the trap.", isTogglable = false },
                new ButtonInfo { buttonText = "Timmys To Trap", method =() => GTH.TimmysToTrap(), toolTip = "Makes all the timmys go to the trap.", isTogglable = true },
                new ButtonInfo { buttonText = "Stalkers To Trap", method =() => GTH.StalkersToTrap(), toolTip = "Makes all the stalkers go to the trap.", isTogglable = true },
                new ButtonInfo { buttonText = "Monsters To Trap", method =() => GTH.MonstersToTrap(), toolTip = "Makes all the monsters go to the trap.", isTogglable = true },

                new ButtonInfo { buttonText = "Timmy Work <color=cyan>[RT]</color>", method =() => GTH.TimmyWork(), toolTip = "Makes a firework out of timmys.", isTogglable = true },
                new ButtonInfo { buttonText = "Stalker Work <color=cyan>[RT]</color>", method =() => GTH.StalkerWork(), toolTip = "Makes a firework out of stalkers.", isTogglable = true },

                new ButtonInfo { buttonText = "Joystick Control Timmys <color=cyan>[RJ]</color>", method =() => GTH.JoystickControlTimmys(), toolTip = "Lets you control the timmys movement with your right joystick.", isTogglable = true },
                new ButtonInfo { buttonText = "Joystick Control Stalkers <color=cyan>[RJ]</color>", method =() => GTH.JoystickControlStalkers(), toolTip = "Lets you control the stalkers movement with your right joystick.", isTogglable = true },
                new ButtonInfo { buttonText = "Joystick Control Monsters <color=cyan>[RJ]</color>", method =() => GTH.JoystickControlMonters(), toolTip = "Lets you control the monsters movement with your right joystick.", isTogglable = true },
            },

            new ButtonInfo[] { }, // Temporary Category | 18

             new ButtonInfo[] { // Enabled | 19
                new ButtonInfo { buttonText = "Exit Enabled", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
            },

            new ButtonInfo[] { // Favorites | 20
                new ButtonInfo { buttonText = "Exit Favorite", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Returns to the main page of the menu." },
            },

            new[] { // Admin | 21
                new ButtonInfo { buttonText = "Exit Admin", method =() => CurrentCategoryName = "Main", isTogglable = false, toolTip = "Opens the visual page for the menu"},
                new ButtonInfo { buttonText = "Get Console Users", method =() => Experimental.GetMenuUsers(), isTogglable = false, toolTip = "Gets all users using console"},
                new ButtonInfo { buttonText = "Console Users NameTag", enableMethod =() => Console.ServerDataJupiterX.instance.adminnametags = true, disableMethod =() => Console.ServerDataJupiterX.instance.adminnametags = false, isTogglable = true, toolTip = "Enables the console nametags"},
                new ButtonInfo { buttonText = "Admin Quit All", method =() => Experimental.ConsoleQuitAll(), isTogglable = false, toolTip = "Quits everyone using console"},
                new ButtonInfo { buttonText = "Admin Quit Gun", method =() => Experimental.ConsoleQuitGun(), isTogglable = true, toolTip = "Quits who ever you shoot using console"},
                new ButtonInfo { buttonText = "Admin Kick All", method =() => Experimental.ConsoleKickAll(), isTogglable = false, toolTip = "Kicks everyone using console"},
                new ButtonInfo { buttonText = "Admin Kick Gun", method =() => Experimental.ConsoleKickGun(), isTogglable = true, toolTip = "Kicks who ever you shoot using console"},
                new ButtonInfo { buttonText = "Admin Fling All", method =() => Experimental.ConsoleFlingAll(), isTogglable = false, toolTip = "Flings everyone using console"},
                new ButtonInfo { buttonText = "Admin Fling Gun", method =() => Experimental.ConsoleFlingGun(), isTogglable = true, toolTip = "Flings who ever you shoot using console"},
                new ButtonInfo { buttonText = "Admin Bring All", method =() => Experimental.ConsoleBringAll(), isTogglable = false, toolTip = "Brings everyone using console to you"},
                new ButtonInfo { buttonText = "Admin Bring Gun", method =() => Experimental.ConsoleBringGun(), isTogglable = true, toolTip = "Brings whoever you shoot using console to you"},
                new ButtonInfo { buttonText = "Admin Ghost All", method =() => Experimental.ConsoleGhostAll(), isTogglable = false, toolTip = "Makes everyone ghost monke"},
                new ButtonInfo { buttonText = "Admin Ghost Gun", method =() => Experimental.ConsoleGhostGun(), isTogglable = true, toolTip = "Makes who ever you shoot ghost monke"},
                new ButtonInfo { buttonText = "Admin UnGhost All", method =() => Experimental.ConsoleUnGhostAll(), isTogglable = false, toolTip = "Fixes everyones rig"},
                new ButtonInfo { buttonText = "Admin UnGhost Gun", method =() => Experimental.ConsoleUnGhostGun(), isTogglable = true, toolTip = "Fixes who you shoot rig"},
                new ButtonInfo { buttonText = "Admin Disable Movement All", method =() => Experimental.ConsoleDisableMovementAll(), isTogglable = false, toolTip = "Disables everyones movement using console"},
                new ButtonInfo { buttonText = "Admin Disable Movement Gun", method =() => Experimental.ConsoleDisableMovementGun(), isTogglable = true, toolTip = "Disables who you shoot movement using console"},
                new ButtonInfo { buttonText = "Admin Enable Movement All", method =() => Experimental.ConsoleEnableMovementAll(), isTogglable = false, toolTip = "Reanbles everyones movement using console"},
                new ButtonInfo { buttonText = "Admin Enable Movement Gun", method =() => Experimental.ConsoleEnableMovementGun(), isTogglable = true, toolTip = "Reanbles who you shoot movement using console"},
                new ButtonInfo { buttonText = "Admin Mute All", method =() => Experimental.ConsoleMuteAll(), isTogglable = false, toolTip = "Mutes everyone using console"},
                new ButtonInfo { buttonText = "Admin Mute Gun", method =() => Experimental.ConsoleMuteGun(), isTogglable = true, toolTip = "Mutes who you shoot using console"},
                new ButtonInfo { buttonText = "Admin UnMute All", method =() => Experimental.ConsoleUnMuteAll(), isTogglable = false, toolTip = "UnMutes everyone using console"},
                new ButtonInfo { buttonText = "Admin UnMute Gun", method =() => Experimental.ConsoleUnMuteGun(), isTogglable = true, toolTip = "UnMutes who you shoot using console"},
                new ButtonInfo { buttonText = "Admin Network Player All", method =() => Experimental.ConsoleNetworkPlayerAll(), isTogglable = false, toolTip = "Spawns a network player at people using console"},
                new ButtonInfo { buttonText = "Admin Network Player Gun", method =() => Experimental.ConsoleNetworkPlayerGun(), isTogglable = true, toolTip = "Spawns a network player at who you shoot using console"},
                new ButtonInfo { buttonText = "Admin Target All", method =() => Experimental.ConsoleTargetPlayerAll(), isTogglable = false, toolTip = "Spawns a stickable target at everyone using console"},
                new ButtonInfo { buttonText = "Admin Target Gun", method =() => Experimental.ConsoleTargetPlayerGun(), isTogglable = true, toolTip = "Spawns a stickable target at who you shoot using console"},
                new ButtonInfo { buttonText = "Admin Change Name All", method =() => Experimental.ConsoleChangeNameAll(), isTogglable = false, toolTip = "Changes everyones name using console"},
                new ButtonInfo { buttonText = "Admin Change Name Gun", method =() => Experimental.ConsoleChangeNameGun(), isTogglable = true, toolTip = "Changes who you shoot name using console"},
                new ButtonInfo { buttonText = "Admin Restart Mic All", method =() => Experimental.ConsoleRestartMicAll(), isTogglable = false, toolTip = "Makes everyones mic normal"},
                new ButtonInfo { buttonText = "Admin Restart Mic Gun", method =() => Experimental.ConsoleRestartMicGun(), isTogglable = true, toolTip = "Makes who you shoot mic normal"},
            },

            new[] { // Plugin Settings | 22
                new ButtonInfo { buttonText = "Exit Plugin Settings", method =() => CurrentCategoryName = "Settings", isTogglable = false, toolTip = "Returns you back to the settings menu."},
                new ButtonInfo { buttonText = "Reload Plugins", method = PluginManager.ReloadPlugins, isTogglable = false, toolTip = "Reloads all of your plugins." }
            },

            new [] // public not seen to user
            {
                //new ButtonInfo { buttonText = "Search", method = KeyboardManager.Search, isTogglable = false, toolTip = "Lets you search for specific mods."},
                new ButtonInfo { buttonText = "Global Return", method = Settings.GlobalReturn, isTogglable = false, toolTip = "Returns you to the previous category."},
                new ButtonInfo { buttonText = "Accept Prompt", method =() => { NotifiLib.ClearAllNotifications(); CurrentPrompt.AcceptAction?.Invoke(); Utility.StopCurrentPrompt(); }, isTogglable = false},
                new ButtonInfo { buttonText = "Decline Prompt", method =() => { NotifiLib.ClearAllNotifications(); CurrentPrompt.DeclineAction?.Invoke(); Utility.StopCurrentPrompt(); }, isTogglable = false},
            }
        };

        public static string[] categoryNames =
        {
            "Main",                
            "Settings",             
            "Menu Settings",        
            "Movement Settings",    
            "Gun Settings",        

            "Important",           
            "Safety",               
            "Computer",             
            "Movement",             
            "Advantage",            
            "VRRig",               
            "Visual",  
            "Fun",
            "Name",                
            "Prefabs",            
            "Overpowered",
            "Experimental",     
            "Master", 

            "Soundboard",           
            "Players",             
            "GTH",                  

            "Temporary Category",
            
            "Enabled",
            "Favorite",
            "Admin",
            "Plugin Settings",
            "Internal"
        };

        public static int _currentCategoryIndex;
        public static event Action OnCategoryChanged;

        public static int CurrentCategoryIndex
        {
            get => _currentCategoryIndex;
            set
            {
                _currentCategoryIndex = value;
                pageNumber = 0;

                OnCategoryChanged?.Invoke();
            }
        }

        public static string CurrentCategoryName
        {
            get => Buttons.categoryNames[CurrentCategoryIndex];
            set =>
                CurrentCategoryIndex = Buttons.GetCategory(value);
        }

        private static readonly Dictionary<string, (int Category, int Index)> cacheGetIndex = new Dictionary<string, (int Category, int Index)>(); // Looping through 800 elements is not a light task :/

        public static ButtonInfo GetIndex(string buttonText)
        {
            if (buttonText == null)
                return null;

            if (cacheGetIndex.TryGetValue(buttonText, out var cacheData))
            {
                try
                {
                    if (buttons[cacheData.Category][cacheData.Index].buttonText == buttonText)
                        return buttons[cacheData.Category][cacheData.Index];
                }
                catch { cacheGetIndex.Remove(buttonText); }
            }

            int categoryIndex = 0;
            foreach (ButtonInfo[] buttons in buttons)
            {
                int buttonIndex = 0;
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        try
                        {
                            cacheGetIndex.Add(buttonText, (categoryIndex, buttonIndex));
                        }
                        catch
                        {
                            cacheGetIndex.Remove(buttonText);
                        }

                        return button;
                    }
                    buttonIndex++;
                }
                categoryIndex++;
            }

            return null;
        }
        public static int GetCategory(string categoryName) =>
            categoryNames.ToList().IndexOf(categoryName);

        public static int AddCategory(string categoryName)
        {
            List<ButtonInfo[]> buttonInfoList = buttons.ToList();
            buttonInfoList.Add(new ButtonInfo[] { });
            buttons = buttonInfoList.ToArray();

            List<string> categoryList = categoryNames.ToList();
            categoryList.Add(categoryName);
            categoryNames = categoryList.ToArray();

            return buttons.Length - 1;
        }
        public static void RemoveCategory(string categoryName)
        {
            List<ButtonInfo[]> buttonInfoList = buttons.ToList();
            buttonInfoList.RemoveAt(GetCategory(categoryName));
            buttons = buttonInfoList.ToArray();

            List<string> categoryList = categoryNames.ToList();
            categoryList.Remove(categoryName);
            categoryNames = categoryList.ToArray();
        }
        public static void AddButton(int category, ButtonInfo button, int index = -1)
        {
            List<ButtonInfo> buttonInfoList = buttons[category].ToList();
            if (index > 0)
                buttonInfoList.Insert(index, button);
            else
                buttonInfoList.Add(button);

            buttons[category] = buttonInfoList.ToArray();
        }
        public static void AddButtons(int category, ButtonInfo[] buttons, int index = -1)
        {
            List<ButtonInfo> buttonInfoList = Buttons.buttons[category].ToList();
            if (index > 0)
            {
                for (int i = 0; i < buttons.Length; i++)
                    buttonInfoList.Insert(index + i, buttons[i]);
            }
            else
                buttonInfoList.AddRange(buttons);

            Buttons.buttons[category] = buttonInfoList.ToArray();
        }
        public static void RemoveButton(int category, string name, int index = -1)
        {
            List<ButtonInfo> buttonInfoList = buttons[category].ToList();
            if (index > 0)
                buttonInfoList.RemoveAt(index);
            else
            {
                foreach (var button in buttonInfoList.Where(button => button.buttonText == name))
                {
                    buttonInfoList.Remove(button);
                    break;
                }
            }

            buttons[category] = buttonInfoList.ToArray();
        }
    }
}

// Mod Graveyard - mods here are removed for one reason or another
/*
new ButtonInfo { buttonText = "Dick Spawn [<color=cyan>RT</color>]", method =() => Experimental.DickSpawn(), isTogglable = true },
new ButtonInfo { buttonText = "MeowMeow Cube Spawn [<color=cyan>RT</color>]", method = () => Experimental.MeowMeowCubeSpawn(), isTogglable = true },

new ButtonInfo { buttonText = "Eternal Sugar Cookie Spam [<color=cyan>RT</color>]", method =() => Experimental.eternalsugercookieSpammer(), isTogglable = true }, aw man

new ButtonInfo { buttonText = "Get F'd Spawn", method = () => Experimental.GetFuckedNetPlayers(), isTogglable = false },

new ButtonInfo { buttonText = "Ban Gun [JX Modding Game]", method =() => Overpowered.BanGunJXModding(), isTogglable = true },

new ButtonInfo { buttonText = "Dynamic Animations", enableMethod =() => dynamicAnimations = true, disableMethod =() => dynamicAnimations = false },

new ButtonInfo { buttonText = "Teleport To Slingshot", method =() => Utility.BetaTPToSling(), isTogglable = true },
*/