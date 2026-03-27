using Godot;
using System;

public partial class Camera2d : Camera2D
{
    private bool _isDragging = false;

    [Export] 
    public float MinZoom = 0.5f;
    
    [Export] 
    public float MaxZoom = 2.0f;
    
    [Export] 
    public float ZoomStep = 0.1f;

    [Export]
    public TileMapLayer MapLayer;

    public override void _Ready()
    {
        if (MapLayer != null)
        {
            SetCameraLimits();
            ClampCameraPosition();
        }
    }

    private void SetCameraLimits()
    {
        // Get the used rectangle of the tilemap and calculate the limits in pixels
        Rect2I mapRect = MapLayer.GetUsedRect();
        Vector2I tileSize = MapLayer.TileSet.TileSize;

        LimitLeft = mapRect.Position.X * tileSize.X;
        LimitTop = mapRect.Position.Y * tileSize.Y;
        LimitRight = mapRect.End.X * tileSize.X;
        LimitBottom = mapRect.End.Y * tileSize.Y;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseButton)
        {
            if (mouseButton.ButtonIndex == MouseButton.Right)
            {
                _isDragging = mouseButton.IsPressed();
            }

            if (mouseButton.IsPressed())
            {
                if (mouseButton.ButtonIndex == MouseButton.WheelUp)
                {
                    ApplyZoom(ZoomStep);
                }
                else if (mouseButton.ButtonIndex == MouseButton.WheelDown)
                {
                    ApplyZoom(-ZoomStep);
                }
            }
        }

        if (@event is InputEventMouseMotion mouseMotion && _isDragging)
        {
            Position -= mouseMotion.Relative / Zoom;
            ClampCameraPosition(); // On bloque la Position pour l'empêcher de "s'enfoncer" dans la bordure !
        }
    }

    private void ApplyZoom(float amount)
    {
        Vector2 newZoom = Zoom + new Vector2(amount, amount);
        // Use clamp to ensure the new zoom level stays within the defined limits
        newZoom.X = Mathf.Clamp(newZoom.X, MinZoom, MaxZoom);
        newZoom.Y = Mathf.Clamp(newZoom.Y, MinZoom, MaxZoom);
        Zoom = newZoom;
        
        ClampCameraPosition(); // Need to verify the camera position after zooming, as the visible area changes and might cause the camera to go out of bounds
    }

    private void ClampCameraPosition()
    {
        if (MapLayer == null)
        {
            return;
        }

        // Calculate the size of the visible area
        Vector2 visibleSize = GetViewportRect().Size / Zoom;
        // Calculate half of the visible size because the node is centered, so on each side we have half of the visible area
        Vector2 halfSize = visibleSize / 2.0f;

        // Calculate the limits for the camera position
        float minX = LimitLeft + halfSize.X;
        float maxX = LimitRight - halfSize.X;
        float minY = LimitTop + halfSize.Y;
        float maxY = LimitBottom - halfSize.Y;

        // If the map is smaller than the visible area, we need to center the camera on the map and ignore the limits
        if (maxX < minX)
        {
            minX = maxX = (LimitLeft + LimitRight) / 2.0f;
        }

        if (maxY < minY)
        {
            minY = maxY = (LimitTop + LimitBottom) / 2.0f;
        } 

        // Clamp the camera position to ensure it stays within the calculated limits
        Vector2 clampedPosition = Position;
        clampedPosition.X = Mathf.Clamp(Position.X, minX, maxX);
        clampedPosition.Y = Mathf.Clamp(Position.Y, minY, maxY);
        
        Position = clampedPosition;
    }
}