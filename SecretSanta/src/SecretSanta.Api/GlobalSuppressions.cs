
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task", Justification = "Await will always be called correctly in this project - no need to safeguard against improper calls")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1721:Property names should not match get methods", Justification = "In this case the User, GetUser distinction is fine and not confusing because we implemented it cleanly. Unnecessary strictness.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1307:Specify StringComparison", Justification = "Do not require Globalization in this project")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "In this case we want the URL to be a string for flexibility reasons")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1060:Move pinvokes to native methods class", Justification = "Stack Walk is fine here, we trust those that have access to this class", Scope = "type", Target = "~T:SecretSanta.Api.Models.CurrentDirectoryHelpers")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "In this case we need the setter for flexibility and testing purposes when touching the DB")]