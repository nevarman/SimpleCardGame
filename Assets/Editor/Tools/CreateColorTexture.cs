using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
public class CreateColorTexture : EditorWindow {

	const string texturePath = "Textures/SimpleColor";
	private Color color = Color.white;
	private int size = 16;
	private string _name;

	[MenuItem ("Tools/Texture/Create Simple Texture")]
	static void Init ()
	{
		CreateColorTexture window = (CreateColorTexture)EditorWindow.GetWindow (typeof (CreateColorTexture));
		window.Show();
	}

	void OnGUI () 
	{
		color = EditorGUILayout.ColorField("Color",color);
		size = EditorGUILayout.IntField("Size",size);
		_name = EditorGUILayout.TextField("File Name",_name);
		if(GUILayout.Button("Create Texture"))
		{
			CreateTexture();
		}
	}

	void CreateTexture ()
	{
		string dataPath = Path.Combine(Application.dataPath,texturePath);
		if(!Directory.Exists(dataPath))
			Directory.CreateDirectory(dataPath);

		Texture2D texture = new Texture2D(size,size);
		for(int i = 0; i< texture.width; i++)
		{
			for(int j = 0; j< texture.height; j++)
			{
				texture.SetPixel(i,j,color);
			}
		}
		texture.Apply();
		byte[] bytes = texture.EncodeToJPG();
		string name =  string.IsNullOrEmpty(_name) ? string.Format("{0}{1}{2}.jpg",color.r,color.g,color.b) : _name;
		if(!name.Contains(".jpg")) name += ".jpg";
		File.WriteAllBytes(Path.Combine(dataPath,name),bytes);
		AssetDatabase.Refresh();
	}
}
