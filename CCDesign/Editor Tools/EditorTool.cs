using System.Windows.Forms;

namespace CCTools.CCDesign
{
	public abstract class EditorTool
	{
		public static readonly EditorTool Default = new DefaultEditorTool();
		public static readonly EditorTool Select = new SelectEditorTool();
		public static readonly EditorTool PathMaker = new PathMakerEditorTool();
		public static readonly EditorTool TrapConnector = new TrapConnectorEditorTool();
		public static readonly EditorTool CloneConnector = new CloneConnectorEditorTool();

		public void OnMouseDown(LevelMapEditor editor, TileLocation location, MouseButtons mouseButtons, Keys modifierKeys)
		{
			switch (mouseButtons)
			{
				case MouseButtons.Left:
					OnLeftMouseButtonDown(editor, location, modifierKeys);
					break;
				case MouseButtons.Right:
					OnRightMouseButtonDown(editor, location, modifierKeys);
					break;
				default:
					break;
			}
		}

		public void OnMouseMove(LevelMapEditor editor, TileLocation location, MouseButtons mouseButtons, Keys modifierKeys)
		{
			switch (mouseButtons)
			{
				case MouseButtons.Left:
					OnLeftMouseButtonMove(editor, location, modifierKeys);
					break;
				case MouseButtons.Right:
					OnRightMouseButtonMove(editor, location, modifierKeys);
					break;
				default:
					break;
			}
		}

		protected virtual void OnLeftMouseButtonDown(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			Default.OnLeftMouseButtonDown(editor, location, modifierKeys);
		}

		protected virtual void OnRightMouseButtonDown(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			Default.OnRightMouseButtonDown(editor, location, modifierKeys);
		}

		protected virtual void OnLeftMouseButtonMove(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			Default.OnLeftMouseButtonMove(editor, location, modifierKeys);
		}

		protected virtual void OnRightMouseButtonMove(LevelMapEditor editor, TileLocation location, Keys modifierKeys)
		{
			Default.OnRightMouseButtonMove(editor, location, modifierKeys);
		}
	}
}
