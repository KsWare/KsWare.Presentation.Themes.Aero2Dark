using System;
using System.Runtime.CompilerServices;
using KsWare.Presentation.Resources.Core;

namespace KsWare.Presentation.Themes.Aero2Dark {

	public class OverrideResources : ResourceDictionaryEx {

		public Uri ThemeColors { get => GetItem<Uri>(); set => SetItem(value); }

		public Uri ButtonColors { get => GetItem<Uri>(); set => SetItem(value); }

		// TODO all the other Uris

		private static string Key(string callerMemberName) => $"Aero2Dark.Override.{callerMemberName}";

		private void SetItem<T>(T value, [CallerMemberName] string callerMemberName = null) {
			var key = Key(callerMemberName);
			if (!Contains(key)) Add(key,value);
			else this[key] = value;
		}

		private T GetItem<T>([CallerMemberName] string callerMemberName = null) {
			var key = Key(callerMemberName);
			if (!Contains(key)) return default;
			return (T) this[key];
		}

	}

}
