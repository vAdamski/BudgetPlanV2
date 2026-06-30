using System.Reflection;
using BudgetPlan.Api;
using BudgetPlan.Application;
using BudgetPlan.Contracts;
using BudgetPlan.Domain;
using BudgetPlan.Frontend;
using BudgetPlan.Persistence;

namespace BudgetPlan.Tests.Architecture;



public class LayerTests
{
    private const string ProjectPrefix = "BudgetPlan.";

    private static readonly Assembly DomainAssembly =
        typeof(DomainReference).Assembly;

    private static readonly Assembly ApplicationAssembly =
        typeof(ApplicationReference).Assembly;

    private static readonly Assembly PersistenceAssembly =
        typeof(PersistenceReference).Assembly;

    private static readonly Assembly ContractsAssembly =
        typeof(ContractsReference).Assembly;

    private static readonly Assembly ApiAssembly =
        typeof(ApiReference).Assembly;

    private static readonly Assembly BlazorAssembly =
        typeof(BlazorReference).Assembly;

    private static readonly IReadOnlyDictionary<string, Assembly> ProjectAssemblies =
        new Dictionary<string, Assembly>
        {
            [GetAssemblyName(DomainAssembly)] = DomainAssembly,
            [GetAssemblyName(ApplicationAssembly)] = ApplicationAssembly,
            [GetAssemblyName(PersistenceAssembly)] = PersistenceAssembly,
            [GetAssemblyName(ContractsAssembly)] = ContractsAssembly,
            [GetAssemblyName(ApiAssembly)] = ApiAssembly,
            [GetAssemblyName(BlazorAssembly)] = BlazorAssembly
        };

    public static TheoryData<string, string[]> AllowedDependencies =>
        new()
        {
            {
                GetAssemblyName(DomainAssembly),
                []
            },
            {
                GetAssemblyName(ApplicationAssembly),
                [
                    GetAssemblyName(DomainAssembly)
                ]
            },
            {
                GetAssemblyName(PersistenceAssembly),
                [
                    GetAssemblyName(ApplicationAssembly),
                    GetAssemblyName(DomainAssembly)
                ]
            },
            {
                GetAssemblyName(ContractsAssembly),
                []
            },
            {
                GetAssemblyName(ApiAssembly),
                [
                    GetAssemblyName(ApplicationAssembly),
                    GetAssemblyName(PersistenceAssembly),
                    GetAssemblyName(ContractsAssembly),
                    GetAssemblyName(DomainAssembly)
                ]
            },
            {
                GetAssemblyName(BlazorAssembly),
                [
                    GetAssemblyName(ContractsAssembly)
                ]
            }
        };

    [Theory]
    [MemberData(nameof(AllowedDependencies))]
    public void Project_Should_NotHaveForbiddenProjectDependencies(
        string projectName,
        string[] allowedDependencies)
    {
        // Arrange
        var testedAssembly = ProjectAssemblies[projectName];

        // Act
        var actualDependencies = testedAssembly
            .GetReferencedAssemblies()
            .Select(assemblyName => assemblyName.Name)
            .OfType<string>()
            .Where(name => name.StartsWith(
                ProjectPrefix,
                StringComparison.Ordinal))
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        var forbiddenDependencies = actualDependencies
            .Except(
                allowedDependencies,
                StringComparer.Ordinal)
            .OrderBy(name => name, StringComparer.Ordinal)
            .ToArray();

        // Assert
        Assert.True(
            forbiddenDependencies.Length == 0,
            $"""
             Projekt '{projectName}' posiada niedozwolone zależności:
             {FormatDependencies(forbiddenDependencies)}

             Dozwolone zależności:
             {FormatDependencies(allowedDependencies)}

             Wszystkie wykryte zależności:
             {FormatDependencies(actualDependencies)}
             """);
    }

    private static string GetAssemblyName(Assembly assembly)
    {
        return assembly.GetName().Name
               ?? throw new InvalidOperationException(
                   $"Assembly '{assembly.FullName}' nie posiada nazwy.");
    }

    private static string FormatDependencies(IEnumerable<string> dependencies)
    {
        var dependencyList = dependencies.ToArray();

        return dependencyList.Length == 0
            ? "- brak"
            : string.Join(
                Environment.NewLine,
                dependencyList.Select(dependency => $"- {dependency}"));
    }
}