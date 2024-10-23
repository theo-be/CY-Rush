using Godot;
using System;

public partial class Perso_mvnt_rectiligne : CharacterBody2D
{
	// Constantes pour la vitesse et la vélocité du saut
	public const float Speed = 300.0f;
	public const float JumpVelocity = -450f;

	// Variables pour le double saut
	private int jumpCount = 0;
	private const int MaxJumps = 2;

	// Multiplayer authority
	public override void _EnterTree()
	{
		// On définit l'autorité du joueur selon son nom ou ID.
		SetMultiplayerAuthority(int.Parse(Name.ToString()));
	}

	public override void _PhysicsProcess(double delta)
	{
		// On vérifie si le joueur a l'autorité sur ce personnage avant de gérer ses mouvements.
		if (!IsMultiplayerAuthority())
		{
			return;
		}

		// Récupérer la vélocité actuelle
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Réinitialiser le compteur de sauts si le joueur est au sol
		if (IsOnFloor())
		{
			jumpCount = 0;  // On est au sol, donc on réinitialise les sauts
		}

		// Handle Jump (saut ou double saut)
		if (Input.IsActionJustPressed("ui_accept") && jumpCount < MaxJumps)
		{
			velocity.Y = JumpVelocity;
			jumpCount++;  // Incrémente le nombre de sauts
		}

		// Get the input direction and handle the movement/deceleration.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
