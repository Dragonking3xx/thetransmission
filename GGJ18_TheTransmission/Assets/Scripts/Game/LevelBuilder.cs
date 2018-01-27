using UnityEngine;
using System.Collections;

// NOTE(chris): Copied in from GGJ15 project
public class LevelBuilder : MonoBehaviour
{
	// NOTE: replace these with the objects specific to this game
	public GameObject Wall;
	public GameObject BoxDestructible;
	public GameObject BoxMovable;
	public GameObject Grass;
	public GameObject Tree;
	public GameObject Mushroom;

	public GameObject Cat;
	public GameObject CatFood;
	public GameObject CatBowl;

	public GameObject DoorOpen;
	public GameObject DoorClosed;
	public GameObject Stairs;

	public Vector2 topLeft;
	public TextAsset levelAsset;

	// Use this for initialization
	void Start()
	{
		CreateLevelFromTextFile();
	}

	public void CreateLevelFromTextFile(TextAsset levelAsset=null)
	{
		if (levelAsset != null) this.levelAsset = levelAsset;

		// TODO read from asset
		string[] data = levelAsset.ToString().Split("\n"[0]);

		GameObject block;

		for (int y = 0; y < data.Length; y++)
		{
			string line = data[y];
			for (int x = 0; x < line.Length; x++)
			{
				block = null;

				switch (line[x])
				{
					case 'w':
						block = (GameObject)(Instantiate(Wall));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						block.tag = "ground";
						break;
					case 'b':
						block = (GameObject)(Instantiate(BoxDestructible));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						break;
					case 'm':
						block = (GameObject)(Instantiate(BoxMovable));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						break;
					case 'g':
						block = (GameObject)(Instantiate(Grass));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						block.tag = "ground";
						break;

					case 'M':
						block = (GameObject)(Instantiate(Mushroom));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						break;
					case 't':
						block = (GameObject)(Instantiate(Tree));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						break;

					case 'f':
						block = (GameObject)(Instantiate(CatFood));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						break;
					case 'F':
						block = (GameObject)(Instantiate(CatBowl));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						break;
					case 'c':
						block = (GameObject)(Instantiate(Cat));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						break;

					case 'd':
						block = (GameObject)(Instantiate(DoorOpen));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						break;
					case 'D':
						block = (GameObject)(Instantiate(DoorClosed));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						break;
					case 's':
						block = (GameObject)(Instantiate(Stairs));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						block.tag = "ground";
						break;
					case 'S':
						block = (GameObject)(Instantiate(Stairs));
						block.transform.position = new Vector3(topLeft.x + x * 2, topLeft.y - y * 2, 0);
						block.transform.localScale = new Vector3(-1, 1, 1); // invert direction
						block.tag = "ground";
						break;
					// TODO more types?
					default:
						block = null;
						break;
				}
			}
		}
	}
}