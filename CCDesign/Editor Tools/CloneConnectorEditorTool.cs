using System.Windows.Forms;

namespace CCTools.CCDesign
{
	public class CloneConnectorEditorTool : EditorTool
	{
		protected override void OnLeftMouseButtonDown(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			editor.SetCloneConnection(location);
		}

		protected override void OnLeftMouseButtonMove(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			// Do nothing
		}
	}
}
