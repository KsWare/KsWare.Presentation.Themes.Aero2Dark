using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IsolatedTestApp;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
	public MainWindow() {
		InitializeComponent();
	}

	private void OnResourceDictionaryEndInit(object? sender, EventArgs e) {
//		var rA = Application.Current.TryFindResource("Aero2Dark.Button.Static.Background");
//		var rW = this.TryFindResource("Aero2Dark.Button.Static.Background");

//		var rW = (Style)this.TryFindResource("Aero2Dark.RepeatButtonStyle");
//		ValidateStyle(rW);
	}

	private void ValidateStyle(Style style) {
		if (style == null) {
			Debug.WriteLine("Style not found or is null.");
			return;
		}

		PresentationTraceSources.DataBindingSource.Switch.Level = SourceLevels.All;
		PresentationTraceSources.DataBindingSource.Listeners.Add(new ConsoleTraceListener());

		var testElement = new RepeatButton() {
			Style = style
		};
		testElement.ApplyTemplate();

	}

}