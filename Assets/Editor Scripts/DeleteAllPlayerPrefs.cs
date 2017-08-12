using System.Collections;
using UnityEngine;
using UnityEditor;

public class DeleteAllPlayerPrefs : ScriptableObject {
	  
	[MenuItem ("Editor/Delete All Player Prefs")]
	static void DeletePrefs() {

		bool doDeletion = EditorUtility.DisplayDialog("Delete all Player Prefs?", 
			                                          "Are you sure you want to delete all Player Prefs? (This action cannot be undone.", 
			                                          "Yes", 
			                                          "No");

		if (doDeletion) {
			PlayerPrefs.DeleteAll();
		}
	}
}
