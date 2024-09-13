using System.Windows;
using KsWare.Presentation.Resources.Core;
using ShowMeTheXAML;

namespace Aero2DarkTestApp {

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application {

		protected override void OnStartup(StartupEventArgs e) {
			XamlDisplay.Init();
			ThemeManager.RegisterTheme("Aero2Dark","/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml");
			base.OnStartup(e);
		}

	}

}

