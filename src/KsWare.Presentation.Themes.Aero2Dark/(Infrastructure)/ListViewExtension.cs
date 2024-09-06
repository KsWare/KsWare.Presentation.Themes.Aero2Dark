using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KsWare.Presentation.Themes.Aero2Dark {

	public class ListViewExtension {

		private static GridView _gridView;

		public static readonly DependencyProperty ColumnHeaderContainerStyleProperty = DependencyProperty.RegisterAttached(
			"ColumnHeaderContainerStyle",
			typeof(Style),
			typeof(ListViewExtension),
			new PropertyMetadata(null, OnGridViewStyleChanged));

		public static void SetColumnHeaderContainerStyle(DependencyObject element, Style value) {
			element.SetValue(ColumnHeaderContainerStyleProperty, value);
		}

		public static Style GetColumnHeaderContainerStyle(DependencyObject element) {
			return (Style) element.GetValue(ColumnHeaderContainerStyleProperty);
		}

		private static void OnGridViewStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
//			if (d is ListView listView && e.NewValue is Style newStyle) {
//				var gridViewStyle = new Style(typeof(GridView)) {
//					BasedOn = newStyle
//				};
//				listView.Resources[typeof(GridView)] = gridViewStyle;
//			}
			if (d is ListView listView && e.NewValue is Style newStyle) {
				if (listView.View is GridView gridView) {
					gridView.ColumnHeaderContainerStyle = newStyle;
				}
			}
		}

	}

}
