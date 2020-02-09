using System.Windows.Forms;

namespace CCTools.CCDesign
{
	public class SelectEditorTool : EditorTool
	{
		protected override void OnLeftMouseButtonDown(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			editor.Selection = (modifierKeys & Keys.Shift) == Keys.Shift ? TileRectangle.FromLTRB(editor.Selection.Left, editor.Selection.Top, location.X, location.Y) : new TileRectangle(location, 1, 1);
		}

		protected override void OnLeftMouseButtonMove(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			editor.Selection = TileRectangle.FromLTRB(editor.Selection.Left, editor.Selection.Top, location.X, location.Y);
		}
	}
}
