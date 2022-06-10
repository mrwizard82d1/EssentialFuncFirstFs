dotnet new sln -o Ch04
cd Ch04
mkdir src
dotnet new console -lang F# -o src/Ch04
dotnet sln add src/Ch04/Ch04.fsproj
mkdir tests
dotnet new xunit -lang F# -o tests/Ch04Tests
dotnet sln add tests/Ch04Tests/Ch04Tests.fsproj
cd tests/Ch04Tests
dotnet add reference ../../src/Ch04/Ch04.fsproj
dotnet add package FsUnit
dotnet add package FsUnit.XUnit
dotnet build
dotnet test

