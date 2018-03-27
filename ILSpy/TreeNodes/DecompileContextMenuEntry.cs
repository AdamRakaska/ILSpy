using System.Linq;
using System.Windows;
using Mono.Cecil;

namespace ICSharpCode.ILSpy.TreeNodes
{
	[ExportContextMenuEntry(Header = "Decompile To File...", Icon = "images/SaveFile.png", Category ="Save", Order = 50)]
	public class DecompileContextMenuEntry : IContextMenuEntry
	{
		public bool IsVisible(TextViewContext context)
		{
			return (context.SelectedTreeNodes != null && context.SelectedTreeNodes.Length > 0);
		}

		public bool IsEnabled(TextViewContext context) => true;

		public void Execute(TextViewContext context)
		{
			if (context.SelectedTreeNodes != null && context.SelectedTreeNodes.Length > 0) {

				MainWindow.Instance.TextView.SaveToDisk(
					Languages.GetLanguage("C#"),
					context.SelectedTreeNodes.Select(tn => tn as ILSpyTreeNode),
					new DecompilationOptions() { FullDecompilation = true }
					//new DecompilationOptions(new LanguageVersion("C# 6.0 / VS 2015")/*, new Options.DecompilerSettings() {   } */) { FullDecompilation = true }
				);
			}
		}

		private IMemberTreeNode[] GetMemberNodeFromContext(TextViewContext context)
		{
			return context.SelectedTreeNodes?.Length > 0 ? context.SelectedTreeNodes as IMemberTreeNode[] : null;
		}		
	}
}