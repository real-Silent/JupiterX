using TMPro;
using UnityEngine;

namespace JupiterX.Managers
{
    public class KeyboardManager
    {
        public static GameObject CreateKeyboard()
        {
            GameObject root = new GameObject("VRKeyboard");

            root.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

            GameObject basePlate = GameObject.CreatePrimitive(PrimitiveType.Cube);
            basePlate.name = "Background";
            basePlate.transform.SetParent(root.transform);

            basePlate.transform.localScale = new Vector3(0.5f, 0.04f, 0.25f);
            basePlate.transform.localPosition = Vector3.zero;
            basePlate.GetComponent<Renderer>().material.color = new Color(0.6f, 0.3f, 0f);

            GameObject menuSpawn = new GameObject("MenuSpawnPosition");
            menuSpawn.transform.SetParent(root.transform);

            menuSpawn.transform.localPosition = new Vector3(0f, 0.12f, 0f); // ABOVE
            menuSpawn.transform.localRotation = Quaternion.identity;
            menuSpawn.transform.localScale = Vector3.one;

            float keySize = 0.035f;
            float spacing = 0.05f;
            float startX = -0.225f;
            float startZ = 0.09f;

            string[] rows =
            {
                "QWERTYUIOP",
                "ASDFGHJKL",
                "ZXCVBNM"
            };

            for (int row = 0; row < rows.Length; row++)
            {
                float rowOffset =
                    row == 1 ? spacing * 0.5f :
                    row == 2 ? spacing * 1.2f : 0f;

                for (int col = 0; col < rows[row].Length; col++)
                {
                    char letter = rows[row][col];

                    GameObject key = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    key.name = letter.ToString();
                    key.transform.SetParent(root.transform);

                    key.transform.localScale = new Vector3(keySize, keySize * 0.6f, keySize);

                    key.transform.localPosition = new Vector3(
                        startX + (col * spacing) + rowOffset,
                        0.025f,
                        startZ - (row * spacing)
                    );

                    key.GetComponent<Renderer>().material.color = new Color(0.85f, 0.45f, 0.1f);

                    var colComp = key.GetComponent<BoxCollider>();
                    colComp.isTrigger = true;

                    GameObject textObj = new GameObject("Text");
                    textObj.transform.SetParent(key.transform);

                    textObj.transform.localPosition = new Vector3(0, 0.018f, 0);
                    textObj.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    textObj.transform.localScale = Vector3.one * 0.01f;

                    TextMeshPro tmp = textObj.AddComponent<TextMeshPro>();
                    tmp.text = letter.ToString();
                    tmp.fontSize = 8;
                    tmp.alignment = TextAlignmentOptions.Center;
                    tmp.color = Color.white;

                    tmp.rectTransform.sizeDelta = new Vector2(1, 1);

                    key.AddComponent<Classes.KeyboardKey>().key = letter.ToString();
                    key.layer = 2;
                }
            }

            return root;
        }
    }
}