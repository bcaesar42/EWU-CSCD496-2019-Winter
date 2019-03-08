
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "We Know Better - We want underscores in our unit tests since they are not meant to be accessed directly except for testing")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Do not directly await a Task", Justification = "Await will always be called correctly in this project - no need to safeguard against improper calls")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "ToReturn in this case needs to be manually set, so setter is required", Scope = "member", Target = "~P:SecretSanta.Api.Tests.TestableGiftService.ToReturn")]