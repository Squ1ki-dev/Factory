using UnityEngine;

public class Menu : MonoBehaviour {
    public Texture logo;
    public Texture[] levelImages;
    public GUISkin guiSkin; // Used for the custom styling of the selection grid used for level selection
    private MenuStage menu = MenuStage.TitleScreen; // Menu mode
    private int levelSelectionGridIndex = -1; // Used for the selection grid

    private enum MenuStage {
        TitleScreen,
        LevelSelect
    }

    private void OnGUI() {
        switch(menu) {
            case MenuStage.TitleScreen:
                // Mode 0 - Main
#if !UNITY_2017_4_OR_NEWER
                if(Application.isWebPlayer) {
                    GUI.Box(new Rect(-5, Screen.height - 55, Screen.width + 10, 70), GUIContent.none);
                    GUI.DrawTexture(new Rect(Screen.width - 398, Screen.height - 150, 380, 80), logo, ScaleMode.ScaleToFit, true, 0); // Game logo

                    // Change the menu mode to 1 if the user clicks "Select Level"
                    if(GUI.Button(new Rect(Screen.width * 0.5f - 75f, Screen.height - 40f, 150f, 30f), "Play")) {
                        menu = MenuStage.LevelSelect;
                    }
                } else {
#endif
                    GUI.Box(new Rect(-5, Screen.height - 55, Screen.width + 10, 70), GUIContent.none);
                    GUI.DrawTexture(new Rect(Screen.width - 398, Screen.height - 150, 380, 80), logo, ScaleMode.ScaleToFit, true, 0); // Game logo

                    // Change the menu mode to 1 if the user clicks "Select Level"
                    if(GUI.Button(new Rect(Screen.width * 0.5f - 160f, Screen.height - 40f, 150f, 30f), "Select Level")) {
                        menu = MenuStage.LevelSelect;
                    }
                    if(GUI.Button(new Rect(Screen.width * 0.5f + 10f, Screen.height - 40f, 150f, 30f), "Quit")) {
                        Application.Quit();
                    }
#if !UNITY_2017_4_OR_NEWER
                }
#endif
                break;
            case MenuStage.LevelSelect:
                // Mode 1 - Selection
                var oldSelectionGridIndex = levelSelectionGridIndex;
                levelSelectionGridIndex = GUI.SelectionGrid(
                    position: new Rect(Screen.width * 0.5f - 384f, Screen.height * 0.5f - 135f, 768f, 204f), 
                    selected: levelSelectionGridIndex, 
                    images: levelImages, 
                    xCount: 5, 
                    style: GUI.skin.button);

                // If the user clicked a button. The id of "gridInt" would be different to the "oldGridInt" of -1.
                if(levelSelectionGridIndex != oldSelectionGridIndex) {
                    Application.LoadLevel(levelSelectionGridIndex + 1);
                }

                // Back button
                if(GUI.Button(new Rect(Screen.width * 0.5f + 140f, Screen.height * 0.5f + 130f, 140f, 30f), "Back")) {
                    menu = MenuStage.TitleScreen;
                }
                break;
        }
    }
}