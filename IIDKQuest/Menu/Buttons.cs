using JupiterX.Classes;
using JupiterX.Mods;
using static JupiterX.Settings;
using static JupiterX.Menu.Main;
using UnityEngine;

// this menu was created by Silent (@s1lnt)
// if you remove this it counts as skidding
namespace JupiterX.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods | 0
                new ButtonInfo { buttonText = "Settings", method =() => Settings.MovePage(1), isTogglable = false, toolTip = "Opens the main settings page for the menu."},
                new ButtonInfo { buttonText = "Important", method =() => Settings.MovePage(2), isTogglable = false, toolTip = "Opens the important mods page for the menu."},
                new ButtonInfo { buttonText = "Safety", method =() => Settings.MovePage(3), isTogglable = false, toolTip = "Opens the safety mods page for the menu."},
                new ButtonInfo { buttonText = "Computer", method =() => Settings.MovePage(4), isTogglable = false, toolTip = "Opens the computer mods page for the menu."},
                new ButtonInfo { buttonText = "Movement", method =() => Settings.MovePage(5), isTogglable = false, toolTip = "Opens the movement mods page for the menu."},
                new ButtonInfo { buttonText = "Advantage", method =() => Settings.MovePage(6), isTogglable = false, toolTip = "Opens the advantage mods page for the menu."},
                new ButtonInfo { buttonText = "VRRig", method =() => Settings.MovePage(7), isTogglable = false, toolTip = "Opens the vrrig mods page for the menu."},
                new ButtonInfo { buttonText = "Visual", method =() => Settings.MovePage(8), isTogglable = false, toolTip = "Opens the visual mods page for the menu."},
                new ButtonInfo { buttonText = "Name", method =() => Settings.MovePage(9), isTogglable = false, toolTip = "Opens the name mods page for the menu."},
                new ButtonInfo { buttonText = "Prefabs", method =() => Settings.MovePage(10), isTogglable = false, toolTip = "Opens the prefab mods page for the menu."},
                new ButtonInfo { buttonText = "Overpowered", method =() => Settings.MovePage(11), isTogglable = false, toolTip = "Opens the overpowered page for the menu."},
                new ButtonInfo { buttonText = "Experimental", method =() => Settings.MovePage(12), isTogglable = false, toolTip = "Opens the expermental mods page for the menu."},
                new ButtonInfo { buttonText = "Master", method =() => Settings.MovePage(13), isTogglable = false, toolTip = "Opens the master mods page for the menu."},
                new ButtonInfo { buttonText = "Soundboard", method =() => Settings.Soundboard(), isTogglable = false, toolTip = "Opens the soundboard page for the menu."},
                new ButtonInfo { buttonText = "Player", method =() => Settings.Players(), isTogglable = false, toolTip = "Opens the player mods page for the menu."},
            },

            new ButtonInfo[] { // Settings | 1
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Return to Settings", method =() => Settings.MovePage(1), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
                new ButtonInfo { buttonText = "Right Hand", enableMethod =() => Settings.RightHand(), disableMethod =() => Settings.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "Save Preferences", method =() => Utility.SavePreferences(), isTogglable = false, toolTip = "Saves your enabled mods to file." },
                new ButtonInfo { buttonText = "Load Preferences", method =() => Utility.LoadPreferences(), isTogglable = false, toolTip = "Loads your saved mods from a file." },
                new ButtonInfo { buttonText = "Stump Text", method =() => Settings.EnableStumpText(), disableMethod =() => Settings.DisableStumpText(), enabled = stumptext, toolTip = "Toggles the stump text."},
                new ButtonInfo { buttonText = "Custom Boards", enabled = true, isTogglable = true, toolTip = "Enables the custom boards in stump." },
                new ButtonInfo { buttonText = "Move Stump Text Gun", method =() => Utility.MoveStumpTextGun(), isTogglable = true, toolTip = "Lets you move the stump text with a gun." },
                new ButtonInfo { buttonText = "Version Text", enableMethod =() => Settings.EnableFPSCounter(), disableMethod =() => Settings.DisableFPSCounter(), enabled = fpsCounter, toolTip = "Toggles the Version Text."},
                new ButtonInfo { buttonText = "Notifications", enableMethod =() => Settings.EnableNotis(), disableMethod =() => Settings.DisableNotis(), enabled = !Settings.disableNotis, toolTip = "Toggles the Notifcations."},
                new ButtonInfo { buttonText = "Disconnect Button", enableMethod =() => Settings.EnableDisconnectButton(), disableMethod =() => Settings.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
                new ButtonInfo { buttonText = "Change Page Type", method =() => Utility.ChangePageType(), overlapText = "Change Page Type <color=cyan>[Side]</color>", isTogglable = false, toolTip = "Changes the page type." },
                new ButtonInfo { buttonText = "Freeze Player In Menu", method =() => Utility.FreezePlayerInMenu(), isTogglable = true, toolTip = "Lets you float while the menu is open."},
                new ButtonInfo { buttonText = "Ghost In Menu", method =() => Utility.GhostInMenu(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Makes you have ghost monke when menu is open."},

                new ButtonInfo { buttonText = "Change Fly Speed", method =() => Movement.ChangeFlySpeed(), overlapText = "Change Fly Speed <color=cyan>[Very Slow]</color>", isTogglable = false, toolTip = "Changes the current fly speed." },
                new ButtonInfo { buttonText = "Change Arm Length", method =() => Movement.ChangeArmLength(), overlapText = "Change Arm Length <color=cyan>[Stean]</color>", isTogglable = false, toolTip = "Changes your arm length." },

                // Creds to iiDk again <3
                new ButtonInfo { buttonText = "Credits to iiDk for gun lib/settings", isTogglable = false, toolTip = "Credits to iiDk for the gunlib and gunlib settings <3." },
                new ButtonInfo { buttonText = "Change Gun Line Quality", overlapText = "Change Gun Line Quality <color=grey>[</color><color=cyan>Normal</color><color=grey>]</color>", method =() => Settings.ChangeGunLineQuality(), enableMethod =() => Settings.ChangeGunLineQuality(), disableMethod =() => Settings.ChangeGunLineQuality(false), isTogglable = false, toolTip = "Changes the amount of points on your gun."},
                new ButtonInfo { buttonText = "Change Gun Variation", overlapText = "Change Gun Variation <color=grey>[</color><color=cyan>Default</color><color=grey>]</color>", method =() => Settings.ChangeGunVariation(), enableMethod =() => Settings.ChangeGunVariation(), disableMethod =() => Settings.ChangeGunVariation(false), isTogglable = false, toolTip = "Changes the look of the gun."},
                new ButtonInfo { buttonText = "Change Gun Direction", overlapText = "Change Gun Direction <color=grey>[</color><color=cyan>Default</color><color=grey>]</color>", method =() => Settings.ChangeGunDirection(), enableMethod =() => Settings.ChangeGunDirection(), disableMethod =() => Settings.ChangeGunDirection(false), isTogglable = false, toolTip = "Changes the direction of the gun."},

                new ButtonInfo { buttonText = "Gun Sounds", enableMethod =() => GunSounds = true, disableMethod =() => GunSounds = false, toolTip = "Gives the gun laser sounds for when you press grip and trigger."},
                new ButtonInfo { buttonText = "Gun Particles", enableMethod =() => GunParticles = true, disableMethod =() => GunParticles = false, toolTip = "Gives the gun particles when you shoot it."},
                new ButtonInfo { buttonText = "Swap Gun Hand", enableMethod =() => SwapGunHand = true, disableMethod =() => SwapGunHand = false, toolTip = "Swaps the hand gun mods work with."},
                new ButtonInfo { buttonText = "Gripless Guns", enableMethod =() => GriplessGuns = true, disableMethod =() => GriplessGuns = false, toolTip = "Forces your grip to be held for guns."},
                new ButtonInfo { buttonText = "Triggerless Guns", enableMethod =() => TriggerlessGuns = true, disableMethod =() => TriggerlessGuns = false, toolTip = "Forces your trigger to be held for guns."},
                new ButtonInfo { buttonText = "Hard Gun Lock", enableMethod =() => HardGunLocks = true, disableMethod =() => HardGunLocks = false, toolTip = "Locks the guns even when letting go of grip until you press <color=green>B</color>."},
                new ButtonInfo { buttonText = "Small Gun Pointer", enableMethod =() => smallGunPointer = true, disableMethod =() => smallGunPointer = false, toolTip = "Makes the ball at the end of every gun mod smaller."},
                new ButtonInfo { buttonText = "Smooth Gun Pointer", enableMethod =() => SmoothGunPointer = true, disableMethod =() => SmoothGunPointer = false, toolTip = "Makes the ball at the end of every gun mod smoother."},
                new ButtonInfo { buttonText = "Disable Gun Pointer", enableMethod =() => disableGunPointer = true, disableMethod =() => disableGunPointer = false, toolTip = "Disables the ball at the end of every gun mod."},
                new ButtonInfo { buttonText = "Disable Gun Line", enableMethod =() => disableGunLine = true, disableMethod =() => disableGunLine = false, toolTip = "Disables the gun from your hand to the end of every gun mod."},

            },

            new ButtonInfo[] { // Important | 2
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Quit Game", method =() => Important.QuitGame(), isTogglable = false, toolTip = "Quits your game." },
                new ButtonInfo { buttonText = "Anti AFK", method =() => Important.AntiAFK(), isTogglable = true, toolTip = "Disables the afk kick you get." },
                new ButtonInfo { buttonText = "Clear Notifcations", method =() => NotificationManager.ClearAllNotifications(), isTogglable = false, toolTip = "Clears all the notifications." },

                new ButtonInfo { buttonText = "Mute Gun", method =() => Experimental.MuteGun(), isTogglable = true, toolTip = "Lets you mute the player you shoot the gun at." },
                new ButtonInfo { buttonText = "UnMute Gun", method =() => Experimental.UnMuteGun(), isTogglable = true, toolTip = "Lets you unmute the player you shoot the gun at." },
                new ButtonInfo { buttonText = "Mute All", method =() => Experimental.MuteAll(), isTogglable = false, toolTip = "Lets you mute everyone in the current room." },
                new ButtonInfo { buttonText = "UnMute All", method =() => Experimental.UnMuteAll(), isTogglable = false, toolTip = "Lets you unmute everyone in the current room." },
            },

            new ButtonInfo[] { // Safety | 3
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Panic", method =() => Utility.Panic(), isTogglable = false, toolTip = "Disables every mod you have enabled." },
                new ButtonInfo { buttonText = "Anti Report [<color=yellow>Disconnect</color>]", method =() => Utility.BetaAntiReport(false, true), isTogglable = true, toolTip = "Disconnects you when someone is close to the report button." },
                new ButtonInfo { buttonText = "Anti Report [<color=yellow>Crash</color>]", method =() => Utility.BetaAntiReport(true, false), isTogglable = true, toolTip = "Crashes the person who tries to report you when someone is close to the report button." },
                new ButtonInfo { buttonText = "Anti Moderator", method =() => Utility.BetaAntiCosmetic("LBAAK."), isTogglable = true, toolTip = "Disconnects you when someone has the moderator stick." },
                new ButtonInfo { buttonText = "Anti Admin", method =() => Utility.BetaAntiCosmetic("LBAAD."), isTogglable = true, toolTip = "Disconnects you when someone has the admin badge." },
                new ButtonInfo { buttonText = "Anti Finger Painter", method =() => Utility.BetaAntiCosmetic("LBADE."), isTogglable = true, toolTip = "Disconnects you when someone has the finger painter." },
            },

            new ButtonInfo[] { // Computer | 4
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Disconnect", method =() => Computer.Leave(), isTogglable = false, toolTip = "Disconnects you from the lobby." },
                new ButtonInfo { buttonText = "Reconnect", method =() => Important.Reconnect(), isTogglable = false, toolTip = "Reconnects you to the current lobby." },
                new ButtonInfo { buttonText = "Join Random", method =() => Computer.Jrr(), isTogglable = false, toolTip = "Lets you join a random room." },
                new ButtonInfo { buttonText = "Join Code '1'", method =() => Computer.JoinCode("1"), isTogglable = false, toolTip = "Lets you join the code \"1\"." },
                new ButtonInfo { buttonText = "Join Code 'JupiterX'", method =() => Computer.JoinCode("_@-JupiterX-@_"), isTogglable = false, toolTip = "Lets you join the code \"JupiterX\"."  },
                new ButtonInfo { buttonText = "Join Code 'Mods'", method =() => Computer.JoinCode("MODS"), isTogglable = false, toolTip = "Lets you join the code \"MODS\"."  },
                new ButtonInfo { buttonText = "Join Code 'Mod'", method =() => Computer.JoinCode("MOD"), isTogglable = false, toolTip = "Lets you join the code \"MOD\"."  },
                new ButtonInfo { buttonText = "Join Code 'Pbbv'", method =() => Computer.JoinCode("PBBV"), isTogglable = false, toolTip = "Lets you join the code \"PBBV\"."  },
                new ButtonInfo { buttonText = "Join Code 'Daisy'", method =() => Computer.JoinCode("DAISY"), isTogglable = false, toolTip = "Lets you join the code \"DAISY\"."  },
            },

            new ButtonInfo[] { // Movement | 5
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
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
            },

            new ButtonInfo[] { // Advantage | 6
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Tag All", method =() => Advantage.TagAll(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you tag everyone in the lobby."  },
                new ButtonInfo { buttonText = "Tag Aura", method =() => Advantage.TagAura(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you tag someone when they come close to you."  },
                new ButtonInfo { buttonText = "Tag Gun", method =() => Advantage.TagGun(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you tag someone with a gun."  },
                new ButtonInfo { buttonText = "Flick Tag Gun", method =() => Advantage.FlickTagGun(), isTogglable = true, toolTip = "Lets you flick tag someone with a gun."  },
                new ButtonInfo { buttonText = "Tag Gun RPC", method =() => Advantage.TagGunRPC(), isTogglable = true, toolTip = "Lets you tag someone with a gun."  },
            },

            new ButtonInfo[] { // VRRig | 7
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Ghost Monke <color=cyan>[A]</color>", method =() => vRRig.GhostMonke(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you become a ghost."  },
                new ButtonInfo { buttonText = "Invis Monke <color=cyan>[B]</color>", method =() => vRRig.InvisMonke(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you become invisable."  },
                new ButtonInfo { buttonText = "Grab Rig <color=cyan>[<color=cyan>Grips</color>]</color>", method =() => vRRig.GrabRig(), disableMethod = Utility.FixGhostRig, isTogglable = true, toolTip = "Lets you grab your rig while holding right grip."  },
                new ButtonInfo { buttonText = "Spaz Rig", method =() => vRRig.SpazRig(), disableMethod = vRRig.FixSpazRig, isTogglable = true, toolTip = "Makes your rig spazz out."  },
            },

            new ButtonInfo[] { // Visual | 8
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Chams", method =() => Visual.Chams(true), disableMethod =() => Visual.Chams(false), isTogglable = true, toolTip = "Lets you see players through walls."  },
                new ButtonInfo { buttonText = "Full Bright", method =() => Visual.fullBright(), disableMethod =() => Visual.fulldrak(), isTogglable = true, toolTip = "Lets you see in the dark."  },
                new ButtonInfo { buttonText = "Tracers", method =() => Visual.Tracers(), isTogglable = true, toolTip = "Points lines at other players."  },
                new ButtonInfo { buttonText = "Box ESP", method =() => Visual.BoxESP(), isTogglable = true, toolTip = "Lets you see players through walls."  },
                new ButtonInfo { buttonText = "Sphere ESP", method =() => Visual.SphereESP(), isTogglable = true, toolTip = "Lets you see players through walls."  },
                new ButtonInfo { buttonText = "Name Tags", method =() => Visual.NameTagESP(), isTogglable = true, toolTip = "Lets you see player info above there head with a name tag."  },
                new ButtonInfo { buttonText = "Velocity Label", method =() => Visual.VelocityLabel(), isTogglable = true, toolTip = "Lets you see your velocity with a label on your right hand."  },
                new ButtonInfo { buttonText = "Player Count Label", method =() => Visual.LeftTaggedLabel(), isTogglable = true, toolTip = "Lets you see how manu tagged people are left."  }, // put in graveyard
            },

            new ButtonInfo[] { // Name | 9
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Menu Name Tag", method =() => Name.MenuNameTag(), isTogglable = true, toolTip = "Sets your name to the menu name."  },
                new ButtonInfo { buttonText = "Owner Name", method =() => Name.ChangeName("\nOwner"), isTogglable = true, toolTip = "Sets your name as Owner."  },
                new ButtonInfo { buttonText = "BSU Skids Name", method =() => Name.ChangeName("BSU Menu is skidded\nBSU Skids"), isTogglable = true, toolTip = "Sets your name as BSU Skids."  },
                new ButtonInfo { buttonText = "PBBV Name", method =() => Name.ChangeName("\nPBBV"), isTogglable = true, toolTip = "Sets your name as PBBV."  },
                new ButtonInfo { buttonText = "ECHO Name", method =() => Name.ChangeName("\nECHO"), isTogglable = true, toolTip = "Sets your name as ECHO."  },
                new ButtonInfo { buttonText = "DAISY09 Name", method =() => Name.ChangeName("\nDAISY09"), isTogglable = true, toolTip = "Sets your name as DAISY09."  },
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

            new ButtonInfo[] { // Prefabs | 10
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Clear Prefabs", method =() => Prefabs.ClearPrefabs(), isTogglable = false, toolTip = "Clears every prefab with a photon view."  },
                new ButtonInfo { buttonText = "Cube Spam [<color=cyan>Grips</color>]", method =() => Prefabs.CubeSpam(), isTogglable = true, toolTip = "Lets you spam cubes while holding grip."  },
                new ButtonInfo { buttonText = "Give Cube Spam Gun", method =() => Prefabs.GiveSpamGun(0), isTogglable = true, toolTip = "Lets you give someone cube spam when they hold grip."  },
                new ButtonInfo { buttonText = "Target Spam [<color=cyan>Grips</color>]", method =() => Prefabs.TargetSpam(), isTogglable = true, toolTip = "Lets you spam stickable targets while holding grip."  },
                new ButtonInfo { buttonText = "Give Target Spam Gun", method =() => Prefabs.GiveSpamGun(1), isTogglable = true, toolTip = "Lets you give someone stickable target spam when they hold grip."  },
                new ButtonInfo { buttonText = "Network Player Spam [<color=cyan>Grips</color>]", method =() => Prefabs.NetworkPlayerSpam(), isTogglable = true, toolTip = "Lets you spam network players when holding grip."  },
                new ButtonInfo { buttonText = "Give Network Player Spam Gun", method =() => Prefabs.GiveSpamGun(2), isTogglable = true, toolTip = "Lets you give someone network player spam when they are holding grip."  },
                new ButtonInfo { buttonText = "Enemy Spam [<color=cyan>Grips</color>]", method =() => Prefabs.EnemySpam(), isTogglable = true, toolTip = "Lets you spam enemys when holding grip."  },
                new ButtonInfo { buttonText = "Give Enemy Spam Gun", method =() => Prefabs.GiveSpamGun(3), isTogglable = true, toolTip = "Lets you give someone enemy spam when they are holding grip."  },
                new ButtonInfo { buttonText = "Cube Gun", method =() => Prefabs.CubeGun(), isTogglable = true, toolTip = "Lets you shoot cubes with a gun."  },
                new ButtonInfo { buttonText = "Target Gun", method =() => Prefabs.TargetGun(), isTogglable = true, toolTip = "Lets you shoot targets with a gun."  },
                new ButtonInfo { buttonText = "Network Player Gun", method =() => Prefabs.NetworkPlayerGun(), isTogglable = true, toolTip = "Lets you shoot network players with a gun."  },
                new ButtonInfo { buttonText = "Enemy Gun", method =() => Prefabs.EnemyGun(), isTogglable = true, toolTip = "Lets you shoot enemys with a gun."  },
            },

            new ButtonInfo[] { // Overpowered | 11
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Set Master", method =() => Overpowered.SetMaster(), isTogglable = true, toolTip = "Sets you as master client."  },

                new ButtonInfo { buttonText = "RPC Lag All [<color=cyan>RT</color>]", method =() => Overpowered.RPCLag(), isTogglable = true, toolTip = "Lags people using rpcs while holding right trigger."  },

                new ButtonInfo { buttonText = "Kick Gun [<color=red>W?</color>]", method =() => Overpowered.KickGun(), isTogglable = true, toolTip = "Lets you kick someone with a gun in stump."  },
                new ButtonInfo { buttonText = "Kick All [<color=red>W?</color>]", method =() => Overpowered.KickAll(), isTogglable = true, toolTip = "Lets you kick everyone in stump."  },

                new ButtonInfo { buttonText = "Inf Shiny Rocks", method =() => Overpowered.BetaChangeShinyRock(int.MaxValue), isTogglable = false, toolTip = "Gives you infinite shiny rocks."  },

                new ButtonInfo { buttonText = "Unban Self", method =() => Experimental.UnBanSelf(), isTogglable = false, toolTip = "Unbans yourself if you get banned."  },

                new ButtonInfo { buttonText = "Spaz Forest Targets", method =() => Experimental.SpazForestTargets(), isTogglable = true, toolTip = "Spazzes out the targets that are in forest."  },

                new ButtonInfo { buttonText = "Ban All", method =() => Utility.BanAll(), isTogglable = true, toolTip = "Lets you ban everyone in the current room."  },
                new ButtonInfo { buttonText = "Ban Gun", method =() => Overpowered.BanGun(), isTogglable = true, toolTip = "Lets you ban someone you shoot at."  },

                new ButtonInfo { buttonText = "Crash All [<color=cyan>RT</color>]", method =() => Overpowered.CrashAll(), isTogglable = true, toolTip = "Lets you crash all while holding right trigger."  },
                new ButtonInfo { buttonText = "Crash All V2 [<color=cyan>RT</color>]", method =() => Overpowered.CrashAllV2(), isTogglable = true, toolTip = "Lets you crash all while holding right trigger."  },
                new ButtonInfo { buttonText = "Crash All V3 [<color=cyan>RT</color>]", method =() => Overpowered.CrashAllV3(), isTogglable = true, toolTip = "Lets you crash all while holding right trigger."  },
                new ButtonInfo { buttonText = "Crash Gun", method =() => Overpowered.CrashGun(), isTogglable = true, toolTip = "Lets you crash someone you shoot at."  },
                new ButtonInfo { buttonText = "Crash Gun V2", method =() => Overpowered.CrashGunV2(), isTogglable = true, toolTip = "Lets you crash someone you shoot at."  },

                new ButtonInfo { buttonText = "Spam Pop & Unpop Balloon [<color=cyan>RT</color>]", method =() => Experimental.BalloonSpam(), isTogglable = true, toolTip = "Spams everyones balloon while holding right trigger."  },

                new ButtonInfo { buttonText = "Set GameMode [<color=yellow>CASUAL</color>]", method =() => Experimental.SetGameMode("CASUAL"), isTogglable = false, toolTip = "Sets the game mode to casual."  },
                new ButtonInfo { buttonText = "Set GameMode [<color=yellow>INFECTION</color>]", method =() => Experimental.SetGameMode("INFECTION"), isTogglable = false, toolTip = "Sets the game mode to infection."  },
                new ButtonInfo { buttonText = "Set GameMode [<color=yellow>HUNT</color>]", method =() => Experimental.SetGameMode("HUNT"), isTogglable = false, toolTip = "Sets the game mode to hunt."  },
                new ButtonInfo { buttonText = "Set GameMode [<color=yellow>PAINTBRAWL</color>]", method =() => Experimental.SetGameMode("PAINTBRAWL"), isTogglable = false, toolTip = "Sets the game mode to battle."  },
            },

            new ButtonInfo[] { // Experimental | 12
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
                new ButtonInfo { buttonText = "Cube All [<color=cyan>RT</color>]", method =() => Experimental.CumAll(), isTogglable = true, toolTip = "Lets you spawn cubes at others while holding right trigger."  },
                new ButtonInfo { buttonText = "Get Fucked Spawn [Forest, Targets]", method =() => Experimental.GetFucked(), isTogglable = false, toolTip = "Spawns the word 'Get Fucked' using stickable targets in forest."  },

                new ButtonInfo { buttonText = "Spam Mute All", method =() => Utility.PacketStresser(), isTogglable = true, toolTip = "Spams all the report and mute buttons."  },
                new ButtonInfo { buttonText = "Spam Mute All V2", method =() => Utility.BetaSpamMuteAll(), isTogglable = true, toolTip = "Spams all the report and mute buttons."  },

                new ButtonInfo { buttonText = "Stick In Cart", method =() => Utility.BetaAddItemToCart("LBAAK."), isTogglable = false, toolTip = "Lets you buy the moderator stick."  },
                new ButtonInfo { buttonText = "Admin In Cart", method =() => Utility.BetaAddItemToCart("LBAAD."), isTogglable = false, toolTip = "Lets you buy the admin badge."  },
                new ButtonInfo { buttonText = "UNR Sweater In Cart", method =() => Utility.BetaAddItemToCart("LBACP."), isTogglable = false, toolTip = "Lets you buy the unreleased sweater."  },
                new ButtonInfo { buttonText = "Finger Painter In Cart", method =() => Utility.BetaAddItemToCart("LBADE."), isTogglable = false, toolTip = "Lets you buy the finger painter."  },

                new ButtonInfo { buttonText = "Gold Wrench In Cart", method =() => Utility.BetaAddItemToCart("LBABC."), isTogglable = false, toolTip = "Lets you buy the golden wrench."  },
                new ButtonInfo { buttonText = "Cat Ears In Cart", method =() => Utility.BetaAddItemToCart("LHAAB."), isTogglable = false, toolTip = "Lets you buy the cat ears."  },
                new ButtonInfo { buttonText = "Diamond Balloon In Cart", method =() => Utility.BetaAddItemToCart("LMAAR."), isTogglable = false, toolTip = "Lets you buy the diamond balloon."  },

                new ButtonInfo { buttonText = "Canyon Pin In Cart", method =() => Utility.BetaAddItemToCart("LBAAG."), isTogglable = false, toolTip = "Lets you buy the canyons pin."  },
                new ButtonInfo { buttonText = "City Pin In Cart", method =() => Utility.BetaAddItemToCart("LBAAH."), isTogglable = false, toolTip = "Lets you buy the city pin."  },
                new ButtonInfo { buttonText = "Crystals Pin In Cart", method =() => Utility.BetaAddItemToCart("LBAAF."), isTogglable = false, toolTip = "Lets you buy the caves pin."  },
                new ButtonInfo { buttonText = "Gorilla Pin In Cart", method =() => Utility.BetaAddItemToCart("LBAAI."), isTogglable = false, toolTip = "Lets you buy the gorilla pin."  },
                new ButtonInfo { buttonText = "Mountain Pin In Cart", method =() => Utility.BetaAddItemToCart("LBABH."), isTogglable = false, toolTip = "Lets you buy the mountains pin."  },
                new ButtonInfo { buttonText = "Tree Pin In Cart", method =() => Utility.BetaAddItemToCart("LBAAA."), isTogglable = false, toolTip = "Lets you buy the tree pin."  },
            },

            new ButtonInfo[] { // Master | 13
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },

                new ButtonInfo { buttonText = "Set Master Gun", method =() => Overpowered.SetMasterGun(), isTogglable = true, toolTip = "Lets you set someone as master client."  },

                new ButtonInfo { buttonText = "Slow Gun", method =() => Overpowered.SlowGun(), isTogglable = true, toolTip = "Lets you slow someone with a gun."  },
                new ButtonInfo { buttonText = "Slow All", method =() => Overpowered.SlowAll(), isTogglable = true, toolTip = "Lets you slow everyone in the lobby."  },

                new ButtonInfo { buttonText = "Mat All V1", method =() => Overpowered.DoMatStuffIdk(), isTogglable = true, toolTip = "Changes everyones mat index."  },
                new ButtonInfo { buttonText = "Mat All V2", method =() => Experimental.ChangeMatIndexAll(), isTogglable = true, toolTip = "Changes everyones mat index."  },

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
            },

            new ButtonInfo[] { // Soundboard | 14
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
            },

            new ButtonInfo[] { // Players | 15
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
            },

             new ButtonInfo[] { // PlayersTab Fixer ? | 16
                new ButtonInfo { buttonText = "Return to Main", method =() => Settings.MovePage(0), isTogglable = false, toolTip = "Returns to the main page of the menu." },
            },
        };
    }
}

// Mod Graveyard - mods here are removed for one reason or another
/*
new ButtonInfo { buttonText = "Dick Spawn [<color=cyan>RT</color>]", method =() => Experimental.DickSpawn(), isTogglable = true },
new ButtonInfo { buttonText = "MeowMeow Cube Spawn [<color=cyan>RT</color>]", method = () => Experimental.MeowMeowCubeSpawn(), isTogglable = true },

new ButtonInfo { buttonText = "Eternal Sugar Cookie Spam [<color=cyan>RT</color>]", method =() => Experimental.eternalsugercookieSpammer(), isTogglable = true },

new ButtonInfo { buttonText = "Get F'd Spawn", method = () => Experimental.GetFuckedNetPlayers(), isTogglable = false },

new ButtonInfo { buttonText = "Ban Gun [JX Modding Game]", method =() => Overpowered.BanGunJXModding(), isTogglable = true },

new ButtonInfo { buttonText = "Dynamic Animations", enableMethod =() => dynamicAnimations = true, disableMethod =() => dynamicAnimations = false },

new ButtonInfo { buttonText = "Timmy Spam [<color=cyan>RT</color>]", method =() => GTH.TimmySpam(), isTogglable = true },
new ButtonInfo { buttonText = "Stalker Spam [<color=cyan>RT</color>]", method =() => GTH.StalkerSpam(), isTogglable = true },
new ButtonInfo { buttonText = "Timmy All [<color=cyan>RT</color>]", method =() => GTH.TimmyAll(), isTogglable = true },
new ButtonInfo { buttonText = "Stalker All [<color=cyan>RT</color>]", method =() => GTH.StalkerAll(), isTogglable = true },
new ButtonInfo { buttonText = "Timmy Gun", method =() => GTH.TimmyGun(), isTogglable = true },
new ButtonInfo { buttonText = "Stalker Gun", method =() => GTH.StalkerGun(), isTogglable = true },
new ButtonInfo { buttonText = "Kill Gun", method =() => GTH.KillGun(), isTogglable = true },
new ButtonInfo { buttonText = "Kill All [<color=cyan>RT</color>]", method =() => GTH.KillAll(), isTogglable = true },
new ButtonInfo { buttonText = "Timmy Gun (Forest)", method =() => GTH.MoveTimmy(), isTogglable = true },
new ButtonInfo { buttonText = "Spaz Timmy (Forest) [<color=cyan>RT</color>]", method =() => GTH.SpazTimmy(), isTogglable = true },
new ButtonInfo { buttonText = "Fling Timmy (Forest) [<color=cyan>RT</color>]", method =() => GTH.FlingTimmy(), isTogglable = true },
new ButtonInfo { buttonText = "Move Timmy To All [<color=cyan>RT</color>]", method =() => GTH.TimmyAllRigs(), isTogglable = true },

new ButtonInfo { buttonText = "Teleport To Slingshot", method =() => Utility.BetaTPToSling(), isTogglable = true },
*/