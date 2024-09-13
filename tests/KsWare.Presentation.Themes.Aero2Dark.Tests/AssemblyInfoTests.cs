using KsWare.Presentation.Themes.Aero2Dark;
namespace KsWare.Presentation.Themes.Aero2Dark.Tests;

public class AssemblyInfoTests {

	[Test]
	public void TestNamespace() {
		var assembly = AssemblyInfo.Assembly;
		var assemblyName = assembly.GetName(false).Name;
		var assemblyInfoNamespace = typeof(AssemblyInfo).Namespace;
		Assert.That(assemblyInfoNamespace, Is.EqualTo(assemblyName));
	}

}
