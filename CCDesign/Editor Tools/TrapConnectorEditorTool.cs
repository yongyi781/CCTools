using System.Windows.Forms;

namespace CCTools.CCDesign
{
	public class TrapConnectorEditorTool : EditorTool
	{
		protected override void OnLeftMouseButtonDown(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			editor.SetTrapConnection(location);
		}

		protected override void OnLeftMouseButtonMove(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			// Do nothing
		}
	}
}
