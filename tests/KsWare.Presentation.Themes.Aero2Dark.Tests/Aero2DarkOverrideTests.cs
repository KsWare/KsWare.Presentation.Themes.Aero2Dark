using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using KsWare.Presentation.Resources.Core;
using SystemColors = System.Windows.SystemColors;
using static KsWare.Presentation.Core.Utils.ResourceHelper;

namespace KsWare.Presentation.Themes.Aero2Dark.Tests;

[TestFixture]
[SuppressMessage("ReSharper", "LocalizableElement")]
// ReSharper disable once TestFileNameWarning
public class Aero2DarkOverrideTests {

	[SetUp]
	public void Setup() {

	}

	[Test]
	public void ThemeColors_Brush_Mapping() {
		var d = new ResourceDictionary{Source = new Uri("pack://application:,,,/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml")};
		var e = new FrameworkElement {Resources = d};

		foreach (var rootKey in GetSystemColorsKeyNames()) {
			var c0 = e.TryFindResource($"Aero2Dark.ThemeColors.{rootKey}Color");
			var b1 = (SolidColorBrush)e.TryFindResource(GetSystemColorResourceKey($"{rootKey}BrushKey"));
			Assert.That(b1,Is.Not.Null);
			var c1 = b1.Color;
			Assert.That(c1,Is.EqualTo(c0));
			
		}
	}

	public static Type[] SupportedTypes = new Type[] {
		typeof(Button)
	};

	[Test]
	public static void SupportedTypesTest() {
		var d = new ResourceDictionary{Source = new Uri("pack://application:,,,/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml")};
		var fe = new FrameworkElement {Resources = d};

		foreach (var fet in GetFrameworkElementTypes()) {
			var style = (Style)fe.TryFindResource(fet);
			var source = FindSource(fet, fe);
			var hasStyle = style!=null ? $"{{x:Type {fet.Name}}}" : "";
			var basedOnKey = GetKeyName(style?.BasedOn, fe) ?? "";
			Console.WriteLine($"{fet.Name,-32} {hasStyle} {basedOnKey} {source}");
		}
	}

	[Test]
	public static void SupportedTypesTest_DataGridCell() {
		var d = new ResourceDictionary{Source = new Uri("pack://application:,,,/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml")};
		var fe = new FrameworkElement {Resources = d};

		var fet = typeof(DataGridCell);
		var style = (Style)fe.TryFindResource(fet);
		var source = FindSource(fet, null);
		var hasStyle = style!=null ? $"{{x:Type {fet.Name}}}" : "";
		var basedOnKey = GetKeyName(style?.BasedOn, fe) ?? "";
		Console.WriteLine($"{fet.Name,-32} {hasStyle} {basedOnKey} {source}");
	}

	private static string? FindSource(object key, FrameworkElement fe) {
		if (key == null) return null;
		var resInfo = FindResource(key, fe);
		if (resInfo == null) return null;
		foreach (var d in resInfo.Hierarchy.AsEnumerable().Reverse()) {
			if (d.Source != null) return d.Source.ToString();
		}
		return null;
	}

	private static string? GetKeyName(Style? style, FrameworkElement fe) {
		if (style == null) return null;
		return FindResourceByValue(style, fe)?.Key switch {
			null => null,
			string s => s,
			Type t => $"{{x:Type {t.Name}}}",
			var o => o.ToString()
		};
	}

	[Test]
	public void StyleTest() {
		var d = ThemeResourceDictionary.Load("pack://application:,,,/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml");
		var e = new FrameworkElement {Resources = d};

		foreach (var controlType in SupportedTypes) {
			var buttonStyle = (Style) e.FindResource(typeof(Button));
			var expectedBaseStyle = (Style) e.FindResource($"Aero2Dark.{controlType.Name}Style");
			// Überprüfen, ob der Stil basierend auf dem angegebenen Basisstil ist
			Assert.That(buttonStyle, Is.Not.Null);
			Assert.That(buttonStyle.BasedOn, Is.EqualTo(expectedBaseStyle));
		}
	}



	public ResourceKey GetSystemColorResourceKey(string colorName) {
		var property = typeof(SystemColors).GetProperty(colorName, BindingFlags.Static | BindingFlags.Public);
		if (property == null) throw new ArgumentException($"No SystemColor with the name {colorName} found.");
		return (ResourceKey)property.GetValue(null);
	}

	public static IEnumerable<Type> GetFrameworkElementTypes() {
		var namespaces = new[] {
			"System.Windows.Controls",
			"System.Windows.Controls.Primitives"
		};
		var types = new List<Type>();
		foreach (var namespaceName in namespaces) {
			types.AddRange(Assembly.GetAssembly(typeof(FrameworkElement))
				.GetTypes()
				.Where(t => t.Namespace == namespaceName && !t.IsAbstract && typeof(FrameworkElement).IsAssignableFrom(t))
			);
		}
		return types;
	}

	private string[] GetSystemColorsKeyNames() {
		return typeof(SystemColors)
			.GetProperties()
			.Where(p => p.Name.EndsWith("BrushKey"))
			.Select(p => p.Name[..^8])
			.ToArray();
	}

	[Test]
	public void ThemeFonts_GenerateMapping() {
		foreach (var rootKey in GetSystemFontsKeyNames()) {
			var r0 = GetSystemFontsResourceKey($"{rootKey}Key");
			var k0 = $"{{x:Static SystemFonts.{rootKey}Key}}";
			var k1 = $"Aero2Dark.ThemeFonts.{rootKey}}}";
			var p = typeof(SystemFonts).GetProperty(rootKey, BindingFlags.Public | BindingFlags.Static);
			var v = p.GetValue(null);
//			Console.WriteLine($"{rootKey} {p.PropertyType.Name}");
			Console.WriteLine($"<{p.PropertyType.Name} x:Key=\"{k1}\">{v}</{p.PropertyType.Name}>");
		}
	}

	[Test]
	public void ThemeFonts_Mapping() {
		var d = ThemeResourceDictionary.Load("pack://application:,,,/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml");
		var e = new FrameworkElement {Resources = d};

		foreach (var rootKey in GetSystemFontsKeyNames()) {
			var r0 = e.TryFindResource(GetSystemFontsResourceKey($"{rootKey}Key"));
			var r1 = e.TryFindResource($"Aero2Dark.ThemeFonts.{rootKey}");
			Assert.That(r1,Is.Not.Null,$"Aero2Dark.ThemeFonts.{rootKey} not found");
			Assert.That(r1,Is.EqualTo(r0));
		}
	}

	private string[] GetSystemFontsKeyNames() {
		return typeof(SystemFonts)
			.GetProperties()
			.Where(p => p.Name.EndsWith("Key"))
			.Select(p => p.Name[..^3])
			.ToArray();
	}

	public ResourceKey GetSystemFontsResourceKey(string keyName) {
		var property = typeof(SystemFonts).GetProperty(keyName, BindingFlags.Static | BindingFlags.Public);
		if (property == null) throw new ArgumentException($"No SystemFontsKey with the name {keyName} found.");
		return (ResourceKey)property.GetValue(null);
	}


}