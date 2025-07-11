# LinqToXsdCore Release Notes

## Version 3.4.10
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.10
* https://www.nuget.org/packages/XObjectsCore/3.4.10
  * All types in a choice should be considered as optional: [#77](https://github.com/mamift/LinqToXsdCore/pull/77).

## Version 3.4.9
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.9
* https://www.nuget.org/packages/XObjectsCore/3.4.9
  * Fixes issue with new code generation facility from 3.4.8; see [#76](https://github.com/mamift/LinqToXsdCore/pull/76).

## Version 3.4.8
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.8
* https://www.nuget.org/packages/XObjectsCore/3.4.8
  * Added new code generation facility: Base type properties of the content type are not generated in wrapper types; see [#74](https://github.com/mamift/LinqToXsdCore/issues/74).

## Version 3.4.7
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.7
* https://www.nuget.org/packages/XObjectsCore/3.4.7
  * Fixed a bug with fixed enum values being generated as string, resulting in uncompilable code, see [#68](https://github.com/mamift/LinqToXsdCore/issues/68).

## Version 3.4.6
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.6
* https://www.nuget.org/packages/XObjectsCore/3.4.6
  * Fixed a bug where Timezone info was lost (conversions from DateTimeOffset to DateTime no longer occur). 

## Version 3.4.5
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.5
* https://www.nuget.org/packages/XObjectsCodeGen/3.2.5 (XObjectsCodeGen is an internal dependency used by LinqToXsdCore and is not meant to be used in shipping libraries).
  * The code generator will no longer generate  static `XName` fields for inherited properties. See [GitHUb PR65](https://github.com/mamift/LinqToXsdCore/pull/65).

## Version 3.4.4
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.4
* https://www.nuget.org/packages/XObjectsCore/3.4.4
  * Fixes an issue wtih simple types that add restrictions to `xs:date` or `xs:time` would not work with UseDateOnly or UseTimeOnly. See [GitHub PR61](https://github.com/mamift/LinqToXsdCore/pull/61).
  * Fixes a regression (**InvalidCastException**) when parsing `xs:date` or `xs:time` into a good old DateTime.
* https://www.nuget.org/packages/XObjectsCodeGen/3.2.4 (XObjectsCodeGen is an internal dependency used by XObjectsCore and LinqToXsdCore and is not meant to be used in shipping libraries).
  * Fixes an issue with generated C# model for lists of enums which does not compile because the type of the property is generated incorrectly as `List<string>` instead of `List<TEnum>`.

## Version 3.4.3
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.3
* https://www.nuget.org/packages/XObjectsCore/3.4.3
  * This release adds support for nillable elements, (where elements may have the following attribute `xsd:nil="true"` to indicate they are of nil value; which is an alternate way of modelling optional elements, rather than just omit the element entirely). See [GitHub PR60](https://github.com/mamift/LinqToXsdCore/pull/60).
* https://www.nuget.org/packages/XObjectsCodeGen/3.2.3 (XObjectsCodeGen is an internal dependency used by XObjectsCore and LinqToXsdCore and is not meant to be used in shipping libraries).
  * This release of XObjectsCodeGen has been updated to accommodate support for nillable elements described above.

## Version 3.4.2
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.2
	* Fixed a code generation bug (exhibited in XmlSpec schema (xmlspec.xsd)) when the simple type enum for xlink:type attribute was not being generated.
	* Fixed a code generation bug whereby the wrong type name was referenced when the property name and type name were the same, and the XML schema did not have [Element Form Default="qualified"] set and the property type was reflecting an XSD union type (like the xml:lang attribute) (exhibited in 'W3C XMLSchema v1.xsd').
	* Fixes a code generation bug with certain XName fields (that are only referenced in the static constructor of a type) were not getting created.
	* Backported .NET 6 `DateOnly` and `TimeOnly` type serialisation for `xs:date` and `xs:time` types to .NET Standard 2.0 using [Portable.System.DateTimeOnly](https://www.nuget.org/packages/Portable.System.DateTimeOnly); there remains a .NET 6 version and a .NET Standard 2.0 version of the XObjectsCore and LinqToXsd nuget packages, but are now functionally the same.


## Version 3.4.1
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.1
	* Fixes a null-reference error in the XListVisualizable which can cause debugger enumeration to fail in Visual Studio on an `XTypedElement` list.
	* The LinqToXsd code generator tool will now filter out XSDs that target v1.1 of XML Schema as .NET only supports v1.0.
	* Fixes a code generation bug that can be thrown in rare cases when the `xs:NCName` type is used in a schema.
	* The code generator will now generate a `value?.ToString()` expression on a property value setter when the property type is nullable.

## Version 3.4.0
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.4.0
	* `<SplitCodeFiles By="Namespace" />` now works, but it requires a new `File` filename attribute on every `<Namespace />`. **If you had set this unimplemented option in your config before upgrading, you will get an error and need to modify your config!**
	* `<NullableReferences>` option should now be set inside `<CodeGeneration>`. It still works in `<Configuration>` for backward compatibility.
	* New options `<UseDateOnly>` and `<UseTimeOnly>` inside `<CodeGeneration>`. When `true`, .net 6 `DateOnly` and `TimeOnly` types will be generated for `xs:date` and `xs:time` properties.
	* _Minor breaking change:_ if you used `XObjectsCodeGen` directly (as a library), a few public APIs have changed to support splitting files by namespace. There is no breaking change for users of LinqToXsd tool and `XObjectsCore` runtime.

## Version 3.3.3
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.3.3
	* Fixes an issue with the `NullableReferences` element not appearing in generated config files.

## Version 3.3.2
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.3.2
	* This update only applies to the LinqToXsd global `dotnet` tool; it fixes a bug whereby a class constructor was being generated for classes for XSD elements, whose schema types are simple types, and said simple types are defined as enum restrictions on string types (like `NMToken` or `xs:string`) and the class constructor accepted a value for the enum type. The functional constructor now converts from the given string value to the inner enum type by parsing the string value and convert it to it's proper inner enum type (stored in the TypedValue property).

## Version 3.3.1
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.3.1
	* This update only applies to the LinqToXsd global `dotnet` tool; it re-enables .NET Core 3.1 as a runtime target, and also adds .NET 5, alongside .NET 6 that was added in 3.3.0.
	* PDB debug information is now embedded inside the shipping assemblies.

## Version 3.3.0

Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.3.0
* https://www.nuget.org/packages/XObjectsCore/3.3.0
	* The LinqToXsd global tool now targets .NET 6 (note that the supporting library [XObjectsCore](https://www.nuget.org/packages/XObjectsCore/3.3.0) and all code generated by the LinqToXsd tool still targets .NET Standard 2.)
	* This new release fixes bugs with the content model and enum code generation. It incorporates contributions from [rvuistin](https://github.com/rvuistin) in [PR36](https://github.com/mamift/LinqToXsdCore/pull/36) (many thanks!).

## LinqToXsdCore 3.2.1 and XObjectsCodeGen 3.2.1 (no changes to XObjectsCore)
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.2.1
* https://www.nuget.org/packages/XObjectsCodeGen/3.2.1
	* Disabled checking for duplicate enum type declaration when creating a nested enum type for a property on a generated class (partially resolves [GitHub Issue 31](https://github.com/mamift/LinqToXsdCore/issues/31)).

## LinqToXsdCore 3.2.0, XObjectsCore 3.2.0 and XObjectsCodeGen 3.2.0
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.2.0
* https://www.nuget.org/packages/XObjectsCore/3.2.0
	* XObjectsCore has now had all the code generation facilities split into a separate library: XObjectsCodeGen. This change now means that generated code no longer needs to depend on System.CodeDom when it is used in shipping apps or libraries.
	* Fixes an error that occurs when attempting to pass null to a property that had validation logic in the property setter (see [GitHub PR28](https://github.com/mamift/LinqToXsdCore/pull/28)).
* https://www.nuget.org/packages/XObjectsCodeGen/3.2.0
	* The number of `XName` instantiations in generated code is now greatly reduced (see [GitHub PR23](https://github.com/mamift/LinqToXsdCore/pull/23)).
	* Now includes an option for null-annotating generated code (see [GitHub PR29](https://github.com/mamift/LinqToXsdCore/pull/29)).	
	* Enums in generated code are now properly generated as CLR value types (see [GitHub 21](https://github.com/mamift/LinqToXsdCore/pull/21)).
	* Updated to fix ambiguous types where an element is a base type and a derrived type is used (see [GitHub 16](https://github.com/mamift/LinqToXsdCore/pull/16)).

## LinqToXsdCore 3.1.0 and XObjectsCore 3.1.0
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.1.0
* https://www.nuget.org/packages/XObjectsCore/3.1.0
	* Fixed a enum type generation bug that occurs when referencing locally defined enum types.
	* Also fixed another one enum generation bug for multiple inline enum types defined with the same name; the XSD spec allows for multiple inline enum types to share the same name so long as they're not defined in the same complex type or element, and are not global types.
	* Fixed an issue with ambiguous types where an element is a base type and a derrived type is used.

## LinqToXsdCore 3.0.1 and XObjectsCore 3.0.1
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.0.1
* https://www.nuget.org/packages/XObjectsCore/3.0.1
	* Fixed [Github Issue #10](https://github.com/mamift/LinqToXsdCore/issues/10)
	* Imported code changesets for v2.0.2 from the legacy [LinqToXsd project](https://archive.codeplex.com/?p=linqtoxsd), which generates the proper type definitions for union types.
	* Switched to a tripartite versioning scheme.
	* The global tool LinqToXsd now targets .NET Core 3.1 in addition to .NET Core 2.1. This allows users using either version of .NET Core to still use the global tool to generate code.

## LinqToXsdCore 3.0.0.12 and XObjectsCore 3.0.0.11
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.0.0.12
* https://www.nuget.org/packages/XObjectsCore/3.0.0.11
	* When a group of XSD files or a folder of them all import or include each other, LinqToXsd cannot decide which one to use as the entry point for code generation, so now the CLI throws an exception when that condition is met while trying to resolve which XSD file to use.
	* Reverts "Avoid type name conflicts in generated code" from previous release, as it broke the code generation of the `BuildWrapperDictionary()` method generated inside the `LinqToXsdTypeManager`; it adds `typeof(void)` expressions, which breaks untyped `XElement` type conversion. Previous (and correct) behavior was to add `typeof(T)` expressions where T was the generated complex or global element type.
	* Fixes an issue whereby setting a string value to an attribute whose type was `AnyAtomicType` resulted in an error.
	* Fixes an issue when using the static Parse() or Load() methods on an internal generated type.

## LinqToXsdCore 3.0.0.11 and XObjectsCore 3.0.0.10
Nuget packages:
* https://www.nuget.org/packages/LinqToXsdCore/3.0.0.11
* https://www.nuget.org/packages/XObjectsCore/3.0.0.10
	* Avoid type name conflicts in generated code. 
	* Do not prefix an identifier with the '@' character when not needed.

## XObjectsCore 3.0.0.9
Nuget packages: 
* https://www.nuget.org/packages/XObjectsCore/3.0.0.9

Added `XTypedElementEqualityComparer` and `XTypedElementDeepEqualityComparer` classes that implement `IEqualityComparer{T}` for the `XTypedElement` class.

## LinqToXsdCore 3.0.0.10 and XObjectsCore 3.0.0.8
Nuget packages: 
* https://www.nuget.org/packages/XObjectsCore/3.0.0.8
* https://www.nuget.org/packages/LinqToXsdCore/3.0.0.10

Modified the behaviour of retrieving the value of an attribute, when the schema type is anyAtomicType (which is the default for attributes when no type is given). The value literal is now returned as a string (pre-existing behaviour would throw an exception saying that anyAtomicType is not a supported conversion to the CLR type 'string').

## XObjectsCore 3.0.0.7
Nuget packages: 
* https://www.nuget.org/packages/XObjectsCore/3.0.0.7
	* Fixed a regression bug with previous release.

## LinqToXsdCore 3.0.0.9 and XObjectsCore 3.0.0.6
Nuget packages: 
* https://www.nuget.org/packages/XObjectsCore/3.0.0.6
	* Fixed an issue when performing an explicit type conversion from XElement to its XTyped-equivalent when the XTyped-equivalent type was an internal class.
* https://www.nuget.org/packages/LinqToXsdCore/3.0.0.9
	* Generating a new config file no longers includes the Xml.Schema.Linq schema namespace mapping. Also generating a new config file will generate a default namespace mapping when the XSD does not target a namespace. 

## LinqToXsdCore 3.0.0.8
Nuget packages: 
* https://www.nuget.org/packages/LinqToXsdCore/3.0.0.8
	* Implemented saving merged output from multiple XSD files when generating a config file (using 'config -e' switch) using a folder as a source.

## XObjectsCore 3.0.0.5 and LinqToXsdCore 3.0.0.7
Nuget packages: 
* https://www.nuget.org/packages/XObjectsCore/3.0.0.5
	* Reversed a change made that removed the virtual keyword on properties on generated types. Added a test for it.
* https://www.nuget.org/packages/LinqToXsdCore/3.0.0.7
	* Dropped emitting errors to a custom handler. Was outputting red console text needlessly.

## XObjectsCore 3.0.0.4 and LinqToXsdCore 3.0.0.6
Nuget packages: 
* https://www.nuget.org/packages/XObjectsCore/3.0.0.4
* https://www.nuget.org/packages/LinqToXsdCore/3.0.0.6

Fixes a bug that caused XTypedElement.Clone() to fail when generated code had the `internal` visibility modifier. This manifested in the CLI tool, when attempting to use it to generate an example configuration file `linqtoxsd config 'file.xsd' -e`.