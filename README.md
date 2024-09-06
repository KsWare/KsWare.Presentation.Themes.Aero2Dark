# KsWare.Presentation.Themes.Aero2Dark

## Usage

### Application

app.xaml
```xml
    <Application.Resources>
         <ResourceDictionary>
             <ResourceDictionary.MergedDictionaries>
                 <aero2Dark:OverrideResources
				 	ThemeColors="Aero2Dark.Override.ThemeColors.xaml" -->
				 	ButtonColors="Aero2Dark.Override.ButtonColors.xaml" -->
				/>
	            <ThemeResourceDictionary ThemeName="Aero2Dark" Source="/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml"/>
             </ResourceDictionary.MergedDictionaries>
         </ResourceDictionary>
    </Application.Resources>
```

### Local

app.xaml *)
```xml
    <Application.Resources>
         <ResourceDictionary>
             <ResourceDictionary.MergedDictionaries>
                 <aero2Dark:OverrideResources
				 	ThemeColors="Aero2Dark.Override.ThemeColors.xaml" -->
				 	ButtonColors="Aero2Dark.Override.ButtonColors.xaml" -->
				/>
             </ResourceDictionary.MergedDictionaries>
         </ResourceDictionary>
    </Application.Resources>
```
*) OverrideResources only works in app.xaml


Window.xaml 
```xml
<Window 
  ...
  ThemeLoader.Source="/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml"
  ...>
 ```
  The ThemeLoader works with all ResourceDictionaries it is a shortcut for:
```xml
    <Window.Resources>
         <ResourceDictionary>
             <ResourceDictionary.MergedDictionaries>
	            <ThemeResourceDictionary ThemeName="Aero2Dark" Source="/KsWare.Presentation.Themes.Aero2Dark;component/Resources/Aero2Dark.NormalColor.xaml"/>
             </ResourceDictionary.MergedDictionaries>
         </ResourceDictionary>
    </Window.Resources>
```
