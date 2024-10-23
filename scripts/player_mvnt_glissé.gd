extends CharacterBody2D

@export var player_id: int = 1  # ID du joueur
const SPEED = 300.0
const JUMP_VELOCITY = -450.0
const FRICTION = 1200.0  # Valeur pour la friction

# Variables pour le double saut
var jump_count = 0
const MAX_JUMPS = 2

# Variable pour gérer la gravité manuellement si nécessaire
const GRAVITY = 1000.0

func _enter_tree() -> void:
	set_multiplayer_authority(name.to_int())

func _physics_process(delta: float) -> void:
	if is_multiplayer_authority():
		var velocity = self.velocity

	# Appliquer la gravité si le personnage n'est pas au sol
	if not is_on_floor():
		velocity.y += GRAVITY * delta

	# Réinitialiser le compteur de sauts lorsque le personnage touche le sol
	if is_on_floor():
		jump_count = 0

	# Gérer le saut ou le double saut
	if Input.is_action_just_pressed("ui_accept") and jump_count < MAX_JUMPS:
		velocity.y = JUMP_VELOCITY
		jump_count += 1

	# Gérer les déplacements en fonction de l'entrée directionnelle
	var direction = Input.get_vector("ui_left", "ui_right", "ui_up", "ui_down")
	if direction != Vector2.ZERO:
		velocity.x = direction.x * SPEED
	else:
		# Appliquer la friction si aucune touche n'est pressée
		velocity.x = move_toward(velocity.x, 0, FRICTION * delta)

	# Appliquer la vélocité et gérer les collisions avec move_and_slide
	self.velocity = velocity
	move_and_slide()
