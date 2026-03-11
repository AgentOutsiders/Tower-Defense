using Godot;
using System;

public partial class PlacementSystem : Node2D
{
	public TileMapLayer PlacementGrid; 

    public PackedScene TowerScene;

    
	public override void _Ready()
	{
		PlacementGrid = GetNode<TileMapLayer>("PlacementGrid");
		TowerScene = GD.Load<PackedScene>("res://Entities/Towers/Tower.tscn");
	}

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            Vector2 mousePosition = GetGlobalMousePosition();

            mousePosition = PlacementGrid.ToLocal(mousePosition); // Transform global mouse position to local coordinates of the PlacementGrid

            Vector2I gridPosition = PlacementGrid.LocalToMap(mousePosition); // Transform local mouse position to grid coordinates (Vector2I for integer grid positions)

            Vector2I atlasCoords = PlacementGrid.GetCellAtlasCoords(gridPosition);
            
            if (atlasCoords != new Vector2I(-1, -1))
            {
                PlaceTower(gridPosition);
            }
            else
            {
                GD.Print("Invalid placement: No tile at grid position " + gridPosition);
            }
        }
    }

    private void PlaceTower(Vector2I gridPosition)
    {
        Node2D tower = TowerScene.Instantiate<Node2D>();
        

        Vector2 localPos = PlacementGrid.MapToLocal(gridPosition);
        tower.GlobalPosition = PlacementGrid.ToGlobal(localPos);

        AddChild(tower);

        PlacementGrid.SetCell(gridPosition, -1); // remove the tile from the placement grid
        
        GD.Print("Tower placed successfully at grid coordinates: " + gridPosition);
    }
}