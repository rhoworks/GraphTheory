# Graph Theory .NET

[![NuGet downloads](https://img.shields.io/nuget/dt/GraphTheory.svg)](https://www.nuget.org/packages/GraphTheory) [![Version](https://img.shields.io/nuget/v/GraphTheory.svg)](https://www.nuget.org/packages/GraphTheory)

A platform agnostic and fluent Graph library in C#.

Please see http://rho.works for more details. Major updates coming soon.

### Current Graph Model

The goal is to build a fluent API but bare with me--it's tricky. So far all you can do is insert some nodes. Sometime tomorrow I'll update it so you can add edges. Over the weekend you should be able to use a fluent API to create a simple graph. Afterward the fun will begin and you should be able to do cool stuff like select sub-graphs and merge two graphs together.

```cs
var g1 = new Graph< int >();
g1.Insert(5, 6, 7);

var g2 = new Graph< string >();
g2.Insert("a", "b", "c");

var g3 = new Graph< double >();
g3.Insert(1.0);
g3.Insert(3.14159);

```

Please contact me when you visit http://rho.works and fire a question my way on one of the posts.

I have been adding [documentation](documentation/QuickStartGuide.md) to help understand how to use this library. While its still early, it might be an interesting read, check it out.
