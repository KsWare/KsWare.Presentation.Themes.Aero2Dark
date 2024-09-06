using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using BF = System.Reflection.BindingFlags;

namespace KsWare.Presentation.Themes.Aero2Dark.Tests {

	public class CommonTests {

		[Test]
		public void CheckStyleableControls() {
			foreach (var type in GetStyleableControls()) {
				Console.WriteLine($"{type.FullName}");
			}
			Assert.Inconclusive();
		}


		private Type[] GetStyleableControls() {
			return typeof(Control).Assembly.GetTypes()
				.Where(t => typeof(FrameworkElement).IsAssignableFrom(t))
				.Where(t=>!t.Namespace.StartsWith("MS.Internal"))
				.Where(t=>t.GetProperties().Any(p=>p.PropertyType==typeof(Style)))
				.ToArray();
		}

		[Test]
		public void CheckKeys() {
			foreach (var type in GetControls()) {
				var keys = type.GetProperties(BF.Static|BF.Public|BF.FlattenHierarchy)
					.Where(p=>p.Name.EndsWith("Key"));
				foreach (var pi in keys) {
					Console.WriteLine($"{pi.DeclaringType.FullName}.{pi.Name} {pi.PropertyType.Name}");
				}
			}
			Assert.Inconclusive();
		}

		private Type[] GetControls() {
			return typeof(Control).Assembly.GetTypes()
				.Where(t => typeof(FrameworkElement).IsAssignableFrom(t))
				.ToArray();
		}

		[Test]
		public void SupportedTypesTest() {
			var assembly = AssemblyInfo.Assembly;
			var resourceNames = ReadResourceKeys().ToArray();
			var templatedElements = GetTemplatedElements().ToArray();
			var missingCount=0;
			var successCount=0;
			foreach (var type in templatedElements) {
				var resourceName = $"{type.Name}.baml";
				var found = resourceNames.Any(name => name.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase));
				if (!found) {
					Console.WriteLine($"Missing {type.Name}.xaml");
					missingCount++;
				}
				else {
					successCount++;
				}
			}

			if (missingCount > 0) {
				Assert.That(successCount,Is.EqualTo(templatedElements.Length),$"missing {missingCount} files");
			}
			else {
				Assert.Pass($"Success. All {successCount} types supported");
			}
			
		}

		private static IEnumerable<string> ReadResourceKeys() {
			// Die 'g.resources' Datei durchsuchen
			using var stream = AssemblyInfo.Assembly.GetManifestResourceStream($"{AssemblyInfo.Assembly.GetName().Name}.g.resources");
			if (stream == null) yield break;
			using ResourceReader reader = new ResourceReader(stream);
			foreach (DictionaryEntry entry in reader) {
				var resourceKey = (string)entry.Key;
				yield return resourceKey;
			}
		}

		public static IEnumerable<Type> GetTemplatedElements() {
			var assembly = typeof(Control).Assembly;

			var tFrameworkTemplate = typeof(FrameworkTemplate);
			var tStyle = typeof(Style);

			foreach (var type in assembly.GetTypes()) {
				if (type.IsAbstract) continue;
				if (type.ContainsGenericParameters) continue;
				if (type.GetConstructor(new Type[] { }) == null) continue;

				var properties = new List<PropertyInfo>();
				foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
					if (tFrameworkTemplate.IsAssignableFrom(prop.PropertyType))
						properties.Add(prop);
					if (tStyle.IsAssignableFrom(prop.PropertyType))
						properties.Add(prop);
				}
				if (properties.Count== 0) continue;
				if (properties.Count(p=>tFrameworkTemplate.IsAssignableFrom(p.PropertyType))==0) continue;
				if (properties.Count==1 && properties[0].Name=="FocusVisualStyle") continue;
				yield return type;
			}
		}
	}

}
