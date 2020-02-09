using System.Windows.Forms;

namespace CCTools.CCDesign
{
	public class DefaultEditorTool : EditorTool
	{
		protected override void OnLeftMouseButtonDown(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			if (editor.LayerMode == LayerMode.LowerLayer)
				modifierKeys |= Keys.Shift;
			switch (modifierKeys)
			{
				case Keys.Alt | Keys.Shift:
					editor.LeftTile = editor.Level.LowerLayer[location];
					break;
				case Keys.Alt:
					editor.LeftTile = editor.Level.UpperLayer[location];
					break;
				case Keys.Shift:
					editor.SetLowerLayerTile(location, editor.LeftTile);
					break;
				default:
					editor.SetUpperLayerTile(location, editor.LeftTile);
					break;
			}
			editor.TileLocationHistory.Clear();
			editor.TileLocationHistory.Add(location);
		}

		protected override void OnRightMouseButtonDown(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			if (editor.LayerMode == LayerMode.LowerLayer)
				modifierKeys |= Keys.Shift;
			switch (modifierKeys)
			{
				case Keys.Alt | Keys.Shift:
					editor.RightTile = editor.Level.LowerLayer[location];
					break;
				case Keys.Alt:
					editor.RightTile = editor.Level.UpperLayer[location];
					break;
				case Keys.Shift:
					editor.SetLowerLayerTile(location, editor.RightTile);
					break;
				default:
					editor.SetUpperLayerTile(location, editor.RightTile);
					break;
			}
			editor.TileLocationHistory.Clear();
			editor.TileLocationHistory.Add(location);
		}

		protected override void OnLeftMouseButtonMove(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			if (editor.LayerMode == LayerMode.LowerLayer)
				modifierKeys |= Keys.Shift;
			switch (modifierKeys)
			{
				case Keys.Alt | Keys.Shift:
					editor.LeftTile = editor.Level.LowerLayer[location];
					break;
				case Keys.Alt:
					editor.LeftTile = editor.Level.UpperLayer[location];
					break;
				case Keys.Shift:
					editor.SetLowerLayerTile(location, editor.LeftTile);
					break;
				default:
					editor.SetUpperLayerTile(location, editor.LeftTile);
					break;
			}
			editor.TileLocationHistory.Add(location);
		}

		protected override void OnRightMouseButtonMove(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			if (editor.LayerMode == LayerMode.LowerLayer)
				modifierKeys |= Keys.Shift;
			switch (modifierKeys)
			{
				case Keys.Alt | Keys.Shift:
					editor.RightTile = editor.Level.LowerLayer[location];
					break;
				case Keys.Alt:
					editor.RightTile = editor.Level.UpperLayer[location];
					break;
				case Keys.Shift:
					editor.SetLowerLayerTile(location, editor.RightTile);
					break;
				default:
					editor.SetUpperLayerTile(location, editor.RightTile);
					break;
			}
			editor.TileLocationHistory.Add(location);
		}
	}
}
