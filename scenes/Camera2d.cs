using Godot;

public partial class CameraController : Camera2D
{
	// Marges pour les bords de l'écran avant que la caméra ne commence à suivre le joueur
	[Export] public float MarginTop = 100;
	[Export] public float MarginBottom = 100;
	[Export] public float MarginLeft = 150;
	[Export] public float MarginRight = 150;

	private Vector2 screenSize;

	public override void _Ready()
	{
		// Récupère la taille de l'écran
		screenSize = new Vector2(
			(float)ProjectSettings.GetSetting("display/window/size/width"),
			(float)ProjectSettings.GetSetting("display/window/size/height")
		);
	}

	public void _Process(float delta)
	{
		// Récupère la position du joueur
		var player = GetParent().GetNode<Node2D>("Player");
		Vector2 playerPosition = player.GlobalPosition;

		// Position actuelle de la caméra
		Vector2 cameraPosition = GlobalPosition;

		// Calculer les limites de la zone morte
		float leftBound = cameraPosition.X - (screenSize.X / 2) + MarginLeft;
		float rightBound = cameraPosition.X + (screenSize.X / 2) - MarginRight;
		float topBound = cameraPosition.Y - (screenSize.Y / 2) + MarginTop;
		float bottomBound = cameraPosition.Y + (screenSize.Y / 2) - MarginBottom;

		// Si le joueur dépasse la limite à droite, on déplace la caméra à droite
		if (playerPosition.X > rightBound)
		{
			cameraPosition.X = playerPosition.X - (screenSize.X / 2) + MarginRight;
		}
		// Si le joueur dépasse la limite à gauche, on déplace la caméra à gauche
		else if (playerPosition.X < leftBound)
		{
			cameraPosition.X = playerPosition.X - (screenSize.X / 2) + MarginLeft;
		}

		// Si le joueur dépasse la limite en bas, on déplace la caméra vers le bas
		if (playerPosition.Y > bottomBound)
		{
			cameraPosition.Y = playerPosition.Y - (screenSize.Y / 2) + MarginBottom;
		}
		// Si le joueur dépasse la limite en haut, on déplace la caméra vers le haut
		else if (playerPosition.Y < topBound)
		{
			cameraPosition.Y = playerPosition.Y - (screenSize.Y / 2) + MarginTop;
		}

		// Appliquer la nouvelle position de la caméra
		GlobalPosition = cameraPosition;
	}
}
