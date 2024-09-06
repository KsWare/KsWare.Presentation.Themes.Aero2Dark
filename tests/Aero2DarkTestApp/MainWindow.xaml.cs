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
using KsWare.Presentation.Resources.Core;

// ReSharper disable LocalizableElement

namespace Aeor2DarkTestApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {

	public MainWindow() {

		//TODO BUG WORKAROUND
//		foreach (var type in typeof(Button).Assembly.GetTypes()) TryFindResource(type); //preload all styles
		
		InitializeComponent();
		this.MouseMove += MainWindow_MouseMove;

//		var d = new ThemeResourceDictionary {
//			Source = new Uri("/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml", UriKind.Relative),
//			EnableTrace = true
//		};
//		Resources.MergedDictionaries.Insert(0,d);
	}

	private void MainWindow_MouseMove(object sender, MouseEventArgs e) {
		if (!Keyboard.IsKeyDown(Key.LeftCtrl)) return;

		var element = (object)Mouse.DirectlyOver; // IInputElement
		if (element is not FrameworkElement) {
			var position = Mouse.GetPosition(this);
			var hitTestResult = VisualTreeHelper.HitTest(this, position);
			if (hitTestResult == null) return;
			element = hitTestResult.VisualHit; // DependencyObject
		}
		if(element==null) return;
		if (element is not FrameworkElement felement) {
			OutputTextBlock.Text = element.GetType().Name;
			return;
		}

		
//		if(VisualTreeHelper.GetParent(element1) is not FrameworkElement element) return;

		var template = GetProperty<ControlTemplate>(felement, Control.TemplateProperty, out string templateSource);
		template = template == null ? "{null}" : "{ControlTemplate}";

		// Get the background color of the element
		var background  = GetProperty<Brush>(felement, Control.BackgroundProperty, out string backgroundSource);
		var borderBrush = GetProperty<Brush>(felement, Border.BorderBrushProperty, out string borderBrushSource);
		var foreground  = GetProperty<Brush>(felement, Control.ForegroundProperty, out string foregroundSource);
		
		OutputTextBlock.Text = $"Element: {felement}\n" +
		                       $"Template: {template} (Source: {templateSource})\n" +
		                       $"Background: {background} (Source: {backgroundSource})\n" +
		                       $"BorderBrush: {borderBrush} (Source: {borderBrushSource})\n" +
		                       $"Foreground: {foreground} (Source: {foregroundSource})";
	}

	private object? GetProperty<T>(FrameworkElement element, DependencyProperty property, out string source) {
		// Get the local value of the property
		if (element.ReadLocalValue(property) is T value && (object)value != DependencyProperty.UnsetValue) {
			source = "Local value";
			return value;
		}

		// Check if the value is defined in the resources of the element
		var foundItem = FindResourceInElement<T>(element, property, out source);
		if (foundItem.HasValue) return foundItem.Value;

		// Check if the brush is defined in the resources of the application
		var dictionaryStack = new List<ResourceDictionary>();
		foundItem = FindResourceInApplication<T>(property, dictionaryStack, out source);
		if (foundItem.HasValue) return foundItem.Value;

		source = "Not found";
		return null;
	}

	private ResourceItem<T> FindResourceInElement<T>(FrameworkElement element, DependencyProperty property, out string source) {
		source = null;

		// Check the element resources
		foreach (var resourceKey in element.Resources.Keys) {
			if (element.Resources[resourceKey] is T resBrush && Equals(element.GetValue(property), resBrush)) {
				source = $"Element resource ({resourceKey})";
				return new ResourceItem<T>(resBrush);
			}
		}

		// Check the parent elements' resources
		var parent = element.Parent as FrameworkElement;
		while (parent != null) {
			foreach (var resourceKey in parent.Resources.Keys) {
				if (parent.Resources[resourceKey] is not T resBrush ||
				    !Equals(element.GetValue(property), resBrush)) continue;
				source = $"Parent resource ({resourceKey})";
				return new ResourceItem<T>(resBrush);
			}
			parent = parent.Parent as FrameworkElement;
		}

		return ResourceItem<T>.Null;
	}

	private ResourceItem<T> FindResourceInApplication<T>(DependencyProperty property, List<ResourceDictionary> stack, out string source) {
		return FindResourceInDictionary<T>(Application.Current.Resources, property, stack, out source);
	}

	private ResourceItem<T> FindResourceInDictionary<T>(ResourceDictionary dictionary, DependencyProperty property, List<ResourceDictionary> stack, out string source) {
		source = null;
		var foundItem = ResourceItem<T>.Null;
		stack.Add(dictionary);
		foreach (var resourceKey in dictionary.Keys) {
			var value = (object?)null;
			try { value = dictionary[resourceKey]; } catch { }
			if (value is not T resValue) continue;
			if (Application.Current.TryFindResource(resourceKey) is T foundResource && Equals(resValue, foundResource)) {
				foundItem = new ResourceItem<T>(resValue);
				source = $"Application resource ({resourceKey})";
				return foundItem;
			}
		}
		stack.Add(dictionary);
		foreach (var mergedDictionary in dictionary.MergedDictionaries) {
			var item = FindResourceInDictionary<T>(mergedDictionary, property, stack, out string mergedSource);
			if (item.HasValue) {
				foundItem = item;
				source = mergedSource;
			}
		}

		if (!foundItem.HasValue) stack.RemoveAt(stack.Count - 1);
		return foundItem;
	}

	public List<Item> MyListViewItems { get; } = new List<Item> {
		new Item {Name = "Item A", Value = "A"},
		new Item {Name = "Item B", Value = "B"},
		new Item {Name = "Item C", Value = "C"},
	};

	public List<Item> MyTreeViewItems { get; } = new List<Item> {
		new Item {Name = "Item A", Value = "A"},
		new Item {Name = "Item B", Value = "B" , Children = {
			new Item{Name = "Item B2", Value = "B2"}
		}},
		
	};

	public struct ResourceItem<T> {

		public ResourceItem(T value) {
			Value = value;
			HasValue = true;
			IsUnsetValue = false;
		}

		public T Value { get; }
		public bool HasValue { get; }
		public bool IsUnsetValue { get; private set; }

		public static ResourceItem<T> Null = new();
		public static ResourceItem<T> UnsetValue = new() {IsUnsetValue=true};
	}

}

public class Item {

	public string Name { get; set; }
	public string Value { get; set; }
	public List<Item> Children { get; set; } = new List<Item>();

}

public class DataGridData {

	// Statisches Feld, das die Daten enthält
	public static List<DataGridData> Sample { get; } = new List<DataGridData> {
		new DataGridData {
			TextValue = "Text 1",
			IsChecked = true,
			SelectedOption = "Option1",
			Options = new List<string> { "Option1", "Option2", "Option3" },
			Link = "https://example.com",
			LinkText = "Example",
			CustomText = "Custom 1"
		},
		new DataGridData {
			TextValue = "Text 2",
			IsChecked = false,
			SelectedOption = "Option2",
			Options = new List<string> { "Option1", "Option2", "Option3" },
			Link = "https://example.org",
			LinkText = "Org",
			CustomText = "Custom 2"
		}
	};

	public string TextValue { get; set; }
	public bool IsChecked { get; set; }
	public string SelectedOption { get; set; }
	public List<string> Options { get; set; }
	public string Link { get; set; }
	public string LinkText { get; set; }
	public string CustomText { get; set; }
}