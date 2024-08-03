﻿// See https://aka.ms/new-console-template for more information
using Microsoft.CodeAnalysis;
using CSScripting;
using CSScriptLib;

// publishing as a single-file with and without runtime
// (project file contains PublishSingleFile=true)
// dotnet publish --configuration Release --output .\publish --self-contained false
// dotnet publish --configuration Release --output .\publish --self-contained true

// ---------------------------
// evaluation of a simple expression
var div = CSScript.Evaluator.Eval("6/3");

Console.WriteLine("CSScript.Evaluator.Eval|expression: " + div);

// ---------------------------
// evaluation of a complex script
var calc = CSScript.Evaluator
                   .Eval(@"using System;
                           public class Script
                           {
                               public int Sum(int a, int b)
                               {
                                   Console.WriteLine(Settings.Value);
                                   return a+b;
                               }
                           }
                           return new Script();");

int sum = calc.Sum(1, 2);
Console.WriteLine("CSScript.Evaluator.Eval|class: " + sum);

// ---------------------------
// compilation of a regular C# code
var asm = CSScript.Evaluator
                   .CompileCode(@"using System;
                                 public class Script
                                 {
                                     public int Div(int a, int b)
                                     {
                                         return a/b;
                                     }
                                 }", new CompileInfo { CodeKind = SourceCodeKind.Script });

dynamic script = asm.CreateObject("*.Script");

Console.WriteLine("CSScript.Evaluator.CompileCode|class: " + script.Div(16, 2));

public class Settings
{
    public static string Value = "Host App value...";
}