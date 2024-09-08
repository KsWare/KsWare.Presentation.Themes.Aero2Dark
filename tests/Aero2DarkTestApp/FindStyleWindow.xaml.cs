using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KsWare.Presentation.Resources.Core;

namespace Aero2DarkTestApp;
/// <summary>
/// Interaction logic for FindStyleWindow.xaml
/// </summary>
public partial class FindStyleWindow : Window {

	public FindStyleWindow() {
		InitializeComponent();

		//Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml") });
		object style;
		Debug.WriteLine($"FindResource Aero2Dark.ToolBarCheckBoxStyle");
		Debug.WriteLine($"{TryFindResource("Aero2Dark.ToolBarCheckBoxStyle")?.GetObjectId().ToString()??"NULL"}");
		Debug.WriteLine($"FindResource Aero2Dark.CheckBoxStyle");
		Debug.WriteLine($"{TryFindResource("Aero2Dark.CheckBoxStyle")?.GetObjectId().ToString()??"NULL"}");


		var md = (ThemeResourceDictionary)Resources.MergedDictionaries[1];
		Debug.WriteLine($"FindResource direct CheckBox");
		Debug.WriteLine($"{(style=md.TryFindResource(typeof(CheckBox)))?.GetObjectId().ToString()??"NULL"}");

		Debug.WriteLine($"FindResource CheckBox");
		Debug.WriteLine($"{(style=TryFindResource(typeof(CheckBox)))?.GetObjectId().ToString()??"NULL"}");
		Debug.WriteLine($"FindResource CheckBox");
		Debug.WriteLine($"{(style=TryFindResource(typeof(CheckBox)))?.GetObjectId().ToString()??"NULL"}");
	}
}
