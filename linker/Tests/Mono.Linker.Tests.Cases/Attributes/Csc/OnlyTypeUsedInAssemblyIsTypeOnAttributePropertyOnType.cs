﻿using Mono.Linker.Tests.Cases.Attributes.Dependencies;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

[assembly: KeptAttributeAttribute (typeof (System.Diagnostics.DebuggableAttribute))]

namespace Mono.Linker.Tests.Cases.Attributes.Csc {
	/// <summary>
	/// This explicit csc test exists to ensure that csc adds references in this scenario
	/// </summary>
	[SetupCSharpCompilerToUse ("csc")]
	[SetupCompileBefore ("LibraryWithType.dll", new [] { typeof(TypeDefinedInReference) })]
	[SetupCompileBefore ("LibraryWithAttribute.dll", new [] { typeof(AttributeDefinedInReference) })]
	[KeptTypeInAssembly ("LibraryWithType.dll", typeof (TypeDefinedInReference))]
	[RemovedMemberInAssembly ("LibraryWithType.dll", typeof (TypeDefinedInReference), "Unused()")]
	[KeptMemberInAssembly ("LibraryWithAttribute.dll", typeof (AttributeDefinedInReference), ".ctor()")]
	[KeptMemberInAssembly ("LibraryWithAttribute.dll", typeof (AttributeDefinedInReference), "set_PropertyType(System.Type)")]
	public class OnlyTypeUsedInAssemblyIsTypeOnAttributePropertyOnType {
		public static void Main ()
		{
			var foo = new Foo ();
		}

		[Kept]
		[KeptMember (".ctor()")]
		[KeptAttributeAttribute (typeof (AttributeDefinedInReference))]
		[AttributeDefinedInReference (PropertyType = typeof (TypeDefinedInReference))]
		class Foo {
		}
	}
}