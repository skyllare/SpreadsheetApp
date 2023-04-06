// <copyright file="GlobalSuppressions.cs" company="Skyllar Estil">
// Copyright (c) Skyllar Estil. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "We did not learn the lambda format in class", Scope = "member", Target = "~E:SpreadsheetEngine.Spreadsheet.CellPropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1130:Use lambda syntax", Justification = "We did not learn the lambda format in class", Scope = "member", Target = "~E:SpreadsheetEngine.Cell.PropertyChanged")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Told to use protected in assignment", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.rowIndex")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Told to use protected in assignment", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.columnIndex")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Told to use protected in assignment", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.cellText")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "Told to use protected in assignment", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.cellValue")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:Field names should begin with lower-case letter", Justification = "This is the name given to use from assignment", Scope = "member", Target = "~F:SpreadsheetEngine.Cell.BGColor")]
