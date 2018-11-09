using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(DamageSection))]
public class IngredientDrawer : PropertyDrawer {
	// Draw the property inside the given rect
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
		

		// Using BeginProperty / EndProperty on the parent property means that
		// prefab override logic works on the entire property.
		EditorGUI.BeginProperty(position, label, property);
		label.text = "";
		// Draw label

		position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

		// Don't make child fields be indented
		var indent = EditorGUI.indentLevel;
		EditorGUI.indentLevel = 0;

		// Calculate rects
		Rect nameRect = new Rect(position.x, position.y, 60, 15);

		Rect prefixMaxValue = new Rect(position.x + 90, position.y, 40, 15);
		Rect maxValue = new Rect(position.x + 125, position.y, 50, 15);

		Rect prefixValue = new Rect(position.x + 190, position.y, 70, 15);
		Rect value = new Rect(position.x + 245, position.y, 50, 15);
		
		// Draw fields - passs GUIContent.none to each so they are drawn without labels
		EditorGUI.PropertyField(value, property.FindPropertyRelative("Value"), GUIContent.none);
		EditorGUI.PropertyField(maxValue, property.FindPropertyRelative("MaxValue"), GUIContent.none);
		EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("Type"), GUIContent.none);

		EditorGUI.LabelField(prefixMaxValue, new GUIContent("Max"));
		EditorGUI.LabelField(prefixValue, new GUIContent("Current"));

		if (property.FindPropertyRelative("Type").enumDisplayNames[property.FindPropertyRelative("Type").enumValueIndex] == "Armor") {
			Rect slider = new Rect(position.x + 90, position.y + 20, 315, 15);
			EditorGUI.Slider(slider, property.FindPropertyRelative("DamageReduction"), 0, 1);
		}
		// Set indent back to what it was
		EditorGUI.indentLevel = indent;
		EditorGUI.EndProperty();
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {

		if (property.FindPropertyRelative("Type").enumDisplayNames[property.FindPropertyRelative("Type").enumValueIndex] == "Armor") {

			return base.GetPropertyHeight(property, label) + 27;
		}
		return base.GetPropertyHeight(property, label);


	}
}